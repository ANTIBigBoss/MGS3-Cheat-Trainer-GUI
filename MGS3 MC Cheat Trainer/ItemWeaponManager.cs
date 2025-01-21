using System.Diagnostics;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    internal class ItemWeaponManager
    {
        internal static void ToggleWeapon(Weapon weapon, bool enable)
        {
            short stateValue = enable ? (short)1 : (short)-1;

            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());

            if (weaponAddress != IntPtr.Zero)
            {
                MemoryManager.WriteMemory(processHandle, weaponAddress, stateValue);
                LoggingManager.Instance.Log($"Toggled {weapon.Name} {(enable ? "on" : "off")}");

                MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
                if (processHandle != IntPtr.Zero)
                {
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
            }
        }

        internal static void ToggleItemState(Item item, bool enable)
        {
            short stateValue = enable ? (short)1 : (short)-1;
            IntPtr itemAddress = ItemAddresses.GetAddress(item.Index, MemoryManager.Instance);
            IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());

            if (itemAddress != IntPtr.Zero && processHandle != IntPtr.Zero)
            {
                MemoryManager.WriteMemory(processHandle, itemAddress, stateValue);
                LoggingManager.Instance.Log($"Toggled {item.Name} {(enable ? "on" : "off")}");

                MemoryManager.NativeMethods.CloseHandle(processHandle);
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address or open process for {item.Name}");
                if (processHandle != IntPtr.Zero)
                {
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
            }
        }

        internal static void ModifyItemCapacity(Item item, string itemCountStr)
        {
            if (!short.TryParse(itemCountStr, out short newCapacity))
            {
                LoggingManager.Instance.Log("Invalid item count string.");
                return;
            }

            IntPtr itemAddress = ItemAddresses.GetAddress(item.Index, MemoryManager.Instance);

            if (itemAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());

                if (processHandle != IntPtr.Zero)
                {
                    IntPtr currentCapacityAddress = IntPtr.Add(itemAddress, ItemAddresses.CurrentCapacityOffset);
                    IntPtr maxCapacityAddress = IntPtr.Add(itemAddress, item.MaxCapacityOffset.ToInt32());
                    MemoryManager.WriteMemory(processHandle, currentCapacityAddress, newCapacity);
                    MemoryManager.WriteMemory(processHandle, maxCapacityAddress, newCapacity);

                    LoggingManager.Instance.Log($"Updated capacity for {item.Name} to {newCapacity}.");

                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to open process handle.");
                }
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {item.Name}");
            }
        }

        internal static void ModifyMaxItemCapacity(Item item, string itemCountStr)
        {
            if (!short.TryParse(itemCountStr, out short newCapacity))
            {
                LoggingManager.Instance.Log("Invalid capacity value.");
                return;
            }

            IntPtr itemAddress = ItemAddresses.GetAddress(item.Index, MemoryManager.Instance);

            if (itemAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    if (item.MaxCapacityOffset != IntPtr.Zero)
                    {
                        IntPtr maxCapacityAddress = IntPtr.Add(itemAddress, item.MaxCapacityOffset.ToInt32());

                        MemoryManager.WriteMemory(processHandle, maxCapacityAddress, newCapacity);
                    }
                    else
                    {
                        MemoryManager.WriteMemory(processHandle, itemAddress, newCapacity);
                    }

                    LoggingManager.Instance.Log($"Updated max capacity for {item.Name} to {newCapacity}.");
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to open process handle.");
                }
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {item.Name}");
            }
        }

        internal static void ModifyClipSize(Weapon weapon, string clipSize)
        {
            if (!short.TryParse(clipSize, out short newSize))
            {
                LoggingManager.Instance.Log("Invalid clip size value.");
                return;
            }

            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);
            if (weaponAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    IntPtr clipSizeAddress = IntPtr.Add(weaponAddress, weapon.ClipOffset.ToInt32());
                    MemoryManager.WriteMemory(processHandle, clipSizeAddress, newSize);
                    LoggingManager.Instance.Log($"Updated clip size for {weapon.Name} to {newSize}.");
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to open process handle.");
                }
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ModifyMaxClipSize(Weapon weapon, string clipSize)
        {
            if (!short.TryParse(clipSize, out short newSize))
            {
                LoggingManager.Instance.Log("Invalid max clip size value.");
                return;
            }

            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);
            if (weaponAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    IntPtr maxClipSizeAddress = IntPtr.Add(weaponAddress, weapon.MaxClipOffset.ToInt32());
                    MemoryManager.WriteMemory(processHandle, maxClipSizeAddress, newSize);
                    LoggingManager.Instance.Log($"Updated max clip size for {weapon.Name} to {newSize}.");
                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to open process handle.");
                }
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ModifyCurrentAndMaxClipSize(Weapon weapon, string clipSize)
        {
            if (!short.TryParse(clipSize, out short newSize))
            {
                LoggingManager.Instance.Log("Invalid clip size value.");
                return;
            }

            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);
            if (weaponAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    IntPtr clipSizeAddress = IntPtr.Add(weaponAddress, weapon.ClipOffset.ToInt32());
                    IntPtr maxClipSizeAddress = IntPtr.Add(weaponAddress, weapon.MaxClipOffset.ToInt32());

                    MemoryManager.WriteMemory(processHandle, clipSizeAddress, newSize);
                    MemoryManager.WriteMemory(processHandle, maxClipSizeAddress, newSize);
                    LoggingManager.Instance.Log($"Updated current and max clip sizes for {weapon.Name} to {newSize}.");

                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to open process handle.");
                }
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ModifyAmmo(Weapon weapon, string ammoCount)
        {
            if (!short.TryParse(ammoCount, out short ammoValue))
            {
                LoggingManager.Instance.Log("Invalid ammo count value.");
                return;
            }

            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);
            if (weaponAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    IntPtr currentAmmoAddress = IntPtr.Add(weaponAddress, WeaponAddresses.CurrentAmmoOffset);

                    MemoryManager.WriteMemory(processHandle, currentAmmoAddress, ammoValue);
                    LoggingManager.Instance.Log($"Updated ammo for {weapon.Name} to {ammoValue}.");

                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to open process handle.");
                }
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ModifyMaxAmmo(Weapon weapon, string ammoCount)
        {
            if (!short.TryParse(ammoCount, out short ammoValue))
            {
                LoggingManager.Instance.Log("Invalid max ammo count value.");
                return;
            }

            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);
            if (weaponAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    int offset = weapon.MaxAmmoOffset.ToInt32();
                    IntPtr ammoAddress = IntPtr.Add(weaponAddress, offset);

                    MemoryManager.WriteMemory(processHandle, ammoAddress, ammoValue);
                    LoggingManager.Instance.Log($"Updated max ammo for {weapon.Name} to {ammoValue}.");

                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to open process handle.");
                }
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ModifyCurrentAndMaxAmmo(Weapon weapon, string ammoCount)
        {
            if (!short.TryParse(ammoCount, out short ammoValue))
            {
                LoggingManager.Instance.Log("Invalid ammo count value.");
                return;
            }

            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);
            if (weaponAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    IntPtr currentAmmoAddress = IntPtr.Add(weaponAddress, WeaponAddresses.CurrentAmmoOffset);
                    IntPtr maxAmmoAddress = IntPtr.Add(weaponAddress, weapon.MaxAmmoOffset.ToInt32());
                    MemoryManager.WriteMemory(processHandle, currentAmmoAddress, ammoValue);
                    MemoryManager.WriteMemory(processHandle, maxAmmoAddress, ammoValue);
                    LoggingManager.Instance.Log($"Updated current and max ammo for {weapon.Name} to {ammoValue}.");

                    MemoryManager.NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    LoggingManager.Instance.Log("Failed to open process handle.");
                }
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ToggleSuppressor(Weapon suppressableWeapon)
        {
            IntPtr suppressorAddress = IntPtr.Zero;
            IntPtr weaponAddress = WeaponAddresses.GetAddress(suppressableWeapon.Index, MemoryManager.Instance);

            if (weaponAddress != IntPtr.Zero)
            {
                suppressorAddress = IntPtr.Add(weaponAddress, suppressableWeapon.SuppressorToggleOffset.ToInt32());
                LoggingManager.Instance.Log($"Suppressor address: {suppressorAddress}");
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {suppressableWeapon.Name}");
                return;
            }

            Process process;

            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                LoggingManager.Instance.Log("Failed to retrieve MGS3 process");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            if (!ReadWriteToggledSuppressorValue(processHandle, suppressorAddress))
            {
                LoggingManager.Instance.Log($"Failed to toggle suppressor for {suppressableWeapon.Name}");
            }

            NativeMethods.CloseHandle(processHandle);
        }

        public static bool ReadWriteToggledSuppressorValue(IntPtr processHandle, IntPtr address)
        {
            bool success = MemoryManager.NativeMethods.ReadProcessMemory(processHandle, address, out short currentValue, sizeof(short), out int bytesRead);
            if (!success || bytesRead != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read suppressor value at {address}");
                return false;
            }

            short valueToWrite = (currentValue == 16) ? (short)0 : (short)16;

            try
            {
                success = MemoryManager.WriteMemory(processHandle, address, valueToWrite);
                LoggingManager.Instance.Log($"Toggled suppressor to {(valueToWrite == 16 ? "on" : "off")}");
                return success;
            }
            catch
            {
                LoggingManager.Instance.Log($"Failed to write suppressor value at {address}");
                return false;
            }
        }
    }
}