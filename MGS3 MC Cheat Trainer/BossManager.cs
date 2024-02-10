namespace MGS3_MC_Cheat_Trainer

{
    public class BossManager
    {
        // We'll use this to find The Fear's AOB once the form loads, it's pretty laggy atm but don't have a better fix
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
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 18); // Health offset
            MemoryManager.WriteShortToMemory(processHandle, healthAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static void WriteTheFearStamina(short value)
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 10); // Stamina offset
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static short ReadTheFearHealth()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 18); // Adjust for actual health offset
            short healthValue = MemoryManager.ReadShortFromMemory(processHandle, healthAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return healthValue;
        }

        public static short ReadTheFearStamina()
        {
            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 10); // Adjust for actual stamina offset
            short staminaValue = MemoryManager.ReadShortFromMemory(processHandle, staminaAddress);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return staminaValue;
        }
    }
}