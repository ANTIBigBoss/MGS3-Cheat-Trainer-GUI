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

        #region Misc Form Functions

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
            return ReadMemoryValue("CQCSlamNormal", 24, true, 4, DataType.Int32);
        }

        public string CQCSlamExtremeDamage()
        {
            return ReadMemoryValue("CQCSlamNormal", 35, true, 4, DataType.Int32);
        }

        public string WpNadeDamage()
        {
            return ReadMemoryValue("WpNadeDamage", 4, false, 4, DataType.UInt32);
        }

        public string ZzzDrain()
        {
            return ReadMemoryValue("SleepControl2", 6, false, 6, DataType.ByteArray);
        }

        public string ShotgunDamage()
        {
            return ReadMemoryValue("ShotgunDamage", 6, false, 6, DataType.ByteArray);
        }

        public string M63Damage()
        {
            return ReadMemoryValue("M63Damage", 2, false, 2, DataType.UInt16);
        }

        public string StunNadeDamage()
        {
            return ReadMemoryValue("StunNadeDamage", 6, false, 6, DataType.ByteArray);
        }

        public string KnifeForkDamage()
        {
            return ReadMemoryValue("KnifeForkDamage", 6, false, 6, DataType.ByteArray);
        }

        public string TriplePunchDamage()
        {
            return ReadMemoryValue("StunPunchDamage", 1775, false, 1, DataType.UInt8);
        }

        public string StunRollDamage()
        {
            return ReadMemoryValue("StunRollDamage", 6, false, 6, DataType.ByteArray);
        }

        public string ZzzWeaponsDamage1()
        {
            return ReadMemoryValue("ZZZWeaponsDamage", 4, false, 4, DataType.UInt32);
        }

        public string ZzzWeaponsDamage2()
        {
            return ReadMemoryValue("StunPunchDamage", 8970, true, 4, DataType.UInt32);
        }

        public string MostLethalWeaponsDamage()
        {
            return ReadMemoryValue("MostWeaponsDamage", 2, false, 2, DataType.UInt16);
        }

        public string ExplosionDamage()
        {
            return ReadMemoryValue("ExplosionDamage", 24, true, 4, DataType.UInt32);
        }

        public string ThroatSlitDamage()
        {
            return ReadMemoryValue("ThroatSlitDamage", 26, false, 4, DataType.UInt32);
        }

        public string SleepStatus1()
        {
            return ReadMemoryValue("SleepControl", 6, false, 6, DataType.ByteArray);
        }

        public string SleepStatus2()
        {
            return ReadMemoryValue("SleepControl", 6, false, 6, DataType.ByteArray);
        }

        public string StunPunchInstructions()
        {
            return ReadMemoryValue("StunPunchDamage", 24, true, 6, DataType.ByteArray);
        }

        public string SinglePunchDamage()
        {
            return ReadMemoryValue("StunPunchDamage", 2, false, 1, DataType.UInt8);
        }

        public string PunchKnockOverThreshold()
        {
            return ReadMemoryValue("StunPunchDamage", 33, true, 1, DataType.UInt8);
        }

        public string GetPissFilterValueAsString()
        {
            return ReadMemoryValue("PissFilter", 5525, false, 1, DataType.Int8);
        }

        public string ReadAlertStatus()
        {
            return ReadMemoryValue("AlertMemoryRegion", 78, true, 1, DataType.UInt8);
        }

        #endregion

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