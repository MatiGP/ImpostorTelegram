
namespace ImpostorTelegram
{
    partial class ImpostorTelegram
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChatText = new System.Windows.Forms.TextBox();
            this.SendMessage_Button = new System.Windows.Forms.Button();
            this.ChatReceiver = new System.Windows.Forms.Label();
            this.LoadImage_Button = new System.Windows.Forms.Button();
            this.LoadSound_Button = new System.Windows.Forms.Button();
            this.ImageReceiver = new System.Windows.Forms.PictureBox();
            this.Play = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImageReceiver)).BeginInit();
            this.SuspendLayout();
            // 
            // ChatText
            // 
            this.ChatText.Location = new System.Drawing.Point(35, 32);
            this.ChatText.Name = "ChatText";
            this.ChatText.Size = new System.Drawing.Size(100, 23);
            this.ChatText.TabIndex = 0;
            // 
            // SendMessage_Button
            // 
            this.SendMessage_Button.Location = new System.Drawing.Point(35, 79);
            this.SendMessage_Button.Name = "SendMessage_Button";
            this.SendMessage_Button.Size = new System.Drawing.Size(100, 47);
            this.SendMessage_Button.TabIndex = 1;
            this.SendMessage_Button.Text = "button1";
            this.SendMessage_Button.UseVisualStyleBackColor = true;
            this.SendMessage_Button.Click += new System.EventHandler(this.SendMessage_Button_Click);
            // 
            // ChatReceiver
            // 
            this.ChatReceiver.AutoSize = true;
            this.ChatReceiver.Location = new System.Drawing.Point(196, 60);
            this.ChatReceiver.Name = "ChatReceiver";
            this.ChatReceiver.Size = new System.Drawing.Size(38, 15);
            this.ChatReceiver.TabIndex = 2;
            this.ChatReceiver.Text = "label1";
            // 
            // LoadImage_Button
            // 
            this.LoadImage_Button.Location = new System.Drawing.Point(12, 350);
            this.LoadImage_Button.Name = "LoadImage_Button";
            this.LoadImage_Button.Size = new System.Drawing.Size(100, 47);
            this.LoadImage_Button.TabIndex = 3;
            this.LoadImage_Button.Text = "LoadImage";
            this.LoadImage_Button.UseVisualStyleBackColor = true;
            this.LoadImage_Button.Click += new System.EventHandler(this.LoadImage_Button_Click);
            // 
            // LoadSound_Button
            // 
            this.LoadSound_Button.Location = new System.Drawing.Point(688, 350);
            this.LoadSound_Button.Name = "LoadSound_Button";
            this.LoadSound_Button.Size = new System.Drawing.Size(100, 47);
            this.LoadSound_Button.TabIndex = 4;
            this.LoadSound_Button.Text = "LoadSound";
            this.LoadSound_Button.UseVisualStyleBackColor = true;
            this.LoadSound_Button.Click += new System.EventHandler(this.LoadSound_Button_Click);
            // 
            // ImageReceiver
            // 
            this.ImageReceiver.Location = new System.Drawing.Point(143, 259);
            this.ImageReceiver.Name = "ImageReceiver";
            this.ImageReceiver.Size = new System.Drawing.Size(148, 138);
            this.ImageReceiver.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImageReceiver.TabIndex = 5;
            this.ImageReceiver.TabStop = false;
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(701, 311);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 6;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(359, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 7;
            // 
            // ImpostorTelegram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.ImageReceiver);
            this.Controls.Add(this.LoadSound_Button);
            this.Controls.Add(this.LoadImage_Button);
            this.Controls.Add(this.ChatReceiver);
            this.Controls.Add(this.SendMessage_Button);
            this.Controls.Add(this.ChatText);
            this.Name = "ImpostorTelegram";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ImageReceiver)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatText;
        private System.Windows.Forms.Button SendMessage_Button;
        private System.Windows.Forms.Label ChatReceiver;
        private System.Windows.Forms.Button LoadImage_Button;
        private System.Windows.Forms.Button LoadSound_Button;
        private System.Windows.Forms.PictureBox ImageReceiver;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

