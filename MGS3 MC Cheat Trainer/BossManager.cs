namespace MGS3_MC_Cheat_Trainer
{
    public class BossManager
    {
        /* Might be able to just use this inside of the BossForm_Load function

        public void DetermineMapString()
        {
            try
            {
                string result = MemoryManager.Instance.FindLocationStringDirectlyInRange();
                // Use message box for debugging only
                //MessageBox.Show(result, "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == "s051b")
                {
                    FindTheFearAOB();
                }
                else if (result == "s032b")
                {
                    FindTheFearAOB(); // Pain and Fear share the same AOB
                }
                else if (result == "s122a")
                {
                    FindTheFearAOB(); // Volgin's first fight shares the same AOB as well
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        */

        

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

        public static void WriteTheFearStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Stamina offset
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
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

        public static short ReadTheFearStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Adjust for actual stamina offset
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
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

        public static void WriteThePainStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Stamina offset
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
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

        public static short ReadThePainStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Adjust for actual stamina offset
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
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

        public static void WriteVolginStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Stamina offset
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
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

        public static short ReadVolginStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 8); // Adjust for actual stamina offset
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }

        #endregion
    }
}