using System;
using System.Diagnostics;
using System.Windows.Forms; // For MessageBox
using static MGS3_MC_Cheat_Trainer.Constants;
using static MGS3_MC_Cheat_Trainer.HelperMethods;

// Use MemoryManager
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public class MiscManager
    {
        private static MiscManager instance;
        private static readonly object lockObj = new object();

        private MiscManager()
        {
        }

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
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                // Ensure the game process is running and accessible
                Process process = MemoryManager.GetMGS3Process();
                if (process == null)
                {
                    CustomMessageBoxManager.CustomMessageBox("Game process not found.", "Error");
                    return;
                }

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero)
                {
                    CustomMessageBoxManager.CustomMessageBox("Failed to open game process.", "Error");
                    return;
                }

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
                    bool writeSuccess = MemoryManager.WriteMemory(processHandle, cameraAddress, cameraSetting);
                    if (!writeSuccess)
                    {
                        CustomMessageBoxManager.CustomMessageBox("Failed to change the camera setting.", "Write Error");
                    }
                }
                else
                {
                    CustomMessageBoxManager.CustomMessageBox("Failed to find the camera in the game's memory.",
                        "Error");
                }
            }
            finally
            {
                // Always close the process handle if it was opened
                if (processHandle != IntPtr.Zero)
                {
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
            }
        }

        #region Camo Index Functions

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
                if (process == null)
                {
                    LoggingManager.Instance.Log("Failed to find the game process.");
                    return;
                }

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to open the game process.");
                    return;
                }

                // Hardcoded address for the camo index for the time being
                IntPtr baseAddress = process.MainModule.BaseAddress;
                IntPtr offset = new IntPtr(0x1E14C24); // Example offset, replace with actual
                IntPtr targetAddress = IntPtr.Add(baseAddress, offset.ToInt32());

                // Use the generic WriteMemory<T> method to write the integer value
                bool result = MemoryManager.WriteMemory(processHandle, targetAddress, newValue);
                if (!result)
                {
                    LoggingManager.Instance.Log("Failed to write the new camo index value.");
                }
                else
                {
                    LoggingManager.Instance.Log(
                        $"Successfully wrote new camo index value: {newValue} at {targetAddress.ToString("X")}");
                }
            }
            finally
            {
                // Always close the handle if it was opened
                if (processHandle != IntPtr.Zero)
                {
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
            }
        }


        public int ReadCamoIndex()
        {
            IntPtr processHandle = IntPtr.Zero;
            int camoIndexValue = 0;

            try
            {
                Process process = MemoryManager.GetMGS3Process();
                if (process == null)
                {
                    LoggingManager.Instance.Log("Process not found.");
                    return camoIndexValue; // Returning default value if process not found
                }

                processHandle = MemoryManager.OpenGameProcess(process);
                if (processHandle == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to open game process.");
                    return camoIndexValue; // Returning default value if unable to open process
                }

                IntPtr baseAddress = process.MainModule.BaseAddress;
                IntPtr offset = new IntPtr(0x1E14C24); // Adjust the offset as necessary
                IntPtr targetAddress = IntPtr.Add(baseAddress, offset.ToInt32());

                // Using ReadMemoryBytes to encapsulate reading process
                byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, targetAddress, 4);
                if (buffer != null && buffer.Length == 4)
                {
                    camoIndexValue = BitConverter.ToInt32(buffer, 0);
                    // Optionally log the read value for debugging
                    // LoggingManager.Instance.Log($"Current camo index value: {camoIndexValue}");
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to read camo index from memory.");
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
            }

            return camoIndexValue;
        }

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

        #endregion

        #region FOV Slider

        public void SetFovSlider(float newFovValue)
        {
            IntPtr processHandle = HelperMethods.Instance.GetProcessHandle();
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to get game process for setting FOV.");
                return;
            }

            try
            {
                IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, "FovSlider", (int)MiscOffsets.FovOffsetSub);
                if (targetAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to find FOV AOB pattern.");
                    return;
                }

                bool result = WriteMemory(processHandle, targetAddress, BitConverter.GetBytes(newFovValue));
                LoggingManager.Instance.Log(result ? "FOV value set successfully." : "Failed to write the new FOV value.");
            }
            finally
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        public float ReadFovSlider()
        {
            IntPtr processHandle = HelperMethods.Instance.GetProcessHandle();
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to get game process for reading FOV.");
                return 0.0f;
            }

            try
            {
                IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, "FovSlider", (int)MiscOffsets.FovOffsetSub);
                if (targetAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to find FOV AOB pattern.");
                    return 0.0f;
                }

                byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, targetAddress, sizeof(float));
                if (buffer == null || buffer.Length != sizeof(float))
                {
                    LoggingManager.Instance.Log("Failed to read FOV value from memory.");
                    return 0.0f;
                }

                return BitConverter.ToSingle(buffer, 0);
            }
            finally
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        #endregion

        #region BatteryDrain

        public bool IsBatteryDrainNOP()
        {
            IntPtr processHandle = HelperMethods.Instance.GetProcessHandle();
            if (processHandle == IntPtr.Zero) return false;

            IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, "BatteryDrain", (int)MiscOffsets.BatteryDrainInstructionsSub);
            if (targetAddress == IntPtr.Zero) return false;

            return HelperMethods.Instance.VerifyMemory(processHandle, targetAddress, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
        }

        public void DisableBatteryDrain()
        {
            ModifyBatteryDrain(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 }, "disable");
        }

        public bool IsBatteryDrainNormal()
        {
            IntPtr processHandle = HelperMethods.Instance.GetProcessHandle();
            if (processHandle == IntPtr.Zero) return false;

            IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, "BatteryDrain", (int)MiscOffsets.BatteryDrainInstructionsSub);
            if (targetAddress == IntPtr.Zero) return false;

            return HelperMethods.Instance.VerifyMemory(processHandle, targetAddress, new byte[] { 0x66, 0x29, 0x88, 0x4E, 0x0A, 0x00, 0x00 });
        }

        public void EnableBatteryDrain()
        {
            ModifyBatteryDrain(new byte[] { 0x66, 0x29, 0x88, 0x4E, 0x0A, 0x00, 0x00 }, "enable");
        }

        private void ModifyBatteryDrain(byte[] bytes, string action)
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                processHandle = HelperMethods.Instance.GetProcessHandle();
                if (processHandle == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log($"Failed to {action} the battery drain. Game process not found.");
                    return;
                }

                IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, "BatteryDrain", (int)MiscOffsets.BatteryDrainInstructionsSub);
                if (targetAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log($"Failed to {action} the battery drain. AOB pattern not found.");
                    return;
                }

                bool success = WriteMemory(processHandle, targetAddress, bytes);
                LoggingManager.Instance.Log(success ? $"Battery drain {action}d successfully." : $"Failed to {action} the battery drain.");
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        #endregion

        #region Infinite Ammo and Reload

        public bool IsInfiniteAmmoEnabled()
        {
            return CheckAmmoStatus(new byte[] { 0x90, 0x90, 0x90, 0x90 });
        }

        public void EnableInfAmmoAndReload()
        {
            ModifyAmmoAndReload(new byte[] { 0x90, 0x90, 0x90, 0x90 }, "enable");
        }

        public bool IsAmmoAndReloadFinite()
        {
            return CheckAmmoStatus(new byte[] { 0x0F, 0xB7, 0x41, 0x28 });
        }

        public void DisableInfAmmoAndReload()
        {
            ModifyAmmoAndReload(new byte[] { 0x0F, 0xB7, 0x41, 0x28 }, "disable");
        }

        private bool CheckAmmoStatus(byte[] expectedBytes)
        {
            IntPtr processHandle = HelperMethods.Instance.GetProcessHandle();
            if (processHandle == IntPtr.Zero) return false;

            IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, "InfAmmoNoReload", (int)MiscOffsets.InfiniteAmmoAndReloadSub);
            if (targetAddress == IntPtr.Zero) return false;

            return HelperMethods.Instance.VerifyMemory(processHandle, targetAddress, expectedBytes);
        }

        private void ModifyAmmoAndReload(byte[] bytes, string action)
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                processHandle = HelperMethods.Instance.GetProcessHandle();
                if (processHandle == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log($"Failed to {action} Infinite Ammo and Reload. Game process not found.");
                    return;
                }

                IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, "InfAmmoNoReload", (int)MiscOffsets.InfiniteAmmoAndReloadSub);
                if (targetAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log($"Failed to {action} Infinite Ammo and Reload. AOB pattern not found.");
                    return;
                }

                bool success = WriteMemory(processHandle, targetAddress, bytes);
                LoggingManager.Instance.Log(success ? $"Successfully {action}d Infinite Ammo and Reload." : $"Failed to {action} Infinite Ammo and Reload.");
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        #endregion

        #region HUD Control

        // Annoyingly these are backwards for partial and full hud disables
        public void PartialDisableHUD()
        {
            ModifyHUD(0x01, "disable");
        }

        public void EnableHUD()
        {
            ModifyHUD(0x00, "enable");
        }

        public void FullDisableHUD()
        {
            ModifyFullHUD(0x00, "disable");
        }

        public void EnableHUDBackFromFull()
        {
            ModifyFullHUD(0x01, "enable");
        }

        private void ModifyHUD(byte hudValue, string action)
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                processHandle = HelperMethods.Instance.GetProcessHandle();
                if (processHandle == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log($"Failed to {action} the HUD. Game process not found.");
                    return;
                }

                IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, "PissFilter", (int)MiscOffsets.NoHudPartialSub);
                if (targetAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log($"Failed to {action} the HUD. AOB pattern not found.");
                    return;
                }

                bool success = WriteMemory(processHandle, targetAddress, new byte[] { hudValue });
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        private void ModifyFullHUD(byte hudValue, string action)
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                processHandle = HelperMethods.Instance.GetProcessHandle();
                if (processHandle == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log($"Failed to {action} the HUD. Game process not found.");
                    return;
                }

                IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, "PissFilter", (int)MiscOffsets.NoHudFullSub);
                if (targetAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log($"Failed to {action} the HUD. AOB pattern not found.");
                    return;
                }

                bool success = WriteMemory(processHandle, targetAddress, new byte[] { hudValue });
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        #endregion

        #region Damage Multiplier Functions

        
        /// <summary>
        /// Enables Snake's Damage Multiplier.
        /// </summary>
        public static void EnableDamageMultiplier()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("Failed to find the game process.");
                return;
            }

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open game process.");
                return;
            }

            try
            {
                // Locate CamoOperations
                IntPtr camoOperationsAddress = MemoryManager.Instance.FindAob("CamoOperations");
                if (camoOperationsAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to locate CamoOperations AOB pattern.");
                    return;
                }

                // Instruction Address
                IntPtr instructionAddress = IntPtr.Subtract(camoOperationsAddress, 138786);
                IntPtr shortValueAddress = IntPtr.Add(instructionAddress, 2);

                // Enable Instruction
                byte[] enableBytes = new byte[] { 0x66, 0xBD, 0x0A, 0x00, 0x66, 0x0F, 0xAF, 0xCD, 0x90 };
                bool success = MemoryManager.WriteMemory(processHandle, instructionAddress, enableBytes);

                // Validate Short Value
                byte[] currentShortBytes = MemoryManager.ReadMemoryBytes(processHandle, shortValueAddress, 2);
                ushort shortValue = BitConverter.ToUInt16(currentShortBytes, 0);

                if (shortValue > 100 || shortValue == 0)
                {
                    LoggingManager.Instance.Log($"Invalid short value ({shortValue}), resetting to 10.");
                    MemoryManager.WriteMemory(processHandle, shortValueAddress, new byte[] { 0x0A, 0x00 }); // Default to 10
                }

                LoggingManager.Instance.Log(success
                    ? "Damage Multiplier successfully enabled."
                    : "Failed to enable Damage Multiplier.");
            }
            finally
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        /// <summary>
        /// Disables Snake's Damage Multiplier.
        /// </summary>
        public static void DisableDamageMultiplier()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("Failed to find the game process.");
                return;
            }

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open game process.");
                return;
            }

            try
            {
                // Locate CamoOperations
                IntPtr camoOperationsAddress = MemoryManager.Instance.FindAob("CamoOperations");
                if (camoOperationsAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to locate CamoOperations AOB pattern.");
                    return;
                }

                // Instruction Address
                IntPtr instructionAddress = IntPtr.Subtract(camoOperationsAddress, 138786);

                // Disable Instruction
                byte[] disableBytes = new byte[] { 0xF6, 0x05, 0x08, 0xF1, 0xAD, 0x01, 0x03, 0x75, 0x57 };
                bool success = MemoryManager.WriteMemory(processHandle, instructionAddress, disableBytes);

                LoggingManager.Instance.Log(success
                    ? "Damage Multiplier successfully disabled."
                    : "Failed to disable Damage Multiplier.");
            }
            finally
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        /// <summary>
        /// Adjusts Snake's Damage Multiplier short value.
        /// </summary>
        public static void AdjustDamageMultiplier(byte multiplierValue)
        {
            if (multiplierValue < 1 || multiplierValue > 100)
            {
                LoggingManager.Instance.Log("Invalid multiplier value. Must be between 1 and 100.");
                return;
            }

            Process process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("Failed to find the game process.");
                return;
            }

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open game process.");
                return;
            }

            try
            {
                // Locate CamoOperations
                IntPtr camoOperationsAddress = MemoryManager.Instance.FindAob("CamoOperations");
                if (camoOperationsAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to locate CamoOperations AOB pattern.");
                    return;
                }

                // Address of the short multiplier
                IntPtr shortValueAddress = IntPtr.Subtract(camoOperationsAddress, 138784);

                byte[] multiplierBytes = BitConverter.GetBytes((ushort)multiplierValue);
                bool success = MemoryManager.WriteMemory(processHandle, shortValueAddress, multiplierBytes);

                LoggingManager.Instance.Log(success
                    ? $"Damage Multiplier value set to {multiplierValue}."
                    : "Failed to set Damage Multiplier value.");
            }
            finally
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        /// <summary>
        /// Reads the current Snake Damage Multiplier.
        /// </summary>
        public static ushort ReadDamageMultiplier()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("Failed to find the game process.");
                return 0;
            }

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open game process.");
                return 0;
            }

            try
            {
                IntPtr camoOperationsAddress = MemoryManager.Instance.FindAob("CamoOperations");
                if (camoOperationsAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to locate CamoOperations AOB pattern.");
                    return 0;
                }

                IntPtr shortValueAddress = IntPtr.Subtract(camoOperationsAddress, 138784);

                byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, shortValueAddress, 2);
                return buffer != null ? BitConverter.ToUInt16(buffer, 0) : (ushort)0;
            }
            finally
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        /// <summary>
        /// Checks if Snake's Damage Multiplier is active.
        /// </summary>
        public static bool IsDamageMultiplierActive()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("Failed to find the game process.");
                return false;
            }

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open game process.");
                return false;
            }

            try
            {
                // Find CamoOperations AOB
                IntPtr camoOperationsAddress = MemoryManager.Instance.FindAob("CamoOperations");
                if (camoOperationsAddress == IntPtr.Zero)
                {
                    LoggingManager.Instance.Log("Failed to locate CamoOperations AOB pattern.");
                    return false;
                }

                // Address of the full instruction
                IntPtr instructionAddress = IntPtr.Subtract(camoOperationsAddress, 138786);
                // Address of the embedded short multiplier
                IntPtr shortValueAddress = IntPtr.Add(instructionAddress, 2);

                // Read the full instruction (8 bytes)
                byte[] instructionBuffer = MemoryManager.ReadMemoryBytes(processHandle, instructionAddress, 8);
                if (instructionBuffer == null || instructionBuffer.Length != 8)
                {
                    LoggingManager.Instance.Log("Failed to read Damage Multiplier instruction bytes.");
                    return false;
                }

                // Check if the static parts of the instruction match
                bool instructionMatches =
                    instructionBuffer[0] == 0x66 &&
                    instructionBuffer[1] == 0xBD &&
                    instructionBuffer[4] == 0x66 &&
                    instructionBuffer[5] == 0x0F &&
                    instructionBuffer[6] == 0xAF &&
                    instructionBuffer[7] == 0xCD;

                if (!instructionMatches)
                {
                    LoggingManager.Instance.Log("Instruction pattern does not match the active Damage Multiplier.");
                    return false;
                }

                // Validate the short value (XX XX)
                byte[] shortValueBuffer = MemoryManager.ReadMemoryBytes(processHandle, shortValueAddress, 2);
                if (shortValueBuffer == null || shortValueBuffer.Length != 2)
                {
                    LoggingManager.Instance.Log("Failed to read Damage Multiplier short value.");
                    return false;
                }

                ushort shortValue = BitConverter.ToUInt16(shortValueBuffer, 0);
                if (shortValue > 100 || shortValue == 0)
                {
                    LoggingManager.Instance.Log($"Invalid Damage Multiplier short value detected: {shortValue} (Expected 1-100).");
                    return false;
                }

                LoggingManager.Instance.Log($"Damage Multiplier is active with a valid short value: {shortValue}");
                return true;
            }
            finally
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }


        #endregion

    }
}