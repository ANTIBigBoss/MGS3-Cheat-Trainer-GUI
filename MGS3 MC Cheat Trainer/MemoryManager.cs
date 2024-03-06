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
                // Just do nothing for now so the user isn't spammed with messages
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


        // Gonna use this template for future AOBs with a wildcard
        public bool IsWildCardMatch(byte[] buffer, int position, byte[] pattern, string mask)
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

        public IntPtr FindAOBInCameraRange(byte[] aobPattern, string mask)
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

            IntPtr startAddress = IntPtr.Add(baseAddress, 0x00FFFF);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1000000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr address = ScanMemory(process.Handle, startAddress, size, aobPattern, mask);
            if (address != IntPtr.Zero)
            {
                LoggingManager.Instance.Log($"Found AOB at address {address.ToString("X")}.");
                return address;
            }

            return IntPtr.Zero;
        }

        public IntPtr FindAOBInModelRange(byte[] aobPattern, string mask)
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

            IntPtr startAddress = IntPtr.Add(baseAddress, 0x00000);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0xC00000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr address = ScanMemory(process.Handle, startAddress, size, aobPattern, mask);
            if (address != IntPtr.Zero)
            {
                return address;
            }

            return IntPtr.Zero;
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

        public void TeleportSnake(float x, float y, float z)
        {
            FindAndStoreSnakesPositionAOB();
            IntPtr processHandle = OpenGameProcess(GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                return;
            }

            if (FoundSnakePositionAddress == IntPtr.Zero && !FindAndStoreSnakesPositionAOB())
            {
                NativeMethods.CloseHandle(processHandle);
                return;
            }

            float[] newPosition = new float[] { x, y, z };
            for (int i = 0; i < newPosition.Length; i++)
            {
                IntPtr currentAddress = IntPtr.Add(FoundSnakePositionAddress, 7 + i * 4);
                bool success = WriteFloatToMemory(processHandle, currentAddress, newPosition[i]);
                if (!success)
                {
                    break;
                }
            }

            NativeMethods.CloseHandle(processHandle);
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
                    IntPtr currentAddress = IntPtr.Add(guardBaseAddress, 7 + i * 4); // Adjust if necessary
                    bool success = WriteFloatToMemory(processHandle, currentAddress, newPosition[i]);
                    if (!success)
                    {

                        // Optionally, break or continue based on your error handling preference
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

        public IntPtr FindAlertMemoryRegion(byte[] aobPattern, string mask)
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
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1FF0000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr address = ScanMemory(process.Handle, startAddress, size, aobPattern, mask);
            if (address != IntPtr.Zero)
            {
                return address;
            }


            return IntPtr.Zero;
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
            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1CFFF00);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1E00000);
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