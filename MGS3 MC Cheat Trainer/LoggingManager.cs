using System;
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
                { "CQC Slam Normal Damage", () => DebugMethodManager.Instance.CQCSlamNormalDamage() },
                { "CQC Slam Extreme Damage", () => DebugMethodManager.Instance.CQCSlamExtremeDamage() },
                { "Wp Grenade Damage", () => DebugMethodManager.Instance.WpNadeDamage() },
                // The notes I had for the zzz stuff were a mess so I should reorganize this eventually
                { "Zzz Drain Instructions", () => DebugMethodManager.Instance.ZzzDrain() },
                { "Sleep Status Instructions 2", () => DebugMethodManager.Instance.SleepStatus2() },
                { "Sleep Status Instructions 1", () => DebugMethodManager.Instance.SleepStatus1() },
                { "Zzz Weapons Damage 1", () => DebugMethodManager.Instance.ZzzWeaponsDamage1() },
                // End of zzz stuff
                { "Shotgun Damage Instructions", () => DebugMethodManager.Instance.ShotgunDamage() },
                { "M63 Damage", () => DebugMethodManager.Instance.M63Damage() },
                { "Stun Grenade Damage", () => DebugMethodManager.Instance.StunNadeDamage() },
                { "Knife and Fork Damage Instructions", () => DebugMethodManager.Instance.KnifeForkDamage() },
                { "Stun Punch Triple", () => DebugMethodManager.Instance.TriplePunchDamage() },
                { "Stun Roll Damage", () => DebugMethodManager.Instance.StunRollDamage() },
                { "Most Lethal Weapons Damage", () => DebugMethodManager.Instance.MostLethalWeaponsDamage() },
                { "Explosion Damage", () => DebugMethodManager.Instance.ExplosionDamage() },
                { "Stun Punch Single", () => DebugMethodManager.Instance.SinglePunchDamage() },
                { "Stun Punch Instructions", () => DebugMethodManager.Instance.StunPunchInstructions() },
                { "Stun Punch Knock over Threshold", () => DebugMethodManager.Instance.PunchKnockOverThreshold() },
                { "Throat Slit Damage", () => DebugMethodManager.Instance.ThroatSlitDamage() },
                { "Piss Filter", () => DebugMethodManager.Instance.GetPissFilterValueAsString() },
                { "Snake Long Sleep", () => DebugMethodManager.Instance.GetSnakeLongSleepValue() },
                { "Force Direction", () => DebugMethodManager.Instance.GetForceDirectionValue() },
                { "Snake Short Sleep", () => DebugMethodManager.Instance.GetSnakeShortSleepValue() },
                { "Vomit Fire", () => DebugMethodManager.Instance.GetVomitFireValue() },
                { "Box Crouch", () => DebugMethodManager.Instance.GetBoxCrouchValue() },
                { "Bunny Hop", () => DebugMethodManager.Instance.GetBunnyHopValue() },
                { "Fake Death", () => DebugMethodManager.Instance.GetFakeDeathValue() },
                { "Alert Status", () => DebugMethodManager.Instance.ReadAlertStatus() },
                { "Battery Drain Instructions", () => DebugMethodManager.Instance.ReadBatteryInstructions() },
                { "Infinite Ammo and Reload Instructions", () => DebugMethodManager.Instance.ReadInfiniteAmmoAndReload() },
                { "No HUD Partial", () => DebugMethodManager.Instance.GetPartialHudValue() },
                { "Item and Weapon Window", () => DebugMethodManager.Instance.GetItemAndWeaponWindowValue() },
                { "Real Time Item Swapping Instructions", () => DebugMethodManager.Instance.RealTimeWeaponItemSwapping() },
                { "Light Near Snake", () => DebugMethodManager.Instance.GetLightNearSnakeValueAsString() },
                { "Map Colour", () => DebugMethodManager.Instance.GetColourMapValueAsString() },
                { "Sky Colour", () => DebugMethodManager.Instance.GetSkyColourValueAsString() },
                { "Sky Value", () => DebugMethodManager.Instance.GetSkyChangingByteValueAsString() },
                { "Current Difficulty Value", () => DebugMethodManager.Instance.GetDifficultyValue() },
                { "Total Continues", () => DebugMethodManager.Instance.GetContinuesValue() },
                { "Total Saves", () => DebugMethodManager.Instance.GetSavesValue() },
                { "Total Alerts Triggered", () => DebugMethodManager.Instance.GetAlertsTriggeredValue() },
                { "Total Humans Killed", () => DebugMethodManager.Instance.GetHumansKilledValue() },
                { "Total Special Items Used", () => DebugMethodManager.Instance.GetSpecialItemsUsedValue() },
                { "Total Plants and Animals Captured", () => DebugMethodManager.Instance.GetPlantsAndAnimalsCapturedValue() },
                { "Total Serious Injuries", () => DebugMethodManager.Instance.GetSeriousInjuriesValue() },
                { "Total Total Damage Taken", () => DebugMethodManager.Instance.GetTotalDamageTakenValue() },
                { "Total Play Time", () => DebugMethodManager.Instance.GetPlayTimeValue() },
                { "Total Life Med Used", () => DebugMethodManager.Instance.GetLifeMedsUsedValue() },
                { "Map String", () => DebugMethodManager.Instance.GetMapStringValue() },
                { "R_Sna", () => DebugMethodManager.Instance.GetR_SnaValue() },
                { "Snake's Equipped Weapon", () => DebugMethodManager.Instance.GetSnakesEquippedWeaponValue() },
                { "Snake's Equipped Item", () => DebugMethodManager.Instance.GetSnakesEquippedItemValue() },
                { "Snake's Equipped Facepaint", () => DebugMethodManager.Instance.GetSnakesFacepaintValue() },
                { "Snake's Equipped Camo", () => DebugMethodManager.Instance.GetSnakesCamoValue() },
                { "Snake's Current Health", () => DebugMethodManager.Instance.GetSnakesCurrentHealthValue() },
                { "Snake's Max Health", () => DebugMethodManager.Instance.GetSnakesMaxHealthValue() },
                { "Snake's Current Stamina", () => DebugMethodManager.Instance.GetSnakesCurrentStaminaValue() },
                { "Snake's Serious Injury 1", () => DebugMethodManager.Instance.GetSeriousInjury1Value() },
                { "Snake's Serious Injury 2", () => DebugMethodManager.Instance.GetSeriousInjury2Value() },
                { "Snake's Serious Injury 3", () => DebugMethodManager.Instance.GetSeriousInjury3Value() },
                { "Snake's Serious Injury 4", () => DebugMethodManager.Instance.GetSeriousInjury4Value() },
                { "Snake's Serious Injury 5", () => DebugMethodManager.Instance.GetSeriousInjury5Value() },
                { "Snake's Serious Injury 6", () => DebugMethodManager.Instance.GetSeriousInjury6Value() },
                { "Snake's Serious Injury 7", () => DebugMethodManager.Instance.GetSeriousInjury7Value() },
                { "Snake's Serious Injury 8", () => DebugMethodManager.Instance.GetSeriousInjury8Value() },
                { "Snake's Serious Injury 9", () => DebugMethodManager.Instance.GetSeriousInjury9Value() },
                { "Snake's Serious Injury 10", () => DebugMethodManager.Instance.GetSeriousInjury10Value() },
                { "Snake's Serious Injury 11", () => DebugMethodManager.Instance.GetSeriousInjury11Value() },
                { "Snake's Serious Injury 12", () => DebugMethodManager.Instance.GetSeriousInjury12Value() },
                { "Snake's Serious Injury 13", () => DebugMethodManager.Instance.GetSeriousInjury13Value() },
                { "Snake's Serious Injury 14", () => DebugMethodManager.Instance.GetSeriousInjury14Value() },
                { "Snake's Serious Injury 15", () => DebugMethodManager.Instance.GetSeriousInjury15Value() },
                { "Snake's Serious Injury 16", () => DebugMethodManager.Instance.GetSeriousInjury16Value() },
                { "Snake's Serious Injury 17", () => DebugMethodManager.Instance.GetSeriousInjury17Value() },
                { "Snake's Serious Injury 18", () => DebugMethodManager.Instance.GetSeriousInjury18Value() },
                { "Snake's Serious Injury 19", () => DebugMethodManager.Instance.GetSeriousInjury19Value() },
                { "Snake's Serious Injury 20", () => DebugMethodManager.Instance.GetSeriousInjury20Value() },
                { "Snake's Serious Injury 21", () => DebugMethodManager.Instance.GetSeriousInjury21Value() },
                { "Snake's Serious Injury 22", () => DebugMethodManager.Instance.GetSeriousInjury22Value() },
                { "Snake's Serious Injury 23", () => DebugMethodManager.Instance.GetSeriousInjury23Value() },
                { "Snake's Serious Injury 24", () => DebugMethodManager.Instance.GetSeriousInjury24Value() },
                { "Snake's Serious Injury 25", () => DebugMethodManager.Instance.GetSeriousInjury25Value() },
                { "Snake's Serious Injury 26", () => DebugMethodManager.Instance.GetSeriousInjury26Value() },
                { "Snake's Serious Injury 27", () => DebugMethodManager.Instance.GetSeriousInjury27Value() },
                { "Snake's Serious Injury 28", () => DebugMethodManager.Instance.GetSeriousInjury28Value() },
                { "Snake's Serious Injury 29", () => DebugMethodManager.Instance.GetSeriousInjury29Value() },
                { "Snake's Serious Injury 30", () => DebugMethodManager.Instance.GetSeriousInjury30Value() },
                { "Snake's Serious Injury 31", () => DebugMethodManager.Instance.GetSeriousInjury31Value() },
                { "Snake's Serious Injury 32", () => DebugMethodManager.Instance.GetSeriousInjury32Value() },
                { "Snake's Serious Injury 33", () => DebugMethodManager.Instance.GetSeriousInjury33Value() },
                { "Snake's Serious Injury 34", () => DebugMethodManager.Instance.GetSeriousInjury34Value() },
                { "Snake's Serious Injury 35", () => DebugMethodManager.Instance.GetSeriousInjury35Value() },
                { "Snake's Serious Injury 36", () => DebugMethodManager.Instance.GetSeriousInjury36Value() },
                { "Snake's Serious Injury 37", () => DebugMethodManager.Instance.GetSeriousInjury37Value() },
                { "Snake's Serious Injury 38", () => DebugMethodManager.Instance.GetSeriousInjury38Value() },
                { "Snake's Serious Injury 39", () => DebugMethodManager.Instance.GetSeriousInjury39Value() },
                { "Snake's Serious Injury 40", () => DebugMethodManager.Instance.GetSeriousInjury40Value() },
                { "Snake's Serious Injury 41", () => DebugMethodManager.Instance.GetSeriousInjury41Value() },
                { "Snake's Serious Injury 42", () => DebugMethodManager.Instance.GetSeriousInjury42Value() },
                { "Snake's Serious Injury 43", () => DebugMethodManager.Instance.GetSeriousInjury43Value() },
                { "Snake's Serious Injury 44", () => DebugMethodManager.Instance.GetSeriousInjury44Value() },
                { "Snake's Serious Injury 45", () => DebugMethodManager.Instance.GetSeriousInjury45Value() },
                { "Snake's Serious Injury 46", () => DebugMethodManager.Instance.GetSeriousInjury46Value() },
                { "Snake's Serious Injury 47", () => DebugMethodManager.Instance.GetSeriousInjury47Value() },
                { "Snake's Serious Injury 48", () => DebugMethodManager.Instance.GetSeriousInjury48Value() },
                { "Snake's Serious Injury 49", () => DebugMethodManager.Instance.GetSeriousInjury49Value() },
                { "Snake's Serious Injury 50", () => DebugMethodManager.Instance.GetSeriousInjury50Value() },
                { "Snake's Serious Injury 51", () => DebugMethodManager.Instance.GetSeriousInjury51Value() },
                { "Snake's Serious Injury 52", () => DebugMethodManager.Instance.GetSeriousInjury52Value() },
                { "Snake's Serious Injury 53", () => DebugMethodManager.Instance.GetSeriousInjury53Value() },
                { "Snake's Serious Injury 54", () => DebugMethodManager.Instance.GetSeriousInjury54Value() },
                { "Snake's Serious Injury 55", () => DebugMethodManager.Instance.GetSeriousInjury55Value() },
                { "Snake's Serious Injury 56", () => DebugMethodManager.Instance.GetSeriousInjury56Value() },
                { "Snake's Serious Injury 57", () => DebugMethodManager.Instance.GetSeriousInjury57Value() },
                { "Snake's Serious Injury 58", () => DebugMethodManager.Instance.GetSeriousInjury58Value() },
                { "Snake's Serious Injury 59", () => DebugMethodManager.Instance.GetSeriousInjury59Value() },
                { "Snake's Serious Injury 60", () => DebugMethodManager.Instance.GetSeriousInjury60Value() },
                { "Snake's Serious Injury 61", () => DebugMethodManager.Instance.GetSeriousInjury61Value() },
                { "Snake's Serious Injury 62", () => DebugMethodManager.Instance.GetSeriousInjury62Value() },
                { "Snake's Serious Injury 63", () => DebugMethodManager.Instance.GetSeriousInjury63Value() },
                { "Snake's Serious Injury 64", () => DebugMethodManager.Instance.GetSeriousInjury64Value() },
                { "Snake's Serious Injury 65", () => DebugMethodManager.Instance.GetSeriousInjury65Value() },
                { "Snake's Serious Injury 66", () => DebugMethodManager.Instance.GetSeriousInjury66Value() },
                { "Snake's Serious Injury 67", () => DebugMethodManager.Instance.GetSeriousInjury67Value() },
                { "Snake's Serious Injury 68", () => DebugMethodManager.Instance.GetSeriousInjury68Value() },
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