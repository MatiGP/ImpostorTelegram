using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpostorTelegram
{
    abstract class MessageUi : FlowLayoutPanel
    {
        protected Label fromLabel;
           
        protected void SetUpMessageUI()
        {
            AutoSize = true;
            FlowDirection = FlowDirection.TopDown;
            BackColor = Constants.MAIN_BACKGROUND_COLOR;
            Margin = new Padding(15, 8, 15, 8);
            Padding = new Padding(8);
        }

        protected void SetUpAuthorLabel(string from)
        {
            fromLabel = new Label();
            fromLabel.AutoSize = true;
            fromLabel.Font = Constants.GLOBAL_NORMAL_FONT;
            fromLabel.ForeColor = Constants.FONT_COLOR;
            fromLabel.Text = $"{from}:";
            Controls.Add(fromLabel);
        }
    }
}
