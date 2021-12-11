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
        private static string m_Server = null;
        private static string m_DataBase = null;
        private static string m_UserID = null;
        private static string m_Password = null;
        public DatabaseUtils()
        {
            SetLoginFields();
            //OpenConnection();
        }

        private void SetLoginFields()
        {
            m_Server = "localhost";
            m_DataBase = "userdatabase";
            m_UserID = "root";
            m_Password = "";
        }

        private static MySqlConnection CreateConnection()
        {
            string connectionString = string.Format(Constants.DB_CONNECTION_STRING_FORMAT, m_Server, m_DataBase, m_UserID, m_Password);
            return new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                //m_Connection.Open();
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
            MySqlConnection m_Connection = CreateConnection();
            m_Connection.Open();

            MySqlCommand cmd = new MySqlCommand("AddOnlineUser", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter parameter = new MySqlParameter("newUser", message.Author);
            cmd.Parameters.Add(parameter);
            cmd.ExecuteNonQuery();
            m_Connection.Close();
        }

        public static void RemoveUserFromDatabase(Message message)
        {
            MySqlConnection m_Connection = CreateConnection();
            m_Connection.Open();

            MySqlCommand cmd = new MySqlCommand("RemoveUserFromOnline", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter parameter = new MySqlParameter("newUser", message.Author);
            cmd.Parameters.Add(parameter);
            cmd.ExecuteNonQuery();
            m_Connection.Close();
        }

        public static MySqlDataReader GetOnlineUsers()
        {
            MySqlConnection m_Connection = CreateConnection();
            m_Connection.Open();

            MySqlCommand cmd = new MySqlCommand("GetAllOnlineUsers", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd.ExecuteReader();
        }

        public static void AddMessageToChatHistory(string queueID, string message)
        {
            MySqlConnection m_Connection = CreateConnection();
            m_Connection.Open();

            MySqlCommand cmd = new MySqlCommand("AddMessageToChatHistory", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            MySqlParameter channelID = new MySqlParameter("channelID", queueID);
            MySqlParameter mess = new MySqlParameter("mess", message);
            
            cmd.Parameters.Add(channelID);
            cmd.Parameters.Add(mess);
            
            cmd.ExecuteNonQuery();
            m_Connection.Close();
        }

        public static MySqlDataReader GetPreviousMessages(string queueID)
        {
            MySqlConnection m_Connection = CreateConnection();
            m_Connection.Open();

            MySqlCommand cmd = new MySqlCommand("GetPreviousMessages", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlParameter qID = new MySqlParameter("qID", queueID);

            cmd.Parameters.Add(qID);

            return cmd.ExecuteReader();
        }

        public static void Create1To1ChatRoom(string chatRoomName)
        {
            MySqlConnection m_Connection = CreateConnection();
            m_Connection.Open();

            MySqlCommand cmd = new MySqlCommand("AddChatRoom", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlParameter parameter = new MySqlParameter("name", chatRoomName);
            cmd.Parameters.Add(parameter);
            cmd.ExecuteNonQuery();
            m_Connection.Close();
        }

        public static bool Is1To1ChatRoomCreated(string sourceUser, string destinationUser, out string roomName)
        {
            MySqlConnection m_Connection = CreateConnection();
            m_Connection.Open();

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

        public static bool IsUserBannedFromChat(string sourceUser, string roomName)
        {
            MySqlConnection m_Connection = CreateConnection();
            m_Connection.Open();

            MySqlCommand cmd = new MySqlCommand("CheckBanList", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlParameter chatRoomName = new MySqlParameter("channelID", roomName);
            MySqlParameter chatUserName = new MySqlParameter("name", sourceUser);

            cmd.Parameters.Add(chatRoomName);
            cmd.Parameters.Add(chatUserName);

            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

            bool isBanned = false;

            while (mySqlDataReader.Read())
            {
                isBanned = mySqlDataReader.GetString(0) != null;
            }

            mySqlDataReader.Close();
            mySqlDataReader.Dispose();

            return isBanned;
        }

        public static void BanUserFromRoom(string userName, string roomName)
        {
            MySqlConnection m_Connection = CreateConnection();
            m_Connection.Open();

            MySqlCommand cmd = new MySqlCommand("BanUserFromChannel", m_Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            MySqlParameter user = new MySqlParameter("name", userName);
            MySqlParameter room = new MySqlParameter("channelID", roomName);
            cmd.Parameters.Add(user);
            cmd.Parameters.Add(room);
            cmd.ExecuteNonQuery();
            m_Connection.Close();
        }
    }
}
