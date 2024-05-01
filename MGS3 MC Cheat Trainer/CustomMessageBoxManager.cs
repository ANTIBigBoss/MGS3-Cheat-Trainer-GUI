using System;
using System.Drawing;
using System.Windows.Forms;

namespace MGS3_MC_Cheat_Trainer
{
    internal class CustomMessageBoxManager
    {
        private enum CustomColor
        {
            MGS3ButtonColor, // 156,156,124
            SurvivalViewerColor // 36,44,36
        }

        private static readonly Color MGS3ButtonColor = Color.FromArgb(156, 156, 124);
        private static readonly Color SurvivalViewerColor = Color.FromArgb(36, 44, 36);

        private static Point _lastClick;

        public static void CustomMessageBox(string message, string caption)
        {

            // Future implementation for sound effects:
            // PlayMessageBoxSound("path_to_your_sound_file.wav");

            // Create a new form
            Form messageBoxForm = new Form()
            {
                FormBorderStyle = FormBorderStyle.None, // No default title bar
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false,
                BackColor = SurvivalViewerColor // Set background color
            };

            // Custom icon implementation for later use
            //SetMessageBoxIcon(messageBoxForm, "path_to_your_icon_file.ico");

            // Create a custom title bar panel
            Panel titleBarPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = SystemInformation.CaptionHeight, // Set height to match system caption height
                BackColor = MGS3ButtonColor,
            };
            messageBoxForm.Controls.Add(titleBarPanel);

            // Create a label for the title text
            Label titleLabel = new Label()
            {
                Text = caption,
                AutoSize = true,
                ForeColor = Color.Black, // Set color of the title text
                BackColor = MGS3ButtonColor,
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold),
                Location = new Point(10, (titleBarPanel.Height - 20) / 2) // Center vertically
            };
            titleBarPanel.Controls.Add(titleLabel);

            // Mouse down event for form movement
            titleBarPanel.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    _lastClick = e.Location;
                }
            };

            // Mouse move event for form movement
            titleBarPanel.MouseMove += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    messageBoxForm.Left += e.X - _lastClick.X;
                    messageBoxForm.Top += e.Y - _lastClick.Y;
                }
            };

            // Create a label for the message content
            Label messageLabel = new Label()
            {
                Text = message,
                AutoSize = true,
                Left = 20,
                Top = titleBarPanel.Bottom + 20,
                MaximumSize = new Size(440, 0),
                ForeColor = Color.Black,
                BackColor = MGS3ButtonColor,
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold),
            };
            messageBoxForm.Controls.Add(messageLabel);

            // Calculate form width and height based on label size
            int formWidth = Math.Min(messageLabel.Width + 40, 500); // Limit maximum width to 500 pixels
            int formHeight = Math.Min(messageLabel.Height + 120, 200); // Limit maximum height to 200 pixels

            messageBoxForm.Width = formWidth;
            messageBoxForm.Height = formHeight;

            // Adjust label position
            messageLabel.Left = (messageBoxForm.ClientSize.Width - messageLabel.Width) / 2;

            // Calculate the horizontal position of the buttons
            int buttonLeft = (messageBoxForm.ClientSize.Width - 2 * 75 - 20) / 2;

            // Create a button to copy the message
            Button copyButton = new Button()
            {
                Text = "Copy",
                Width = 75,
                Height = 30,
                DialogResult = DialogResult.OK,
                Location = new Point(buttonLeft, messageLabel.Bottom + 10), // Slightly higher
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold),
                BackColor = MGS3ButtonColor,
                ForeColor = Color.Black // Set button text color
            };
            copyButton.Click += (sender, e) =>
            {
                Clipboard.SetText(message);
            };
            messageBoxForm.Controls.Add(copyButton);

            // Create a close button
            Button okButton = new Button()
            {
                Text = "Ok",
                Width = 75,
                Height = 30,
                DialogResult = DialogResult.Cancel,
                Location = new Point(copyButton.Right + 20, copyButton.Top), // Next to the copy button
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold),
                BackColor = MGS3ButtonColor,
                ForeColor = Color.Black // Set button text color
            };
            okButton.Click += (sender, e) =>
            {
                messageBoxForm.Close();
            };
            messageBoxForm.Controls.Add(okButton);

            // Show the form as a dialog
            messageBoxForm.ShowDialog();
        }
    }
}

/* Future Implementations for Icons and sounds:

private static void PlayMessageBoxSound(string soundFilePath)
   {
       try
       {
           SoundPlayer soundPlayer = new SoundPlayer(soundFilePath);
           soundPlayer.Play();
       }
       catch (Exception ex)
       {
           Console.WriteLine("Error playing sound: " + ex.Message);
       }
   }

   private static void SetMessageBoxIcon(Form form, string iconFilePath)
   {
       try
       {
           form.Icon = new Icon(iconFilePath);
       }
       catch (Exception ex)
       {
           Console.WriteLine("Error setting custom icon: " + ex.Message);
       }
   }

*/