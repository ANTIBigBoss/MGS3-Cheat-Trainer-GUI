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
        private static Point lastFormLocation;

        public static MemoryManager Instance => _instance ?? (_instance = new MemoryManager());

        // This will save form location in memory for when a new form is opened so each form opens relative to the last form
        public static void LogFormLocation(Form form, string formName)
        {
            Point location = form.Location; // Get the passed form's location
            string message = $"Switching to {formName}. Current form location: X = {location.X}, Y = {location.Y}\n";
            LoggingManager.Instance.Log(message);
        }

        public static void UpdateLastFormLocation(Point location)
        {
            lastFormLocation = location;
        }

        // Method to get the last form location
        public static Point GetLastFormLocation()
        {
            return lastFormLocation;
        }

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

        public static Process GetMGS3Process()
        {
            Process? process = Process.GetProcessesByName(Constants.PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                // Just do nothing for now so the user isn't spammed with messages
            }
            return process;
        }

        public static IntPtr OpenGameProcess(Process process)
        {
            if (process == null)
            {
                return IntPtr.Zero;
            }

            IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            if (processHandle == IntPtr.Zero)
            {
            }

            return processHandle;
        }

        public static bool ReadProcessMemory(IntPtr processHandle, IntPtr address, byte[] buffer, uint size, out int bytesRead)
        {
            return NativeMethods.ReadProcessMemory(processHandle, address, buffer, size, out bytesRead);
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

        public static float ReadFloatFromMemory(IntPtr processHandle, IntPtr address)
        {
            byte[] buffer = new byte[4];
            if (NativeMethods.ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out int bytesRead) && bytesRead == buffer.Length)
            {
                return BitConverter.ToSingle(buffer, 0);
            }

            return -1;
        }

        public static bool WriteFloatToMemory(IntPtr processHandle, IntPtr address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            // Log attempt to write float value
            LoggingManager.Instance.Log($"Attempting to write float value {value} to address {address.ToString("X")}.");

            bool success = NativeMethods.WriteProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out int bytesWritten);

            if (!success)
            {
                int errorCode = Marshal.GetLastWin32Error();
                // Log failure with error code
                LoggingManager.Instance.Log($"Failed to write float value {value} to memory. Win32 Error Code: {errorCode}");
                return false;
            }
            if (bytesWritten != buffer.Length)
            {
                // Log partial write
                LoggingManager.Instance.Log($"Partial write. Attempted to write {buffer.Length} bytes, but only {bytesWritten} were written.");
                return false;
            }

            // Optional: Validate the write operation
            float writtenValue = ReadFloatFromMemory(processHandle, address);
            if (Math.Abs(writtenValue - value) > float.Epsilon)
            {
                // Log validation failure
                LoggingManager.Instance.Log($"Validation failed. Expected value {value}, but read back {writtenValue}.");
                return false;
            }

            // Log success
            LoggingManager.Instance.Log($"Successfully wrote float value {value} to address {address.ToString("X")}.");
            return true;
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

            return -1;
        }

        public static int WriteShortToMemory(IntPtr processHandle, IntPtr address, short value)
        {
            if (NativeMethods.WriteProcessMemory(processHandle, address, ref value, sizeof(short), out int bytesWritten))
                return bytesWritten;

            return -1;
        }

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
            // 64 KB buffer
            int bufferSize = 65536;
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

        // Only use this if you're searching through Dynamic memory it's sort of a last resort for AOBs
        public IntPtr ScanWideMemory(IntPtr processHandle, IntPtr startAddress, long size, byte[] pattern, string mask)
        {
            // 2000 KB buffer (I think this increases the speed of the scan) probably need a better
            // way to optimize for boss AOBs since the range is so large to find them
            int bufferSize = 10000000;
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

        // This currently seems it would work for wildcards but I don't think it does
        public bool IsMatch(byte[] buffer, int position, byte[] pattern, string mask)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                // If the mask at this position is a wildcard ('?'), or the bytes match, continue checking
                if (mask[i] == '?' || buffer[position + i] == pattern[i])
                {
                    continue;
                }
                // Mismatch found, return false immediately
                return false;
            }
            // All bytes matched
            return true;
        }

        

        // Should realy just make a master AOB function for this and setup an enum or something for the different AOBs
        public IntPtr FindAOBInWeaponAndItemTableRange(byte[] aobPattern, string mask)
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
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

                return IntPtr.Zero;
            }

            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1000000);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1E00000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr address = ScanMemory(process.Handle, startAddress, size, aobPattern, mask);
            if (address != IntPtr.Zero)
            {
                LoggingManager.Instance.Log($"Found AOB at address {address.ToString("X")}.");
                return address;
            }

            return IntPtr.Zero;
        }


        public IntPtr FindAOBIn1ERange(byte[] aobPattern, string mask)
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
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

                return IntPtr.Zero;
            }

            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1E00000);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1FF0000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr address = ScanMemory(process.Handle, startAddress, size, aobPattern, mask);
            if (address != IntPtr.Zero)
            {
                LoggingManager.Instance.Log($"Found AOB at address {address.ToString("X")}.");
                return address;
            }

            return IntPtr.Zero;
        }

        public void AdjustCamoIndex(int newValue)
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                // Ensure the game process is running and accessible
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return;

                // Hardcoded address for the camo index, assuming "METAL GEAR SOLID3.exe"+1DE58A4 is the base address + offset notation
                IntPtr baseAddress = process.MainModule.BaseAddress;
                IntPtr offset = new IntPtr(0x1DE58A4); // The offset you provided
                IntPtr targetAddress = IntPtr.Add(baseAddress, offset.ToInt32()); // Calculate the actual memory address

                // Write the new camo index value to the hardcoded address
                bool result = MemoryManager.Instance.WriteIntToMemory(processHandle, targetAddress, newValue);
                if (!result)
                {
                    // Log failure or handle it
                    LoggingManager.Instance.Log("Failed to write the new camo index value.");
                }
                else
                {
                    // Optionally log success
                    LoggingManager.Instance.Log($"Successfully wrote new camo index value: {newValue} at {targetAddress.ToString("X")}");
                }
            }
            finally
            {
                // Always close the process handle if it's not zero
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }


        /* Commenting out until a stable AOB is found for this
        public void AdjustCamoIndex(int newValue)
        {
            // AOB Pattern and Mask for the camo index
            byte[] aobPattern = Constants.AOBs["Alphabet"].Pattern;
            string mask = Constants.AOBs["Alphabet"].Mask;
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                // Ensure the game process is running and accessible
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return;

                // Find the AOB in the designated memory range
                IntPtr aobAddress = MemoryManager.Instance.FindAOBIn1ERange(aobPattern, mask);
                if (aobAddress == IntPtr.Zero) return; // If AOB not found, exit

                // Calculate the target address using the AOB address and the offset to the camo index
                int camoIndexOffset = 0x2ACE // 10958 in decimal
                    ; // Example offset in hex, replace with actual
                IntPtr targetAddress = IntPtr.Subtract(aobAddress, camoIndexOffset);

                // Write the new camo index value to the calculated address
                bool result = MemoryManager.Instance.WriteIntToMemory(processHandle, targetAddress, newValue);
                if (!result)
                {
                    // Log failure or handle it
                    LoggingManager.Instance.Log("Failed to write the new camo index value.");
                }
                else
                {
                    // Optionally log success
                    LoggingManager.Instance.Log($"Successfully wrote new camo index value: {newValue} at {targetAddress.ToString("X")}");
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }
        */

        public string ReadValueAtAddressInHexAndDecimal(IntPtr address)
        {
            IntPtr processHandle = IntPtr.Zero;
            string result = string.Empty;

            try
            {
                // Ensure the game process is running and accessible
                Process process = MemoryManager.GetMGS3Process();
                if (process == null)
                {
                    return "Process not found.";
                }

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero)
                {
                    return "Failed to open process.";
                }

                // Use the ReadIntFromMemory method to read the value at the target address
                int value = MemoryManager.Instance.ReadIntFromMemory(processHandle, address);

                // Convert the value to hexadecimal format
                string hexValue = value.ToString("X");

                // Log and return the value in both hexadecimal and decimal formats
                result = $"Value at address {address.ToString("X")}: Hex = {hexValue}, Decimal = {value}";
                LoggingManager.Instance.Log(result);
            }
            catch (Exception ex)
            {
                result = $"Error reading memory: {ex.Message}";
                LoggingManager.Instance.Log(result);
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
            }

            return result;
        }

        public string GetCamoIndexReadout()
        {
            IntPtr aobAddress = FindAOBIn1ERange(Constants.AOBs["ItemsTable"].Pattern, Constants.AOBs["ItemsTable"].Mask);
            if (aobAddress == IntPtr.Zero)
            {
                return "AOB not found.";
            }

            int camoIndexOffset = 0x89AE; // Example offset, adjust as needed
            IntPtr targetAddress = IntPtr.Add(aobAddress, camoIndexOffset);
            return ReadValueAtAddressInHexAndDecimal(targetAddress);
        }

        public int ReadIntFromMemory(IntPtr processHandle, IntPtr address)
        {
            int bytesRead;
            byte[] buffer = new byte[4]; // 4 bytes for an int
            bool success = NativeMethods.ReadProcessMemory(processHandle, address, buffer, 4, out bytesRead);
            if (success && bytesRead == 4)
            {
                return BitConverter.ToInt32(buffer, 0); // Convert bytes to int
            }
            else
            {
                throw new Exception("Failed to read memory.");
            }
        }


        public bool WriteIntToMemory(IntPtr processHandle, IntPtr address, int value)
        {
            // Convert int to byte array
            byte[] bytes = BitConverter.GetBytes(value);

            // Call WriteProcessMemory
            return NativeMethods.WriteProcessMemory(processHandle, address, bytes, (uint)bytes.Length, out _);
        }

        public void WriteShortToMemory(IntPtr address, short value)
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                return;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {

                return;
            }

            bool success = NativeMethods.WriteProcessMemory(processHandle, address, ref value, sizeof(short), out int bytesWritten);
            if (!success || bytesWritten != sizeof(short))
            {

            }

            NativeMethods.CloseHandle(processHandle);
        }

        public static void WriteByteValueToMemory(IntPtr address, byte value)
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                return;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {

                return;
            }

            bool success = NativeMethods.WriteProcessMemory(processHandle, address, new byte[] { value }, 1, out int bytesWritten);
            if (!success || bytesWritten != 1)
            {

            }

            NativeMethods.CloseHandle(processHandle);
        }

        // AOB pattern located at Constant.cs name is "CamoOperations" for aobPattern and mask
        public IntPtr FindCamoIndexMemoryRegion(byte[] aobPattern, string mask)
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                return IntPtr.Zero;
            }

            IntPtr baseAddress = IntPtr.Zero;
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName == "METAL GEAR SOLID3.exe")
                {
                    baseAddress = module.BaseAddress;
                    break;
                }
            }

            if (baseAddress == IntPtr.Zero)
            {

                return IntPtr.Zero;
            }

            IntPtr startAddress = IntPtr.Add(baseAddress, 0x100000);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x430000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr address = ScanMemory(process.Handle, startAddress, size, aobPattern, mask);
            if (address != IntPtr.Zero)
            {
                return address;
            }


            return IntPtr.Zero;
        }

        public static bool NOPCamoIndex(IntPtr processHandle, IntPtr address)
        {
            IntPtr targetAddress = IntPtr.Subtract(address, 4);
            byte[] nopBytes = new byte[] { 0x90, 0x90, 0x90, 0x90 };
            return NativeMethods.WriteProcessMemory(processHandle, targetAddress, nopBytes, (uint)nopBytes.Length, out int bytesWritten) && bytesWritten == nopBytes.Length;
        }

        public static bool RestoreCamoIndex(IntPtr processHandle, IntPtr address)
        {
            IntPtr targetAddress = IntPtr.Subtract(address, 4);
            byte[] restoreBytes = new byte[] { 0x89, 0x44, 0x2B, 0x24 };
            return NativeMethods.WriteProcessMemory(processHandle, targetAddress, restoreBytes, (uint)restoreBytes.Length, out int bytesWritten) && bytesWritten == restoreBytes.Length;
        }

        public void EnableNOPCamoIndex()
        {
            Process process = MemoryManager.GetMGS3Process(); // Correctly using class name for static method
            if (process == null) return;

            IntPtr processHandle = MemoryManager.OpenGameProcess(process); // Correctly using class name for static method
            if (processHandle == IntPtr.Zero) return;

            // Correctly accessing pattern and mask
            var aob = Constants.AOBs["CamoOperations"];
            IntPtr camoIndexAddress = this.FindCamoIndexMemoryRegion(aob.Pattern, aob.Mask);
            if (camoIndexAddress != IntPtr.Zero)
            {
                MemoryManager.NOPCamoIndex(processHandle, camoIndexAddress);
            }

            NativeMethods.CloseHandle(processHandle);
        }

        // Method to restore the camo index
        public static void RestoreCamoIndex()
        {
            Process process = MemoryManager.GetMGS3Process(); // Correctly using class name for static method
            if (process == null) return;

            IntPtr processHandle = MemoryManager.OpenGameProcess(process); // Correctly using class name for static method
            if (processHandle == IntPtr.Zero) return;

            // Correctly accessing pattern and mask
            var aob = Constants.AOBs["CamoOperations"];
            IntPtr camoIndexAddress = MemoryManager.Instance.FindCamoIndexMemoryRegion(aob.Pattern, aob.Mask);
            if (camoIndexAddress != IntPtr.Zero)
            {
                MemoryManager.RestoreCamoIndex(processHandle, camoIndexAddress);
            }

            NativeMethods.CloseHandle(processHandle);
        }

        public static byte[] ReadOriginalBytesBeforeAOB(IntPtr processHandle, IntPtr address)
        {
            // Address of the 4 bytes before the AOB pattern
            IntPtr targetAddress = IntPtr.Subtract(address, 4);
            byte[] buffer = new byte[4]; // To hold the original 4-byte value
            if (NativeMethods.ReadProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out int bytesRead) && bytesRead == buffer.Length)
            {
                return buffer;
            }
            else
            {
                throw new IOException("Failed to read original bytes before AOB.");
            }
        }

        public string Read4BytesBeforeCamoAOB()
        {
            try
            {
                var process = GetMGS3Process();
                if (process == null)
                {
                    return "Process not found.";
                }

                IntPtr processHandle = OpenGameProcess(process);
                if (processHandle == IntPtr.Zero)
                {
                    return "Failed to open process.";
                }

                var (pattern, mask) = Constants.AOBs["CamoOperations"];
                IntPtr camoIndexAddress = FindCamoIndexMemoryRegion(pattern, mask);
                if (camoIndexAddress == IntPtr.Zero)
                {
                    NativeMethods.CloseHandle(processHandle);
                    return "Camo Index AOB not found.";
                }

                byte[] originalBytes = ReadOriginalBytesBeforeAOB(processHandle, camoIndexAddress);
                NativeMethods.CloseHandle(processHandle);

                // Reverse the array to match Cheat Engine's display order
                Array.Reverse(originalBytes);

                // Convert the read bytes into a hexadecimal string format
                return BitConverter.ToString(originalBytes).Replace("-", " ");
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // This is mostly to help me stay ahead of game updates so I can log the offset differences quicker if anything gets shifted around
        public void LogAOBAddresses()
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("Process not found.");
                return;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process.");
                return;
            }

            IntPtr baseAddress = process.MainModule.BaseAddress;
            long moduleSize = process.MainModule.ModuleMemorySize;

            foreach (var aobEntry in Constants.AOBs)
            {
                string name = aobEntry.Key;
                if (string.IsNullOrEmpty(name)) continue; // Skip empty placeholder entries

                byte[] pattern = aobEntry.Value.Pattern;
                string mask = aobEntry.Value.Mask;

                var foundAddresses = ScanForAllAobInstances(processHandle, baseAddress, moduleSize, pattern, mask);
                if (foundAddresses.Count > 0)
                {
                    foreach (var address in foundAddresses)
                    {
                        long offset = address.ToInt64() - baseAddress.ToInt64();
                        // Read the AOB bytes from the found address
                        byte[] aobBytes = new byte[pattern.Length];
                        if (NativeMethods.ReadProcessMemory(processHandle, address, aobBytes, (uint)aobBytes.Length, out _))
                        {
                            string aobHexString = BitConverter.ToString(aobBytes).Replace("-", " ");
                            LoggingManager.Instance.Log($"{name}: Instance found at: {address.ToString("X")}, METAL GEAR SOLID3.exe+{offset:X}, AOB: {aobHexString}");
                        }
                        else
                        {
                            LoggingManager.Instance.Log($"{name}: Instance found at: {address.ToString("X")}, METAL GEAR SOLID3.exe+{offset:X}, but failed to read AOB bytes.");
                        }
                    }
                }
                else
                {
                    LoggingManager.Instance.Log($"{name}: AOB not found.");
                }
            }

            NativeMethods.CloseHandle(processHandle);
        }

        private List<IntPtr> ScanForAllAobInstances(IntPtr processHandle, IntPtr baseAddress, long moduleSize, byte[] pattern, string mask)
        {
            List<IntPtr> foundAddresses = new List<IntPtr>();
            IntPtr endAddress = IntPtr.Add(baseAddress, (int)moduleSize);
            int patternLength = pattern.Length;
            byte[] searchBuffer = new byte[65536]; // Example buffer size, adjust based on your needs
            long currentPosition = baseAddress.ToInt64();

            while (currentPosition < endAddress.ToInt64())
            {
                if (!NativeMethods.ReadProcessMemory(processHandle, new IntPtr(currentPosition), searchBuffer, (uint)searchBuffer.Length, out int bytesRead) || bytesRead == 0)
                {
                    break; // Exit if read fails or reads zero bytes
                }

                for (int i = 0; i <= bytesRead - patternLength; i++)
                {
                    if (IsMatch(searchBuffer, i, pattern, mask))
                    {
                        IntPtr foundAddress = new IntPtr(currentPosition + i);
                        foundAddresses.Add(foundAddress);
                        i += patternLength - 1; // Move past this match to avoid overlapping finds
                    }
                }

                currentPosition += bytesRead - patternLength + 1; // Move window, avoiding missing overlaps
            }

            return foundAddresses;
        }

        public IntPtr FoundSnakePositionAddress { get; private set; } = IntPtr.Zero;

        public bool FindAndStoreSnakesPositionAOB()
        {
            var process = GetMGS3Process();
            if (process == null)
            {

                return false;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {

                return false;
            }

            // Corrected access to tuple elements: .Pattern and .Mask
            var patternStanding = Constants.AOBs["SnakeAndBossesStanding"].Pattern;
            var maskStanding = Constants.AOBs["SnakeAndBossesStanding"].Mask;

            var patternProne = Constants.AOBs["SnakeAndGuardProne"].Pattern;
            var maskProne = Constants.AOBs["SnakeAndGuardProne"].Mask;

            IntPtr startAddress = new IntPtr(0x100FFFFFFFF); // Example start range
            IntPtr endAddress = new IntPtr(0x30000000000); // Example end range
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            // Search for the first pattern
            IntPtr foundAddressStanding = ScanWideMemory(processHandle, startAddress, size, patternStanding, maskStanding);
            if (foundAddressStanding != IntPtr.Zero)
            {
                FoundSnakePositionAddress = foundAddressStanding;
                NativeMethods.CloseHandle(processHandle);
                return true;
            }

            // If the first pattern wasn't found, search for the second pattern
            IntPtr foundAddressProne = ScanWideMemory(processHandle, startAddress, size, patternProne, maskProne);
            if (foundAddressProne != IntPtr.Zero)
            {
                FoundSnakePositionAddress = foundAddressProne;
                NativeMethods.CloseHandle(processHandle);
                return true;
            }

            NativeMethods.CloseHandle(processHandle);
            return false;
        }

        public float[] ReadSnakePosition(IntPtr processHandle)
        {
            IntPtr baseAddress = IntPtr.Add(FoundSnakePositionAddress, 7);

            float[] position = new float[3];
            for (int i = 0; i < position.Length; i++)
            {
                IntPtr currentAddress = IntPtr.Add(baseAddress, i * 4); // 4 bytes per float
                position[i] = ReadFloatFromMemory(processHandle, currentAddress);
            }

            return position;
        }

        public void TeleportSnake(float[][] coordinates)
        {
            if (FoundSnakePositionAddress == IntPtr.Zero && !FindAndStoreSnakesPositionAOB())
            {
                LoggingManager.Instance.Log("Failed to find or verify Snake's position AOB.");
                return;
            }

            IntPtr processHandle = OpenGameProcess(GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process handle.");
                return;
            }

            foreach (var set in coordinates)
            {
                float x = set[0];
                float y = set[1];
                float z = set[2];
                // Execute the teleport to new position using the current set of coordinates
                // Note: Implement the TeleportToPosition method as previously described
                TeleportToPosition(processHandle, x, y, z);
            }

            NativeMethods.CloseHandle(processHandle);
        }


        private void TeleportToPosition(IntPtr processHandle, float x, float y, float z)
        {
            float[] newPosition = new float[] { x, y, z };
            for (int i = 0; i < newPosition.Length; i++)
            {
                IntPtr currentAddress = IntPtr.Add(FoundSnakePositionAddress, 7 + i * 4); // Adjust the offset as necessary
                bool success = WriteFloatToMemory(processHandle, currentAddress, newPosition[i]);
                if (!success)
                {
                    LoggingManager.Instance.Log($"Failed to teleport Snake to new position: X={x}, Y={y}, Z={z}.");
                    break; // Exit if any teleport fails
                }
            }
        }

        public void RaiseSnakeYBy4000()
        {
            if (FoundSnakePositionAddress == IntPtr.Zero && !FindAndStoreSnakesPositionAOB())
            {
                LoggingManager.Instance.Log("Failed to find or verify Snake's position AOB.");
                return;
            }

            IntPtr processHandle = OpenGameProcess(GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process handle.");
                return;
            }

            // Read current position
            float[] currentPosition = ReadSnakePosition(processHandle);
            if (currentPosition == null || currentPosition.Length < 3)
            {
                LoggingManager.Instance.Log("Failed to read Snake's current position.");
                NativeMethods.CloseHandle(processHandle);
                return;
            }

            // Increase Y by 4000 units
            float newY = currentPosition[1] + 4000f;

            // Prepare the address for Y coordinate
            IntPtr yAddress = IntPtr.Add(FoundSnakePositionAddress, 7 + 4); // Assuming Y is the second float after the AOB pattern

            // Write the new Y position
            bool success = WriteFloatToMemory(processHandle, yAddress, newY);
            if (success)
            {
                LoggingManager.Instance.Log($"Successfully raised Snake's Y position by 4000 units to {newY}.");
            }
            else
            {
                LoggingManager.Instance.Log("Failed to update Snake's Y position.");
            }

            NativeMethods.CloseHandle(processHandle);
        }


        public List<IntPtr> ScanForAllInstances(IntPtr processHandle, IntPtr startAddress, long size, byte[] pattern, string mask)
        {
            List<IntPtr> foundAddresses = new List<IntPtr>();
            int bufferSize = 10000000;
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            long endAddress = startAddress.ToInt64() + size;
            for (long address = startAddress.ToInt64(); address < endAddress; address += bufferSize)
            {
                int effectiveSize = (int)Math.Min(bufferSize, endAddress - address);
                bool success = ReadProcessMemory(processHandle, new IntPtr(address), buffer, (uint)effectiveSize, out bytesRead);
                if (!success || bytesRead == 0) continue;

                for (int i = 0; i <= bytesRead - pattern.Length; i++)
                {
                    if (IsMatch(buffer, i, pattern, mask))
                    {
                        foundAddresses.Add(new IntPtr(address + i));
                        i += pattern.Length - 1;
                    }
                }
            }

            return foundAddresses;
        }

        public List<IntPtr> FindAllGuardsPositionAOBs()
        {
            var process = GetMGS3Process();
            if (process == null)
            {

                return new List<IntPtr>();
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {

                return new List<IntPtr>();
            }

            var (pattern, mask) = Constants.AOBs["GuardPatroling"];
            IntPtr startAddress = new IntPtr(0x100FFFFFFFF); // Adjust as necessary
            IntPtr endAddress = new IntPtr(0x30000000000); // Adjust as necessary
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            var guardsAddresses = ScanForAllInstances(processHandle, startAddress, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            return guardsAddresses;
        }

        public List<float[]> ReadGuardsPositions(IntPtr processHandle, List<IntPtr> guardsAddresses)
        {
            List<float[]> guardsPositions = new List<float[]>();
            foreach (IntPtr baseAddress in guardsAddresses)
            {
                float[] position = new float[3];
                for (int i = 0; i < position.Length; i++)
                {
                    IntPtr currentAddress = IntPtr.Add(baseAddress, 7 + i * 4); // Assuming the same offset and structure
                    position[i] = ReadFloatFromMemory(processHandle, currentAddress);
                }
                guardsPositions.Add(position);
            }
            return guardsPositions;
        }       

        public void MoveGuardsToPosition(IntPtr processHandle, List<IntPtr> guardsAddresses, float[] newPosition)
        {
            foreach (IntPtr guardBaseAddress in guardsAddresses)
            {
                // Calculate the correct address offsets for X, Y, Z positions
                for (int i = 0; i < newPosition.Length; i++)
                {
                    IntPtr currentAddress = IntPtr.Add(guardBaseAddress, 7 + i * 4);
                    bool success = WriteFloatToMemory(processHandle, currentAddress, newPosition[i]);
                    if (!success)
                    {

                    }
                }
            }
        }

        public void MoveAllGuardsToSnake()
        {
            IntPtr processHandle = OpenGameProcess(GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {

                return;
            }

            FindAndStoreSnakesPositionAOB();
            if (FoundSnakePositionAddress == IntPtr.Zero)
            {

                NativeMethods.CloseHandle(processHandle);
                return;
            }

            float[] snakePosition = ReadSnakePosition(processHandle);
            var guardsAddresses = FindAllGuardsPositionAOBs();
            if (guardsAddresses.Count > 0)
            {
                MoveGuardsToPosition(processHandle, guardsAddresses, snakePosition);

            }
            else
            {

            }

            NativeMethods.CloseHandle(processHandle);
        }

        public string FindLocationStringDirectlyInRange()
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                return "Game process not found.";
            }

            IntPtr processHandle = OpenGameProcess(process);
            IntPtr baseAddress = process.MainModule.BaseAddress;
            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1C00000);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1D00000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            foreach (StringManager.LocationString location in Enum.GetValues(typeof(StringManager.LocationString)))
            {
                var locationString = location.ToString();
                byte[] pattern = Encoding.ASCII.GetBytes(locationString);
                string mask = new string('x', pattern.Length);

                IntPtr foundAddress = ScanMemory(processHandle, startAddress, size, pattern, mask);
                if (foundAddress != IntPtr.Zero)
                {
                    string areaName = StringManager.LocationAreaNames.TryGetValue(location, out var name) ? name : "Unknown Area";

                    // Checking for cutscene indicators
                    foreach (var suffix in new[] { "_0", "_1" })
                    {
                        byte[] cutscenePattern = Encoding.ASCII.GetBytes(locationString + suffix);
                        IntPtr cutsceneFoundAddress = ScanMemory(processHandle, startAddress, size, cutscenePattern, mask + "x" + "x");

                        if (cutsceneFoundAddress != IntPtr.Zero)
                        {
                            NativeMethods.CloseHandle(processHandle);
                            return $"Location String: {locationString}{suffix} (Cutscene) \nArea Name: {areaName} \nMemory Address: {cutsceneFoundAddress.ToString("X")}";
                        }
                    }

                    NativeMethods.CloseHandle(processHandle);
                    return $"Location String: {locationString} \nArea Name: {areaName} \nMemory Address: {foundAddress.ToString("X")}";
                }
            }

            NativeMethods.CloseHandle(processHandle);
            return "No Location String found in specified range.";
        }

        /* Same as the above function but only to find the location string it 
           currently is only used in the BossForm use the player's area to 
           know which boss they can tamper with for the UI elements */
        public string ExtractLocationStringFromResult(string result)
        {
            string[] parts = result.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                string locationStringPart = parts[0];
                string[] locationStringParts = locationStringPart.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (locationStringParts.Length > 1)
                {
                    string locationString = locationStringParts[1].Trim();

                    if (locationString.EndsWith("_0") || locationString.EndsWith("_1"))
                    {
                        return locationString + " (Cutscene)";
                    }
                    else
                    {
                        return locationString;
                    }
                }
            }
            return "Unknown";
        }

        public IntPtr ReadIntPtr(IntPtr processHandle, IntPtr address)
        {
            byte[] buffer = new byte[8]; // Correct for 64-bit
            if (ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _))
            {
                return (IntPtr)BitConverter.ToInt64(buffer, 0);
            }
            return IntPtr.Zero;
        }

    }
}