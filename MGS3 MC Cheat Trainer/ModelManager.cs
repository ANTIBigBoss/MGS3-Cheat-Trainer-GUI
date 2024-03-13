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
                IntPtr foundAddress = AobManager.Instance.FindAOBInModelRange(pattern, mask);
                return foundAddress;
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

            byte[] buffer = new byte[] { value };

            IntPtr foundAddress = FindModelAddress("ModelDistortion");

            if (foundAddress == IntPtr.Zero)
            {
                return;
            }

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
                IntPtr foundAddress = AobManager.Instance.FindAOBInCameraRange(pattern, mask);
                return foundAddress;
            }
            catch (Exception ex)
            {
                return IntPtr.Zero;
            }
        }
    }
}