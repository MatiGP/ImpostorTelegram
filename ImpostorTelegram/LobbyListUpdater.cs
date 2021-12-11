using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ImpostorTelegram
{
    class LobbyListUpdater
    {       
        public event EventHandler<Message> OnUpdate;

        private IModel m_LobbbyChannel = null;
        private EventingBasicConsumer m_EventingConsumer = null;

        public LobbyListUpdater()
        {
            m_LobbbyChannel = RabbitUtils.CreateConnection();
            m_LobbbyChannel.ExchangeDeclare(Constants.DEFAULT_LOBBY_EXCHANGE_NAME,
                Constants.EXCHANGE_TYPES[EExchangeType.Fanout],
                true, 
                false, 
                null);
        }

        public void EnterLobby(string userCreds)
        {
            m_LobbbyChannel.BasicPublish(Constants.DEFAULT_LOBBY_EXCHANGE_NAME, "", null, RabbitUtils.LobbyEnteredMessage(userCreds));            
        }

        public void LeaveLobby(string userCreds)
        {
            m_LobbbyChannel.BasicPublish(Constants.DEFAULT_LOBBY_EXCHANGE_NAME, "", null, RabbitUtils.LobbyExitMessage(userCreds));
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

            switch (message.MessageType)
            {
                case EMessageType.UserEnter:
                    DatabaseUtils.AddNewUserToDatabase(message);
                    break;
                case EMessageType.UserExit:
                    DatabaseUtils.RemoveUserFromDatabase(message);
                    break;

            }   

           OnUpdate?.Invoke(this, message);
        }
     
        public void GetPreviousUsers()
        {
            MySqlDataReader reader = DatabaseUtils.GetOnlineUsers();

            while (reader.Read())
            {
                Message dummyMess = new Message() { Author = reader.GetString(0), MessageType = EMessageType.UserEnter };
                OnUpdate.Invoke(this, dummyMess);
            }

            reader.Close();
            reader.Dispose();
        }
    }
}
