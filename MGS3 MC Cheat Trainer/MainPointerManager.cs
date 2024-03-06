using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MGS3_MC_Cheat_Trainer.MemoryManager;
using static MGS3_MC_Cheat_Trainer.Constants;

namespace MGS3_MC_Cheat_Trainer
{
    internal class MainPointerManager
    {

        internal static void ApplyInjury(Constants.InjuryType injuryType)
        {
            try
            {
                Process process = GetMGS3Process();
                if (process == null)
                {
                    return;
                }

                IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
                IntPtr moduleBaseAddress = process.MainModule.BaseAddress;
                IntPtr pointerToInjurySlot = moduleBaseAddress + Constants.MainPointerRegionOffset;

                byte[] buffer = new byte[IntPtr.Size];
                if (!NativeMethods.ReadProcessMemory(processHandle, pointerToInjurySlot, buffer, (uint)buffer.Length, out _))
                {
                    NativeMethods.CloseHandle(processHandle);
                    return;
                }

                IntPtr baseInjurySlotAddress = (IntPtr.Size == 8) ? (IntPtr)BitConverter.ToInt64(buffer, 0) : (IntPtr)BitConverter.ToInt32(buffer, 0);
                byte[] injuryBytes = Constants.InjuryData.GetInjuryBytes(injuryType);

                bool injuryApplied = false;
                for (int slot = 1; slot <= Constants.Offsets.InjurySlots.TotalSlots; slot++)
                {
                    IntPtr injurySlotAddress = IntPtr.Add(baseInjurySlotAddress, Constants.Offsets.InjurySlots.CalculateOffset(slot));

                    byte[] currentInjuryData = ReadMemoryBytes(processHandle, injurySlotAddress, Constants.Offsets.InjurySlots.SlotSize);
                    if (currentInjuryData == null || !currentInjuryData.SequenceEqual(new byte[Constants.Offsets.InjurySlots.SlotSize]))
                    {
                        continue; // Slot is not empty, skip it
                    }

                    if (NativeMethods.WriteProcessMemory(processHandle, injurySlotAddress, injuryBytes, (uint)injuryBytes.Length, out _))
                    {
                        injuryApplied = true;
                        break; // Exit the loop as injury is applied
                    }
                }

                if (!injuryApplied)
                {
                }
                NativeMethods.CloseHandle(processHandle);
            }
            catch (Exception ex)
            {
            }
        }

        internal static void RemoveAllInjuries()
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                return;
            }

            IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            IntPtr moduleBaseAddress = process.MainModule.BaseAddress;
            IntPtr pointerToInjurySlot = moduleBaseAddress + Constants.MainPointerRegionOffset;

            byte[] buffer = new byte[IntPtr.Size];
            if (!NativeMethods.ReadProcessMemory(processHandle, pointerToInjurySlot, buffer, (uint)buffer.Length, out _))
            {
                NativeMethods.CloseHandle(processHandle);
                return;
            }

            IntPtr baseInjurySlotAddress = (IntPtr.Size == 8) ? (IntPtr)BitConverter.ToInt64(buffer, 0) : (IntPtr)BitConverter.ToInt32(buffer, 0);

            byte[] emptyInjuryData = new byte[Constants.Offsets.InjurySlots.SlotSize]; // Array of zeros


            bool allCleared = true;
            for (int slot = 1; slot <= 68; slot++)
            {
                IntPtr injurySlotAddress = IntPtr.Add(baseInjurySlotAddress, Constants.Offsets.InjurySlots.CalculateOffset(slot));

                // Write the empty pattern to the slot
                bool writeSuccess = NativeMethods.WriteProcessMemory(processHandle, injurySlotAddress, emptyInjuryData, (uint)emptyInjuryData.Length, out _);
                if (!writeSuccess)
                {
                    allCleared = false;
                    break; // Stop the process if unable to write to memory
                }
            }

            NativeMethods.CloseHandle(processHandle);

            if (allCleared)
            {
            }
        }

        internal static void ModifyHealthOrStamina(Constants.HealthType healthType, int value, bool setExactValue = false)
        {
            Process process;

            try
            {
                process = GetMGS3Process();
            }
            catch
            {
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            IntPtr pointerBase = (IntPtr)Constants.MainPointerRegionOffset;
            IntPtr pointerAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)pointerBase);
            byte[] pointerBuffer = new byte[IntPtr.Size];
            NativeMethods.ReadProcessMemory(processHandle, pointerAddress, pointerBuffer, (uint)pointerBuffer.Length, out _);
            IntPtr valuePointer = (IntPtr.Size == 8) ? (IntPtr)BitConverter.ToInt64(pointerBuffer, 0) : (IntPtr)BitConverter.ToInt32(pointerBuffer, 0);

            int valueOffset;
            // Adjust the valueOffset based on the healthType
            switch (healthType)
            {
                case Constants.HealthType.MaxHealth:
                    valueOffset = Constants.Offsets.Health.Max;
                    break;
                case Constants.HealthType.Stamina:
                    valueOffset = Constants.Offsets.Stamina.Current;
                    break;
                default:
                case Constants.HealthType.CurrentHealth:
                    valueOffset = Constants.Offsets.Health.Current;
                    break;
            }

            IntPtr valueAddress = IntPtr.Add(valuePointer, valueOffset);
            byte[] valueBuffer = new byte[sizeof(short)];

            NativeMethods.ReadProcessMemory(processHandle, valueAddress, valueBuffer, (uint)valueBuffer.Length, out _);
            short currentValue = BitConverter.ToInt16(valueBuffer, 0);

            short newValue;
            if (setExactValue)
            {
                newValue = (short)Math.Max(0, Math.Min(value, short.MaxValue));
            }
            else
            {
                newValue = (short)Math.Max(0, Math.Min(currentValue + value, short.MaxValue));
            }

            byte[] newValueBuffer = BitConverter.GetBytes(newValue);
            NativeMethods.WriteProcessMemory(processHandle, valueAddress, newValueBuffer, (uint)newValueBuffer.Length, out _);

            NativeMethods.CloseHandle(processHandle);
        }
    }
}



/*
        internal static void LogInjurySlots()
        {
            StringBuilder logBuilder = new StringBuilder();
            Process process = GetMGS3Process();
            if (process == null)
            {
                MessageBox.Show("Game process not found.");
                return;
            }

            IntPtr processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);
            IntPtr moduleBaseAddress = process.MainModule.BaseAddress;
            IntPtr pointerToInjurySlot = moduleBaseAddress + Constants.MainPointerRegionOffset;

            byte[] buffer = new byte[IntPtr.Size];
            if (!NativeMethods.ReadProcessMemory(processHandle, pointerToInjurySlot, buffer, (uint)buffer.Length, out _))
            {
                MessageBox.Show("Failed to read the base injury slot address.");
                NativeMethods.CloseHandle(processHandle);
                return;
            }

            IntPtr baseInjurySlotAddress = (IntPtr.Size == 8) ? (IntPtr)BitConverter.ToInt64(buffer, 0) : (IntPtr)BitConverter.ToInt32(buffer, 0);

            for (int slot = 1; slot <= 68; slot++)
            {
                IntPtr injurySlotAddress = IntPtr.Add(baseInjurySlotAddress, CalculateInjuryOffset(slot));
                byte[] injuryData = ReadMemoryBytes(processHandle, injurySlotAddress, Constants.slotSize);

                if (injuryData != null)
                {
                    string injuryBytes = BitConverter.ToString(injuryData).Replace("-", " ");
                    logBuilder.AppendLine($"Injury Slot {slot}: {injuryBytes}");
                }
                else
                {
                    logBuilder.AppendLine($"Injury Slot {slot}: [Failed to read]");
                }
            }

            NativeMethods.CloseHandle(processHandle);
            MessageBox.Show(logBuilder.ToString(), "Injury Slot Data");
        } */