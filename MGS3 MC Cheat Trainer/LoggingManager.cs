﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    internal class LoggingManager
    {
        private static LoggingManager instance;
        private static readonly object padlock = new object();
        private static string logFolderPath;
        private static string logFileName = "MGS3_MC_Cheat_Trainer_Log.txt";
        private static string logPath;

        // Static constructor to initialize logFolderPath and logPath
        static LoggingManager()
        {
            // Use the Documents folder for storing logs, with a specific subfolder for the application
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string appLogFolder = "MGS3 CT Logs"; // Specific name for the log folder

            logFolderPath = Path.Combine(documentsFolder, appLogFolder);
            logPath = Path.Combine(logFolderPath, logFileName);

            EnsureLogFileExists();
        }

        private LoggingManager()
        {
            // Private constructor to prevent instance creation outside of this class
        }

        public static LoggingManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LoggingManager();
                    }

                    return instance;
                }
            }
        }

        private static void EnsureLogFileExists()
        {
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            if (!File.Exists(logPath))
            {
                using (var stream = File.Create(logPath))
                {
                    // Immediately close the stream to release the file
                }
            }
        }

        public void Log(string message)
        {
            try
            {
                using (var writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while trying to log: {ex.Message}");
            }
        }

        // This is mostly to help me stay ahead of game updates so I can log the offset differences quicker if anything gets shifted around
        // Currently only logs the AOB addresses, but could be expanded to log other addresses as well
        public void LogAOBAddresses()
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("Process not found.");
                return;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process.");
                return;
            }

            IntPtr baseAddress = process.MainModule.BaseAddress;
            long moduleSize = process.MainModule.ModuleMemorySize;

            foreach (var aobEntry in AobManager.AOBs)
            {
                string name = aobEntry.Key;
                if (string.IsNullOrEmpty(name)) continue; // Skip empty placeholder entries

                byte[] pattern = aobEntry.Value.Pattern;
                string mask = aobEntry.Value.Mask;

                var foundAddresses =
                    MemoryManager.Instance.ScanForAllAobInstances(processHandle, baseAddress, moduleSize, pattern,
                        mask);
                if (foundAddresses.Count > 0)
                {
                    foreach (var address in foundAddresses)
                    {
                        long offset = address.ToInt64() - baseAddress.ToInt64();
                        // Read the AOB bytes from the found address
                        byte[] aobBytes = new byte[pattern.Length];
                        if (NativeMethods.ReadProcessMemory(processHandle, address, aobBytes, (uint)aobBytes.Length,
                                out _))
                        {
                            string aobHexString = BitConverter.ToString(aobBytes).Replace("-", " ");
                            LoggingManager.Instance.Log(
                                $"{name}: Instance found at: {address.ToString("X")}, METAL GEAR SOLID3.exe+{offset:X}, AOB: {aobHexString}");
                        }
                        else
                        {
                            LoggingManager.Instance.Log(
                                $"{name}: Instance found at: {address.ToString("X")}, METAL GEAR SOLID3.exe+{offset:X}, but failed to read AOB bytes.");
                        }
                    }
                }
                else
                {
                    LoggingManager.Instance.Log($"{name}: AOB not found.");
                }
            }

            NativeMethods.CloseHandle(processHandle);
        }

        public void LogAllMemoryAddressesandValues()
        {
            var LogMemoryAddresses = new Dictionary<string, Func<string>>()
            {
                // Based on order within memory
                { "CQC Slam Normal Damage", () => DebugFunctionManager.Instance.CQCSlamNormalDamage() },
                { "CQC Slam Extreme Damage", () => DebugFunctionManager.Instance.CQCSlamExtremeDamage() },
                { "Wp Grenade Damage", () => DebugFunctionManager.Instance.WpNadeDamage() },
                // The notes I had for the zzz stuff were a mess so I should reorganize this eventually
                { "Zzz Drain Instructions", () => DebugFunctionManager.Instance.ZzzDrain() },
                { "Sleep Status Instructions 2", () => DebugFunctionManager.Instance.SleepStatus2() },
                { "Sleep Status Instructions 1", () => DebugFunctionManager.Instance.SleepStatus1() },
                { "Zzz Weapons Damage 1", () => DebugFunctionManager.Instance.ZzzWeaponsDamage1() },
                // End of zzz stuff
                { "Shotgun Damage Instructions", () => DebugFunctionManager.Instance.ShotgunDamage() },
                { "M63 Damage", () => DebugFunctionManager.Instance.M63Damage() },
                { "Stun Grenade Damage", () => DebugFunctionManager.Instance.StunNadeDamage() },
                { "Knife and Fork Damage Instructions", () => DebugFunctionManager.Instance.KnifeForkDamage() },
                { "Stun Punch Triple", () => DebugFunctionManager.Instance.TriplePunchDamage() },
                { "Stun Roll Damage", () => DebugFunctionManager.Instance.StunRollDamage() },
                { "Most Lethal Weapons Damage", () => DebugFunctionManager.Instance.MostLethalWeaponsDamage() },
                { "Explosion Damage", () => DebugFunctionManager.Instance.ExplosionDamage() },
                { "Stun Punch Single", () => DebugFunctionManager.Instance.SinglePunchDamage() },
                { "Stun Punch Instructions", () => DebugFunctionManager.Instance.StunPunchInstructions() },
                { "Stun Punch Knock over Threshold", () => DebugFunctionManager.Instance.PunchKnockOverThreshold() },
                { "Throat Slit Damage", () => DebugFunctionManager.Instance.ThroatSlitDamage() },
                { "Piss Filter", () => DebugFunctionManager.Instance.GetPissFilterValueAsString() },
                { "Snake Long Sleep", () => DebugFunctionManager.Instance.GetSnakeLongSleepValue() },
                { "Force Direction", () => DebugFunctionManager.Instance.GetForceDirectionValue() },
                { "Snake Short Sleep", () => DebugFunctionManager.Instance.GetSnakeShortSleepValue() },
                { "Vomit Fire", () => DebugFunctionManager.Instance.GetVomitFireValue() },
                { "Box Crouch", () => DebugFunctionManager.Instance.GetBoxCrouchValue() },
                { "Bunny Hop", () => DebugFunctionManager.Instance.GetBunnyHopValue() },
                { "Fake Death", () => DebugFunctionManager.Instance.GetFakeDeathValue() },
                { "Alert Status", () => DebugFunctionManager.Instance.ReadAlertStatus() },
                { "Battery Drain Instructions", () => DebugFunctionManager.Instance.ReadBatteryInstructions() },
                { "Infinite Ammo and Reload Instructions", () => DebugFunctionManager.Instance.ReadInfiniteAmmoAndReload() },
                { "No HUD Partial", () => DebugFunctionManager.Instance.GetPartialHudValue() },
                { "Item and Weapon Window", () => DebugFunctionManager.Instance.GetItemAndWeaponWindowValue() },
                { "Real Time Item Swapping Instructions", () => DebugFunctionManager.Instance.RealTimeWeaponItemSwapping() },
                { "Light Near Snake", () => DebugFunctionManager.Instance.GetLightNearSnakeValueAsString() },
                { "Map Colour", () => DebugFunctionManager.Instance.GetColourMapValueAsString() },
                { "Sky Colour", () => DebugFunctionManager.Instance.GetSkyColourValueAsString() },
                { "Sky Value", () => DebugFunctionManager.Instance.GetSkyChangingByteValueAsString() },
            };

            foreach (var reading in LogMemoryAddresses)
            {
                string message = reading.Value.Invoke();
                LoggingManager.Instance.Log($"{reading.Key}:\n{message}\n");
            }
        }

        public static void LogAllWeaponsAndItemsAddresses()
        {
            LoggingManager.Instance.Log("Logging all weapons and items' addresses...");

            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open game process.");
                return;
            }

            // Get the base address of the game module
            Process process = MemoryManager.GetMGS3Process();
            IntPtr baseAddress = process.MainModule.BaseAddress;

            // Log Weapons
            LoggingManager.Instance.Log("Weapons:");
            var weapons = typeof(MGS3UsableObjects).GetFields()
                .Where(field => field.FieldType == typeof(Weapon))
                .Select(field => field.GetValue(null) as Weapon)
                .ToList();

            foreach (var weapon in weapons)
            {
                IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);
                if (weaponAddress == IntPtr.Zero) continue;

                // Calculate the offset relative to the base address
                long relativeOffset = weaponAddress.ToInt64() - baseAddress.ToInt64();
                LoggingManager.Instance.Log($"{weapon.Name} - Address: {weaponAddress.ToString("X")} (METAL GEAR SOLID 3.exe+{relativeOffset:X})");

                if (weapon.MaxAmmoOffset != IntPtr.Zero)
                {
                    IntPtr maxAmmoAddress = WeaponAddresses.GetMaxAmmoAddress(weaponAddress);
                    relativeOffset = maxAmmoAddress.ToInt64() - baseAddress.ToInt64();
                    LoggingManager.Instance.Log($"Max Ammo Address: {maxAmmoAddress.ToString("X")} (METAL GEAR SOLID 3.exe+{relativeOffset:X})");
                }

                if (weapon.ClipOffset != IntPtr.Zero)
                {
                    IntPtr clipAddress = WeaponAddresses.GetClipAddress(weaponAddress);
                    relativeOffset = clipAddress.ToInt64() - baseAddress.ToInt64();
                    LoggingManager.Instance.Log($"Clip Address: {clipAddress.ToString("X")} (METAL GEAR SOLID 3.exe+{relativeOffset:X})");
                }

                if (weapon.MaxClipOffset != IntPtr.Zero)
                {
                    IntPtr maxClipAddress = WeaponAddresses.GetMaxClipAddress(weaponAddress);
                    relativeOffset = maxClipAddress.ToInt64() - baseAddress.ToInt64();
                    LoggingManager.Instance.Log($"Max Clip Address: {maxClipAddress.ToString("X")} (METAL GEAR SOLID 3.exe+{relativeOffset:X})");
                }

                if (weapon.SuppressorToggleOffset != IntPtr.Zero)
                {
                    IntPtr suppressorToggleAddress = WeaponAddresses.GetSuppressorToggleAddress(weaponAddress);
                    relativeOffset = suppressorToggleAddress.ToInt64() - baseAddress.ToInt64();
                    LoggingManager.Instance.Log($"Suppressor Toggle Address: {suppressorToggleAddress.ToString("X")} (METAL GEAR SOLID 3.exe+{relativeOffset:X})");
                }
            }

            // Log Items
            LoggingManager.Instance.Log("Items:");
            var items = typeof(MGS3UsableObjects).GetFields()
                .Where(field => field.FieldType == typeof(Item))
                .Select(field => field.GetValue(null) as Item)
                .ToList();

            foreach (var item in items)
            {
                IntPtr itemAddress = ItemAddresses.GetAddress(item.Index, MemoryManager.Instance);
                if (itemAddress == IntPtr.Zero) continue;

                // Calculate the offset relative to the base address
                long relativeOffset = itemAddress.ToInt64() - baseAddress.ToInt64();
                LoggingManager.Instance.Log($"{item.Name} - Address: {itemAddress.ToString("X")} (METAL GEAR SOLID 3.exe+{relativeOffset:X})");

                if (item.MaxCapacityOffset != IntPtr.Zero)
                {
                    IntPtr maxCapacityAddress = ItemAddresses.GetMaxAddress(itemAddress);
                    relativeOffset = maxCapacityAddress.ToInt64() - baseAddress.ToInt64();
                    LoggingManager.Instance.Log($"Max Capacity Address: {maxCapacityAddress.ToString("X")} (METAL GEAR SOLID 3.exe+{relativeOffset:X})");
                }
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            LoggingManager.Instance.Log("Finished logging weapons and items' addresses.");
        }

    }

}



/* Button to implement later to locate the log file/folder

   private void btnOpenLogFolder_Click(object sender, EventArgs e)
   {
       // Use the Process.Start method to open the log folder in File Explorer
       try
       {
           Process.Start(new ProcessStartInfo
           {
               FileName = LoggingManager.LogFolderPath,
               UseShellExecute = true,
               Verb = "open"
           });
       }
       catch (Exception ex)
       {
           MessageBox.Show($"Failed to open log folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }
   }
*/