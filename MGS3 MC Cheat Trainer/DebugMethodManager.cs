using System.Text;
using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    public class DebugMethodManager
    {
        private static DebugMethodManager instance;
        private static readonly object lockObj = new object();

        private DebugMethodManager()
        {
        }

        public static DebugMethodManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new DebugMethodManager();
                    }

                    return instance;
                }
            }
        }

        /// <summary>
        /// aobKey is the key to the AOB pattern in the dictionary<br></br><br></br>
        /// offset is the offset to add or subtract from the AOB pattern<br></br><br></br>
        /// forwardInMemory is TRUE means Forward in memory, FALSE means Backward in memory<br></br><br></br>
        /// bytesToRead is the number of bytes to read from the target address<br></br><br></br>
        /// dataType is the Data type listed in the Constants.DataType enum eg:<br></br> UInt8, Int8, Int16, UInt16, Int32, UInt32, Float, Int64, UInt64, Double, ByteArray
        /// </summary>
        /// <param name="aobKey"></param>
        /// <param name="offset"></param>
        /// <param name="forwardInMemory"></param>
        /// <param name="bytesToRead"></param>
        /// <param name="dataType"> </param>
        /// <returns></returns>
        public string ReadMemoryValue(string aobKey, int offset, bool forwardInMemory, int bytesToRead, DataType dataType)
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                return "Error: Could not open process.";
            }

            IntPtr aobResult = MemoryManager.Instance.FindAob(aobKey);
            if (aobResult == IntPtr.Zero)
            {
                return "Error: AOB pattern not found.";
            }

            IntPtr targetAddress = forwardInMemory ? IntPtr.Add(aobResult, offset) : IntPtr.Subtract(aobResult, offset);
            return MemoryManager.ReadMemoryValueAsString(processHandle, targetAddress, bytesToRead, dataType);
        }

        public string CQCSlamNormalDamage()
        {
            return ReadMemoryValue("CQCSlamNormal", (int)DamageOffsets.CQCSlam1Add, true, 4, DataType.Int32);
        }

        public string CQCSlamExtremeDamage()
        {
            return ReadMemoryValue("CQCSlamNormal", (int)DamageOffsets.CQCSlam2Add, true, 4, DataType.Int32);
        }

        public string WpNadeDamage()
        {
            return ReadMemoryValue("WpNadeDamage", (int)DamageOffsets.WpNadeSub, false, 4, DataType.UInt32);
        }

        public string ZzzDrain()
        {
            return ReadMemoryValue("SleepControl2", (int)DamageOffsets.ZzzDrainSub, false, 6, DataType.ByteArray);
        }

        public string SleepStatus2()
        {
            return ReadMemoryValue("SleepControl", (int)DamageOffsets.SleepStatus2Sub, false, 4, DataType.Int32);
        }

        public string SleepStatus1()
        {
            return ReadMemoryValue("SleepControl", (int)DamageOffsets.SleepStatus1Sub, false, 6, DataType.ByteArray);
        }

        public string ZzzWeaponsDamage1()
        {
            return ReadMemoryValue("ZZZWeaponsDamage", (int)DamageOffsets.ZzzWeapons1Sub, false, 4, DataType.UInt32);
        }

        public string ShotgunDamage()
        {
            return ReadMemoryValue("ShotgunDamage", (int)DamageOffsets.ShotgunSub, false, 6, DataType.ByteArray);
        }

        public string M63Damage()
        {
            return ReadMemoryValue("M63Damage", (int)DamageOffsets.M63Sub, false, 2, DataType.UInt16);
        }

        public string StunNadeDamage()
        {
            return ReadMemoryValue("StunNadeDamage", (int)DamageOffsets.StunNadeSub, false, 6, DataType.ByteArray);
        }

        public string KnifeForkDamage()
        {
            return ReadMemoryValue("KnifeForkDamage", (int)DamageOffsets.KnifeForkSub, false, 6, DataType.ByteArray);
        }

        public string TriplePunchDamage()
        {
            return ReadMemoryValue("StunPunchDamage", (int)DamageOffsets.TriplePunchSub, false, 1, DataType.UInt8);
        }

        public string StunRollDamage()
        {
            return ReadMemoryValue("StunRollDamage", (int)DamageOffsets.StunRollSub, false, 6, DataType.ByteArray);
        }

        public string MostLethalWeaponsDamage()
        {
            return ReadMemoryValue("MostWeaponsDamage", (int)DamageOffsets.MostLethalSub, false, 2, DataType.UInt16);
        }

        public string ExplosionDamage()
        {
            return ReadMemoryValue("ExplosionDamage", (int)DamageOffsets.ExplosiveAdd, true, 4, DataType.UInt32);
        }

        public string ThroatSlitDamage()
        {
            return ReadMemoryValue("ThroatSlitDamage", (int)DamageOffsets.ThroatSlitSub, false, 4, DataType.UInt32);
        }

        public string StunPunchInstructions()
        {
            return ReadMemoryValue("StunPunchDamage", (int)DamageOffsets.StunPunchInstructionsAdd, true, 6, DataType.ByteArray);
        }

        public string SinglePunchDamage()
        {
            return ReadMemoryValue("StunPunchDamage", (int)DamageOffsets.SinglePunchSub, false, 1, DataType.UInt8);
        }

        public string PunchKnockOverThreshold()
        {
            return ReadMemoryValue("StunPunchDamage", (int)DamageOffsets.PunchKnockOverAdd, true, 1, DataType.UInt8);
        }

        public string GetPissFilterValueAsString()
        {
            return ReadMemoryValue("PissFilter", (int)MiscOffsets.PissFilterSub, false, 1, DataType.Int8);
        }

        /* ChangeLightNearSnakeAdd = 5571, // Night = 0x40 0x1C 0xC8 || Day = 0x24 0xF4 0xC7
            ColourMapChangingAdd = 5580, // Night = 0x00 0x1B 0x0A 0x03 || Day = 0x00 0x5E 0x72 0x64
            SkyColourChangingAdd = 5584, // Night = 0x00 0x1F 0x13 0x09 || Day = 0x00 0xFF 0xFF 0x44
            SkyChangingAdd = 5588, // Night = x0F || Day = x05 
        */
        public string GetLightNearSnakeValueAsString()
        {
            return ReadMemoryValue("PissFilter", (int)TimeOfDayOffsets.ChangeLightNearSnakeSub, false, 3, DataType.ByteArray);
        }

        public string GetColourMapValueAsString()
        {
            return ReadMemoryValue("PissFilter", (int)TimeOfDayOffsets.ColourMapChangingSub, false, 4, DataType.ByteArray);
        }

        public string GetSkyColourValueAsString()
        {
            return ReadMemoryValue("PissFilter", (int)TimeOfDayOffsets.SkyColourChangingSub, false, 4, DataType.ByteArray);
        }

        public string GetSkyChangingByteValueAsString()
        {
            return ReadMemoryValue("PissFilter", (int)TimeOfDayOffsets.SkyChangingSub, false, 1, DataType.Int8);
        }

        public string GetSnakeLongSleepValue()
        {
            return ReadMemoryValue("PissFilter", (int)AnimationOffsets.SnakeLongSleepSub, false, 1, DataType.Int8);
        }

        public string GetForceDirectionValue()
        {
            return ReadMemoryValue("Alphabet", (int)AnimationOffsets.ForceDirectionSub, false, 1, DataType.UInt8);
        }

        public string GetSnakeShortSleepValue()
        {
            return ReadMemoryValue("Alphabet", (int)AnimationOffsets.SnakeShortSleepSub, false, 1, DataType.UInt8);
        }

        public string GetVomitFireValue()
        {
            return ReadMemoryValue("Alphabet", (int)AnimationOffsets.VomitFireSub, false, 1, DataType.UInt8);
        }

        public string GetBoxCrouchValue()
        {
            return ReadMemoryValue("Alphabet", (int)AnimationOffsets.BoxCrouchSub, false, 1, DataType.UInt8);
        }

        public string GetBunnyHopValue()
        {
            return ReadMemoryValue("Alphabet", (int)AnimationOffsets.VomitFireSub, false, 1, DataType.UInt8);
        }

        public string GetFakeDeathValue()
        {
            return ReadMemoryValue("Alphabet", (int)AnimationOffsets.FakeDeathSub, false, 1, DataType.UInt8);
        }

        public string ReadAlertStatus()
        {
            return ReadMemoryValue("AlertMemoryRegion", (int)AlertOffsets.AlertTriggerAdd, true, 1, DataType.UInt8);
        }

        public string ReadBatteryInstructions()
        {
            return ReadMemoryValue("BatteryDrain", (int)MiscOffsets.BatteryDrainInstructionsSub, false, 7, DataType.ByteArray);
        }

        public string ReadInfiniteAmmoAndReload()
        {
            return ReadMemoryValue("InfiniteAmmoAndReload", (int)MiscOffsets.InfiniteAmmoAndReloadSub, false, 4, DataType.UInt32);
        }

        public string GetPartialHudValue()
        {
            return ReadMemoryValue("PissFilter", (int)MiscOffsets.NoHudPartialSub, false, 1, DataType.UInt8);
        }

        public string GetItemAndWeaponWindowValue()
        {
            return ReadMemoryValue("PissFilter", (int)MiscOffsets.ItemAndWeaponWindowAdd, true, 1, DataType.UInt8);
        }

        public string RealTimeWeaponItemSwapping()
        {
            return ReadMemoryValue("RealTimeItemSwap", (int)MiscOffsets.RealTimeWeaponItemSwappingAdd, true, 6, DataType.ByteArray);
        }

        public string GetDifficultyValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.DifficultySub, false, 1, DataType.UInt8);
        }

        public string GetContinuesValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.ContinuesSub, false, 2, DataType.UInt16);
        }

        public string GetSavesValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SavesSub, false, 2, DataType.UInt16);
        }

        public string GetAlertsTriggeredValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.AlertsTriggeredSub, false, 2, DataType.UInt16);
        }

        public string GetHumansKilledValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.HumansKilledSub, false, 2, DataType.UInt16);
        }

        public string GetSpecialItemsUsedValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SpecialItemsUsedSub, false, 1, DataType.UInt8);
        }

        public string GetPlantsAndAnimalsCapturedValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.PlantsAndAnimalsCapturedSub, false, 1, DataType.UInt8);
        }

        public string GetMealsEatenValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.MealsEatenSub, false, 2, DataType.UInt16);
        }

        public string GetSeriousInjuriesValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjuriesSub, false, 2, DataType.UInt16);
        }

        public string GetTotalDamageTakenValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.TotalDamageTakenSub, false, 4, DataType.UInt32);
        }

        public string GetPlayTimeValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.PlayTimeSub, false, 4, DataType.UInt32);
        }

        public string GetLifeMedsUsedValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.LifeMedsUsedSub, false, 2, DataType.UInt16);
        }

        public string GetMapStringValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.MapStringSub, false, 16, DataType.ByteArray);
        }

        public string GetR_SnaValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SnakesId_r_sna01Sub, false, 16, DataType.ByteArray);
        }

        public string GetSnakesEquippedWeaponValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SnakesEquippedWeaponSub, false, 1, DataType.UInt8);
        }

        public string GetSnakesEquippedItemValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SnakesEquippedItemSub, false, 1, DataType.UInt8);
        }

        public string GetSnakesFacepaintValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SnakesEquippedFacepaintSub, false, 1, DataType.UInt8);
        }

        public string GetSnakesCamoValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SnakesEquippedCamoSub, false, 1, DataType.UInt8);
        }

        public string GetSnakesCurrentHealthValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SnakesCurrentHealthSub, false, 2, DataType.UInt16);
        }

        public string GetSnakesMaxHealthValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SnakesMaxHealthSub, false, 2, DataType.UInt16);
        }

        public string GetSnakesCurrentStaminaValue()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SnakesCurrentStaminaSub, false, 2, DataType.UInt16);
        }

        #region 68 of Snake's Injury Slots
        public string GetSeriousInjury1Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury1Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury2Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury2Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury3Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury3Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury4Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury4Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury5Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury5Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury6Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury6Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury7Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury7Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury8Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury8Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury9Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury9Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury10Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury10Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury11Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury11Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury12Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury12Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury13Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury13Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury14Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury14Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury15Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury15Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury16Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury16Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury17Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury17Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury18Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury18Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury19Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury19Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury20Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury20Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury21Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury21Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury22Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury22Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury23Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury23Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury24Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury24Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury25Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury25Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury26Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury26Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury27Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury27Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury28Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury28Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury29Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury29Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury30Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury30Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury31Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury31Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury32Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury32Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury33Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury33Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury34Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury34Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury35Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury35Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury36Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury36Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury37Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury37Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury38Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury38Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury39Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury39Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury40Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury40Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury41Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury41Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury42Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury42Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury43Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury43Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury44Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury44Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury45Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury45Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury46Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury46Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury47Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury47Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury48Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury48Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury49Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury49Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury50Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury50Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury51Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury51Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury52Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury52Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury53Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury53Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury54Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury54Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury55Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury55Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury56Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury56Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury57Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury57Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury58Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury58Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury59Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury59Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury60Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury60Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury61Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury61Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury62Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury62Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury63Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury63Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury64Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury64Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury65Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury65Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury66Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury66Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury67Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury67Sub, false, 14, DataType.ByteArray);
        }

        public string GetSeriousInjury68Value()
        {
            return ReadMemoryValue("PointerBytes", (int)MainPointerAddresses.SeriousInjury68Sub, false, 14, DataType.ByteArray);
        }

        #endregion

    }
}