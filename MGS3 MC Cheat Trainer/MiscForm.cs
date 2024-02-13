using System.Buffers;
using System.Diagnostics;
using System.Text;
using static MGS3_MC_Cheat_Trainer.Constants;

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

        private void WeaponFormSwap_Click(object sender, EventArgs e) // Weapon Form Swap
        {
            WeaponForm f1 = new WeaponForm();
            f1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) // Item Form Swap
        {
            ItemForm f2 = new ItemForm();
            f2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) // Camo Form Swap
        {
            CamoForm f3 = new CamoForm();
            f3.Show();
            this.Hide();
        }

        // Form Closing
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void NormalHUD_Click(object sender, EventArgs e)
        {
            ModelManager.ChangeHud((int)Constants.HudOptions.Normal);
        }

        private void ShrinkHUD_Click(object sender, EventArgs e)
        {
            ModelManager.ChangeHud((int)Constants.HudOptions.Shrunk);
        }

        private void NoHUD_Click(object sender, EventArgs e)
        {
            ModelManager.ChangeHud((int)Constants.HudOptions.None);
        }

        private void NormalCam_Click(object sender, EventArgs e)
        {
            ModelManager.ChangeCamera((int)Constants.CameraOptions.Normal);
        }

        private void UpsideDownCam_Click(object sender, EventArgs e)
        {
            ModelManager.ChangeCamera((int)Constants.CameraOptions.UpsideDown);
        }

        private void UpdateModelValueTextBox()
        {
            // This method updates the text box with the current value of the slider
            ModelCurrentValue.Text = ModelSlider.Value.ToString();
        }

        // Broken from v1.4.1 update
        private void ModelSlider_Scroll(object sender, EventArgs e)
        {
            // Update the model with the slider's current value
            //byte sliderValue = (byte)ModelSlider.Value;
            //ModelManager.ModifyModel(Constants.MGS3DistortionEffects.Normal, sliderValue);
            // Update the text box to reflect the current slider value
            //UpdateModelValueTextBox();
        }

        private void ResetModelsToNormal_Click(object sender, EventArgs e)
        {
            ModelManager.ModifyModel(Constants.MGS3DistortionEffects.Normal, 80);

            // Just so the user knows where the defult location of the slider should be since 0 isn't the default
            ModelSlider.Value = 80;
            UpdateModelValueTextBox();
        }

        private void ChangeModelNumber_Click(object sender, EventArgs e)
        {
            // Retrieve the value from the textbox
            string textValue = ModelChangeValue.Text;

            // Attempt to parse the value to an integer
            if (int.TryParse(textValue, out int modelValue))
            {
                // Ensure the value is within the acceptable range
                if (modelValue >= 0 && modelValue <= 255)
                {
                    // Call the ModifyModel function with the parsed value
                    ModelManager.ModifyModel(Constants.MGS3DistortionEffects.Normal, (byte)modelValue);

                    // Optionally, update the slider to reflect this value
                    ModelSlider.Value = modelValue;
                    UpdateModelValueTextBox();
                }
                else
                {
                    // Value is out of range, notify the user
                    MessageBox.Show("Please enter a value between 0 and 255.");
                }
            }
            else
            {
                // Value couldn't be parsed, notify the user
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void Minus1ModelValue_Click(object sender, EventArgs e)
        {
            // Decrease the slider value by 1 if it's greater than the minimum
            if (ModelSlider.Value > ModelSlider.Minimum)
            {
                ModelSlider.Value--;
                ModelManager.ModifyModel(Constants.MGS3DistortionEffects.Normal, (byte)ModelSlider.Value);
                // Update the text box to reflect the current slider value
                UpdateModelValueTextBox();
            }
        }

        private void Plus1ModelValue_Click(object sender, EventArgs e)
        {
            // Increase the slider value by 1 if it's less than the maximum
            if (ModelSlider.Value < ModelSlider.Maximum)
            {
                ModelSlider.Value++;
                ModelManager.ModifyModel(Constants.MGS3DistortionEffects.Normal, (byte)ModelSlider.Value);
                // Update the text box to reflect the current slider value
                UpdateModelValueTextBox();
            }
        }

        private void HealthFormSwap_Click(object sender, EventArgs e)
        {
            StatsAndAlertForm form5 = new();
            form5.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MemoryManager.Instance.FindAndStoreTheFearAOB())
            {
                MessageBox.Show($"TheFear AOB found at: 0x{MemoryManager.Instance.FoundTheFearAddress.ToInt64():X}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("TheFear AOB not found.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string result = MemoryManager.Instance.FindLocationStringDirectlyInRange();
                MessageBox.Show(result, "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        // This was more of a debug button to see the values of the alert timers so I could determine the offsets of the timers
        private void button6_Click(object sender, EventArgs e)
        {
            MemoryManager memoryManager = new MemoryManager();
            var aobPattern = Constants.AOBs["AlertMemoryRegion"].Pattern;
            var mask = Constants.AOBs["AlertMemoryRegion"].Mask;

            IntPtr alertMemoryRegion = memoryManager.FindAlertMemoryRegion(aobPattern, mask);
            if (alertMemoryRegion == IntPtr.Zero)
            {
                MessageBox.Show("Failed to find alert memory region.");
                return;
            }

            short alertTimerValue = AlertManager.ReadAlertTimerValue(alertMemoryRegion);
            short evasionTimerValue = AlertManager.ReadEvasionTimerValue(alertMemoryRegion);
            short cautionTimerValue = AlertManager.ReadCautionTimerValue(alertMemoryRegion);

            MessageBox.Show($"Alert Timer: {alertTimerValue}, \nEvasion Timer: {evasionTimerValue}, \nCaution Timer: {cautionTimerValue}");
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

    }
}