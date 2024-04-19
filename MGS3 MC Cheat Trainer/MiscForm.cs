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
            // Check if the camo index is nopped and set the checkbox accordingly
            CamoCheckBoxCheck();
            this.Activated += MiscForm_Activated;
            this.Deactivate += MiscForm_Deactivate;
            // Add event handlers
            this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);
            CamoIndexChanges.CheckedChanged += new EventHandler(CamoIndexChanges_CheckedChanged);

            // All UI information for Model Distortion/Manipulation
            ModelSlider.Minimum = 0;
            ModelSlider.Maximum = 255;
            ModelSlider.Value = 40; // Default byte value and where the slider should start at
            ChangeModelNumber.Click += new EventHandler(ChangeModelNumber_Click);
            ModelSlider.Scroll += new EventHandler(ModelSlider_Scroll);

            // Timer to check Snake's position once his AOB is found
            SnakesPosition.Enabled = true;
            SnakesPosition.Interval = 1000;
            SnakesPosition.Tick += new EventHandler(SnakesPosition_Tick);

            // Timer and slider information for camo index
            System.Windows.Forms.Timer camoIndexTimer = new System.Windows.Forms.Timer();
            CamoIndexSlider.Minimum = -1000;
            CamoIndexSlider.Maximum = 1000;
            camoIndexTimer.Interval = 1000;
            camoIndexTimer.Tick += CamoIndexTimer_Tick;
            camoIndexTimer.Start();

            // Slider range mapping constants

            const int SliderMin = 0;
            const int SliderMax = 15;
            // Initialize slider range
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
            // Detach event handler while loading the form
            CamoIndexChanges.CheckedChanged -= CamoIndexChanges_CheckedChanged;

            // Set form location
            this.Location = MemoryManager.GetLastFormLocation();

            // Reattach event handler after loading the form
            CamoIndexChanges.CheckedChanged += CamoIndexChanges_CheckedChanged;

            // Read the current FOV value from game memory
            float currentFovValue = MiscManager.Instance.ReadFovSlider();

            // Map the FOV value to the slider range
            int sliderValue = (int)((currentFovValue - MinFov) / (MaxFov - MinFov) * (SliderMax - SliderMin));

            // Set the FOV slider position
            FovSlider.Value = Math.Max(FovSlider.Minimum, Math.Min(sliderValue, FovSlider.Maximum));

            PissFilterCheckBox.Checked = MiscManager.Instance.IsPissFilterInstructionsNopped();

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // using MiscManager.Instance.IsCamoIndexNopped(); we make a function to check if the camo index is nopped so we can use it when the form is loaded to determine if the checkbox should be checked or not
        #region Camo  


        private void CamoIndexSlider_Scroll(object sender, EventArgs e)
        {
            // Store the user's camo index value when the slider is adjusted
            TimerManager.UserCamoIndex = CamoIndexSlider.Value;

            // Adjust the camo index value if necessary
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
            // Optional: Update the UI or log only if needed, to reduce performance overhead
            int currentCamoIndex = MiscManager.Instance.ReadCamoIndex();
            float percentage = (float)currentCamoIndex / 10;
            CamoIndexTextbox.Text = $"Camo Index is at {percentage}%";

            // The critical part: Adjust camo index continuously if NOP is applied
            if (MiscManager.Instance.IsCamoIndexNopped())
            {
                TimerManager.UserCamoIndex = CamoIndexSlider.Value;
                MiscManager.Instance.AdjustCamoIndex(TimerManager.UserCamoIndex);
            }

            // Ensure the slider is enabled/disabled based on NOP status
            CamoIndexSlider.Enabled = MiscManager.Instance.IsCamoIndexNopped();
        }


        private void CamoIndexChanges_CheckedChanged(object sender, EventArgs e)
        {
            {
                // Check if the checkbox is checked
                if (CamoIndexChanges.Checked)
                {
                    MiscManager.Instance.EnableNOPCamoIndex();
                }
                else
                {
                    MiscManager.RestoreCamoIndex();
                }
            }
        }

        private bool CamoCheckBoxCheck()
        {
            // if the camo index is nopped, return true and make sure the checkbox is checked
            if (MiscManager.Instance.IsCamoIndexNopped())
            {
                CamoIndexChanges.Checked = true;
                return true;
            }

            // if the camo index is not nopped, return false and make sure the checkbox is not checked
            else
            {
                CamoIndexChanges.Checked = false;
                return false;
            }
        }

        #endregion


        #endregion

        #region Form Swaps
        private void WeaponFormSwap_Click(object sender, EventArgs e) // Weapon Form Swap
        {
            LoggingManager.Instance.Log("Navigating to Weapon Form from the Item Form");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "WeaponForm");
            WeaponForm f1 = new WeaponForm();
            f1.Show();
            this.Hide();
            LoggingManager.Instance.Log("Swapping to Weapon Form from the Misc Form");
        }

        private void button2_Click(object sender, EventArgs e) // Item Form Swap
        {
            LoggingManager.Instance.Log("Navigating to Item Form from the Misc Form");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "ItemForm");
            ItemForm f2 = new ItemForm();
            f2.Show();
            this.Hide();
            LoggingManager.Instance.Log("Swapping to Item Form from the Misc Form");
        }


        private void button1_Click(object sender, EventArgs e) // Camo Form Swap
        {
            LoggingManager.Instance.Log("Navigating to Camo Form from the Misc Form");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "CamoForm");
            CamoForm f3 = new CamoForm();
            f3.Show();
            this.Hide();
            LoggingManager.Instance.Log("Swapping to Camo Form from the Misc Form");
        }

        private void HealthFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("Navigating to Health Form from the Misc Form");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "HealthForm");
            StatsAndAlertForm form5 = new();
            form5.Show();
            this.Hide();
            LoggingManager.Instance.Log("Swapping to Stats and Alert Form from the Misc Form");
        }

        private void SwapToBossForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("Navigating to Boss Form from the Misc Form");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "BossForm");
            BossForm bossForm = new BossForm();
            bossForm.Show();
            this.Hide();
            LoggingManager.Instance.Log("Swapping to Boss Form from the Misc Form");
        }
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

        #region Xyz Manipulation
        private void TeleportGuardsToSnake_Click(object sender, EventArgs e)
        {
            XyzManager.Instance.MoveAllGuardsToSnake();
        }

        private void SnakeJump_Click(object sender, EventArgs e)
        {
            XyzManager.Instance.RaiseSnakeYBy4000();
        }

        private void SnakesXYZaob_Click(object sender, EventArgs e)
        {
            AobManager.Instance.FindAndStoreSnakesPositionAOB();
            if (AobManager.Instance.FoundSnakePositionAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    float[] snakePosition = XyzManager.Instance.ReadSnakePosition(processHandle);
                    MessageBox.Show($"Snake's Position: \nX={snakePosition[0]}, \nY={snakePosition[1]}, \nZ={snakePosition[2]}", "Snake Position", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    MessageBox.Show("Failed to open process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Snake position AOB not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Pretty hacky but, it'll work if the player is moving backwards when not on the ladder.
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


        private void ParseTextBoxesPositions_Click(object sender, EventArgs e)
        {
            // Ensure all textboxes have valid float values
            if (float.TryParse(TextBoxSnakeX.Text, out float x) &&
                float.TryParse(TextBoxSnakeY.Text, out float y) &&
                float.TryParse(TextBoxSnakeZ.Text, out float z))
            {
                // Use a single set of coordinates array for the new position
                float[][] coordinates = new float[][] { new float[] { x, y, z } };

                if (AobManager.Instance.FoundSnakePositionAddress == IntPtr.Zero && !AobManager.Instance.FindAndStoreSnakesPositionAOB())
                {
                    MessageBox.Show("Failed to find or verify Snake's position AOB.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    try
                    {
                        // Execute the teleport to new position using the coordinates from textboxes
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

        private void SnakesPosition_Tick(object sender, EventArgs e)
        {
            FetchAndDisplaySnakePosition();
        }

        private void FetchAndDisplaySnakePosition()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle != IntPtr.Zero)
            {
                try
                {
                    float[] position = XyzManager.Instance.ReadSnakePosition(processHandle);
                    if (position != null)
                    {
                        // Assuming ReadTextBoxSnakeX, etc., are your TextBox controls
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
        #endregion

        private void LogAOBs_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.LogAOBAddresses();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Call the method and store the result in a variable
            string result = StringManager.Instance.FindLocationStringFollowingR_Sna01();

            // Display the result in a MessageBox
            MessageBox.Show(result, "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Call the new method and store the result in a variable
            List<string> results = StringManager.Instance.FindAllR_Sna01AndLocationStringInstances();

            // Concatenate all results into a single string for display
            string concatenatedResults = string.Join(Environment.NewLine, results);

            // Display the results in a MessageBox
            MessageBox.Show(concatenatedResults, "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoggingManager.Instance.Log($"{results.Count} instances of R_Sna01 and LocationString found. Search results are as follows:\n{concatenatedResults}");
        }


        private void PissFilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PissFilterCheckBox.Checked)
            {
                // Disable the piss filter instructions and the filter itself
                MiscManager.Instance.DisablePissFilterInstructions();
                MiscManager.Instance.DisablePissFilter();
            }

            else
            {
                // Enable the piss filter instructions and the filter itself
                MiscManager.Instance.EnablePissFilterInstructions();
                MiscManager.Instance.EnablePissFilter();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // Retrieve formatted strings from MiscManager
                    string details1 = MiscManager.Instance.GetPissFilterInstructionsDetails();
                    string details2 = MiscManager.Instance.GetPissFilterInstructionsDetails2();

                    // Display the results in a MessageBox
                    MessageBox.Show($"{details1}\n{details2}", "Memory Address Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, possibly logging them or displaying a different message
                    MessageBox.Show("Failed to retrieve memory details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MiscManager.Instance.ReadPissFilterValue().ToString("X"));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MiscManager.Instance.DisablePissFilterInstructions();
            MiscManager.Instance.DisablePissFilter();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MiscManager.Instance.EnablePissFilterInstructions();
            MiscManager.Instance.EnablePissFilter();
        }

        private void CopySnakesLocationToTextboxes_Click(object sender, EventArgs e)
        {
            string message = DamageManager.Instance.ReadMostWeaponsDamage();
            CustomMessageBoxManager.CustomMessageBox(message, "Most Weapons Damage");
        }
    }
}