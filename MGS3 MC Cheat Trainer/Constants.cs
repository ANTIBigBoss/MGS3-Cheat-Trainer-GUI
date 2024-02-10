using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

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

        public static readonly Dictionary<string, (byte[] Pattern, string Mask)> AOBs = new Dictionary<string, (byte[] Pattern, string Mask)>
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
                (new byte[] {
                    0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                },
                "xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx"
                )
            },
            {
                "UpsideDownCamera", // BF 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
                (new byte[] {
                    0xBF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                },
                "xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx"
                )
            },
            {
                "AlertMemoryRegion", // ?? ?? 00 00 ?? ?? 00 00 50 46 00 00 FF FF FF FF
                (new byte[] { 0x00, 0x00, 0x50, 0x46, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF },
                "?? ?? 00 00 ?? ?? 00 00 50 46 00 00 FF FF FF FF"
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
                (new byte[] { 0x29, 0x5C, 0x8F, 0x3F, 0x8F, 0xC2, 0xB5, 0x3F, 0x8F, 0xC2, 0xB5, 0x3F},
                "xx xx xx xx xx xx xx xx xx xx xx xx"
                )
            },

            {
                "TheFearAOB", // 02 00 F0 49 02 00
                (new byte[] { 0x02, 0x00, 0xF0, 0x49, 0x02, 0x00 },
                "xx xx xx xx xx xx")
            },

        };

        /* List of strings for areas of the game that can be read to use for conditional checks 
           A few things to note:
           1. _0 attached to these strings means a cutscene is playing
           2. v = Virtuous Mission, s = Snake Eater
           3. A lot of areas in memory have these strings so we can use any consistent one to run a check for the area
           4. Some spots in memory use this string + a file path probably to tell the game to load a specific scenario, etc
           i.e (fr/stage/s023a/manifest.txt, assets/gcx/fr/scenerio_stage_s023a.gcx, fr/stage/s023a/cache/00180720.gcx)
        */
        public enum LocationString
        {
            kyle_op, // Opening Snake Eater video also appears when Snake Easter video plays after the Virtuous Mission
            title, // CQC Start screen and Main Menu
            theater, // Demo Theater Mode
            v001a,  // Dremuchji South - Starting Area in Virtuous Mission
            s001a,  // Opening Cutscenes to Operation Snake Eater
            s002a,  // Dremuchji East - OSE Starting Area
            v003a,  // Dremuchji Swampland - Croc area in V mission
            s003a,  // Dremuchji Swampland - Croc area in V mission
            v004a,  // Dremuchji North - First KGB Troop in V and where Boss breaks Snake's gun in SE
            s004a,  // Dremuchji North - First KGB Troop in V and where Boss breaks Snake's gun in SE
            v005a,  // Dolinovodno Rope Bridge - Rope Bridge
            s005a,  // Dolinovodno Rope Bridge - Rope Bridge
            v006a,  // Rassvet - Sokolov/Ocelot Unit
            v006b,  // Rassvet - Sokolov/Ocelot Unit
            s006a,  // Rassvet - Sokolov/Ocelot Unit
            s006b,  // Rassvet - Sokolov/Ocelot Unit
            s007a,  // VM Bridge Fall Aftermath Zone
            v007a,  // Dolinovodno Riverbank
            s012a,  // Chyornyj Prud - Swamp
            s021a,  // Bolshaya Past South
            s022a,  // Bolshaya Past North
            s023a,  // Ocelot Battle Area
            s031a,  // Chyornaya Peschera Cave Branch - Cave After Ocelot
            s032a,  // Chyornaya Peschera Cave - Before and After Pain boss fight
            s032b,  // Chyornaya Peschera Cave - Pain Boss Battle
            s033a,  // Chyornaya Peschera Cave Entrance
            s041a,  // Ponizovje South - Entrance/Sorrow Fight Area
            s042a,  // Ponizovje Armory
            s043a,  // Ponizovje Warehouse Exterior - Outside where you can kill the end early
            s044a,  // Ponizovje Warehouse - Interior of the warehouse
            s051a,  // Graniny Gorki South - Fear Boss fight
            s051b,  // Graniny Gorki South - Fear Boss fight
            s052a,  // Graniny Gorki Lab Exterior: Outside Walls - Area after all traps/where you fight Fear later
            s052b,  // Graniny Gorki Lab Exterior: Inside Walls 
            s053a,  // Graniny Gorki Lab 1F/2F
            s054a,  // Graniny Gorki Lab Interior
            s055a,  // Graniny Gorki Lab B1 (Prison Cells)
            s056a,  // Graniny Gorki Lab B1 (Granin Basement Area)
            s045a,  // Svyatogornyj South - Just after Ponizovje Warehouse
            s061a,  // Svyatogornyj West
            s062a,  // Svyatogornyj East - M63 Area with the house
            s063a,  // Sokrovenno South - The End Fight
            s064a,  // Sokrovenno West - The End fight area with the river
            s065a,  // Sokrovenno North - Area where The End dies and you head to the ladder area
            s066a,  // Krasnogorje Tunnel - What a thrill...
            s071a,  // Krasnogorje Mountain Base - Start of the mountain area
            s072b,  // Krasnogorje Mountainside - s072a might be hovercrafts instead of Hind-D?
            s073a,  // Krasnogorje Mountaintop - Before cutscene with Eva
            s074a,  // Krasnogorje Mountaintop Ruins - Eva Cutscene
            s075a,  // Krasnogorje Mountaintop: Behind Ruins
            s081a,  // Groznyj Grad Underground Tunnel - Before/During/After the fight with The Fury
            s091a,  // Groznyj Grad Southwest - Area after fight with The Fury
            s091b,  // During Escape
            s091c,  // Back after escape
            s092a,  // Groznyj Grad Northwest - Armory/ where you escape into the tunnels
            s092b,  // During Escape
            s092c,  // Back after escape
            s093a,  // Groznyj Grad Northeast - Has the Enterance to the weapon's lab East Wing
            s093b,  // During Escape
            s093c,  // Back after escape
            s094a,  // Groznyj Grad Southeast - Area with Enterance to the prison cells
            s094b,  // During Escape
            s094c,  // Back after escape
            s101a,  // Groznyj Grad Weapon's Lab: East Wing - Where you take Raikov's outfit
            s101b,  // Groznyj Grad Weapon's Lab: East Wing - After taking Raikov's outfit/Also when back to plant the C3
            s113a,  // Groznyj Grad Sewers
            s112a,  // Groznyj Grad Torture Room always this regardless of before, during, or after torture
            s111a,  // Groznyj Grad Weapon's Lab: West Wing Corridor - Before you trigger Sokolov cutscene
            s121a,  // Groznyj Grad Weapon's Lab: Main Wing
            s122a,  // Groznyj Grad Weapon's Lab: Main Wing B1 - Volgin Fight both parts
            s141a,  // Unsure on name of this area - Sorrow Boss Fight
            s151a,  // Tikhogornyj - After Sorrow Fight regardless of if Ocelot Unit is there or not and for Tikhogornyj: Behind Waterfall
            s161a,  // Groznyj Grad - 1st Part of bike chase
            s162a,  // Groznyj Grad Runway South - 2nd Part of bike chase
            s163a,  // Groznyj Grad Runway - 3rd part of bike chase
            s163b,  // Groznyj Grad Runway - 4th path of chase with shagohod only
            s171a,  // Groznyj Grad Rail Bridge - Shooting the C3
            s171b,  // Groznyj Grad Rail Bridge - Fighting the Shagohod on and off the bike
            s181a,  // Groznyj Grad Rail Bridge North - 1st Escape after beating Volgin
            s182a,  // Lazorevo South - Part of the chase with Hovercrafts
            s183a,  // Lazorevo North - Final part of chase before on foot with Eva
            s191a,  // Zaozyorje West - On Foot Area
            s192a,  // Zaozyorje East - Final area before The Boss
            s201a,  // Rokovj Bereg - Boss Arena
            s211a,  // Wig: Interior - When you pick which SAA
            ending  // Credits, etc
        }

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
        internal const int MainPointerRegionOffset = 0x00AEC9D8;
        internal static IntPtr HudOffset = (IntPtr)0xAE345F;

        public class MGS3Distortion : BaseMGS3Status
        {
            public IntPtr ModelManipulationOffset;
            public byte Value { get; set; }

            public MGS3Distortion(string name, IntPtr memoryOffset, byte value) : base(name, memoryOffset)
            {
                ModelManipulationOffset = memoryOffset;
                Value = value;
            }
        }

        public class MGS3DistortionEffects
        {
            public static readonly MGS3Distortion Normal = new("Normal", (IntPtr)0xA4CD2, 80);
        }

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