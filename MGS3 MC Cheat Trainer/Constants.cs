namespace MGS3_MC_Cheat_Trainer
{
    public class Constants
    {
        public class GameStatus
        {
            internal IntPtr _memoryOffset;
        }

        public interface IMGS3Status
        {
            private static string name = "";
            public static string Name { get { return name; } }
        }

        public abstract class BaseMGS3Status : IMGS3Status
        {
            protected GameStatus gameStatus { get; set; }
            public string Name { get; set; }
            public IntPtr MemoryOffset { get { return gameStatus._memoryOffset; } }

            public BaseMGS3Status(string name, IntPtr memoryOffset)
            {
                Name = name;
                gameStatus = new GameStatus { _memoryOffset = memoryOffset };
            }
        }

        // Dictionary of AOBs
        public static readonly Dictionary<string, (byte[] Pattern, string Mask)> AOBs =
            new Dictionary<string, (byte[] Pattern, string Mask)>
            {
                {
                    "WeaponsTable", // 00 00 AA 77 63 00
                    (new byte[] { 0x00, 0x00, 0xAA, 0x77, 0x63, 0x00 },
                        "xx xx xx xx xx xx")
                },
                {
                    "ItemsTable", // 00 00 DA 5A 2B 00
                    (new byte[] { 0x00, 0x00, 0xDA, 0x5A, 0x2B, 0x00 },
                        "xx xx xx xx xx xx")
                },
                {
                    "NotUpsideDownCamera", // 40 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
                    (new byte[]
                        {
                            0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                        },
                        "xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx"
                    )
                },
                {
                    "UpsideDownCamera", // BF 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
                    (new byte[]
                        {
                            0xBF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                        },
                        "xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx"
                    )
                },
                {
                    "AlertMemoryRegion", // ?? ?? 00 00 ?? ?? 00 00 50 46 00 00 FF FF FF FF
                    // ??xx??xxxxxxxxxx
                    (new byte[] { 0x00, 0x00, 0x50, 0x46, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF },
                        "? ? x x ? ? x x x x x x x x x x"
                    )
                },

                {
                    "LeftBandana", // 33 33 93 3F 9A 99 B9 3F 9A 99 B9 3F
                    (new byte[] { 0x33, 0x33, 0x93, 0x3F, 0x9A, 0x99, 0xB9, 0x3F, 0x9A, 0x99, 0xB9, 0x3F },
                        "xx xx xx xx xx xx xx xx xx xx xx xx"
                    )
                },
                {
                    "RightBandana", // 29 5C 8F 3F 8F C2 B5 3F 8F C2 B5 3F
                    (new byte[] { 0x29, 0x5C, 0x8F, 0x3F, 0x8F, 0xC2, 0xB5, 0x3F, 0x8F, 0xC2, 0xB5, 0x3F },
                        "xx xx xx xx xx xx xx xx xx xx xx xx"
                    )
                },

                {
                    // Fear, Pain and Volgin are confirmed for this AOB Ocelot did not work
                    "TheFearAOB", // F0 49 02 00 F0 49 02 00
                    (new byte[] { 0xF0, 0x49, 0x02, 0x00, 0xF0, 0x49, 0x02, 0x00 },
                        "xx xx xx xx xx xx xx xx")
                },

                {
                    "ModelDistortion", // 45 0F 29 43 C8 45 0F 29 4B B8 45 0F 29 53 A8 45 0F 29 5B 98 45 0F 29 63 88 44 0F 29 6C 24 30
                    (new byte[] { 0x45, 0x0F, 0x29, 0x43, 0xC8, 0x45, 0x0F, 0x29, 0x4B, 0xB8, 0x45, 0x0F, 0x29, 0x53, 0xA8, 0x45, 0x0F, 0x29, 0x5B, 0x98, 0x45, 0x0F, 0x29, 0x63, 0x88, 0x44, 0x0F, 0x29, 0x6C, 0x24, 0x30 },
                        "xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx")
                },

                {
                    "TheFury", // 01 00 01 00 D0 00 00 00 06 00
                    (new byte[] { 0x01, 0x00, 0x01, 0x00, 0xD0, 0x00, 0x00, 0x00, 0x06, 0x00 },
                        "xx xx xx xx xx xx xx xx xx xx")
                },

                {
                    // Starting Area for the boss fight with The End
                    "TheEnds063a", // 01 00 01 00 F0 74 00 00 80
                    (new byte[] { 0x01, 0x00, 0x01, 0x00, 0xF0, 0x74, 0x00, 0x00, 0x80 },
                        "xx xx xx xx xx xx xx xx xx")
                },

                {
                    // Area where The End dies and you proceed to the ladder area also works for s064a
                    "TheEnds065a", // 01 00 01 00 00 01 00 00 80 80 80 80 80 80 80 80
                    (new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80 },
                        "xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx")
                },

                {
                    "Ocelot", // E0 66 00 00 80 80 80 80 80 80 80 80 80
                    (new byte[] { 0xE0, 0x66, 0x00, 0x00, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80 },
                        "xx xx xx xx xx xx xx xx xx xx xx xx xx")
                },

                {
                    "TheBoss", // D0 6E 00 00 80 80 80 80 80 80 80
                    (new byte[] { 0xD0, 0x6E, 0x00, 0x00, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80 },
                        "xx xx xx xx xx xx xx xx xx xx xx")
                },

                {
                    "Shagohod", // E6 99 48 00
                    (new byte[] { 0xE6, 0x99, 0x48, 0x00 },
                        "xx xx xx xx")
                },

                {
                    "VolginOnShagohod", // 00 80 ED C4 00 00 80 3F 00 00 80 3F
                    (new byte[] { 0x00, 0x80, 0xED, 0xC4, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F },
                        "xx xx xx xx xx xx xx xx xx xx xx xx")
                },

                {
                    "SnakeAndBossesStanding", // 00 00 00 90 01 20 03
                    (new byte[] { 0x00, 0x00, 0x00, 0x90, 0x01, 0x20, 0x03 },
                        "xx xx xx xx xx xx xx")
                },

                {
                    "SnakeAndGuardProne", // 00 00 00 96 00 2C 01
                    (new byte[] { 0x00, 0x00, 0x00, 0x96, 0x00, 0x2C, 0x01 },
                        "xx xx xx xx xx xx xx")
                },

                {
                    "GuardPatroling", // 00 00 00 90 01 52 03
                    (new byte[] { 0x00, 0x00, 0x00, 0x90, 0x01, 0x52, 0x03 },
                        "xx xx xx xx xx xx xx")
                },

                {
                    "CamoOperations", // 3D E8 03 00 00 7C 07 B9 13 00 00 00 EB
                    (new byte[] { 0x3D, 0xE8, 0x03, 0x00, 0x00, 0x7C, 0x07, 0xB9, 0x13, 0x00, 0x00, 0x00, 0xEB },
                        "x x x x x x x x x x x x x")
                },

                {
                    "Alphabet", // 30 00 00 31 00 00 32 00 00 33 - 10958/2ACE is the camo index
                    (new byte[] { 0x30, 0x00, 0x00, 0x31, 0x00, 0x00, 0x32, 0x00, 0x00, 0x33 },
                        "x x x x x x x x x x")
                },

                // All Damages are the two bytes before the AOB
                {
                    "MostWeaponsDamage", // 00 00 E8 98 65 FF FF 8B
                    (new byte[] { 0x00, 0x00, 0xE8, 0x98, 0x65, 0xFF, 0xFF, 0x8B },
                        "x x x x x x x x")
                },

                {
                    "M63Damage", // 00 00 E8 6B 6C FF FF 8B
                    (new byte[] { 0x00, 0x00, 0xE8, 0x6B, 0x6C, 0xFF, 0xFF, 0x8B },
                        "x x x x x x x x")
                },

                {
                    "ExplosionDamage", // 00 00 8B CD E8 30 3E 56
                    (new byte[] { 0x00, 0x00, 0x8B, 0xCD, 0xE8, 0x30, 0x3E, 0x56 },
                        "x x x x x x x x")
                },
                // Shotgun and throat slit are different from other weapons
                {
                    // This AOB is directly before the 2 byte value that needs to be set to 90 90 to
                    // disable the damage C8 2B is the default for allowing Shotgun Damage 
                    "ShotgunDamage", // E9 05 09 00 00 8B 96 04 01 00 00 8B CD E8 55 46 56 00 8B 8E 38 01 00 00
                    (new byte[] { 0xE9, 0x05, 0x09, 0x00, 0x00, 0x8B, 0x96, 0x04, 0x01, 0x00, 0x00, 0x8B, 0xCD, 0xE8, 0x55, 0x46, 0x56, 0x00, 0x8B, 0x8E, 0x38, 0x01, 0x00, 0x00 },
                        "x x x x x x x x x x x x x x x x x x x x x x x x")
                },

                {
                    // 22 bytes before the AOB is the 4-byte damage value but the damage value is
                    // actually what it sets the guard's health to
                    "ThroatSlitDamage", // 83 C4 20 5F C3 48 8D 93 00 04 00 00 41 B9 29 
                    (new byte[] { 0x83, 0xC4, 0x20, 0x5F, 0xC3, 0x48, 0x8D, 0x93, 0x00, 0x04, 0x00, 0x00, 0x41, 0xB9, 0x29 },
                        "x x x x x x x x x x x x x x x")
                },
                {   // 22 Bytes before the AOB is the 4-byte damage value
                    "NeckSnapDamage", // 83 C4 20 5F C3 48 8D 93 00 04 00 00 41 B9 2B 00 00 00 4C 8D 05 A2 CA 94
                    (new byte[] { 0x83, 0xC4, 0x20, 0x5F, 0xC3, 0x48, 0x8D, 0x93, 0x00, 0x04, 0x00, 0x00, 0x41, 0xB9, 0x2B },
                        "x x x x x x x x x x x x x x x")
                },
                {   // 0 Bytes before the AOB is the 4-byte damage value
                    "WpNadeDamage", // C3 CC CC CC CC CC 33 C0 39 81 38
                    (new byte[] { 0xC3, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0x33, 0xC0, 0x39, 0x81, 0x38 },
                        "x x x x x x x x x x x")
                },
            };
       
        // Should go and find the float values for HUD and Camera for future use and make it a slider
        public enum HudOptions // 0xADB40F options
        {
            None = 0,
            Normal = 63,
            Shrunk = 64
        }
        public enum CameraOptions // 0xAE3B37 options
        {
            Normal = 191,
            UpsideDown = 64
        }
        public enum AlertModes // 0x1D9C3D8 options
        {
            Normal = 0,
            Alert = 16,
            Caution = 32,
            Evasion = 128 // Doesn't trigger evasion but this is the value for it
        }
        public enum HealthType // Can probably use these enums for boss/eva's stats
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
        internal const int MainPointerRegionOffset = 0x00A9C9D8;
        internal static IntPtr HudOffset = (IntPtr)0xAE345F;

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