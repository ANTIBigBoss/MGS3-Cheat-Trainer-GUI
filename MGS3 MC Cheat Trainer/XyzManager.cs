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

        private XyzManager()
        {
        }

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

            // 3 for X, Y, Z each is a float
            float[] position = new float[3]; 
            for (int i = 0; i < position.Length; i++)
            {
                IntPtr currentAddress = IntPtr.Add(baseAddress, i * 4);
                byte[] bytes = ReadMemoryBytes(processHandle, currentAddress, 4);
                if (bytes != null && bytes.Length == 4)
                {
                    position[i] = BitConverter.ToSingle(bytes, 0);
                }
                else
                {
                    position[i] = 0;
                }
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
                return;
            }

            foreach (var set in coordinates)
            {
                float x = set[0];
                float y = set[1];
                float z = set[2];
                TeleportToPosition(processHandle, x, y, z);
            }

            NativeMethods.CloseHandle(processHandle);
        }

        private void TeleportToPosition(IntPtr processHandle, float x, float y, float z)
        {
            float[] newPosition = new float[] { x, y, z };
            for (int i = 0; i < newPosition.Length; i++)
            {
                IntPtr currentAddress = IntPtr.Add(AobManager.Instance.FoundSnakePositionAddress, 7 + i * 4);
                bool success = MemoryManager.WriteMemory<float>(processHandle, currentAddress, newPosition[i]);
                if (!success)
                {
                    break;
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

            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                return;
            }

            // Read current position
            float[] currentPosition = XyzManager.Instance.ReadSnakePosition(processHandle);
            if (currentPosition == null || currentPosition.Length < 3)
            {
                LoggingManager.Instance.Log("Failed to read Snake's current position.");
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return;
            }

            // Increase Y by 4000 units
            float newY = currentPosition[1] + 4000f;

            // Prepare the address for Y coordinate
            IntPtr yAddress = IntPtr.Add(AobManager.Instance.FoundSnakePositionAddress, 7 + 4);

            // Use the generic WriteMemory method to write the new Y position
            bool success = MemoryManager.WriteMemory<float>(processHandle, yAddress, newY);
            if (success)
            {
                LoggingManager.Instance.Log($"Successfully raised Snake's Y position by 4000 units to {newY}.");
            }
            else
            {
                LoggingManager.Instance.Log("Failed to update Snake's Y position.");
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public List<IntPtr> FindAllGuardsPositionAOBs()
        {
            LoggingManager.Instance.Log("Searching for guards button clicked");
            Process process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                return new List<IntPtr>();
            }

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                return new List<IntPtr>();
            }

            if (!AobManager.AOBs.TryGetValue("GuardPatroling", out var aobData))
            {
                LoggingManager.Instance.Log("AOB not found for 'GuardPatroling'.");
                return new List<IntPtr>();
            }

            byte[] pattern = aobData.Pattern;
            string mask = aobData.Mask;
            IntPtr startOffset = aobData.StartOffset ?? IntPtr.Zero;
            IntPtr endOffset = aobData.EndOffset ?? new IntPtr(long.MaxValue);
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
                    IntPtr currentAddress = IntPtr.Add(baseAddress, 7 + i * 4);
                    byte[] bytes = ReadMemoryBytes(processHandle, currentAddress, 4);
                    if (bytes != null && bytes.Length == 4)
                    {
                        position[i] = BitConverter.ToSingle(bytes, 0);
                    }
                    else
                    {
                        position[i] = 0;
                    }
                }
                guardsPositions.Add(position);
            }
            return guardsPositions;
        }

        public void MoveGuardsToPosition(IntPtr processHandle, List<IntPtr> guardsAddresses, float[] newPosition)
        {
            foreach (IntPtr guardBaseAddress in guardsAddresses)
            {
                for (int i = 0; i < newPosition.Length; i++)
                {
                    IntPtr currentAddress = IntPtr.Add(guardBaseAddress, 7 + i * 4);
                    bool success = MemoryManager.WriteMemory<float>(processHandle, currentAddress, newPosition[i]);
                    if (!success)
                    {
                        LoggingManager.Instance.Log($"Failed to move guard at {guardBaseAddress.ToInt64():X} to new position {newPosition[i]} for coordinate index {i}.");
                        break;
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
