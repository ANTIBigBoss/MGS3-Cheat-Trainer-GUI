using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    
    public partial class StatsAndAlertForm : Form
    {
        private bool infiniteAlertCheckboxState;
        private bool infiniteEvasionCheckboxState;
        private bool infiniteCautionCheckboxState;

        #region Form Load and Close

        public StatsAndAlertForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(StatsAndAlertForm_FormClosing);

            // Optionally, re-apply effects when form is shown or focused
            this.FormClosing += new FormClosingEventHandler(StatsAndAlertForm_FormClosing);

            InitializeProgressBars();

            // Set the initial checkbox state based on the infiniteAlertEnabled variable
            InfiniteAlert.Checked = AlertManager.IsInfiniteAlertEnabled;
            InfiniteEvasion.Checked = AlertManager.IsInfiniteEvasionEnabled;
            InfiniteCaution.Checked = AlertManager.IsInfiniteCautionEnabled;

            // Update the class-level variable with the checkbox state
            infiniteAlertCheckboxState = InfiniteAlert.Checked;
            infiniteAlertCheckboxState = InfiniteEvasion.Checked;
            infiniteAlertCheckboxState = InfiniteCaution.Checked;

            System.Windows.Forms.Timer continuousMonitoringTimer = new System.Windows.Forms.Timer();
            continuousMonitoringTimer.Interval = 1000; // Update every second
            continuousMonitoringTimer.Tick += new EventHandler(ContinuousMonitoringTimer_Tick);
            continuousMonitoringTimer.Start();
        }

        private void InitializeProgressBars()
        {
            AlertProgressBar.Minimum = 0;
            AlertProgressBar.Maximum = 18000;
            AlertProgressBar.ProgressBarColour = Color.Red;

            EvasionProgressBar.Minimum = 0;
            EvasionProgressBar.Maximum = 18000;
            EvasionProgressBar.ProgressBarColour = Color.Orange;

            CautionProgressBar.Minimum = 0;
            CautionProgressBar.Maximum = 18000;
            CautionProgressBar.ProgressBarColour = Color.Yellow;
        }
        private void StatsAndAlertForm_Load(object sender, EventArgs e)
        {
            this.Location = MemoryManager.GetLastFormLocation();
        }

        private void StatsAndAlertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Form Swaps
        private void WeaponFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("Navigating to Weapon Form from the Item Form");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "WeaponForm");
            WeaponForm form1 = new();
            form1.Show();
            this.Hide();
        }

        // This is the item form I made some mistakes on click event names on this form
        private void HealthFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("Navigating to Item Form from the Stats Form");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "HealthForm");
            ItemForm f2 = new ItemForm();
            f2.Show();
            this.Hide();
        }

        // Says ItemForm but it's actually the Camo Form
        private void CamoFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("Navigating to Camo Form from the Stats Form");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "CamoForm");
            CamoForm f3 = new CamoForm();
            f3.Show();
            this.Hide();
        }

        private void MiscFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("Navigating to Misc Form from the Stats Form");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "MiscForm");
            MiscForm f4 = new MiscForm();
            f4.Show();
            this.Hide();
        }

        private void SwapToBossForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("Navigating to Boss Form from the Stats Form");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "BossForm");
            BossForm form6 = new();
            form6.Show();
            this.Hide();
        }
        #endregion

        #region Snake's Health and Stamina
        // Health and Stamina along with pointer logic
        private void Plus100HpValue_Click(object sender, EventArgs e)
        {
            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.CurrentHealth, 100);
        }

        private void Minus100HpValue_Click(object sender, EventArgs e)
        {
            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.CurrentHealth, -100);
        }

        private void CurrentHpTo1_Click(object sender, EventArgs e)
        {
            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.CurrentHealth, 1, true);
        }

        private void MaxHpTo1_Click(object sender, EventArgs e)
        {

            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.MaxHealth, 1, true);
        }

        private void ZeroHP_Click(object sender, EventArgs e)
        {
            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.CurrentHealth, 0, true);
        }

        private void SetStaminaToZero_Click(object sender, EventArgs e)
        {
            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.Stamina, 0, true);
        }

        // I thought 10000 was a bar at first but it's actually 7500 per bar 
        // so this function name is misleading LOL will fix in V2
        private void Plus10000StaminaValue_Click(object sender, EventArgs e)
        {
            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.Stamina, 7500);
        }

        private void Minus10000StaminaValue_Click(object sender, EventArgs e)
        {
            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.Stamina, -7500);
        }

        private void FullStamina30000Value_Click(object sender, EventArgs e)
        {
            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.Stamina, 30000, true);
        }

        private void Plus100MaxHpValue_Click(object sender, EventArgs e)
        {
            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.MaxHealth, 100);
        }

        private void Minus100MaxHpValue_Click(object sender, EventArgs e)
        {
            MainPointerManager.ModifyHealthOrStamina(Constants.HealthType.MaxHealth, -100);
        }
        #endregion

        #region Alert Statuses
        private void button3_Click(object sender, EventArgs e) // Alert Mode Trigger
        {
            AlertManager.TriggerAlert(AlertModes.Alert);
        }

        // This was my workaround fix to stop a double message box from showing when the user tries to check more than one infinite alert mode
        private bool suppressAlertMessages = false;

        private void InfiniteAlert_CheckedChanged(object sender, EventArgs e)
        {
            // If suppressAlertMessages is true, return without further processing
            if (suppressAlertMessages)
            {
                return;
            }

            if (InfiniteEvasion.Checked)
            {
                suppressAlertMessages = true; // Suppress other alert messages temporarily
                InfiniteAlert.Checked = false;
                MessageBox.Show("Only one Infinite Status allowed at once. \nDeselect Evasion Mode to use this.");
                suppressAlertMessages = false; // Allow messages to be shown again
            }
            else if (InfiniteCaution.Checked)
            {
                suppressAlertMessages = true; // Suppress other alert messages temporarily
                InfiniteAlert.Checked = false;
                MessageBox.Show("Only one Infinite Status allowed at once. \nDeselect Caution Mode to use this.");
                suppressAlertMessages = false; // Allow messages to be shown again
            }
            else
            {
                // Update the class-level variable with the checkbox state
                infiniteAlertCheckboxState = InfiniteAlert.Checked;

                // Check if the checkbox is checked
                if (InfiniteAlert.Checked)
                {
                    AlertManager.TriggerAlert(AlertModes.Alert);
                    // Start the alert timer when the checkbox is checked
                    AlertManager.ToggleInfiniteAlert(true);
                }
                else
                {
                    // Stop the alert timer when the checkbox is unchecked
                    AlertManager.ToggleInfiniteAlert(false);
                }
            }
        }

        private void InfiniteCaution_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressAlertMessages)
            {
                return;
            }

            if (InfiniteEvasion.Checked)
            {
                suppressAlertMessages = true; // Suppress other alert messages temporarily
                InfiniteCaution.Checked = false;
                MessageBox.Show("Only one Infinite Status allowed at once. \nDeselect Evasion Mode to use this.");
                suppressAlertMessages = false; // Allow messages to be shown again
            }
            else if (InfiniteAlert.Checked)
            {
                suppressAlertMessages = true; // Suppress other alert messages temporarily
                InfiniteCaution.Checked = false;
                MessageBox.Show("Only one Infinite Status allowed at once. \nDeselect Alert Mode to use this.");
                suppressAlertMessages = false; // Allow messages to be shown again
            }
            else
            {
                AlertManager.ToggleInfiniteCaution(InfiniteCaution.Checked);
            }
        }

        private void EvasionButton_Click(object sender, EventArgs e)
        {
            // First, find the alert memory region to read the timer values
            IntPtr alertMemoryRegion = MemoryManager.Instance.FindAob("AlertMemoryRegion");
            if (alertMemoryRegion == IntPtr.Zero)
            {
                return;
            }

            // Read current alert and evasion timer values
            short alertTimerValue = AlertManager.ReadAlertTimerValue(alertMemoryRegion);
            short evasionTimerValue = AlertManager.ReadEvasionTimerValue(alertMemoryRegion);

            // Check if either alert or evasion timer indicates an ongoing state
            if (alertTimerValue > 0 || evasionTimerValue > 0)
            {
                MessageBox.Show("Evasion cannot be triggered during active alert or evasion state.", "Action Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Safe to trigger the evasion sequence
                AlertManager.StartEvasionSequence();
            }
        }

        private void InfiniteEvasion_CheckedChanged(object sender, EventArgs e)
        {
            // If suppressAlertMessages is true, return without further processing
            if (suppressAlertMessages)
            {
                return;
            }

            if (InfiniteAlert.Checked)
            {
                suppressAlertMessages = true; // Suppress other alert messages temporarily
                InfiniteEvasion.Checked = false;
                MessageBox.Show("Only one Infinite Status allowed at once. \nDeselect Alert Mode to use this.");
                suppressAlertMessages = false; // Allow messages to be shown again
            }
            else if (InfiniteCaution.Checked)
            {
                suppressAlertMessages = true; // Suppress other alert messages temporarily
                InfiniteEvasion.Checked = false;
                MessageBox.Show("Only one Infinite Status allowed at once. \nDeselect Caution Mode to use this.");
                suppressAlertMessages = false; // Allow messages to be shown again
            }
            else
            {
                AlertManager.ToggleInfiniteEvasion(InfiniteEvasion.Checked);
            }
        }

        private void button9_Click(object sender, EventArgs e) // Caution Mode Trigger
        {
            AlertManager.TriggerAlert(AlertModes.Caution);
        }

        /* Have the progress bar parse the value of the MGS3AlertTimers for Alert, Evasion and Caution 
         then the checkbox will freeze the progress bar value until we uncheck it the the memory value
        and the progress bar value will start going down again */
        private void UpdateProgressBar(ColouredProgressBar progressBar, int timerValue)
        {
            // Ensure the update is done on the UI thread
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action(() => UpdateProgressBar(progressBar, timerValue)));
            }
            else
            {
                if (timerValue >= progressBar.Minimum && timerValue <= progressBar.Maximum)
                {
                    progressBar.Value = timerValue;
                }
                else
                {
                    // Log or handle the error case
                    Console.WriteLine($"Timer value out of range: {timerValue}");
                }
            }
        }

        private void ContinuousMonitoringTimer_Tick(object sender, EventArgs e)
        {
            IntPtr alertMemoryRegion = MemoryManager.Instance.FindAob("AlertMemoryRegion");
            if (alertMemoryRegion == IntPtr.Zero)
            {
                Console.WriteLine("Error: Alert memory region not found.");
                return;
            }

            // Read the timer values
            short alertTimerValue = AlertManager.ReadAlertTimerValue(alertMemoryRegion);
            short evasionTimerValue = AlertManager.ReadEvasionTimerValue(alertMemoryRegion);
            short cautionTimerValue = AlertManager.ReadCautionTimerValue(alertMemoryRegion);

            // Safely update the progress bars on the UI thread otherwise enjoy lag city
            this.Invoke((MethodInvoker)delegate
            {
                UpdateProgressBar(AlertProgressBar, alertTimerValue);
                UpdateProgressBar(EvasionProgressBar, evasionTimerValue);
                UpdateProgressBar(CautionProgressBar, cautionTimerValue);
            });
        }

        private void ClearCautionAndEvasion_Click(object sender, EventArgs e)
        {
            AlertManager.RemoveEvasionAndCaution();
        }

        #endregion

        #region Snake's Serious Injuries
        private void BurnInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.SevereBurns);
        }

        private void CutInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.DeepCut);
        }

        private void GunshotRifleInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.GunshotWoundRifle);
        }

        private void GunshotShotgunInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.GunshotWoundShotgun);
        }

        private void BoneFractureInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.BoneFracture);
        }

        private void BulletBeeInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.BulletBee);
        }

        private void LeechesInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.Leeches);
        }

        private void ArrowInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.ArrowWound);
        }

        private void TranqInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.TranqDart);
        }

        private void VenomPoisoningInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.Poisoned);
        }

        private void FoodPoisoningInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.FoodPoisoning);
        }

        private void CommonColdInjury_Click(object sender, EventArgs e)
        {
            MainPointerManager.ApplyInjury(Constants.InjuryType.Cold);
        }

        private void RemoveInjuries_Click(object sender, EventArgs e)
        {
            MainPointerManager.RemoveAllInjuries();
        }
        #endregion

    }
}