namespace MGS3_MC_Cheat_Trainer
{
    public class Constants
    {
        // For reading different memory types and their values
        public enum DataType
        {
            // UInt = Unsigned Integer (No negative values)
            // Int = Signed Integer (Negative and positive values)
            UInt8, // Unsigned Byte
            Int8,  // Signed Byte
            Int16, // Signed Short
            UInt16,// Unsigned Short
            Int32, // Signed 4 byte integer
            UInt32, // Unsigned 4 byte integer
            Float, // 4 byte floating point number
            Int64, // Signed 8 byte integer
            UInt64, // Unsigned 8 byte integer
            Double, // 8 byte floating point number
            ByteArray, // For anything in between or larger than 8 bytes
        }

        // Add/Sub to indicate which IntPtr to use aka IntPtr.Add or IntPtr.Subtract
        public enum AlertOffsets
        {
            AlertTriggerAdd = 78,
            AlertTimerSub = 6,
            EvasionTimerAdd = 18,
            CautionTimerSub = 2,
        }

        public enum BossOffsets
        {
            OcelotHealthSub = 916,
            OcelotStaminaSub = 908,
            PainFearVolginHealthSub = 16,
            PainFearVolginStaminaSub = 8,
            Ends063aHealthSub = 1360, 
            Ends063aStaminaSub = 1364,
            Ends064as065aHealthSub = 608,
            Ends064as065aStaminaSub = 612,
            FuryHealthSub = 160,
            FuryStaminaSub = 152,
            ShagohodHealthSub = 105,
            VolginOnShagohodHealthSub = 936,
            VolginOnShagohodStaminaSub = 928,
            TheBossHealthSub = 1444,
            TheBossStaminaSub = 1440,
        }

        public enum CameraOptions
        {
            Normal = 191,
            UpsideDown = 64
        }

        public enum AlertModes
        {
            Normal = 0, // Never used in trainer but good to have just in case 
            Alert = 16,
            Caution = 32,
            Evasion = 128 // Doesn't trigger evasion but this is the value for it
        }

        public enum HealthType
        {
            CurrentHealth,
            MaxHealth,
            Stamina
            // Not sure if the max stamina exists or not or is modifiable
        }

        public enum InjuryType
        {
            NoInjury,
            SevereBurns,
            DeepCut,
            GunshotWoundRifle,
            GunshotWoundShotgun,
            BoneFracture,
            BulletBee,
            Leeches,
            ArrowWound,
            TranqDart,
            Poisoned,
            FoodPoisoning,
            Cold,
        }

        public const string PROCESS_NAME = "METAL GEAR SOLID3";

        internal const int MainPointerRegionOffset = 0x00A9DA98;

        public static class InjuryData
        {
            public static byte[] GetInjuryBytes(InjuryType injuryType)
            {
                switch (injuryType)
                {
                    case InjuryType.NoInjury:
                        return new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                    case InjuryType.SevereBurns:
                        return new byte[] { 153, 255, 22, 0, 176, 255, 245, 12, 1, 2, 0, 0, 36, 1 };

                    case InjuryType.DeepCut:
                        return new byte[] { 208, 0, 15, 0, 35, 0, 15, 131, 2, 6, 0, 0, 42, 1 };

                    case InjuryType.GunshotWoundRifle:
                        return new byte[] { 156, 255, 80, 0, 95, 0, 255, 90, 3, 0, 0, 0, 10, 1 };

                    case InjuryType.GunshotWoundShotgun:
                        return new byte[] { 78, 0, 25, 255, 175, 255, 253, 216, 4, 19, 0, 0, 42, 1 };

                    case InjuryType.BoneFracture:
                        return new byte[] { 30, 0, 242, 254, 216, 255, 0, 0, 5, 16, 0, 0, 22, 1 };

                    case InjuryType.BulletBee:
                        return new byte[] { 123, 255, 251, 255, 20, 0, 250, 67, 6, 2, 0, 0, 122, 0 };

                    case InjuryType.Leeches:
                        return new byte[] { 171, 255, 117, 255, 119, 0, 253, 127, 7, 0, 0, 0, 44, 1 };

                    case InjuryType.ArrowWound:
                        return new byte[] { 27, 0, 48, 0, 131, 255, 253, 255, 8, 2, 0, 0, 37, 1 };

                    case InjuryType.TranqDart:
                        return new byte[] { 221, 255, 204, 0, 75, 0, 33, 122, 9, 2, 0, 0, 33, 0 };

                    case InjuryType.Poisoned:
                        return new byte[] { 0, 0, 100, 0, 0, 0, 0, 0, 10, 2, 0, 0, 44, 1 };

                    case InjuryType.FoodPoisoning:
                        return new byte[] { 10, 0, 100, 0, 10, 0, 0, 0, 13, 1, 0, 0, 44, 1 };

                    case InjuryType.Cold:
                        return new byte[] { 0, 0, 100, 0, 0, 0, 0, 0, 12, 4, 0, 0, 44, 1 };

                    default:
                        throw new ArgumentOutOfRangeException(nameof(injuryType), injuryType, null);
                }
            }
        }

        public static class Offsets
        {
            public static class Health
            {
                public const int Current = 0x684;
                public const int Max = 0x686;
            }

            public static class Stamina
            {
                public const int Current = 0xA4A;
            }

            public static class InjurySlots
            {
                public const int TotalSlots = 68;
                public const int SlotSize = 14; // Size of each injury slot
                public const int BaseOffset = 0x688; // base offset for the first injury

                public static int CalculateOffset(int slotNumber)
                {
                    return BaseOffset + (SlotSize * (slotNumber - 1));
                }
            }
        }
    }
}