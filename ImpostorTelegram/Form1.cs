using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private Receiver m_Receiver = null;
        private Sender m_Sender = null;

        public ImpostorTelegram()
        {
            InitializeComponent();
            m_Receiver = new Receiver();
            m_Sender = new Sender();

            m_Receiver.OnMessageReceived += HandleMessageReceived;
        }

        private void HandleMessageReceived(object sender, string e)
        {
            ChatReceiver.Invoke(new Action(() => { ChatReceiver.Text = e; }));
        }

        private void SendMessage_Button_Click(object sender, EventArgs e)
        {
            m_Sender.SendMessage(ChatText.Text);
                     
        }
    }
}
