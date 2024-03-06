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

        
        public IntPtr FoundTheFuryAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheEnds063aAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheEnds065aAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundShagohodAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundVolginOnShagohodAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheBossAddress { get; private set; } = IntPtr.Zero;

        // This variable can be used for any boss I think
        public IntPtr DynamicAOBAddress = IntPtr.Zero;

        #region Ocelot AOB
        public IntPtr FoundOcelotAddress { get; private set; } = IntPtr.Zero;
        public IntPtr StoredOcelotAddress = IntPtr.Zero;
        public IntPtr OcelotHealthAddress = IntPtr.Zero;
        public IntPtr OcelotStaminaAddress = IntPtr.Zero;


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
                CalculateOcelotAddress(StoredOcelotAddress, 916, "Health");
                CalculateOcelotAddress(StoredOcelotAddress, 908, "Stamina");
            }

            // If not found, try the backup method
            if (OcelotHealthAddress == IntPtr.Zero)
            {
                DynamicBackupToFindAndStoreOcelotAOB();
            }

            return OcelotHealthAddress != IntPtr.Zero;
        }

         // This essentially looks for an AOB near a pointer address and then calculates the distance
         // between the AOB and the pointer address and then the function after accesses the pointer
         // this saves the program taking forever to find the AOB within 30 million potential addresses
         
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

            // Unique AOB pattern near the pointer address the address isn't always the same
            // so we use wildcards (?) for the bytes that change this way it can determine
            // what is actually 0x00 and what is a wildcard
            byte[] pattern = new byte[] { 0xC0, 0x37, 0x00, 0x00, 0x00, 0x7F, 0x00, 0x00, 0x20, 0x11, 0x00, 0x00, 0x00, 0x7F };
            
            string mask = "xx???xxxxx???x";
            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1000000);
            long size = 0x3000000 - 0x1000000;
            IntPtr foundAddress = MemoryManager.Instance.ScanMemory(processHandle, startAddress, size, pattern, mask);

            if (foundAddress != IntPtr.Zero)
            {
                dynamicAddress = IntPtr.Subtract(foundAddress, 848);
                LoggingManager.Instance.Log($"Dynamic AOB address for Ocelot is: 0x{dynamicAddress.ToString("X")}");
                NativeMethods.CloseHandle(processHandle);
                return true;
            }
            else
            {
                LoggingManager.Instance.Log("AOB not found within specified range.");
                LoggingManager.Instance.Log($"First address searched: 0x{startAddress.ToString("X")}, Last address searched: 0x{size.ToString("X")}");
                NativeMethods.CloseHandle(processHandle);
                return false;
            }
        }

        private void CalculateOcelotAddress(IntPtr dynamicAddress, int offset, string addressType)
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

            IntPtr pointerAddress = MemoryManager.Instance.ReadIntPtr(processHandle, dynamicAddress);
            if (pointerAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to read pointer from dynamic address.");
                NativeMethods.CloseHandle(processHandle);
                return;
            }
            // This is the offset the pointer in Cheat Engine is using at the address
            IntPtr targetAddress = IntPtr.Add(pointerAddress, 0x5DC);
            LoggingManager.Instance.Log($"Target address is: 0x{targetAddress.ToString("X")}");

            IntPtr calculatedAddress = IntPtr.Subtract(targetAddress, offset);
            LoggingManager.Instance.Log($"{addressType} Address calculated and set: 0x{calculatedAddress.ToString("X")}");

            short value = ReadShortFromMemory(processHandle, calculatedAddress);

            if (value != -1)
            {
                LoggingManager.Instance.Log($"Short value at adjusted address (0x{calculatedAddress.ToString("X")}) is: {value}");
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to read short value from adjusted address for {addressType}.");
            }

            NativeMethods.CloseHandle(processHandle);

            if (addressType == "Health")
            {
                OcelotHealthAddress = calculatedAddress;
            }
            else if (addressType == "Stamina")
            {
                OcelotStaminaAddress = calculatedAddress;
            }
        }

        // Backup way to find the AOB incase the user isn't on the latest version of the game
        public bool DynamicBackupToFindAndStoreOcelotAOB()
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

            var (pattern, mask) = Constants.AOBs["Ocelot"];
            IntPtr startAddress = new IntPtr(0x10FFFFFFFFF); // Example start range
            IntPtr endAddress = new IntPtr(0x30000000000); // Example end range
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            IntPtr foundAddress = MemoryManager.Instance.ScanWideMemory(processHandle, startAddress, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            if (foundAddress != IntPtr.Zero)
            {
                FoundOcelotAddress = foundAddress; // Store found address
                return true;
            }

            return false;
        }


        // This will work on The Fear, Pain and Volgin if our pointer way of doing things doesn't work
        public bool DynamicBackupToFindAndStoreTheFearAOB()
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
        #endregion

        #region The Pain AOB
        public IntPtr FoundThePainAddress { get; private set; } = IntPtr.Zero;
        public IntPtr StoredThePainAddress = IntPtr.Zero;
        public IntPtr ThePainHealthAddress = IntPtr.Zero;
        public IntPtr ThePainStaminaAddress = IntPtr.Zero;

        public bool FindAndStoreThePainAOB()
        {
            if (ThePainHealthAddress != IntPtr.Zero)
            {
                return true; // If already calculated, immediately return true.
            }

            if (StoredThePainAddress == IntPtr.Zero && TryFindThePainDynamicAddress(out IntPtr dynamicAddress))
            {
                StoredThePainAddress = dynamicAddress;
            }

            if (StoredThePainAddress != IntPtr.Zero)
            {
                CalculateThePainAddress(StoredThePainAddress, 16, "Health");
                CalculateThePainAddress(StoredThePainAddress, 8, "Stamina");
            }

            // If not found, try the backup method
            if (ThePainHealthAddress == IntPtr.Zero)
            {
                DynamicBackupToFindAndStoreTheFearAOB();
            }

            return ThePainHealthAddress != IntPtr.Zero;
        }

        private bool TryFindThePainDynamicAddress(out IntPtr dynamicAddress)
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

            // AOB used is the same for Ocelot but pointer address is the only difference
            // hoping all bosses are in this region for reusability
            byte[] pattern = new byte[] { 0xC0, 0x37, 0x00, 0x00, 0x00, 0x7F, 0x00, 0x00, 0x20, 0x11, 0x00, 0x00, 0x00, 0x7F };       
            string mask = "xx???xxxxx???x";
            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1000000);
            long size = 0x3000000 - 0x1000000;
            IntPtr foundAddress = MemoryManager.Instance.ScanMemory(processHandle, startAddress, size, pattern, mask);

            if (foundAddress != IntPtr.Zero)
            {
                // This one is 418 bytes back as opposed to 848 for Ocelot
                dynamicAddress = IntPtr.Subtract(foundAddress, 1304);
                LoggingManager.Instance.Log($"Dynamic AOB address for The Pain is: 0x{dynamicAddress.ToString("X")}");
                NativeMethods.CloseHandle(processHandle);
                return true;
            }
            else
            {
                LoggingManager.Instance.Log("AOB not found within specified range.");
                LoggingManager.Instance.Log($"First address searched: 0x{startAddress.ToString("X")}, Last address searched: 0x{size.ToString("X")}");
                NativeMethods.CloseHandle(processHandle);
                return false;
            }
        }

        private void CalculateThePainAddress(IntPtr dynamicAddress, int offset, string addressType)
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

            IntPtr pointerAddress = MemoryManager.Instance.ReadIntPtr(processHandle, dynamicAddress);
            if (pointerAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to read pointer from dynamic address.");
                NativeMethods.CloseHandle(processHandle);
                return;
            }
            // This is the offset the pointer in Cheat Engine is using at the address
            IntPtr targetAddress = IntPtr.Add(pointerAddress, 0x2D8);
            LoggingManager.Instance.Log($"Target address is: 0x{targetAddress.ToString("X")}");

            IntPtr calculatedAddress = IntPtr.Subtract(targetAddress, offset);
            LoggingManager.Instance.Log($"{addressType} Address calculated and set: 0x{calculatedAddress.ToString("X")}");

            short value = ReadShortFromMemory(processHandle, calculatedAddress);

            if (value != -1)
            {
                LoggingManager.Instance.Log($"Short value at adjusted address (0x{calculatedAddress.ToString("X")}) is: {value}");
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to read short value from adjusted address for {addressType}.");
            }

            NativeMethods.CloseHandle(processHandle);

            if (addressType == "Health")
            {
                ThePainHealthAddress = calculatedAddress;
            }
            else if (addressType == "Stamina")
            {
                ThePainStaminaAddress = calculatedAddress;
            }
        }

        #endregion
        #region The Fear AOB

        public IntPtr FoundTheFearAddress { get; private set; } = IntPtr.Zero;
        public IntPtr StoredTheFearAddress = IntPtr.Zero;
        public IntPtr TheFearHealthAddress = IntPtr.Zero;
        public IntPtr TheFearStaminaAddress = IntPtr.Zero;

        public bool FindAndStoreTheFearAOB()
        {
            if (TheFearHealthAddress != IntPtr.Zero)
            {
                return true; // If already calculated, immediately return true.
            }

            if (StoredTheFearAddress == IntPtr.Zero && TryFindTheFearDynamicAddress(out IntPtr dynamicAddress))
            {
                StoredTheFearAddress = dynamicAddress;
            }

            if (StoredTheFearAddress != IntPtr.Zero)
            {
                CalculateTheFearAddress(StoredTheFearAddress, 16, "Health");
                CalculateTheFearAddress(StoredTheFearAddress, 8, "Stamina");
            }

            if (TheFearHealthAddress == IntPtr.Zero)
            {
                DynamicBackupToFindAndStoreTheFearAOB();
            }

            return TheFearHealthAddress != IntPtr.Zero;
        }

        private bool TryFindTheFearDynamicAddress(out IntPtr dynamicAddress)
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

            // AOB used is the same for Ocelot but pointer address is the only difference
            // hoping all bosses are in this region for reusability
            byte[] pattern = new byte[] { 0xC0, 0x37, 0x00, 0x00, 0x00, 0x7F, 0x00, 0x00, 0x20, 0x11, 0x00, 0x00, 0x00, 0x7F };
            string mask = "xx???xxxxx???x";
            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1000000);
            long size = 0x3000000 - 0x1000000;
            IntPtr foundAddress = MemoryManager.Instance.ScanMemory(processHandle, startAddress, size, pattern, mask);

            if (foundAddress != IntPtr.Zero)
            {
                // This one is not near Ocelot or Pain so going forward 902856 bytes and seeing if this works I should find
                // Something closers but this will do for testing until I have someone else to compare the memory with
                dynamicAddress = IntPtr.Add(foundAddress, 902856);
                LoggingManager.Instance.Log($"Dynamic AOB address for The Fear is: 0x{dynamicAddress.ToString("X")}");
                NativeMethods.CloseHandle(processHandle);
                return true;
            }
            else
            {
                LoggingManager.Instance.Log("AOB not found within specified range.");
                LoggingManager.Instance.Log($"First address searched: 0x{startAddress.ToString("X")}, Last address searched: 0x{size.ToString("X")}");
                NativeMethods.CloseHandle(processHandle);
                return false;
            }
        }

        private void CalculateTheFearAddress(IntPtr dynamicAddress, int offset, string addressType)
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

            IntPtr pointerAddress = MemoryManager.Instance.ReadIntPtr(processHandle, dynamicAddress);
            if (pointerAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to read pointer from dynamic address.");
                NativeMethods.CloseHandle(processHandle);
                return;
            }
            // This is the offset the pointer in Cheat Engine is using at the address
            IntPtr targetAddress = IntPtr.Add(pointerAddress, 0x368);
            LoggingManager.Instance.Log($"Target address is: 0x{targetAddress.ToString("X")}");

            IntPtr calculatedAddress = IntPtr.Subtract(targetAddress, offset);
            LoggingManager.Instance.Log($"{addressType} Address calculated and set: 0x{calculatedAddress.ToString("X")}");

            short value = ReadShortFromMemory(processHandle, calculatedAddress);

            if (value != -1)
            {
                LoggingManager.Instance.Log($"Short value at adjusted address (0x{calculatedAddress.ToString("X")}) is: {value}");
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to read short value from adjusted address for {addressType}.");
            }

            NativeMethods.CloseHandle(processHandle);

            if (addressType == "Health")
            {
                TheFearHealthAddress = calculatedAddress;
            }
            else if (addressType == "Stamina")
            {
                TheFearStaminaAddress = calculatedAddress;
            }
        }

        #endregion

        // Everyone from here down is using the old function for finding AOBs I want to wait for a new update I think
        // to see if my dynamic pointer finding way works and the memory AOBs are still the same or not

        public bool FindAndStoreTheFuryAOB()
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
                return false;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
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
                return false;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
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
                return false;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
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
                return false;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
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
                return false;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
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