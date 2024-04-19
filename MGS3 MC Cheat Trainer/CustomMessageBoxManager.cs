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



        public static void CustomMessageBox(string message, string caption)
        {

            
            // Create a new form
            Form messageBoxForm = new Form()
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false,
                BackColor = SurvivalViewerColor // Set background color
            };

            // Influence the title bar color
            
            


            // Create a label and set its properties
            Label messageLabel = new Label()
            {
                Text = message,
                AutoSize = true, // Adjust label size based on content
                Left = 20,
                Top = 20,
                MaximumSize = new Size(440, 0), // Limit label width to 440 pixels
                ForeColor = Color.Black,// Set text color
                BackColor = MGS3ButtonColor,
                // Set font as Segoe UI, 11.25pt, style=Bold
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold)
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
                BackColor = MGS3ButtonColor, // Set button background color
                ForeColor = Color.Black,// Set button text color
                // Remove border around the button
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold)

            };
            copyButton.Click += (sender, e) => {
                Clipboard.SetText(message);
            };
            messageBoxForm.Controls.Add(copyButton);

            // Create a close button
            Button closeButton = new Button()
            {
                Text = "Ok",
                Width = 75,
                Height = 30,
                DialogResult = DialogResult.Cancel,
                Location = new Point(copyButton.Right + 20, copyButton.Top), // Next to the copy button
                BackColor = MGS3ButtonColor, // Set button background color
                ForeColor = Color.Black,// Set button text color
                // Remove border around the button
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold)
            };
            messageBoxForm.Controls.Add(closeButton);

            // Show the form as a dialog
            messageBoxForm.ShowDialog();
        }


    }
}
