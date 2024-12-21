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

        public enum DamageOffsets
        {
            CQCSlam1Add = 24,
            CQCSlam2Add = 35,
            WpNadeSub = 4,
            ZzzDrainSub = 6,
            ShotgunSub = 6,
            M63Sub = 2,
            StunNadeSub = 6,
            KnifeForkSub = 6,
            TriplePunchSub = 1775,
            StunRollSub = 6,
            ZzzWeapons1Sub = 4,
            MostLethalSub = 2,
            ExplosiveAdd = 24,
            ThroatSlitSub = 26,
            SleepStatus1Sub = 6,
            SleepStatus2Sub = 90,
            StunPunchInstructionsAdd = 24,
            SinglePunchSub = 2,
            PunchKnockOverAdd = 33
        }

        public enum MiscOffsets
        {
            PissFilterSub = 5525,
            FovOffsetSub = 4,
            PissInstructionsOffset1 = 1,
            PissInstructionsOffset2 = 2781,
            BatteryDrainInstructionsSub = 7,
            InfiniteAmmoAndReloadSub = 4,
            NoHudPartialSub = 19,
            NoHudFullSub = 20,
            ItemAndWeaponWindowAdd = 136620,
            RealTimeWeaponItemSwappingAdd = 1023,
        }

        public enum AnimationOffsets
        {
            // Difference from Piss Filter AOB not sure if anything closer is stable or not but we can compare post update if this works
            // It did not work they gone
            SnakeLongSleepSub = 0, // Refind this broke during MGS3's 2.0.0 update
            ForceDirectionSub = 11749,
            SnakeShortSleepSub = 11735,
            VomitFireSub = 11734,
            BoxCrouchSub = 11724,
            BunnyHopSub = 11722,
            FakeDeathSub = 11720,
            RealDeathSub = 11718, 
            StopSnakeMovementSub = 11718,
            StopSnakeMovementFpvSub = 11731,
            StopSnakeMovementNoDamageAdd = 32715

        }

        // Sub and Add determine to use IntPtr.Add or IntPtr.Subtract
        public enum MainPointerAddresses
        {
            StartOfPointerSub = 2636, // Byte: will probably only use as a reference point
            // Game Stats, some patterns here don't make sense so might need to confirm with Swiss:
            DifficultySub = 2630, // Byte: 10 = V.Easy, 20 = Easy, 30 = Normal, 40 = Hard, 50 = Extreme, 60 = European Extreme
            ContinuesSub = 2584, // Short
            SavesSub = 2582, // Short
            AlertsTriggeredSub = 2580, // Short
            HumansKilledSub = 2578, // Short
            SpecialItemsUsedSub = 2575, // Stealth = 1, Infinity FP = 2, Ez Gun = 4. Adding the totals tells what you used. 
            PlantsAndAnimalsCapturedSub = 2573, // Swiss had 2 bytes but a max of 48 would imply it's probably a byte
            SeriousInjuriesSub = 2572, // Short
            TotalDamageTakenSub = 2570, // Might be 4 bytes/Int32
            TotalDamageTakenSubTest = 2568,
            MealsEatenSub = 2566,
            PlayTimeSub = 2560, // Probably 4 bytes/Int32, MGS1 had 4 bytes for playtime
            LifeMedsUsedSub = 1188, // Short: Weird this one is so far away, but confirmed it worked.
            
            KerotansShotSub = 0, // Need to figure out where this is wasn't in the CT Swiss sent me

            // Misc Snake's Stats:
            SnakesId_r_sna01Sub = 2616, // String: Not sure if will use in the trainer but good to have
            MapStringSub = 2600, // String: This is the address that we are using for the map string
            SnakesEquippedWeaponSub = 1144, // Byte: Careful not to equip something out of Snake's backpack or it will crash
            SnakesEquippedItemSub = 1142, // Byte: Seems more relaxed on not crashing the game if out of Snake's backpack
            SnakesEquippedCamoSub = 974, // Byte: Don't exceed max count or equip something not acquired yet
            SnakesEquippedFacepaintSub = 973, // Byte: Don't exceed max count or equip something not acquired yet
            SnakesCurrentHealthSub = 968, // Short: Can go to max of a short but I wouldn't advise going over 400
            SnakesMaxHealthSub = 966, // Short: Same as above but healing an injury when over 400 will bring it back down to 400
            SnakesCurrentStaminaSub = 2, // Short: Can go to max of a short but doesn't do much I'd advise the max being 30000

            // Serious Injuries (Each of these is 14 bytes after the other) based on the old logic I had 68 slots:
            SeriousInjury1Sub = 964, // Each array is 14 bytes long
            SeriousInjury2Sub = 950,
            SeriousInjury3Sub = 936,
            SeriousInjury4Sub = 922,
            SeriousInjury5Sub = 908,
            SeriousInjury6Sub = 894,
            SeriousInjury7Sub = 880,
            SeriousInjury8Sub = 866,
            SeriousInjury9Sub = 852,
            SeriousInjury10Sub = 838,
            SeriousInjury11Sub = 824,
            SeriousInjury12Sub = 810,
            SeriousInjury13Sub = 796,
            SeriousInjury14Sub = 782,
            SeriousInjury15Sub = 768,
            SeriousInjury16Sub = 754,
            SeriousInjury17Sub = 740,
            SeriousInjury18Sub = 726,
            SeriousInjury19Sub = 712,
            SeriousInjury20Sub = 698,
            SeriousInjury21Sub = 684,
            SeriousInjury22Sub = 670,
            SeriousInjury23Sub = 656,
            SeriousInjury24Sub = 642,
            SeriousInjury25Sub = 628,
            SeriousInjury26Sub = 614,
            SeriousInjury27Sub = 600,
            SeriousInjury28Sub = 586,
            SeriousInjury29Sub = 572,
            SeriousInjury30Sub = 558,
            SeriousInjury31Sub = 544,
            SeriousInjury32Sub = 530,
            SeriousInjury33Sub = 516,
            SeriousInjury34Sub = 502,
            SeriousInjury35Sub = 488,
            SeriousInjury36Sub = 474,
            SeriousInjury37Sub = 460,
            SeriousInjury38Sub = 446,
            SeriousInjury39Sub = 432,
            SeriousInjury40Sub = 418,
            SeriousInjury41Sub = 404,
            SeriousInjury42Sub = 390,
            SeriousInjury43Sub = 376,
            SeriousInjury44Sub = 362,
            SeriousInjury45Sub = 348,
            SeriousInjury46Sub = 334,
            SeriousInjury47Sub = 320,
            SeriousInjury48Sub = 306,
            SeriousInjury49Sub = 292,
            SeriousInjury50Sub = 278,
            SeriousInjury51Sub = 264,
            SeriousInjury52Sub = 250,
            SeriousInjury53Sub = 236,
            SeriousInjury54Sub = 222,
            SeriousInjury55Sub = 208,
            SeriousInjury56Sub = 194,
            SeriousInjury57Sub = 180,
            SeriousInjury58Sub = 166,
            SeriousInjury59Sub = 152,
            SeriousInjury60Sub = 138,
            SeriousInjury61Sub = 124,
            SeriousInjury62Sub = 110,
            SeriousInjury63Sub = 96,
            SeriousInjury64Sub = 82,
            SeriousInjury65Sub = 68,
            SeriousInjury66Sub = 54,
            SeriousInjury67Sub = 40,
            SeriousInjury68Sub = 26,
        }


        public enum TimeOfDayOffsets
        {
            ChangeLightNearSnakeSub = 5571, // Night = 0x40 0x1C 0xC8 || Day = 0x24 0xF4 0xC7
            ColourMapChangingSub = 5580, // Night = 0x00 0x1B 0x0A 0x03 || Day = 0x00 0x5E 0x72 0x64
            SkyColourChangingSub = 5584, // Night = 0x00 0x1F 0x13 0x09 || Day = 0x00 0xFF 0xFF 0x44
            SkyChangingSub = 5588, // Night = x0F || Day = x05 
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

        internal const int MainPointerRegionOffset = 0x00ACBE18;

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