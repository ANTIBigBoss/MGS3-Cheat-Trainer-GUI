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

            // Declare WriteProcessMemory with bytes
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

            // Declare ReadProcessMemory for short
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, out short lpBuffer, uint size, out int lpNumberOfBytesRead);

            // Declare ReadProcessMemory with bytes
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesRead);

            // Declare CloseHandle
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool CloseHandle(IntPtr hObject);

            // Add VirtualAllocEx here
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
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
                LoggingManager.Instance.Log($"OpenGameProcess Exception: {ex.Message}");
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

        public static string FormatMemoryRead(byte[] buffer, int bytesToRead, string addressHex, string moduleOffset, DataType dataType)
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

        /// <summary>
        /// Writes a value of type T to the specified address in the target process.
        /// </summary>
        /// <typeparam name="T">Type of the value to write.</typeparam>
        /// <param name="processHandle">Handle to the target process.</param>
        /// <param name="address">Address in the target process to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <returns>True if the write operation was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Sets specific bits in a short value.
        /// </summary>
        /// <param name="currentValue">Current short value.</param>
        /// <param name="startBit">Starting bit position.</param>
        /// <param name="endBit">Ending bit position.</param>
        /// <param name="valueToSet">Value to set in the specified bits.</param>
        /// <returns>Modified short value with specific bits set.</returns>
        public static short SetSpecificBits(short currentValue, int startBit, int endBit, int valueToSet)
        {
            int maskLength = endBit - startBit + 1;
            int mask = ((1 << maskLength) - 1) << startBit;
            return (short)((currentValue & ~mask) | ((valueToSet << startBit) & mask));
        }

        #endregion

        #region Aob Scanning and Helpers

        /// <summary>
        /// Scans memory for a specific byte pattern with an associated mask.
        /// </summary>
        /// <param name="processHandle">Handle to the target process.</param>
        /// <param name="startAddress">Starting address for the scan.</param>
        /// <param name="size">Size of the memory region to scan.</param>
        /// <param name="pattern">Byte pattern to search for.</param>
        /// <param name="mask">Mask indicating wildcards (e.g., "x ? x ?").</param>
        /// <returns>Address where the pattern is found; otherwise, IntPtr.Zero.</returns>
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

        /// <summary>
        /// Scans a wider memory region for a specific byte pattern with an associated mask.
        /// </summary>
        /// <param name="processHandle">Handle to the target process.</param>
        /// <param name="startAddress">Starting address for the scan.</param>
        /// <param name="size">Size of the memory region to scan.</param>
        /// <param name="pattern">Byte pattern to search for.</param>
        /// <param name="mask">Mask indicating wildcards (e.g., "x ? x ?").</param>
        /// <returns>Address where the pattern is found; otherwise, IntPtr.Zero.</returns>
        public IntPtr ScanWideMemory(IntPtr processHandle, IntPtr startAddress, long size, byte[] pattern, string mask)
        {
            // 10 MB buffer for wider scans
            int bufferSize = 10_000_000;
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

        /// <summary>
        /// Scans memory for a specific string pattern.
        /// </summary>
        /// <param name="processHandle">Handle to the target process.</param>
        /// <param name="startAddress">Starting address for the scan.</param>
        /// <param name="size">Size of the memory region to scan.</param>
        /// <param name="pattern">Byte pattern representing the string.</param>
        /// <returns>Address where the string is found; otherwise, IntPtr.Zero.</returns>
        public IntPtr ScanForStringMemory(IntPtr processHandle, IntPtr startAddress, long size, byte[] pattern)
        {
            int bufferSize = 65536; // 64 KB
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

        /// <summary>
        /// Checks for a direct byte match without wildcards.
        /// </summary>
        /// <param name="buffer">Buffer containing the memory bytes.</param>
        /// <param name="position">Current position in the buffer.</param>
        /// <param name="pattern">Byte pattern to match.</param>
        /// <param name="bytesRead">Total bytes read into the buffer.</param>
        /// <returns>True if the pattern matches at the current position; otherwise, false.</returns>
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

        /// <summary>
        /// Finds a dynamic AOB pattern in the target process.
        /// </summary>
        /// <param name="key">Key identifying the AOB pattern.</param>
        /// <returns>Address where the AOB pattern is found; otherwise, IntPtr.Zero.</returns>
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

            return foundAddress;
        }

        /// <summary>
        /// Checks if a byte pattern matches at a specific position within a buffer, considering a mask.
        /// </summary>
        /// <param name="buffer">Buffer containing the memory bytes.</param>
        /// <param name="position">Current position in the buffer.</param>
        /// <param name="pattern">Byte pattern to match.</param>
        /// <param name="mask">Mask indicating wildcards ('x' for exact match, '?' for wildcard).</param>
        /// <returns>True if the pattern matches at the current position; otherwise, false.</returns>
        public bool IsMatch(byte[] buffer, int position, byte[] pattern, string mask)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                // Remove spaces from mask if present
                char maskChar = mask[i] == ' ' ? 'x' : mask[i];

                // If the mask at this position is a wildcard ('?'), or the bytes match, continue checking
                if (maskChar == '?' || buffer[position + i] == pattern[i])
                {
                    continue;
                }
                // Mismatch found, return false immediately
                return false;
            }
            // All bytes matched
            return true;
        }

        /// <summary>
        /// Finds the first occurrence of an AOB pattern by key.
        /// </summary>
        /// <param name="key">Key identifying the AOB pattern.</param>
        /// <returns>Address where the AOB pattern is found; otherwise, IntPtr.Zero.</returns>
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

            return resultAddress;
        }

        /// <summary>
        /// Scans memory for all instances of a specific byte pattern with an associated mask.
        /// </summary>
        /// <param name="processHandle">Handle to the target process.</param>
        /// <param name="baseAddress">Base address to start scanning.</param>
        /// <param name="moduleSize">Size of the memory region to scan.</param>
        /// <param name="pattern">Byte pattern to search for.</param>
        /// <param name="mask">Mask indicating wildcards (e.g., "x ? x ?").</param>
        /// <returns>List of addresses where the pattern is found.</returns>
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

        /// <summary>
        /// Scans memory for all instances of a specific byte pattern with an associated mask.
        /// </summary>
        /// <param name="processHandle">Handle to the target process.</param>
        /// <param name="startAddress">Starting address for the scan.</param>
        /// <param name="size">Size of the memory region to scan.</param>
        /// <param name="pattern">Byte pattern to search for.</param>
        /// <param name="mask">Mask indicating wildcards (e.g., "x ? x ?").</param>
        /// <returns>List of addresses where the pattern is found.</returns>
        public List<IntPtr> ScanForAllInstances(IntPtr processHandle, IntPtr startAddress, long size, byte[] pattern, string mask)
        {
            List<IntPtr> foundAddresses = new List<IntPtr>();
            int bufferSize = 10_000_000; // 10 MB buffer
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

        /// <summary>
        /// Finds and logs the last occurrence of an AOB pattern by key.
        /// </summary>
        /// <param name="key">Key identifying the AOB pattern.</param>
        /// <param name="aobName">Name of the AOB pattern for logging.</param>
        /// <returns>Address of the last found AOB pattern; otherwise, IntPtr.Zero.</returns>
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
                //LoggingManager.Instance.Log($"{aobName}: Game process not found.");
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
                //LoggingManager.Instance.Log($"{aobName} AOB not found.");
                return IntPtr.Zero;
            }

            // Get the last found address
            IntPtr lastFoundAddress = foundAddresses[^1]; // Using C# 8.0 index from end

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

        /// <summary>
        /// Reads a pointer from the target process's memory.
        /// </summary>
        /// <param name="processHandle">Handle to the target process.</param>
        /// <param name="address">Address to read the pointer from.</param>
        /// <returns>The pointer read from memory; otherwise, IntPtr.Zero.</returns>
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