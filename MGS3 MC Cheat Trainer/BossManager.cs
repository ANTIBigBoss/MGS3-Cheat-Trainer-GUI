namespace MGS3_MC_Cheat_Trainer
{
    // This whole form needs some classes setup but I wanted to verify each worked with how
    // tempermental the game is I will probably do this sometime after I release V 2.0 publicly
    // and the auto update feature is working but for now this works and is a good start
    public class BossManager
    {
        #region Ocelot
        public static short FindOcelotAOB()
        {
            if (!MemoryManager.Instance.FindAndStoreOcelotAOB())
            {
                MessageBox.Show("Ocelot AOB address not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Indicate failure
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, MemoryManager.Instance.FoundOcelotAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteOcelotHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundOcelotAddress, 916); // Health offset
            MemoryManager.WriteShortToMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadOcelotHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundOcelotAddress, 916); // Adjust for actual health offset
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, healthAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static bool IsOcelotDead()
        {
            return ReadOcelotHealth() == 0;
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

        */

        public static void WriteOcelotStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundOcelotAddress, 908); // Stamina offset
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }       

        public static short ReadOcelotStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundOcelotAddress, 908); // Adjust for actual stamina offset
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsOcelotStunned()
        {
            return ReadOcelotStamina() == 0;
        }

        #endregion

        #region The Pain
        public static void WriteThePainHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 16); // Health offset
            MemoryManager.WriteShortToMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadThePainHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 16); // Adjust for actual health offset
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, healthAddress);
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
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Stamina offset
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadThePainStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Adjust for actual stamina offset
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsThePainStunned()
        {
            return ReadThePainStamina() == 0;
        }

        #endregion

        #region The Fear
        public static short FindTheFearAOB()
        {

            if (!MemoryManager.Instance.FindAndStoreTheFearAOB())
            {
                MessageBox.Show("TheFear AOB address not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Indicate failure
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, MemoryManager.Instance.FoundTheFearAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteTheFearHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 16); // Health offset
            MemoryManager.WriteShortToMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFearHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 16); // Adjust for actual health offset
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, healthAddress);
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
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Stamina offset
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }
        
        public static short ReadTheFearStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Adjust for actual stamina offset
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsTheFearStunned()
        {
            return ReadTheFearStamina() == 0;
        }

        #endregion

        #region The End

        public static short FindTheEnds063aAOB()
        {

            if (!MemoryManager.Instance.FindAndStoreTheEnds063aAOB())
            {
                MessageBox.Show("The Ends063a AOB address not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Indicate failure
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, MemoryManager.Instance.FoundTheEnds063aAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteTheEnds063aHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheEnds063aAddress, 1360);
            // Interesting accident find but if we go back to 1384 instead of 1360 we can just make
            // The End invincible maybe something to look into once everyone else is implemented
            MemoryManager.WriteShortToMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheEnds063aHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheEnds063aAddress, 1360);
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, healthAddress);
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
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheEnds063aAddress, 1364);
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheEnds063aStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheEnds063aAddress, 1364);
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
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

            if (!MemoryManager.Instance.FindAndStoreTheEnds065aAOB())
            {
                MessageBox.Show("The Ends065a AOB address not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Indicate failure
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, MemoryManager.Instance.FoundTheEnds063aAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }


        public static void WriteTheEnds065aHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheEnds065aAddress, 608);
            // Interesting accident find but if we go back to 1384 instead of 1360 we can just make
            // The End invincible maybe something to look into once everyone else is implemented
            MemoryManager.WriteShortToMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }


        public static short ReadTheEnds065aHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheEnds065aAddress, 608);
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, healthAddress);
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
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheEnds065aAddress, 612);
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheEnds064aStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheEnds065aAddress, 612);
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }


        public static short ReadTheEnds065aStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheEnds065aAddress, 612);
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
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

            if (!MemoryManager.Instance.FindAndStoreTheFuryAOB())
            {
                MessageBox.Show("The Fury AOB address not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Indicate failure
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, MemoryManager.Instance.FoundTheFuryAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static void WriteTheFuryHealth(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFuryAddress, 160); // Health offset
            MemoryManager.WriteShortToMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFuryHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFuryAddress, 160); // Adjust for actual health offset
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, healthAddress);
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
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFuryAddress, 152); // Stamina offset
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFuryStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFuryAddress, 152); // Adjust for actual stamina offset
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
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
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 16); // Health offset
            MemoryManager.WriteShortToMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadVolginHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 16); // Adjust for actual health offset
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, healthAddress);
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
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Stamina offset
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadVolginStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Adjust for actual stamina offset
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        public static bool IsVolginStunned()
        {
            return ReadVolginStamina() == 0;
        }

        #endregion
    }
}