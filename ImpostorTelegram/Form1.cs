using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace ImpostorTelegram
{
    public partial class ImpostorTelegram : Form
    {
        private Receiver m_Receiver = null;
        private Sender m_Sender = null;

        private LoginScreen m_LoginScreen = null;
        private LobbyList m_LobbyList = null;
        private ChatUiScreen m_ChatUiScreen = null;
        private LobbyListUpdater m_LobbyListUpdater = null;
        private RoomJoinScreen m_RoomJoinScreen = null;
        private DatabaseUtils m_DatabaseUtils = null;

        private string m_CurrentRoomChat = null;

        public ImpostorTelegram()
        {
            InitializeComponent();           
        }
        
        private void ImpostorTelegram_Load(object sender, EventArgs e)
        {
            m_DatabaseUtils = new DatabaseUtils();

            m_LobbyListUpdater = new LobbyListUpdater();

            MinimumSize = new Size(400, 600);
            Size = new Size(400, 600);
            BackColor = Constants.MAIN_BACKGROUND_COLOR;

            m_LoginScreen = new LoginScreen();          
            Controls.Add(m_LoginScreen);
            m_LoginScreen.Visible = true;

            m_LobbyList = new LobbyList();
            Controls.Add(m_LobbyList);
            m_LobbyList.Visible = false;

            m_ChatUiScreen = new ChatUiScreen();
            Controls.Add(m_ChatUiScreen);
            m_ChatUiScreen.Visible = false;

            m_RoomJoinScreen = new RoomJoinScreen();
            Controls.Add(m_RoomJoinScreen);
            m_RoomJoinScreen.Visible = false;

            BindEvents();
        }

        private void HandleSuccesfulLogin(object sender, string userCreds)
        {
            m_Sender = new Sender(userCreds);           
            m_Receiver = new Receiver(userCreds);
            m_Receiver.OnMessageReceived += HandleMessageReceived;

            m_LobbyList.SaveUserName(userCreds);

            m_LobbyListUpdater.GetPreviousUsers();
            m_LobbyListUpdater.BindUserQueue(userCreds);
            m_LobbyListUpdater.EnterLobby(userCreds);

            m_LobbyList.Visible = true;
            m_LoginScreen.Visible = false;
        }

        private void BindEvents()
        {
            m_LoginScreen.OnSuccesfulLogin += HandleSuccesfulLogin;
           
            m_ChatUiScreen.OnTextMessageSent += HandleTextMessageSent;
            m_ChatUiScreen.OnImageMessageSent += HandleImageMessageSent;
            m_ChatUiScreen.OnBackPressed += HandleChatUIBackPressed;
           
            m_LobbyList.OnUserSelected += HandleChatSelected;
            
            m_LobbyListUpdater.OnUpdate += m_LobbyList.UpdateUsers;
            m_LobbyList.OnGroupButtonPressed += HandleGroupButtonPressed;

            m_RoomJoinScreen.OnRoomCreated += HandleRoomCreated;
        }

        private void HandleGroupButtonPressed(object sender, EventArgs e)
        {
            m_LobbyList.Visible = false;
            m_RoomJoinScreen.Visible = true;
        }

        private void HandleRoomCreated(object sender, string roomName)
        {
            RabbitUtils.DeclareQueueExchange(m_Sender.Channel, roomName, Constants.EXCHANGE_TYPES[EExchangeType.Fanout]);
            RabbitUtils.BindExchangeToQueue(m_Sender.Channel, roomName, m_Sender.User);
            m_ChatUiScreen.OpenChat(roomName);
            m_CurrentRoomChat = roomName;

        }

        private void HandleChatSelected(object sender, string targetUserName)
        {
            string chatRoomName = string.Empty;            
            if(DatabaseUtils.Is1To1ChatRoomCreated(m_Sender.User, targetUserName, out chatRoomName))
            {
                RabbitUtils.DeclareQueueExchange(m_Sender.Channel, chatRoomName, Constants.EXCHANGE_TYPES[EExchangeType.Fanout]);
                RabbitUtils.BindExchangeToQueue(m_Sender.Channel, chatRoomName, m_Sender.User);
                m_CurrentRoomChat = chatRoomName;
            }
            else
            {
                string localChatRoomName = string.Format(Constants.DB_CHATROOM_STRING_FORMAT, m_Sender.User, targetUserName);
                
                DatabaseUtils.Create1To1ChatRoom(localChatRoomName);
                RabbitUtils.DeclareQueueExchange(m_Sender.Channel, localChatRoomName, Constants.EXCHANGE_TYPES[EExchangeType.Fanout]);
                RabbitUtils.BindExchangeToQueue(m_Sender.Channel, localChatRoomName, m_Sender.User);
                m_CurrentRoomChat = localChatRoomName;
            }

            m_ChatUiScreen.OpenChat(m_CurrentRoomChat);

            m_ChatUiScreen.Visible = true;
            m_LobbyList.Visible = false;
        }

        private void HandleChatUIBackPressed(object sender, EventArgs e)
        {
            m_ChatUiScreen.Visible = false;
            m_LobbyList.Visible = true;
        }

        private void HandleImageMessageSent(object sender, Image imageToSend)
        {
            m_Sender.SendImageMessage(imageToSend, m_CurrentRoomChat);
        }

        private void HandleTextMessageSent(object sender, string textToSend)
        {
            m_Sender.SendTextMessage(textToSend, m_CurrentRoomChat);
        }

        private void HandleMessageReceived(object sender, Message receivedMessage)
        {
            m_ChatUiScreen.AddMessageToUi(receivedMessage);
        }

        private void ImpostorTelegram_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(m_Sender != null)
            {
                m_LobbyListUpdater.LeaveLobby(m_Sender.User);
            }        
        }   
    }
}
