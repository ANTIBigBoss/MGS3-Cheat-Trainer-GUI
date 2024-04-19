namespace MGS3_MC_Cheat_Trainer
{
    public class DamageManager
    {
        private static DamageManager instance;
        private static readonly object lockObj = new object();

        // Private constructor to prevent external instantiation
        private DamageManager()
        {
        }

        // Public property to access the instance
        public static DamageManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new DamageManager();
                    }

                    return instance;
                }
            }
        }

        public string ReadMostWeaponsDamage()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr aobResult = MemoryManager.Instance.FindAob("MostWeaponsDamage");
            IntPtr targetAddress = IntPtr.Subtract(aobResult, 2); // Two bytes before the AOB
            return MemoryManager.ReadMemoryValueAsString<short>(processHandle, targetAddress, 2);
        }

        public string ReadM63Damage()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr aobResult = MemoryManager.Instance.FindAob("M63Damage");
            IntPtr targetAddress = IntPtr.Subtract(aobResult, 2); // Two bytes before the AOB
            return MemoryManager.ReadMemoryValueAsString<short>(processHandle, targetAddress, 2);
        }

        public string ReadExplosionDamage()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr aobResult = MemoryManager.Instance.FindAob("ExplosionDamage");
            IntPtr targetAddress = IntPtr.Add(aobResult, 2); // Two bytes after the AOB
            return MemoryManager.ReadMemoryValueAsString<int>(processHandle, targetAddress, 4);
        }

        public string ReadThroatSlitDamage()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr aobResult = MemoryManager.Instance.FindAob("ThroatSlitDamage");
            IntPtr targetAddress = IntPtr.Subtract(aobResult, 22); // 22 bytes before the AOB
            return MemoryManager.ReadMemoryValueAsString<int>(processHandle, targetAddress, 4);
        }

        public string ReadNeckSnapDamage()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            IntPtr aobResult = MemoryManager.Instance.FindAob("NeckSnapDamage");
            IntPtr targetAddress = IntPtr.Subtract(aobResult, 22); // 22 bytes before the AOB
            return MemoryManager.ReadMemoryValueAsString<int>(processHandle, targetAddress, 4);
        }



    }
}