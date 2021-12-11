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
        public IModel Channel => m_Channel;

        private string m_User = string.Empty;
        
        private IModel m_Channel;

        public Sender(string user)
        {          
            m_Channel = RabbitUtils.CreateConnection();
            m_User = user;

            m_Channel.QueueDeclare(queue: m_User,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            m_Channel.QueueDeclare($"{m_User}_LobbyListener", true, false, false, null);                        
        }
      
        public void SendTextMessage(string message, string destination)
        {
            Message mess = new Message()
            {
                Author = m_User,
                MessageText = RabbitUtils.CreateEncodedMessage(message),
                MessageType = EMessageType.Text
            };

            string stringMess = JsonConvert.SerializeObject(mess);
         
            byte[] body = RabbitUtils.CreateEncodedMessage(stringMess);

            m_Channel.BasicPublish(exchange: destination,
                                 routingKey: m_User,
                                 basicProperties: null,
                                 body: body);

            DatabaseUtils.AddMessageToChatHistory(destination, stringMess);
        }

        public void SendImageMessage(Image image, string destination)
        {        
            Message mess = new Message()
            {
                Author = m_User,
                MessageText = RabbitUtils.CreateEncodedImage(image),
                MessageType = EMessageType.Image
            };

            string stringMess = JsonConvert.SerializeObject(mess);

            byte[] body = RabbitUtils.CreateEncodedMessage(stringMess);

            m_Channel.BasicPublish(exchange: destination,
                                 routingKey: m_User,
                                 basicProperties: null,
                                 body: body);
        }

        public void SendSoundMessage(byte[] soundByteArray, string destination)
        {           
            Message mess = new Message()
            {
                Author = m_User,
                MessageText = soundByteArray,
                MessageType = EMessageType.Sound
            };

            string stringMess = JsonConvert.SerializeObject(mess);

            byte[] body = RabbitUtils.CreateEncodedMessage(stringMess);

            m_Channel.BasicPublish(exchange: destination,
                                 routingKey: m_User,
                                 basicProperties: null,
                                 body: body);
        }

        public void SendBanRequest(string destination, string userToBan)
        {
            byte[] banMessage = RabbitUtils.GetBanSystemMessage(userToBan);

            m_Channel.BasicPublish(exchange: destination,
                                 routingKey: m_User,
                                 basicProperties: null,
                                 body: banMessage);

            byte[] body = RabbitUtils.RequestUserBan(userToBan, destination);

            m_Channel.BasicPublish(exchange: destination,
                                 routingKey: m_User,
                                 basicProperties: null,
                                 body: body);
            
            DatabaseUtils.BanUserFromRoom(userToBan, destination);
        }

        public void SendJoinMessage(string roomName)
        {
            byte[] body = RabbitUtils.GetUserJoinedSystemMessage(m_User);
            
            m_Channel.BasicPublish(exchange: roomName,
                                 routingKey: m_User,
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

