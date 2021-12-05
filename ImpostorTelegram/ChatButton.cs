using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpostorTelegram
{
    class ChatButton : Panel
    {
        public event EventHandler<string> OnButtonPressed;

        PictureBox UserAvatar;
        Label UserName;
        string m_UserCreds;
        Color BaseButtonBackColor = Color.Transparent;
        Color fontColor = Color.White;
        public ChatButton()
        {
            OnButtonCreate();
        }

        private void OpenChat(object sender, EventArgs e)
        {
            OnButtonPressed?.Invoke(this, m_UserCreds);
        }

        private void MouseON(object sender, EventArgs e)
        {
            BackColor = Constants.HIGHLIGHT_BACKGROUND_COLOR;
        }

        private void MouseOff(object sender, EventArgs e)
        {
            BackColor = BaseButtonBackColor;
        }
        public ChatButton(string userCreds)
        {
            OnButtonCreate();
            m_UserCreds = userCreds;
            UserName.Text = m_UserCreds;
        }
        void OnButtonCreate()
        {
            Dock = DockStyle.Top;
            Margin = new Padding(0);
            UserAvatar = new PictureBox();
            UserAvatar.BackColor = Color.Transparent;
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\contacts.png");
            UserAvatar.Image = Image.FromFile(fullPath);
            UserAvatar.Width = 74;
            UserAvatar.Height = 74;
            UserAvatar.Location = new Point(6, 6);
            UserAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            BackColor = BaseButtonBackColor;

            UserName = new Label();
            UserName.Text = m_UserCreds;
            UserName.Font = new Font("Century Gothic", 14);
            UserName.BackColor = Color.Transparent;
            UserName.AutoSize = true;
            UserName.Padding = new Padding(100, 10, 5, 5);
            UserName.ForeColor = fontColor;
            Controls.Add(UserAvatar);
            Controls.Add(UserName);

            UserAvatar.Click += OpenChat;
            UserName.Click += OpenChat;
            Click += OpenChat;

            UserAvatar.MouseEnter += MouseON;
            UserName.MouseEnter += MouseON;
            MouseEnter += MouseON;

            UserAvatar.MouseLeave += MouseOff;
            UserName.MouseLeave += MouseOff;
            MouseLeave += MouseOff;
            
        }
    }
}
