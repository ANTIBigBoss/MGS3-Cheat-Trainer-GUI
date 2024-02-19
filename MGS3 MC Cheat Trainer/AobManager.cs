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

        public IntPtr FoundOcelotAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheFearAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheFuryAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheEnds063aAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheEnds065aAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundShagohodAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundVolginOnShagohodAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheBossAddress { get; private set; } = IntPtr.Zero;

        public bool FindAndStoreOcelotAOB()
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