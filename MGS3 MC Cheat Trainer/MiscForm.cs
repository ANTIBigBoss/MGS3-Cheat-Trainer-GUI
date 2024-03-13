using System.Diagnostics;
using System.Text;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class MiscForm : Form
    {

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
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Location = MemoryManager.GetLastFormLocation();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

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
        private void NormalHUD_Click(object sender, EventArgs e)
        {
            ModelManager.ChangeHud((int)Constants.HudOptions.Normal);
            LoggingManager.Instance.Log("HUD set to Normal");
        }

        private void ShrinkHUD_Click(object sender, EventArgs e)
        {
            ModelManager.ChangeHud((int)Constants.HudOptions.Shrunk);
            LoggingManager.Instance.Log("HUD shrunk successfully");
        }

        private void NoHUD_Click(object sender, EventArgs e)
        {
            ModelManager.ChangeHud((int)Constants.HudOptions.None);
            LoggingManager.Instance.Log("HUD removed");
        }

        private void NormalCam_Click(object sender, EventArgs e)
        {
            ModelManager.ChangeCamera((int)Constants.CameraOptions.Normal);
            LoggingManager.Instance.Log("Camera has been set to normal");
        }

        private void UpsideDownCam_Click(object sender, EventArgs e)
        {
            ModelManager.ChangeCamera((int)Constants.CameraOptions.UpsideDown);
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
            ModelManager.ModifyModel(sliderValue);
            UpdateModelValueTextBox();
        }

        private void ResetModelsToNormal_Click(object sender, EventArgs e)
        {
            ModelManager.ModifyModel(40);
            ModelSlider.Value = 40;
            UpdateModelValueTextBox();
            LoggingManager.Instance.Log("Models reset to normal");
        }

        private void ChangeModelNumber_Click(object sender, EventArgs e)
        {
            if (int.TryParse(ModelChangeValue.Text, out int modelValue) && modelValue >= 0 && modelValue <= 255)
            {
                ModelManager.ModifyModel((byte)modelValue);
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
                ModelManager.ModifyModel((byte)ModelSlider.Value);
                UpdateModelValueTextBox();
                LoggingManager.Instance.Log("Model value decreased by 1");
            }
        }

        private void Plus1ModelValue_Click(object sender, EventArgs e)
        {
            if (ModelSlider.Value < ModelSlider.Maximum)
            {
                ModelSlider.Value++;
                ModelManager.ModifyModel((byte)ModelSlider.Value);
                UpdateModelValueTextBox();
                LoggingManager.Instance.Log("Model value increased by 1");
            }
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {

            if (MemoryManager.Instance.FoundSnakePositionAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    float[] snakePosition = MemoryManager.Instance.ReadSnakePosition(processHandle);
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

        private void TeleportGuardsToSnake_Click(object sender, EventArgs e)
        {
            MemoryManager.Instance.MoveAllGuardsToSnake();
        }

        private void SnakeJump_Click(object sender, EventArgs e)
        {
            MemoryManager.Instance.RaiseSnakeYBy4000();
        }

        private void SnakesXYZaob_Click(object sender, EventArgs e)
        {
            MemoryManager.Instance.FindAndStoreSnakesPositionAOB();
            if (MemoryManager.Instance.FoundSnakePositionAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    float[] snakePosition = MemoryManager.Instance.ReadSnakePosition(processHandle);
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
        #region Camo  
        private void NopCamo_Click(object sender, EventArgs e)
        {
            // Ensure the MemoryManager has the necessary method to perform the NOP operation
            MemoryManager.Instance.EnableNOPCamoIndex();
        }


        private void RestoreCamo_Click(object sender, EventArgs e)
        {
            MemoryManager.RestoreCamoIndex();

        }

        private void Read4ByteBeforeCamoAOB_Click(object sender, EventArgs e)
        {
            string result = MemoryManager.Instance.Read4BytesBeforeCamoAOB();
            MessageBox.Show(result);
        }

        private void LogAOBs_Click(object sender, EventArgs e)
        {
            MemoryManager.Instance.LogAOBAddresses();
        }

        private void CamoIndexSlider_Scroll(object sender, EventArgs e)
        {
            // Get the new value from the slider
            int newValue = CamoIndexSlider.Value;

            // Call the method to adjust the camo index
            MemoryManager.Instance.AdjustCamoIndex(newValue);
        }


        private void btnReadCamoIndex_Click(object sender, EventArgs e)
        {
            string result = MemoryManager.Instance.GetCamoIndexReadout();
            MessageBox.Show(result, "Read Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Name is misleading since I changed my mind and 10 will increase/decrease the camo index by 1%
        private void Minus50Camo_Click(object sender, EventArgs e)
        {
            int newValue = CamoIndexSlider.Value - 10;
            if (newValue >= CamoIndexSlider.Minimum)
            {
                CamoIndexSlider.Value = newValue;
                MemoryManager.Instance.AdjustCamoIndex(newValue);
            }
        }

        private void Plus50Camo_Click(object sender, EventArgs e)
        {
            int newValue = CamoIndexSlider.Value + 10;
            if (newValue <= CamoIndexSlider.Maximum)
            {
                CamoIndexSlider.Value = newValue;
                MemoryManager.Instance.AdjustCamoIndex(newValue);
            }
        }
        #endregion

        private async void LadderSkip_Click_1(object sender, EventArgs e)
        {
            // Define multiple sets of coordinates for successive teleportation
            float[][] coordinates = new float[][]
            {
                
                new float[] { -1273f, 901f, -30441f }, // First set of coordinates
                // Keep increasing the Y to test theory of how the ladder gcx works
                new float[] { -1251f, 2000f, -30441f },
                new float[] { -1251f, 154840f, -31441f },
                new float[] { -1251f, 4000f, -30441f },
                new float[] { -1251f, 154840f, -30430f },
               




                // Final Float
                
            };

            foreach (var coordSet in coordinates)
            {
                // Execute the teleport to new position using the current set of coordinates
                MemoryManager.Instance.TeleportSnake(new float[][] { coordSet }); // Adjust the method if necessary

                // Add a half-second delay between teleports
                await Task.Delay(1000); // 500 milliseconds = 0.5 seconds
            }
        }


    }
}