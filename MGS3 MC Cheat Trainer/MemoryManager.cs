using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static MGS3_MC_Cheat_Trainer.ItemForm;

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
        private const int HealthPointerOffset = 0x00AE49D8;
        private const int HealthOffset = 0x684;
        private const int MaxHealthOffset = 0x686;
        private const int StaminaOffset = 0xA4A;

        #region Private Functions
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

            
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
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

        private static void ToggleObjectState(IntPtr objectOffset, bool enableObject)
        {
            string stateValue = enableObject ? "1" : "-1";
            ModifyShortValueObject(objectOffset, stateValue);
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
        #endregion

        #region Internal Functions
        internal static void ChangeAlertMode(byte alertMode)
        {
            ModifyByteValueObject(Constants.AlertStatus.AlertStatusOffset, alertMode);
        }

        internal static void TriggerSnakeAnimation(byte animation)
        {
            ModifyByteValueObject(Constants.MGS3SnakeAnimations.LongSleep.AnimationOffset, animation); //TODO: fix this, this is not complete/correct
        }

        internal static void ToggleWeapon(Weapon weapon, bool enable)
        {
            ToggleObjectState(weapon.MemoryOffset, enable);
        }

        internal static void ToggleItemState(Item item, bool enable)
        {
            ToggleObjectState(item.MemoryOffset, enable);
        }

        internal static void ModifyItemCapacity(Item item, string itemCountStr)
        {
            if(item.MaxCapacityOffset != default)
                ModifyShortObjectWithMaxCount(item.MemoryOffset, item.MaxCapacityOffset, itemCountStr);
            else
                ModifyShortValueObject(item.MemoryOffset, itemCountStr);
        }

        internal static void ModifyClipSize(ClippedWeapon weapon, string clipSize)
        {
            ModifyShortObjectWithMaxCount(weapon.ClipOffset, weapon.MaxClipOffset, clipSize);
        }

        internal static void ModifyAmmo(AmmoWeapon weapon, string ammoCount)
        {
            ModifyShortObjectWithMaxCount(weapon.MemoryOffset, weapon.MaxAmmoOffset, ammoCount);
        }
        internal static void ToggleSuppressor(SuppressableWeapon suppressableWeapon)
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

        internal static void AdjustSuppressorCapacity(SuppressableWeapon suppressableWeapon, bool increaseCapacity)
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

            IntPtr suppressorCapacityAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)suppressableWeapon.SuppressorCapacityOffset);
            short currentValue;
            try
            {
                currentValue = ReadShortFromMemory(processHandle, suppressorCapacityAddress);
            }
            catch
            {
                MessageBox.Show($"Unable to find {suppressableWeapon.Name} in memory.");
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
                    MessageBox.Show($"Suppressor capacity for {suppressableWeapon.Name} set to {newValue}.");
                }
                catch
                {
                    MessageBox.Show($"Unable to modify suppressor capacity for {suppressableWeapon.Name}.");
                    return;
                }
            }

            NativeMethods.CloseHandle(processHandle);
        }
        #endregion
    }
}
