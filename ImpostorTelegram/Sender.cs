using System;
using RabbitMQ.Client;
using System.Text;

namespace ImpostorTelegram
{
    class Sender
    {
        private IModel channel;
        public Sender()
        {
            channel = RabbitUtils.CreateConnection();
        }

        public void SendSimpleMessage(string message)
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            byte[] body = RabbitUtils.CreateEncodedMessage(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
        }


    }
}
