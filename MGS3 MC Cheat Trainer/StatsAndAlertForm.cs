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
            this.Load += StatsAndAlertForm_Load;
            this.FormClosing += new FormClosingEventHandler(StatsAndAlertForm_FormClosing);

            // Optionally, re-apply effects when form is shown or focused
            this.FormClosing += new FormClosingEventHandler(StatsAndAlertForm_FormClosing);

            InitializeProgressBars();

            // Set the initial checkbox state based on the infiniteAlertEnabled variable
            InfiniteAlert.Checked = TimerManager.IsInfiniteAlertEnabled;
            InfiniteEvasion.Checked = TimerManager.IsInfiniteEvasionEnabled;
            InfiniteCaution.Checked = TimerManager.IsInfiniteCautionEnabled;

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

            DamageManager.Instance.ReadAllTypeDamageValues();

            SetLethalRadio();
            SetSleepRadio();
            SetStunRadio();

            // Battery Drain Checkbox
            CheckBatteryDrain();


        }

        private void SetLethalRadio()
        {
            bool isInvincible = DamageManager.Instance.AreLethalValuesInvincible();
            bool isVeryStrong = DamageManager.Instance.AreLethalValuesVeryStrong();
            bool isDefault = DamageManager.Instance.AreLethalValuesDefault();
            bool isVeryWeak = DamageManager.Instance.AreLethalValuesVeryWeak();
            bool isOneShotKill = DamageManager.Instance.AreLethalValuesOneshot();

            if (isInvincible)
            {
                LoggingManager.Instance.Log("LETHAL DAMAGE settings are set to invincible.");
                NeckSnapLethalRadio.Checked = true;
            }

            else if (isVeryStrong)
            {
                LoggingManager.Instance.Log("LETHAL DAMAGE settings are set to very strong.");
                VeryStrongLethalRadio.Checked = true;
            }

            else if (isDefault)
            {
                LoggingManager.Instance.Log("LETHAL DAMAGE settings are set to default.");
                NormalLethalRadio.Checked = true;
            }

            else if (isVeryWeak)
            {
                LoggingManager.Instance.Log("LETHAL DAMAGE settings are set to very weak.");
                VeryWeakLethalRadio.Checked = true;
            }

            else if (isOneShotKill)
            {
                LoggingManager.Instance.Log("LETHAL DAMAGE settings are set to one-shot kill.");
                OneShotKillLethalRadio.Checked = true;
            }

            else
            {
                LoggingManager.Instance.Log("LETHAL DAMAGE settings are custom or unrecognized.");
            }
        }

        private void SetSleepRadio()
        {
            bool isInvincible = DamageManager.Instance.AreSleepValuesInvincible();
            bool isVeryStrong = DamageManager.Instance.AreSleepValuesVeryStrong();
            bool isDefault = DamageManager.Instance.AreSleepValuesNormal();
            bool isVeryWeak = DamageManager.Instance.AreSleepValuesVeryWeak();
            bool isOneShotSleep = DamageManager.Instance.AreSleepValuesOneShot();

            if (isInvincible)
            {
                LoggingManager.Instance.Log("SLEEP DAMAGE settings are set to INVINCIBLE.");
                InvincibleZzzRadio.Checked = true;
            }

            else if (isVeryStrong)
            {
                LoggingManager.Instance.Log("SLEEP DAMAGE settings are set to VERY STRONG");
                VeryStrongZzzRadio.Checked = true;
            }

            else if (isDefault)
            {
                LoggingManager.Instance.Log("SLEEP DAMAGE settings are set to DEFAULT.");
                NormalZzzRadio.Checked = true;
            }

            else if (isVeryWeak)
            {
                LoggingManager.Instance.Log("SLEEP DAMAGE settings are set to VERY WEAK");
                VeryWeakZzzRadio.Checked = true;
            }

            else if (isOneShotSleep)
            {
                LoggingManager.Instance.Log("SLEEP DAMAGE settings are set to ONE SHOT SLEEP");
                OneShotSleepZzzRadio.Checked = true;
            }

            else
            {
                LoggingManager.Instance.Log("SLEEP DAMAGE settings are custom or unrecognized.");
            }
        }



        public void SetStunRadio()
        {
            bool isNeckSnap = DamageManager.Instance.AreStunValuesInvincible();
            bool isVeryStrong = DamageManager.Instance.AreStunValuesVeryStrong();
            bool isNormal = DamageManager.Instance.AreStunValuesDefault();
            bool isVeryWeak = DamageManager.Instance.AreStunValuesVeryWeak();
            bool isOneShotStun = DamageManager.Instance.AreStunValuesOneShot();

            if (isNeckSnap)
            {
                LoggingManager.Instance.Log("STUN DAMAGE settings are set to neck INVINCIBLE.");
                NeckSnapStunRadio.Checked = true;
            }

            else if (isVeryStrong)
            {
                LoggingManager.Instance.Log("STUN DAMAGE settings are set to VERY STRONG");
                VeryStrongStunRadio.Checked = true;
            }

            else if (isNormal)
            {
                LoggingManager.Instance.Log("STUN DAMAGE settings are set to DEFAULT.");
                NormalStunRadio.Checked = true;
            }

            else if (isVeryWeak)
            {
                LoggingManager.Instance.Log("STUN DAMAGE settings are set to VERY WEAK");
                VeryWeakStunRadio.Checked = true;
            }

            else if (isOneShotStun)
            {
                LoggingManager.Instance.Log("STUN DAMAGE settings are set to ONE SHOT STUN");
                OneShotStunStunRadio.Checked = true;
            }

            else
            {
                LoggingManager.Instance.Log("STUN DAMAGE settings are custom or unrecognized.");
            }
        }

        // Lethal Radio Buttons
        private void NeckSnapLethalRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllLethalInvincibleValues();
        }

        private void VeryStrongLethalRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllLethalVeryStrongValues();
        }

        private void NormalLethalRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllLethalDefaultValues();
        }

        private void VeryWeakLethalRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllLethalVeryWeakValues();
        }

        private void OneShotKillLethalRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllLethalOneshotValues();
        }

        // Sleep Radio Buttons
        private void InvincibleZzzRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllSleepInvincibleValues();
        }

        private void VeryStrongZzzRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllSleepVeryStrongValues();
        }

        private void NormalZzzRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllSleepNormalValues();
        }

        private void VeryWeakZzzRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllSleepVeryWeakValues();
        }

        private void OneShotSleepZzzRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllSleepOneShotValues();
        }

        // Stun Radio Buttons
        private void NeckSnapStunRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllStunInvincibleValues();
        }

        private void VeryStrongStunRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllStunVeryStrongValues();
        }

        private void NormalStunRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllStunDefaultValues();
        }

        private void VeryWeakStunRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllStunVeryWeakValues();
        }

        private void OneShotStunStunRadio_CheckedChanged(object sender, EventArgs e)
        {
            DamageManager.Instance.WriteAllStunOneShotValues();
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
                // The t/f statements are just to stop multiple checkboxes from being checked at once
                // As it could have unintended consequences so this was a good way to stop that
                suppressAlertMessages = true;
                InfiniteAlert.Checked = false;
                MessageBox.Show("Only one Infinite Status allowed at once. \nDeselect Evasion Mode to use this.");
                suppressAlertMessages = false;
            }
            else if (InfiniteCaution.Checked)
            {
                suppressAlertMessages = true;
                InfiniteAlert.Checked = false;
                MessageBox.Show("Only one Infinite Status allowed at once. \nDeselect Caution Mode to use this.");
                suppressAlertMessages = false;
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
                    TimerManager.ToggleInfiniteAlert(true);
                }
                else
                {
                    // Stop the alert timer when the checkbox is unchecked
                    TimerManager.ToggleInfiniteAlert(false);
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
                TimerManager.ToggleInfiniteCaution(InfiniteCaution.Checked);
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
                TimerManager.StartEvasionSequence();
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
                TimerManager.ToggleInfiniteEvasion(InfiniteEvasion.Checked);
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


        private void BatteryDrainCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BatteryDrainCheckBox.Checked)
            {
                MiscManager.Instance.DisableBatteryDrain();
            }

            else
            {
                MiscManager.Instance.EnableBatteryDrain();
            }
        }

        
        public void CheckBatteryDrain()
        {
            if (MiscManager.Instance.IsBatteryDrainNOP())
            {
                BatteryDrainCheckBox.Checked = true;
            }

            else
            {
                BatteryDrainCheckBox.Checked = false;
            }
            

        }

    }
}