using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpostorTelegram
{
    class RoomJoinScreen : TableLayoutPanel
    {
        public event EventHandler<string> OnRoomCreated;

        public TextBox password;
        public RoomJoinScreen()
        {
            Label loginHintText = new Label();
            loginHintText.AutoSize = false;
            loginHintText.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            loginHintText.TextAlign = ContentAlignment.MiddleCenter;
            loginHintText.Text = "Write room password:";
            loginHintText.Font = Constants.GLOBAL_NORMAL_FONT;
            loginHintText.ForeColor = Constants.FONT_COLOR;

            password = new TextBox();
            password.AutoSize = false;
            password.Anchor = AnchorStyles.Top;
            password.Width = 130;
            password.Height = 30;
            password.Font = Constants.GLOBAL_NORMAL_FONT;

            Button joinRoomButton = new Button();
            joinRoomButton.Text = "Join";
            joinRoomButton.Font = Constants.GLOBAL_NORMAL_FONT;
            joinRoomButton.ForeColor = Constants.FONT_COLOR;
            joinRoomButton.BackColor = Constants.SECONDARY_BACKGROUND_COLOR;
            joinRoomButton.AutoSize = false;
            joinRoomButton.Anchor = AnchorStyles.Top;
            joinRoomButton.Click += JoinChatRoom;
            joinRoomButton.Width = 130;
            joinRoomButton.Height = 40;

            BackColor = Constants.MAIN_BACKGROUND_COLOR;
            Padding = new Padding(0, 100, 0, 100);
            Dock = DockStyle.Fill;
            ColumnCount = 1;
            RowCount = 3;
            ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));

            for (int i = 0; i < 3; i++)
            {
                RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            }

            Controls.Add(loginHintText);
            Controls.Add(password);
            Controls.Add(joinRoomButton);
        }

        private void JoinChatRoom(object sender, EventArgs e)
        {
            if (password.Text.Length > 0)
            {
                OnRoomCreated.Invoke(this, password.Text);
                password.Text = "";
            }
            else
            {
                MessageBox.Show("You need to enter at least 1 char");
            }
        }
    }
}
