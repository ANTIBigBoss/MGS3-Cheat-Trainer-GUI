using static MGS3_MC_Cheat_Trainer.MemoryManager;
using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    internal class ModelManager
    {

        private static IntPtr FindModelAddress(string aobKey)
        {
            try
            {
                (byte[] pattern, string mask) = Constants.AOBs["ModelDistortion"];
                IntPtr foundAddress = MemoryManager.Instance.FindAOBInModelRange(pattern, mask);
                // Additional offset may be needed depending on the AOB's location relative to the actual value you want to modify
                // IntPtr targetAddress = IntPtr.Add(foundAddress, additionalOffset);
                return foundAddress; // or return targetAddress if additional offset is used
            }
            catch (Exception ex)
            {
                return IntPtr.Zero;
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

            byte[] buffer = new byte[] { value }; // Value to write

            // Use the "ModelDistortion" key from the Constants.AOBs dictionary
            IntPtr foundAddress = FindModelAddress("ModelDistortion");

            if (foundAddress == IntPtr.Zero)
            {
                return;
            }

            // Adjust the address to target the byte before the array
            IntPtr targetAddress = IntPtr.Subtract(foundAddress, 1); // One byte before the array

            bool success = NativeMethods.WriteProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out bytesWritten);

            if (!success || bytesWritten != buffer.Length)
            {
            }

            NativeMethods.CloseHandle(processHandle);
        }

        internal static void ChangeHud(byte hudStatus)
        {
            ModifyByteValueObject(Constants.HudOffset, hudStatus);
        }

        internal static void ChangeCamera(byte cameraSetting)
        {
            string aobKey = cameraSetting == (byte)Constants.CameraOptions.Normal ? "NotUpsideDownCamera" : "UpsideDownCamera";
            IntPtr cameraAddress = FindCameraAddress(aobKey);
            if (cameraAddress != IntPtr.Zero)
            {
                MemoryManager.WriteByteValueToMemory(cameraAddress, cameraSetting);
            }
            else
            {
            }
        }

        private static IntPtr FindCameraAddress(string aobKey)
        {
            try
            {
                (byte[] pattern, string mask) = Constants.AOBs[aobKey];
                IntPtr foundAddress = MemoryManager.Instance.FindAOBInCameraRange(pattern, mask);
                // Additional offset may be needed depending on the AOB's location relative to the actual value you want to modify
                // IntPtr targetAddress = IntPtr.Add(foundAddress, additionalOffset);
                return foundAddress; // or return targetAddress if additional offset is used
            }
            catch (Exception ex)
            {
                return IntPtr.Zero;
            }
        }



    }
}