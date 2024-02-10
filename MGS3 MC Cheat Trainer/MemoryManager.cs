using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using static MGS3_MC_Cheat_Trainer.Constants;
using System.Threading.Tasks;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public class MemoryManager
    {
        private static MemoryManager _instance;
        public static MemoryManager Instance => _instance ?? (_instance = new MemoryManager());
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

        //static IntPtr PROCESS_BASE_ADDRESS = IntPtr.Zero;
        public static IntPtr PROCESS_BASE_ADDRESS = IntPtr.Zero;


        /* This might need a rework to account for if the game is close while a checkbox is checked 
        since it'll keep trying to find the process and throw an exception */
        public static Process GetMGS3Process()
        {
            Process? process = Process.GetProcessesByName(Constants.PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                throw new NullReferenceException();
            }
            return process;
        }

        public static void ModifyByteValueObject(IntPtr objectOffset, byte value)
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
            IntPtr targetAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)objectOffset); // Adjusted to add base address

            bool success = NativeMethods.WriteProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out bytesWritten);

            if (!success || bytesWritten != buffer.Length)
            {
                MessageBox.Show($"Failed to write memory with value {value}.");
            }

            NativeMethods.CloseHandle(processHandle);
        }

        public static bool ReadWriteToggledSuppressorValue(IntPtr processHandle, IntPtr address)
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

        public static short ReadShortFromMemory(IntPtr processHandle, IntPtr address)
        {
            if (NativeMethods.ReadProcessMemory(processHandle, address, out short value, sizeof(short), out int bytesRead))
            {
                if (bytesRead == sizeof(short))
                {
                    return value;
                }
            }

            throw new InvalidOperationException("Failed to read short from memory.");
        }

        public static int WriteShortToMemory(IntPtr processHandle, IntPtr address, short value)
        {
            if (NativeMethods.WriteProcessMemory(processHandle, address, ref value, sizeof(short), out int bytesWritten))
                return bytesWritten;

            throw new IOException();
        }

        // We just want to be checking the timer so we can trigger the alert again if needed
        public static byte ReadByteFromMemory(IntPtr processHandle, IntPtr address)
        {
            byte[] buffer = new byte[1];
            if (NativeMethods.ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out int bytesRead) && bytesRead == buffer.Length)
            {
                return buffer[0];
            }

            throw new IOException("Failed to read byte from memory.");
        }

        public static short SetSpecificBits(short currentValue, int startBit, int endBit, int valueToSet)
        {
            int maskLength = endBit - startBit + 1;
            int mask = ((1 << maskLength) - 1) << startBit;
            return (short)((currentValue & ~mask) | ((valueToSet << startBit) & mask));
        }

        public static byte[] ReadMemoryBytes(IntPtr processHandle, IntPtr address, int bytesToRead)
        {
            byte[] buffer = new byte[bytesToRead];
            if (NativeMethods.ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _))
            {
                return buffer;
            }
            return null;
        }

        public IntPtr ScanMemory(IntPtr processHandle, IntPtr startAddress, long size, byte[] pattern, string mask)
        {
            int bufferSize = 131072; // 128 KB buffer
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            long endAddress = startAddress.ToInt64() + size;
            for (long address = startAddress.ToInt64(); address < endAddress; address += bufferSize)
            {
                int effectiveSize = (int)Math.Min(bufferSize, endAddress - address);
                bool success = ReadProcessMemory(processHandle, new IntPtr(address), buffer, (uint)effectiveSize, out bytesRead);
                if (!success || bytesRead == 0)
                {
                    continue;
                }

                for (int i = 0; i < bytesRead - pattern.Length; i++)
                {
                    if (IsMatch(buffer, i, pattern, mask))
                    {
                        return new IntPtr(address + i);
                    }
                }
            }

            return IntPtr.Zero;
        }

        public bool IsMatch(byte[] buffer, int position, byte[] pattern, string mask)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                if (mask[i] == '?' || buffer[position + i] == pattern[i])
                {
                    continue;
                }
                return false;
            }
            return true;
        }

        public static IntPtr OpenGameProcess(Process process)
        {
            if (process == null)
            {
                MessageBox.Show("Game process not found.");
                return IntPtr.Zero;
            }

            IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            if (processHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open game process.");
            }

            return processHandle;
        }

        // Implement ReadProcessMemory method
        public static bool ReadProcessMemory(IntPtr processHandle, IntPtr address, byte[] buffer, uint size, out int bytesRead)
        {
            return NativeMethods.ReadProcessMemory(processHandle, address, buffer, size, out bytesRead);
        }

        public IntPtr FindAOBInWeaponAndItemTableRange(byte[] aobPattern, string mask)
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                MessageBox.Show("Game process not found.");
                return IntPtr.Zero;
            }

            IntPtr baseAddress = IntPtr.Zero;
            int moduleSize = 0;
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName == "METAL GEAR SOLID3.exe")
                {
                    baseAddress = module.BaseAddress;
                    moduleSize = module.ModuleMemorySize;
                    break;
                }
            }

            if (baseAddress == IntPtr.Zero)
            {
                MessageBox.Show("METAL GEAR SOLID3.exe module not found.");
                return IntPtr.Zero;
            }

            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1000000);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1E00000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr address = ScanMemory(process.Handle, startAddress, size, aobPattern, mask);
            if (address != IntPtr.Zero)
            {
                return address;
            }

            MessageBox.Show("Pattern not found in specified range.");
            return IntPtr.Zero;
        }

        public IntPtr FindAOBInCameraRange(byte[] aobPattern, string mask)
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                MessageBox.Show("Game process not found.");
                return IntPtr.Zero;
            }

            IntPtr baseAddress = IntPtr.Zero;
            int moduleSize = 0;
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName == "METAL GEAR SOLID3.exe")
                {
                    baseAddress = module.BaseAddress;
                    moduleSize = module.ModuleMemorySize;
                    break;
                }
            }

            if (baseAddress == IntPtr.Zero)
            {
                MessageBox.Show("METAL GEAR SOLID3.exe module not found.");
                return IntPtr.Zero;
            }

            IntPtr startAddress = IntPtr.Add(baseAddress, 0x0FFFFF); // Adjusted start address
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1000000); // Adjusted end address
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr address = ScanMemory(process.Handle, startAddress, size, aobPattern, mask);
            if (address != IntPtr.Zero)
            {
                return address;
            }

            MessageBox.Show("Pattern not found in specified range.");
            return IntPtr.Zero;
        }

        public void WriteShortToMemory(IntPtr address, short value)
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                MessageBox.Show("Game process not found.");
                return;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open process for writing.");
                return;
            }

            bool success = NativeMethods.WriteProcessMemory(processHandle, address, ref value, sizeof(short), out int bytesWritten);
            if (!success || bytesWritten != sizeof(short))
            {
                MessageBox.Show("Failed to write short value to memory.");
            }

            NativeMethods.CloseHandle(processHandle);
        }

        public void WriteByteValueToMemory(IntPtr address, byte value)
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                MessageBox.Show("Game process not found.");
                return;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open process for writing.");
                return;
            }

            bool success = NativeMethods.WriteProcessMemory(processHandle, address, new byte[] { value }, 1, out int bytesWritten);
            if (!success || bytesWritten != 1)
            {
                MessageBox.Show("Failed to write byte value to memory.");
            }

            NativeMethods.CloseHandle(processHandle);
        }

        public IntPtr FindAlertMemoryRegion(byte[] aobPattern, string mask)
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                MessageBox.Show("Game process not found.");
                return IntPtr.Zero;
            }

            IntPtr baseAddress = IntPtr.Zero;
            int moduleSize = 0;
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName == "METAL GEAR SOLID3.exe")
                {
                    baseAddress = module.BaseAddress;
                    moduleSize = module.ModuleMemorySize;
                    break;
                }
            }

            if (baseAddress == IntPtr.Zero)
            {
                MessageBox.Show("METAL GEAR SOLID3.exe module not found.");
                return IntPtr.Zero;
            }

            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1000000);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1FF0000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr address = ScanMemory(process.Handle, startAddress, size, aobPattern, mask);
            if (address != IntPtr.Zero)
            {
                return address;
            }

            MessageBox.Show("Pattern not found in specified range.");
            return IntPtr.Zero;
        }
        
        public IntPtr FoundTheFearAddress { get; private set; } = IntPtr.Zero;

        public bool FindAndStoreTheFearAOB()
        {
            var process = GetMGS3Process();
            if (process == null)
            {
                MessageBox.Show("MGS3 process not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open process for scanning.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var (pattern, mask) = Constants.AOBs["TheFearAOB"];
            IntPtr startAddress = new IntPtr(0x22AFFFFFFFF); // Example start range
            IntPtr endAddress = new IntPtr(0x23B00000000); // Example end range
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr foundAddress = ScanMemory(processHandle, startAddress, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            if (foundAddress != IntPtr.Zero)
            {
                FoundTheFearAddress = foundAddress; // Store found address
                return true;
            }

            return false;
        }

    }
}