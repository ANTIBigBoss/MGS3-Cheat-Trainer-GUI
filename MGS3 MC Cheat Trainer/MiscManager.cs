using System;
using System.Diagnostics;
using System.Windows.Forms; // For MessageBox
// Use MemoryManager
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public class MiscManager
    {
        private static MiscManager instance;
        private static readonly object lockObj = new object();

        // Private constructor to prevent external instantiation
        private MiscManager() { }

        // Public property to access the instance
        public static MiscManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new MiscManager();
                    }
                    return instance;
                }
            }
        }

        internal static void ModifyModel(byte value)
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

            IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            int bytesWritten;

            byte[] buffer = new byte[] { value };

            // foundAddress is the address of the AOB pattern "ModelDistortion"
            IntPtr foundAddress = MemoryManager.Instance.FindAob("ModelDistortion");

            if (foundAddress == IntPtr.Zero)
            {
                return;
            }

            // Target address takes the foundAddress of ModelDistortion and subtracts 1 to point to the byte before the AOB pattern
            IntPtr targetAddress = IntPtr.Subtract(foundAddress, 1);

            bool success = NativeMethods.WriteProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out bytesWritten);

            if (!success || bytesWritten != buffer.Length)
            {
                
            }

            NativeMethods.CloseHandle(processHandle);
        }

        internal static void ChangeCamera(Constants.CameraOptions cameraOption)
        {
            IntPtr cameraAddress = IntPtr.Zero;

            // Determine which AOB key to use based on the cameraOption
            switch (cameraOption)
            {
                case Constants.CameraOptions.Normal:
                    cameraAddress = MemoryManager.Instance.FindAob("NotUpsideDownCamera");
                    break;
                case Constants.CameraOptions.UpsideDown:
                    cameraAddress = MemoryManager.Instance.FindAob("UpsideDownCamera");
                    break;
            }

            // If a valid address was found, write the new camera setting value
            if (cameraAddress != IntPtr.Zero)
            {
                byte cameraSetting = (byte)cameraOption;
                MemoryManager.WriteByteValueToMemory(cameraAddress, cameraSetting);
            }
            else
            {
                // Optionally, log an error or handle the case where the address wasn't found
                MessageBox.Show("Camera AOB not found in memory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


            IntPtr camoIndexAddress = MemoryManager.Instance.FindAob("CamoOperations");
            if (camoIndexAddress != IntPtr.Zero)
            {
                NOPCamoIndex(processHandle, camoIndexAddress);
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

            IntPtr camoIndexAddress = MemoryManager.Instance.FindAob("CamoOperations");
            if (camoIndexAddress != IntPtr.Zero)
            {
                RestoreCamoIndex(processHandle, camoIndexAddress);
            }

            NativeMethods.CloseHandle(processHandle);
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

    }
}