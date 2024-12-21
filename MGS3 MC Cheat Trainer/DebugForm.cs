namespace MGS3_MC_Cheat_Trainer
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        private void CheatEngineDebugButton_Click(object sender, EventArgs e)
        {
            string processName = txtProcessName.Text.Trim();
            txtProcessName.Text = "METAL GEAR SOLID3";
            string currentAddressInput = txtCurrentAddress.Text.Trim();

            // Input Validation
            if (string.IsNullOrEmpty(processName))
            {
                MessageBox.Show("Please enter a process name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(currentAddressInput))
            {
                MessageBox.Show("Please enter the current address.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Parse the current address as hexadecimal
            if (!long.TryParse(currentAddressInput, System.Globalization.NumberStyles.HexNumber, null, out long currentAddress))
            {
                MessageBox.Show("Invalid address format. Please enter a valid hexadecimal number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Retrieve the base address
                IntPtr baseAddress = HelperMethods.Instance.GetBaseAddress(processName);

                // Calculate the offset
                long offset = HelperMethods.Instance.CalculateOffset(baseAddress, currentAddress);

                // Recompute the absolute address
                IntPtr recomputedAddress = HelperMethods.Instance.RecomputeAbsoluteAddress(baseAddress, offset);

                // Generate the Cheat Engine string
                string cheatEngineString = HelperMethods.Instance.GenerateCheatEngineString(processName, offset);

                // Display the results
                txtOffset.Text = $"0x{offset:X}";
                txtRecomputedAddress.Text = $"0x{recomputedAddress.ToInt64():X}";
                txtCheatEngineString.Text = cheatEngineString;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Process Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogAllGuardPositions_Click(object sender, EventArgs e)
        {
            XyzManager.Instance.LogAllGuardsPosition();
        }

        private void RandomizeGuards_Click(object sender, EventArgs e)
        {
            // Set the randomization type to guards
            RandomizerManager.Instance.RandomizationType = "guards";

            // Trigger the randomization process
            RandomizerManager.Instance.SearchForRandomizerArea();

            LoggingManager.Instance.Log("Randomization for guards triggered.");
        }

        private void LogAreaAddress_Click(object sender, EventArgs e)
        {
            MessageBox.Show(StringManager.Instance.GetCurrentMemoryAddress());
        }

        private void GenerateLogButton_Click(object sender, EventArgs e)
        {
            LoggingManager.LogAllWeaponsAndItemsAddresses();
            LoggingManager.Instance.LogAOBAddresses();
            LoggingManager.Instance.LogAllMemoryAddressesandValues();
            CustomMessageBoxManager.CustomMessageBox("Information written to log file in:\n C:\\Users\\YourUserNameHere\\Documents\\MGS3 CT Log\\MGS3_MC_Cheat_Trainer_Log.txt", "Log Generated");
        }
    }

}