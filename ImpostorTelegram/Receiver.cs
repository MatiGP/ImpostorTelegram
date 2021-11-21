using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpostorTelegram
{
    class Receiver
    {
        public event EventHandler<Message> OnMessageReceived;

        private IModel m_Channel = null;
        private EventingBasicConsumer m_EventingBasicConsumer = null;
        
        public Receiver()
        {
            m_Channel = RabbitUtils.CreateConnection();

            

            m_Channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            m_EventingBasicConsumer = new EventingBasicConsumer(m_Channel);
            m_EventingBasicConsumer.Received += HandleMessageReceived;

            m_Channel.BasicConsume("hello", true, m_EventingBasicConsumer);
            
        }
        private void HandleMessageReceived(object sender, BasicDeliverEventArgs deliverArgs)
        {
            byte[] message = deliverArgs.Body.ToArray();
            Message receivedMessage = RabbitUtils.GetDecodedMessage(message);

            OnMessageReceived?.Invoke(this, receivedMessage);
        }     
    }
}
