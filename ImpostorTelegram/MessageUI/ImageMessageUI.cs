using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpostorTelegram.MessageUI
{
    class ImageMessageUI : MessageUi
    {
        private PictureBox pictureBox;
        public ImageMessageUI(Message message)
        {
            SetUpMessageUI();
            SetUpAuthorLabel(message.Author);

            pictureBox = new PictureBox();

            pictureBox.Image = RabbitUtils.GetDecodedImage(message.MessageText);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new Size(256, 256);
            Controls.Add(pictureBox);
        }
    }
}
