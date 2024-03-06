using System.Diagnostics;
using System.Text;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class MiscForm : Form
    {
        IntPtr processHandle; // Ensure this is correctly initialized
        private System.Windows.Forms.Timer alertCheckTimer;
        private MemoryManager memoryManager;
        public MiscForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);

            // Range for the model manipulation
            ModelSlider.Minimum = 0;
            ModelSlider.Maximum = 255;
            ModelSlider.Value = 40; // Default byte value and where the slider should start at
            memoryManager = new MemoryManager();
            ChangeModelNumber.Click += new EventHandler(ChangeModelNumber_Click);
            ModelSlider.Scroll += new EventHandler(ModelSlider_Scroll);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #region Form Swaps
        private void WeaponFormSwap_Click(object sender, EventArgs e) // Weapon Form Swap
        {
            WeaponForm f1 = new WeaponForm();
            f1.Show();
            this.Hide();
            LoggingManager.Instance.Log("Swapping to Weapon Form from the Misc Form");
        }

        private void button2_Click(object sender, EventArgs e) // Item Form Swap
        {
            ItemForm f2 = new ItemForm();
            f2.Show();
            this.Hide();
            LoggingManager.Instance.Log("Swapping to Item Form from the Misc Form");
        }


        private void button1_Click(object sender, EventArgs e) // Camo Form Swap
        {
            CamoForm f3 = new CamoForm();
            f3.Show();
            this.Hide();
            LoggingManager.Instance.Log("Swapping to Camo Form from the Misc Form");
        }

        private void HealthFormSwap_Click(object sender, EventArgs e)
        {
            StatsAndAlertForm form5 = new();
            form5.Show();
            this.Hide();
            LoggingManager.Instance.Log("Swapping to Stats and Alert Form from the Misc Form");
        }

        private void SwapToBossForm_Click(object sender, EventArgs e)
        {
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

        private void LadderSkip_Click(object sender, EventArgs e)
        {
            // Coordinates to teleport Snake to
            /* Weird bug gcx related that will teleport you through the floor if you use the coordinates below
            float x = -2950f;
            float y = 154800f;
            float z = -45000f;
            */

            float x = -1349f;
            float y = 151995f;
            float z = -29446f;

            MemoryManager.Instance.TeleportSnake(x, y, z);
        }

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

        private void button4_Click(object sender, EventArgs e)
        {
            MemoryManager.Instance.FindAndStoreSnakesPositionAOB();
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
    }
}