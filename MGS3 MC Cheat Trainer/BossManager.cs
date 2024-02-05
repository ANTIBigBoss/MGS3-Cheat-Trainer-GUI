namespace MGS3_MC_Cheat_Trainer

{
    public class BossManager
    {
        // Attempt to find and use TheFearAOB address immediately within each method
        public static short ReadTheFearHealth()
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
            if (!MemoryManager.Instance.FindAndStoreTheFearAOB())
            {
                MessageBox.Show("TheFear AOB address not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr healthAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 18); // Health offset
            MemoryManager.WriteShortToMemory(processHandle, healthAddress, value);
            MessageBox.Show("Health value updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static void WriteTheFearStamina(short value)
        {
            if (!MemoryManager.Instance.FindAndStoreTheFearAOB())
            {
                MessageBox.Show("TheFear AOB address not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr staminaAddress = IntPtr.Subtract(MemoryManager.Instance.FoundTheFearAddress, 10); // Stamina offset
            MemoryManager.WriteShortToMemory(processHandle, staminaAddress, value);
            MessageBox.Show("Stamina value updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }
    }
}

    /*
    public static List<short> ReadShortSequenceBeforeTheFearAOB(int offsetBack, int count)
        {
            // Attempt to find the AOB first
            if (!MemoryManager.Instance.FindAndStoreTheFearAOB())
            {
                MessageBox.Show("TheFear AOB address not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<short>(); // Return an empty list or handle this case as appropriate
            }

            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            List<short> values = new List<short>();

            IntPtr baseAddress = MemoryManager.Instance.FoundTheFearAddress;
            for (int i = 0; i < count; i++)
            {
                // Adjust calculation for subtracting offset
                IntPtr addressToRead = IntPtr.Add(baseAddress, -(offsetBack + (i * sizeof(short))));
                short value = MemoryManager.ReadShortFromMemory(processHandle, addressToRead);
                values.Add(value);
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return values;
        } 
    */