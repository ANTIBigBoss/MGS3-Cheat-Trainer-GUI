using static MGS3_MC_Cheat_Trainer.MemoryManager;
using static MGS3_MC_Cheat_Trainer.Constants;
using System.Diagnostics;
using System.Windows.Forms; // Ensure you have this using directive for MessageBox

namespace MGS3_MC_Cheat_Trainer
{
    internal class ModelManager
    {

        internal static void ModifyModel(Constants.MGS3Distortion model, byte value)
        {
            Process process;

            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                MessageBox.Show($"Cannot find process: {Constants.PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            int bytesWritten;

            byte[] buffer = new byte[] { value }; // Value to write
            IntPtr targetAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)model.ModelManipulationOffset); // Adjusted to add base address

            bool success = NativeMethods.WriteProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out bytesWritten);

            if (!success || bytesWritten != buffer.Length)
            {
                MessageBox.Show($"Failed to write memory for model {model.Name} with value {value}.");
            }

            NativeMethods.CloseHandle(processHandle);
        }
        internal static void TriggerSnakeAnimation(Constants.SnakeAnimation snakeAnimation)
        {
            ModifyByteValueObject(snakeAnimation.AnimationOffset, snakeAnimation.Value);
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
                MemoryManager.Instance.WriteByteValueToMemory(cameraAddress, cameraSetting);
            }
            else
            {
                MessageBox.Show("Failed to find camera address.");
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
                MessageBox.Show($"Error finding camera address: {ex.Message}");
                return IntPtr.Zero;
            }
        }



    }
}