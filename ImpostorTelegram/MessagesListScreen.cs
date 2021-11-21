using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpostorTelegram
{
    class MessagesListScreen : TableLayoutPanel
    {
        public static MessagesListScreen Instance;
        public MessagesListScreen()
        {
            Instance = this;
            BackColor = Constants.MAIN_BACKGROUND_COLOR;
            Dock = DockStyle.Top;
            ColumnCount = 1;
            Height = 6;
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            Label messagesScreenLabel = new Label();
            messagesScreenLabel.Text = "Impostor Telegram";
            messagesScreenLabel.Font = Constants.GLOBAL_BIG_FONT;
            messagesScreenLabel.AutoSize = false;
            messagesScreenLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            messagesScreenLabel.TextAlign = ContentAlignment.MiddleCenter;
            messagesScreenLabel.ForeColor = Constants.FONT_COLOR;
            RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            Controls.Add(messagesScreenLabel);
            Height += 80;


            ////////////////////////////// TYMCZASOWE
            ChatButton testButton = new ChatButton("Kacper", "Kotecki");
            testButton.Width = 300;
            ChatButton testButton2 = new ChatButton("Mateusz", "Świeca");
            testButton2.Width = 300;
            testButton2.Location = new Point(0, 80);
            ChatButton testButton3 = new ChatButton("Lech", "Kaczyński");
            testButton2.Width = 300;
            testButton2.Location = new Point(0, 80);
            RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            Controls.Add(testButton);
            Height += 80;
            RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            Controls.Add(testButton2);
            Height += 80;
            RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            Controls.Add(testButton3);
            Height += 80;
            /////////////////////////////
        }
        public void makeVisible()
        {
            Visible = true;
        }
    }
}
