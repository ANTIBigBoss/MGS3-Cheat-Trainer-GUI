using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    internal class XyzManager
    {
        private static XyzManager instance;
        private static readonly object lockObj = new object();


        // Private constructor to prevent external instantiation
        private XyzManager()
        {
        }

        // Public property to access the instance
        public static XyzManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new XyzManager();
                    }

                    return instance;
                }
            }
        }

        public float[] ReadSnakePosition(IntPtr processHandle)
        {
            IntPtr baseAddress = IntPtr.Add(AobManager.Instance.FoundSnakePositionAddress, 7);

            float[] position = new float[3];
            for (int i = 0; i < position.Length; i++)
            {
                IntPtr currentAddress = IntPtr.Add(baseAddress, i * 4); // 4 bytes per float
                position[i] = ReadFloatFromMemory(processHandle, currentAddress);
            }

            return position;
        }

        public void TeleportSnake(float[][] coordinates)
        {
            if (AobManager.Instance.FoundSnakePositionAddress == IntPtr.Zero && !AobManager.Instance.FindAndStoreSnakesPositionAOB())
            {
                LoggingManager.Instance.Log("Failed to find or verify Snake's position AOB.");
                return;
            }

            IntPtr processHandle = OpenGameProcess(GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process handle.");
                return;
            }

            foreach (var set in coordinates)
            {
                float x = set[0];
                float y = set[1];
                float z = set[2];
                // Execute the teleport to new position using the current set of coordinates
                // Note: Implement the TeleportToPosition method as previously described
                TeleportToPosition(processHandle, x, y, z);
            }

            NativeMethods.CloseHandle(processHandle);
        }


        private void TeleportToPosition(IntPtr processHandle, float x, float y, float z)
        {
            float[] newPosition = new float[] { x, y, z };
            for (int i = 0; i < newPosition.Length; i++)
            {
                IntPtr currentAddress = IntPtr.Add(AobManager.Instance.FoundSnakePositionAddress, 7 + i * 4); // Adjust the offset as necessary
                bool success = WriteFloatToMemory(processHandle, currentAddress, newPosition[i]);
                if (!success)
                {
                    LoggingManager.Instance.Log($"Failed to teleport Snake to new position: X={x}, Y={y}, Z={z}.");
                    break; // Exit if any teleport fails
                }
            }
        }

        public void RaiseSnakeYBy4000()
        {
            LoggingManager.Instance.Log("Raise Snake's Y button clicked");
            if (AobManager.Instance.FoundSnakePositionAddress == IntPtr.Zero && !AobManager.Instance.FindAndStoreSnakesPositionAOB())
            {
                LoggingManager.Instance.Log("Failed to find or verify Snake's position AOB.");
                return;
            }

            IntPtr processHandle = OpenGameProcess(GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process handle.");
                return;
            }

            // Read current position
            float[] currentPosition = ReadSnakePosition(processHandle);
            if (currentPosition == null || currentPosition.Length < 3)
            {
                LoggingManager.Instance.Log("Failed to read Snake's current position.");
                NativeMethods.CloseHandle(processHandle);
                return;
            }

            // Increase Y by 4000 units
            float newY = currentPosition[1] + 4000f;

            // Prepare the address for Y coordinate
            IntPtr yAddress = IntPtr.Add(AobManager.Instance.FoundSnakePositionAddress, 7 + 4); // Assuming Y is the second float after the AOB pattern

            // Write the new Y position
            bool success = WriteFloatToMemory(processHandle, yAddress, newY);
            if (success)
            {
                LoggingManager.Instance.Log($"Successfully raised Snake's Y position by 4000 units to {newY}.");
            }
            else
            {
                LoggingManager.Instance.Log("Failed to update Snake's Y position.");
            }

            NativeMethods.CloseHandle(processHandle);
        }

        public List<IntPtr> FindAllGuardsPositionAOBs()
        {
            LoggingManager.Instance.Log("Searching for guards button clicked");
            Process process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("Process not found.");
                return new List<IntPtr>();
            }

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process handle.");
                return new List<IntPtr>();
            }

            if (!AobManager.AOBs.TryGetValue("GuardPatroling", out var aobData))
            {
                LoggingManager.Instance.Log("AOB not found for 'GuardPatroling'.");
                return new List<IntPtr>();
            }

            byte[] pattern = aobData.Pattern;
            string mask = aobData.Mask;
            IntPtr startOffset = aobData.StartOffset ?? IntPtr.Zero; // Provide a default if null
            IntPtr endOffset = aobData.EndOffset ?? new IntPtr(long.MaxValue); // Provide a default if null
            long size = (long)(endOffset.ToInt64() - startOffset.ToInt64());

            var guardsAddresses = MemoryManager.Instance.ScanForAllInstances(processHandle, startOffset, size, pattern, mask);
            NativeMethods.CloseHandle(processHandle);

            return guardsAddresses;
        }

        public List<float[]> ReadGuardsPositions(IntPtr processHandle, List<IntPtr> guardsAddresses)
        {
            List<float[]> guardsPositions = new List<float[]>();
            foreach (IntPtr baseAddress in guardsAddresses)
            {
                float[] position = new float[3];
                for (int i = 0; i < position.Length; i++)
                {
                    IntPtr currentAddress = IntPtr.Add(baseAddress, 7 + i * 4); // Assuming the same offset and structure
                    position[i] = ReadFloatFromMemory(processHandle, currentAddress);
                }
                guardsPositions.Add(position);
            }
            return guardsPositions;
        }

        public void MoveGuardsToPosition(IntPtr processHandle, List<IntPtr> guardsAddresses, float[] newPosition)
        {
            foreach (IntPtr guardBaseAddress in guardsAddresses)
            {
                // Calculate the correct address offsets for X, Y, Z positions
                for (int i = 0; i < newPosition.Length; i++)
                {
                    IntPtr currentAddress = IntPtr.Add(guardBaseAddress, 7 + i * 4);
                    bool success = WriteFloatToMemory(processHandle, currentAddress, newPosition[i]);
                    if (!success)
                    {

                    }
                }
            }
        }

        public void MoveAllGuardsToSnake()
        {
            IntPtr processHandle = OpenGameProcess(GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {

                return;
            }

            AobManager.Instance.FindAndStoreSnakesPositionAOB();
            if (AobManager.Instance.FoundSnakePositionAddress == IntPtr.Zero)
            {

                NativeMethods.CloseHandle(processHandle);
                return;
            }

            float[] snakePosition = ReadSnakePosition(processHandle);
            var guardsAddresses = FindAllGuardsPositionAOBs();
            if (guardsAddresses.Count > 0)
            {
                MoveGuardsToPosition(processHandle, guardsAddresses, snakePosition);

            }
            else
            {

            }

            NativeMethods.CloseHandle(processHandle);
        }
    }
}
