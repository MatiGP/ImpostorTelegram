
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
            // ImpostorTelegram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ChatReceiver);
            this.Controls.Add(this.SendMessage_Button);
            this.Controls.Add(this.ChatText);
            this.Name = "ImpostorTelegram";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatText;
        private System.Windows.Forms.Button SendMessage_Button;
        private System.Windows.Forms.Label ChatReceiver;
    }
}

