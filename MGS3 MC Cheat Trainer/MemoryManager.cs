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

        #region Form Location Saving Functions

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

        #endregion

        #region DllImports

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

        #endregion

        #region Base Address and Process Handling

        // This gets the base address of the game from Constants.cs which is METAL GEAR SOLID3.exe
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

        #endregion

        #region Memory Reading

        // Expand this for documentation on how to utilize the read operations
        #region Read Operations Explanation 
        /*
        Usage looks like: byte value = MemoryManager.Instance.ReadByteFromMemory(processHandle, address);
        IntPtr processHandle is the game process and
        IntPtr address is the address to modify (Typically somewhere an AOB is pointing to)
        Using Alert triggering as an example the AOB is found like this:
        IntPtr alertMemoryRegion = memoryManager.FindAob("AlertMemoryRegion");
        Then hypothetically you'd go forward or backward using .Add or .Subtract to get to where you want to read like so:
        IntPtr modifyAddress = IntPtr.Add(alertMemoryRegion, 78); // We take the variable alertMemoryRegion and add 78 to it
        Then you'd read the byte like so:
        byte value = MemoryManager.Instance.ReadByteFromMemory(processHandle, modifyAddress);
        Then you can tie it into a button event to read the value and display it or log it like so:
        LoggingManager.Instance.Log($"Alert value: {value}");
        MessageBox.Show($"Alert value: {value}", "Alert Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
        This logic also remains the same for reading other data types like short, int, and float like so:
        short value = MemoryManager.Instance.ReadShortFromMemory(processHandle, modifyAddress);
        int value = MemoryManager.Instance.ReadMemoryBytes(processHandle, modifyAddress); // Can be used for anything larger than 2 bytes
        float value = MemoryManager.Instance.ReadFloatFromMemory(processHandle, modifyAddress);
        The only difference is the data type and the function used to read it.
        Calling the function in a form would remain the exact same as the byte example.
        */
        #endregion

        // Haven't had much usage for single byte reads, but it's here if needed.
        public static byte ReadByteFromMemory(IntPtr processHandle, IntPtr address)
        {
            byte[] buffer = new byte[1];
            if (NativeMethods.ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out int bytesRead) && bytesRead == buffer.Length)
            {
                return buffer[0];
            }

            throw new IOException("Failed to read byte from memory.");
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
            // return -1 is to ensure we don't crash the program if the read fails.
            // i.e. if you don't find a boss short value since you're not fighting that particular boss.
            // There's probably a better way to handle this but this works for now.
            return -1;
        }

        // Similar to reading a 4 byte value but this is if the data holds in a floating-point value.
        // To read floats in sequence you could do this:
        // float value1 = MemoryManager.Instance.ReadFloatFromMemory(processHandle, modifyAddress);
        // float value2 = MemoryManager.Instance.ReadFloatFromMemory(processHandle, IntPtr.Add(modifyAddress, 4));
        // float value3 = MemoryManager.Instance.ReadFloatFromMemory(processHandle, IntPtr.Add(modifyAddress, 8));
        // The (modifyAddress, 4) and (modifyAddress, 8) are the offsets from the original address.
        // This has only been useful for me for Xyz coordinates.
        public static float ReadFloatFromMemory(IntPtr processHandle, IntPtr address)
        {
            byte[] buffer = new byte[4];
            if (NativeMethods.ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out int bytesRead) && bytesRead == buffer.Length)
            {
                return BitConverter.ToSingle(buffer, 0);
            }

            return -1;
        }

        // Alternatively you could read all 3 floats at once like so:
        // float[] values = MemoryManager.Instance.ReadMemoryBytes(processHandle, modifyAddress, 12);

        public static byte[] ReadMemoryBytes(IntPtr processHandle, IntPtr address, int bytesToRead)
        {
            // bytesToRead is the number of bytes to read you'll since in single byte reads it's always 1
            // but this could also be used in 4 byte or 8 byte reads like so:
            // byte[] buffer = ReadMemoryBytes(processHandle, address, 4);
            // byte[] buffer = ReadMemoryBytes(processHandle, address, 8);
            byte[] buffer = new byte[bytesToRead];
            if (NativeMethods.ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _))
            {
                return buffer;
            }
            return null;
        }

        #endregion

        #region Memory Writing

        #region Write Operations Explanation
        /*
        Basic usage is as follows:
        For Bitwise operations like setting specific bits in a short value:
        short value = MemoryManager.SetSpecificBits(value, 0, 3, 1);
        Single byte writes: MemoryManager.WriteByteValueToMemory(address, value);
        Short writes: MemoryManager.WriteShortToMemory(address, value);
        Int writes: MemoryManager.WriteIntToMemory(processHandle, address, value);
        Float writes: MemoryManager.WriteFloatToMemory(processHandle, address, value);
        
        You can also use a read operation to validate the write like so:
        byte value = MemoryManager.ReadByteFromMemory(processHandle, address);
        if (value == 1)
        {
            LoggingManager.Instance.Log("Write successful.");
            // You can also use MessageBox.Show if you want to let the user know write was successful.
            // and to show what value was written. I wouldn't recommend doing this for every write
            // as many messageboxes can get annoying but it's useful for debugging.
            MessageBox.Show($"Write successful. Effect value is now: {value}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            LoggingManager.Instance.Log("Write failed.");
            // You can use MessageBox.Show if you want to let the user know write failed.
            MessageBox.Show("Write failed. At this effect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // This is also useful if you know probable actions the user might do you can advise them how to fix it.
        }
        
        Setting Bits isn't something I have a read for also so here is an explanation of how to use it:
        I made this since there was no native way to set an evasion status so I found a really roundabout way to do it
        What this function does is it takes a short value and sets specific bits in it
        To break it down: maskLength is the length of the mask, mask is the mask itself, and valueToSet is the value to set
        The function then takes the current value and sets the bits in the mask to the valueToSet
        This is useful for setting specific bits in a short value like the evasion status
        In AlertManager.cs SetEvasionBits is a full example of how to use this function
        for a short explanation this code snippet should help explain how to use it:
        short currentValue = MemoryManager.ReadShortFromMemory(processHandle, modifyAddress);
        short modifiedValue = SetSpecificBits(currentValue, 5, 14, 596); / 5 = start bit, 14 = end bit, and 596 = value to set
        MemoryManager.WriteShortToMemory(processHandle, modifyAddress, modifiedValue); 
        */
        #endregion
        public static short SetSpecificBits(short currentValue, int startBit, int endBit, int valueToSet)
        {
            int maskLength = endBit - startBit + 1;
            int mask = ((1 << maskLength) - 1) << startBit;
            return (short)((currentValue & ~mask) | ((valueToSet << startBit) & mask));
        }

        // Like the read byte method, haven't had much usage for single byte writes, but it's here if needed.
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

        // I think I made this write specifically for bosses to avoid crashes when writing to memory
        // Should look into it and see if these functions can be combined
        public static int WriteShortToMemory(IntPtr processHandle, IntPtr address, short value)
        {
            if (NativeMethods.WriteProcessMemory(processHandle, address, ref value, sizeof(short), out int bytesWritten))
                return bytesWritten;

            return -1;
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

        
        // If you have a float value you want to write to memory you can use this function like so:
        // MemoryManager.WriteFloatToMemory(processHandle, modifyAddress, value);

        // That being said sometimes you might have 3 or more float values you want to write in sequence
        // You can do that like so:
        // MemoryManager.WriteFloatToMemory(processHandle, modifyAddress, value1);
        // MemoryManager.WriteFloatToMemory(processHandle, IntPtr.Add(modifyAddress, 4), value2);
        // MemoryManager.WriteFloatToMemory(processHandle, IntPtr.Add(modifyAddress, 8), value3);
        // The (modifyAddress, 4) and (modifyAddress, 8) are the offsets from the original address.
        // For a more detailed explanation look as MiscForm.cs and XyzManager.cs
        
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


        #endregion

        #region Aob Scanning and Helpers

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

        public IntPtr FindDynamicAob(string key)
        {
            if (!AobManager.AOBs.TryGetValue(key, out var aobData))
            {
                return IntPtr.Zero;
            }

            var process = GetMGS3Process();
            if (process == null || process.MainModule == null)
            {

                return IntPtr.Zero;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {

                return IntPtr.Zero;
            }

            // If start and end addresses are defined, use them to calculate size. Otherwise, signal an error or default to full range scan.
            if (!aobData.StartOffset.HasValue || !aobData.EndOffset.HasValue)
            {

                NativeMethods.CloseHandle(processHandle);
                return IntPtr.Zero;
            }

            IntPtr startAddress = aobData.StartOffset.Value;
            long size = aobData.EndOffset.Value.ToInt64() - startAddress.ToInt64();

            // Use ScanWideMemory for scanning the specified dynamic memory range
            IntPtr foundAddress = ScanWideMemory(processHandle, startAddress, size, aobData.Pattern, aobData.Mask);

            NativeMethods.CloseHandle(processHandle);

            if (foundAddress == IntPtr.Zero)
            {

            }

            return foundAddress;
        }


        // Mask should be in the format "x x x x x x x" for no wildcards
        // or "x ? x ? ? ? x ? x" for wildcards with ? being the wildcard byte
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

        // string key is the key for the AOB in AobManager.cs
        // i.e. "ModelDistortion", "NotUpsideDownCamera", "WeaponsTable", "CamoOperations", etc. 
        public IntPtr FindAob(string key)
        {
            if (!AobManager.AOBs.TryGetValue(key, out var aobData))
            {

                return IntPtr.Zero;
            }

            var process = GetMGS3Process();
            if (process == null || process.MainModule == null)
            {

                return IntPtr.Zero;
            }

            IntPtr baseAddress = process.MainModule.BaseAddress;
            long moduleSize = process.MainModule.ModuleMemorySize;

            // Adjust baseAddress based on StartOffset
            IntPtr startAddress = aobData.StartOffset.HasValue ? IntPtr.Add(baseAddress, (int)aobData.StartOffset.GetValueOrDefault()) : baseAddress;

            // Calculate size based on EndOffset if provided; otherwise, use module size
            // Correctly handle arithmetic to prevent overflow
            long endAddress = aobData.EndOffset.HasValue ? IntPtr.Add(baseAddress, (int)aobData.EndOffset.GetValueOrDefault()).ToInt64() : baseAddress.ToInt64() + moduleSize;
            long size = endAddress - startAddress.ToInt64();

            IntPtr resultAddress = ScanMemory(process.Handle, startAddress, size, aobData.Pattern, aobData.Mask);

            if (resultAddress == IntPtr.Zero)
            {

            }

            return resultAddress;
        }

        public List<IntPtr> ScanForAllAobInstances(IntPtr processHandle, IntPtr baseAddress, long moduleSize, byte[] pattern, string mask)
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

        #endregion
    }
}