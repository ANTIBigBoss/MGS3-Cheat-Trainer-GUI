using System.Diagnostics;
using System.Text;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class MiscForm : Form
    {
        #region Form Load and Close
        private int userCamoIndex;
        const float MinFov = 0.5f;
        const float MaxFov = 1.5f;
        const int SliderMin = 0;
        const int SliderMax = 15;

        public MiscForm()
        {
            InitializeComponent();
            CamoCheckBoxCheck();
            this.Activated += MiscForm_Activated;
            this.Deactivate += MiscForm_Deactivate;
            this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);
            CamoIndexChanges.CheckedChanged += new EventHandler(CamoIndexChanges_CheckedChanged);

            ModelSlider.Minimum = 0;
            ModelSlider.Maximum = 255;
            ModelSlider.Value = 40; // Default byte value and where the slider should start at
            ChangeModelNumber.Click += new EventHandler(ChangeModelNumber_Click);
            ModelSlider.Scroll += new EventHandler(ModelSlider_Scroll);

            System.Windows.Forms.Timer camoIndexTimer = new System.Windows.Forms.Timer();
            CamoIndexSlider.Minimum = -1000;
            CamoIndexSlider.Maximum = 1000;
            camoIndexTimer.Interval = 1000;
            camoIndexTimer.Tick += CamoIndexTimer_Tick;
            camoIndexTimer.Start();

            System.Windows.Forms.Timer snakesPositionTimer = new System.Windows.Forms.Timer();
            snakesPositionTimer.Interval = 100;
            snakesPositionTimer.Tick += SnakesPosition_Tick;
            snakesPositionTimer.Start();

            const int SliderMin = 0;
            const int SliderMax = 15;
            FovSlider.Minimum = SliderMin;
            FovSlider.Maximum = SliderMax;

        }

        private void MiscForm_Activated(object sender, EventArgs e)
        {
            CamoIndexSlider.Value = Math.Max(CamoIndexSlider.Minimum, Math.Min(TimerManager.UserCamoIndex, CamoIndexSlider.Maximum));
        }

        private void MiscForm_Deactivate(object sender, EventArgs e)
        {
            TimerManager.UserCamoIndex = CamoIndexSlider.Value;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            CamoIndexChanges.CheckedChanged -= CamoIndexChanges_CheckedChanged;

            this.Location = MemoryManager.GetLastFormLocation();

            CamoIndexChanges.CheckedChanged += CamoIndexChanges_CheckedChanged;

            float currentFovValue = MiscManager.Instance.ReadFovSlider();

            int sliderValue = (int)((currentFovValue - MinFov) / (MaxFov - MinFov) * (SliderMax - SliderMin));

            FovSlider.Value = Math.Max(FovSlider.Minimum, Math.Min(sliderValue, FovSlider.Maximum));

            PissFilterCheckBox.Checked = FilterManager.Instance.IsPissFilterInstructionsNopped();

            MinimalHudCheck();
            FullHudCheck();
            RealTimeItemSwapCheckbox.Checked = TimerManager.RealTimeSwapping;

            bool isMultiplierActive = MiscManager.IsDamageMultiplierActive();

            ChangeDamageMultiNumberButton.Enabled = isMultiplierActive;
            Plus1MultiValue.Enabled = isMultiplierActive;
            Minus1MultiValue.Enabled = isMultiplierActive;
            DamageToSnakeMultiTextbox.Enabled = isMultiplierActive;


            ActivateDamageMulti.Text = isMultiplierActive ? "Disable Damage Multiplier" : "Enable Damage Multiplier";
        }

        #region Xyz Manipulation

        private void ParseTextBoxesPositions_Click(object sender, EventArgs e)
        {
            // Ensure all textboxes have valid float values
            if (float.TryParse(TextBoxSnakeX.Text, out float x) &&
                float.TryParse(TextBoxSnakeY.Text, out float y) &&
                float.TryParse(TextBoxSnakeZ.Text, out float z))
            {
                float[][] coordinates = new float[][] { new float[] { x, y, z } };

                IntPtr snakePointerAddress = XyzManager.Instance.FindPointerMemory(11810, 0x130);
                if (snakePointerAddress == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to find Snake's position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    try
                    {
                        XyzManager.Instance.TeleportSnake(coordinates);
                    }
                    finally
                    {
                        MemoryManager.NativeMethods.CloseHandle(processHandle);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to open game process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid position values. Please enter valid numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FetchAndDisplaySnakePosition()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle != IntPtr.Zero)
            {
                try
                {
                    // Get the updated Snake position
                    float[] position = XyzManager.Instance.ReadSnakePosition(processHandle);

                    if (position != null)
                    {
                        // Always update the textboxes with the new position values
                        Invoke(new Action(() =>
                        {
                            ReadTextBoxSnakeX.Text = position[0].ToString("F2");
                            ReadTextBoxSnakeY.Text = position[1].ToString("F2");
                            ReadTextBoxSnakeZ.Text = position[2].ToString("F2");
                        }));
                    }
                    else
                    {
                        LoggingManager.Instance.Log("Failed to read Snake's position.");
                    }
                }
                catch (Exception ex)
                {
                    LoggingManager.Instance.Log($"Exception while reading Snake's position: {ex.Message}");
                }
                finally
                {
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
            }
            else
            {
                LoggingManager.Instance.Log("Failed to open game process.");
            }
        }


        private void SnakesPosition_Tick(object sender, EventArgs e)
        {
            FetchAndDisplaySnakePosition();
        }

        private void TeleportGuardsToSnake_Click(object sender, EventArgs e)
        {
            XyzManager.Instance.MoveAllGuardsToSnake();
        }

        private void SnakeJump_Click(object sender, EventArgs e)
        {
            XyzManager.Instance.RaiseSnakeYBy4000();
        }

        // Pretty hacky but, it'll work if the player is moving backwards when not on the ladder.
        // We'll change this to force an area load then find those coordinates in the future.
        private async void LadderSkip_Click_1(object sender, EventArgs e)
        {
            // Define multiple sets of coordinates for successive teleportation
            float[][] coordinates = new float[][]
            {
                // First set of coordinates
                new float[] { -1273f, 901f, -30441f }, 
                // Keep increasing the Y to test theory of how the ladder gcx works
                new float[] { -1251f, 2000f, -30441f },
                new float[] { -1251f, 154840f, -31441f },
                new float[] { -1251f, 4000f, -30441f },
                new float[] { -1251f, 154840f, -30430f },

            };

            foreach (var coordSet in coordinates)
            {
                // Execute the teleport to new position using the current set of coordinates
                XyzManager.Instance.TeleportSnake(new float[][] { coordSet }); // Adjust the method if necessary

                // Add a half-second delay between teleports
                await Task.Delay(1000); // 500 milliseconds = 0.5 seconds
            }
        }
        #endregion

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Conditional check for the hud for when the form is loaded
        public bool MinimalHudCheck()
        {
            if (TimerManager.hudAlwaysHidden)
            {
                MinimalHudcheckbox.Checked = true;
                return true;
            }
            else
            {
                MinimalHudcheckbox.Checked = false;
                return false;
            }
        }

        public bool FullHudCheck()
        {
            if (TimerManager.fullHudAlwaysHidden)
            {
                noHudcheckbox.Checked = true;
                return true;
            }
            else
            {
                noHudcheckbox.Checked = false;
                return false;
            }
        }

        private void MinimalHudcheckbox_CheckedChanged(object sender, EventArgs e)
        {

            if (MinimalHudcheckbox.Checked)
            {
                TimerManager.hudAlwaysHidden = true;
                TimerManager.ToggleMinimalHud(true);
                noHudcheckbox.Enabled = false;
                LoggingManager.Instance.Log("Minimal HUD enabled");
            }
            else
            {
                TimerManager.hudAlwaysHidden = false;
                TimerManager.ToggleMinimalHud(false);
                noHudcheckbox.Enabled = true;
                LoggingManager.Instance.Log("Hud is now back to normal from Minimal HUD");
            }

        }

        private void noHudcheckbox_CheckedChanged(object sender, EventArgs e)
        {

            if (noHudcheckbox.Checked)
            {
                TimerManager.fullHudAlwaysHidden = true;
                TimerManager.ToggleFullHud(true);
                MinimalHudcheckbox.Enabled = false;
                LoggingManager.Instance.Log("No HUD enabled");
            }
            else
            {
                TimerManager.fullHudAlwaysHidden = false;
                TimerManager.ToggleFullHud(false);
                MinimalHudcheckbox.Enabled = true;
                LoggingManager.Instance.Log("Hud is now back to normal from No HUD");
            }

        }

        private void RealTimeItemSwapCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (RealTimeItemSwapCheckbox.Checked)
            {
                TimerManager.RealTimeSwapping = true;
                TimerManager.RealTimeWeaponItemSwapping();
            }
            else
            {
                TimerManager.RealTimeSwapping = false;
                TimerManager.DisableRealTimeWeaponItemSwapping();
            }
        }

        #region Camo  

        private void CamoIndexSlider_Scroll(object sender, EventArgs e)
        {
            userCamoIndex = CamoIndexSlider.Value;

            if (MiscManager.Instance.IsCamoIndexNopped())
            {
                int newValue = CamoIndexSlider.Value;
                MiscManager.Instance.AdjustCamoIndex(newValue);
            }
            else
            {
                MessageBox.Show("Please check the box to enable camo index changes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CamoIndexTimer_Tick(object sender, EventArgs e)
        {
            int currentCamoIndex = MiscManager.Instance.ReadCamoIndex();
            float percentage = (float)currentCamoIndex / 10;
            CamoIndexTextbox.Text = $"Camo Index is at {percentage}%";

            if (MiscManager.Instance.IsCamoIndexNopped())
            {
                userCamoIndex = CamoIndexSlider.Value;
                MiscManager.Instance.AdjustCamoIndex(userCamoIndex);
            }

            CamoIndexSlider.Enabled = MiscManager.Instance.IsCamoIndexNopped();
        }

        private void CamoIndexChanges_CheckedChanged(object sender, EventArgs e)
        {
            if (CamoIndexChanges.Checked)
            {
                MiscManager.Instance.EnableNOPCamoIndex();
            }
            else
            {
                MiscManager.RestoreCamoIndex();
            }
        }

        private bool CamoCheckBoxCheck()
        {
            bool isNopped = MiscManager.Instance.IsCamoIndexNopped();
            CamoIndexChanges.Checked = isNopped;
            return isNopped;
        }

        #endregion

        #endregion

        #region HUD and Camera

        private void NormalCam_Click(object sender, EventArgs e)
        {
            MiscManager.ChangeCamera(Constants.CameraOptions.Normal);
            LoggingManager.Instance.Log("Camera has been set to normal");
        }

        private void UpsideDownCam_Click(object sender, EventArgs e)
        {
            MiscManager.ChangeCamera(Constants.CameraOptions.UpsideDown);
            LoggingManager.Instance.Log("Camera has been set to upside down");
        }

        private void FovSlider_Scroll(object sender, EventArgs e)
        {
            // Read current slider value
            int sliderValue = FovSlider.Value;

            // Map slider value to FOV range
            float newFovValue = MinFov + (sliderValue - SliderMin) * (MaxFov - MinFov) / (SliderMax - SliderMin);

            // Update slider value ensuring it's within the valid range
            int clampedSliderValue = Math.Max(FovSlider.Minimum, Math.Min(sliderValue, FovSlider.Maximum));

            // Update slider value without triggering the Scroll event
            FovSlider.Value = clampedSliderValue;

            // Set new FOV value in game memory
            MiscManager.Instance.SetFovSlider(newFovValue);
        }

        #endregion

        #region Model Manipulation
        private void UpdateModelValueTextBox()
        {
            ModelCurrentValue.Text = ModelSlider.Value.ToString();
        }

        private void ModelSlider_Scroll(object sender, EventArgs e)
        {
            byte sliderValue = (byte)ModelSlider.Value;
            MiscManager.ModifyModel(sliderValue);
            UpdateModelValueTextBox();
        }

        private void ResetModelsToNormal_Click(object sender, EventArgs e)
        {
            MiscManager.ModifyModel(40);
            ModelSlider.Value = 40;
            UpdateModelValueTextBox();
            LoggingManager.Instance.Log("Models reset to normal");
        }

        private void ChangeModelNumber_Click(object sender, EventArgs e)
        {
            if (int.TryParse(ModelChangeValue.Text, out int modelValue) && modelValue >= 0 && modelValue <= 255)
            {
                MiscManager.ModifyModel((byte)modelValue);
                ModelSlider.Value = modelValue;
                UpdateModelValueTextBox();
                LoggingManager.Instance.Log($"Model changed to {modelValue}");
            }
            else
            {
                MessageBox.Show("Please enter a valid number between 0 and 255.");
                LoggingManager.Instance.Log("The user entered an invalid model number");
            }
        }

        private void Minus1ModelValue_Click(object sender, EventArgs e)
        {
            if (ModelSlider.Value > ModelSlider.Minimum)
            {
                ModelSlider.Value--;
                MiscManager.ModifyModel((byte)ModelSlider.Value);
                UpdateModelValueTextBox();
                LoggingManager.Instance.Log("Model value decreased by 1");
            }
        }

        private void Plus1ModelValue_Click(object sender, EventArgs e)
        {
            if (ModelSlider.Value < ModelSlider.Maximum)
            {
                ModelSlider.Value++;
                MiscManager.ModifyModel((byte)ModelSlider.Value);
                UpdateModelValueTextBox();
                LoggingManager.Instance.Log("Model value increased by 1");
            }
        }
        #endregion

        private void PissFilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PissFilterCheckBox.Checked)
            {
                DayChange.Enabled = false;
                NightChange.Enabled = false;
                // Disable the piss filter instructions and the filter itself
                FilterManager.Instance.DisablePissFilterInstructions();
                FilterManager.Instance.DisablePissFilter();
            }

            else
            {
                DayChange.Enabled = true;
                NightChange.Enabled = true;
                // Enable the piss filter instructions and the filter itself
                FilterManager.Instance.EnablePissFilterInstructions();
                FilterManager.Instance.EnablePissFilter();
            }
        }

        private void CopySnakesLocationToTextboxes_Click(object sender, EventArgs e)
        {
            TextBoxSnakeX.Text = ReadTextBoxSnakeX.Text;
            TextBoxSnakeY.Text = ReadTextBoxSnakeY.Text;
            TextBoxSnakeZ.Text = ReadTextBoxSnakeZ.Text;
        }

        private void DayChange_Click(object sender, EventArgs e)
        {
            FilterManager.Instance.SetToDayMode();
            LoggingManager.Instance.Log("Switched to Day Mode");
        }

        private void NightChange_Click(object sender, EventArgs e)
        {
            FilterManager.Instance.SetToNightMode();
            LoggingManager.Instance.Log("Switched to Night Mode");
        }

        private void SwapToWeaponsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Weapon form from the Misc form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "WeaponForm");
            WeaponForm form1 = new();
            form1.Show();
            this.Hide();
        }

        private void SwapToItemsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Item form from the Misc form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "ItemForm");
            ItemForm form2 = new();
            form2.Show();
            this.Hide();
        }

        private void SwapToCamoForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Camo form from the Misc form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "CamoForm");
            CamoForm form3 = new();
            form3.Show();
            this.Hide();
        }

        private void SwapToHealthAndAlertsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Stats and Alert form from the Misc form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "StatsAndAlertForm");
            StatsAndAlertForm form5 = new();
            form5.Show();
            this.Hide();
        }

        private void SwapToBossForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Boss form from the Misc form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "BossForm");
            BossForm form6 = new();
            form6.Show();
            this.Hide();
        }

        private void SwapToGameStatsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Game Stats form from the Misc form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "GameStatsForm");
            GameStatsForm form7 = new();
            form7.Show();
            this.Hide();
        }

        #region Damage Multiplier Controls

        private void UpdateDamageMultiplierTextBox()
        {
            if (int.TryParse(DamageToSnakeMultiTextbox.Text, out int multiplierValue))
            {
                if (multiplierValue < 1 || multiplierValue > 100)
                {
                    MessageBox.Show("Please enter a valid number between 1 and 100.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoggingManager.Instance.Log($"Invalid multiplier value entered: {multiplierValue}");
                    return;
                }

                MiscManager.AdjustDamageMultiplier((byte)multiplierValue);
                LoggingManager.Instance.Log($"Damage Multiplier updated to {multiplierValue}.");
            }
            else
            {
                MessageBox.Show("Please enter a valid number between 1 and 100.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoggingManager.Instance.Log("The user entered an invalid damage multiplier value.");
            }
        }

        private void Minus1MultiValue_Click(object sender, EventArgs e)
        {
            if (int.TryParse(DamageToSnakeMultiTextbox.Text, out int multiplierValue) && multiplierValue > 1)
            {
                multiplierValue--;
                MiscManager.AdjustDamageMultiplier((byte)multiplierValue);
                DamageToSnakeMultiTextbox.Text = multiplierValue.ToString();
                LoggingManager.Instance.Log("Damage Multiplier decreased by 1.");
            }
        }

        private void Plus1MultiValue_Click(object sender, EventArgs e)
        {
            if (int.TryParse(DamageToSnakeMultiTextbox.Text, out int multiplierValue) && multiplierValue < 100)
            {
                multiplierValue++;
                MiscManager.AdjustDamageMultiplier((byte)multiplierValue);
                DamageToSnakeMultiTextbox.Text = multiplierValue.ToString();
                LoggingManager.Instance.Log("Damage Multiplier increased by 1.");
            }
        }

        private void ActivateDamageMulti_Click(object sender, EventArgs e)
        {
            MiscManager.EnableDamageMultiplier();

            ChangeDamageMultiNumberButton.Enabled = true;
            Plus1MultiValue.Enabled = true;
            Minus1MultiValue.Enabled = true;
            DamageToSnakeMultiTextbox.Enabled = true;

            LoggingManager.Instance.Log("Damage Multiplier Enabled.");
        }

        private void DisableDamageMulti_Click(object sender, EventArgs e)
        {
            MiscManager.DisableDamageMultiplier();

            ChangeDamageMultiNumberButton.Enabled = false;
            Plus1MultiValue.Enabled = false;
            Minus1MultiValue.Enabled = false;
            DamageToSnakeMultiTextbox.Enabled = false;

            LoggingManager.Instance.Log("Damage Multiplier Disabled.");
        }

        private void ChangeDamageMultiNumberButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(DamageToSnakeMultiTextbox.Text, out int multiplierValue) && multiplierValue >= 1 && multiplierValue <= 100)
            {
                MiscManager.AdjustDamageMultiplier((byte)multiplierValue);
                LoggingManager.Instance.Log($"Damage Multiplier set to {multiplierValue}.");
            }
            else
            {
                MessageBox.Show("Please enter a valid number between 1 and 100.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoggingManager.Instance.Log("Invalid Damage Multiplier value entered.");
            }
        }

        #endregion

    }
}