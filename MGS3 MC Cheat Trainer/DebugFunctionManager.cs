using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    public class DebugFunctionManager
    {
        private static DebugFunctionManager instance;
        private static readonly object lockObj = new object();

        private DebugFunctionManager()
        {
        }

        public static DebugFunctionManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new DebugFunctionManager();
                    }

                    return instance;
                }
            }
        }


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
            return ReadMemoryValue("PissFilter", (int)AnimationOffsets.ForceDirectionAdd, true, 1, DataType.UInt8);
        }

        public string GetSnakeShortSleepValue()
        {
            return ReadMemoryValue("PissFilter", (int)AnimationOffsets.SnakeShortSleepAdd, true, 1, DataType.UInt8);
        }

        public string GetVomitFireValue()
        {
            return ReadMemoryValue("PissFilter", (int)AnimationOffsets.VomitFireAdd, true, 1, DataType.UInt8);
        }

        public string GetBoxCrouchValue()
        {
            return ReadMemoryValue("PissFilter", (int)AnimationOffsets.BoxCrouchAdd, true, 1, DataType.UInt8);
        }

        public string GetBunnyHopValue()
        {
            return ReadMemoryValue("PissFilter", (int)AnimationOffsets.BunnyHopAdd, true, 1, DataType.UInt8);
        }

        public string GetFakeDeathValue()
        {
            return ReadMemoryValue("PissFilter", (int)AnimationOffsets.FakeDeathAdd, true, 1, DataType.UInt8);
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

    }
}



/* Should implement this in here at some point:

private void button5_Click(object sender, EventArgs e)
   {
       var guardsAddresses = XyzManager.Instance.FindAllGuardsPositionAOBs();
       if (guardsAddresses.Count > 0)
       {
           IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
           if (processHandle != IntPtr.Zero)
           {
               var guardsPositions = XyzManager.Instance.ReadGuardsPositions(processHandle, guardsAddresses);
               StringBuilder sb = new StringBuilder();
               foreach (var position in guardsPositions)
               {
                   sb.AppendLine($"Guard Position: \nX={position[0]}, \nY={position[1]}, \nZ={position[2]}\n");
               }
               MessageBox.Show(sb.ToString(), "Guards Positions", MessageBoxButtons.OK, MessageBoxIcon.Information);
   
               NativeMethods.CloseHandle(processHandle);
           }
           else
           {
               MessageBox.Show("Failed to open process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
       }
       else
       {
           MessageBox.Show("No guards found.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
       }
   }

*/