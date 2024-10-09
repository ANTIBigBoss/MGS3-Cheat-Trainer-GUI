using System;
using System.Diagnostics;
using System.Windows.Forms; // For MessageBox
using static MGS3_MC_Cheat_Trainer.Constants;

// Use MemoryManager
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public class FilterManager
    {
        private static FilterManager instance;
        private static readonly object lockObj = new object();

        private FilterManager()
        {
        }

        public static FilterManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new FilterManager();
                    }

                    return instance;
                }
            }
        }

        #region Piss Filter

        public void DisablePissFilter()
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                processHandle = HelperFunctions.Instance.GetProcessHandle();
                if (processHandle == IntPtr.Zero) return;

                IntPtr targetAddress = HelperFunctions.Instance.GetTargetAddress(processHandle, "PissFilter", 5525);
                if (targetAddress == IntPtr.Zero) return;

                byte[] newFilterValue = { 0x44 };
                if (WriteMemory(processHandle, targetAddress, newFilterValue))
                {
                    LoggingManager.Instance.Log("Piss filter disabled successfully.");
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to disable the piss filter.");
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        public void EnablePissFilter()
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                processHandle = HelperFunctions.Instance.GetProcessHandle();
                if (processHandle == IntPtr.Zero) return;

                IntPtr targetAddress = HelperFunctions.Instance.GetTargetAddress(processHandle, "PissFilter", 5525);
                if (targetAddress == IntPtr.Zero) return;

                byte[] defaultFilterValue = { 0x00 };
                if (WriteMemory(processHandle, targetAddress, defaultFilterValue))
                {
                    LoggingManager.Instance.Log("Piss filter restored successfully.");
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to restore the piss filter.");
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        public byte ReadPissFilterValue()
        {
            IntPtr processHandle = IntPtr.Zero;
            byte pissFilterValue = 0x00;

            try
            {
                processHandle = HelperFunctions.Instance.GetProcessHandle();
                if (processHandle == IntPtr.Zero) return pissFilterValue;

                IntPtr targetAddress = HelperFunctions.Instance.GetTargetAddress(processHandle, "PissFilter", 5525);
                if (targetAddress == IntPtr.Zero) return pissFilterValue;

                byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, targetAddress, 1);
                if (buffer != null && buffer.Length > 0)
                {
                    pissFilterValue = buffer[0];
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to read memory at the target address for Piss Filter.");
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }

            return pissFilterValue;
        }

        public void DisablePissFilterInstructions()
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                processHandle = HelperFunctions.Instance.GetProcessHandle();
                if (processHandle == IntPtr.Zero) return;

                IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilterInstructions");
                if (aobResult == IntPtr.Zero) return;

                IntPtr targetAddress1 = IntPtr.Subtract(aobResult, 1);
                byte[] nop1 = { 0x90 };
                bool success1 = WriteMemory(processHandle, targetAddress1, nop1);

                IntPtr targetAddress2 = IntPtr.Add(aobResult, 2781);
                byte[] disableBytes2 = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
                bool success2 = WriteMemory(processHandle, targetAddress2, disableBytes2);

                if (success1 && success2)
                {
                    LoggingManager.Instance.Log("Piss filter instructions disabled successfully.");
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to disable the piss filter instructions.");
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        public void EnablePissFilterInstructions()
        {
            IntPtr processHandle = IntPtr.Zero;

            try
            {
                processHandle = HelperFunctions.Instance.GetProcessHandle();
                if (processHandle == IntPtr.Zero) return;

                IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilterInstructions");
                if (aobResult == IntPtr.Zero) return;

                IntPtr targetAddress1 = IntPtr.Subtract(aobResult, 1);
                byte[] originalByte1 = { 0x48 };
                bool success1 = WriteMemory(processHandle, targetAddress1, originalByte1);

                IntPtr targetAddress2 = IntPtr.Add(aobResult, 2781);
                byte[] restoreBytes2 = { 0xF3, 0x0F, 0x11, 0x99, 0x78, 0x03, 0x00, 0x00 };
                bool success2 = WriteMemory(processHandle, targetAddress2, restoreBytes2);

                if (success1 && success2)
                {
                    LoggingManager.Instance.Log("Piss filter instructions restored successfully.");
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to restore the piss filter instructions.");
                }
            }
            finally
            {
                if (processHandle != IntPtr.Zero) MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        public bool IsPissFilterInstructionsNopped()
        {
            IntPtr processHandle = HelperFunctions.Instance.GetProcessHandle();
            if (processHandle == IntPtr.Zero) return false;

            IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilterInstructions");
            if (aobResult == IntPtr.Zero) return false;

            IntPtr targetAddress1 = IntPtr.Subtract(aobResult, 1);
            byte[] expectedBytes1 = { 0x90 };
            bool isFirstNopped = HelperFunctions.Instance.VerifyMemory(processHandle, targetAddress1, expectedBytes1);

            IntPtr targetAddress2 = IntPtr.Add(aobResult, 2781);
            byte[] expectedBytes2 = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
            bool isSecondNopped = HelperFunctions.Instance.VerifyMemory(processHandle, targetAddress2, expectedBytes2);

            MemoryManager.NativeMethods.CloseHandle(processHandle);

            return isFirstNopped && isSecondNopped;
        }

        public (IntPtr, byte) GetPissFilterInstructionsAddress()
        {
            IntPtr processHandle = HelperFunctions.Instance.GetProcessHandle();
            if (processHandle == IntPtr.Zero) return (IntPtr.Zero, 0x00);

            IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilterInstructions");
            if (aobResult == IntPtr.Zero) return (IntPtr.Zero, 0x00);

            IntPtr targetAddress = IntPtr.Subtract(aobResult, 1);
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, targetAddress, 1);
            if (buffer == null || buffer.Length == 0) return (targetAddress, 0x00);

            MemoryManager.NativeMethods.CloseHandle(processHandle);

            return (targetAddress, buffer[0]);
        }

        public (IntPtr, byte[]) GetPissFilterInstructionsAddress2()
        {
            IntPtr processHandle = HelperFunctions.Instance.GetProcessHandle();
            if (processHandle == IntPtr.Zero) return (IntPtr.Zero, new byte[8]);

            IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilterInstructions");
            if (aobResult == IntPtr.Zero) return (IntPtr.Zero, new byte[8]);

            IntPtr targetAddress2 = IntPtr.Add(aobResult, 2781);
            byte[] buffer2 = MemoryManager.ReadMemoryBytes(processHandle, targetAddress2, 8);

            MemoryManager.NativeMethods.CloseHandle(processHandle);

            return (targetAddress2, buffer2);
        }

        public string GetPissFilterInstructionsDetails()
        {
            var (address, value) = GetPissFilterInstructionsAddress();
            return $"First Instruction Address: {address.ToInt64():X8}, Value: {value:X2}";
        }

        public string GetPissFilterInstructionsDetails2()
        {
            var (address, values) = GetPissFilterInstructionsAddress2();
            return $"Second Instruction Address: {address.ToInt64():X8}, Values: {BitConverter.ToString(values).Replace("-", " ")}";
        }

        #endregion

        #region Time of Day

        public void SetToDayMode()
        {
            SetTimeOfDayValues(
                new byte[] { 0x40, 0x9C, 0xC5 }, // Light Near Snake
                new byte[] { 0x00, 0x5E, 0x72, 0x64 }, // Map Colour
                new byte[] { 0x00, 0xFF, 0xFF, 0x44 }, // Sky Colour
                0x05); // Sky Value
        }

        public void SetToNightMode()
        {
            SetTimeOfDayValues(
                new byte[] { 0x40, 0x1C, 0xC8 }, // Light Near Snake
                new byte[] { 0x00, 0x1B, 0x0A, 0x03 }, // Map Colour
                new byte[] { 0x00, 0x1F, 0x13, 0x09 }, // Sky Colour
                0x0F); // Sky Value
        }

        private void SetTimeOfDayValues(byte[] lightNearSnake, byte[] colourMap, byte[] skyColour, byte skyByte)
        {
            IntPtr processHandle = HelperFunctions.Instance.GetProcessHandle();
            if (processHandle == IntPtr.Zero) return;

            try
            {
                // Subtract offsets to target correct addresses, mimicking reading logic
                WriteTimeOfDayMemory(processHandle, "PissFilter", (int)TimeOfDayOffsets.ChangeLightNearSnakeSub, lightNearSnake);
                WriteTimeOfDayMemory(processHandle, "PissFilter", (int)TimeOfDayOffsets.ColourMapChangingSub, colourMap);
                WriteTimeOfDayMemory(processHandle, "PissFilter", (int)TimeOfDayOffsets.SkyColourChangingSub, skyColour);
                WriteTimeOfDayMemory(processHandle, "PissFilter", (int)TimeOfDayOffsets.SkyChangingSub, new byte[] { skyByte });
            }
            finally
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
        }

        private void WriteTimeOfDayMemory(IntPtr processHandle, string patternName, int offset, byte[] bytes)
        {
            IntPtr baseAddress = HelperFunctions.Instance.GetTargetAddress(processHandle, patternName, 0);
            IntPtr targetAddress = IntPtr.Subtract(baseAddress, offset);

            if (targetAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log($"Failed to find or access {patternName} at offset {offset}.");
                return;
            }

            LoggingManager.Instance.Log($"Attempting to write to {patternName} at address: {targetAddress.ToString("X")}, Offset: {offset}, Data: {BitConverter.ToString(bytes)}");

            if (HelperFunctions.Instance.WriteMemory(processHandle, targetAddress, bytes))
            {
                LoggingManager.Instance.Log($"Successfully wrote memory for {patternName} at address: {targetAddress.ToString("X")} with data: {BitConverter.ToString(bytes)}");
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to write memory for {patternName} at address: {targetAddress.ToString("X")}");
            }
        }

        #endregion

    }
}