using System.Diagnostics;
using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    public class DamageManager
    {
        private static DamageManager instance;
        private static readonly object lockObj = new object();

        private DamageManager()
        {
        }

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

        private void WriteValues<T>(IntPtr processHandle, string aobKey, int offset, bool forward, T value)
        {
            IntPtr aobResult = MemoryManager.Instance.FindAob(aobKey);
            if (aobResult == IntPtr.Zero)
            {
                LoggingManager.Instance.Log($"Error: AOB pattern not found for {aobKey}.");
                return;
            }

            IntPtr targetAddress = forward ? IntPtr.Add(aobResult, offset) : IntPtr.Subtract(aobResult, offset);
            bool writeResult = MemoryManager.WriteMemory(processHandle, targetAddress, value);
            LoggingManager.Instance.Log(writeResult ? $"{aobKey} written successfully." : $"Failed to write {aobKey}.");
        }

        public void WriteAllLethalInvincibleValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            // Writing values using byte arrays for byte values and ushort directly for numeric values
            WriteValues(processHandle, "WpNadeDamage", 4, false, new byte[] { 0xE8, 0x03, 0x00, 0x00 });  // 1000 in little-endian hex
            WriteValues(processHandle, "ShotgunDamage", 6, false, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });  // NOP
            WriteValues(processHandle, "M63Damage", 2, false, 0);  // Using direct decimal for ushort
            WriteValues(processHandle, "KnifeForkDamage", 6, false, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });  // NOP
            WriteValues(processHandle, "MostWeaponsDamage", 2, false, 0);  // Using direct decimal for ushort
            WriteValues(processHandle, "ExplosionDamage", 24, true, 0);  // Using direct decimal for ushort
            WriteValues(processHandle, "ThroatSlitDamage", 26, false, new byte[] { 0xE8, 0x03, 0x00, 0x00 });  // 1000 in little-endian hex

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public void WriteAllLethalVeryStrongValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            WriteValues(processHandle, "WpNadeDamage", 4, false, new byte[] { 0xFA, 0x00, 0x00, 0x00 });
            WriteValues(processHandle, "ShotgunDamage", 6, false, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            WriteValues(processHandle, "M63Damage", 2, false, 100);
            WriteValues(processHandle, "KnifeForkDamage", 6, false, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            WriteValues(processHandle, "MostWeaponsDamage", 2, false, 100);
            WriteValues(processHandle, "ExplosionDamage", 24, true, 100);
            WriteValues(processHandle, "ThroatSlitDamage", 26, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });
            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public void WriteAllLethalDefaultValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            // Writing values using byte arrays for byte values and ushort directly for numeric values
            WriteValues(processHandle, "WpNadeDamage", 4, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });  // Zero
            WriteValues(processHandle, "ShotgunDamage", 6, false, new byte[] { 0x89, 0x8E, 0x38, 0x01, 0x00, 0x00 });  // Specific pattern
            WriteValues(processHandle, "M63Damage", 2, false, 1000);  // Using direct decimal for ushort
            WriteValues(processHandle, "KnifeForkDamage", 6, false, new byte[] { 0x29, 0x86, 0x38, 0x01, 0x00, 0x00 });  // Specific pattern
            WriteValues(processHandle, "MostWeaponsDamage", 2, false, 1000);  // Using direct decimal for ushort
            WriteValues(processHandle, "ExplosionDamage", 24, true, 1000);  // Using direct decimal for ushort
            WriteValues(processHandle, "ThroatSlitDamage", 26, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });  // Zero

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public void WriteAllLethalOneshotValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            // Writing values using byte arrays for byte values and ushort directly for numeric values
            WriteValues(processHandle, "WpNadeDamage", 4, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });  // Zero
            WriteValues(processHandle, "ShotgunDamage", 6, false, new byte[] { 0x89, 0x8E, 0x38, 0x01, 0x00, 0x00 });  // Specific pattern
            WriteValues(processHandle, "M63Damage", 2, false, 30000);  // Using direct decimal for ushort
            WriteValues(processHandle, "KnifeForkDamage", 6, false, new byte[] { 0x29, 0x86, 0x38, 0x01, 0x00, 0x00 });  // Specific pattern
            WriteValues(processHandle, "MostWeaponsDamage", 2, false, 30000);  // Using direct decimal for ushort
            WriteValues(processHandle, "ExplosionDamage", 24, true, 30000);  // Using direct decimal for ushort
            WriteValues(processHandle, "ThroatSlitDamage", 26, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });  // Zero

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }


        public void ReadAllLethalValues()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling ReadAllLethalValues function.");
                return;
            }

            // Ensure offsets used here match those used in WriteValues method
            LoggingManager.Instance.Log("Reading all lethal damage values...");

            // Consistently use the correct offsets as used in the writing functions
            LoggingManager.Instance.Log("WpNadeDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4)).Replace("-", " "));
            LoggingManager.Instance.Log("ShotgunDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6)).Replace("-", " "));
            LoggingManager.Instance.Log("M63Damage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2)).Replace("-", " "));
            LoggingManager.Instance.Log("KnifeForkDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6)).Replace("-", " "));
            LoggingManager.Instance.Log("MostWeaponsDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2)).Replace("-", " "));
            LoggingManager.Instance.Log("ExplosionDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2)).Replace("-", " "));
            LoggingManager.Instance.Log("ThroatSlitDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ThroatSlitDamage"), 26), 4)).Replace("-", " "));

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public bool AreLethalValuesInvincible()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling AreLethalValuesInvincible function.");
                return false;
            }

            bool areInvincible = true;
            areInvincible &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4)
                .SequenceEqual(new byte[] { 0xE8, 0x03, 0x00, 0x00 }); // 1000 in little-endian byte order
            areInvincible &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6)
                .SequenceEqual(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            areInvincible &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2)
                .SequenceEqual(new byte[] { 0x00, 0x00 });
            areInvincible &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6)
                .SequenceEqual(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            areInvincible &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2)
                .SequenceEqual(new byte[] { 0x00, 0x00 });
            areInvincible &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2)
                .SequenceEqual(new byte[] { 0x00, 0x00 });
            areInvincible &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ThroatSlitDamage"), 26), 4)
                .SequenceEqual(new byte[] { 0xE8, 0x03, 0x00, 0x00 }); // 1000 in little-endian byte order

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areInvincible;
        }

        public bool AreLethalValuesVeryStrong()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling AreLethalValuesVeryStrong function.");
                return false;
            }

            bool areVeryStrong = true;
            areVeryStrong &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4)
                .SequenceEqual(new byte[] { 0xFA, 0x00, 0x00, 0x00 });
            areVeryStrong &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6)
                .SequenceEqual(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            areVeryStrong &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2)
                .SequenceEqual(new byte[] { 0x30, 0x75 });
            areVeryStrong &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6)
                .SequenceEqual(new byte[] { 0x30, 0x75, 0x38, 0x01, 0x00, 0x00 });
            areVeryStrong &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2)
                .SequenceEqual(new byte[] { 0x30, 0x75 });
            areVeryStrong &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2)
                .SequenceEqual(new byte[] { 0x30, 0x75 });
            areVeryStrong &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ThroatSlitDamage"), 26), 4)
                .SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areVeryStrong;
            }
        
        public bool AreLethalValuesDefault()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling AreLethalValuesDefault function.");
                return false;
            }

            bool areDefault = true;
            // Using BitConverter to convert byte array to ushort and compare with decimal value directly
            areDefault &= BitConverter.ToUInt16(MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2), 0) == 1000;
            areDefault &= BitConverter.ToUInt16(MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2), 0) == 1000;
            areDefault &= BitConverter.ToUInt16(MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2), 0) == 1000;

            // Other checks can remain in byte array format since they directly relate to byte-specific operations
            areDefault &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4)
                .SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            areDefault &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6)
                .SequenceEqual(new byte[] { 0x89, 0x8E, 0x38, 0x01, 0x00, 0x00 });
            areDefault &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6)
                .SequenceEqual(new byte[] { 0x29, 0x86, 0x38, 0x01, 0x00, 0x00 });
            areDefault &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ThroatSlitDamage"), 26), 4)
                .SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areDefault;
        }

        public bool AreLethalValuesOneshot()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling AreLethalValuesOneshot function.");
                return false;
            }

            bool areOneshot = true;
            areOneshot &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4)
                .SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            areOneshot &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6)
                .SequenceEqual(new byte[] { 0x89, 0x8E, 0x38, 0x01, 0x00, 0x00 });
            areOneshot &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2)
                .SequenceEqual(new byte[] { 0x30, 0x75 });
            areOneshot &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6)
                .SequenceEqual(new byte[] { 0x30, 0x75, 0x38, 0x01, 0x00, 0x00 });
            areOneshot &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2)
                .SequenceEqual(new byte[] { 0x30, 0x75 });
            areOneshot &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2)
                .SequenceEqual(new byte[] { 0x30, 0x75 });
            areOneshot &= MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob
                ("ThroatSlitDamage"), 26), 4).SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areOneshot;
        }

    }
}