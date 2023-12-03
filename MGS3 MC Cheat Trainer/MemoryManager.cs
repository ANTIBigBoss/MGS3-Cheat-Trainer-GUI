using System.Diagnostics;
using System.Runtime.InteropServices;
using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    internal class MemoryManager
    {
        // PInvoke declarations
        public static class NativeMethods
        {
            // Declare OpenProcess
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

            // Declare WriteProcessMemory with short
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref short lpBuffer, uint nSize, out int lpNumberOfBytesWritten);
            // and with bytes
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

            // Declare ReadProcessMemory
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, out short lpBuffer, uint size, out int lpNumberOfBytesRead);
            // and with bytes
            [DllImport("kernel32.dll")]
            public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesRead);

            // Declare CloseHandle
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool CloseHandle(IntPtr hObject);
        }

        static IntPtr PROCESS_BASE_ADDRESS = IntPtr.Zero;

        #region Private Functions
        private IntPtr ResolvePointerAddress(IntPtr baseAddress, IntPtr pointerOffset, IntPtr finalOffset)
        {
            // NOTE: Presently, this function has no use.

            byte[] buffer = new byte[IntPtr.Size]; // Corrected here
            Process process = GetMGS3Process();
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            NativeMethods.ReadProcessMemory(processHandle, IntPtr.Add(baseAddress, (int)pointerOffset), buffer, (uint)buffer.Length, out _);

            IntPtr pointerAddress;
            if (IntPtr.Size == 8) // 64-bit
            {
                pointerAddress = (IntPtr)BitConverter.ToInt64(buffer, 0);
            }
            else // 32-bit
            {
                pointerAddress = (IntPtr)BitConverter.ToInt32(buffer, 0);
            }

            return IntPtr.Add(pointerAddress, (int)finalOffset);
        }

        /* Experimental future functions to try and not have to refind the base address every time the game updates
        Known base addresses: 7FF71CB30000, 
        public static IntPtr SignatureScan(Process process, byte[] signature, string mask)
        {
            IntPtr baseAddress = process.MainModule.BaseAddress;
            int processSize = process.MainModule.ModuleMemorySize;

            for (int i = 0; i < processSize - signature.Length; i++)
            {
                byte[] buffer = new byte[signature.Length];
                IntPtr address = IntPtr.Add(baseAddress, i);
                ReadProcessMemory(process.Handle, address, buffer, buffer.Length, out _);

                if (IsMatch(buffer, signature, mask))
                    return address;
            }

            return IntPtr.Zero;
        }

        private static bool IsMatch(byte[] buffer, byte[] signature, string mask)
        {
            for (int i = 0; i < signature.Length; i++)
            {
                if (mask[i] == 'x' && buffer[i] != signature[i])
                    return false;
            }
            return true;
        }
        */

        private static short GetShortFromString(string countString)
        {
            if (!short.TryParse(countString, out short countShort))
            {
                MessageBox.Show("Invalid count. Please enter a valid number.");
                return default;
            }

            return countShort;
        }

        private static Process GetMGS3Process()
        {
            Process? process = Process.GetProcessesByName(Constants.PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                throw new NullReferenceException();
            }
            return process;
        }

        private static void ModifyShortObjectWithMaxCount(IntPtr objectCountOffset, IntPtr objectMaxCountOffset, string value)
        {
            ModifyShortValueObject(objectCountOffset, value, objectMaxCountOffset);
        }

        #pragma warning disable CS8602 // Dereference of a possibly null reference. Start disable here.
        private static void ModifyByteValueObject(IntPtr objectOffset, byte value)
        {
            Process process;

            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                MessageBox.Show($"Cannot find process: {Constants.PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            int bytesWritten;

            byte[] buffer = new byte[] { value }; // Value to write
            IntPtr targetAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int) objectOffset); // Adjusted to add base address

            bool success = NativeMethods.WriteProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out bytesWritten);

            if (!success || bytesWritten != buffer.Length)
            {
                MessageBox.Show($"Failed to write memory with value {value}.");
            }

            NativeMethods.CloseHandle(processHandle);
        }

        private static void ModifyShortValueObject(IntPtr objectOffset, string value, IntPtr maxCountOffset = default)
        {
            //TODO: just change value parameter to take in a short instead of a string to get around this...
            short shortValue = GetShortFromString(value);
            
            Process process;

            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                MessageBox.Show($"Cannot find process: {Constants.PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            int bytesWritten;

            // Modify current count
            IntPtr currentItemAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)objectOffset);
            bytesWritten = WriteShortToMemory(processHandle, currentItemAddress, shortValue);

            // Modify max count if specified
            if (maxCountOffset != default)
            {
                IntPtr maxItemAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)maxCountOffset);
                bytesWritten = WriteShortToMemory(processHandle, maxItemAddress, shortValue);
            }

            NativeMethods.CloseHandle(processHandle);
        }

        private static bool ReadWriteToggledSuppressorValue(IntPtr processHandle, IntPtr address)
        {
            bool success = NativeMethods.ReadProcessMemory(processHandle, address, out short currentValue, sizeof(short), out int bytesRead);
            if (!success || bytesRead != sizeof(short))
            {
                return false;
            }

            short valueToWrite = (currentValue == 16) ? (short)0 : (short)16;
            //success = NativeMethods.WriteProcessMemory(processHandle, address, ref valueToWrite, sizeof(short), out int bytesWritten);
            //return success && bytesWritten == sizeof(short);

            try
            {
                int bytesWritten = WriteShortToMemory(processHandle, address, valueToWrite);
                return bytesWritten == sizeof(short);
            }
            catch
            {
                return false;
            }
        }

        private static short ReadShortFromMemory(IntPtr processHandle, IntPtr address)
        {
            if(NativeMethods.ReadProcessMemory(processHandle, address, out short value, sizeof(short), out int bytesRead))
                return value;

            throw new IOException();
        }

        private static int WriteShortToMemory(IntPtr processHandle, IntPtr address, short value)
        {
            if(NativeMethods.WriteProcessMemory(processHandle, address, ref value, sizeof(short), out int bytesWritten))
                return bytesWritten;

            throw new IOException();
        }
        #pragma warning disable CS8602 // Dereference of a possibly null reference. End disable here.
        #endregion

        #region Internal Functions
        internal static void ChangeAlertMode(byte alertMode)
        {
            ModifyByteValueObject(Constants.AlertStatusOffset, alertMode);
        }

        internal static void TriggerSnakeAnimation(Constants.SnakeAnimation snakeAnimation)
        {
            ModifyByteValueObject(snakeAnimation.AnimationOffset, snakeAnimation.Value);
        }

        internal static void ChangeHud(byte hudStatus)
        {
            ModifyByteValueObject(Constants.HudOffset, hudStatus);
        }

        internal static void ChangeCamera(byte cameraSetting)
        {
            ModifyByteValueObject(Constants.CamOffset, cameraSetting);
        }

        internal static void ToggleWeapon(Weapon weapon, bool enable)
        {
            //TODO: this can be made straight to short instead of string once ModifyShortValueObject accepts just a short
            string stateValue = enable ? "1" : "-1";
            ModifyShortValueObject(weapon.MemoryOffset, stateValue);
        }

        internal static void ToggleItemState(Item item, bool enable)
        {
            //TODO: this can be made straight to short instead of string once ModifyShortValueObject accepts just a short
            string stateValue = enable ? "1" : "-1";
            ModifyShortValueObject(item.MemoryOffset, stateValue);
        }

        internal static void ModifyItemCapacity(Item item, string itemCountStr)
        {
            if(item.MaxCapacityOffset != default)
                ModifyShortObjectWithMaxCount(item.MemoryOffset, item.MaxCapacityOffset, itemCountStr);
            else
                ModifyShortValueObject(item.MemoryOffset, itemCountStr);
        }

        internal static void ModifyClipSize(Weapon weapon, string clipSize)
        {
            ModifyShortObjectWithMaxCount(weapon.ClipOffset, weapon.MaxClipOffset, clipSize);
        }

        internal static void ModifyAmmo(Weapon weapon, string ammoCount)
        {
            ModifyShortObjectWithMaxCount(weapon.MemoryOffset, weapon.MaxAmmoOffset, ammoCount);
        }

        internal static void ModifyHealthOrStamina(Constants.HealthType healthType, int value, bool setExactValue = false)
        {
            Process process;

            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                MessageBox.Show($"Cannot find process: {Constants.PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            IntPtr pointerBase = (IntPtr)HealthPointerOffset;
            IntPtr pointerAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)pointerBase);
            byte[] pointerBuffer = new byte[IntPtr.Size];
            NativeMethods.ReadProcessMemory(processHandle, pointerAddress, pointerBuffer, (uint)pointerBuffer.Length, out _);
            IntPtr valuePointer = (IntPtr.Size == 8) ? (IntPtr)BitConverter.ToInt64(pointerBuffer, 0) : (IntPtr)BitConverter.ToInt32(pointerBuffer, 0);

            int valueOffset;
            // Adjust the valueOffset based on the healthType
            switch (healthType)
            {
                case HealthType.MaxHealth:
                    valueOffset = MaxHealthOffset;
                    break;
                case HealthType.Stamina:
                    valueOffset = StaminaOffset;
                    break;
                default:
                case HealthType.CurrentHealth:
                    valueOffset = CurrentHealthOffset;
                    break;
            }

            IntPtr valueAddress = IntPtr.Add(valuePointer, valueOffset);
            byte[] valueBuffer = new byte[sizeof(short)];

            NativeMethods.ReadProcessMemory(processHandle, valueAddress, valueBuffer, (uint)valueBuffer.Length, out _);
            short currentValue = BitConverter.ToInt16(valueBuffer, 0);

            short newValue;
            if (setExactValue)
            {
                newValue = (short)Math.Max(0, Math.Min(value, short.MaxValue));
            }
            else
            {
                newValue = (short)Math.Max(0, Math.Min(currentValue + value, short.MaxValue));
            }

            byte[] newValueBuffer = BitConverter.GetBytes(newValue);
            NativeMethods.WriteProcessMemory(processHandle, valueAddress, newValueBuffer, (uint)newValueBuffer.Length, out _);

            NativeMethods.CloseHandle(processHandle);
        }

        internal static void ToggleSuppressor(Weapon suppressableWeapon)
        {
            Process process;

            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                MessageBox.Show($"Cannot find process: {Constants.PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            IntPtr suppressorAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)suppressableWeapon.SuppressorToggleOffset);
            if (!ReadWriteToggledSuppressorValue(processHandle, suppressorAddress))
            {
                MessageBox.Show("Failed to toggle suppressor.");
            }

            NativeMethods.CloseHandle(processHandle);
        }

        internal static void AdjustSuppressorCapacity(Item suppressorItem, bool increaseCapacity)
        {
            Process process = GetMGS3Process();

            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                MessageBox.Show($"Cannot find process: {Constants.PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            int bytesWritten;

            IntPtr suppressorCapacityAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)suppressorItem.MemoryOffset);
            short currentValue;
            try
            {
                currentValue = ReadShortFromMemory(processHandle, suppressorCapacityAddress);
            }
            catch
            {
                MessageBox.Show($"Unable to find {suppressorItem.Name} in memory.");
                return;
            }

            // Ensure the value stays within ushort bounds
            ushort newValue = (ushort)(increaseCapacity ? Math.Min((ushort)currentValue + 30, ushort.MaxValue) : Math.Max((ushort)currentValue - 30, ushort.MinValue));
            if (newValue == currentValue)
            {
                MessageBox.Show(increaseCapacity ? "Suppressor capacity is already at maximum." : "Suppressor capacity is already at minimum.");
            }
            else
            {
                try
                {
                    bytesWritten = WriteShortToMemory(processHandle, suppressorCapacityAddress, (short)newValue);
                    MessageBox.Show($"Suppressor capacity for {suppressorItem.Name} set to {newValue}.");
                }
                catch
                {
                    MessageBox.Show($"Unable to modify suppressor capacity for {suppressorItem.Name}.");
                    return;
                }
            }

            NativeMethods.CloseHandle(processHandle);
        }

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
                MessageBox.Show($"Cannot find process: {Constants.PROCESS_NAME}");
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

        // We just want to be checking the timer so we can trigger the alert again if needed
        private static byte ReadByteFromMemory(IntPtr processHandle, IntPtr address)
        {
            byte[] buffer = new byte[1];
            if (NativeMethods.ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out int bytesRead) && bytesRead == buffer.Length)
            {
                return buffer[0];
            }

            throw new IOException("Failed to read byte from memory.");
        }

        #endregion
    }
}
