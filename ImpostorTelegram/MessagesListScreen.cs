using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ImpostorTelegram
{
    class MessagesListScreen : TableLayoutPanel
    {
        public event EventHandler<string> OnUserSelected;

        private Dictionary<string, ChatButton> m_MessageButtons = new Dictionary<string, ChatButton>();

        public MessagesListScreen()
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
            Dock = DockStyle.Top;
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
            Height += 80;
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

            Invoke(new Action(() =>
            {
                RowStyles.Add(new RowStyle(SizeType.Absolute, Constants.MESSAGE_BUTTON_HEIGHT));
                Controls.Add(newButton);
                Height += Constants.MESSAGE_BUTTON_HEIGHT;
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

                chatButton.Invoke(new Action(() =>
                {
                    chatButton.Dispose();
                    Refresh();
                }));

                m_MessageButtons.Remove(userName);
            }
        }


    }
}
