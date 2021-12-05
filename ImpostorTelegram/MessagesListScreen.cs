using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ImpostorTelegram
{
    class MessagesListScreen : TableLayoutPanel
    {
        public static MessagesListScreen Instance;
        private Dictionary<string, ChatButton> m_MessageButtons = new Dictionary<string, ChatButton>();

        private EventingBasicConsumer m_EventingBasicConsumer = null;
        private IModel model = null;
        private TableLayoutPanel onlineUsersScrollUi = null;

        public MessagesListScreen()
        {
            Instance = this;
            model = RabbitUtils.CreateConnection();

            model.QueueDeclare(queue: Constants.DEFAULT_LOBBY_NAME,
                               durable: true,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

            model.ExchangeDeclare(Constants.DEFAULT_LOBBY_EXCHANGE, "fanout");
            model.QueueBind(Constants.DEFAULT_LOBBY_NAME, Constants.DEFAULT_LOBBY_EXCHANGE, "");

            m_EventingBasicConsumer = new EventingBasicConsumer(model);
            m_EventingBasicConsumer.Received += HandleMessageReceived;
            model.BasicConsume(Constants.DEFAULT_LOBBY_NAME, false, m_EventingBasicConsumer);

            for (int i = 0; i < model.ConsumerCount(Constants.DEFAULT_LOBBY_NAME); i++)
            {
                BasicGetResult basicGetResult = model.BasicGet(Constants.DEFAULT_LOBBY_NAME, false);

                if (basicGetResult == null) continue;

                byte[] vs = basicGetResult.Body.ToArray();

                Message m = RabbitUtils.GetDecodedMessage(vs);

                switch (m.MessageType)
                {
                    case EMessageType.UserEnter:
                        AddUserButton(m.Author);
                        break;
                    case EMessageType.UserExit:
                        RemoveUserButton(m.Author);
                        break;
                }
            }
            SetUpView();
        }

        private void HandleMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            byte[] message = e.Body.ToArray();
            Message receivedMessage = RabbitUtils.GetDecodedMessage(message);

            switch (receivedMessage.MessageType)
            {
                case EMessageType.UserEnter:
                    AddUserButton(receivedMessage.Author);
                    break;
                case EMessageType.UserExit:
                    RemoveUserButton(receivedMessage.Author);
                    break;              
            }
            
        }

        private void SetUpView()
        {
            Dock = DockStyle.Fill;

            BackColor = Constants.MAIN_BACKGROUND_COLOR;
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

            GroupButton groupButton = new GroupButton();
            bottomGroupPanel.Controls.Add(groupButton);
        }

        private void AddUserButton(string userName)
        {
            ChatButton newButton = new ChatButton(userName);
            m_MessageButtons.Add(userName, newButton);

            onlineUsersScrollUi.Invoke(new Action(() =>
            {
                onlineUsersScrollUi.Controls.Add(newButton);
            }));          
        }

        private void RemoveUserButton(string userName)
        {
            if (m_MessageButtons.ContainsKey(userName))
            {
                ChatButton chatButton = m_MessageButtons[userName];

                chatButton.Invoke(new Action(() => {
                    chatButton.Dispose();
                    Refresh();
                }));

                m_MessageButtons.Remove(userName);
            }
        }
    }
}
