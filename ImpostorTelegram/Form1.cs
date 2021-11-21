using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImpostorTelegram
{
    public partial class ImpostorTelegram : Form
    {
        private Receiver m_Receiver = null;
        private Sender m_Sender = null;

        Font globalNormalFont = new Font("Century Gothic", 14);
        Font globalBigFont = new Font("Century Gothic Bold", 30);
        Panel appMainPanel, welcomePanel, inMessageUiPanel;
        TextBox nameTextBox, surnameTextBox;
        Color mainBackgroundColor = Color.FromArgb(255, 37, 39, 77);
        Color secondaryBackgroudColor = Color.FromArgb(255, 38, 40, 102);
        Color fontColor = Color.White;
       
        public ImpostorTelegram()
        {
            InitializeComponent();

            m_Receiver = new Receiver();
            m_Sender = new Sender();

            m_Receiver.OnMessageReceived += HandleMessageReceived;
        }
        void OnFirstLaunch()
        {
            if(true)
            {
                HideAllChats();

            }
        }
        void HideAllChats()
        {
            appMainPanel.Visible = false;
            ////////////tymczasowo//////////////////////////////////////////////////////
            welcomePanel.Visible = false;
        }
        private void HandleMessageReceived(object sender, string e)
        {
            //ChatReceiver.Invoke(new Action(() => { ChatReceiver.Text = e; }));
        }

        private void SendMessage_Button_Click(object sender, EventArgs e)
        {
            //m_Sender.SendMessage(ChatText.Text);
        }

        private void ImpostorTelegram_Load(object sender, EventArgs e)
        {
            #region Form1Settings
            MinimumSize = new Size(400, 600);
            #endregion

            appMainPanel = new Panel();
            appMainPanel.Dock = DockStyle.Fill;
            appMainPanel.BackColor = mainBackgroundColor;
            appMainPanel.AutoScroll = true;
            Controls.Add(appMainPanel);

            #region WelcomeScreen
            welcomePanel = new Panel();
            welcomePanel.Dock = DockStyle.Fill;
            Controls.Add(welcomePanel);

            Label AppNameLabel, NameLabel, SurnameLabel;
            AppNameLabel = new Label();
            NameLabel = new Label();
            SurnameLabel = new Label();
            AppNameLabel.AutoSize = false;
            AppNameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            AppNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            AppNameLabel.Text = "Impostor Telegram";
            AppNameLabel.Font = globalBigFont;
            AppNameLabel.ForeColor = fontColor;

            NameLabel.AutoSize = false;
            NameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            NameLabel.TextAlign = ContentAlignment.MiddleCenter;
            NameLabel.Text = "Your Name";
            NameLabel.Font = globalNormalFont;
            NameLabel.ForeColor = fontColor;

            SurnameLabel.AutoSize = false;
            SurnameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            SurnameLabel.TextAlign = ContentAlignment.MiddleCenter;
            SurnameLabel.Text = "Your Surname";
            SurnameLabel.Font = globalNormalFont;
            SurnameLabel.ForeColor = fontColor;

            surnameTextBox = new TextBox();
            surnameTextBox.Width = 130;
            surnameTextBox.AutoSize = false;
            surnameTextBox.Anchor = AnchorStyles.Top;
            
            nameTextBox = new TextBox();
            nameTextBox.AutoSize = false;
            nameTextBox.Anchor =  AnchorStyles.Top;
            nameTextBox.Width = 130;
           
            Button CreateAccountButton = new Button();
            CreateAccountButton.Text = "Create";
            CreateAccountButton.Font = globalBigFont;
            CreateAccountButton.AutoSize = false;
            CreateAccountButton.Anchor = AnchorStyles.Top;
            CreateAccountButton.Click += OnCreateButtonClick;
            CreateAccountButton.Width = 130;
            CreateAccountButton.Height = 40;
            CreateAccountButton.FlatStyle = FlatStyle.Flat;
            CreateAccountButton.FlatAppearance.BorderSize = 0;
            CreateAccountButton.BackColor = Color.FromArgb(255, 166, 166, 166);

            TableLayoutPanel NewUserMenu = new TableLayoutPanel();
            NewUserMenu.BackColor = mainBackgroundColor;
            NewUserMenu.Padding = new Padding(0, 100, 0, 100);
            NewUserMenu.Dock = DockStyle.Fill;
            NewUserMenu.ColumnCount = 1;
            NewUserMenu.RowCount = 6;
            NewUserMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            
            for (int i = 0; i < 6; i++)
            {
                NewUserMenu.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            }               
            
            NewUserMenu.Controls.Add(AppNameLabel);
            NewUserMenu.Controls.Add(NameLabel);
            NewUserMenu.Controls.Add(nameTextBox);
            NewUserMenu.Controls.Add(SurnameLabel);
            NewUserMenu.Controls.Add(surnameTextBox);
            NewUserMenu.Controls.Add(CreateAccountButton);

            welcomePanel.Controls.Add(NewUserMenu);

            #endregion

            #region MessagesScreen
            TableLayoutPanel MessegesLayoutPanel;
            MessegesLayoutPanel = new TableLayoutPanel();
            MessegesLayoutPanel.BackColor = mainBackgroundColor;
            MessegesLayoutPanel.Dock = DockStyle.Top;
            MessegesLayoutPanel.ColumnCount = 1;
            MessegesLayoutPanel.Height = 6;
            MessegesLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            Label messagesScreenLabel = new Label();
            messagesScreenLabel.Text = "Impostor Telegram";
            messagesScreenLabel.Font = globalBigFont;
            messagesScreenLabel.AutoSize = false;
            messagesScreenLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            messagesScreenLabel.TextAlign = ContentAlignment.MiddleCenter;
            messagesScreenLabel.ForeColor = fontColor;
            MessegesLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            MessegesLayoutPanel.Controls.Add(messagesScreenLabel);
            MessegesLayoutPanel.Height += 80;


            //////////////////////////////
            ChatButton testButton = new ChatButton("Kacper", "Kotecki");
            testButton.Width = 300;
            ChatButton testButton2 = new ChatButton("Mateusz", "Świeca");
            testButton2.Width = 300;
            testButton2.Location = new Point(0, 80);
            ChatButton testButton3 = new ChatButton("Lech", "Kaczyński");
            testButton2.Width = 300;
            testButton2.Location = new Point(0, 80);
            MessegesLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            MessegesLayoutPanel.Controls.Add(testButton);
            MessegesLayoutPanel.Height += 80;
            MessegesLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            MessegesLayoutPanel.Controls.Add(testButton2);
            MessegesLayoutPanel.Height += 80;
            MessegesLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            MessegesLayoutPanel.Controls.Add(testButton3);
            MessegesLayoutPanel.Height += 80;
            /////////////////////////////

            appMainPanel.Controls.Add(MessegesLayoutPanel);
            #endregion


            #region InChatUi
            inMessageUiPanel = new Panel();
            inMessageUiPanel.Dock = DockStyle.Fill;
            inMessageUiPanel.BackColor = mainBackgroundColor;
            inMessageUiPanel.AutoScroll = true;
            Controls.Add(inMessageUiPanel);

            Panel statusBarPanel = new Panel();
            statusBarPanel.BackColor = mainBackgroundColor;
            statusBarPanel.Dock = DockStyle.Fill;
            statusBarPanel.Margin = new Padding(0);

            PictureBox UserAvatar = new PictureBox();
            UserAvatar.BackColor = Color.Transparent;
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\contacts.png");
            UserAvatar.Image = Image.FromFile(fullPath);
            UserAvatar.Width = 74;
            UserAvatar.Height = 74;
            UserAvatar.Location = new Point(6, 6);
            UserAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            statusBarPanel.Controls.Add(UserAvatar);

            Label userName = new Label();
            userName.Text = $"Imię i Nazwisko z kim rozmawiasz";
            userName.Font = globalNormalFont;
            userName.BackColor = Color.Transparent;
            userName.AutoSize = true;
            userName.Padding = new Padding(100, 30, 5, 5);
            userName.ForeColor = fontColor;
            statusBarPanel.Controls.Add(userName);



            TableLayoutPanel messageScrollUi;
            messageScrollUi = new TableLayoutPanel();
            messageScrollUi.BackColor = secondaryBackgroudColor;
            messageScrollUi.Dock = DockStyle.Fill;
            messageScrollUi.ColumnCount = 1;
            messageScrollUi.Margin = new Padding(0);




            PictureBox uploadIcon = new PictureBox();
            uploadIcon.BackColor = Color.Transparent;
            string uploadIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\upload.png");
            uploadIcon.Image = Image.FromFile(uploadIconPath);
            uploadIcon.Width = 70;
            uploadIcon.Height = 70;
            uploadIcon.Location = new Point(5, 5);
            uploadIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            TextBox typeMessageUiTextbox = new TextBox();
            typeMessageUiTextbox.Multiline = true;
            typeMessageUiTextbox.Dock = DockStyle.Fill;
            typeMessageUiTextbox.Font = globalNormalFont;

            PictureBox sendIcon = new PictureBox();
            sendIcon.BackColor = Color.Transparent;
            string sendIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\send.png");
            sendIcon.Image = Image.FromFile(sendIconPath);
            sendIcon.Width = 70;
            sendIcon.Height = 70;
            sendIcon.Location = new Point(5, 5);
            sendIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            TableLayoutPanel sendMessagePanel = new TableLayoutPanel();
            sendMessagePanel.BackColor = mainBackgroundColor;
            sendMessagePanel.Dock = DockStyle.Fill;
            sendMessagePanel.Margin = new Padding(0);
            sendMessagePanel.ColumnCount = 3;
            sendMessagePanel.RowCount = 1;
            sendMessagePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            sendMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            sendMessagePanel.Controls.Add(uploadIcon);
            sendMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            sendMessagePanel.Controls.Add(typeMessageUiTextbox);
            sendMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            sendMessagePanel.Controls.Add(sendIcon);

            TableLayoutPanel inMessageUiTablePanel;
            inMessageUiTablePanel = new TableLayoutPanel();
            inMessageUiTablePanel.BackColor = mainBackgroundColor;
            inMessageUiTablePanel.Dock = DockStyle.Fill;
            inMessageUiTablePanel.ColumnCount = 1;
            inMessageUiTablePanel.Height = 6;
            inMessageUiTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            inMessageUiTablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            inMessageUiTablePanel.Controls.Add(statusBarPanel);

            inMessageUiTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            inMessageUiTablePanel.Controls.Add(messageScrollUi);
            inMessageUiTablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            inMessageUiTablePanel.Controls.Add(sendMessagePanel);

            inMessageUiPanel.Controls.Add(inMessageUiTablePanel);

            #endregion

            OnFirstLaunch();
        }

        private void OnCreateButtonClick(object sender, EventArgs e)
        {
            if (nameTextBox.TextLength > 0 && surnameTextBox.TextLength > 0)
            {
                welcomePanel.Visible = false;
                appMainPanel.Visible = true;
            }
            else
            {
                MessageBox.Show("Your Name and Surname can't be null");
            }
        }
    }
}
