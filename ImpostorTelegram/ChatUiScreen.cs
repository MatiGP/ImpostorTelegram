﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpostorTelegram
{
    class ChatUiScreen : TableLayoutPanel
    {
        public static ChatUiScreen Instance;
        private Label userName;
        PictureBox backIcon;
        PictureBox uploadIcon;
        TextBox typeMessageUiTextbox;
        PictureBox sendIcon;

        public ChatUiScreen()
        {
            Instance = this;
            Dock = DockStyle.Fill;
            BackColor = Constants.MAIN_BACKGROUND_COLOR;
            AutoScroll = true;


            TableLayoutPanel statusBarPanel = new TableLayoutPanel();
            statusBarPanel.BackColor = Constants.MAIN_BACKGROUND_COLOR;
            statusBarPanel.Dock = DockStyle.Fill;
            statusBarPanel.Margin = new Padding(0);
            statusBarPanel.ColumnCount = 3;
            statusBarPanel.RowCount = 1;
            statusBarPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            PictureBox UserAvatar = new PictureBox();
            UserAvatar.BackColor = Color.Transparent;
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\contacts.png");
            UserAvatar.Image = Image.FromFile(fullPath);
            UserAvatar.Width = 74;
            UserAvatar.Height = 74;
            UserAvatar.Location = new Point(6, 6);
            UserAvatar.SizeMode = PictureBoxSizeMode.StretchImage;

            userName = new Label();
            userName.Text = $"Imię i Nazwisko z kim rozmawiasz";
            userName.Font = Constants.GLOBAL_NORMAL_FONT;
            userName.BackColor = Color.Transparent;
            userName.AutoSize = true;
            userName.Padding = new Padding(20, 30, 5, 5);
            userName.ForeColor = Constants.FONT_COLOR;

            backIcon = new PictureBox();
            backIcon.BackColor = Color.Transparent;
            string backIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\back.png");
            backIcon.Image = Image.FromFile(backIconPath);
            backIcon.Width = 74;
            backIcon.Height = 74;
            backIcon.Location = new Point(6, 6);
            backIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            backIcon.Click += backIconClick;
            backIcon.MouseEnter += mouseEnter;
            backIcon.MouseLeave += mouseLeave;


            statusBarPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            statusBarPanel.Controls.Add(UserAvatar);
            statusBarPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            statusBarPanel.Controls.Add(userName);
            statusBarPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            statusBarPanel.Controls.Add(backIcon);

            TableLayoutPanel messageScrollUi;
            messageScrollUi = new TableLayoutPanel();
            messageScrollUi.BackColor = Constants.SECONDARY_BACKGROUND_COLOR;
            messageScrollUi.Dock = DockStyle.Fill;
            messageScrollUi.ColumnCount = 1;
            messageScrollUi.Margin = new Padding(0);

            uploadIcon = new PictureBox();
            uploadIcon.BackColor = Color.Transparent;
            string uploadIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\upload.png");
            uploadIcon.Image = Image.FromFile(uploadIconPath);
            uploadIcon.Width = 70;
            uploadIcon.Height = 70;
            uploadIcon.Location = new Point(5, 5);
            uploadIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            uploadIcon.Click += uploadIconClick;
            uploadIcon.MouseEnter += mouseEnter;
            uploadIcon.MouseLeave += mouseLeave;

            typeMessageUiTextbox = new TextBox();
            typeMessageUiTextbox.Multiline = true;
            typeMessageUiTextbox.Dock = DockStyle.Fill;
            typeMessageUiTextbox.Font = Constants.GLOBAL_NORMAL_FONT;

            sendIcon = new PictureBox();
            sendIcon.BackColor = Color.Transparent;
            string sendIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\send.png");
            sendIcon.Image = Image.FromFile(sendIconPath);
            sendIcon.Width = 70;
            sendIcon.Height = 70;
            sendIcon.Location = new Point(5, 5);
            sendIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            sendIcon.Click += sendIconClick;
            sendIcon.MouseEnter += mouseEnter;
            sendIcon.MouseLeave += mouseLeave;

            TableLayoutPanel sendMessagePanel = new TableLayoutPanel();
            sendMessagePanel.BackColor = Constants.MAIN_BACKGROUND_COLOR;
            sendMessagePanel.Dock = DockStyle.Fill;
            sendMessagePanel.Margin = new Padding(0);
            sendMessagePanel.ColumnCount = 3;
            sendMessagePanel.RowCount = 1;
            sendMessagePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            sendMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            sendMessagePanel.Controls.Add(uploadIcon);
            sendMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            sendMessagePanel.Controls.Add(typeMessageUiTextbox);
            sendMessagePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            sendMessagePanel.Controls.Add(sendIcon);

            TableLayoutPanel inMessageUiTablePanel;
            inMessageUiTablePanel = new TableLayoutPanel();
            inMessageUiTablePanel.BackColor = Constants.MAIN_BACKGROUND_COLOR;
            inMessageUiTablePanel.Dock = DockStyle.Fill;
            inMessageUiTablePanel.ColumnCount = 1;
            inMessageUiTablePanel.Height = 6;
            inMessageUiTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            inMessageUiTablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            inMessageUiTablePanel.Controls.Add(statusBarPanel);

            inMessageUiTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            inMessageUiTablePanel.Controls.Add(messageScrollUi);
            inMessageUiTablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            inMessageUiTablePanel.Controls.Add(sendMessagePanel);
            Controls.Add(inMessageUiTablePanel);
        }

        private void uploadIconClick(object sender, EventArgs e)
        {
            MessageBox.Show("brak implemenacji 2");
        }

        private void sendIconClick(object sender, EventArgs e)
        {
            MessageBox.Show("brak implemenacji");
        }

        private void mouseLeave(object sender, EventArgs e)
        {
            backIcon.BackColor = Color.Transparent;
        }

        private void mouseEnter(object sender, EventArgs e)
        {
            backIcon.BackColor = Constants.SECONDARY_BACKGROUND_COLOR;
        }

        private void backIconClick(object sender, EventArgs e)
        {
            Visible = false;
            MessagesListScreen.Instance.Visible = true;
        }

        public void openChat(string name)
        {
            userName.Text = name;
            Visible = true;
        }
    }
}
