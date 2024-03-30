using static MGS3_MC_Cheat_Trainer.MemoryManager;
using static MGS3_MC_Cheat_Trainer.Constants;
using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    internal class AlertManager
    {
        
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