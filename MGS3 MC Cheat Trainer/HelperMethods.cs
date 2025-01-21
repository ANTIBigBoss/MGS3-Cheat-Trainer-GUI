using System.Diagnostics;
using static MGS3_MC_Cheat_Trainer.MemoryManager;
using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    public class HelperMethods
    {
        private static HelperMethods instance;
        private static readonly object lockObj = new object();

        private HelperMethods()
        {
        }

        public static HelperMethods Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new HelperMethods();
                    }

                    return instance;
                }
            }
        }

        #region Helpers
        /// <summary>
        /// This method is used to get the process handle of the game.<br></br>
        /// i.e. METAL GEAR SOLID3.exe
        /// </summary>
        /// <returns></returns>
        public IntPtr GetProcessHandle()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null) return IntPtr.Zero;

            return MemoryManager.OpenGameProcess(process);
        }

        /// <summary>
        /// Gets the target address based on the AOB pattern and offset.
        /// </summary>
        /// <param name="processHandle"></param>
        /// <param name="patternName"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public IntPtr GetTargetAddress(IntPtr processHandle, string patternName, int offset)
        {
            IntPtr address = MemoryManager.Instance.FindAob(patternName);
            if (address == IntPtr.Zero) return IntPtr.Zero;

            return IntPtr.Subtract(address, offset);
        }

        /// <summary>
        /// Verifies the memory at the specified address. This is used to check if the cheat is already applied.
        /// Also helpful to check if the game is in the correct state.
        /// </summary>
        /// <param name="processHandle"></param>
        /// <param name="address"></param>
        /// <param name="expectedBytes"></param>
        /// <returns></returns>
        public bool VerifyMemory(IntPtr processHandle, IntPtr address, byte[] expectedBytes)
        {
            byte[] buffer = new byte[expectedBytes.Length];
            if (MemoryManager.ReadProcessMemory(processHandle, address, buffer, (uint)expectedBytes.Length, out _))
            {
                return buffer.SequenceEqual(expectedBytes);
            }

            return false;
        }

        public bool WriteMemory(IntPtr processHandle, IntPtr address, byte[] bytes)
        {
            return MemoryManager.WriteMemory(processHandle, address, bytes);
        }

        #endregion

        #region AddressCalculation

        /// <summary>
        /// Retrieves the base address of the specified process.
        /// </summary>
        /// <param name="processName">Name of the process. Defaults to "METAL GEAR SOLID3".</param>
        /// <returns>Base address as IntPtr.</returns>
        /// <exception cref="ArgumentException">Thrown when the process is not found.</exception>
        public IntPtr GetBaseAddress(string processName = "METAL GEAR SOLID3")
        {
            var processes = Process.GetProcessesByName(processName);

            if (processes.Length == 0)
            {
                throw new ArgumentException($"Process '{processName}' not found. Make sure it is running.");
            }

            return processes[0].MainModule.BaseAddress;
        }

        /// <summary>
        /// Calculates the offset based on the current address and the base address.
        /// </summary>
        /// <param name="baseAddress">Base address of the process.</param>
        /// <param name="currentAddress">Current address found (in hexadecimal).</param>
        /// <returns>Offset as a long integer.</returns>
        public long CalculateOffset(IntPtr baseAddress, long currentAddress)
        {
            return currentAddress - baseAddress.ToInt64();
        }

        /// <summary>
        /// Recomputes the absolute address using the base address and offset.
        /// </summary>
        /// <param name="baseAddress">Base address of the process.</param>
        /// <param name="offset">Offset to apply.</param>
        /// <returns>Recomputed absolute address as IntPtr.</returns>
        public IntPtr RecomputeAbsoluteAddress(IntPtr baseAddress, long offset)
        {
            return IntPtr.Add(baseAddress, (int)offset);
        }

        /// <summary>
        /// Generates a formatted Cheat Engine string.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <param name="offset">Offset value.</param>
        /// <returns>Formatted Cheat Engine string.</returns>
        public string GenerateCheatEngineString(string processName, long offset)
        {
            return $"{processName}.exe+{offset:X}";
        }

        

        public string ReadMemoryValueOnlyAsString(string aobKey, int offset, int bytesToRead, DataType dataType)
        {
            try
            {
                IntPtr processHandle = HelperMethods.Instance.GetProcessHandle();
                if (processHandle == IntPtr.Zero)
                    return "Error: Process handle is invalid.";

                IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, aobKey, offset);
                if (targetAddress == IntPtr.Zero)
                    return "Error: Target address is invalid.";

                byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, targetAddress, bytesToRead);
                if (buffer == null || buffer.Length != bytesToRead)
                    return "Error: Could not read memory.";

                return HelperMethods.Instance.FormatReadValueForTextboxes(buffer, dataType);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public static string ReadMemoryValueAndAddressAsString(IntPtr processHandle, IntPtr address, int bytesToRead, DataType dataType)
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

        public string FormatReadValueForTextboxes(byte[] buffer, DataType dataType)
        {
            string rawBytes = BitConverter.ToString(buffer);

            string result = dataType switch
            {
                DataType.UInt8 => buffer[0].ToString(),
                DataType.UInt16 => BitConverter.ToUInt16(buffer, 0).ToString(),
                DataType.UInt32 => BitConverter.ToUInt32(buffer, 0).ToString(),
                DataType.Int8 => ((sbyte)buffer[0]).ToString(),
                DataType.Int16 => BitConverter.ToInt16(buffer, 0).ToString(),
                DataType.Int32 => BitConverter.ToInt32(buffer, 0).ToString(),
                DataType.Float => BitConverter.ToSingle(buffer, 0).ToString("F2"),
                _ => BitConverter.ToString(buffer).Replace("-", " "),
            };

            result = result.Trim();
            return result;
        }

        #endregion

    }
}