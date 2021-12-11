using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpostorTelegram
{
    class GroupButton : Button
    {
        public event EventHandler OnGroupButtonPressed;

        public GroupButton()
        {
            Dock = DockStyle.Fill;
            BackColor = Constants.SECONDARY_BACKGROUND_COLOR;

            Font = Constants.GLOBAL_NORMAL_FONT;
            ForeColor = Constants.FONT_COLOR;
            Text = "Join/Create group room";
            TextAlign = ContentAlignment.MiddleCenter;

            Click += GroupButton_Click;
        }

        private void GroupButton_Click(object sender, EventArgs e)
        {
            OnGroupButtonPressed.Invoke(this, EventArgs.Empty);

            //MessagesListScreen.Instance.Visible = false;
            //RoomJoinScreen.Instance.Visible = true;
        }
    }
}
