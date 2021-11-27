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

        public MessagesListScreen()
        {
            Instance = this;
            model = RabbitUtils.CreateConnection();

            model.QueueDeclare(queue: Constants.DEFAULT_LOBBY_NAME,
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

            model.ExchangeDeclare(Constants.DEFAULT_LOBBY_EXCHANGE, "fanout");
            model.QueueBind(Constants.DEFAULT_LOBBY_NAME, Constants.DEFAULT_LOBBY_EXCHANGE, "");

            m_EventingBasicConsumer = new EventingBasicConsumer(model);
            m_EventingBasicConsumer.Received += HandleMessageReceived;

            model.BasicConsume(Constants.DEFAULT_LOBBY_NAME, false, m_EventingBasicConsumer);
            
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

        public void makeVisible()
        {
            Visible = true;
        }

        private void AddUserButton(string userName)
        {
            ChatButton newButton = new ChatButton(userName);
            m_MessageButtons.Add(userName, newButton);

            Invoke(new Action(() =>
            {
                RowStyles.Add(new RowStyle(SizeType.Absolute, Constants.MESSAGE_BUTTON_HEIGHT));
                Controls.Add(newButton);
                Height += Constants.MESSAGE_BUTTON_HEIGHT;
            }));          
        }

        private void RemoveUserButton(string userName)
        {
            if (m_MessageButtons.ContainsKey(userName))
            {
                ChatButton chatButton = m_MessageButtons[userName];
                chatButton.Dispose();

                m_MessageButtons.Remove(userName);
            }
        }


    }
}
