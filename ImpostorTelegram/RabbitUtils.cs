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

            IConnection connection = connectionFactory.CreateConnection(connectionFactory.ClientProvidedName);
            return connection.CreateModel();        
        }
        
        public static byte[] CreateEncodedMessage(string message)
        {
            return Encoding.UTF8.GetBytes(message);
        }

        public static byte[] CreateEncodedMessage(Message message)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(message);
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
    }

    public enum EMessageType
    {
        None = -1,
        Text = 0,
        Image = 1,
        Sound = 2
    }   
}
