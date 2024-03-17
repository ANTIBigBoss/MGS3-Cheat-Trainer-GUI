using System.Diagnostics;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    internal class ItemWeaponManager
    {
        internal static void ToggleWeapon(Weapon weapon, bool enable)
        {
            short stateValue = enable ? (short)1 : (short)-1;

            // Retrieve the updated address for the weapon
            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);

            if (weaponAddress != IntPtr.Zero)
            {
                // Assuming the toggle address is the same as the weapon's base address (adjust as needed)
                MemoryManager.Instance.WriteShortToMemory(weaponAddress, stateValue);
                LoggingManager.Instance.Log($"Toggled {weapon.Name} {(enable ? "on" : "off")}");
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ToggleItemState(Item item, bool enable)
        {
            short stateValue = enable ? (short)1 : (short)-1;

            // Retrieve the updated address for the item
            IntPtr itemAddress = ItemAddresses.GetAddress(item.Index, MemoryManager.Instance);

            if (itemAddress != IntPtr.Zero)
            {
                // Modify the short value at the item's address
                MemoryManager.Instance.WriteShortToMemory(itemAddress, stateValue);
                LoggingManager.Instance.Log($"Toggled {item.Name} {(enable ? "on" : "off")}");
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {item.Name}");
            }
        }

        internal static void ModifyItemCapacity(Item item, string itemCountStr)
        {
            short newCapacity;
            if (!short.TryParse(itemCountStr, out newCapacity))
            {
                return;
            }

            // Retrieve the updated address for the item
            IntPtr itemAddress = ItemAddresses.GetAddress(item.Index, MemoryManager.Instance);

            if (itemAddress != IntPtr.Zero)
            {
                // Calculate specific addresses for current capacity and maximum capacity
                IntPtr currentCapacityAddress = IntPtr.Add(itemAddress, ItemAddresses.CurrentCapacityOffset);
                IntPtr maxCapacityAddress = IntPtr.Add(itemAddress, item.MaxCapacityOffset.ToInt32());

                // Write the new capacity value to the calculated addresses
                MemoryManager.Instance.WriteShortToMemory(currentCapacityAddress, newCapacity);
                MemoryManager.Instance.WriteShortToMemory(maxCapacityAddress, newCapacity);
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {item.Name}");
            }
        }

        // Should try and implement in the ItemForm eventually
        internal static void ModifyMaxItemCapacity(Item item, string itemCountStr)
        {
            short newCapacity;
            if (!short.TryParse(itemCountStr, out newCapacity))
            {
                return;
            }

            // Retrieve the updated address for the item
            IntPtr itemAddress = ItemAddresses.GetAddress(item.Index, MemoryManager.Instance);

            if (itemAddress != IntPtr.Zero)
            {
                // Check if the item has a maximum capacity
                if (item.MaxCapacityOffset != IntPtr.Zero)
                {
                    // Calculate the address for the maximum capacity
                    IntPtr maxCapacityAddress = IntPtr.Add(itemAddress, item.MaxCapacityOffset.ToInt32());

                    // Write the new capacity to the maximum capacity address
                    MemoryManager.Instance.WriteShortToMemory(maxCapacityAddress, newCapacity);
                }
                else
                {
                    // If the item doesn't have a maximum capacity, simply modify the short value at the item's address
                    MemoryManager.Instance.WriteShortToMemory(itemAddress, newCapacity);
                }
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {item.Name}");
            }
        }


        internal static void ModifyClipSize(Weapon weapon, string clipSize)
        {
            short newSize;
            if (!short.TryParse(clipSize, out newSize))
            {
                return;
            }

            // Retrieve the updated base address for the weapon
            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);

            if (weaponAddress != IntPtr.Zero)
            {
                IntPtr clipSizeAddress = IntPtr.Add(weaponAddress, weapon.ClipOffset.ToInt32());
                MemoryManager.Instance.WriteShortToMemory(clipSizeAddress, newSize);
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ModifyMaxClipSize(Weapon weapon, string clipSize)
        {
            short newSize;
            if (!short.TryParse(clipSize, out newSize))
            {
                return;
            }

            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);

            if (weaponAddress != IntPtr.Zero)
            {
                IntPtr maxClipSizeAddress = IntPtr.Add(weaponAddress, weapon.MaxClipOffset.ToInt32());
                MemoryManager.Instance.WriteShortToMemory(maxClipSizeAddress, newSize);
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ModifyCurrentAndMaxClipSize(Weapon weapon, string clipSize)
        {
            short newSize;
            if (!short.TryParse(clipSize, out newSize))
            {
                return;
            }

            // Retrieve the updated base address for the weapon
            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);

            if (weaponAddress != IntPtr.Zero)
            {
                // Calculate specific addresses for clip size and maximum clip size
                IntPtr clipSizeAddress = IntPtr.Add(weaponAddress, weapon.ClipOffset.ToInt32());
                IntPtr maxClipSizeAddress = IntPtr.Add(weaponAddress, weapon.MaxClipOffset.ToInt32());

                // Write the new clip size to the calculated addresses
                MemoryManager.Instance.WriteShortToMemory(clipSizeAddress, newSize);
                MemoryManager.Instance.WriteShortToMemory(maxClipSizeAddress, newSize);
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }


        internal static void ModifyAmmo(Weapon weapon, string ammoCount)
        {
            short ammoValue;
            if (!short.TryParse(ammoCount, out ammoValue))
            {
                return;
            }

            // Retrieve the updated base address for the weapon
            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);

            if (weaponAddress != IntPtr.Zero)
            {
                // Calculate specific addresses for current ammo, max ammo, and clip
                IntPtr currentAmmoAddress = IntPtr.Add(weaponAddress, WeaponAddresses.CurrentAmmoOffset);              

                // Write the ammo value to the calculated addresses
                MemoryManager.Instance.WriteShortToMemory(currentAmmoAddress, ammoValue);
                // You can also update the clip if needed
                // MemoryManager.Instance.WriteShortToMemory(clipAddress, ammoValue);
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        // This Changes this max ammo but not the current ammo
        internal static void ModifyMaxAmmo(Weapon weapon, string ammoCount)
        {
            short ammoValue;
            if (!short.TryParse(ammoCount, out ammoValue))
            {
                return;
            }

            // Retrieve the updated address for the weapon
            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);

            if (weaponAddress != IntPtr.Zero)
            {
                // Calculate the specific ammo address
                int offset = weapon.MaxAmmoOffset.ToInt32(); // Convert IntPtr to int
                IntPtr ammoAddress = IntPtr.Add(weaponAddress, offset);

                // Write the ammo value to the calculated address
                MemoryManager.Instance.WriteShortToMemory(ammoAddress, ammoValue);
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ModifyCurrentAndMaxAmmo(Weapon weapon, string ammoCount)
        {
            short ammoValue;
            if (!short.TryParse(ammoCount, out ammoValue))
            {
                return;
            }

            // Retrieve the updated base address for the weapon
            IntPtr weaponAddress = WeaponAddresses.GetAddress(weapon.Index, MemoryManager.Instance);

            if (weaponAddress != IntPtr.Zero)
            {
                // Calculate specific addresses for current ammo, max ammo, and clip
                IntPtr currentAmmoAddress = IntPtr.Add(weaponAddress, WeaponAddresses.CurrentAmmoOffset);
                IntPtr maxAmmoAddress = IntPtr.Add(weaponAddress, weapon.MaxAmmoOffset.ToInt32());

                // Write the ammo value to the calculated addresses
                MemoryManager.Instance.WriteShortToMemory(currentAmmoAddress, ammoValue);
                MemoryManager.Instance.WriteShortToMemory(maxAmmoAddress, ammoValue);
                // You can also update the clip if needed
                // MemoryManager.Instance.WriteShortToMemory(clipAddress, ammoValue);
            }
            else
            {
                LoggingManager.Instance.Log($"Failed to retrieve address for {weapon.Name}");
            }
        }

        internal static void ToggleSuppressor(Weapon suppressableWeapon)
        {
            IntPtr suppressorAddress = IntPtr.Zero;

            // Retrieve the updated base address for the weapon
            IntPtr weaponAddress = WeaponAddresses.GetAddress(suppressableWeapon.Index, MemoryManager.Instance);

            if (weaponAddress != IntPtr.Zero)
            {
                // Calculate the address for the suppressor toggle
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

        // Not much use for these functions since I release V2.0 of the trainer, but it feels like a waste to remove it.
        internal static void AdjustSuppressorCapacity(Item suppressorItem, bool increaseCapacity)
        {
            IntPtr suppressorAddress = ItemAddresses.GetAddress(suppressorItem.Index, MemoryManager.Instance);

            if (suppressorAddress == IntPtr.Zero)
            {
                return;
            }

            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            if (processHandle == IntPtr.Zero)
            {
                return;
            }

            // Read the current suppressor capacity
            short currentValue = MemoryManager.ReadShortFromMemory(processHandle, suppressorAddress);

            // Ensure the value stays within ushort bounds
            ushort newValue = (ushort)(increaseCapacity ? Math.Min((ushort)currentValue + 30, ushort.MaxValue) : Math.Max((ushort)currentValue - 30, ushort.MinValue));

            if (newValue == currentValue)
            {
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return;
            }

            // Write the new suppressor capacity value
            int bytesWritten = MemoryManager.WriteShortToMemory(processHandle, suppressorAddress, (short)newValue);
            if (bytesWritten != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to change suppressor capacity for {suppressorItem.Name}");
            }
            else
            {
                LoggingManager.Instance.Log($"Changed suppressor capacity to {newValue}");
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }

        public static bool ReadWriteToggledSuppressorValue(IntPtr processHandle, IntPtr address)
        {
            bool success = NativeMethods.ReadProcessMemory(processHandle, address, out short currentValue, sizeof(short), out int bytesRead);
            if (!success || bytesRead != sizeof(short))
            {
                LoggingManager.Instance.Log($"Failed to read suppressor value at {address}");
                return false;
            }

            short valueToWrite = (currentValue == 16) ? (short)0 : (short)16;

            try
            {
                int bytesWritten = WriteShortToMemory(processHandle, address, valueToWrite);
                LoggingManager.Instance.Log($"Toggled suppressor to {(valueToWrite == 16 ? "on" : "off")}");
                return bytesWritten == sizeof(short);
            }
            catch
            {
                LoggingManager.Instance.Log($"Failed to write suppressor value at {address}");
                return false;
            }
        }
    }
}