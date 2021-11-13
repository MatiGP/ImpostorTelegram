using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImpostorTelegram
{
    public static class RabbitUtils
    {
        public static IModel CreateConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            IConnection connection = connectionFactory.CreateConnection();
            return connection.CreateModel();        
        }

        public static byte[] CreateEncodedMessage(string message)
        {
            return Encoding.UTF8.GetBytes(message);
        }

        public static string GetEncodedMessage(byte[] message)
        {
            return Encoding.UTF8.GetString(message);
        }

        public static void SendMessage(IModel channel, byte[] decodedMessage)
        {
            channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, null);
            channel.BasicPublish(exchange: "", "hello", null, decodedMessage);
        }






    }
}
