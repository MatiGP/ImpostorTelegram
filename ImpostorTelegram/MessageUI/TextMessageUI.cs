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
    class TextMessageUI : MessageUi
    {
        private Label messLabel;
        public TextMessageUI(Message message)
        {
            SetUpMessageUI();
            SetUpAuthorLabel(message.Author);

            messLabel = new Label();
            messLabel.AutoSize = true;
            messLabel.Font = Constants.GLOBAL_NORMAL_FONT;
            messLabel.ForeColor = Constants.FONT_COLOR;
            messLabel.Text = RabbitUtils.GetDecodedString(message.MessageText);
            Controls.Add(messLabel);
        }
    }
}
