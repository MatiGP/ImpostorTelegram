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
    class SoundMessageUI : MessageUi
    {
        private PictureBox playButton;

        public SoundMessageUI(Message message)
        {
            SetUpMessageUI();
            SetUpAuthorLabel(message.Author);

            TableLayoutPanel musicTableLayoutPanel = new TableLayoutPanel();
            musicTableLayoutPanel.BackColor = Constants.MAIN_BACKGROUND_COLOR;
            musicTableLayoutPanel.AutoSize = true;
            musicTableLayoutPanel.Dock = DockStyle.Fill;
            musicTableLayoutPanel.Margin = new Padding(0);
            musicTableLayoutPanel.ColumnCount = 2;
            musicTableLayoutPanel.RowCount = 1;
            musicTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));

            Label musicName = new Label();
            musicName.ForeColor = Constants.FONT_COLOR;
            musicName.Font = Constants.GLOBAL_NORMAL_FONT;
            musicName.Text = "Testowa nazwa do zmiany.mp3";
            musicName.Margin = new Padding(0, 30, 0, 0);
            musicName.AutoSize = true;

            playButton = new PictureBox();
            string playButtonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Resorces\\playMusic.png");
            playButton.Image = Image.FromFile(playButtonPath);
            playButton.Width = 74;
            playButton.Height = 74;
            playButton.Location = new Point(6, 6);
            playButton.SizeMode = PictureBoxSizeMode.StretchImage;
            playButton.Click += playButtonClick;
            playButton.MouseEnter += playButtonEnter;
            playButton.MouseLeave += playButtonLeave;

            musicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            musicTableLayoutPanel.Controls.Add(musicName);
            musicTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            musicTableLayoutPanel.Controls.Add(playButton);

            Controls.Add(musicTableLayoutPanel);
        }
        private void playButtonLeave(object sender, EventArgs e)
        {
            playButton.BackColor = Color.Transparent;
        }

        private void playButtonEnter(object sender, EventArgs e)
        {
            playButton.BackColor = Constants.HIGHLIGHT_BACKGROUND_COLOR;
        }

        private void playButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Brak implementacji");
        }
    }
}
