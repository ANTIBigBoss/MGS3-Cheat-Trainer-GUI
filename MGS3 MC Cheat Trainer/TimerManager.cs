using System.Diagnostics;
using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    public class TimerManager
    {
        private static TimerManager instance;
        private static readonly object lockObj = new object();

        private TimerManager() { }

        public static TimerManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new TimerManager();
                    }
                    return instance;
                }
            }
        }

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

            HudTrackingTimer.Interval = 100;
            HudTrackingTimer.Tick += HudTrackingTimer_Tick;

            RealTimeSwapTrackingTimer.Interval = 100;
            RealTimeSwapTrackingTimer.Tick += RealTimeSwapTrackingTimer_Tick;
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
                LoggingManager.Instance.Log("Infinite Camo Index enabled.");
            }
            else
            {
                camoIndexTimer.Stop();
                LoggingManager.Instance.Log("Infinite Camo Index disabled.");
            }
        }

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

        #region HUD Tracker

        private static System.Windows.Forms.Timer HudTrackingTimer = new System.Windows.Forms.Timer();

        public static bool hudAlwaysHidden = false;

        public static void HudTrackingTimer_Tick(object sender, EventArgs e)
        {

            if (hudAlwaysHidden)
            {
                MiscManager.Instance.PartialDisableHUD();
            }
            else
            {
                MiscManager.Instance.EnableHUD();
            }
            
        }

        public static void ToggleMinimalHud(bool enable)
        {
            hudAlwaysHidden = enable;
            if (enable)
            {
                TimerManager.HudTrackingTimer.Start();
            }
            else if (!enable)
            {
                TimerManager.HudTrackingTimer.Stop();
                LoggingManager.Instance.Log("Minimal HUD disabled.\n");
            }
        }

        #endregion


        #region Real Time Weapon/Item Swap

        private static System.Windows.Forms.Timer RealTimeSwapTrackingTimer = new System.Windows.Forms.Timer();

        // Track if real-time swapping is enabled
        public static bool RealTimeSwapping = false;

        // Track if the player was previously in a menu
        private static bool wasPlayerInMenu = false;

        // Timer tick event handler
        public static void RealTimeSwapTrackingTimer_Tick(object sender, EventArgs e)
        {
            if (RealTimeSwapping)
            {
                PutPlayerInBoxIfChangingWeapons();
            }
            else
            {
                if (wasPlayerInMenu)
                {
                    TakePlayerOutOfBox();
                    wasPlayerInMenu = false;
                }
            }
        }

        // Method to check if the player is in a menu and put them in a box if they are
        public static void PutPlayerInBoxIfChangingWeapons()
        {
            bool isInMenu = IsPlayerInMenu();

            if (isInMenu)
            {
                if (!wasPlayerInMenu)
                {
                    PutPlayerInBox();
                    wasPlayerInMenu = true;
                }
            }
            else if (wasPlayerInMenu)
            {
                TakePlayerOutOfBox();
                wasPlayerInMenu = false;
            }
        }

        // Method to check if the player is in a menu
        private static bool IsPlayerInMenu()
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return false;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return false;

                IntPtr itemWindowAddress = MemoryManager.Instance.FindAob("PissFilter");
                if (itemWindowAddress == IntPtr.Zero) return false;

                IntPtr targetAddress = IntPtr.Add(itemWindowAddress, (int)MiscOffsets.ItemAndWeaponWindowAdd);
                byte[] buffer = new byte[1];
                if (MemoryManager.ReadProcessMemory(processHandle, targetAddress, buffer, 1, out _))
                {
                    return buffer[0] == 0x04;
                }

                return false;
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        // Methods to modify the player's box state
        public static void PutPlayerInBox()
        {
            ModifyPlayerBoxState(0x21, "Player put in the box successfully.", "Failed to put the player in the box.");
        }

        public static void TakePlayerOutOfBox()
        {
            ModifyPlayerBoxState(0x00, "Player taken out of the box successfully.", "Failed to take the player out of the box.");
        }

        private static void ModifyPlayerBoxState(byte boxValue, string successMessage, string failureMessage)
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return;

                IntPtr itemWindowAddress = MemoryManager.Instance.FindAob("PissFilter");
                if (itemWindowAddress == IntPtr.Zero) return;

                IntPtr targetAddress = IntPtr.Add(itemWindowAddress, (int)AnimationOffsets.BoxCrouchAdd);
                bool success = MemoryManager.WriteMemory(processHandle, targetAddress, boxValue);
                if (!success)
                {
                    LoggingManager.Instance.Log(failureMessage);
                }
                else
                {
                    LoggingManager.Instance.Log(successMessage);
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        // Methods to enable/disable real-time weapon/item swapping
        public static void RealTimeWeaponItemSwapping()
        {
            ModifyRealTimeWeaponItemSwapping(new byte[] { 0x85, 0x05, 0xC0, 0x59, 0xA7, 0x01 }, "Real Time Weapon and Item Swapping enabled successfully.");
            RealTimeSwapTrackingTimer.Start();
        }

        public static void DisableRealTimeWeaponItemSwapping()
        {
            ModifyRealTimeWeaponItemSwapping(new byte[] { 0x85, 0x05, 0x77, 0xF1, 0xC4, 0x01 }, "Real Time Weapon and Item Swapping disabled successfully.");
            RealTimeSwapTrackingTimer.Stop();
        }

        private static void ModifyRealTimeWeaponItemSwapping(byte[] weaponSwapBytes, string successMessage)
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null)
                {
                    LoggingManager.Instance.Log("Failed to find game process.");
                    return;
                }

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to open game process.");
                    return;
                }

                IntPtr weaponSwapAddress = MemoryManager.Instance.FindAob("RealTimeItemSwap");
                if (weaponSwapAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to find Real Time Weapon Swap AOB pattern.");
                    return;
                }

                IntPtr targetAddress = IntPtr.Add(weaponSwapAddress, (int)MiscOffsets.RealTimeWeaponItemSwappingAdd);
                bool success = MemoryManager.WriteMemory(processHandle, targetAddress, weaponSwapBytes);
                if (!success)
                {
                    LoggingManager.Instance.Log("Failed to modify Real Time Weapon and Item Swapping.");
                }
                else
                {
                    LoggingManager.Instance.Log(successMessage);
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }
    

    #endregion
}
}
