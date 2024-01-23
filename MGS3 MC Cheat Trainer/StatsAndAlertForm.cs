using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class StatsAndAlertForm : Form
    {
        public StatsAndAlertForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(StatsAndAlertForm_FormClosing);

            // Initialize and set timer intervals
            AlertTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // 1 second interval for maintaining alert status
            };
            AlertTimer.Tick += new EventHandler(AlertCheckTimer_Tick);

            CautionTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // 1 second interval for maintaining caution status
            };
            CautionTimer.Tick += new EventHandler(CautionCheckTimer_Tick);

            continuousMonitoringTimer = new System.Windows.Forms.Timer
            {
                Interval = 50 // Interval in milliseconds, adjust as needed
            };
            continuousMonitoringTimer.Tick += new EventHandler(ContinuousMonitoringTimer_Tick);

            continuousMonitoringTimer.Start(); // Start the timer

            InfiniteEvasionTimer = new System.Windows.Forms.Timer
            {
                Interval = 38000 // 1 second interval for maintaining evasion status
            };

            InfiniteEvasionTimer.Tick += new EventHandler(InfiniteEvasionTimer_Tick);

            InitializeProgressBars();
        }

        private void InitializeProgressBars()
        {
            // Progress bar settings

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

        // Form Closing Event
        private void StatsAndAlertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

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

        private void button3_Click(object sender, EventArgs e) // Alert Mode Trigger
        {
            AlertManager.TriggerAlert(AlertModes.Alert);
        }
        private async void EvasionButton_Click(object sender, EventArgs e) // Evasion Mode Trigger
        {
            // Step 1: Trigger caution mode
            AlertManager.TriggerAlert(AlertModes.Caution);

            await Task.Delay(3000);

            // Step 2: Modify the evasion bits
            AlertManager.TriggerAlert(AlertModes.Evasion);

            await Task.Delay(750);

            // Step 3: Trigger alert mode
            AlertManager.TriggerAlert(AlertModes.Alert);
        }

        private void button9_Click(object sender, EventArgs e) // Caution Mode Trigger
        {
            AlertManager.TriggerAlert(AlertModes.Caution);
        }

        private void InfiniteAlert_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null && checkBox.Checked)
            {
                // Trigger the alert and make a check every second to where the alert timer is at
                AlertManager.InfiniteStatus(Constants.AlertModes.Alert);
                AlertTimer.Start();
            }
            else // Unchecking the box will stop the timer and check
            {
                AlertTimer.Stop();
            }
        }

        private void AlertCheckTimer_Tick(object sender, EventArgs e)
        {
            // This will re-trigger the alert mode and keep the alert status active.
            AlertManager.InfiniteStatus(Constants.AlertModes.Alert);

            // Update the progress bar with the current alert timer value
            int alertTimerValue = AlertManager.GetAlertTimerValue();
            if (alertTimerValue >= 0)
            {
                UpdateProgressBar(AlertProgressBar, alertTimerValue);
            }
            else
            {
                // Handle the error case
                Console.WriteLine("Error reading alert timer value.");
            }
        }

        private void CautionCheckTimer_Tick(object sender, EventArgs e)
        {
            AlertManager.InfiniteStatus(Constants.AlertModes.Caution);
            // Update the progress bar with the current caution timer value from InfiniteStatus
            int cautionTimerValue = AlertManager.GetCautionTimerValue();
            if (cautionTimerValue >= 0)
            {
                UpdateProgressBar(CautionProgressBar, cautionTimerValue);
            }
            else
            {
                // Handle the error case
                Console.WriteLine("Error reading caution timer value.");
            }
        }

        private async void InfiniteEvasion_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                if (checkBox.Checked)
                {
                    // Immediately trigger the evasion logic when checkbox is checked
                    await TriggerEvasionLogic();

                    // Set timer for subsequent evasion triggers
                    InfiniteEvasionTimer.Interval = 38000; // 38 seconds
                    InfiniteEvasionTimer.Start();
                }
                else
                {
                    InfiniteEvasionTimer.Stop();
                }
            }
        }

        private async void InfiniteEvasionTimer_Tick(object sender, EventArgs e)
        {
            // Evasion logic triggered every 38 seconds after the first execution
            await TriggerEvasionLogic();
        }

        private async Task TriggerEvasionLogic()
        {
            int CautionTimerValue = AlertManager.GetCautionTimerValue();

            if (CautionTimerValue != 0)
            {
                AlertManager.RemoveEvasionAndCaution();
                await Task.Delay(2000); // Clear out caution
            }

            AlertManager.ChangeAlertMode((int)Constants.AlertModes.Caution);
            await Task.Delay(3000);

            AlertManager.SetEvasionBits();
            await Task.Delay(1750);

            AlertManager.ChangeAlertMode((int)Constants.AlertModes.Alert);
        }

        private void InfiniteCaution_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if ((checkBox != null && checkBox.Checked))
            {
                AlertManager.InfiniteStatus(Constants.AlertModes.Caution);
                CautionTimer.Start();
            }
            else
            {
                CautionTimer.Stop();
            }
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

        // This function will update the progress bars every 50 ms
        private void ContinuousMonitoringTimer_Tick(object sender, EventArgs e)
        {
            // Check the current alert status
            var currentAlertStatus = AlertManager.GetAlertStatus();

            // Handle the Alert mode and update the progress bar accordingly
            if (currentAlertStatus == Constants.AlertModes.Alert)
            {
                int alertTimerValue = AlertManager.GetAlertTimerValue();
                if (alertTimerValue >= 0)
                {
                    UpdateProgressBar(AlertProgressBar, alertTimerValue);
                }
            }
            else
            {
                // If not in Alert mode, reset the Alert progress bar to zero
                UpdateProgressBar(AlertProgressBar, 0);
            }

            // Add similar logic for Caution and Evasion if needed
            // Example for Evasion mode
            if (currentAlertStatus == Constants.AlertModes.Evasion)
            {
                int evasionTimerValue = AlertManager.GetEvasionTimerValue();
                if (evasionTimerValue >= 0)
                {
                    UpdateProgressBar(EvasionProgressBar, evasionTimerValue);
                }
            }
            else
            {
                // If not in Evasion mode, reset the Evasion progress bar to zero
                UpdateProgressBar(EvasionProgressBar, 0);
            }

            if (currentAlertStatus == Constants.AlertModes.Caution)
            {
                int cautionTimerValue = AlertManager.GetCautionTimerValue();
                if (cautionTimerValue >= 0)
                {
                    UpdateProgressBar(CautionProgressBar, cautionTimerValue);
                }
            }
            else
            {
                // If not in Caution mode, reset the Caution progress bar to zero
                UpdateProgressBar(CautionProgressBar, 0);
            }
        }

        private void ClearCautionAndEvasion_Click(object sender, EventArgs e)
        {
            AlertManager.RemoveEvasionAndCaution();
        }

        /* Here but don't think I'll use it since it sort of just freezes guards in placeat a heightened 
           alert status and the don't keep looking for you sadly atm I might attach to a 
           freeze timer checkbox maybe in the future */
        private void FreezeEvasionTimer_Tick(object sender, EventArgs e) 
        {
            // Should reuse this for infinite alert however since it works better than what I got for infinite alert atm
            FreezeEvasionTimer.Interval = 1000;
            FreezeEvasionTimer.Tick += (sender, args) => AlertManager.FreezeAlertTimer();
            FreezeEvasionTimer.Start();
        }

        private void WeaponFormSwap_Click(object sender, EventArgs e)
        {
            WeaponForm form1 = new();
            form1.Show();
            this.Hide();
        }

        private void HealthFormSwap_Click(object sender, EventArgs e) // Item form misname
        {
            ItemForm f2 = new ItemForm();
            f2.Show();
            this.Hide();
        }

        private void CamoFormSwap_Click(object sender, EventArgs e)
        {
            ItemForm f3 = new ItemForm();
            f3.Show();
            this.Hide();
        }

        private void MiscFormSwap_Click(object sender, EventArgs e)
        {
            MiscForm f4 = new MiscForm();
            f4.Show();
            this.Hide();
        }

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

    }
}