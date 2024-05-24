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

        #region Lethal Damage

        public void ReadAllLethalValues()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling ReadAllLethalValues function.");
                return;
            }

            LoggingManager.Instance.Log("Reading all lethal damage values...");

            LoggingManager.Instance.Log("WpNadeDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle,
                    IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4)).Replace("-", " "));
            LoggingManager.Instance.Log("ShotgunDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle,
                    IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6)).Replace("-", " "));
            LoggingManager.Instance.Log("M63Damage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle,
                    IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2)).Replace("-", " "));
            LoggingManager.Instance.Log("KnifeForkDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle,
                    IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6)).Replace("-", " "));
            LoggingManager.Instance.Log("MostWeaponsDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle,
                    IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2)).Replace("-", " "));
            LoggingManager.Instance.Log("ExplosionDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle,
                    IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2)).Replace("-", " "));
            LoggingManager.Instance.Log("ThroatSlitDamage: " + BitConverter.ToString(
                MemoryManager.ReadMemoryBytes(processHandle,
                    IntPtr.Subtract(MemoryManager.Instance.FindAob("ThroatSlitDamage"), 26), 4)).Replace("-", " "));

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        #region Lethal Invincible Read/Write

        public void WriteAllLethalInvincibleValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            WriteValues(processHandle, "WpNadeDamage", 4, false, new byte[] { 0xE8, 0x03, 0x00, 0x00 });
            WriteValues(processHandle, "ShotgunDamage", 6, false, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            WriteValues(processHandle, "M63Damage", 2, false, 0);
            WriteValues(processHandle, "KnifeForkDamage", 6, false, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            WriteValues(processHandle, "MostWeaponsDamage", 2, false, 0);
            WriteValues(processHandle, "ExplosionDamage", 24, true, 0);
            WriteValues(processHandle, "ThroatSlitDamage", 26, false, new byte[] { 0xE8, 0x03, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public bool AreLethalValuesInvincible()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling AreLethalValuesInvincible function.");
                return false;
            }

            bool areInvincible = true;

            byte[] wpNadeDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4);
            areInvincible &= wpNadeDamage.SequenceEqual(new byte[] { 0xE8, 0x03, 0x00, 0x00 });

            byte[] shotgunDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6);
            areInvincible &= shotgunDamage.SequenceEqual(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });

            byte[] m63Damage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2);
            areInvincible &= BitConverter.ToInt16(m63Damage, 0) == 0;

            byte[] knifeForkDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6);
            areInvincible &= knifeForkDamage.SequenceEqual(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });

            byte[] mostWeaponsDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2);
            areInvincible &= BitConverter.ToInt16(mostWeaponsDamage, 0) == 0;

            byte[] explosionDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2);
            areInvincible &= BitConverter.ToInt16(explosionDamage, 0) == 0;

            byte[] throatSlitDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("ThroatSlitDamage"), 26), 4);
            areInvincible &= throatSlitDamage.SequenceEqual(new byte[] { 0xE8, 0x03, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areInvincible;
        }


        #endregion

        #region Lethal Very Strong Read/Write

        public void WriteAllLethalVeryStrongValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling WriteAllLethalDefaultValues function.");
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

        public bool AreLethalValuesVeryStrong()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling AreLethalValuesVeryStrong function.");
                return false;
            }

            bool areVeryStrong = true;

            // Read and compare WpNadeDamage
            byte[] wpNadeDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4);
            areVeryStrong &= wpNadeDamage.SequenceEqual(new byte[] { 0xFA, 0x00, 0x00, 0x00 });

            // Read and compare ShotgunDamage
            byte[] shotgunDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6);
            areVeryStrong &= shotgunDamage.SequenceEqual(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });

            // Read and compare M63Damage
            byte[] m63Damage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2);
            areVeryStrong &= BitConverter.ToInt16(m63Damage, 0) == 100;

            // Read and compare KnifeForkDamage
            byte[] knifeForkDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6);
            areVeryStrong &= knifeForkDamage.SequenceEqual(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });

            // Read and compare MostWeaponsDamage
            byte[] mostWeaponsDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2);
            areVeryStrong &= BitConverter.ToInt16(mostWeaponsDamage, 0) == 100;

            // Read and compare ExplosionDamage
            byte[] explosionDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2);
            areVeryStrong &= BitConverter.ToInt16(explosionDamage, 0) == 100;

            // Read and compare ThroatSlitDamage
            byte[] throatSlitDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("ThroatSlitDamage"), 26), 4);
            areVeryStrong &= throatSlitDamage.SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areVeryStrong;
        }

        #endregion

        #region Lethal Default Read/Write

        public void WriteAllLethalDefaultValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            WriteValues(processHandle, "WpNadeDamage", 4, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });
            WriteValues(processHandle, "ShotgunDamage", 6, false, new byte[] { 0x89, 0x8E, 0x38, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "M63Damage", 2, false, 1000);
            WriteValues(processHandle, "KnifeForkDamage", 6, false, new byte[] { 0x29, 0x86, 0x38, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "MostWeaponsDamage", 2, false, 1000);
            WriteValues(processHandle, "ExplosionDamage", 24, true, 1000);
            WriteValues(processHandle, "ThroatSlitDamage", 26, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public bool AreLethalValuesDefault()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling AreLethalValuesDefault function.");
                return false;
            }

            bool areDefault = true;

            byte[] wpNadeDamageBytes = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4);
            areDefault &= wpNadeDamageBytes.SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            byte[] shotgunDamageBytes = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6);
            areDefault &= shotgunDamageBytes.SequenceEqual(new byte[] { 0x89, 0x8E, 0x38, 0x01, 0x00, 0x00 });

            byte[] m63DamageBytes = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2);
            areDefault &= BitConverter.ToUInt16(m63DamageBytes, 0) == 1000;

            byte[] knifeForkDamageBytes = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6);
            areDefault &= knifeForkDamageBytes.SequenceEqual(new byte[] { 0x29, 0x86, 0x38, 0x01, 0x00, 0x00 });

            byte[] mostWeaponsDamageBytes = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2);
            areDefault &= BitConverter.ToUInt16(mostWeaponsDamageBytes, 0) == 1000;

            byte[] explosionDamageBytes = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2);
            areDefault &= BitConverter.ToUInt16(explosionDamageBytes, 0) == 1000;

            byte[] throatSlitDamageBytes = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("ThroatSlitDamage"), 26), 4);
            areDefault &= throatSlitDamageBytes.SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areDefault;
        }

        #endregion

        #region Lethal Very Weak Read/Write

        public void WriteAllLethalVeryWeakValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            WriteValues(processHandle, "WpNadeDamage", 4, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });
            WriteValues(processHandle, "ShotgunDamage", 6, false, new byte[] { 0x89, 0x8E, 0x38, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "M63Damage", 2, false, 2500);
            WriteValues(processHandle, "KnifeForkDamage", 6, false, new byte[] { 0x29, 0x86, 0x38, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "MostWeaponsDamage", 2, false, 2500);
            WriteValues(processHandle, "ExplosionDamage", 24, true, 2500);
            WriteValues(processHandle, "ThroatSlitDamage", 26, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public bool AreLethalValuesVeryWeak()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling AreLethalValuesVeryWeak function.");
                return false;
            }

            bool areVeryWeak = true;

            byte[] wpNadeDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4);
            areVeryWeak &= wpNadeDamage.SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            byte[] shotgunDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6);
            areVeryWeak &= shotgunDamage.SequenceEqual(new byte[] { 0x89, 0x8E, 0x38, 0x01, 0x00, 0x00 });

            byte[] m63Damage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2);
            areVeryWeak &= BitConverter.ToInt16(m63Damage, 0) == 2500;

            byte[] knifeForkDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6);
            areVeryWeak &= knifeForkDamage.SequenceEqual(new byte[] { 0x29, 0x86, 0x38, 0x01, 0x00, 0x00 });

            byte[] mostWeaponsDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2);
            areVeryWeak &= BitConverter.ToInt16(mostWeaponsDamage, 0) == 2500;

            byte[] explosionDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2);
            areVeryWeak &= BitConverter.ToInt16(explosionDamage, 0) == 2500;

            byte[] throatSlitDamage = MemoryManager.ReadMemoryBytes(processHandle,
                IntPtr.Subtract(MemoryManager.Instance.FindAob("ThroatSlitDamage"), 26), 4);
            areVeryWeak &= throatSlitDamage.SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areVeryWeak;
        }

        #endregion

        #region Lethal Oneshot Read/Write

        public void WriteAllLethalOneshotValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            WriteValues(processHandle, "WpNadeDamage", 4, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });
            WriteValues(processHandle, "ShotgunDamage", 6, false, new byte[] { 0x89, 0x8E, 0x38, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "M63Damage", 2, false, 30000);
            WriteValues(processHandle, "KnifeForkDamage", 6, false, new byte[] { 0x29, 0x86, 0x38, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "MostWeaponsDamage", 2, false, 30000);
            WriteValues(processHandle, "ExplosionDamage", 24, true, 30000);
            WriteValues(processHandle, "ThroatSlitDamage", 26, false, new byte[] { 0x00, 0x00, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public bool AreLethalValuesOneshot()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling AreLethalValuesOneshot function.");
                return false;
            }

            bool areOneshot = true;

            byte[] wpNadeDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("WpNadeDamage"), 4), 4);
            areOneshot &= wpNadeDamage.SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            byte[] shotgunDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ShotgunDamage"), 6), 6);
            areOneshot &= shotgunDamage.SequenceEqual(new byte[] { 0x89, 0x8E, 0x38, 0x01, 0x00, 0x00 });

            byte[] m63Damage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("M63Damage"), 2), 2);
            areOneshot &= BitConverter.ToInt16(m63Damage, 0) == 30000;

            byte[] knifeForkDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("KnifeForkDamage"), 6), 6);
            areOneshot &= knifeForkDamage.SequenceEqual(new byte[] { 0x29, 0x86, 0x38, 0x01, 0x00, 0x00 });

            byte[] mostWeaponsDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("MostWeaponsDamage"), 2), 2);
            areOneshot &= BitConverter.ToInt16(mostWeaponsDamage, 0) == 30000;

            byte[] explosionDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("ExplosionDamage"), 24), 2);
            areOneshot &= BitConverter.ToInt16(explosionDamage, 0) == 30000;

            byte[] throatSlitDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("ThroatSlitDamage"), 26), 4);
            areOneshot &= throatSlitDamage.SequenceEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areOneshot;
        }

        #endregion

        #endregion

        #region Stun Damage

        #region Invinible Stun Read/Write

        public void WriteAllStunInvincibleValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                    "Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            WriteValues(processHandle, "CQCSlamNormal", (int)DamageOffsets.CQCSlam1Add, true, 90000);
            WriteValues(processHandle, "CQCSlamNormal", (int)DamageOffsets.CQCSlam2Add, true, 90000);
            WriteValues(processHandle, "StunNadeDamage", (int)DamageOffsets.StunNadeSub, false,
                new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.TriplePunchSub, false, 0);
            WriteValues(processHandle, "StunRollDamage", (int)DamageOffsets.StunRollSub, false,
                new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.StunPunchInstructionsAdd, true,
                new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.SinglePunchSub, false, 0);
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.PunchKnockOverAdd, true, 1);

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public bool AreStunValuesInvincible()
        {
            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling AreStunValuesInvincible function.");
                return false;
            }

            bool areInvincible = true;

            byte[] cqCSlamNormal1 = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("CQCSlamNormal"), (int)DamageOffsets.CQCSlam1Add), 4);
            areInvincible &= BitConverter.ToInt32(cqCSlamNormal1, 0) == 90000;

            byte[] cqCSlamNormal2 = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("CQCSlamNormal"), (int)DamageOffsets.CQCSlam2Add), 4);
            areInvincible &= BitConverter.ToInt32(cqCSlamNormal2, 0) == 90000;

            byte[] stunNadeDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunNadeDamage"), (int)DamageOffsets.StunNadeSub), 6);
            areInvincible &= stunNadeDamage.SequenceEqual(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });

            byte[] triplePunchDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.TriplePunchSub), 1);
            areInvincible &= triplePunchDamage[0] == 0;

            byte[] stunRollDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunRollDamage"), (int)DamageOffsets.StunRollSub), 6);
            areInvincible &= stunRollDamage.SequenceEqual(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });

            byte[] stunPunchInstructions = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.StunPunchInstructionsAdd), 6);
            areInvincible &= stunPunchInstructions.SequenceEqual(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });

            byte[] singlePunchDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.SinglePunchSub), 1);
            areInvincible &= singlePunchDamage[0] == 0;

            byte[] punchKnockOverThreshold = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.PunchKnockOverAdd), 1);
            areInvincible &= punchKnockOverThreshold[0] == 1;

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areInvincible;
        }

        // Very Strong Stun Read/Write

        public void WriteAllStunVeryStrongValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                                       "Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            WriteValues(processHandle, "CQCSlamNormal", (int)DamageOffsets.CQCSlam1Add, true, -1600);
            WriteValues(processHandle, "CQCSlamNormal", (int)DamageOffsets.CQCSlam2Add, true, -1600);
            WriteValues(processHandle, "StunNadeDamage", (int)DamageOffsets.StunNadeSub, false,
                               new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.TriplePunchSub, false, 232);
            WriteValues(processHandle, "StunRollDamage", (int)DamageOffsets.StunRollSub, false,
                               new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.StunPunchInstructionsAdd, true,
                               new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.SinglePunchSub, false, 0);
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.PunchKnockOverAdd, true, 1);

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public void WriteAllStunDefaultValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                                       "Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            WriteValues(processHandle, "CQCSlamNormal", (int)DamageOffsets.CQCSlam1Add, true, -90000);
            WriteValues(processHandle, "CQCSlamNormal", (int)DamageOffsets.CQCSlam2Add, true, -36000);
            WriteValues(processHandle, "StunNadeDamage", (int)DamageOffsets.StunNadeSub, false,
                               new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.TriplePunchSub, false, 232);
            WriteValues(processHandle, "StunRollDamage", (int)DamageOffsets.StunRollSub, false,
                               new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.StunPunchInstructionsAdd, true,
                               new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.SinglePunchSub, false, 1);
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.PunchKnockOverAdd, true, 1);

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public bool AreStunValuesDefault()
        {

            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling AreStunValuesNormal function.");
                return false;
            }

            bool areNormal = true;

            byte[] cqCSlamNormal1 = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("CQCSlamNormal"), (int)DamageOffsets.CQCSlam1Add), 4);
            areNormal &= BitConverter.ToInt32(cqCSlamNormal1, 0) == -90000;

            byte[] cqCSlamNormal2 = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("CQCSlamNormal"), (int)DamageOffsets.CQCSlam2Add), 4);
            areNormal &= BitConverter.ToInt32(cqCSlamNormal2, 0) == -36000;

            byte[] stunNadeDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunNadeDamage"), (int)DamageOffsets.StunNadeSub), 6);
            areNormal &= stunNadeDamage.SequenceEqual(new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });

            byte[] triplePunchDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.TriplePunchSub), 1);
            areNormal &= triplePunchDamage[0] == 232;

            byte[] stunRollDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunRollDamage"), (int)DamageOffsets.StunRollSub), 6);
            areNormal &= stunRollDamage.SequenceEqual(new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });

            byte[] stunPunchInstructions = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.StunPunchInstructionsAdd), 6);
            areNormal &= stunPunchInstructions.SequenceEqual(new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });

            byte[] singlePunchDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.SinglePunchSub), 1);
            areNormal &= singlePunchDamage[0] == 1;

            byte[] punchKnockOverThreshold = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.PunchKnockOverAdd), 1);
            areNormal &= punchKnockOverThreshold[0] == 1;

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areNormal;
            }

        public void WriteAllStunVeryWeakValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log(
                                       "Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            WriteValues(processHandle, "CQCSlamNormal", (int)DamageOffsets.CQCSlam1Add, true, -99999);
            WriteValues(processHandle, "CQCSlamNormal", (int)DamageOffsets.CQCSlam2Add, true, -99999);
            WriteValues(processHandle, "StunNadeDamage", (int)DamageOffsets.StunNadeSub, false, new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.TriplePunchSub, false, 232);
            WriteValues(processHandle, "StunRollDamage", (int)DamageOffsets.StunRollSub, false, new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.StunPunchInstructionsAdd, true, new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.SinglePunchSub, false, 200);
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.PunchKnockOverAdd, true, 1);

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public bool AreStunValuesVeryWeak()
        {

            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling AreStunValuesNormal function.");
                return false;
            }

            bool areNormal = true;

            byte[] cqCSlamNormal1 = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("CQCSlamNormal"), (int)DamageOffsets.CQCSlam1Add), 4);
            areNormal &= BitConverter.ToInt32(cqCSlamNormal1, 0) == -90000;

            byte[] cqCSlamNormal2 = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("CQCSlamNormal"), (int)DamageOffsets.CQCSlam2Add), 4);
            areNormal &= BitConverter.ToInt32(cqCSlamNormal2, 0) == -36000;

            byte[] stunNadeDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunNadeDamage"), (int)DamageOffsets.StunNadeSub), 6);
            areNormal &= stunNadeDamage.SequenceEqual(new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });

            byte[] triplePunchDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.TriplePunchSub), 1);
            areNormal &= triplePunchDamage[0] == 232;

            byte[] stunRollDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunRollDamage"), (int)DamageOffsets.StunRollSub), 6);
            areNormal &= stunRollDamage.SequenceEqual(new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });

            byte[] stunPunchInstructions = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.StunPunchInstructionsAdd), 6);
            areNormal &= stunPunchInstructions.SequenceEqual(new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });

            byte[] singlePunchDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.SinglePunchSub), 1);
            areNormal &= singlePunchDamage[0] == 1;

            byte[] punchKnockOverThreshold = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.PunchKnockOverAdd), 1);
            areNormal &= punchKnockOverThreshold[0] == 1;

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areNormal;
        }

        public void WriteAllStunOneShotValues()
        {
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling WriteAllLethalDefaultValues function.");
                return;
            }

            WriteValues(processHandle, "CQCSlamNormal", (int)DamageOffsets.CQCSlam1Add, true, -99999);
            WriteValues(processHandle, "CQCSlamNormal", (int)DamageOffsets.CQCSlam2Add, true, -99999);
            WriteValues(processHandle, "StunNadeDamage", (int)DamageOffsets.StunNadeSub, false, new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.TriplePunchSub, false, 232);
            WriteValues(processHandle, "StunRollDamage", (int)DamageOffsets.StunRollSub, false, new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.StunPunchInstructionsAdd, true, new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.SinglePunchSub, false, 1);
            WriteValues(processHandle, "StunPunchDamage", (int)DamageOffsets.PunchKnockOverAdd, true, 1);

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public bool AreStunValuesOneShot()
        {

            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Error: Could not open process when calling AreStunValuesNormal function.");
                return false;
            }

            bool areNormal = true;

            byte[] cqCSlamNormal1 = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("CQCSlamNormal"), (int)DamageOffsets.CQCSlam1Add), 4);
            areNormal &= BitConverter.ToInt32(cqCSlamNormal1, 0) == -99999;

            byte[] cqCSlamNormal2 = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("CQCSlamNormal"), (int)DamageOffsets.CQCSlam2Add), 4);
            areNormal &= BitConverter.ToInt32(cqCSlamNormal2, 0) == -99999;

            byte[] stunNadeDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunNadeDamage"), (int)DamageOffsets.StunNadeSub), 6);
            areNormal &= stunNadeDamage.SequenceEqual(new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });

            byte[] triplePunchDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.TriplePunchSub), 1);
            areNormal &= triplePunchDamage[0] == 232;

            byte[] stunRollDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunRollDamage"), (int)DamageOffsets.StunRollSub), 6);
            areNormal &= stunRollDamage.SequenceEqual(new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });

            byte[] stunPunchInstructions = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.StunPunchInstructionsAdd), 6);
            areNormal &= stunPunchInstructions.SequenceEqual(new byte[] { 0x29, 0x86, 0x40, 0x01, 0x00, 0x00 });

            byte[] singlePunchDamage = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Subtract(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.SinglePunchSub), 1);
            areNormal &= singlePunchDamage[0] == 1;

            byte[] punchKnockOverThreshold = MemoryManager.ReadMemoryBytes(processHandle, IntPtr.Add(MemoryManager.Instance.FindAob("StunPunchDamage"), (int)DamageOffsets.PunchKnockOverAdd), 1);
            areNormal &= punchKnockOverThreshold[0] == 1;

            MemoryManager.NativeMethods.CloseHandle(processHandle);
            return areNormal;
        }

        #endregion

        #endregion

    }
}