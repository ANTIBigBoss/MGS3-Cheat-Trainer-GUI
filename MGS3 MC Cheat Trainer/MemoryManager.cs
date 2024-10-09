using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using static MGS3_MC_Cheat_Trainer.Constants;

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

        // Check if the game is running before starting the application if the game isn't running then close the application
        public void TrainerLooksForGame(object sender, EventArgs e)
        {
            var process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("Game process not found. Prompting user to start the game.\n");
                CustomMessageBoxManager.CustomMessageBox("Game process not found. \nIf the game is running please reach out to us on Discord and open a ticket", "Error");
                LoggingManager.Instance.Log("Closing application.\n");
                Application.Exit();
            }
            else
            {
                LoggingManager.Instance.Log("Game process found. Starting logging for this session.\nApplication started successfully.\n");
            }
        }

        public static IntPtr OpenGameProcess(Process process)
        {
            try
            {
                if (process == null)
                {
                    throw new InvalidOperationException("Process not found.");
                }

                IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
                if (processHandle == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Failed to open process.");
                }

                return processHandle;
            }
            catch (Exception ex)
            {
                return IntPtr.Zero;
            }
        }

        public static bool ReadProcessMemory(IntPtr processHandle, IntPtr address, byte[] buffer, uint size, out int bytesRead)
        {
            return NativeMethods.ReadProcessMemory(processHandle, address, buffer, size, out bytesRead);
        }

        #endregion

        #region Memory Reading

        public static byte[] ReadMemoryBytes(IntPtr processHandle, IntPtr address, int bytesToRead)
        {
            byte[] buffer = new byte[bytesToRead];
            if (NativeMethods.ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _))
            {
                return buffer;
            }
            return null;
        }

        public static string ReadMemoryValueAsString(IntPtr processHandle, IntPtr address, int bytesToRead, DataType dataType)
        {
            Process process = GetMGS3Process();
            if (process == null || process.MainModule == null)
            {
                return "Process not found or has exited.";
            }

            byte[] buffer = ReadMemoryBytes(processHandle, address, bytesToRead);
            string addressHex = $"0x{address.ToInt64():X}";
            string moduleOffset = $"METAL GEAR SOLID3.exe+{(address.ToInt64() - process.MainModule.BaseAddress.ToInt64()):X}";

            if (buffer == null || buffer.Length != bytesToRead)
                return $"Failed to read memory from: {moduleOffset} (Address: {addressHex}).";

            return FormatMemoryRead(buffer, bytesToRead, addressHex, moduleOffset, dataType);
        }

        private static string FormatMemoryRead(byte[] buffer, int bytesToRead, string addressHex, string moduleOffset, DataType dataType)
        {
            StringBuilder result = new StringBuilder();
            result.Append($"Address Offset: {moduleOffset}\n");
            result.Append($"Address in Hex: {addressHex}\n");

            switch (dataType)
            {
                case DataType.UInt8:
                    result.Append($"UInt8/Byte\nValue in Decimal: {buffer[0]}\nValue in Hex: {buffer[0]:X2}\n");
                    break;
                case DataType.Int8:
                    sbyte sbyteVal = (sbyte)buffer[0];
                    result.Append($"Int8/Signed Byte\nValue in Decimal: {sbyteVal}\nValue in Hex: {sbyteVal:X2}\n");
                    break;
                case DataType.Int16:
                    short shortVal = BitConverter.ToInt16(buffer, 0);
                    result.Append($"Int16\nValue in Decimal: {shortVal}\nValue in Hex: {shortVal:X4}\n");
                    break;
                case DataType.UInt16:
                    ushort ushortVal = BitConverter.ToUInt16(buffer, 0);
                    result.Append($"UInt16\nValue in Decimal: {ushortVal}\nValue in Hex: {ushortVal:X4}\n");
                    break;
                case DataType.Int32:
                    int intVal = BitConverter.ToInt32(buffer, 0);
                    result.Append($"Int32\nValue in Decimal: {intVal}\nValue in Hex: {intVal:X8}\n");
                    break;
                case DataType.UInt32:
                    uint uintVal = BitConverter.ToUInt32(buffer, 0);
                    result.Append($"UInt32\nValue in Decimal: {uintVal}\nValue in Hex: {uintVal:X8}\n");
                    break;
                case DataType.Int64:
                    long longVal = BitConverter.ToInt64(buffer, 0);
                    result.Append($"Int64\nValue in Decimal: {longVal}\nValue in Hex: {longVal:X16}\n");
                    break;
                case DataType.UInt64:
                    ulong ulongVal = BitConverter.ToUInt64(buffer, 0);
                    result.Append($"UInt64\nValue in Decimal: {ulongVal}\nValue in Hex: {ulongVal:X16}\n");
                    break;
                case DataType.Float:
                    float floatVal = BitConverter.ToSingle(buffer, 0);
                    result.Append($"Float\nValue in Decimal: {floatVal}\nValue in Hex: {BitConverter.ToInt32(buffer, 0):X8}\n");
                    break;
                case DataType.Double:
                    double doubleVal = BitConverter.ToDouble(buffer, 0);
                    result.Append($"Double\nValue in Decimal: {doubleVal}\nValue in Hex: {BitConverter.ToInt64(buffer, 0):X16}\n");
                    break;
                case DataType.ByteArray:
                    result.Append("Byte Array\nValues in Decimal: ");
                    for (int i = 0; i < bytesToRead; i++)
                    {
                        result.Append($"{buffer[i]} ");
                    }
                    result.Append("\nValues in Hex: ");
                    for (int i = 0; i < bytesToRead; i++)
                    {
                        result.Append($"{buffer[i]:X2} ");
                    }
                    break;
                default:
                    throw new InvalidOperationException("Unsupported data type.");
            }

            return result.ToString().Trim();
        }

        #endregion

        #region Memory Writing

        public static bool WriteMemory<T>(IntPtr processHandle, IntPtr address, T value)
        {
            byte[] buffer;

            if (typeof(T) == typeof(byte[]))
            {
                buffer = value as byte[];
            }
            else
            {
                int size = Marshal.SizeOf(typeof(T));
                buffer = new byte[size];
                IntPtr ptr = Marshal.AllocHGlobal(size);
                try
                {
                    Marshal.StructureToPtr(value, ptr, false);
                    Marshal.Copy(ptr, buffer, 0, size);
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return NativeMethods.WriteProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _);
        }

        // This function utilizes the WriteMemory but helps with writing to a specific address you navigate to with an AOB
        // Example (From DamageManager) with Hex and Decimal formats depending on what you're trying to write:
        // WriteValue(processHandle, "M63Damage", 2, false, 1000);  // Using direct decimal for ushort
        // WriteValue(processHandle, "KnifeForkDamage", 6, false, new byte[] { 0x29, 0x86, 0x38, 0x01, 0x00, 0x00 }); 
        public void WriteValue<T>(IntPtr processHandle, string aobKey, int offset, bool forward, T value)
        {
            IntPtr aobResult = MemoryManager.Instance.FindAob(aobKey);
            if (aobResult == IntPtr.Zero)
            {
                LoggingManager.Instance.Log($"Error: AOB pattern not found for {aobKey}.");
                return;
            }

            IntPtr targetAddress = forward ? IntPtr.Add(aobResult, offset) : IntPtr.Subtract(aobResult, offset);
            bool writeResult = MemoryManager.WriteMemory(processHandle, targetAddress, value);
            LoggingManager.Instance.Log(writeResult ? $"{aobKey} written successfully." : $"Failed to write {aobKey}.");
        }

        public static short SetSpecificBits(short currentValue, int startBit, int endBit, int valueToSet)
        {
            int maskLength = endBit - startBit + 1;
            int mask = ((1 << maskLength) - 1) << startBit;
            return (short)((currentValue & ~mask) | ((valueToSet << startBit) & mask));
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

        public IntPtr ScanForStringMemory(IntPtr processHandle, IntPtr startAddress, long size, byte[] pattern)
        {
            int bufferSize = 65536; // 64 KB
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            long endAddress = startAddress.ToInt64() + size;
            for (long address = startAddress.ToInt64(); address < endAddress; address += bufferSize)
            {
                int effectiveSize = (int)Math.Min(bufferSize, endAddress - address);
                bool success = MemoryManager.ReadProcessMemory(processHandle, new IntPtr(address), buffer, (uint)effectiveSize, out bytesRead);
                if (!success || bytesRead == 0)
                {
                    continue;
                }

                for (int i = 0; i <= bytesRead - pattern.Length; i++)
                {
                    if (IsDirectMatch(buffer, i, pattern, bytesRead))
                    {
                        return new IntPtr(address + i);
                    }
                }
            }

            return IntPtr.Zero;
        }

        public bool IsDirectMatch(byte[] buffer, int position, byte[] pattern, int bytesRead)
        {
            if (position + pattern.Length > bytesRead) return false;

            for (int i = 0; i < pattern.Length; i++)
            {
                if (buffer[position + i] != pattern[i])
                {
                    return false;
                }
            }
            return true;
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

        // Store the last found AOB address in a static variable to reduce log spamming
        private static IntPtr lastLoggedAobAddress = IntPtr.Zero;

        public IntPtr FindLastAob(string key, string aobName)
        {
            if (!AobManager.AOBs.TryGetValue(key, out var aobData))
            {
                LoggingManager.Instance.Log($"{aobName} AOB not found in AOB Manager.");
                return IntPtr.Zero;
            }

            var process = GetMGS3Process();
            if (process == null || process.MainModule == null)
            {
                LoggingManager.Instance.Log($"{aobName}: Game process not found.");
                return IntPtr.Zero;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log($"{aobName}: Failed to open game process.");
                return IntPtr.Zero;
            }

            IntPtr baseAddress = process.MainModule.BaseAddress;
            IntPtr startAddress = aobData.StartOffset.HasValue ? IntPtr.Add(baseAddress, (int)aobData.StartOffset.Value) : baseAddress;
            IntPtr endAddress = aobData.EndOffset.HasValue ? IntPtr.Add(baseAddress, (int)aobData.EndOffset.Value) : IntPtr.Add(baseAddress, (int)process.MainModule.ModuleMemorySize);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            // Use ScanForAllInstances to get all occurrences
            List<IntPtr> foundAddresses = ScanForAllInstances(processHandle, startAddress, size, aobData.Pattern, aobData.Mask);

            NativeMethods.CloseHandle(processHandle);

            if (foundAddresses.Count == 0)
            {
                LoggingManager.Instance.Log($"{aobName} AOB not found.");
                return IntPtr.Zero;
            }

            // Get the last found address
            IntPtr lastFoundAddress = foundAddresses[foundAddresses.Count - 1];

            // Only log if the last found AOB address has changed
            if (lastFoundAddress != lastLoggedAobAddress)
            {
                long lastRelativeOffset = lastFoundAddress.ToInt64() - baseAddress.ToInt64();
                LoggingManager.Instance.Log($"Last instance of {aobName} AOB found at: {lastFoundAddress.ToString("X")} (METAL GEAR SOLID 3.exe+{lastRelativeOffset:X})");

                // Update the cached AOB address
                lastLoggedAobAddress = lastFoundAddress;
            }

            return lastFoundAddress;
        }


        // This function is to access a pointer in the memory of MGS3
        public IntPtr ReadIntPtr(IntPtr processHandle, IntPtr address)
        {
            byte[] buffer = new byte[IntPtr.Size]; // Allocate a buffer to hold the pointer (4 bytes for 32-bit, 8 bytes for 64-bit)
            bool success = ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out int bytesRead);

            if (!success || bytesRead != buffer.Length)
            {
                LoggingManager.Instance.Log($"Failed to read pointer at address: {address.ToString("X")}");
                return IntPtr.Zero;
            }

            // Convert the buffer to an IntPtr
            if (IntPtr.Size == 4) // 32-bit system
            {
                return new IntPtr(BitConverter.ToInt32(buffer, 0));
            }
            else // 64-bit system
            {
                return new IntPtr(BitConverter.ToInt64(buffer, 0));
            }
        }

        #endregion
    }
}