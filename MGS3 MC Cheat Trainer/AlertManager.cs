using System;
using System.Windows.Forms;
using static MGS3_MC_Cheat_Trainer.MemoryManager;
using static MGS3_MC_Cheat_Trainer.Constants;
using System.Diagnostics;
using System.Windows.Forms;
using System.Buffers;

namespace MGS3_MC_Cheat_Trainer
{
    internal class AlertManager
    {
        private static System.Windows.Forms.Timer _alertTimer = new System.Windows.Forms.Timer();
        private static System.Windows.Forms.Timer _evasionTimer = new System.Windows.Forms.Timer();
        private static System.Windows.Forms.Timer _cautionTimer = new System.Windows.Forms.Timer();
        private static System.Windows.Forms.Timer evasionStepTimer = new System.Windows.Forms.Timer();
        private static int evasionStep = 0;

        // Couldn't find a better file to put this in so gonna slap it in here for now to track the player's location
        // This way if they crash I can see where they were and decide if it's user error or a bug
        private static System.Windows.Forms.Timer _locationChangeTimer = new System.Windows.Forms.Timer();




        // Define the backing field for IsInfiniteAlertEnabled
        private static bool infiniteAlertEnabled = false;
        private static bool infiniteEvasionEnabled = false;
        private static bool infiniteCautionEnabled = false;

        // Property to check if infinite alert is enabled
        public static bool IsInfiniteAlertEnabled => infiniteAlertEnabled;
        public static bool IsInfiniteEvasionEnabled => infiniteEvasionEnabled;
        public static bool IsInfiniteCautionEnabled => infiniteCautionEnabled;

        static AlertManager()
        {
            _alertTimer.Interval = 1000; // Set the interval to 1 second, adjust as needed
            _alertTimer.Tick += AlertTimer_Tick;

            _evasionTimer.Interval = 1000; // Set the interval to 1 second, adjust as needed
            _evasionTimer.Tick += EvasionTimer_Tick;

            evasionStepTimer.Interval = 3000; // First delay for caution
            evasionStepTimer.Tick += EvasionStepTimer_Tick;

            _cautionTimer.Interval = 1000; // 10 seconds for caution checks
            _cautionTimer.Tick += CautionTimer_Tick;

            _locationChangeTimer.Interval = 2000; // Check every 2 seconds, adjust as needed
            _locationChangeTimer.Tick += LocationChangeTimer_Tick;
        }

        private static string _lastKnownLocation = "";

        private static void LocationChangeTimer_Tick(object sender, EventArgs e)
        {
            // Directly use the result from FindLocationStringDirectlyInRange, which includes all details.
            string currentLocationInfo = StringManager.Instance.FindLocationStringDirectlyInRange();

            if (currentLocationInfo != _lastKnownLocation)
            {
                _lastKnownLocation = currentLocationInfo;
                // Log the change with full details (string, name, and address)
                LoggingManager.Instance.Log($"Location changed: {currentLocationInfo}\n");
            }
        }


        public static void StartLocationTracking()
        {
            _locationChangeTimer.Start();
            LoggingManager.Instance.Log("Location tracking started.");
        }



        private static void AlertTimer_Tick(object sender, EventArgs e)
        {
            // Assuming the alert memory region is already found and stored
            IntPtr alertMemoryRegion = MemoryManager.Instance.FindAob("AlertMemoryRegion");


            if (alertMemoryRegion != IntPtr.Zero)
            {
                WriteMaxAlertTimerValue(alertMemoryRegion);
            }
            else
            {
                // Optionally, stop the timer if the region is not found
                // _alertTimer.Stop();

                ToggleInfiniteAlert(false); // Stop the infinite alert if the region is not found
            }
        }

        private static void EvasionStepTimer_Tick(object sender, EventArgs e)
        {
            switch (evasionStep)
            {
                case 0:
                    // Clear Evasion and Caution to ensure no accidental Alert trigger
                    RemoveEvasionAndCaution();
                    // Trigger caution mode
                    TriggerAlert(AlertModes.Caution);
                    evasionStepTimer.Interval = 3000; // Time until next step
                    evasionStep = 1; // Prepare for next step
                    break;
                case 1:
                    // Modify the evasion bits
                    SetEvasionBits();
                    evasionStepTimer.Interval = 750; // Time until final step
                    evasionStep = 2; // Prepare for final step
                    break;
                case 2:
                    // Trigger alert mode to finalize evasion
                    TriggerAlert(AlertModes.Alert);
                    evasionStepTimer.Stop(); // Sequence complete, stop the timer
                    evasionStep = 0; // Reset for next time
                    break;
            }
        }

        private static void EvasionTimer_Tick(object sender, EventArgs e)
        {
            if (!infiniteEvasionEnabled) return;

            IntPtr alertMemoryRegion = MemoryManager.Instance.FindAob("AlertMemoryRegion");
            if (alertMemoryRegion == IntPtr.Zero) return;

            short alertTimerValue = ReadAlertTimerValue(alertMemoryRegion);
            short evasionTimerValue = ReadEvasionTimerValue(alertMemoryRegion);

            // Initiate evasion sequence if conditions are right
            if (alertTimerValue <= 0 && evasionTimerValue <= 0 && evasionStep == 0) // Make sure evasion sequence isn't already running
            {
                StartEvasionSequence();
            }
        }


        public static void StartEvasionSequence()
        {
            evasionStep = 0; // Reset to start of sequence
            evasionStepTimer.Start(); // Start the sequence
        }

        private static void CautionTimer_Tick(object sender, EventArgs e)
        {
            if (IsInfiniteCautionEnabled)
            {
                // Assuming caution mode needs to be triggered when the timer reaches 0 or periodically
                TriggerAlert(AlertModes.Caution);
            }
        }

        public static void ToggleInfiniteAlert(bool enable)
        {
            infiniteAlertEnabled = enable; // Update the infinite alert status

            if (enable)
            {
                _alertTimer.Start();
                LoggingManager.Instance.Log("Infinite Alert enabled.\n");
            }
            else
            {
                _alertTimer.Stop();
                LoggingManager.Instance.Log("Infinite Alert disabled.\n");
            }
        }

        public static void ToggleInfiniteEvasion(bool enable)
        {
            infiniteEvasionEnabled = enable;
            if (enable)
            {
                _evasionTimer.Start(); // Start checking conditions for evasion
                LoggingManager.Instance.Log("Infinite Evasion enabled.\n");
            }
            else
            {
                _evasionTimer.Stop(); // Stop checking
                evasionStepTimer.Stop(); // Also stop the evasion sequence timer if running
                LoggingManager.Instance.Log("Infinite Evasion disabled.\n");
            }
        }


        public static void ToggleInfiniteCaution(bool enable)
        {
            infiniteCautionEnabled = enable;
            if (enable)
            {
                _cautionTimer.Start();
                LoggingManager.Instance.Log("Infinite Caution enabled.\n");
            }
            else
            {
                _cautionTimer.Stop();
                LoggingManager.Instance.Log("Infinite Caution disabled.\n");
            }
        }


        internal static void WriteMaxAlertTimerValue(IntPtr alertMemoryRegion)
        {
            var memoryManager = new MemoryManager();
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr timerAddress = IntPtr.Add(alertMemoryRegion, AlertTimerOffset);
            short maxTimerValue = short.MaxValue; // Maximum value for a 2-byte variable
            MemoryManager.WriteShortToMemory(processHandle, timerAddress, maxTimerValue);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        internal static void SetEvasionBits()
        {
            MemoryManager memoryManager = new MemoryManager();
            IntPtr alertMemoryRegion = memoryManager.FindAob("AlertMemoryRegion");

            if (alertMemoryRegion == IntPtr.Zero)
            {

                LoggingManager.Instance.Log("Alert memory region not found.\n");
                return;
            }

            IntPtr modifyAddress = IntPtr.Add(alertMemoryRegion, 78); // Same address as TriggerAlert

            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);

            short currentValue = MemoryManager.ReadShortFromMemory(processHandle, modifyAddress);
            short modifiedValue = SetSpecificBits(currentValue, 5, 14, 596);
            MemoryManager.WriteShortToMemory(processHandle, modifyAddress, modifiedValue);

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        internal static void RemoveEvasionAndCaution()
        {
            MemoryManager memoryManager = new MemoryManager();

            IntPtr alertMemoryRegion = memoryManager.FindAob("AlertMemoryRegion");
            if (alertMemoryRegion == IntPtr.Zero)
            {

                return;
            }

            IntPtr modifyAddress = IntPtr.Add(alertMemoryRegion, 78);

            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);

            short currentValue = MemoryManager.ReadShortFromMemory(processHandle, modifyAddress);
            short modifiedValue = SetSpecificBits(currentValue, 6, 15, 400);
            MemoryManager.WriteShortToMemory(processHandle, modifyAddress, modifiedValue);

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            LoggingManager.Instance.Log("Evasion and Caution bits removed.\n");
        }

        internal static void TriggerAlert(AlertModes alertMode)
        {
            MemoryManager memoryManager = MemoryManager.Instance; // Use the singleton instance of MemoryManager

            // Use FindAob method and specify the key for the alert memory region
            IntPtr alertMemoryRegion = memoryManager.FindAob("AlertMemoryRegion");
            if (alertMemoryRegion == IntPtr.Zero)
            {
                MessageBox.Show("Failed to find alert memory region.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // The alert triggering address is 64 bytes after the found region
            // Adjusted to 78 based on your original code, but make sure this offset is correct
            IntPtr triggerAddress = IntPtr.Add(alertMemoryRegion, 78);

            // Set the alert value based on the provided alertMode
            byte alertValue = (byte)alertMode;

            // Write the alert value to the memory
            MemoryManager.WriteByteValueToMemory(triggerAddress, alertValue);

            if (alertMode == AlertModes.Evasion)
            {
                // Set the evasion bits using your existing logic
                // Ensure this method exists and is correctly implemented
                SetEvasionBits();
            }
        }


        public const int AlertTimerOffset = -6;
        public const int EvasionTimerOffset = 18;
        public const int CautionTimerOffset = -2;


        internal static short ReadAlertTimerValue(IntPtr alertMemoryRegion)
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            IntPtr timerAddress = IntPtr.Add(alertMemoryRegion, AlertTimerOffset);
            return MemoryManager.ReadShortFromMemory(processHandle, timerAddress);
        }

        internal static short ReadEvasionTimerValue(IntPtr alertMemoryRegion)
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            IntPtr timerAddress = IntPtr.Add(alertMemoryRegion, EvasionTimerOffset);
            return MemoryManager.ReadShortFromMemory(processHandle, timerAddress);
        }

        internal static short ReadCautionTimerValue(IntPtr alertMemoryRegion)
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            IntPtr timerAddress = IntPtr.Add(alertMemoryRegion, CautionTimerOffset);
            return MemoryManager.ReadShortFromMemory(processHandle, timerAddress);
        }

    }
}