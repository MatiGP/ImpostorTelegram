using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace ImpostorTelegram.MessageUI
{
    class SoundMessageUI : MessageUi
    {      
        private PictureBox playButton;
        private Mp3FileReader m_MP3FileReader = null;
        private WaveOutEvent m_WaveOut = null;

        bool m_IsPlaying = false;

        public SoundMessageUI(Message message)
        {
            SetUpMessageUI();
            SetUpAuthorLabel(message.Author);
            
            #region Design
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
            musicName.Text = "[MusicMP3].mp3";
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
            #endregion

            byte[] fileByte = message.MessageText;
            Stream stream = new MemoryStream(fileByte);
            m_MP3FileReader = new Mp3FileReader(stream);
            m_WaveOut = new WaveOutEvent();
            m_WaveOut.Init(m_MP3FileReader);
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
            if (!m_IsPlaying)
            {
                m_WaveOut.Play();                
                m_IsPlaying = true;
            }
            else
            {
                m_WaveOut.Pause();
                m_IsPlaying = false;
            }
        }

        public void PauseMusic(object sender, EventArgs e)
        {
            m_WaveOut.Pause();
            m_WaveOut.Dispose();
            m_MP3FileReader.Dispose();
        }
    }
}
