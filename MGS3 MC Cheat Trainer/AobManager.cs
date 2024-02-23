using System;
using System.Diagnostics;
using System.Windows.Forms; // For MessageBox
// Use MemoryManager
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public class AobManager
    {
        private static AobManager instance;
        private static readonly object lockObj = new object();

        // Private constructor to prevent external instantiation
        private AobManager() { }

        // Public property to access the instance
        public static AobManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new AobManager();
                    }
                    return instance;
                }
            }
        }
        private MemoryManager memoryManager;

        
        public IntPtr FoundTheFearAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheFuryAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheEnds063aAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheEnds065aAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundShagohodAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundVolginOnShagohodAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheBossAddress { get; private set; } = IntPtr.Zero;

        public IntPtr FoundOcelotAddress { get; private set; } = IntPtr.Zero;
        public IntPtr StoredOcelotAddress = IntPtr.Zero;
        public IntPtr DynamicAOBAddress = IntPtr.Zero;
        public IntPtr OcelotHealthAddress = IntPtr.Zero;


        public bool FindAndStoreOcelotAOB()
        {
            if (OcelotHealthAddress != IntPtr.Zero)
            {
                return true; // If already calculated, immediately return true.
            }

            if (StoredOcelotAddress == IntPtr.Zero && TryFindOcelotDynamicAddress(out IntPtr dynamicAddress))
            {
                StoredOcelotAddress = dynamicAddress;
            }

            if (StoredOcelotAddress != IntPtr.Zero)
            {
                CalculateOcelotHealthAddress(StoredOcelotAddress);
            }

            return OcelotHealthAddress != IntPtr.Zero;
        }

        private bool TryFindOcelotDynamicAddress(out IntPtr dynamicAddress)
        {
            LoggingManager.Instance.Log("Attempting to access the Dynamic Address.");
            dynamicAddress = IntPtr.Zero;
            var process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("MGS3 process not found.");
                return false;
            }

            IntPtr baseAddress = IntPtr.Zero;
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName.Equals("METAL GEAR SOLID3.exe", StringComparison.OrdinalIgnoreCase))
                {
                    baseAddress = module.BaseAddress;
                    LoggingManager.Instance.Log($"METAL GEAR SOLID3.exe module found at: 0x{baseAddress.ToString("X")}");
                    break;
                }
            }

            if (baseAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("METAL GEAR SOLID3.exe module not found.");
                LoggingManager.Instance.Log("Failed to find base address for scanning.");
                return false;
            }

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process for scanning.");
                return false;
            }

            byte[] pattern = new byte[] { 0xC0, 0x37, 0x00, 0x00, 0x00, 0x7F, 0x00, 0x00, 0x20, 0x11, 0x00, 0x00, 0x00, 0x7F };
            string mask = "xx???xxxxx???x";
            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1D00000);
            long size = 0x1E00000 - 0x1D00000;
            IntPtr foundAddress = MemoryManager.Instance.ScanMemory(processHandle, startAddress, size, pattern, mask);

            if (foundAddress != IntPtr.Zero)
            {
                dynamicAddress = IntPtr.Subtract(foundAddress, 848); // Adjust according to your logic
                LoggingManager.Instance.Log($"Dynamic AOB address is: 0x{dynamicAddress.ToString("X")}");
                NativeMethods.CloseHandle(processHandle);
                return true;
            }
            else
            {
                LoggingManager.Instance.Log("AOB not found within specified range.");
                NativeMethods.CloseHandle(processHandle);
                return false;
            }
        }

        private void CalculateOcelotHealthAddress(IntPtr dynamicAddress)
        {
            var process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("MGS3 process not found.");
                return;
            }

            if (dynamicAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Dynamic address not set. Run AOB scan first.");
                return;
            }

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process for scanning.");
                return;
            }

            // Read the pointer from the dynamic address.
            IntPtr pointerAddress = MemoryManager.Instance.ReadIntPtr(processHandle, dynamicAddress);
            if (pointerAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to read pointer from dynamic address.");
                NativeMethods.CloseHandle(processHandle);
                return;
            }

            // Apply the offset of 0x5DC to reach the target memory location.
            IntPtr DynamicAOBAddress = IntPtr.Add(pointerAddress, 0x5DC);
            LoggingManager.Instance.Log($"Target address is: 0x{DynamicAOBAddress.ToString("X")}");

            // Move 916 bytes backwards from the target address.
            OcelotHealthAddress = IntPtr.Subtract(DynamicAOBAddress, 916);
            // Ensure this is logged or indicated as a success.
            LoggingManager.Instance.Log($"Ocelot Health Address calculated and set: 0x{OcelotHealthAddress.ToString("X")}");

            // Now, read the short value from the adjusted address.
            short value = ReadShortFromMemory(processHandle, OcelotHealthAddress);

            if (value != -1) // Assuming -1 indicates a failure to read
            {
                LoggingManager.Instance.Log($"Short value at adjusted address (0x{OcelotHealthAddress.ToString("X")}) is: {value}");
            }
            else
            {
                LoggingManager.Instance.Log("Failed to read short value from adjusted address.");
            }

            NativeMethods.CloseHandle(processHandle);
        }


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
            IntPtr startAddress = new IntPtr(0x10FFFFFFFFF); // Example start range
            IntPtr endAddress = new IntPtr(0x30000000000); // Example end range
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr foundAddress = MemoryManager.Instance.ScanWideMemory(processHandle, startAddress, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            if (foundAddress != IntPtr.Zero)
            {
                FoundTheFearAddress = foundAddress; // Store found address
                return true;
            }

            return false;
        }
        
        public bool FindAndStoreTheFuryAOB()
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

            var (pattern, mask) = Constants.AOBs["TheFury"];
            IntPtr startAddress = new IntPtr(0x10FFFFFFFFF); // Example start range
            IntPtr endAddress = new IntPtr(0x30000000000); // Example end range
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr foundAddress = MemoryManager.Instance.ScanWideMemory(processHandle, startAddress, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            if (foundAddress != IntPtr.Zero)
            {
                FoundTheFuryAddress = foundAddress; // Store found address
                return true;
            }

            return false;
        }
        
        public bool FindAndStoreTheEnds063aAOB()
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

            var (pattern, mask) = Constants.AOBs["TheEnds063a"];
            IntPtr startAddress = new IntPtr(0x10FFFFFFFFF);
            IntPtr endAddress = new IntPtr(0x30000000000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr foundAddress = MemoryManager.Instance.ScanWideMemory(processHandle, startAddress, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            if (foundAddress != IntPtr.Zero)
            {
                FoundTheEnds063aAddress = foundAddress;
                return true;
            }

            return false;
        }
        
        public bool FindAndStoreTheEnds065aAOB()
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

            var (pattern, mask) = Constants.AOBs["TheEnds065a"];
            IntPtr startAddress = new IntPtr(0x10FFFFFFFFF);
            IntPtr endAddress = new IntPtr(0x30000000000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr foundAddress = MemoryManager.Instance.ScanWideMemory(processHandle, startAddress, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            if (foundAddress != IntPtr.Zero)
            {
                FoundTheEnds065aAddress = foundAddress;
                return true;
            }

            return false;
        }
        
        public bool FindAndStoreShagohodAOB()
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

            var (pattern, mask) = Constants.AOBs["Shagohod"];
            IntPtr startAddress = new IntPtr(0x10FFFFFFFFF); // Example start range
            IntPtr endAddress = new IntPtr(0x30000000000); // Example end range
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr foundAddress = MemoryManager.Instance.ScanWideMemory(processHandle, startAddress, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            if (foundAddress != IntPtr.Zero)
            {
                FoundShagohodAddress = foundAddress; // Store found address
                return true;
            }

            return false;
        }
        
        public bool FindAndStoreVolginOnShagohodAOB()
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

            var (pattern, mask) = Constants.AOBs["VolginOnShagohod"];
            IntPtr startAddress = new IntPtr(0x10FFFFFFFFF); // Example start range
            IntPtr endAddress = new IntPtr(0x30000000000); // Example end range
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr foundAddress = MemoryManager.Instance.ScanWideMemory(processHandle, startAddress, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            if (foundAddress != IntPtr.Zero)
            {
                FoundVolginOnShagohodAddress = foundAddress; // Store found address
                return true;
            }

            return false;
        }

        
        public bool FindAndStoreTheBossAOB()
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

            var (pattern, mask) = Constants.AOBs["TheBoss"];
            IntPtr startAddress = new IntPtr(0x10FFFFFFFFF); // Example start range
            IntPtr endAddress = new IntPtr(0x30000000000); // Example end range
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr foundAddress = MemoryManager.Instance.ScanWideMemory(processHandle, startAddress, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            if (foundAddress != IntPtr.Zero)
            {
                FoundTheBossAddress = foundAddress; // Store found address
                return true;
            }

            return false;
        }
    }
}