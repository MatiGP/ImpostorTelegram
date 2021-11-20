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
            AppMainPanel.BackColor = Color.Red;
            Controls.Add(AppMainPanel);

            #region WelcomeScreen
            WelcomePanel = new Panel();
            WelcomePanel.Dock = DockStyle.Fill;
            WelcomePanel.BackColor = Color.Green;
            Controls.Add(WelcomePanel);

            Label AppNameLabel, NameLabel, SurnameLabel;
            AppNameLabel = new Label();
            NameLabel = new Label();
            SurnameLabel = new Label();
            AppNameLabel.AutoSize = false;
            AppNameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            AppNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            AppNameLabel.Text = "Impostor Telegram";

            NameLabel.AutoSize = false;
            NameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            NameLabel.TextAlign = ContentAlignment.MiddleCenter;
            NameLabel.Text = "Your Name";

            SurnameLabel.AutoSize = false;
            SurnameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            SurnameLabel.TextAlign = ContentAlignment.MiddleCenter;
            SurnameLabel.Text = "Your Surname";

            NameTextBox = new TextBox();
            SurnameTextBox = new TextBox();
            NameTextBox.AutoSize = false;
            NameTextBox.Anchor =  AnchorStyles.Top;
            SurnameTextBox.AutoSize = false;
            SurnameTextBox.Anchor = AnchorStyles.Top;

            Button CreateAccountButton = new Button();
            CreateAccountButton.Text = "Create";
            CreateAccountButton.AutoSize = false;
            CreateAccountButton.Anchor = AnchorStyles.Top;
            CreateAccountButton.Click += OnCreateButtonClick;

            TableLayoutPanel NewUserMenu = new TableLayoutPanel();
            NewUserMenu.BackColor = Color.Yellow;
            NewUserMenu.Padding = new Padding(50, 100, 50, 100);
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
