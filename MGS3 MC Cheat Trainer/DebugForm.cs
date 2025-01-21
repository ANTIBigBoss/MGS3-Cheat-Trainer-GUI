namespace MGS3_MC_Cheat_Trainer
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(DebugForm_FormClosing);
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {
            this.Location = MemoryManager.GetLastFormLocation();
        }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "DebugForm");
            Application.Exit();
        }

        private void SwapToWeaponsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Weapon form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "WeaponForm");
            WeaponForm form1 = new();
            form1.Show();
            this.Hide();
        }

        private void SwapToItemsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Item form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "ItemForm");
            ItemForm form2 = new();
            form2.Show();
            this.Hide();
        }

        private void SwapToCamoForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Camo form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "CamoForm");
            CamoForm form3 = new();
            form3.Show();
            this.Hide();
        }

        private void SwapToMiscForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Misc form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "MiscForm");
            MiscForm form4 = new();
            form4.Show();
            this.Hide();
        }

        private void SwapToHealthAndAlertsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Stats and Alert form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "StatsAndAlertForm");
            StatsAndAlertForm form5 = new();
            form5.Show();
            this.Hide();
        }

        private void SwapToBossForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Boss form from the Game Stats form.\n");
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

        private void CheatEngineDebugButton_Click(object sender, EventArgs e)
        {
            string processName = txtProcessName.Text.Trim();
            txtProcessName.Text = "METAL GEAR SOLID3";
            string currentAddressInput = txtCurrentAddress.Text.Trim();

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

            if (!long.TryParse(currentAddressInput, System.Globalization.NumberStyles.HexNumber, null, out long currentAddress))
            {
                MessageBox.Show("Invalid address format. Please enter a valid hexadecimal number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                IntPtr baseAddress = HelperMethods.Instance.GetBaseAddress(processName);
                long offset = HelperMethods.Instance.CalculateOffset(baseAddress, currentAddress);
                IntPtr recomputedAddress = HelperMethods.Instance.RecomputeAbsoluteAddress(baseAddress, offset);
                string cheatEngineString = HelperMethods.Instance.GenerateCheatEngineString(processName, offset);

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

        private void parseGeomFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\MGS3\assets\geom\us\";
            openFileDialog.Filter = "Geom Files (*.geom)|*.geom";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                GeomParser.ParseGeomHeader(filePath);
                MessageBox.Show("Parsing complete!");
            }
        }

        private void editGeomFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\MGS3\assets\geom\us\";
            openFileDialog.Filter = "Geom Files (*.geom)|*.geom";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string geomPath = openFileDialog.FileName;
                GeomEditor.EditGeomFile(geomPath);
            }
        }
    }
}