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
        public string User => m_User;

        private string m_User = string.Empty;
        
        private IModel m_Channel;

        public Sender(string user)
        {
            m_Channel = RabbitUtils.CreateConnection();
            m_User = user;
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
                Author = m_User,
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
                Author = m_User,
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

        public void SendSoundMessage(string pathToSound)
        {
            m_Channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Message mess = new Message()
            {
                Author = m_User,
                MessageText = RabbitUtils.CreateEncodedSound(pathToSound),
                MessageType = EMessageType.Sound
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

