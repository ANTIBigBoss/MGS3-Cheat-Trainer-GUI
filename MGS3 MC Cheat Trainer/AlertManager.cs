using static MGS3_MC_Cheat_Trainer.Constants;
using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    internal class AlertManager
    {
        internal static void WriteMaxAlertTimerValue(IntPtr alertMemoryRegion)
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr timerAddress = IntPtr.Subtract(alertMemoryRegion, (int)Constants.AlertOffsets.AlertTimerSub);
            short maxTimerValue = short.MaxValue;

            MemoryManager.WriteMemory(processHandle, timerAddress, maxTimerValue);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        internal static void SetEvasionBits()
        {
            IntPtr alertMemoryRegion = MemoryManager.Instance.FindAob("AlertMemoryRegion");

            if (alertMemoryRegion == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Alert memory region not found.\n");
                return;
            }

            IntPtr modifyAddress = IntPtr.Add(alertMemoryRegion, (int)Constants.AlertOffsets.AlertTriggerAdd);
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, modifyAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {modifyAddress}");
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return;
            }

            short currentValue = BitConverter.ToInt16(buffer, 0);
            short modifiedValue = MemoryManager.SetSpecificBits(currentValue, 5, 14, 596);

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
            IntPtr alertMemoryRegion = MemoryManager.Instance.FindAob("AlertMemoryRegion");

            if (alertMemoryRegion == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Alert memory region not found.\n");
                return;
            }

            IntPtr modifyAddress = IntPtr.Add(alertMemoryRegion, (int)Constants.AlertOffsets.AlertTriggerAdd);
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
            short modifiedValue = MemoryManager.SetSpecificBits(currentValue, 6, 15, 400);

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

            IntPtr alertMemoryRegion = MemoryManager.Instance.FindAob("AlertMemoryRegion");
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

            IntPtr triggerAddress = IntPtr.Add(alertMemoryRegion, (int)Constants.AlertOffsets.AlertTriggerAdd);

            byte alertValue = (byte)alertMode;

            bool writeSuccess = MemoryManager.WriteMemory(processHandle, triggerAddress, alertValue);
            if (!writeSuccess)
            {
                MessageBox.Show("Failed to write alert value.", "Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (alertMode == AlertModes.Evasion)
                {
                    SetEvasionBits();
                }
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        internal static short ReadAlertTimerValue(IntPtr alertMemoryRegion)
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            IntPtr timerAddress = IntPtr.Subtract(alertMemoryRegion, (int)Constants.AlertOffsets.AlertTimerSub);

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
            IntPtr timerAddress = IntPtr.Add(alertMemoryRegion, (int)Constants.AlertOffsets.EvasionTimerAdd);

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
            IntPtr timerAddress = IntPtr.Subtract(alertMemoryRegion, (int)Constants.AlertOffsets.CautionTimerSub);

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