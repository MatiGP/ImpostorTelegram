﻿using System;
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
        PictureBox UserAvatar;
        Label UserName;
        string _Name;
        string Surname;
        Color BaseButtonBackColor = Color.Transparent;
        Color HoverButtonBackColor = Color.FromArgb(255, 70, 72, 102);
        Color fontColor = Color.White;
        public ChatButton()
        {
            OnButtonCreate();
        }

        private void OpenChat(object sender, EventArgs e)
        {
            MessageBox.Show($"Opened chat with {_Name} {Surname}");
        }

        private void MouseON(object sender, EventArgs e)
        {
            BackColor = HoverButtonBackColor;
        }

        private void MouseOff(object sender, EventArgs e)
        {
            BackColor = BaseButtonBackColor;
        }
        public ChatButton(string Name, string Surname)
        {
            OnButtonCreate();
            this._Name = Name;
            this.Surname = Surname;
            UserName.Text = $"{Name} {Surname}";
        }
        void OnButtonCreate()
        {
            Dock = DockStyle.Fill;
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
            UserName.Text = $"{_Name} {Surname}";
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
