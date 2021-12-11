using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpostorTelegram
{
    public class LoginScreen : TableLayoutPanel
    {
        public event EventHandler<string> OnSuccesfulLogin;

        public TextBox nameTextBox, surnameTextBox;

        public LoginScreen()
        {
            #region Design Stuff
            Label AppNameLabel, NameLabel, SurnameLabel;
            AppNameLabel = new Label();
            NameLabel = new Label();
            SurnameLabel = new Label();
            AppNameLabel.AutoSize = false;
            AppNameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            AppNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            AppNameLabel.Text = "Impostor Telegram";
            AppNameLabel.Font = Constants.GLOBAL_BIG_FONT;
            AppNameLabel.ForeColor = Constants.FONT_COLOR;

            NameLabel.AutoSize = false;
            NameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            NameLabel.TextAlign = ContentAlignment.MiddleCenter;
            NameLabel.Text = "Your Name";
            NameLabel.Font = Constants.GLOBAL_NORMAL_FONT;
            NameLabel.ForeColor = Constants.FONT_COLOR;

            SurnameLabel.AutoSize = false;
            SurnameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            SurnameLabel.TextAlign = ContentAlignment.MiddleCenter;
            SurnameLabel.Text = "Your Surname";
            SurnameLabel.Font = Constants.GLOBAL_NORMAL_FONT;
            SurnameLabel.ForeColor = Constants.FONT_COLOR;

            surnameTextBox = new TextBox();
            surnameTextBox.Width = 130;
            surnameTextBox.Height = 30;
            surnameTextBox.AutoSize = false;
            surnameTextBox.Anchor = AnchorStyles.Top;
            surnameTextBox.Font = Constants.GLOBAL_NORMAL_FONT;

            nameTextBox = new TextBox();
            nameTextBox.AutoSize = false;
            nameTextBox.Anchor = AnchorStyles.Top;
            nameTextBox.Width = 130;
            nameTextBox.Height = 30;
            nameTextBox.Font = Constants.GLOBAL_NORMAL_FONT;

            Button CreateAccountButton = new Button();
            CreateAccountButton.Text = "Create";
            CreateAccountButton.AutoSize = false;
            CreateAccountButton.Anchor = AnchorStyles.Top;
            CreateAccountButton.Click += OnCreateButtonClick;
            CreateAccountButton.Width = 130;
            CreateAccountButton.Height = 40;
            CreateAccountButton.Font = Constants.GLOBAL_NORMAL_FONT;
            CreateAccountButton.ForeColor = Constants.FONT_COLOR;
            CreateAccountButton.BackColor = Constants.SECONDARY_BACKGROUND_COLOR;

            BackColor = Constants.MAIN_BACKGROUND_COLOR;
            Padding = new Padding(0, 100, 0, 100);
            Dock = DockStyle.Fill;
            ColumnCount = 1;
            RowCount = 6;
            ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));

            for (int i = 0; i < 6; i++)
            {
                RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            }

            #endregion

            Controls.Add(AppNameLabel);
            Controls.Add(NameLabel);
            Controls.Add(nameTextBox);
            Controls.Add(SurnameLabel);
            Controls.Add(surnameTextBox);
            Controls.Add(CreateAccountButton);

        }

        private void OnCreateButtonClick(object sender, EventArgs e)
        {
            if (nameTextBox.TextLength > 0 && surnameTextBox.TextLength > 0)
            {
                OnSuccesfulLogin?.Invoke(this, $"{nameTextBox.Text} {surnameTextBox.Text}");
            }
            else
            {
                MessageBox.Show("Your Name and Surname can't be null");
            }
        }
    
    }
}
