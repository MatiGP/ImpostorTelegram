using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ;
using RabbitMQ.Client.Events;
using System.Text.Json;
using Newtonsoft.Json;

namespace ImpostorTelegram
{
    public static class RabbitUtils
    {

        public static IModel CreateConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory() {
                HostName = "localhost"
                
            };

            IConnection connection = connectionFactory.CreateConnection();
            return connection.CreateModel();        
        }

        public static byte[] CreateEncodedMessage(string message)
        {
            return Encoding.UTF8.GetBytes(message);
        }

        public static byte[] CreateEncodedImage(Image image)
        {
            using(MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);                
                return memoryStream.ToArray();
            }                   
        }
        
        public static byte[] CreateEncodedSound(string soundFilePath)
        {
            return File.ReadAllBytes(soundFilePath);
        }

        public static Image GetDecodedImage(byte[] imageByteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(imageByteArray, 0, imageByteArray.Length))
            {
                memoryStream.Write(imageByteArray, 0, imageByteArray.Length);

                return Image.FromStream(memoryStream);
            }
        }

        public static Message GetDecodedMessage(byte[] message)
        {
            string jsonSerializedString = Encoding.UTF8.GetString(message, 0, message.Length);
            return JsonConvert.DeserializeObject<Message>(jsonSerializedString);
        }

        public static string GetDecodedString(byte[] message)
        {
            return Encoding.UTF8.GetString(message, 0, message.Length);
        }

        public static byte[] LobbyEnteredMessage(string userCreds)
        {
            Message message = new Message() { Author = userCreds, MessageType = EMessageType.UserEnter, MessageText = null };
            string jsonString = JsonConvert.SerializeObject(message);

            return CreateEncodedMessage(jsonString);
        }

        public static byte[] LobbyExitMessage(string userCreds)
        {
            Message message = new Message() { Author = userCreds, MessageType = EMessageType.UserExit, MessageText = null };
            string jsonString = JsonConvert.SerializeObject(message);

            return CreateEncodedMessage(jsonString);
        }

        public static byte[] RequestUserBan(string userCreds, string roomName)
        {
            Message message = new Message() { Author = userCreds,
                MessageText = CreateEncodedMessage(roomName),
                MessageType = EMessageType.UserBanned 
            };

            string jsonString = JsonConvert.SerializeObject(message);

            return CreateEncodedMessage(jsonString);
        }

        public static void DeclareQueueExchange(IModel channel, string exchangeName, string type)
        {
            channel.ExchangeDeclare(exchangeName, type, false, false, null);
        }

        public static void BindExchangeToQueue(IModel channel, string exchangeName, string queueName)
        {
            channel.QueueBind(queueName, exchangeName, "", null);                    
        }

        public static byte[] GetBanSystemMessage(string user)
        {
            byte[] banMessage = CreateEncodedMessage($"User {user} terminated.");

            Message message = new Message()
            {
                Author = "[SYSTEM]",
                MessageText = banMessage,
                MessageType = EMessageType.Text
            };

            string jsonString = JsonConvert.SerializeObject(message);

            return CreateEncodedMessage(jsonString);
        }

        public static byte[] GetUserJoinedSystemMessage(string user)
        {
            byte[] joinMessage = CreateEncodedMessage($"{user} joined chat.");

            Message message = new Message()
            {
                Author = "[SYSTEM]",
                MessageText = joinMessage,
                MessageType = EMessageType.Text
            };

            string jsonString = JsonConvert.SerializeObject(message);

            return CreateEncodedMessage(jsonString);
        }


    }

    public enum EMessageType
    {
        //DEFAULT
        None = -1,

        //MESSAGES
        Text = 0,
        Image = 1,
        Sound = 2,
        
        //LOGS
        UserEnter = 3,
        UserExit = 4,
        UserBanned = 5,
        UserJoined = 6
    }   
}
