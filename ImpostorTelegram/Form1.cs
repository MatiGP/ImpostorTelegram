using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private Panel AppMainPanel, WelcomePanel;
        TextBox NameTextBox, SurnameTextBox;
        TableLayoutPanel MessegesLayoutPanel;
        Color mainBackgroundColor = Color.FromArgb(255, 109, 109, 109);
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
            AppMainPanel.Visible = false;
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

            AppMainPanel = new Panel();
            AppMainPanel.Dock = DockStyle.Fill;
            AppMainPanel.BackColor = mainBackgroundColor;
            AppMainPanel.AutoScroll = true;
            Controls.Add(AppMainPanel);

            #region WelcomeScreen
            WelcomePanel = new Panel();
            WelcomePanel.Dock = DockStyle.Fill;
            Controls.Add(WelcomePanel);

            Label AppNameLabel, NameLabel, SurnameLabel;
            AppNameLabel = new Label();
            NameLabel = new Label();
            SurnameLabel = new Label();
            AppNameLabel.AutoSize = false;
            AppNameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            AppNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            AppNameLabel.Text = "Impostor Telegram";
            AppNameLabel.Font = new Font("Century Gothic Bold", 30);

            NameLabel.AutoSize = false;
            NameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            NameLabel.TextAlign = ContentAlignment.MiddleCenter;
            NameLabel.Text = "Your Name";
            NameLabel.Font = new Font("Century Gothic", 14);

            SurnameLabel.AutoSize = false;
            SurnameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            SurnameLabel.TextAlign = ContentAlignment.MiddleCenter;
            SurnameLabel.Text = "Your Surname";
            SurnameLabel.Font = new Font("Century Gothic", 14);


            NameTextBox = new TextBox();
            SurnameTextBox = new TextBox();
            SurnameTextBox.Width = 130;
            NameTextBox.AutoSize = false;
            NameTextBox.Anchor =  AnchorStyles.Top;
            NameTextBox.Width = 130;
            SurnameTextBox.AutoSize = false;
            SurnameTextBox.Anchor = AnchorStyles.Top;

            Button CreateAccountButton = new Button();
            CreateAccountButton.Text = "Create";
            CreateAccountButton.Font = new Font("Century Gothic Bold", 14);
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
                NewUserMenu.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            NewUserMenu.Controls.Add(AppNameLabel);
            NewUserMenu.Controls.Add(NameLabel);
            NewUserMenu.Controls.Add(NameTextBox);
            NewUserMenu.Controls.Add(SurnameLabel);
            NewUserMenu.Controls.Add(SurnameTextBox);
            NewUserMenu.Controls.Add(CreateAccountButton);

            WelcomePanel.Controls.Add(NewUserMenu);

            #endregion

            #region MessagesScreen
            MessegesLayoutPanel = new TableLayoutPanel();
            MessegesLayoutPanel.BackColor = mainBackgroundColor;
            MessegesLayoutPanel.Dock = DockStyle.Top;
            MessegesLayoutPanel.ColumnCount = 1;
            MessegesLayoutPanel.Height = 6;
            MessegesLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            Label messagesScreenLabel = new Label();
            messagesScreenLabel.Text = "Impostor Telegram";
            messagesScreenLabel.Font = new Font("Century Gothic Bold", 30);
            messagesScreenLabel.AutoSize = false;
            messagesScreenLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            messagesScreenLabel.TextAlign = ContentAlignment.MiddleCenter;
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

            AppMainPanel.Controls.Add(MessegesLayoutPanel);
            #endregion

            OnFirstLaunch();
        }

        private void OnCreateButtonClick(object sender, EventArgs e)
        {
            if (NameTextBox.TextLength > 0 && SurnameTextBox.TextLength > 0)
            {
                WelcomePanel.Visible = false;
                AppMainPanel.Visible = true;
            }
            else
            {
                MessageBox.Show("Your Name and Surname can't be null");
            }
        }
    }
}
