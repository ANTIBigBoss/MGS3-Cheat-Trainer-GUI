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

            Form messageBoxForm = new Form()
            {
                FormBorderStyle = FormBorderStyle.None,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false,
                BackColor = SurvivalViewerColor // Set background color
            };

            // Custom icon implementation for later use
            //SetMessageBoxIcon(messageBoxForm, "path_to_icon_file.ico");

            Panel titleBarPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = SystemInformation.CaptionHeight,
                BackColor = MGS3ButtonColor,
            };
            messageBoxForm.Controls.Add(titleBarPanel);

            Label titleLabel = new Label()
            {
                Text = caption,
                AutoSize = true,
                ForeColor = Color.Black,
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

            titleBarPanel.MouseMove += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    messageBoxForm.Left += e.X - _lastClick.X;
                    messageBoxForm.Top += e.Y - _lastClick.Y;
                }
            };

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
            int formWidth = Math.Min(messageLabel.Width + 40, 500);
            int formHeight = Math.Min(messageLabel.Height + 120, 200);

            messageBoxForm.Width = formWidth;
            messageBoxForm.Height = formHeight;

            messageLabel.Left = (messageBoxForm.ClientSize.Width - messageLabel.Width) / 2;

            int buttonLeft = (messageBoxForm.ClientSize.Width - 2 * 75 - 20) / 2;

            Button copyButton = new Button()
            {
                Text = "Copy",
                Width = 75,
                Height = 30,
                DialogResult = DialogResult.OK,
                Location = new Point(buttonLeft, messageLabel.Bottom + 10),
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold),
                BackColor = MGS3ButtonColor,
                ForeColor = Color.Black
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
                Location = new Point(copyButton.Right + 20, copyButton.Top),
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold),
                BackColor = MGS3ButtonColor,
                ForeColor = Color.Black 
            };
            okButton.Click += (sender, e) =>
            {
                messageBoxForm.Close();
            };
            messageBoxForm.Controls.Add(okButton);

            messageBoxForm.ShowDialog();
        }

        // This one is mostly just for the button in GameStatsForm to edit their stats
        public static void ShowEditStatsDialog(MainPointerManager pointerManager)
        {
            Form editForm = new Form()
            {
                FormBorderStyle = FormBorderStyle.None,
                Text = "Edit Stats",
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false,
                BackColor = SurvivalViewerColor,
                Width = 500,
                Height = 500
            };

            Panel titleBarPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = SystemInformation.CaptionHeight,
                BackColor = MGS3ButtonColor,
            };
            editForm.Controls.Add(titleBarPanel);

            Label titleLabel = new Label()
            {
                Text = "Edit Stats",
                AutoSize = true,
                ForeColor = Color.Black,
                BackColor = MGS3ButtonColor,
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold),
                Location = new Point(10, (titleBarPanel.Height - 20) / 2)
            };
            titleBarPanel.Controls.Add(titleLabel);

            titleBarPanel.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    _lastClick = e.Location;
                }
            };

            titleBarPanel.MouseMove += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    editForm.Left += e.X - _lastClick.X;
                    editForm.Top += e.Y - _lastClick.Y;
                }
            };

            Panel inputPanel = new Panel()
            {
                BackColor = MGS3ButtonColor,
                Dock = DockStyle.Fill
            };
            editForm.Controls.Add(inputPanel);

            // Current values
            string difficultyCurrent = pointerManager.ReadDifficulty();
            string playTimeCurrent = pointerManager.ReadPlayTime();
            string savesCurrent = pointerManager.ReadSaves();
            string continuesCurrent = pointerManager.ReadContinues();
            string alertsCurrent = pointerManager.ReadAlertsTriggered();
            string humansKilledCurrent = pointerManager.ReadHumansKilled();
            string injuriesCurrent = pointerManager.ReadTimesSeriouslyInjured();
            string totalDamageCurrent = pointerManager.ReadTotalDamageTaken();
            string mealsCurrent = pointerManager.ReadMealsEaten();
            string lifeMedsCurrent = pointerManager.ReadLifeMedsUsed();
            string specialItemsCurrent = pointerManager.ReadSpecialItemsUsed(); // Read current special items usage

            int topOffset = 40;
            int leftOffset = 20;
            int spacing = 40;

            Label CreateLabel(string text)
            {
                return new Label
                {
                    Text = text,
                    AutoSize = true,
                    ForeColor = Color.Black,
                    BackColor = MGS3ButtonColor,
                    Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                    Location = new Point(leftOffset, topOffset)
                };
            }

            TextBox CreateNumericTextBox(string defaultValue)
            {
                TextBox tb = new TextBox
                {
                    Width = 100,
                    Location = new Point(leftOffset + 180, topOffset - 2),
                    Font = new Font("Segoe UI", 10f),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Text = defaultValue
                };

                tb.KeyPress += (s, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                };

                return tb;
            }

            Label diffLabel = CreateLabel("Difficulty:");
            inputPanel.Controls.Add(diffLabel);

            ComboBox difficultyCombo = new ComboBox
            {
                Location = new Point(leftOffset + 180, topOffset - 2),
                Width = 150,
                Font = new Font("Segoe UI", 10f),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Items = { "Very Easy", "Easy", "Normal", "Hard", "Extreme", "European Extreme" }
            };
            inputPanel.Controls.Add(difficultyCombo);

            if (difficultyCombo.Items.Contains(difficultyCurrent))
                difficultyCombo.SelectedItem = difficultyCurrent;
            else
                difficultyCombo.SelectedIndex = 0;

            topOffset += spacing;

            Label playTimeLabel = CreateLabel("Play Time (HH:MM:SS):");
            inputPanel.Controls.Add(playTimeLabel);
            TextBox playTimeBox = new TextBox
            {
                Width = 100,
                Location = new Point(leftOffset + 180, topOffset - 2),
                Font = new Font("Segoe UI", 10f),
                BackColor = Color.White,
                ForeColor = Color.Black,
                Text = playTimeCurrent
            };
            inputPanel.Controls.Add(playTimeBox);
            topOffset += spacing;

            Label savesLabel = CreateLabel("Saves (0-9999)");
            inputPanel.Controls.Add(savesLabel);
            TextBox savesBox = CreateNumericTextBox(savesCurrent);
            inputPanel.Controls.Add(savesBox);
            topOffset += spacing;

            Label continuesLabel = CreateLabel("Continues (0-9999)");
            inputPanel.Controls.Add(continuesLabel);
            TextBox continuesBox = CreateNumericTextBox(continuesCurrent);
            inputPanel.Controls.Add(continuesBox);
            topOffset += spacing;

            Label alertsLabel = CreateLabel("Alerts Triggered (0-9999)");
            inputPanel.Controls.Add(alertsLabel);
            TextBox alertsBox = CreateNumericTextBox(alertsCurrent);
            inputPanel.Controls.Add(alertsBox);
            topOffset += spacing;

            Label humansLabel = CreateLabel("Humans Killed (0-9999)");
            inputPanel.Controls.Add(humansLabel);
            TextBox humansBox = CreateNumericTextBox(humansKilledCurrent);
            inputPanel.Controls.Add(humansBox);
            topOffset += spacing;

            Label injuriesLabel = CreateLabel("Injuries (0-9999)");
            inputPanel.Controls.Add(injuriesLabel);
            TextBox injuriesBox = CreateNumericTextBox(injuriesCurrent);
            inputPanel.Controls.Add(injuriesBox);
            topOffset += spacing;

            Label totalDamageLabel = CreateLabel("Damage Taken (0-9999)");
            inputPanel.Controls.Add(totalDamageLabel);
            TextBox totalDamageBox = CreateNumericTextBox(totalDamageCurrent);
            inputPanel.Controls.Add(totalDamageBox);
            topOffset += spacing;

            Label lifeMedsLabel = CreateLabel("Life Meds Used (0-9999)");
            inputPanel.Controls.Add(lifeMedsLabel);
            TextBox lifeMedsBox = CreateNumericTextBox(lifeMedsCurrent);
            inputPanel.Controls.Add(lifeMedsBox);
            topOffset += spacing;

            Label mealsLabel = CreateLabel("Meals Eaten (0-9999)");
            inputPanel.Controls.Add(mealsLabel);
            TextBox mealsBox = CreateNumericTextBox(mealsCurrent);
            inputPanel.Controls.Add(mealsBox);
            topOffset += spacing;

            // Special Items
            Label specialItemsLabel = CreateLabel("Special Items Used");
            inputPanel.Controls.Add(specialItemsLabel);

            // Add the exact strings from the MainPointerManager dictionary to the ComboBox
            ComboBox specialItemsCombo = new ComboBox
            {
                Location = new Point(leftOffset + 180, topOffset - 2),
                Width = 285,
                Font = new Font("Segoe UI", 10f),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Items =
        {
            "Not Used",
            "Stealth Camo Used",
            "Infinity Facepaint Used",
            "Stealth Camo + Infinity Facepaint Used",
            "Ez Gun Used",
            "Stealth Camo + Ez Gun Used",
            "Infinity Facepaint + Ez Gun Used",
            "Stealth Camo + Infinity Facepaint + Ez Gun Used"
        }
            };
            inputPanel.Controls.Add(specialItemsCombo);
            topOffset += spacing;

            // Set the combo to current special items
            if (specialItemsCombo.Items.Contains(specialItemsCurrent))
                specialItemsCombo.SelectedItem = specialItemsCurrent;
            else
                specialItemsCombo.SelectedIndex = 0; // Default if not recognized

            Button submitButton = new Button
            {
                Text = "Submit",
                Width = 75,
                Height = 30,
                BackColor = MGS3ButtonColor,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold),
                Location = new Point(editForm.ClientSize.Width / 2 - 85, topOffset + 20)
            };
            inputPanel.Controls.Add(submitButton);

            Button cancelButton = new Button
            {
                Text = "Cancel",
                Width = 75,
                Height = 30,
                BackColor = MGS3ButtonColor,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 11.25f, FontStyle.Bold),
                Location = new Point(editForm.ClientSize.Width / 2 + 10, topOffset + 20)
            };
            inputPanel.Controls.Add(cancelButton);

            cancelButton.Click += (s, e) => editForm.Close();

            submitButton.Click += (s, e) =>
            {
                if (difficultyCombo.SelectedItem == null)
                {
                    CustomMessageBox("Please select a difficulty.", "Input Error");
                    return;
                }

                string selectedDifficulty = (string)difficultyCombo.SelectedItem;
                byte diffVal;
                switch (selectedDifficulty)
                {
                    case "Very Easy": diffVal = 10; break;
                    case "Easy": diffVal = 20; break;
                    case "Normal": diffVal = 30; break;
                    case "Hard": diffVal = 40; break;
                    case "Extreme": diffVal = 50; break;
                    case "European Extreme": diffVal = 60; break;
                    default: diffVal = 30; break; // default Normal
                }

                if (!TimeSpan.TryParseExact(playTimeBox.Text, "hh\\:mm\\:ss", null, out TimeSpan newTime))
                {
                    CustomMessageBox("Invalid PlayTime. Use HH:MM:SS format.", "Input Error");
                    return;
                }
                uint frames = (uint)newTime.TotalSeconds * 60;

                bool CheckUshortLimit(TextBox tb, string fieldName, out ushort val)
                {
                    val = 0;
                    if (!ushort.TryParse(tb.Text, out ushort result))
                    {
                        CustomMessageBox($"{fieldName} must be a number.", "Input Error");
                        return false;
                    }
                    if (result > 9999)
                    {
                        CustomMessageBox($"{fieldName} cannot exceed 9999.", "Input Error");
                        return false;
                    }
                    val = result;
                    return true;
                }

                if (!CheckUshortLimit(alertsBox, "Alerts Triggered", out ushort alertsVal)) return;
                if (!CheckUshortLimit(savesBox, "Saves", out ushort savesVal)) return;
                if (!CheckUshortLimit(continuesBox, "Continues", out ushort continuesVal)) return;
                if (!CheckUshortLimit(humansBox, "Humans Killed", out ushort humansVal)) return;
                if (!CheckUshortLimit(injuriesBox, "Injuries", out ushort injVal)) return;
                if (!CheckUshortLimit(mealsBox, "Meals Eaten", out ushort mealsVal)) return;
                if (!CheckUshortLimit(lifeMedsBox, "Life Meds Used", out ushort lifeMedsVal)) return;
                if (!CheckUshortLimit(totalDamageBox, "Damage Taken", out ushort damageVal)) return;

                // Special items
                if (specialItemsCombo.SelectedItem == null)
                {
                    CustomMessageBox("Please select a Special Items usage.", "Input Error");
                    return;
                }

                string selectedSpecialItems = (string)specialItemsCombo.SelectedItem;
                // Map back to byte
                byte specialItemsVal;
                // We'll do a small switch or dictionary lookup
                switch (selectedSpecialItems)
                {
                    case "Not Used": specialItemsVal = 0; break;
                    case "Stealth Camo Used": specialItemsVal = 1; break;
                    case "Infinity Facepaint Used": specialItemsVal = 2; break;
                    case "Stealth Camo + Infinity Facepaint Used": specialItemsVal = 3; break;
                    case "Ez Gun Used": specialItemsVal = 4; break;
                    case "Stealth Camo + Ez Gun Used": specialItemsVal = 5; break;
                    case "Infinity Facepaint + Ez Gun Used": specialItemsVal = 6; break;
                    case "Stealth Camo + Infinity Facepaint + Ez Gun Used": specialItemsVal = 7; break;
                    default: specialItemsVal = 0; break; // default to Not Used
                }

                bool success = true;
                success &= pointerManager.WriteDifficulty(diffVal);
                success &= pointerManager.WritePlayTime(frames);
                success &= pointerManager.WriteAlertsTriggered(alertsVal);
                success &= pointerManager.WriteSaves(savesVal);
                success &= pointerManager.WriteContinues(continuesVal);
                success &= pointerManager.WriteHumansKilled(humansVal);
                success &= pointerManager.WriteTimesSeriouslyInjured(injVal);
                success &= pointerManager.WriteMealsEaten(mealsVal);
                success &= pointerManager.WriteLifeMedsUsed(lifeMedsVal);
                success &= pointerManager.WriteTotalDamageTaken(damageVal);
                success &= pointerManager.WriteSpecialItemsUsed(specialItemsVal);

                if (success)
                {
                    CustomMessageBox("Stats updated successfully!", "Success");
                    editForm.Close();
                }
                else
                {
                    CustomMessageBox("Some writes failed. Check logs.", "Write Error");
                }
            };

            editForm.Height = topOffset + 120;
            editForm.ShowDialog();
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