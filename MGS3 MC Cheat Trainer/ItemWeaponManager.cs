using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            }
            else
            {
                MessageBox.Show($"{weapon.Name} address not found in memory.");
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
            }
            else
            {
                MessageBox.Show($"{item.Name} address not found in memory.");
            }
        }

        internal static void ModifyItemCapacity(Item item, string itemCountStr)
        {
            short newCapacity;
            if (!short.TryParse(itemCountStr, out newCapacity))
            {
                MessageBox.Show("Invalid item capacity.");
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
                MessageBox.Show($"{item.Name} address not found in memory.");
            }
        }

        internal static void ModifyMaxItemCapacity(Item item, string itemCountStr)
        {
            short newCapacity;
            if (!short.TryParse(itemCountStr, out newCapacity))
            {
                MessageBox.Show("Invalid item capacity.");
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
                MessageBox.Show($"{item.Name} address not found in memory.");
            }
        }


        internal static void ModifyClipSize(Weapon weapon, string clipSize)
        {
            short newSize;
            if (!short.TryParse(clipSize, out newSize))
            {
                MessageBox.Show("Invalid clip size.");
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
                MessageBox.Show($"{weapon.Name} address not found in memory.");
            }
        }

        internal static void ModifyMaxClipSize(Weapon weapon, string clipSize)
        {
            short newSize;
            if (!short.TryParse(clipSize, out newSize))
            {
                MessageBox.Show("Invalid clip size.");
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
                MessageBox.Show($"{weapon.Name} address not found in memory.");
            }
        }

        internal static void ModifyCurrentAndMaxClipSize(Weapon weapon, string clipSize)
        {
            short newSize;
            if (!short.TryParse(clipSize, out newSize))
            {
                MessageBox.Show("Invalid clip size.");
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
                MessageBox.Show($"{weapon.Name} address not found in memory.");
            }
        }


        internal static void ModifyAmmo(Weapon weapon, string ammoCount)
        {
            short ammoValue;
            if (!short.TryParse(ammoCount, out ammoValue))
            {
                MessageBox.Show("Invalid ammo count.");
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
                MessageBox.Show($"{weapon.Name} address not found in memory.");
            }
        }

        // This Changes this max ammo but not the current ammo
        internal static void ModifyMaxAmmo(Weapon weapon, string ammoCount)
        {
            short ammoValue;
            if (!short.TryParse(ammoCount, out ammoValue))
            {
                MessageBox.Show("Invalid ammo count.");
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
                MessageBox.Show($"{weapon.Name} address not found in memory.");
            }
        }

        internal static void ModifyCurrentAndMaxAmmo(Weapon weapon, string ammoCount)
        {
            short ammoValue;
            if (!short.TryParse(ammoCount, out ammoValue))
            {
                MessageBox.Show("Invalid ammo count.");
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
                MessageBox.Show($"{weapon.Name} address not found in memory.");
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
            }
            else
            {
                MessageBox.Show($"{suppressableWeapon.Name} address not found in memory.");
                return;
            }

            Process process;

            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                MessageBox.Show($"Cannot find process: {Constants.PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            if (!ReadWriteToggledSuppressorValue(processHandle, suppressorAddress))
            {
                MessageBox.Show("Failed to toggle suppressor.");
            }

            NativeMethods.CloseHandle(processHandle);
        }

        internal static void AdjustSuppressorCapacity(Item suppressorItem, bool increaseCapacity)
        {
            IntPtr suppressorAddress = ItemAddresses.GetAddress(suppressorItem.Index, MemoryManager.Instance);

            if (suppressorAddress == IntPtr.Zero)
            {
                MessageBox.Show($"Unable to find {suppressorItem.Name} in memory.");
                return;
            }

            Process process = MemoryManager.GetMGS3Process();
            IntPtr processHandle = MemoryManager.NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            if (processHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open process for reading/writing.");
                return;
            }

            // Read the current suppressor capacity
            short currentValue = MemoryManager.ReadShortFromMemory(processHandle, suppressorAddress);

            // Ensure the value stays within ushort bounds
            ushort newValue = (ushort)(increaseCapacity ? Math.Min((ushort)currentValue + 30, ushort.MaxValue) : Math.Max((ushort)currentValue - 30, ushort.MinValue));

            if (newValue == currentValue)
            {
                MessageBox.Show(increaseCapacity ? "Suppressor capacity is already at maximum." : "Suppressor capacity is already at minimum.");
                MemoryManager.NativeMethods.CloseHandle(processHandle);
                return;
            }

            // Write the new suppressor capacity value
            int bytesWritten = MemoryManager.WriteShortToMemory(processHandle, suppressorAddress, (short)newValue);
            if (bytesWritten != sizeof(short))
            {
                MessageBox.Show($"Failed to write new value for {suppressorItem.Name}.");
            }
            else
            {
                MessageBox.Show($"Suppressor capacity for {suppressorItem.Name} set to {newValue}.");
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);
        }
    }
}