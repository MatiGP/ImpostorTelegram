using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImpostorTelegram
{
    public partial class ImpostorTelegram : Form
    {
        public ImpostorTelegram()
        {
            InitializeComponent();
        }
        
        private void ImpostorTelegram_Load(object sender, EventArgs e)
        {
            #region Form1Settings
            MinimumSize = new Size(400, 600);
            BackColor = Constants.MAIN_BACKGROUND_COLOR;
            #endregion


            LoginScreen loginScreeen = new LoginScreen();
            Controls.Add(loginScreeen);

            MessagesListScreen messagesListScreen = new MessagesListScreen();
            Controls.Add(messagesListScreen);
            messagesListScreen.Visible = false;

            ChatUiScreen chatUiScreen = new ChatUiScreen();
            Controls.Add(chatUiScreen);
            chatUiScreen.Visible = false;
        }
    }
}
