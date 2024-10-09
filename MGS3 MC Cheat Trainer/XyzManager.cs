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

        // Variable to store the last known pointer address
        private IntPtr lastKnownPointerAddress = IntPtr.Zero;

        public IntPtr FindPointerMemory(int bytesBefore = 11810, int pointerOffset = 0x130)
        {
            // Find the last instance of the Alphabet AOB
            IntPtr alphabetAobAddress = MemoryManager.Instance.FindLastAob("Alphabet", "Alphabet");

            if (alphabetAobAddress == IntPtr.Zero)
            {
                return IntPtr.Zero; // Failed to find Alphabet AOB
            }

            // Calculate the base address (bytesBefore bytes before the found AOB)
            IntPtr baseAddress = IntPtr.Subtract(alphabetAobAddress, bytesBefore);

            var process = GetMGS3Process();
            if (process == null || process.MainModule == null)
            {
                return IntPtr.Zero;
            }

            // Open the process to read memory
            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }

            // Read the pointer at the base address
            IntPtr pointerValue = MemoryManager.Instance.ReadIntPtr(processHandle, baseAddress);
            NativeMethods.CloseHandle(processHandle);

            if (pointerValue == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }

            // Calculate the final pointer address by adding the offset (0x130)
            return IntPtr.Add(pointerValue, pointerOffset);
        }

        public float[] ReadSnakePosition(IntPtr processHandle)
        {
            // Find the base pointer address using the new method
            IntPtr snakePointerAddress = FindPointerMemory(11810, 0x130);

            // Only proceed if the pointer address is valid
            if (snakePointerAddress == IntPtr.Zero)
            {
                return new float[] { 0, 0, 0 }; // No valid pointer
            }

            // Read the first 3 floats (X, Y, Z) from this base address
            float[] position = new float[3];
            for (int i = 0; i < position.Length; i++)
            {
                IntPtr currentAddress = IntPtr.Add(snakePointerAddress, i * 4);
                byte[] bytes = MemoryManager.ReadMemoryBytes(processHandle, currentAddress, 4);
                if (bytes != null && bytes.Length == 4)
                {
                    position[i] = BitConverter.ToSingle(bytes, 0); // Convert bytes to float
                }
                else
                {
                    position[i] = 0; // Default if failed to read
                }
            }

            // Log the updated Snake's position and memory address only when the pointer changes
            if (snakePointerAddress != lastKnownPointerAddress)
            {
                lastKnownPointerAddress = snakePointerAddress;
                LoggingManager.Instance.Log($"Snake's position updated: X={position[0]}, Y={position[1]}, Z={position[2]}");
                LoggingManager.Instance.Log($"Snake's memory address: {snakePointerAddress.ToString("X")}");
            }

            return position;
        }

        public void TeleportSnake(float[][] coordinates)
        {
            // Find the base pointer for Snake's position
            IntPtr snakePointerAddress = FindPointerMemory(11810, 0x130);

            if (snakePointerAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to find Snake's pointer address.");
                return;
            }

            IntPtr processHandle = OpenGameProcess(GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open game process.");
                return;
            }

            // Teleport Snake to the new coordinates
            foreach (var set in coordinates)
            {
                float x = set[0];
                float y = set[1];
                float z = set[2];

                // Write the new positions (X, Y, Z)
                TeleportToPosition(processHandle, snakePointerAddress, x, y, z);
            }

            NativeMethods.CloseHandle(processHandle);
        }

        public void TeleportToPosition(IntPtr processHandle, IntPtr baseAddress, float x, float y, float z)
        {
            // Write X, Y, Z positions (each float is 4 bytes apart)
            MemoryManager.WriteMemory(processHandle, IntPtr.Add(baseAddress, 0), x);
            MemoryManager.WriteMemory(processHandle, IntPtr.Add(baseAddress, 4), y);
            MemoryManager.WriteMemory(processHandle, IntPtr.Add(baseAddress, 8), z);

            // Log only the changes (No repeated logs)
            LoggingManager.Instance.Log($"Snake teleported to X={x}, Y={y}, Z={z}");
        }

        public void RaiseSnakeYBy4000()
        {
            LoggingManager.Instance.Log("Raise Snake's Y button clicked");

            // Find the base pointer for Snake's position using the updated method
            IntPtr snakePointerAddress = XyzManager.Instance.FindPointerMemory(11810, 0x130);
            if (snakePointerAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to find Snake's pointer address.");
                return;
            }

            // Open the game process
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open game process.");
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

            // Prepare the address for Y coordinate (Y is stored at an offset of +4 from the base position address)
            IntPtr yAddress = IntPtr.Add(snakePointerAddress, 4);

            // Use the generic WriteMemory method to write the new Y position
            bool success = MemoryManager.WriteMemory<float>(processHandle, yAddress, newY);
            if (success)
            {
                LoggingManager.Instance.Log($"Successfully raised Snake's Y position by 4000 units to {newY}.");
                LoggingManager.Instance.Log($"Updated Y position memory address: {yAddress.ToString("X")}");
            }
            else
            {
                LoggingManager.Instance.Log("Failed to update Snake's Y position.");
            }

            // Close the process handle
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public List<IntPtr> FindAllGuardsPositionAOBs(IntPtr snakePointerAddress)
        {
            LoggingManager.Instance.Log("Searching for guards around Snake's position");

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

            // Set up a search range around Snake's current position for guards
            IntPtr startAddress = IntPtr.Subtract(snakePointerAddress, 0x500000); // Search backward from Snake's position
            IntPtr endAddress = IntPtr.Add(snakePointerAddress, 0x500000);       // Search forward from Snake's position
            long searchRangeSize = endAddress.ToInt64() - startAddress.ToInt64();

            // Scan for guards' AOB within the specified range
            var guardsAddresses = MemoryManager.Instance.ScanForAllInstances(processHandle, startAddress, searchRangeSize, aobData.Pattern, aobData.Mask);
            NativeMethods.CloseHandle(processHandle);

            LoggingManager.Instance.Log($"Found {guardsAddresses.Count} guards in range.");
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
                    IntPtr currentAddress = IntPtr.Add(baseAddress, 7 + i * 4); // X, Y, Z coordinates stored at offset 7
                    byte[] bytes = MemoryManager.ReadMemoryBytes(processHandle, currentAddress, 4);
                    if (bytes != null && bytes.Length == 4)
                    {
                        position[i] = BitConverter.ToSingle(bytes, 0); // Convert to float
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
                    IntPtr currentAddress = IntPtr.Add(guardBaseAddress, 7 + i * 4); // X, Y, Z coordinates stored at offset 7
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
                LoggingManager.Instance.Log("Failed to open game process.");
                return;
            }

            // Find Snake's position
            IntPtr snakePointerAddress = XyzManager.Instance.FindPointerMemory(11810, 0x130);
            if (snakePointerAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to find Snake's position.");
                NativeMethods.CloseHandle(processHandle);
                return;
            }

            // Read Snake's current position
            float[] snakePosition = XyzManager.Instance.ReadSnakePosition(processHandle);

            // Find guards' positions relative to Snake's position
            var guardsAddresses = FindAllGuardsPositionAOBs(snakePointerAddress);
            if (guardsAddresses.Count > 0)
            {
                MoveGuardsToPosition(processHandle, guardsAddresses, snakePosition);
                LoggingManager.Instance.Log("Moved all guards to Snake's current position.");
            }
            else
            {
                LoggingManager.Instance.Log("No guards found in range.");
            }

            NativeMethods.CloseHandle(processHandle);
        }

        public void LogAllGuardsPosition()
        {
            LoggingManager.Instance.Log("Logging all guards' positions");

            // Open the game process
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open game process.");
                return;
            }

            // Find guards' positions using the previously defined method
            IntPtr snakePointerAddress = XyzManager.Instance.FindPointerMemory(11810, 0x130);
            if (snakePointerAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to find Snake's position. Aborting guards' scan.");
                NativeMethods.CloseHandle(processHandle);
                return;
            }

            // Search for all guard positions around Snake's position
            var guardsAddresses = FindAllGuardsPositionAOBs(snakePointerAddress);
            if (guardsAddresses.Count == 0)
            {
                LoggingManager.Instance.Log("No guards found in range.");
                NativeMethods.CloseHandle(processHandle);
                return;
            }

            // Iterate through all found guards and log their positions
            foreach (IntPtr guardAddress in guardsAddresses)
            {
                float[] guardPosition = new float[3];
                for (int i = 0; i < guardPosition.Length; i++)
                {
                    IntPtr currentAddress = IntPtr.Add(guardAddress, 7 + i * 4); // X, Y, Z offsets
                    byte[] bytes = MemoryManager.ReadMemoryBytes(processHandle, currentAddress, 4);
                    if (bytes != null && bytes.Length == 4)
                    {
                        guardPosition[i] = BitConverter.ToSingle(bytes, 0); // Convert bytes to float
                    }
                    else
                    {
                        guardPosition[i] = 0;
                    }
                }               

                // Log the position and memory address of the guard
                LoggingManager.Instance.Log($"Guard Position: X={guardPosition[0]}, Y={guardPosition[1]}, Z={guardPosition[2]}");
                LoggingManager.Instance.Log($"Guard Memory Address: {guardAddress.ToString("X")}");
            }

            // Close the process handle after logging
            NativeMethods.CloseHandle(processHandle);
        }

        public void RandomizeGuardLocations(string location)
        {
            List<float[]> randomPositions = GetRandomGuardPositionsForLocation(location);

            if (randomPositions.Count == 0)
            {
                LoggingManager.Instance.Log($"No random positions available for location {location}");
                return;
            }

            // Get the guards' positions and move them
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open game process.");
                return;
            }

            // Get the guards' addresses based on the current map location
            var guardsAddresses = FindAllGuardsPositionAOBs(XyzManager.Instance.FindPointerMemory(11810, 0x130));
            if (guardsAddresses.Count == 0)
            {
                LoggingManager.Instance.Log("No guards found to randomize.");
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return;
            }

            // Shuffle and assign random positions to guards
            Random rand = new Random();
            foreach (var guardAddress in guardsAddresses)
            {
                int randomIndex = rand.Next(randomPositions.Count);
                float[] randomPosition = randomPositions[randomIndex];

                // Move the guard to the randomized location
                MoveGuardsToPosition(processHandle, new List<IntPtr> { guardAddress }, randomPosition);

                LoggingManager.Instance.Log($"Guard at {guardAddress.ToString("X")} moved to position: X={randomPosition[0]}, Y={randomPosition[1]}, Z={randomPosition[2]}");
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public List<float[]> GetRandomGuardPositionsForLocation(string location)
        {
            switch (location)
            {
                case "v004a": // Dremuchji North
                    return new List<float[]>
            {
                new float[] { -30465.01f, 1000.00f, -17661.21f },
                new float[] { -24864.36f, 1000.65f, -17966.54f },
                new float[] { -28456.73f, 1000.00f, -16285.67f },
                new float[] { -31016.55f, 2203.23f, -14764.51f },
                new float[] { -31061.47f, 1159.80f, -16549.63f },
                new float[] { -32056.76f, 1000.00f, -21075.00f },
                new float[] { -26939.59f, 1000.00f, -21356.48f },
            };

                case "v005a": // Dolinovodno Rope Bridge
                    return new List<float[]>
            {
                new float[] { 8992.15f, -3780.00f, -46939.67f },
                
            };

                case "v006a": // Rassvet
                    return new List<float[]>
            {
                new float[] { 130.0f, 0.0f, 400.0f },
                new float[] { 180.0f, 0.0f, 450.0f },
                new float[] { 140.0f, 0.0f, 420.0f },
                new float[] { 160.0f, 0.0f, 440.0f }
            };

                default:
                    LoggingManager.Instance.Log("No random positions available for this location.");
                    return new List<float[]>(); // Return an empty list if no positions are defined for the location
            }
        }
    }
}