using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ImpostorTelegram
{
    class LobbyList : TableLayoutPanel
    {
        public event EventHandler<string> OnUserSelected;
        public event EventHandler OnGroupButtonPressed;

        private Dictionary<string, ChatButton> m_MessageButtons = new Dictionary<string, ChatButton>();

        private GroupButton m_GroupButton = null;
        private TableLayoutPanel onlineUsersScrollUi = null;

        public LobbyList()
        {
            SetUpView();
        }

        public void UpdateUsers(object sender, Message message)
        {           
            switch (message.MessageType)
            {
                case EMessageType.UserEnter:
                    AddUserButton(message.Author);
                    break;
                case EMessageType.UserExit:
                    RemoveUserButton(message.Author);
                    break;
            }
        }

        private void SetUpView()
        {
            BackColor = Constants.MAIN_BACKGROUND_COLOR;
            Dock = DockStyle.Fill;
            ColumnCount = 1;
            Height = 6;
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            Label messagesScreenLabel = new Label();
            messagesScreenLabel.Text = "Impostor Telegram";
            messagesScreenLabel.Font = Constants.GLOBAL_BIG_FONT;
            messagesScreenLabel.AutoSize = false;
            messagesScreenLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            messagesScreenLabel.TextAlign = ContentAlignment.MiddleCenter;
            messagesScreenLabel.ForeColor = Constants.FONT_COLOR;
            RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            Controls.Add(messagesScreenLabel);
           

            m_GroupButton = new GroupButton();

            onlineUsersScrollUi = new TableLayoutPanel();
            onlineUsersScrollUi.BackColor = Constants.SECONDARY_BACKGROUND_COLOR;
            onlineUsersScrollUi.Dock = DockStyle.Fill;
            onlineUsersScrollUi.Margin = new Padding(0);
            onlineUsersScrollUi.AutoScroll = true;
            onlineUsersScrollUi.AutoSize = true;
            onlineUsersScrollUi.BackColor = Constants.MAIN_BACKGROUND_COLOR;


            RowStyles.Add(new RowStyle(SizeType.Percent, 80));
            Controls.Add(onlineUsersScrollUi);

            Panel bottomGroupPanel = new Panel();
            bottomGroupPanel.Dock = DockStyle.Fill;

            RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            Controls.Add(bottomGroupPanel);
         
            bottomGroupPanel.Controls.Add(m_GroupButton);

            m_GroupButton.OnGroupButtonPressed += HandleGroupButtonPressed;
        }

        private void HandleGroupButtonPressed(object sender, EventArgs e)
        {
            OnGroupButtonPressed.Invoke(this, EventArgs.Empty);
        }

        public void SaveUserName(string userName)
        {
            m_MessageButtons.Add(userName, null);
        }

        private void AddUserButton(string userName)
        {
            if (m_MessageButtons.ContainsKey(userName))
            {
                return;
            }

            ChatButton newButton = new ChatButton(userName);
            m_MessageButtons.Add(userName, newButton);

            newButton.OnButtonPressed += HandleNewButtonPressed;

            onlineUsersScrollUi.Invoke(new Action(() =>
            {
                onlineUsersScrollUi.Controls.Add(newButton);
            }));
        }

        private void HandleNewButtonPressed(object sender, string userCreds)
        {
            OnUserSelected.Invoke(this, userCreds);
        }

        private void RemoveUserButton(string userName)
        {
            if (m_MessageButtons.ContainsKey(userName))
            {
                ChatButton chatButton = m_MessageButtons[userName];

                chatButton?.Invoke(new Action(() =>
                {
                    chatButton.Dispose();
                    Refresh();
                }));

                m_MessageButtons.Remove(userName);
            }
        }


    }
}
