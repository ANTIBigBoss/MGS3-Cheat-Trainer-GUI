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
                IntPtr offset = new IntPtr(0x1DE6964);
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

        public void SetFovSlider(float newFovValue)
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return;

                // Find the target address using the AOB pattern
                IntPtr aobResult = MemoryManager.Instance.FindAob("FovSlider");
                if (aobResult == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to find FOV AOB pattern.");
                    return;
                }

                IntPtr targetAddress =
                    IntPtr.Subtract(aobResult, 4); // Assuming FOV value is 4 bytes before the AOB pattern

                bool result = MemoryManager.WriteFloatToMemory(processHandle, targetAddress, newFovValue);
                if (!result)
                {
                    LoggingManager.Instance.Log("Failed to write the new FOV value.");
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        public float ReadFovSlider()
        {
            IntPtr processHandle = IntPtr.Zero;
            float fovValue = 0.0f;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return fovValue;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return fovValue;

                // Find the target address using the AOB pattern
                IntPtr aobResult = MemoryManager.Instance.FindAob("FovSlider");
                if (aobResult == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to find FOV AOB pattern.");
                    return fovValue;
                }

                IntPtr targetAddress = IntPtr.Subtract(aobResult, 4);

                fovValue = MemoryManager.ReadFloatFromMemory(processHandle, targetAddress);
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }

            return fovValue;
        }

        // Using the AOB name "PissFilter" we go back 1595/5525 Byte before this AOB is the filter value the default is either 42 or 00 in hex we want it to be 44 to disable the piss filter
        public void DisablePissFilter()
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return;

                IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilter");
                if (aobResult == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to find Piss Filter AOB pattern.");
                    return;
                }

                IntPtr targetAddress = IntPtr.Subtract(aobResult, 5525);

                // Write the new byte value to memory using WriteByteValueToMemory method
                MemoryManager.WriteByteValueToMemory(targetAddress, 0x44);

                LoggingManager.Instance.Log("Piss filter disabled successfully.");
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        // Restore the piss filter to its default value of 00
        public void EnablePissFilter()
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return;

                IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilter");
                if (aobResult == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to find Piss Filter AOB pattern.");
                    return;
                }

                IntPtr targetAddress = IntPtr.Subtract(aobResult, 5525);

                // Write the default byte value to memory using WriteByteValueToMemory method
                MemoryManager.WriteByteValueToMemory(targetAddress, 0x00);

                LoggingManager.Instance.Log("Piss filter restored successfully.");
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        // Read Value at the piss filter address

        public byte ReadPissFilterValue()
        {
            IntPtr processHandle = IntPtr.Zero;
            byte pissFilterValue = 0x00;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return pissFilterValue;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return pissFilterValue;

                IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilter");
                if (aobResult == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to find Piss Filter AOB pattern.");
                    return pissFilterValue;
                }

                IntPtr targetAddress = IntPtr.Subtract(aobResult, 5525);

                pissFilterValue = MemoryManager.ReadByteFromMemory(processHandle, targetAddress);
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }

            return pissFilterValue;
        }
        /*
               // The byte before this AOB is the instruction value for what changes the filter when a new area loads
               // All wee do here is change 48 into 90 to disable the instruction
               // ADD/2781 bytes after this is the instructions for it writing a value to the filter and checking if the
               // correct value is there changing F3 0F 11 99 78 03 00 00 to 90 90 90 90 90 90 90 90 will allow a checkbox
               // to permanently disable the piss filter or turn it back on
               "PissFilterInstructions", // C7 81 74 03 00 00 00 00 7F 43
               (new byte[] { 0xC7, 0x81, 0x74, 0x03, 0x00, 0x00, 0x00, 0x00, 0x7F, 0x43 },
                   "x x x x x x x x x x",
                   new IntPtr(0x10000),
                   new IntPtr(0xF0000)
               )
           },

         */

        public void DisablePissFilterInstructions()
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return;

                IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilterInstructions");
                if (aobResult == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to find Piss Filter Instructions AOB pattern.");
                    return;
                }

                // Disable the instruction at -1 offset
                IntPtr targetAddress1 = IntPtr.Subtract(aobResult, 1);
                // 48 original byte
                MemoryManager.WriteByteValueToMemory(targetAddress1, 0x90);

                // Disable the second part at +2781 offset
                IntPtr targetAddress2 = IntPtr.Add(aobResult, 2781);
                // F3 0F 11 99 78 03 00 00 original bytes
                byte[] disableBytes2 = new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
                MemoryManager.WriteBytesToMemory(targetAddress2, disableBytes2);

                LoggingManager.Instance.Log("Piss filter instructions disabled successfully.");
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        // Restore the piss filter instructions to their original values
        public void EnablePissFilterInstructions()
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null) return;

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero) return;

                IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilterInstructions");
                if (aobResult == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to find Piss Filter Instructions AOB pattern.");
                    return;
                }

                // Restore the instruction at -1 offset
                IntPtr targetAddress1 = IntPtr.Subtract(aobResult, 1);
                // 90 original byte
                MemoryManager.WriteByteValueToMemory(targetAddress1, 0x48);

                // Restore the second part at +2781 offset
                IntPtr targetAddress2 = IntPtr.Add(aobResult, 2781);
                // 90 90 90 90 90 90 90 90 original bytes
                byte[] restoreBytes2 = new byte[] { 0xF3, 0x0F, 0x11, 0x99, 0x78, 0x03, 0x00, 0x00 };
                MemoryManager.WriteBytesToMemory(targetAddress2, restoreBytes2);

                LoggingManager.Instance.Log("Piss filter instructions restored successfully.");
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        // Check if the piss filter instructions are disabled

        public bool IsPissFilterInstructionsNopped()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null) return false;

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero) return false;

            IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilterInstructions");
            if (aobResult == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to find Piss Filter Instructions AOB pattern.");
                return false;
            }

            // Check the first instruction at -1 offset
            IntPtr targetAddress1 = IntPtr.Subtract(aobResult, 1);
            byte[] buffer1 = new byte[1];
            if (!MemoryManager.ReadProcessMemory(processHandle, targetAddress1, buffer1, 1, out _))
            {
                return false;
            }

            // Check the second instruction at +2781 offset
            IntPtr targetAddress2 = IntPtr.Add(aobResult, 2781);
            byte[] buffer2 = new byte[8];
            if (!MemoryManager.ReadProcessMemory(processHandle, targetAddress2, buffer2, 8, out _))
            {
                return false;
            }

            return buffer1[0] == 0x90 && buffer2[0] == 0x90 && buffer2[1] == 0x90 && buffer2[2] == 0x90 &&
                   buffer2[3] == 0x90 && buffer2[4] == 0x90 && buffer2[5] == 0x90 && buffer2[6] == 0x90 &&
                   buffer2[7] == 0x90;
        }

        // Display the memory address and value of the first instruction of the piss filter

        public (IntPtr, byte) GetPissFilterInstructionsAddress()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null) return (IntPtr.Zero, 0x00);

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero) return (IntPtr.Zero, 0x00);

            IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilterInstructions");
            if (aobResult == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to find Piss Filter Instructions AOB pattern.");
                return (IntPtr.Zero, 0x00);
            }

            // Check the first instruction at -1 offset
            IntPtr targetAddress1 = IntPtr.Subtract(aobResult, 1);
            byte buffer1 = MemoryManager.ReadByteFromMemory(processHandle, targetAddress1);

            return (targetAddress1, buffer1);
        }


        // Display the memory address and value of the second instruction of the piss filter and read the entire 8 bytes
        // i.e. is it's 0xF3, 0x0F, 0x11, 0x99, 0x78, 0x03, 0x00, 0x00 or 0x90 0x90 0x90 0x90 0x90 0x90 0x90 0x90
        // // byte[] buffer = ReadMemoryBytes(processHandle, address, 8); to read the 8 bytes

        public (IntPtr, byte[]) GetPissFilterInstructionsAddress2()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null) return (IntPtr.Zero, new byte[8]);

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero) return (IntPtr.Zero, new byte[8]);

            IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilterInstructions");
            if (aobResult == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to find Piss Filter Instructions AOB pattern.");
                return (IntPtr.Zero, new byte[8]);
            }

            // Check the second instruction at +2781 offset
            IntPtr targetAddress2 = IntPtr.Add(aobResult, 2781);
            byte[] buffer2 = MemoryManager.ReadMemoryBytes(processHandle, targetAddress2, 8);

            return (targetAddress2, buffer2);
        }

        // Method to get the address and value for the first instruction
        public string GetPissFilterInstructionsDetails()
        {
            var (address, value) = GetPissFilterInstructionsAddress();
            return $"First Instruction Address: {address.ToInt64():X8}, Value: {value:X2}";
        }

        // Method to get the address and values for the second instruction
        public string GetPissFilterInstructionsDetails2()
        {
            var (address, values) = GetPissFilterInstructionsAddress2();
            return
                $"Second Instruction Address: {address.ToInt64():X8}, Values: {BitConverter.ToString(values).Replace("-", " ")}";
        }
    }
}