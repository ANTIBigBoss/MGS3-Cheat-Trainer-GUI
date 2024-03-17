using System.Diagnostics;
using System.Text;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class MiscForm : Form
    {
        #region Form Load and Close
        public MiscForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);

            // Range for the model manipulation
            ModelSlider.Minimum = 0;
            ModelSlider.Maximum = 255;
            ModelSlider.Value = 40; // Default byte value and where the slider should start at
            CamoIndexSlider.Minimum = -1000;
            CamoIndexSlider.Maximum = 1000;
            ChangeModelNumber.Click += new EventHandler(ChangeModelNumber_Click);
            ModelSlider.Scroll += new EventHandler(ModelSlider_Scroll);
            SnakesPosition.Enabled = true;
            SnakesPosition.Interval = 1000; // Update every second
            SnakesPosition.Tick += new EventHandler(SnakesPosition_Tick);

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Location = MemoryManager.GetLastFormLocation();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
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

        #region Camo  
        private void NopCamo_Click(object sender, EventArgs e)
        {
            // Ensure the MemoryManager has the necessary method to perform the NOP operation
            MiscManager.Instance.EnableNOPCamoIndex();
        }


        private void RestoreCamo_Click(object sender, EventArgs e)
        {
            MiscManager.RestoreCamoIndex();
        }

        private void LogAOBs_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.LogAOBAddresses();
        }

        private void CamoIndexSlider_Scroll(object sender, EventArgs e)
        {
            // Get the new value from the slider
            int newValue = CamoIndexSlider.Value;

            // Call the method to adjust the camo index
            MiscManager.Instance.AdjustCamoIndex(newValue);
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
        #endregion



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
                        Log("Failed to read Snake's position.");
                    }
                }
                catch (Exception ex)
                {
                    Log($"Exception while reading Snake's position: {ex.Message}");
                }
                finally
                {
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
            }
            else
            {
                Log("Failed to open game process.");
            }
        }

        private void Log(string message)
        {
            // Implement logging to your preference
            Console.WriteLine(message);
        }



    }
}