using static MGS3_MC_Cheat_Trainer.MemoryManager;
using static MGS3_MC_Cheat_Trainer.Constants;
using System.Diagnostics;
using System.Windows.Forms; // Ensure you have this using directive for MessageBox

namespace MGS3_MC_Cheat_Trainer
{
    internal class AlertManager
    {
        private static IntPtr alertMemoryRegion; // Declare at class scope
        private static IntPtr processHandle;
        // Function checks the timer for alert and if it's 0 we trigger the alert mode address
        internal static void InfiniteStatus(Constants.AlertModes mode)
        {
            Process process;
            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                /* Commenting out for now since if the game crashes you'll be stuck in MessageBox loop until you close 
                the program with task manager since if the checkbox is checked it'll keep trying to find the process 
                need to find a better way to handle this */
                //MessageBox.Show($"Cannot find process: {Constants.PROCESS_NAME}");
                //return;
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            IntPtr timerAddress;
            switch (mode)
            {
                case Constants.AlertModes.Alert:
                    timerAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)MGS3AlertModes.Alert.AlertTimerOffset);
                    break;

                /* Evasion kind of useless since Normal status to Evasion just makes the guards moonwalk just 
                putting it in here incase I find a way to use in the future */
                case Constants.AlertModes.Evasion:
                    timerAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)MGS3AlertModes.Evasion.AlertTimerOffset);
                    break;

                case Constants.AlertModes.Caution:
                    timerAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)MGS3AlertModes.Caution.AlertTimerOffset);
                    break;

                default:
                    throw new ArgumentException("Invalid mode");
            }

            byte timerValue;
            try
            {
                timerValue = ReadByteFromMemory(processHandle, timerAddress);
            }
            catch
            {
                MessageBox.Show("Failed to read timer value.");
                return;
            }

            if (timerValue == 0)
            {
                ChangeAlertMode((byte)mode);
            }

            NativeMethods.CloseHandle(processHandle);
        }

        const int PROCESS_VM_READ = 0x0010;

        internal static int GetAlertTimerValue()
        {
            try
            {
                Process process = GetMGS3Process();
                IntPtr processHandle = NativeMethods.OpenProcess(PROCESS_VM_READ, false, process.Id);

                IntPtr alertTimerAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)MGS3AlertModes.Alert.AlertTimerOffset);

                if (NativeMethods.ReadProcessMemory(processHandle, alertTimerAddress, out short alertTimerValue, sizeof(short), out int bytesRead) && bytesRead == sizeof(short))
                {
                    NativeMethods.CloseHandle(processHandle);
                    Console.WriteLine($"Alert Timer Value: {alertTimerValue}"); // Logging the value for debugging
                    return alertTimerValue; // Convert short to int
                }
                else
                {
                    throw new Exception("Failed to read alert timer value");
                }
            }
            catch (Exception ex)
            {
                return -1; // Return an error code or a default value
            }
        }

        internal static int GetEvasionTimerValue()
        {
            try
            {
                Process process = GetMGS3Process();
                IntPtr processHandle = NativeMethods.OpenProcess(PROCESS_VM_READ, false, process.Id);

                IntPtr evasionTimerAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)MGS3AlertModes.Evasion.AlertTimerOffset);

                if (NativeMethods.ReadProcessMemory(processHandle, evasionTimerAddress, out short evasionTimerValue, sizeof(short), out int bytesRead) && bytesRead == sizeof(short))
                {
                    NativeMethods.CloseHandle(processHandle);
                    Console.WriteLine($"Evasion Timer Value: {evasionTimerValue}"); // Logging the value for debugging
                    return evasionTimerValue; // Convert short to int
                }
                else
                {
                    throw new Exception("Failed to read evasion timer value");
                }
            }
            catch (Exception ex)
            {
                return -1; // Return an error code or a default value
            }
        }

        internal static int GetCautionTimerValue()
        {
            try
            {
                Process process = GetMGS3Process();
                IntPtr processHandle = NativeMethods.OpenProcess(PROCESS_VM_READ, false, process.Id);

                IntPtr cautionTimerAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)MGS3AlertModes.Caution.AlertTimerOffset);

                if (NativeMethods.ReadProcessMemory(processHandle, cautionTimerAddress, out short cautionTimerValue, sizeof(short), out int bytesRead) && bytesRead == sizeof(short))
                {
                    NativeMethods.CloseHandle(processHandle);
                    Console.WriteLine($"Caution Timer Value: {cautionTimerValue}"); // Logging the value for debugging
                    return cautionTimerValue; // Convert short to int
                }
                else
                {
                    throw new Exception("Failed to read caution timer value");
                }
            }
            catch (Exception ex)
            {
                return -1; // Return an error code or a default value
            }
        }

        internal static Constants.AlertModes GetAlertStatus()
        {
            try
            {
                Process process = GetMGS3Process();
                IntPtr processHandle = NativeMethods.OpenProcess(PROCESS_VM_READ, false, process.Id);

                // Read the timer values for Alert, Evasion, and Caution
                short alertTimerValue = ReadTimerValue(processHandle, Constants.MGS3AlertModes.Alert.AlertTimerOffset);
                short evasionTimerValue = ReadTimerValue(processHandle, Constants.MGS3AlertModes.Evasion.AlertTimerOffset);
                short cautionTimerValue = ReadTimerValue(processHandle, Constants.MGS3AlertModes.Caution.AlertTimerOffset);

                NativeMethods.CloseHandle(processHandle);

                // Determine which mode is active based on timer values
                if (alertTimerValue > 0)
                    return Constants.AlertModes.Alert;
                else if (evasionTimerValue > 0)
                    return Constants.AlertModes.Evasion;
                else if (cautionTimerValue > 0)
                    return Constants.AlertModes.Caution;
                else
                    return Constants.AlertModes.Normal; // Return 'Normal' when no timers are active
            }
            catch (Exception ex)
            {
                // Handle exceptions, possibly return a default or error state
                return Constants.AlertModes.Normal; // Return 'Normal' as a default
            }
        }

        public static short ReadTimerValue(IntPtr processHandle, IntPtr timerOffset)
        {
            IntPtr timerAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)timerOffset);
            if (NativeMethods.ReadProcessMemory(processHandle, timerAddress, out short timerValue, sizeof(short), out int bytesRead) && bytesRead == sizeof(short))
            {
                return timerValue;
            }
            else
            {
                throw new Exception("Failed to read timer value");
            }
        }

        internal static void RemoveEvasionAndCaution()
        {
            try
            {
                Process process = GetMGS3Process();
                IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

                // Use the AlertStatusOffset directly
                IntPtr address = IntPtr.Add(process.MainModule.BaseAddress, (int)AlertStatusOffset);

                // Read the current 16-bit value
                byte[] buffer = new byte[2];
                if (!NativeMethods.ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _))
                {
                    MessageBox.Show("Failed to read current value.");
                    return;
                }

                // Convert byte array to a 16-bit integer
                short currentValue = BitConverter.ToInt16(buffer, 0);

                // Modify the bits from Binary6 to 15 with the value 400
                short modifiedValue = SetSpecificBits(currentValue, 6, 15, 400);

                // Convert the modified value back to a byte array
                byte[] newValueBytes = BitConverter.GetBytes(modifiedValue);

                // Write the modified 16-bit value back
                if (!NativeMethods.WriteProcessMemory(processHandle, address, newValueBytes, (uint)newValueBytes.Length, out _))
                {
                    MessageBox.Show("Failed to write new value to remove evasion and caution.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception occurred: {ex.Message}");
            }
        }

        internal static void SetEvasionBits()
        {
            try
            {
                Process process = GetMGS3Process();
                IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

                // Use the AlertStatusOffset directly
                IntPtr address = IntPtr.Add(process.MainModule.BaseAddress, (int)AlertStatusOffset);

                // Read the current 16-bit value
                byte[] buffer = new byte[2];
                if (!NativeMethods.ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _))
                {
                    MessageBox.Show("Failed to read current value.");
                    return;
                }

                // Convert byte array to a 16-bit integer
                short currentValue = BitConverter.ToInt16(buffer, 0);

                // Modify the bits from Binary5 to 14 with the value 596
                short modifiedValue = SetSpecificBits(currentValue, 5, 14, 596);

                // Convert the modified value back to a byte array
                byte[] newValueBytes = BitConverter.GetBytes(modifiedValue);

                // Write the modified 16-bit value back
                if (!NativeMethods.WriteProcessMemory(processHandle, address, newValueBytes, (uint)newValueBytes.Length, out _))
                {
                    MessageBox.Show("Failed to write new evasion value.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception occurred: {ex.Message}");
            }
        }

        internal static void FreezeAlertTimer()
        {
            Process process;
            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                // Handle process not found
                return;
            }

            IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            IntPtr alertTimerAddress = IntPtr.Add(process.MainModule.BaseAddress, (int)MGS3AlertModes.Evasion.AlertTimerOffset);

            // Value to freeze the timer at 18000
            short timerValue = 18000;
            byte[] buffer = BitConverter.GetBytes(timerValue);

            // Write the value
            if (!NativeMethods.WriteProcessMemory(processHandle, alertTimerAddress, buffer, (uint)buffer.Length, out _))
            {
                // Handle write failure
            }

            NativeMethods.CloseHandle(processHandle);
        }
        internal static void ChangeAlertMode(byte alertMode)
        {
            ModifyByteValueObject(Constants.AlertStatusOffset, alertMode);
        }

        internal static void TriggerAlert(AlertModes alertMode)
        {
            MemoryManager memoryManager = new MemoryManager();
            var aobPattern = Constants.AOBs["AlertMemoryRegion"].Pattern;
            var mask = Constants.AOBs["AlertMemoryRegion"].Mask;

            IntPtr alertMemoryRegion = memoryManager.FindAlertMemoryRegion(aobPattern, mask);
            if (alertMemoryRegion == IntPtr.Zero)
            {
                MessageBox.Show("Failed to find alert memory region.");
                return;
            }

            // The alert triggering address is 64 bytes after the found region
            IntPtr triggerAddress = IntPtr.Add(alertMemoryRegion, 78);

            // Set the alert value based on the provided alertMode
            byte alertValue = (byte)alertMode;

            memoryManager.WriteByteValueToMemory(triggerAddress, alertValue);

            if (alertMode == AlertModes.Evasion)
            {
                // Set the evasion bits using your existing logic
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