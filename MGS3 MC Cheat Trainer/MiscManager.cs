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
        private MiscManager()
        {
        }

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

            bool success = NativeMethods.WriteProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length,
                out bytesWritten);

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
            return NativeMethods.WriteProcessMemory(processHandle, targetAddress, nopBytes, (uint)nopBytes.Length,
                out int bytesWritten) && bytesWritten == nopBytes.Length;
        }

        public static bool RestoreCamoIndex(IntPtr processHandle, IntPtr address)
        {
            IntPtr targetAddress = IntPtr.Subtract(address, 4);
            byte[] restoreBytes = new byte[] { 0x89, 0x44, 0x2B, 0x24 };
            return NativeMethods.WriteProcessMemory(processHandle, targetAddress, restoreBytes,
                (uint)restoreBytes.Length, out int bytesWritten) && bytesWritten == restoreBytes.Length;
        }

        // These two are a sort of hacky way Trilon found in Cheat Engine to enable
        // and disable the camo index It's not super ideal since it resets to 0%
        // when a new area is loaded or if the game is paused which means players
        // who alt + tab out of the game will lose their camo index everytime so
        // ticket-devaizter brought this to my attention so my workaround for now
        // is for the slider to just force update whatever the slider is when the
        // instructions for Camo Index are NOP'd out
        public void EnableNOPCamoIndex()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null) return;

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero) return;


            IntPtr camoIndexAddress = MemoryManager.Instance.FindAob("CamoOperations");
            if (camoIndexAddress != IntPtr.Zero)
            {
                NOPCamoIndex(processHandle, camoIndexAddress);
            }

            NativeMethods.CloseHandle(processHandle);
        }

        // This just cancels out the NOP instructions for the camo index
        // and restores the original instructions of the Camo Index
        public static void RestoreCamoIndex()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null) return;

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
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

                // Hardcoded address for the camo index for the time being I've been trying to
                // find a good AOB but it has been a pain in the ass with the region it's in
                IntPtr baseAddress = process.MainModule.BaseAddress;
                IntPtr offset = new IntPtr(0x1DE58A4);
                IntPtr targetAddress = IntPtr.Add(baseAddress, offset.ToInt32());

                bool result = MemoryManager.Instance.WriteIntToMemory(processHandle, targetAddress, newValue);
                if (!result)
                {
                    LoggingManager.Instance.Log("Failed to write the new camo index value.");
                }
                else
                {
                    // Commented out to avoid spamming log file uncomment if needed for debugging
                    //LoggingManager.Instance.Log($"Successfully wrote new camo index value: {newValue} at {targetAddress.ToString("X")}");
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        public int ReadCamoIndex()
        {
            IntPtr processHandle = IntPtr.Zero;
            int camoIndexValue = 0;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return camoIndexValue;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return camoIndexValue;

                IntPtr baseAddress = process.MainModule.BaseAddress;
                IntPtr offset = new IntPtr(0x1DE58A4);
                IntPtr targetAddress = IntPtr.Add(baseAddress, offset.ToInt32());

                byte[] buffer = new byte[4];
                if (MemoryManager.ReadProcessMemory(processHandle, targetAddress, buffer, 4, out _))
                {
                    camoIndexValue = BitConverter.ToInt32(buffer, 0);
                    // Commented out the logging to avoid spamming the log file uncomment if needed for debugging
                    //LoggingManager.Instance.Log($"Current camo index value: {camoIndexValue}");
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }

            return camoIndexValue;
        }

        // Read instructions which are directly before IntPtr camoIndexAddress = MemoryManager.Instance.FindAob("CamoOperations"); and it's length is 4 bytes we want to check if the instructions are NOP'd out based on the other functions we have named NOPCamoIndex and RestoreCamoIndex

        public bool IsCamoIndexNopped()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null) return false;

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero) return false;

            IntPtr camoIndexAddress = MemoryManager.Instance.FindAob("CamoOperations");
            if (camoIndexAddress == IntPtr.Zero) return false;

            IntPtr targetAddress = IntPtr.Subtract(camoIndexAddress, 4);
            byte[] buffer = new byte[4];
            if (MemoryManager.ReadProcessMemory(processHandle, targetAddress, buffer, 4, out _))
            {
                return buffer[0] == 0x90 && buffer[1] == 0x90 && buffer[2] == 0x90 && buffer[3] == 0x90;
            }

            return false;
        }

        // We can now use this in the form to run a check if the camo index is NOP'd out or not like so:
        // MiscManager.Instance.IsCamoIndexNopped();
    }
}