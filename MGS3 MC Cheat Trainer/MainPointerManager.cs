using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MGS3_MC_Cheat_Trainer.MemoryManager;
using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    internal class MainPointerManager
    {

        private static readonly Dictionary<int, string> DifficultyMappings = new()
        {
            { 10, "Very Easy" },
            { 20, "Easy" },
            { 30, "Normal" },
            { 40, "Hard" },
            { 50, "Extreme" },
            { 60, "European Extreme" }
        };

        public string ReadDifficulty()
        {
            string difficultyValue = HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.DifficultySub, 1, DataType.UInt8);

            if (int.TryParse(difficultyValue, out int difficultyKey) && DifficultyMappings.TryGetValue(difficultyKey, out string description))
            {
                return description;
            }
            return "Unknown Difficulty";
        }

        public string ReadPlayTime()
        {
            string playTimeValue = HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.PlayTimeSub, 4, DataType.UInt32);

            if (uint.TryParse(playTimeValue, out uint totalFrames))
            {
                uint totalSeconds = totalFrames / 60;
                TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
                int hours = (int)timeSpan.TotalHours;
                string formattedTime = $"{hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
                LoggingManager.Instance.Log($"Formatted PlayTime: {formattedTime}");

                return formattedTime;
            }

            return "00:00:00";
        }


        public string ReadContinues()
        {
            return HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.ContinuesSub, 2, DataType.UInt16);
        }

        public string ReadSaves()
        {
            return HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.SavesSub, 2, DataType.UInt16);
        }

        public string ReadAlertsTriggered()
        {
            return HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.AlertsTriggeredSub, 2, DataType.UInt16);
        }

        public string ReadHumansKilled()
        {
            return HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.HumansKilledSub, 2, DataType.UInt16);
        }

        public string ReadTimesSeriouslyInjured()
        {
            return HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.SeriousInjuriesSub, 2, DataType.UInt16);
        }

        public string ReadTotalDamageTaken()
        {
            return HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.TotalDamageTakenSubTest, 2, DataType.UInt16);
        }

        public string ReadLifeMedsUsed()
        {
            return HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.LifeMedsUsedSub, 2, DataType.UInt16);
        }

        public string ReadPlantsAndAnimalsCaptured()
        {
            return HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes",
                (int)MainPointerAddresses.PlantsAndAnimalsCapturedSub, 1, DataType.UInt8);
        }

        public string ReadMealsEaten()
        {
            return HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.MealsEatenSub, 2, DataType.UInt16);
        }

        // Stealth = 1, Infinity FP = 2, Ez Gun = 4. Adding the totals tells what you used. 
        private static readonly Dictionary<int, string> SpecialItemCombos = new()
            {
                { 0, "NOT USED" },
                { 1, "STEALTH CAMO USED" },
                { 2, "INFINITY FACEPAINT USED" },
                { 3, "STEALTH CAMO + INFINITY FACEPAINT USED" },
                { 4, "EZ GUN USED" },
                { 5, "STEALTH CAMO + EZ GUN USED" },
                { 6, "INFINITY FACEPAINT + EZ GUN USED" },
                { 7, "STEALTH CAMO + INFINITY FACEPAINT + EZ GUN USED" }
            };

        public string ReadSpecialItemsUsed()
        {
            string specialItemUsedValue = HelperMethods.Instance.ReadMemoryValueOnlyAsString("PointerBytes", (int)MainPointerAddresses.SpecialItemsUsedSub, 1, DataType.UInt8);

            if (int.TryParse(specialItemUsedValue, out int specialItemUsedKey) && SpecialItemCombos.TryGetValue(specialItemUsedKey, out string description))
            {
                return description;
            }

            return "Unable to determine usage";
        }

        /// <summary>
        /// Generic method to write a value to memory.
        /// </summary>
        private bool WriteStat<T>(string aobKey, int offset, T value)
        {
            IntPtr processHandle = HelperMethods.Instance.GetProcessHandle();
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to get process handle for writing.");
                return false;
            }

            IntPtr targetAddress = HelperMethods.Instance.GetTargetAddress(processHandle, aobKey, offset);
            if (targetAddress == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to get target address for writing.");
                return false;
            }

            bool success = MemoryManager.WriteMemory(processHandle, targetAddress, value);
            if (!success)
            {
                LoggingManager.Instance.Log($"Failed to write value at offset {offset:X}.");
            }
            return success;
        }

        public bool WriteDifficulty(byte value) => WriteStat("PointerBytes", (int)MainPointerAddresses.DifficultySub, value);

        public bool WritePlayTime(uint framesValue) => WriteStat("PointerBytes", (int)MainPointerAddresses.PlayTimeSub, framesValue);

        public bool WriteAlertsTriggered(ushort value) => WriteStat("PointerBytes", (int)MainPointerAddresses.AlertsTriggeredSub, value);
        public bool WriteSaves(ushort value) => WriteStat("PointerBytes", (int)MainPointerAddresses.SavesSub, value);
        public bool WriteContinues(ushort value) => WriteStat("PointerBytes", (int)MainPointerAddresses.ContinuesSub, value);
        public bool WriteHumansKilled(ushort value) => WriteStat("PointerBytes", (int)MainPointerAddresses.HumansKilledSub, value);
        public bool WriteTimesSeriouslyInjured(ushort value) => WriteStat("PointerBytes", (int)MainPointerAddresses.SeriousInjuriesSub, value);
        public bool WriteMealsEaten(ushort value) => WriteStat("PointerBytes", (int)MainPointerAddresses.MealsEatenSub, value);
        public bool WriteLifeMedsUsed(ushort value) => WriteStat("PointerBytes", (int)MainPointerAddresses.LifeMedsUsedSub, value);
        public bool WritePlantsAndAnimalsCaptured(byte value) => WriteStat("PointerBytes", (int)MainPointerAddresses.PlantsAndAnimalsCapturedSub, value);
        public bool WriteSpecialItemsUsed(byte value) => WriteStat("PointerBytes", (int)MainPointerAddresses.SpecialItemsUsedSub, value);
        public bool WriteTotalDamageTaken(ushort value) => WriteStat("PointerBytes", (int)MainPointerAddresses.TotalDamageTakenSubTest, value);


        public string ProjectedRank(string difficulty)
        {
            int alerts = SafeParse(ReadAlertsTriggered());
            int kills = SafeParse(ReadHumansKilled());
            int lifeMedsUsed = SafeParse(ReadLifeMedsUsed());
            int continues = SafeParse(ReadContinues());
            int saves = SafeParse(ReadSaves());
            int mealsEaten = SafeParse(ReadMealsEaten());
            int plantsCaptured = SafeParse(ReadPlantsAndAnimalsCaptured());
            int injuries = SafeParse(ReadTimesSeriouslyInjured());
            int damageBars = SafeParse(ReadTotalDamageTaken());
            string specialItemsUsed = ReadSpecialItemsUsed();
            string formattedPlayTime = ReadPlayTime();
            TimeSpan playTime = SafeParseTime(formattedPlayTime);
            LoggingManager.Instance.Log($"Play Time for Rank Projection: {playTime}");

            var rankConditions = new List<RankCondition>
    {
    // Top Performance Ranks
    new RankCondition("FoxHound", "Extreme/European Extreme", maxPlayTimeMinutes: 5 * 60, maxSaves: 25, maxContinues: 0, specialItems: "Not Used", maxLifeMeds: 0, maxKills: 0, maxAlerts: 0, maxDamageBars: 5, maxInjuries: 20),
    new RankCondition("Fox", "Hard/Extreme/European Extreme", maxPlayTimeMinutes: 5 * 60, maxSaves: 35, maxContinues: 0, specialItems: "Not Used", maxLifeMeds: 0, maxKills: 0, maxAlerts: 3, maxDamageBars: 5, maxInjuries: 20),
    new RankCondition("Doberman", "Normal/Hard/Extreme/European Extreme", maxPlayTimeMinutes: (5 * 60) + 30, maxSaves: 50, maxContinues: 0, specialItems: "Not Used", maxLifeMeds: 0, maxKills: 0, maxAlerts: 5, maxDamageBars: 5),


    new RankCondition("Hound", "Easy/Normal/Hard/Extreme/European Extreme", maxPlayTimeMinutes: 6 * 60, maxSaves: 25, maxContinues: 0, specialItems: "Not Used", maxLifeMeds: 0, maxKills: 0, maxAlerts: 10),

    // Extreme Playtime Ranks
    new RankCondition("Chicken", "Very Easy/Easy", minPlayTimeMinutes: 50 * 60, minSaves: 100, minContinues: 60,
                      specialItems: "Allowed", minLifeMeds: 10, minKills: 250, minAlerts: 250, minDamageBars: 30),
    new RankCondition("Mouse", "Normal", minPlayTimeMinutes: 50 * 60, minSaves: 100, minContinues: 60,
                      specialItems: "Allowed", minLifeMeds: 10, minKills: 250, minAlerts: 250, minDamageBars: 30),
    new RankCondition("Rabbit", "Hard", minPlayTimeMinutes: 50 * 60, minSaves: 100, minContinues: 60,
                      specialItems: "Allowed", minLifeMeds: 10, minKills: 250, minAlerts: 250, minDamageBars: 30),
    new RankCondition("Ostrich", "Extreme/European Extreme", minPlayTimeMinutes: 50 * 60, minSaves: 100, minContinues: 60,
                      specialItems: "Allowed", minLifeMeds: 10, minKills: 250, minAlerts: 250, minDamageBars: 30),

    // Special Titles
    new RankCondition("Chameleon", "Any", maxKills: 0),
    new RankCondition("Markhor", "Any", minPlantsCaptured: 48),
    new RankCondition("Pigeon", "Any", maxKills: 0),

    // Injury-Based Ranks all other below this might need minInjuries at 21 since these ranks go sequentially
    new RankCondition("Flying Squirrel", "Very Easy/Easy", maxInjuries: 20),
    new RankCondition("Bat", "Normal", maxInjuries: 20),
    new RankCondition("Flying Fox", "Hard", maxInjuries: 20),
    new RankCondition("Night Owl", "Extreme/European Extreme", maxInjuries: 20),

    // Playtime-Based Ranks
    new RankCondition("Swallow", "Very Easy/Easy", maxPlayTimeMinutes: 5 * 60),
    new RankCondition("Falcon", "Normal", maxPlayTimeMinutes: 5 * 60),
    new RankCondition("Hawk", "Hard", maxPlayTimeMinutes: 5 * 60),
    new RankCondition("Eagle", "Extreme/European Extreme", maxPlayTimeMinutes: 5 * 60),

    // Meals-Based Ranks
    new RankCondition("Pig", "Very Easy/Easy", minMeals: 250),
    new RankCondition("Elephant", "Normal", minMeals: 250),
    new RankCondition("Mammoth", "Hard", minMeals: 250),
    new RankCondition("Whale", "Extreme/European Extreme", minMeals: 250),

    // Alerts Triggered
    new RankCondition("Cow", "Any", minAlerts: 300),

    // Kills-Based Ranks
    new RankCondition("Piranha", "Very Easy/Easy", minKills: 250),
    new RankCondition("Shark", "Normal", minKills: 250),
    new RankCondition("Jaws", "Hard", minKills: 250),
    new RankCondition("Orca", "Extreme/European Extreme", minKills: 250),

    // High Injury-Based Ranks
    new RankCondition("Mongoose", "Very Easy/Easy", minInjuries: 250),
    new RankCondition("Hyena", "Normal", minInjuries: 250),
    new RankCondition("Tasmanian Devil", "Extreme/European Extreme", minInjuries: 250),
    
    // 50+ hours
    new RankCondition("Koala", "Very Easy/Easy", minPlayTimeMinutes: 50 * 60),
    new RankCondition("Capybara", "Normal", minPlayTimeMinutes: 50 * 60),
    new RankCondition("Sloth", "Hard", minPlayTimeMinutes: 50 * 60),
    new RankCondition("Giant Panda", "Extreme/European Extreme", minPlayTimeMinutes: 50 * 60),

    // Saves-Based Ranks
    new RankCondition("Cat", "Very Easy/Easy", minSaves: 100),
    new RankCondition("Deer", "Normal", minSaves: 100),
    new RankCondition("Zebra", "Hard", minSaves: 100),
    new RankCondition("Hippopotamus", "Extreme/European Extreme", minSaves: 100),

    // Continues and Kills
    new RankCondition("Scorpion", "Any", maxContinues: 50, maxKills: 100, maxAlerts: 20),
    new RankCondition("Jaguar", "Any", maxContinues: 50, maxKills: 100, minAlerts: 21, maxAlerts: 50),
    new RankCondition("Iguana", "Any", maxContinues: 50, maxKills: 100, minAlerts: 51),

    new RankCondition("Tarantula", "Any", maxContinues: 50, minKills: 101, maxAlerts: 20),
    new RankCondition("Panther", "Any", maxContinues: 50, minKills: 101, minAlerts: 21, maxAlerts: 50),
    new RankCondition("Crocodile", "Any", maxContinues: 50, minKills: 101, minAlerts: 51),

    new RankCondition("Centipede", "Any", minContinues: 51, maxKills: 100, maxAlerts: 20),
    new RankCondition("Leopard", "Any", minContinues: 51, maxKills: 100, minAlerts: 21, maxAlerts: 50),
    new RankCondition("Komodo Dragon", "Any", minContinues: 50, minKills: 1, maxKills: 100, minAlerts: 81, maxAlerts: 248, minInjuries: 21),

    new RankCondition("Spider", "Any", minContinues: 51, minKills: 101, maxAlerts: 19),
    new RankCondition("Puma", "Any", minContinues: 51, minKills: 101, maxKills: 299, minAlerts: 22, maxAlerts: 50),
    new RankCondition("Alligator", "Any", minContinues: 51, minKills: 101, minAlerts: 51)
};

            foreach (var rank in rankConditions)
            {

                if (rank.Matches(difficulty, alerts, kills, lifeMedsUsed, continues, saves, mealsEaten, injuries, damageBars, plantsCaptured, playTime, specialItemsUsed))
                {
                    return rank.Name;
                }
            }

            return "Unknown Rank";
        }

        public class RankCondition
        {
            public string Name { get; }
            public string Difficulty { get; }
            public int? MaxPlayTimeMinutes { get; }
            public int? MinPlayTimeMinutes { get; }
            public int? MaxSaves { get; }
            public int? MinSaves { get; }
            public int? MaxContinues { get; }
            public int? MinContinues { get; }
            public string SpecialItems { get; }
            public int? MaxKills { get; }
            public int? MinKills { get; }
            public int? MaxAlerts { get; }
            public int? MinAlerts { get; }
            public int? MaxLifeMeds { get; }
            public int? MinLifeMeds { get; }
            public int? MaxDamageBars { get; }
            public int? MinDamageBars { get; }
            public int? MaxInjuries { get; }
            public int? MinInjuries { get; }
            public int? MinPlantsCaptured { get; }
            public int? MinMeals { get; }

            public RankCondition(string name, string difficulty = null,
                int? maxPlayTimeMinutes = null, int? minPlayTimeMinutes = null,
                int? maxSaves = null, int? minSaves = null,
                int? maxContinues = null, int? minContinues = null,
                string specialItems = null,
                int? maxKills = null, int? minKills = null,
                int? maxAlerts = null, int? minAlerts = null,
                int? maxLifeMeds = null, int? minLifeMeds = null,
                int? maxDamageBars = null, int? minDamageBars = null,
                int? maxInjuries = null, int? minInjuries = null,
                int? minPlantsCaptured = null, int? minMeals = null)
            {
                Name = name;
                Difficulty = difficulty;
                MaxPlayTimeMinutes = maxPlayTimeMinutes;
                MinPlayTimeMinutes = minPlayTimeMinutes;
                MaxSaves = maxSaves;
                MinSaves = minSaves;
                MaxContinues = maxContinues;
                MinContinues = minContinues;
                SpecialItems = specialItems;
                MaxKills = maxKills;
                MinKills = minKills;
                MaxAlerts = maxAlerts;
                MinAlerts = minAlerts;
                MaxLifeMeds = maxLifeMeds;
                MinLifeMeds = minLifeMeds;
                MaxDamageBars = maxDamageBars;
                MinDamageBars = minDamageBars;
                MaxInjuries = maxInjuries;
                MinInjuries = minInjuries;
                MinPlantsCaptured = minPlantsCaptured;
                MinMeals = minMeals;
            }

            public bool Matches(string difficulty, int alerts, int kills, int lifeMeds, int continues, int saves,
                    int meals, int injuries, int damageBars, int plantsCaptured, TimeSpan playTime, string specialItems)
            {
                if (Difficulty != null && Difficulty != "Any")
                {
                    var possibleDifficulties = Difficulty.Split('/');
                    var normalizedDifficulty = NormalizeDifficulty(difficulty);

                    if (!possibleDifficulties.Contains(normalizedDifficulty)) return false;
                }

                if (MaxPlayTimeMinutes.HasValue && playTime.TotalMinutes > MaxPlayTimeMinutes.Value) return false;
                if (MinPlayTimeMinutes.HasValue && playTime.TotalMinutes <= MinPlayTimeMinutes.Value) return false;

                if (MaxSaves.HasValue && saves > MaxSaves.Value) return false;
                if (MinSaves.HasValue && saves <= MinSaves.Value) return false;
                if (MaxContinues.HasValue && continues > MaxContinues.Value) return false;
                if (MinContinues.HasValue && continues <= MinContinues.Value) return false;

                if (MaxKills.HasValue && kills > MaxKills.Value) return false;
                if (MinKills.HasValue && kills < MinKills.Value) return false;

                if (MaxAlerts.HasValue && alerts > MaxAlerts.Value) return false;
                if (MinAlerts.HasValue && alerts < MinAlerts.Value) return false;

                if (MaxLifeMeds.HasValue && lifeMeds > MaxLifeMeds.Value) return false;
                if (MinLifeMeds.HasValue && lifeMeds < MinLifeMeds.Value) return false;

                if (MaxDamageBars.HasValue && damageBars > MaxDamageBars.Value) return false;
                if (MinDamageBars.HasValue && damageBars < MinDamageBars.Value) return false;

                if (MaxInjuries.HasValue && injuries > MaxInjuries.Value) return false;
                if (MinInjuries.HasValue && injuries < MinInjuries.Value) return false;

                if (MinPlantsCaptured.HasValue && plantsCaptured < MinPlantsCaptured.Value) return false;
                if (MinMeals.HasValue && meals < MinMeals.Value) return false;

                if (SpecialItems != null)
                {
                    if (SpecialItems == "Allowed")
                    {
                    }
                    else if (SpecialItems == "Not Used")
                    {
                        if (specialItems != "Not Used") return false;
                    }
                    else
                    {
                        if (specialItems != SpecialItems) return false;
                    }
                }

                return true;
            }




            private string NormalizeDifficulty(string difficulty)
            {
                return difficulty switch
                {
                    "European Extreme" => "European Extreme",
                    "Extreme" => "Extreme",
                    "Hard" => "Hard",
                    "Normal" => "Normal",
                    "Easy" => "Easy",
                    "Very Easy" => "Very Easy",
                    _ => difficulty
                };
            }
        }


        private int SafeParse(string value)
        {
            return int.TryParse(value, out int result) ? result : 0;
        }

        private TimeSpan SafeParseTime(string value)
        {
            LoggingManager.Instance.Log($"Parsing PlayTime value: '{value}'");
            var parts = value.Split(':');
            if (parts.Length == 3
                && int.TryParse(parts[0], out int hours)
                && int.TryParse(parts[1], out int minutes)
                && int.TryParse(parts[2], out int seconds))
            {
                return new TimeSpan(hours, minutes, seconds);
            }
            LoggingManager.Instance.Log("Manual TimeSpan parse failed, returning zero.");
            return TimeSpan.Zero;
        }


        internal static void ApplyInjury(Constants.InjuryType injuryType)
        {
            try
            {
                Process process = GetMGS3Process();
                if (process == null)
                {
                    return;
                }

                IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
                IntPtr moduleBaseAddress = process.MainModule.BaseAddress;
                IntPtr pointerToInjurySlot = moduleBaseAddress + Constants.MainPointerRegionOffset;

                byte[] buffer = new byte[IntPtr.Size];
                if (!NativeMethods.ReadProcessMemory(processHandle, pointerToInjurySlot, buffer, (uint)buffer.Length, out _))
                {
                    NativeMethods.CloseHandle(processHandle);
                    return;
                }

                IntPtr baseInjurySlotAddress = (IntPtr.Size == 8) ? (IntPtr)BitConverter.ToInt64(buffer, 0) : (IntPtr)BitConverter.ToInt32(buffer, 0);
                byte[] injuryBytes = Constants.InjuryData.GetInjuryBytes(injuryType);

                bool injuryApplied = false;
                for (int slot = 1; slot <= Constants.Offsets.InjurySlots.TotalSlots; slot++)
                {
                    IntPtr injurySlotAddress = IntPtr.Add(baseInjurySlotAddress, Constants.Offsets.InjurySlots.CalculateOffset(slot));

                    byte[] currentInjuryData = ReadMemoryBytes(processHandle, injurySlotAddress, Constants.Offsets.InjurySlots.SlotSize);
                    if (currentInjuryData == null || !currentInjuryData.SequenceEqual(new byte[Constants.Offsets.InjurySlots.SlotSize]))
                    {
                        continue;
                    }

                    if (NativeMethods.WriteProcessMemory(processHandle, injurySlotAddress, injuryBytes, (uint)injuryBytes.Length, out _))
                    {
                        injuryApplied = true;
                        break;
                    }
                }

                if (!injuryApplied)
                {
                }
                NativeMethods.CloseHandle(processHandle);
            }
            catch (Exception ex)
            {
            }
        }

        internal static void RemoveAllInjuries()
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                return;
            }

            IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            IntPtr moduleBaseAddress = process.MainModule.BaseAddress;
            IntPtr pointerToInjurySlot = moduleBaseAddress + Constants.MainPointerRegionOffset;

            byte[] buffer = new byte[IntPtr.Size];
            if (!NativeMethods.ReadProcessMemory(processHandle, pointerToInjurySlot, buffer, (uint)buffer.Length, out _))
            {
                NativeMethods.CloseHandle(processHandle);
                return;
            }

            IntPtr baseInjurySlotAddress = (IntPtr.Size == 8) ? (IntPtr)BitConverter.ToInt64(buffer, 0) : (IntPtr)BitConverter.ToInt32(buffer, 0);

            byte[] emptyInjuryData = new byte[Constants.Offsets.InjurySlots.SlotSize];


            bool allCleared = true;
            for (int slot = 1; slot <= 68; slot++)
            {
                IntPtr injurySlotAddress = IntPtr.Add(baseInjurySlotAddress, Constants.Offsets.InjurySlots.CalculateOffset(slot));

                bool writeSuccess = NativeMethods.WriteProcessMemory(processHandle, injurySlotAddress, emptyInjuryData, (uint)emptyInjuryData.Length, out _);
                if (!writeSuccess)
                {
                    allCleared = false;
                    break;
                }
            }

            NativeMethods.CloseHandle(processHandle);

            if (allCleared)
            {
            }
        }

        internal static void ModifyHealthOrStamina(Constants.HealthType healthType, int value, bool setExactValue = false)
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

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            IntPtr pointerBase = (IntPtr)Constants.MainPointerRegionOffset;
            IntPtr pointerAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)pointerBase);
            byte[] pointerBuffer = new byte[IntPtr.Size];
            NativeMethods.ReadProcessMemory(processHandle, pointerAddress, pointerBuffer, (uint)pointerBuffer.Length, out _);
            IntPtr valuePointer = (IntPtr.Size == 8) ? (IntPtr)BitConverter.ToInt64(pointerBuffer, 0) : (IntPtr)BitConverter.ToInt32(pointerBuffer, 0);

            int valueOffset;
            switch (healthType)
            {
                case Constants.HealthType.MaxHealth:
                    valueOffset = Constants.Offsets.Health.Max;
                    break;
                case Constants.HealthType.Stamina:
                    valueOffset = Constants.Offsets.Stamina.Current;
                    break;
                default:
                case Constants.HealthType.CurrentHealth:
                    valueOffset = Constants.Offsets.Health.Current;
                    break;
            }

            IntPtr valueAddress = IntPtr.Add(valuePointer, valueOffset);
            byte[] valueBuffer = new byte[sizeof(short)];

            NativeMethods.ReadProcessMemory(processHandle, valueAddress, valueBuffer, (uint)valueBuffer.Length, out _);
            short currentValue = BitConverter.ToInt16(valueBuffer, 0);

            short newValue;
            if (setExactValue)
            {
                newValue = (short)Math.Max(0, Math.Min(value, short.MaxValue));
            }
            else
            {
                newValue = (short)Math.Max(0, Math.Min(currentValue + value, short.MaxValue));
            }

            byte[] newValueBuffer = BitConverter.GetBytes(newValue);
            NativeMethods.WriteProcessMemory(processHandle, valueAddress, newValueBuffer, (uint)newValueBuffer.Length, out _);

            NativeMethods.CloseHandle(processHandle);
        }
    }
}
