using MGS3_MC_Cheat_Trainer;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    // This whole form needs some classes setup but I wanted to verify each worked with how
    // tempermental the game is I will probably do this sometime after I release V 2.0 publicly
    // and the auto update feature is working but for now this works and is a good start
    public class BossManager
    {
        // Instance of AobManager
        private static AobManager _instance;


        public static short FindOcelotAOB()
        {
            // Try twice to find the Ocelot AOB if the first attempt fails
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

            // Read health value using the new ReadMemoryBytes function
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, ocelotAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {ocelotAddress}");
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
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundOcelotAddress, 916); // Health offset
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }


        public static short ReadOcelotHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundOcelotAddress, 916); // Adjust for actual health offset

            // Read health value using the new ReadMemoryBytes function
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {healthAddress}");
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1; // Return an error indicator, such as -1
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
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundOcelotAddress, 908); // Stamina offset
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadOcelotStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundOcelotAddress, 908);
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {staminaAddress}");
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
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 16); // Health offset
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadThePainHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 16); // Adjust for actual health offset

            // Read health value using the new ReadMemoryBytes function
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {healthAddress}");
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1; // Return an error indicator, such as -1
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
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 8);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadThePainStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 8);
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {staminaAddress}");
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
                LoggingManager.Instance.Log($"Failed to read memory at {AobManager.Instance.FoundTheFearAddress}");
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
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 16); // Health offset
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFearHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 16); // Adjust for actual health offset

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {healthAddress}");
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
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 8);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFearStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 8);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {staminaAddress}");
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
                return -1; // Indicate failure
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            // Read health value using the new ReadMemoryBytes function
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, AobManager.Instance.FoundTheEnds063aAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {AobManager.Instance.FoundTheEnds063aAddress}");
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
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds063aAddress, 1360);
            // Interesting accident find but if we go back to 1384 instead of 1360 we can just make
            // The End invincible maybe something to look into once everyone else is implemented
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheEnds063aHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds063aAddress, 1360);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {healthAddress}");
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
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds063aAddress, 1364);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheEnds063aStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds063aAddress, 1364); // Adjust for actual stamina offset

            // Read stamina value using the new ReadMemoryBytes function
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {staminaAddress}");
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return -1; // Return an error indicator, such as -1
            }

            short staminaValue = BitConverter.ToInt16(buffer, 0);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }


        public static bool IsTheEnds063aStunned()
        {
            return ReadTheEnds063aStamina() == 0;
        }

        // This find will apply to s064a as well as s065a so the case in BossForm will use this but then use the offset differences that s064a has to get to The End's health and stamina
        public static short FindTheEnds065aAOB()
        {
            if (!AobManager.Instance.FindAndStoreTheEnds065aAOB())
            {
                LoggingManager.Instance.Log("The Ends065a AOB address not found.");
                return -1; // Indicate failure
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            // Read health value using the new ReadMemoryBytes function
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, AobManager.Instance.FoundTheEnds065aAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {AobManager.Instance.FoundTheEnds065aAddress}");
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
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds065aAddress, 608);
            // Interesting accident find but if we go back to 1384 instead of 1360 we can just make
            // The End invincible maybe something to look into once everyone else is implemented
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }


        public static short ReadTheEnds065aHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds065aAddress, 608);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {healthAddress}");
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
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds065aAddress, 612);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheEnds065aStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheEnds065aAddress, 612);

            // Read stamina value using the new ReadMemoryBytes function
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {staminaAddress}");
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
                LoggingManager.Instance.Log($"Failed to read memory at {AobManager.Instance.FoundTheFuryAddress}");
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
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFuryAddress, 160);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFuryHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFuryAddress, 160);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {healthAddress}");
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
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFuryAddress, 152);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFuryStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFuryAddress, 152);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {staminaAddress}");
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
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 16);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadVolginHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 16);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {healthAddress}");
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
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 8);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadVolginStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheFearAddress, 8);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {staminaAddress}");
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
                LoggingManager.Instance.Log($"Failed to read memory at {AobManager.Instance.FoundShagohodAddress}");
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
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundShagohodAddress, 105);
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadShagohodHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundShagohodAddress, 105);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {healthAddress}");
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

        // Shagohod doesn't have stamina

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
                LoggingManager.Instance.Log($"Failed to read memory at {AobManager.Instance.FoundVolginOnShagohodAddress}");
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
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundVolginOnShagohodAddress, 936); // Health offset
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadVolginOnShagohodHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundVolginOnShagohodAddress, 936);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {healthAddress}");
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
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundVolginOnShagohodAddress, 928);
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadVolginOnShagohodStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundVolginOnShagohodAddress, 928);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {staminaAddress}");
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
                return -1; // Indicate failure
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, AobManager.Instance.FoundTheBossAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {AobManager.Instance.FoundTheBossAddress}");
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
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheBossAddress, 1444); // Health offset
            MemoryManager.WriteMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheBossHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(AobManager.Instance.FoundTheBossAddress, 1444);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, healthAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {healthAddress}");
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
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheBossAddress, 1440); // Stamina offset
            MemoryManager.WriteMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheBossStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(AobManager.Instance.FoundTheBossAddress, 1440);

            byte[] buffer = MemoryManager.ReadMemoryBytes(processHandle, staminaAddress, sizeof(short));
            if (buffer == null || buffer.Length != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read memory at {staminaAddress}");
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



/* Look at AobManager.cs bottom of the file to see why all this is commented out for now:
  
 public static void FindOcelotAOB()
{
    if (AobManager.Instance.FoundOcelotAddress == IntPtr.Zero)
    {
        AobManager.Instance.FindAndStoreOcelotAOB();
    }
}

public static void WriteOcelotHealth(short value)
{

    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return;
    }

    // Directly use the calculated OcelotHealthAddress for writing the health value.
    MemoryManager.WriteShortToMemory(processHandle, AobManager.Instance.OcelotHealthAddress, value);
    MemoryManager.NativeMethods.CloseHandle(processHandle);
}

public static short ReadOcelotHealth()
{

    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return -1; // Indicate error
    }

    // Use the calculated health address to read Ocelot's health.
    short healthValue = MemoryManager.ReadShortFromMemory(processHandle, AobManager.Instance.OcelotHealthAddress);
    NativeMethods.CloseHandle(processHandle);
    return healthValue;
}

// Unneeded once refactor is complete
public static bool IsOcelotDead()
{
    return ReadOcelotHealth() == 0;
}
      
public static void WriteOcelotStamina(short value)
{

    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return;
    }

    // Directly use the calculated OcelotHealthAddress for writing the health value.
    MemoryManager.WriteShortToMemory(processHandle, AobManager.Instance.OcelotStaminaAddress, value);
    MemoryManager.NativeMethods.CloseHandle(processHandle);
}



public static short ReadOcelotStamina()
{

    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return -1; // Indicate error
    }

    // Use the calculated health address to read Ocelot's health.
    short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, AobManager.Instance.OcelotStaminaAddress);
    NativeMethods.CloseHandle(processHandle);
    return staminaValue;
}


public static bool IsOcelotStunned()
{
    return ReadOcelotStamina() == 0;
}
/* I might do this for Ocelot down the line but I think I found the max health and stamina 
   values for him but too much of a rabbit hole to invest time into right now so I'll put
   some pseudo code here for now on how I would do it

 public static short ReadOcelotMaxHealth()
 // after calculating the offset for active health if you go forward by D464/54372 you will find the max health value for Ocelot and the same would be true for stamina
{
    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    // We would use the found Ocelot health address in ReadOcelotHealth and then add the offset to get the max health value
    ReadOcelotHealth();
    IntPtr healthAddress = IntPtr.Add(MemoryManager.Instance.FoundOcelotAddress, 54372); // Adjust for actual health offset
    short healthValue = MemoryManager.ReadShortFromMemory(processHandle, healthAddress);
    MemoryManager.NativeMethods.CloseHandle(processHandle);
    return healthValue;
}


#endregion

#region The Pain


public static void FindThePainAOB()
{
    if (AobManager.Instance.FoundThePainAddress == IntPtr.Zero)
    {
        AobManager.Instance.FindAndStoreThePainAOB();
    }
}

public static void WriteThePainHealth(short value)
{ 
    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return;
    }

    MemoryManager.WriteShortToMemory(processHandle, AobManager.Instance.ThePainHealthAddress, value);
    MemoryManager.NativeMethods.CloseHandle(processHandle);
}

public static short ReadThePainHealth()
{

    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return -1; // Indicate error
    }

    // Use the calculated health address to read Ocelot's health.
    short healthValue = MemoryManager.ReadShortFromMemory(processHandle, AobManager.Instance.ThePainHealthAddress);
    NativeMethods.CloseHandle(processHandle);
    return healthValue;
}

public static bool IsThePainDead()
{
    return ReadThePainHealth() == 0;
}

public static void WriteThePainStamina(short value)
{
    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return;
    }

    MemoryManager.WriteShortToMemory(processHandle, AobManager.Instance.ThePainStaminaAddress, value);
    MemoryManager.NativeMethods.CloseHandle(processHandle);


}

public static short ReadThePainStamina()
{
    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return -1; // Indicate error
    }

    // Use the calculated health address to read Ocelot's health.
    short healthValue = MemoryManager.ReadShortFromMemory(processHandle, AobManager.Instance.ThePainStaminaAddress);
    NativeMethods.CloseHandle(processHandle);
    return healthValue;

}

public static bool IsThePainStunned()
{
    return ReadThePainStamina() == 0;
}

#endregion

#region The Fear
// Same logic as Pain and Ocelot

public static void FindTheFearAOB()
{
    if (AobManager.Instance.FoundTheFearAddress == IntPtr.Zero)
    {
        AobManager.Instance.FindAndStoreTheFearAOB();
    }
}

public static void WriteTheFearHealth(short value)
{
    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return;
    }

    MemoryManager.WriteShortToMemory(processHandle, AobManager.Instance.TheFearHealthAddress, value);
    MemoryManager.NativeMethods.CloseHandle(processHandle);
}

public static short ReadTheFearHealth()
{
    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return -1; // Indicate error
    }

    // Use the calculated health address to read Ocelot's health.
    short healthValue = MemoryManager.ReadShortFromMemory(processHandle, AobManager.Instance.TheFearHealthAddress);
    NativeMethods.CloseHandle(processHandle);
    return healthValue;
}


public static bool IsTheFearDead()
{
    return ReadTheFearHealth() == 0;
}

public static void WriteTheFearStamina(short value)
{
    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return;
    }

    MemoryManager.WriteShortToMemory(processHandle, AobManager.Instance.TheFearStaminaAddress, value);
    MemoryManager.NativeMethods.CloseHandle(processHandle);
}

public static short ReadTheFearStamina()
{
    var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
    if (processHandle == IntPtr.Zero)
    {
        LoggingManager.Instance.Log("Failed to open game process.");
        return -1; // Indicate error
    }

    // Use the calculated health address to read Ocelot's health.
    short healthValue = MemoryManager.ReadShortFromMemory(processHandle, AobManager.Instance.TheFearStaminaAddress);
    NativeMethods.CloseHandle(processHandle);
    return healthValue;
}

public static bool IsTheFearStunned()
{
    return ReadTheFearStamina() == 0;
}

#endregion 
  
 
 */