using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpostorTelegram
{
    class DatabaseUtils
    {
        private string m_Server = null;
        private string m_DataBase = null;
        private string m_UserID = null;
        private string m_Password = null;
        private static MySqlConnection m_Connection = null;

        public DatabaseUtils()
        {
            ConnectToDatabase();
            OpenConnection();
        }

        private void ConnectToDatabase()
        {
            m_Server = "localhost";
            m_DataBase = "userdatabase";
            m_UserID = "root";
            m_Password = "";

            string connectionString = string.Format(Constants.DB_CONNECTION_STRING_FORMAT, m_Server, m_DataBase, m_UserID, m_Password);
            m_Connection = new MySqlConnection(connectionString);
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

        public static void AddNewUserToDatabase(Message message)
        {
            MySqlCommand cmd = new MySqlCommand("AddOnlineUser", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter parameter = new MySqlParameter("newUser", message.Author);
            cmd.Parameters.Add(parameter);
            cmd.ExecuteNonQuery();
        }

        public static void RemoveUserFromDatabase(Message message)
        {
            MySqlCommand cmd = new MySqlCommand("RemoveUserFromOnline", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter parameter = new MySqlParameter("newUser", message.Author);
            cmd.Parameters.Add(parameter);
            cmd.ExecuteNonQuery();
        }

        public static MySqlDataReader GetOnlineUsers()
        {
            MySqlCommand cmd = new MySqlCommand("GetAllOnlineUsers", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd.ExecuteReader();
        }

        public static void AddMessageToChatHistory(string queueID, string message)
        {
            MySqlCommand cmd = new MySqlCommand("AddMessageToChatHistory", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            MySqlParameter channelID = new MySqlParameter("channelID", queueID);
            MySqlParameter mess = new MySqlParameter("mess", message);
            
            cmd.Parameters.Add(channelID);
            cmd.Parameters.Add(mess);
            
            cmd.ExecuteNonQuery();
        }

        public static MySqlDataReader GetPreviousMessages(string queueID)
        {
            MySqlCommand cmd = new MySqlCommand("GetPreviousMessages", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlParameter qID = new MySqlParameter("qID", queueID);

            cmd.Parameters.Add(qID);

            return cmd.ExecuteReader();
        }

        public static void Create1To1ChatRoom(string chatRoomName)
        {
            MySqlCommand cmd = new MySqlCommand("AddChatRoom", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlParameter parameter = new MySqlParameter("name", chatRoomName);
            cmd.Parameters.Add(parameter);
            cmd.ExecuteNonQuery();
        }

        public static bool Is1To1ChatRoomCreated(string sourceUser, string destinationUser, out string roomName)
        {
            roomName = string.Empty;
            MySqlCommand cmd = new MySqlCommand("CheckChatRoom", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            string chatRoomFormat = string.Format(Constants.DB_CHATROOM_STRING_FORMAT, sourceUser, destinationUser);
            string chatRoomFormatAlt = string.Format(Constants.DB_CHATROOM_STRING_FORMAT, destinationUser, sourceUser);

            MySqlParameter chatRoomName = new MySqlParameter("name", chatRoomFormat);
            MySqlParameter chatRoomNameAlt = new MySqlParameter("nameAlt", chatRoomFormatAlt);

            cmd.Parameters.Add(chatRoomName);
            cmd.Parameters.Add(chatRoomNameAlt);

            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            
            bool isRoomPresent = false;

            while (mySqlDataReader.Read())
            {
                isRoomPresent = mySqlDataReader.GetString(0) != null;
                roomName = mySqlDataReader.GetString(0);
            }

            mySqlDataReader.Close();
            mySqlDataReader.Dispose();

            return isRoomPresent;                                
        }
    }
}
