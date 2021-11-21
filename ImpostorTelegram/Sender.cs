using System;
using RabbitMQ.Client;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Drawing;

namespace ImpostorTelegram
{
    class Sender
    {
        public string User => $"{m_UserName} {m_UserSurname}";
     
        private string m_UserName = "";
        private string m_UserSurname = "";
        
        private IModel m_Channel;

        public Sender(string name, string surname)
        {
            m_Channel = RabbitUtils.CreateConnection();
            m_UserName = name;
            m_UserSurname = surname;
            
        }

        public void SendTextMessage(string message)
        {
            m_Channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Message mess = new Message()
            {
                Author = $"{m_UserName} {m_UserSurname}",
                MessageText = RabbitUtils.CreateEncodedMessage(message),
                MessageType = EMessageType.Text
            };

            string stringMess = JsonConvert.SerializeObject(mess);
         
            byte[] body = RabbitUtils.CreateEncodedMessage(stringMess);

            m_Channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
           
        }

        public void SendImageMessage(Image image)
        {
            m_Channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Message mess = new Message()
            {
                Author = $"{m_UserName} {m_UserSurname}",
                MessageText = RabbitUtils.CreateEncodedImage(image),
                MessageType = EMessageType.Image
            };

            string stringMess = JsonConvert.SerializeObject(mess);

            byte[] body = RabbitUtils.CreateEncodedMessage(stringMess);

            m_Channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
        }

    }
    public struct Message
    {
        public byte[] MessageText;
        public EMessageType MessageType;
        public string Author;
    }
}

