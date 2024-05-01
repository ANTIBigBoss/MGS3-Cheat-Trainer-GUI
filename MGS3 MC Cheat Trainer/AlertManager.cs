using static MGS3_MC_Cheat_Trainer.MemoryManager;
using static MGS3_MC_Cheat_Trainer.Constants;
using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    internal class AlertManager
    {

        internal static void WriteMaxAlertTimerValue(IntPtr alertMemoryRegion)
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr timerAddress = IntPtr.Add(alertMemoryRegion, AlertTimerOffset);
            short maxTimerValue = short.MaxValue; // Maximum value for a 2-byte variable

            // Using the generic WriteMemory method to write the short value
            MemoryManager.WriteMemory(processHandle, timerAddress, maxTimerValue);
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
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());

            // Read current value using the new ReadMemoryBytes function
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, modifyAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {modifyAddress}");
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return;
            }

            short currentValue = BitConverter.ToInt16(buffer, 0);
            short modifiedValue = SetSpecificBits(currentValue, 5, 14, 596);

            // Using the generic WriteMemory method to apply the new value
            bool writeSuccess = MemoryManager.WriteMemory(processHandle, modifyAddress, modifiedValue);
            if (!writeSuccess)
            {
                LoggingManager.Instance.Log($"Failed to write memory at {modifyAddress}");
            }
            else
            {
                LoggingManager.Instance.Log($"Evasion bits successfully set to {modifiedValue} at {modifyAddress}");
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        internal static void RemoveEvasionAndCaution()
        {
            MemoryManager memoryManager = new MemoryManager();
            IntPtr alertMemoryRegion = memoryManager.FindAob("AlertMemoryRegion");

            if (alertMemoryRegion == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Alert memory region not found.\n");
                return;
            }

            IntPtr modifyAddress = IntPtr.Add(alertMemoryRegion, 78);
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());

            // Read current value using the new ReadMemoryBytes function
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, modifyAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {modifyAddress}");
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return;
            }

            short currentValue = BitConverter.ToInt16(buffer, 0);
            short modifiedValue = SetSpecificBits(currentValue, 6, 15, 400);

            // Using the generic WriteMemory method to apply the new value
            bool writeSuccess = MemoryManager.WriteMemory(processHandle, modifyAddress, modifiedValue);
            if (!writeSuccess)
            {
                LoggingManager.Instance.Log($"Failed to write memory at {modifyAddress}");
            }
            else
            {
                LoggingManager.Instance.Log("Evasion and Caution bits removed.");
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);
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

            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open game process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // The alert triggering address is 78 bytes after the found region
            IntPtr triggerAddress = IntPtr.Add(alertMemoryRegion, 78);

            // Set the alert value based on the provided alertMode
            byte alertValue = (byte)alertMode;

            // Write the alert value to the memory using the generic WriteMemory method
            bool writeSuccess = WriteMemory(processHandle, triggerAddress, alertValue);
            if (!writeSuccess)
            {
                MessageBox.Show("Failed to write alert value.", "Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (alertMode == AlertModes.Evasion)
                {
                    // Set the evasion bits using your existing logic
                    SetEvasionBits(); // Ensure this method exists and is correctly implemented
                }
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }


        public const int AlertTimerOffset = -6;
        public const int EvasionTimerOffset = 18;
        public const int CautionTimerOffset = -2;

        internal static short ReadAlertTimerValue(IntPtr alertMemoryRegion)
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            IntPtr timerAddress = IntPtr.Add(alertMemoryRegion, AlertTimerOffset);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, timerAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read alert timer value at {timerAddress}");
                return 0;
            }

            return BitConverter.ToInt16(buffer, 0);
        }

        internal static short ReadEvasionTimerValue(IntPtr alertMemoryRegion)
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            IntPtr timerAddress = IntPtr.Add(alertMemoryRegion, EvasionTimerOffset);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, timerAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read evasion timer value at {timerAddress}");
                return 0;
            }

            return BitConverter.ToInt16(buffer, 0);
        }

        internal static short ReadCautionTimerValue(IntPtr alertMemoryRegion)
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            IntPtr timerAddress = IntPtr.Add(alertMemoryRegion, CautionTimerOffset);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, timerAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read caution timer value at {timerAddress}");
                return 0;
            }

            return BitConverter.ToInt16(buffer, 0);
        }

    }
}