using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    internal class TimerManager
    {
        public static int UserCamoIndex { get; set; } = 40;

        static TimerManager()
        {
            AlertTimer.Interval = 1000;
            AlertTimer.Tick += AlertTimer_Tick;

            EvasionTimer.Interval = 1000;
            EvasionTimer.Tick += EvasionTimer_Tick;

            EvasionStepTimer.Interval = 3000;
            EvasionStepTimer.Tick += EvasionStepTimer_Tick;

            CautionTimer.Interval = 1000;
            CautionTimer.Tick += CautionTimer_Tick;

            LocationChangeTimer.Interval = 2000;
            LocationChangeTimer.Tick += LocationChangeTimer_Tick;

            camoIndexTimer.Interval = 1000;
            camoIndexTimer.Tick += CamoIndexTimer_Tick;
        }

        #region Camo Index Timer
        private static System.Windows.Forms.Timer camoIndexTimer = new System.Windows.Forms.Timer();
        private static bool infiniteCamoIndexEnabled = false;
        public static bool IsInfiniteCamoIndexEnabled => infiniteCamoIndexEnabled;

        private static void CamoIndexTimer_Tick(object sender, EventArgs e)
        {
            
        }


        public static void ToggleInfiniteCamoIndex(bool enable)
        {
            infiniteCamoIndexEnabled = enable;
            if (enable)
            {
                camoIndexTimer.Start();
                // Optionally apply NOP immediately if needed here
                LoggingManager.Instance.Log("Infinite Camo Index enabled.");
            }
            else
            {
                camoIndexTimer.Stop();
                // Optionally restore original state immediately if needed here
                LoggingManager.Instance.Log("Infinite Camo Index disabled.");
            }
        }

        // use in a form like enable/disable like this:
        // ;
        // TimerManager.ToggleInfiniteCamoIndex(false);

        #endregion

        #region Infinite Alert Status
        private static System.Windows.Forms.Timer AlertTimer = new System.Windows.Forms.Timer();
        public static bool IsInfiniteAlertEnabled => infiniteAlertEnabled;
        private static bool infiniteAlertEnabled = false;

        private static void AlertTimer_Tick(object sender, EventArgs e)
        {
            IntPtr alertMemoryRegion = MemoryManager.Instance.FindAob("AlertMemoryRegion");

            if (alertMemoryRegion != IntPtr.Zero)
            {
                AlertManager.TriggerAlert(AlertModes.Alert);
            }
            else
            {
                // Optionally, stop the timer if the region is not found
                // AlertTimer.Stop();

                ToggleInfiniteAlert(false); // Stop the infinite alert if the region is not found
            }
        }

        public static void ToggleInfiniteAlert(bool enable)
        {
            infiniteAlertEnabled = enable; // Update the infinite alert status

            if (enable)
            {
                AlertTimer.Start();
                LoggingManager.Instance.Log("Infinite Alert enabled.\n");
            }
            else
            {
                AlertTimer.Stop();
                LoggingManager.Instance.Log("Infinite Alert disabled.\n");
            }
        }
        #endregion

        #region Infinite Evasion Status
        private static System.Windows.Forms.Timer EvasionTimer = new System.Windows.Forms.Timer();
        private static System.Windows.Forms.Timer EvasionStepTimer = new System.Windows.Forms.Timer();
        private static int EvasionStep = 0;
        private static bool infiniteEvasionEnabled = false;
        public static bool IsInfiniteEvasionEnabled => infiniteEvasionEnabled;

        public static void StartEvasionSequence()
        {
            EvasionStep = 0; // Reset to start of sequence
            EvasionStepTimer.Start(); // Start the sequence
        }
        private static void EvasionStepTimer_Tick(object sender, EventArgs e)
        {
            switch (EvasionStep)
            {
                case 0:
                    // Clear Evasion and Caution to ensure no accidental Alert trigger
                    AlertManager.RemoveEvasionAndCaution();
                    // Trigger caution mode
                    AlertManager.TriggerAlert(AlertModes.Caution);
                    EvasionStepTimer.Interval = 3000; // Time until next step
                    EvasionStep = 1; // Prepare for next step
                    break;
                case 1:
                    // Modify the evasion bits
                    AlertManager.SetEvasionBits();
                    EvasionStepTimer.Interval = 750; // Time until final step
                    EvasionStep = 2; // Prepare for final step
                    break;
                case 2:
                    // Trigger alert mode to finalize evasion
                    AlertManager.TriggerAlert(AlertModes.Alert);
                    EvasionStepTimer.Stop(); // Stop the timer
                    EvasionStep = 0; // Reset for next time
                    break;
            }
        }

        private static void EvasionTimer_Tick(object sender, EventArgs e)
        {
            if (!infiniteEvasionEnabled) return;

            IntPtr alertMemoryRegion = MemoryManager.Instance.FindAob("AlertMemoryRegion");
            if (alertMemoryRegion == IntPtr.Zero) return;

            short alertTimerValue = AlertManager.ReadAlertTimerValue(alertMemoryRegion);
            short evasionTimerValue = AlertManager.ReadEvasionTimerValue(alertMemoryRegion);

            // Initiate evasion sequence if conditions are right and make sure it's not already in progress
            if (alertTimerValue <= 0 && evasionTimerValue <= 0 && EvasionStep == 0) 
            {
                StartEvasionSequence();
            }
        }

        public static void ToggleInfiniteEvasion(bool enable)
        {
            infiniteEvasionEnabled = enable;
            if (enable)
            {
                EvasionTimer.Start();
                LoggingManager.Instance.Log("Infinite Evasion enabled.\n");
            }
            else
            {
                EvasionTimer.Stop();
                EvasionStepTimer.Stop();
                LoggingManager.Instance.Log("Infinite Evasion disabled.\n");
            }
        }

        #endregion

        #region Infinite Caution Status

        private static System.Windows.Forms.Timer CautionTimer = new System.Windows.Forms.Timer();
        private static bool infiniteCautionEnabled = false;
        public static bool IsInfiniteCautionEnabled => infiniteCautionEnabled;

        private static void CautionTimer_Tick(object sender, EventArgs e)
        {
            if (IsInfiniteCautionEnabled)
            {
                // Basically just trigger caution mode every second
                AlertManager.TriggerAlert(AlertModes.Caution);
            }
        }

        public static void ToggleInfiniteCaution(bool enable)
        {
            infiniteCautionEnabled = enable;
            if (enable)
            {
                CautionTimer.Start();
                LoggingManager.Instance.Log("Infinite Caution enabled.\n");
            }
            else
            {
                CautionTimer.Stop();
                LoggingManager.Instance.Log("Infinite Caution disabled.\n");
            }
        }

        #endregion

        #region Map String Tracking
        private static System.Windows.Forms.Timer LocationChangeTimer = new System.Windows.Forms.Timer();
        private static string LastKnownLocation = "";

        private static void LocationChangeTimer_Tick(object sender, EventArgs e)
        {
            string currentLocationInfo = StringManager.Instance.FindLocationStringDirectlyInRange();

            if (currentLocationInfo != LastKnownLocation)
            {
                LastKnownLocation = currentLocationInfo;
                LoggingManager.Instance.Log($"Location changed: {currentLocationInfo}\n");
            }
        }

        public static void StartLocationTracking()
        {
            LocationChangeTimer.Start();
            LoggingManager.Instance.Log("Location tracking started.");
        }
        #endregion
    }
}
