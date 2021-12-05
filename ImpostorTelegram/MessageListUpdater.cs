using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpostorTelegram
{

    class MessageListUpdater
    {
        public event EventHandler<Message> OnUpdate;

        private IModel m_LobbbyChannel = null;
        private EventingBasicConsumer m_EventingConsumer = null;
        public MessageListUpdater()
        {
            m_LobbbyChannel = RabbitUtils.CreateConnection();
            m_LobbbyChannel.ExchangeDeclare(Constants.DEFAULT_LOBBY_EXCHANGE_NAME,
                Constants.EXCHANGE_TYPES[EExchangeType.Fanout],
                true, 
                false, 
                null);
        }

        public void Publish(string userCreds)
        {
            m_LobbbyChannel.BasicPublish(Constants.DEFAULT_LOBBY_EXCHANGE_NAME, "", null, RabbitUtils.LobbyEnteredMessage(userCreds));            
        }

        public void BindUserQueue(string userCreds)
        {
            m_LobbbyChannel.QueueBind($"{userCreds}_LobbyListener", Constants.DEFAULT_LOBBY_EXCHANGE_NAME, "", null);
            m_EventingConsumer = new EventingBasicConsumer(m_LobbbyChannel);
            m_EventingConsumer.Received += HandleMessageReceived;
            m_LobbbyChannel.BasicConsume($"{userCreds}_LobbyListener", false, m_EventingConsumer);
        }

        private void HandleMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            Message message = RabbitUtils.GetDecodedMessage(e.Body.ToArray());
            OnUpdate?.Invoke(this, message);
        }    
    }
}
