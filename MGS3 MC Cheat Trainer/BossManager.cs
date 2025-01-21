using System.Reflection.Metadata;

namespace MGS3_MC_Cheat_Trainer
{
    public class BossManager
    {
        private static AobManager _instance;

        public static short FindOcelotAOB()
        {
            if (!AobManager.Instance.FindAndStoreOcelotAOB())
            {
                if (!AobManager.Instance.FindAndStoreOcelotAOB())
                {
                    LoggingManager.Instance.Log("Ocelot AOB address not found.");
                    return -1;
                }
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr ocelotAddress = AobManager.Instance.FoundOcelotAddress;

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, ocelotAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {               
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteOcelotHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundOcelotAddress, (int)Constants.BossOffsets.OcelotHealthSub);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadOcelotHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundOcelotAddress, (int)Constants.BossOffsets.OcelotHealthSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsOcelotDead()
        {
            return ReadOcelotHealth() == 0;
        } 

        public static void WriteOcelotStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundOcelotAddress, (int)Constants.BossOffsets.OcelotStaminaSub);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadOcelotStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundOcelotAddress, (int)Constants.BossOffsets.OcelotStaminaSub);
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short staminaValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsOcelotStunned()
        {
            return ReadOcelotStamina() == 0;
        }

        public static void WriteThePainHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginHealthSub);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadThePainHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginHealthSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsThePainDead()
        {
            return ReadThePainHealth() == 0;
        }

        public static void WriteThePainStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginStaminaSub);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadThePainStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginStaminaSub);
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short staminaValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsThePainStunned()
        {
            return ReadThePainStamina() == 0;
        }

        public static short FindTheFearAOB()
        {

            if (!AobManager.Instance.FindAndStoreTheFearAOB())
            {
                if (!AobManager.Instance.FindAndStoreTheFearAOB())
                {
                    LoggingManager.Instance.Log("The Fear AOB address not found.");
                    return -1;
                }
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, AobManager.Instance.FoundTheFearAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteTheFearHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress,
                (int)Constants.BossOffsets.PainFearVolginHealthSub);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFearHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginHealthSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsTheFearDead()
        {
            return ReadTheFearHealth() == 0;
        }

        public static void WriteTheFearStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginStaminaSub);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFearStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginStaminaSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short staminaValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsTheFearStunned()
        {
            return ReadTheFearStamina() == 0;
        }

        #region The End

        public static short FindTheEnds063aAOB()
        {
            if (!AobManager.Instance.FindAndStoreTheEnds063aAOB())
            {
                LoggingManager.Instance.Log("The Ends063a AOB address not found.");
                return -1;
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, AobManager.Instance.FoundTheEnds063aAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteTheEnds063aHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds063aAddress, (int)Constants.BossOffsets.Ends063aHealthSub);
            /* Interesting accident find but if we go back to 1384 instead of 1360 we can just make
               The End invincible maybe something to look into once everyone else is implemented*/
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheEnds063aHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds063aAddress, (int)Constants.BossOffsets.Ends063aHealthSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsTheEnds063aDead()
        {
            return ReadTheEnds063aHealth() == 0;
        }

        public static void WriteTheEnds063aStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds063aAddress, (int)Constants.BossOffsets.Ends063aStaminaSub);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheEnds063aStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds063aAddress, (int)Constants.BossOffsets.Ends063aStaminaSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short staminaValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsTheEnds063aStunned()
        {
            return ReadTheEnds063aStamina() == 0;
        }

        /* This find will apply to s064a as well as s065a so the Switch Case 
         * in BossForm will use this but then use the offset differences 
         * that s064a has to get to The End's health and stamina */
        public static short FindTheEnds065aAOB()
        {
            if (!AobManager.Instance.FindAndStoreTheEnds065aAOB())
            {
                LoggingManager.Instance.Log("The Ends065a AOB address not found.");
                return -1;
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, AobManager.Instance.FoundTheEnds065aAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteTheEnds065aHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds065aAddress, (int)Constants.BossOffsets.Ends064as065aHealthSub);
            // Interesting accidental find but if we go back to 1384 instead of 1360 we can just make The End invincible maybe something to look into once everyone else is implemented
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheEnds065aHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds065aAddress, (int)Constants.BossOffsets.Ends064as065aHealthSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsTheEnds065aDead()
        {
            return ReadTheEnds065aHealth() == 0;
        }

        public static void WriteTheEnds065aStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds065aAddress, (int)Constants.BossOffsets.Ends064as065aStaminaSub);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheEnds065aStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds065aAddress, (int)Constants.BossOffsets.Ends064as065aStaminaSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short staminaValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsTheEnds065aStunned()
        {
            return ReadTheEnds065aStamina() == 0;
        }

        #endregion

        #region The Fury
        public static short FindTheFuryAOB()
        {
            if (!AobManager.Instance.FindAndStoreTheFuryAOB())
            {
                LoggingManager.Instance.Log("The Fury AOB address not found.");
                return -1;
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, AobManager.Instance.FoundTheFuryAddress, sizeof(short));

            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteTheFuryHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFuryAddress, (int)Constants.BossOffsets.FuryHealthSub);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFuryHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFuryAddress, (int)Constants.BossOffsets.FuryHealthSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsTheFuryDead()
        {
            return ReadTheFuryHealth() == 0;
        }

        public static void WriteTheFuryStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFuryAddress, (int)Constants.BossOffsets.FuryStaminaSub);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFuryStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFuryAddress, (int)Constants.BossOffsets.FuryStaminaSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short staminaValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsTheFuryStunned()
        {
            return ReadTheFuryStamina() == 0;
        }
       
        #endregion

        #region Volgin

        public static void WriteVolginHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginHealthSub);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadVolginHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginHealthSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsVolginDead()
        {
            return ReadVolginHealth() == 0;
        }

        public static void WriteVolginStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginStaminaSub);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadVolginStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, (int)Constants.BossOffsets.PainFearVolginStaminaSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short staminaValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsVolginStunned()
        {
            return ReadVolginStamina() == 0;
        }

        #endregion

        #region Shagohod

        public static short FindShagohodAOB()
        {
            if (!AobManager.Instance.FindAndStoreShagohodAOB())
            {
                LoggingManager.Instance.Log("Shagohod AOB address not found.");
                return -1;
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, AobManager.Instance.FoundShagohodAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteShagohodHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundShagohodAddress, (int)Constants.BossOffsets.ShagohodHealthSub);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadShagohodHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundShagohodAddress, (int)Constants.BossOffsets.ShagohodHealthSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsShagohodDead()
        {
            return ReadShagohodHealth() == 0;
        }

        // Shagohod doesn't have stamina so continue to the next boss

        #endregion

        #region Volgin on Shagohod

        public static short FindVolginOnShagohodAOB()
        {
            if (!AobManager.Instance.FindAndStoreVolginOnShagohodAOB())
            {
                LoggingManager.Instance.Log("Volgin on Shagohod AOB address not found.");
                return -1;
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, AobManager.Instance.FoundVolginOnShagohodAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteVolginOnShagohodHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundVolginOnShagohodAddress, (int)Constants.BossOffsets.VolginOnShagohodHealthSub);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadVolginOnShagohodHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundVolginOnShagohodAddress, (int)Constants.BossOffsets.VolginOnShagohodHealthSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsVolginOnShagohodDead()
        {
            return ReadVolginOnShagohodHealth() == 0;
        }

        public static void WriteVolginOnShagohodStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundVolginOnShagohodAddress, (int)Constants.BossOffsets.VolginOnShagohodStaminaSub);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadVolginOnShagohodStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundVolginOnShagohodAddress, (int)Constants.BossOffsets.VolginOnShagohodStaminaSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short staminaValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsVolginOnShagohodStunned()
        {
            return ReadVolginOnShagohodStamina() == 0;
        }

        #endregion

        #region The Boss

        public static short FindTheBossAOB()
        {
            if (!AobManager.Instance.FindAndStoreTheBossAOB())
            {
                LoggingManager.Instance.Log("The Boss AOB address not found.");
                return -1;
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, AobManager.Instance.FoundTheBossAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteTheBossHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheBossAddress, (int)Constants.BossOffsets.TheBossHealthSub);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheBossHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheBossAddress, (int)Constants.BossOffsets.TheBossHealthSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short healthValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsTheBossDead()
        {
            return ReadTheBossHealth() == 0;
        }

        public static void WriteTheBossStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheBossAddress, (int)Constants.BossOffsets.TheBossStaminaSub);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheBossStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheBossAddress, (int)Constants.BossOffsets.TheBossStaminaSub);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1;
            }

            short staminaValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsTheBossStunned()
        {
            return ReadTheBossStamina() == 0;
        }

        #endregion

    }
}