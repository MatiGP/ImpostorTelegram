using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

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
            //ChatReceiver.Invoke((Action) (() => { ChatReceiver.Text = e; }));
        }

        private void SendMessage_Button_Click(object sender, EventArgs e)
        {

        }

        private void LoadImage_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png)|*.png";

            string filePath = string.Empty;
            string fileContent = string.Empty;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;

                Stream fileStream = openFileDialog.OpenFile();
                                  
                byte[] imageBytes = RabbitUtils.CreateEncodedImage(Image.FromFile(filePath));

            }    
        }

        private void LoadSound_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.wav)|*.wav";

            string filePath = string.Empty;
            string fileContent = string.Empty;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                 RabbitUtils.CreateEncodedSound(filePath);
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
                            
        }
    }
}
