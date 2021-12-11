using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using ImpostorTelegram.MessageUI;
using MySql.Data.MySqlClient;

namespace ImpostorTelegram
{
    class ChatUiScreen : TableLayoutPanel
    {
        public event EventHandler OnBackPressed;

        public event EventHandler<string> OnTextMessageSent;
        public event EventHandler<Image> OnImageMessageSent;

        private Label m_UserName;
        PictureBox m_BackIcon;
        PictureBox m_UploadIcon;
        TextBox m_MessageText;
        PictureBox m_SendIcon;
        FlowLayoutPanel m_MessageScrollUI;

        private string m_ChatName = null;

        public ChatUiScreen()
        {
            #region Design Stuff
            Dock = DockStyle.Fill;
            BackColor = Constants.MAIN_BACKGROUND_COLOR;
            AutoScroll = true;


            TableLayoutPanel statusBarPanel = new TableLayoutPanel();
            statusBarPanel.BackColor = Constants.MAIN_BACKGROUND_COLOR;
            statusBarPanel.Dock = DockStyle.Fill;
            statusBarPanel.Margin = new Padding(0);
            statusBarPanel.ColumnCount = 3;
            statusBarPanel.RowCount = 1;
            statusBarPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            PictureBox UserAvatar = new PictureBox();
            UserAvatar.BackColor = Color.Transparent;
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\contacts.png");
            UserAvatar.Image = Image.FromFile(fullPath);
            UserAvatar.Width = 74;
            UserAvatar.Height = 74;
            UserAvatar.Location = new Point(6, 6);
            UserAvatar.SizeMode = PictureBoxSizeMode.StretchImage;

            m_UserName = new Label();
            m_UserName.Text = $"Imię i Nazwisko z kim rozmawiasz";
            m_UserName.Font = Constants.GLOBAL_NORMAL_FONT;
            m_UserName.BackColor = Color.Transparent;
            m_UserName.AutoSize = true;
            m_UserName.Padding = new Padding(20, 30, 5, 5);
            m_UserName.ForeColor = Constants.FONT_COLOR;

            m_BackIcon = new PictureBox();
            m_BackIcon.BackColor = Color.Transparent;
            string backIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\back.png");
            m_BackIcon.Image = Image.FromFile(backIconPath);
            m_BackIcon.Width = 74;
            m_BackIcon.Height = 74;
            m_BackIcon.Location = new Point(6, 6);
            m_BackIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            m_BackIcon.Click += BackIconClick;
            m_BackIcon.MouseEnter += OnMouseEnter;
            m_BackIcon.MouseLeave += OnMouseLeave;


            statusBarPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            statusBarPanel.Controls.Add(UserAvatar);
            statusBarPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            statusBarPanel.Controls.Add(m_UserName);
            statusBarPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            statusBarPanel.Controls.Add(m_BackIcon);

            m_MessageScrollUI = new FlowLayoutPanel();
            m_MessageScrollUI.BackColor = Constants.SECONDARY_BACKGROUND_COLOR;
            m_MessageScrollUI.Dock = DockStyle.Fill;
            m_MessageScrollUI.Margin = new Padding(0);
            m_MessageScrollUI.AutoScroll = true;
            m_MessageScrollUI.WrapContents = false;
            m_MessageScrollUI.FlowDirection = FlowDirection.TopDown;           

            m_UploadIcon = new PictureBox();
            m_UploadIcon.BackColor = Color.Transparent;
            string uploadIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\upload.png");
            m_UploadIcon.Image = Image.FromFile(uploadIconPath);
            m_UploadIcon.Width = 70;
            m_UploadIcon.Height = 70;
            m_UploadIcon.Location = new Point(5, 5);
            m_UploadIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            m_UploadIcon.Click += UploadIconButton;
            m_UploadIcon.MouseEnter += UploadMouseEnter;
            m_UploadIcon.MouseLeave += UploadMouseLeave;

            m_MessageText = new TextBox();
            m_MessageText.Multiline = true;
            m_MessageText.Dock = DockStyle.Fill;
            m_MessageText.Font = Constants.GLOBAL_NORMAL_FONT;

            m_SendIcon = new PictureBox();
            m_SendIcon.BackColor = Color.Transparent;
            string sendIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\send.png");
            m_SendIcon.Image = Image.FromFile(sendIconPath);
            m_SendIcon.Width = 70;
            m_SendIcon.Height = 70;
            m_SendIcon.Location = new Point(5, 5);
            m_SendIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            m_SendIcon.Click += sendIconClick;
            m_SendIcon.MouseEnter += SendIconMouseEnter;
            m_SendIcon.MouseLeave += SendIconMouseLeave;

            TableLayoutPanel sendMessagePanel = new TableLayoutPanel();
            sendMessagePanel.BackColor = Constants.MAIN_BACKGROUND_COLOR;
            sendMessagePanel.Dock = DockStyle.Fill;
            sendMessagePanel.Margin = new Padding(0);
            sendMessagePanel.ColumnCount = 3;
            sendMessagePanel.RowCount = 1;
            sendMessagePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            sendMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            sendMessagePanel.Controls.Add(m_UploadIcon);
            sendMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            sendMessagePanel.Controls.Add(m_MessageText);
            sendMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            sendMessagePanel.Controls.Add(m_SendIcon);

            TableLayoutPanel inMessageUiTablePanel;
            inMessageUiTablePanel = new TableLayoutPanel();
            inMessageUiTablePanel.BackColor = Constants.MAIN_BACKGROUND_COLOR;
            inMessageUiTablePanel.Dock = DockStyle.Fill;
            inMessageUiTablePanel.ColumnCount = 1;
            inMessageUiTablePanel.Height = 6;
            inMessageUiTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            inMessageUiTablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            inMessageUiTablePanel.Controls.Add(statusBarPanel);
            inMessageUiTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            inMessageUiTablePanel.Controls.Add(m_MessageScrollUI);
            inMessageUiTablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            inMessageUiTablePanel.Controls.Add(sendMessagePanel);
            inMessageUiTablePanel.AutoScroll = true;
            Controls.Add(inMessageUiTablePanel);

            #endregion

        }

        private void UploadMouseLeave(object sender, EventArgs e)
        {
            m_UploadIcon.BackColor = Color.Transparent;
        }

        private void UploadMouseEnter(object sender, EventArgs e)
        {
            m_UploadIcon.BackColor = Constants.HIGHLIGHT_BACKGROUND_COLOR;
        }

        private void SendIconMouseLeave(object sender, EventArgs e)
        {
            m_SendIcon.BackColor = Color.Transparent;
        }

        private void SendIconMouseEnter(object sender, EventArgs e)
        {
            m_SendIcon.BackColor = Constants.HIGHLIGHT_BACKGROUND_COLOR;
        }

        public void AddMessageToUi(Message message)
        {
            MessageUi messageUI = null;

            switch (message.MessageType)
            {
                case EMessageType.Text:
                    messageUI = new TextMessageUI(message);
                    break;
                case EMessageType.Sound:
                    messageUI = new SoundMessageUI(message);
                    break;
                case EMessageType.Image:
                    messageUI = new ImageMessageUI(message);
                    break;
            }
            m_MessageScrollUI.Invoke(new Action(() =>
            {
                m_MessageScrollUI.Controls.Add(messageUI);
            }));
        }

        private void UploadIconButton(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png)|*.png";

            string filePath = string.Empty;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;

                //Stream fileStream = openFileDialog.OpenFile();
                OnImageMessageSent?.Invoke(this, Image.FromFile(filePath));
            }
        }

        private void sendIconClick(object sender, EventArgs e)
        {
            OnTextMessageSent?.Invoke(this, m_MessageText.Text);
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            m_BackIcon.BackColor = Color.Transparent;
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            m_BackIcon.BackColor = Constants.HIGHLIGHT_BACKGROUND_COLOR;
        }

        private void BackIconClick(object sender, EventArgs e)
        {
            Visible = false;
            OnBackPressed?.Invoke(this, EventArgs.Empty);
        }

        public void OpenChat(string name)
        {
            m_UserName.Text = name;
            Visible = true;
            m_ChatName = name;
            LoadPreviousMessages(name);
        }
        
        private void LoadPreviousMessages(string ID)
        {
            m_MessageScrollUI.Invoke(new Action(() =>
            {
                m_MessageScrollUI.Controls.Clear();
            }));

            MySqlDataReader previousMessages = DatabaseUtils.GetPreviousMessages(ID);

            while (previousMessages.Read())
            {
                Message mess = JsonConvert.DeserializeObject<Message>(previousMessages.GetString(0));
                AddMessageToUi(mess);
            }

            previousMessages.Close();
            previousMessages.Dispose();
        }
    }
}
