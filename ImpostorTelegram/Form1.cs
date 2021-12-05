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
        private MessagesListScreen m_MessagesListScreen = null;
        private ChatUiScreen m_ChatUiScreen = null;
        private MessageListUpdater m_MessageListUpdater = null;

        public ImpostorTelegram()
        {
            InitializeComponent();           
        }
        
        private void ImpostorTelegram_Load(object sender, EventArgs e)
        {
            m_MessageListUpdater = new MessageListUpdater();

            MinimumSize = new Size(400, 600);
            Size = new Size(400, 600);
            BackColor = Constants.MAIN_BACKGROUND_COLOR;

            m_LoginScreen = new LoginScreen();          
            Controls.Add(m_LoginScreen);
            m_LoginScreen.Visible = true;

            m_MessagesListScreen = new MessagesListScreen();
            Controls.Add(m_MessagesListScreen);
            m_MessagesListScreen.Visible = false;

            m_ChatUiScreen = new ChatUiScreen();
            Controls.Add(m_ChatUiScreen);
            m_ChatUiScreen.Visible = false;

            BindEvents();
        }

        private void HandleSuccesfulLogin(object sender, string userCreds)
        {
            m_Sender = new Sender(userCreds);           
            m_Receiver = new Receiver(userCreds);
            m_Receiver.OnMessageReceived += HandleMessageReceived;

            m_MessageListUpdater.GetPrevUsers();
            m_MessageListUpdater.BindUserQueue(userCreds);
            m_MessageListUpdater.Publish(userCreds);

            m_MessagesListScreen.Visible = true;
            m_LoginScreen.Visible = false;
        }

        private void BindEvents()
        {
            m_LoginScreen.OnSuccesfulLogin += HandleSuccesfulLogin;
           
            m_ChatUiScreen.OnTextMessageSent += HandleTextMessageSent;
            m_ChatUiScreen.OnImageMessageSent += HandleImageMessageSent;
            m_ChatUiScreen.OnBackPressed += HandleChatUIBackPressed;
           
            m_MessagesListScreen.OnUserSelected += HandleChatSelected;
            
            m_MessageListUpdater.OnUpdate += m_MessagesListScreen.UpdateUsers;
        }

        private void HandleChatSelected(object sender, string userName)
        {
            m_ChatUiScreen.OpenChat(userName);
            m_ChatUiScreen.Visible = true;
            m_MessagesListScreen.Visible = false;
        }

        private void HandleChatUIBackPressed(object sender, EventArgs e)
        {
            m_ChatUiScreen.Visible = false;
            m_MessagesListScreen.Visible = true;
        }

        private void HandleImageMessageSent(object sender, Image imageToSend)
        {
            m_Sender.SendImageMessage(imageToSend);
        }

        private void HandleTextMessageSent(object sender, string textToSend)
        {
            m_Sender.SendTextMessage(textToSend);
        }

        private void HandleMessageReceived(object sender, Message receivedMessage)
        {
            m_ChatUiScreen.AddMessageToUi(receivedMessage);
        }

        private void ImpostorTelegram_FormClosing(object sender, FormClosingEventArgs e)
        {
            //m_LobbyConnectionManager.LeaveLobby(m_Sender.User);
        }   
    }
}
