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
    class MessageListUpdater
    {       
        public event EventHandler<Message> OnUpdate;

        private IModel m_LobbbyChannel = null;
        private EventingBasicConsumer m_EventingConsumer = null;

        private string m_Server = null;
        private string m_DataBase = null;
        private string m_UserID = null;
        private string m_Password = null;
        private MySqlConnection m_Connection = null;
        

        public MessageListUpdater()
        {
            m_LobbbyChannel = RabbitUtils.CreateConnection();
            m_LobbbyChannel.ExchangeDeclare(Constants.DEFAULT_LOBBY_EXCHANGE_NAME,
                Constants.EXCHANGE_TYPES[EExchangeType.Fanout],
                true, 
                false, 
                null);

            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
            m_Server = "localhost";
            m_DataBase = "userdatabase";
            m_UserID = "root";
            m_Password = "";
            
            string connectionString = string.Format(Constants.DB_CONNECTION_STRING_FORMAT, m_Server, m_DataBase, m_UserID, m_Password);
            m_Connection = new MySqlConnection(connectionString);

            OpenConnection();
        }

        private bool OpenConnection()
        {
            try
            {
                m_Connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Nie można połączyć się z serwerem");
                        break;
                    case 1045:
                        MessageBox.Show("Złe hasło / login");
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
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
                    AddNewUserToDatabase(message);
                    break;
                case EMessageType.UserExit:
                    RemoveUserFromDatabase(message);
                    break;

            }   

           OnUpdate?.Invoke(this, message);
        }

        private void AddNewUserToDatabase(Message message)
        {
            MySqlCommand cmd = new MySqlCommand("AddOnlineUser", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter parameter = new MySqlParameter("newUser", message.Author);
            cmd.Parameters.Add(parameter);
            cmd.ExecuteNonQuery();
        }

        private void RemoveUserFromDatabase(Message message)
        {
            MySqlCommand cmd = new MySqlCommand("RemoveUserFromOnline", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter parameter = new MySqlParameter("newUser", message.Author);
            cmd.Parameters.Add(parameter);
            cmd.ExecuteNonQuery();
        }

        public void GetPreviousUsers()
        {
            MySqlCommand cmd = new MySqlCommand("GetAllOnlineUsers", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Message dummyMess = new Message() { Author = reader.GetString(0), MessageType = EMessageType.UserEnter };
                OnUpdate.Invoke(this, dummyMess);
            }

            reader.Dispose();
        }
    }
}
