﻿using System;
using System.Diagnostics;
using System.Windows.Forms; // For MessageBox
// Use MemoryManager
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public class AobManager
    {
        private static AobManager instance;
        private static readonly object lockObj = new object();

        // Private constructor to prevent external instantiation
        private AobManager() { }

        // Public property to access the instance
        public static AobManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new AobManager();
                    }
                    return instance;
                }
            }
        }
        private MemoryManager memoryManager;

        public static readonly Dictionary<string, (byte[] Pattern, string Mask, IntPtr? StartOffset, IntPtr? EndOffset)> AOBs =
            new Dictionary<string, (byte[] Pattern, string Mask, IntPtr? StartOffset, IntPtr? EndOffset)>

            {
                // Expand this to see documentation on how I use AOBs in this project:
                #region AOB Explanation

                /*
                This section is how I handle AOB scanning usage is as follows:

                1. Add the AOB to the dictionary like so:
                "NameOfAobKeyHere", // Generally use a name that contextually makes sense for what it accesses

                2. Add the byte[] pattern this can be a steady pattern or a pattern with wildcards
                To use without wildcards list them normally like this:
                (new byte[] {0x00, 0x00, 0xAA, 0x77, 0x63, 0x00 },
                The Mask would like like so for this pattern:
                "x x x x x x" // each x represents a byte in the pattern

                3. If you need to use wildcards use the ? character like so:
                (new byte[] { 0x00, 0x00, 0x50, 0x46, 0x00, 0x00 },
                0x00 is what I use for wildcards however if a byte is 00 and not a wildcard the mask tells the scanner to ignore it
                The Mask would look like this for this pattern:
                "x ? x x x ? // Anything in ? is a byte that is accounted for but not acknowledged what the byte is per se
                It'd be the same as telling the pattern: (new byte[] {0x00, 0x??, 0xAA, 0x77, 0x63, 0x?? },

                4. If you need to specify a range of memory to search for the AOB use the StartOffset and EndOffset
                from FindAob function in Memory Manager. We're basically telling it look for the pattern in this range of memory
                new IntPtr(0x1000000),
                new IntPtr(0x1FF0000)
                Would basically tell the scanner to look for the pattern in the range of memory from:
                METAL GEAR SOLID3.exe+0x1000000 to METAL GEAR SOLID3.exe+0x1FF0000
                Once implemented you can use the AOB like so:
                IntPtr aobResult = MemoryManager.FindAob("NameOfAobKeyHere");

                5. If you need to move forward or backward in memory you can use IntPtr.Add or IntPtr.Subtract like so:
                IntPtr targetAddress = IntPtr.Add(aobResult, 1); // This would move the address 1 byte forward from the AOB
                IntPtr targetAddress = IntPtr.Subtract(aobResult, 1); // This would move the address 1 byte backward from the AOB
                Note: When using IntPtr.Add/Subtract .Net uses the numbers in decimal, so you don't need to convert to hex.
                */
                #endregion

                {
                    "AlertMemoryRegion", // ?? ?? 00 00 ?? ?? 00 00 50 46 00 00 FF FF FF FF
                    // ??xx??xxxxxxxxxx
                    (new byte[] { 0x00, 0x00, 0x50, 0x46, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF },
                        "? ? x x ? ? x x x x x x x x x x",
                        
                        // Specify the range of memory in the exe to search for
                        new IntPtr(0x1000000),
                        new IntPtr(0x1FF0000)
                    )
                },

                {
                    "WeaponsTable", // 00 00 AA 77 63 00
                    (new byte[] { 0x00, 0x00, 0xAA, 0x77, 0x63, 0x00 },
                        "x x x x x x",
                        new IntPtr(0x1C00000),
                        new IntPtr(0x1D00000)
                        // Once found 12 bytes forward is the Survival Knife
                        // Usage to travel forward looks like this it would also be using the class information from BaseMGS3Object.cs:
                        // IntPtr.Add(aobResult, Constants.TestAOBs["WeaponsTable"].Pattern.Length + 12 + (WeaponOffset * index));
                        )
                },
                {    
                    "ItemsTable", // 00 00 DA 5A 2B 00
                    (new byte[] { 0x00, 0x00, 0xDA, 0x5A, 0x2B, 0x00 },
                        "x x x x x x",
                        new IntPtr(0x1C00000),
                        new IntPtr(0x1D00000)
                        // Same as weapons but 12 bytes to get to the Life Medicine
                        // Usage to travel forward looks like this it would also be using the class information from BaseMGS3Object.cs:
                        //IntPtr.Add(aobResult, Constants.TestAOBs["ItemsTable"].Pattern.Length + 12 + (ItemOffset * index));
                        )
                },

                {   // Small Chance but this might be usable for animations and camo index(89BE/35262) 
                    "Animations", // 00 00 DA 5A 2B 00
                    // Same as Items but we search in a further region that has the same byte pattern
                    (new byte[] { 0x00, 0x00, 0xDA, 0x5A, 0x2B, 0x00 },
                        "x x x x x x",
                        new IntPtr(0x1D00000),
                        new IntPtr(0x1E00000)
                    )
                },

                {
                    "ModelDistortion", // 45 0F 29 43 C8 45 0F 29 4B B8 45 0F 29 53 A8 45 0F 29 5B 98 45 0F 29 63 88 44 0F 29 6C 24 30
                    (new byte[] { 0x45, 0x0F, 0x29, 0x43, 0xC8, 0x45, 0x0F, 0x29, 0x4B, 0xB8, 0x45, 0x0F, 0x29, 0x53, 0xA8, 0x45, 0x0F, 0x29, 0x5B, 0x98, 0x45, 0x0F, 0x29, 0x63, 0x88, 0x44, 0x0F, 0x29, 0x6C, 0x24, 0x30 },
                        "x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x x",
                        new IntPtr(0x000000),
                        new IntPtr(0xC00000)
                        )
                },

                {
                    "NotUpsideDownCamera", // 40 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
                    (new byte[]
                        {
                            0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                        },
                        "x x x x x x x x x x x x x x x x x x x x x x x x x x x x",
                        new IntPtr(0x00FFFF0),
                        new IntPtr(0x1000000)
                    )
                },
                {
                    "UpsideDownCamera", // BF 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
                    (new byte[]
                        {
                            0xBF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                        },
                        "x x x x x x x x x x x x x x x x x x x x x x x x x x x x x",
                        new IntPtr(0x00FFFF0),
                        new IntPtr(0x1000000)
                    )
                },

                {
                    "CamoOperations", // 3D E8 03 00 00 7C 07 B9 13 00 00 00 EB
                    (new byte[] { 0x3D, 0xE8, 0x03, 0x00, 0x00, 0x7C, 0x07, 0xB9, 0x13, 0x00, 0x00, 0x00, 0xEB },
                        "x x x x x x x x x x x x x",
                        new IntPtr(0x100000),
                        new IntPtr(0x430000)
                    )
                },

                {
                    "LeftBandana", // 33 33 93 3F 9A 99 B9 3F 9A 99 B9 3F
                    (new byte[] { 0x33, 0x33, 0x93, 0x3F, 0x9A, 0x99, 0xB9, 0x3F, 0x9A, 0x99, 0xB9, 0x3F },
                        "x x x x x x x x x x x x",
                        new IntPtr(0xA00000),
                        new IntPtr(0xB00000)
                    )
                },

                {
                    "RightBandana", // 29 5C 8F 3F 8F C2 B5 3F 8F C2 B5 3F
                    (new byte[] { 0x29, 0x5C, 0x8F, 0x3F, 0x8F, 0xC2, 0xB5, 0x3F, 0x8F, 0xC2, 0xB5, 0x3F },
                        "x x x x x x x x x x x x",
                        new IntPtr(0xA00000),
                        new IntPtr(0xB00000)
                    )
                },

                // These are all placeholder ranges until I solve where everything is
                {
                    "Alphabet", // 30 00 00 31 00 00 32 00 00 33 - 10958/2ACE is the camo index
                    (new byte[] { 0x30, 0x00, 0x00, 0x31, 0x00, 0x00, 0x32, 0x00, 0x00, 0x33 },
                        "x x x x x x x x x x",
                        new IntPtr(0x000000),
                        new IntPtr(0xC00000)
                    )
                },

                // All Damages are the two bytes before the AOB
                {
                    "MostWeaponsDamage", // 00 00 E8 98 65 FF FF 8B
                    (new byte[] { 0x00, 0x00, 0xE8, 0x98, 0x65, 0xFF, 0xFF, 0x8B },
                        "x x x x x x x x",
                        new IntPtr(0x000000),
                        new IntPtr(0xC00000)
                    )
                },

                {
                    "M63Damage", // 00 00 E8 6B 6C FF FF 8B
                    (new byte[] { 0x00, 0x00, 0xE8, 0x6B, 0x6C, 0xFF, 0xFF, 0x8B },
                        "x x x x x x x x",
                        new IntPtr(0x000000),
                        new IntPtr(0xC00000)
                    )
                },

                {
                    "ExplosionDamage", // 00 00 8B CD E8 30 3E 56
                    (new byte[] { 0x00, 0x00, 0x8B, 0xCD, 0xE8, 0x30, 0x3E, 0x56 },
                        "x x x x x x x x",
                        new IntPtr(0x000000),
                        new IntPtr(0xC00000)
                    )
                },

                // Shotgun and throat slit are different from other weapons
                {
                    // This AOB is directly before the 2 byte value that needs to be set to 90 90 to
                    // disable the damage C8 2B is the default for allowing Shotgun Damage 
                    "ShotgunDamage", // E9 05 09 00 00 8B 96 04 01 00 00 8B CD E8 55 46 56 00 8B 8E 38 01 00 00
                    (new byte[] { 0xE9, 0x05, 0x09, 0x00, 0x00, 0x8B, 0x96, 0x04, 0x01, 0x00, 0x00, 0x8B, 0xCD, 0xE8, 0x55, 0x46, 0x56, 0x00, 0x8B, 0x8E, 0x38, 0x01, 0x00, 0x00 },
                        "x x x x x x x x x x x x x x x x x x x x x x x x",
                        new IntPtr(0x000000),
                        new IntPtr(0xC00000)
                    )
                },

                {
                    // 22 bytes before the AOB is the 4-byte damage value but the damage value is
                    // actually what it sets the guard's health to
                    "ThroatSlitDamage", // 83 C4 20 5F C3 48 8D 93 00 04 00 00 41 B9 29 
                    (new byte[] { 0x83, 0xC4, 0x20, 0x5F, 0xC3, 0x48, 0x8D, 0x93, 0x00, 0x04, 0x00, 0x00, 0x41, 0xB9, 0x29 },
                        "x x x x x x x x x x x x x x x",
                        new IntPtr(0x000000),
                        new IntPtr(0xC00000)
                    )
                },

                {   // 22 Bytes before the AOB is the 4-byte damage value
                    "NeckSnapDamage", // 83 C4 20 5F C3 48 8D 93 00 04 00 00 41 B9 2B 00 00 00 4C 8D 05 A2 CA 94
                    (new byte[] { 0x83, 0xC4, 0x20, 0x5F, 0xC3, 0x48, 0x8D, 0x93, 0x00, 0x04, 0x00, 0x00, 0x41, 0xB9, 0x2B },
                        "x x x x x x x x x x x x x x x",
                        new IntPtr(0x000000),
                        new IntPtr(0xC00000)
                    )
                },

                {   // 0 Bytes before the AOB is the 4-byte damage value
                    "WpNadeDamage", // C3 CC CC CC CC CC 33 C0 39 81 38
                    (new byte[] { 0xC3, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0x33, 0xC0, 0x39, 0x81, 0x38 },
                        "x x x x x x x x x x x",
                        new IntPtr(0x000000),
                        new IntPtr(0xC00000)
                    )
                },

                {
                    // Fear, Pain and Volgin are confirmed for this AOB Ocelot did not work
                    "TheFearAOB", // F0 49 02 00 F0 49 02 00
                    (new byte[] { 0xF0, 0x49, 0x02, 0x00, 0xF0, 0x49, 0x02, 0x00 },
                        "x x x x x x x x",
                        new IntPtr(0x100FFFFFFFF),
                        new IntPtr(0x30000000000)
                    )
                },

                {
                    "TheFury", // 01 00 01 00 D0 00 00 00 06 00
                    (new byte[] { 0x01, 0x00, 0x01, 0x00, 0xD0, 0x00, 0x00, 0x00, 0x06, 0x00 },
                        "x x x x x x x x x x",
                        new IntPtr(0x100FFFFFFFF),
                        new IntPtr(0x30000000000)
                    )
                },

                {
                    // Starting Area for the boss fight with The End
                    "TheEnds063a", // 01 00 01 00 F0 74 00 00 80
                    (new byte[] { 0x01, 0x00, 0x01, 0x00, 0xF0, 0x74, 0x00, 0x00, 0x80 },
                        "x x x x x x x x x",
                        new IntPtr(0x100FFFFFFFF),
                        new IntPtr(0x30000000000)
                    )
                },

                {
                    // Area where The End dies and you proceed to the ladder area also works for s064a
                    "TheEnds065a", // 01 00 01 00 00 01 00 00 80 80 80 80 80 80 80 80
                    (new byte[] { 0x01, 0x00, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80 },
                        "x x x x x x x x x x x x x x x x",
                        new IntPtr(0x100FFFFFFFF),
                        new IntPtr(0x30000000000)
                    )
                },

                {
                    "Ocelot", // E0 66 00 00 80 80 80 80 80 80 80 80 80
                    (new byte[] { 0xE0, 0x66, 0x00, 0x00, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80 },
                        "x x x x x x x x x x x x x",
                        new IntPtr(0x100FFFFFFFF),
                        new IntPtr(0x30000000000)
                    )
                },

                {
                    "TheBoss", // D0 6E 00 00 80 80 80 80 80 80 80
                    (new byte[] { 0xD0, 0x6E, 0x00, 0x00, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80 },
                        "x x x x x x x x x x x",
                        new IntPtr(0x100FFFFFFFF),
                        new IntPtr(0x30000000000)
                    )
                },

                {
                    "Shagohod", // E6 99 48 00
                    (new byte[] { 0xE6, 0x99, 0x48, 0x00 },
                        "x x x x",
                        new IntPtr(0x100FFFFFFFF),
                        new IntPtr(0x30000000000)
                    )
                },

                {
                    "VolginOnShagohod", // 00 80 ED C4 00 00 80 3F 00 00 80 3F
                    (new byte[] { 0x00, 0x80, 0xED, 0xC4, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F },
                        "x x x x x x x x x x x x",
                        new IntPtr(0x100FFFFFFFF),
                        new IntPtr(0x30000000000)
                    )
                },


                {
                    "GuardPatroling", // 00 00 00 90 01 52 03
                    (new byte[] { 0x00, 0x00, 0x00, 0x90, 0x01, 0x52, 0x03 },
                        "x x x x x x x",
                        new IntPtr(0x100FFFFFFFF),
                        new IntPtr(0x30000000000)
                    )
                },

                {
                    "SnakeAndBossesStanding", // 00 00 00 90 01 20 03
                    (new byte[] { 0x00, 0x00, 0x00, 0x90, 0x01, 0x20, 0x03 },
                        "x x x x x x x",
                    new IntPtr(0x100FFFFFFFF),
                    new IntPtr(0x30000000000)
                    )
                },

                {
                    "SnakeAndGuardProne", // 00 00 00 96 00 2C 01
                    (new byte[] { 0x00, 0x00, 0x00, 0x96, 0x00, 0x2C, 0x01 },
                        "x x x x x x x",
                    new IntPtr(0x100FFFFFFFF),
                    new IntPtr(0x30000000000)
                    )
                },
            };

        public IntPtr FoundSnakePositionAddress { get; private set; } = IntPtr.Zero;

        public bool FindAndStoreSnakesPositionAOB()
        {
            IntPtr foundAddressStanding = MemoryManager.Instance.FindDynamicAob("SnakeAndBossesStanding");
            if (foundAddressStanding != IntPtr.Zero)
            {
                FoundSnakePositionAddress = foundAddressStanding;
                return true;
            }

            IntPtr foundAddressProne = MemoryManager.Instance.FindDynamicAob("SnakeAndGuardProne");
            if (foundAddressProne != IntPtr.Zero)
            {
                FoundSnakePositionAddress = foundAddressProne;
                return true;
            }

            // If neither pattern is found, return false.
            return false;
        }

        


        public IntPtr FoundOcelotAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheFearAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheEnds063aAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheEnds065aAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheFuryAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundShagohodAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundVolginOnShagohodAddress { get; private set; } = IntPtr.Zero;
        public IntPtr FoundTheBossAddress { get; private set; } = IntPtr.Zero;

        public bool FindAndStoreOcelotAOB()
        {
            IntPtr foundAddress = MemoryManager.Instance.FindDynamicAob("Ocelot");
            if (foundAddress != IntPtr.Zero)
            {
                FoundOcelotAddress = foundAddress;
                return true;
            }
            return false;
        }

        public bool FindAndStoreTheFearAOB()
        {
            IntPtr foundAddress = MemoryManager.Instance.FindDynamicAob("TheFearAOB");
            if (foundAddress != IntPtr.Zero)
            {
                FoundTheFearAddress = foundAddress;
                return true;
            }
            return false;
        }

        public bool FindAndStoreTheEnds063aAOB()
        {
            IntPtr foundAddress = MemoryManager.Instance.FindDynamicAob("TheEnds063a");
            if (foundAddress != IntPtr.Zero)
            {
                FoundTheEnds063aAddress = foundAddress;
                return true;
            }
            return false;
        }

        public bool FindAndStoreTheEnds065aAOB()
        {
            IntPtr foundAddress = MemoryManager.Instance.FindDynamicAob("TheEnds065a");
            if (foundAddress != IntPtr.Zero)
            {
                FoundTheEnds065aAddress = foundAddress;
                return true;
            }
            return false;
        }

        public bool FindAndStoreTheFuryAOB()
        {
            IntPtr foundAddress = MemoryManager.Instance.FindDynamicAob("TheFury");
            if (foundAddress != IntPtr.Zero)
            {
                FoundTheFuryAddress = foundAddress;
                return true;
            }
            return false;
        }

        public bool FindAndStoreTheBossAOB()
        {
            IntPtr foundAddress = MemoryManager.Instance.FindDynamicAob("TheBoss");
            if (foundAddress != IntPtr.Zero)
            {
                FoundTheBossAddress = foundAddress;
                return true;
            }
            return false;
        }

        public bool FindAndStoreShagohodAOB()
        {
            IntPtr foundAddress = MemoryManager.Instance.FindDynamicAob("Shagohod");
            if (foundAddress != IntPtr.Zero)
            {
                FoundShagohodAddress = foundAddress;
                return true;
            }
            return false;
        }

        public bool FindAndStoreVolginOnShagohodAOB()
        {
            IntPtr foundAddress = MemoryManager.Instance.FindDynamicAob("VolginOnShagohod");
            if (foundAddress != IntPtr.Zero)
            {
                FoundVolginOnShagohodAddress = foundAddress;
                return true;
            }
            return false;
        }

    }
}














/* Not as Dynamic as I hoped but the logic in theory is still good if a consistent AOB is found
  
    public bool FindAndStoreOcelotAOB()
   {
       if (OcelotHealthAddress != IntPtr.Zero)
       {
           return true; // If already calculated, immediately return true.
       }
   
       if (StoredOcelotAddress == IntPtr.Zero && TryFindOcelotDynamicAddress(out IntPtr dynamicAddress))
       {
           StoredOcelotAddress = dynamicAddress;
       }
   
       if (StoredOcelotAddress != IntPtr.Zero)
       {
           CalculateOcelotAddress(StoredOcelotAddress, 916, "Health");
           CalculateOcelotAddress(StoredOcelotAddress, 908, "Stamina");
       }
   
       // If not found, try the backup method
       if (OcelotHealthAddress == IntPtr.Zero)
       {
           DynamicBackupToFindAndStoreOcelotAOB();
       }
   
       return OcelotHealthAddress != IntPtr.Zero;
   }
   
    // This essentially looks for an AOB near a pointer address and then calculates the distance
    // between the AOB and the pointer address and then the function after accesses the pointer
    // this saves the program taking forever to find the AOB within 30 million potential addresses
    
   private bool TryFindOcelotDynamicAddress(out IntPtr dynamicAddress)
   {
       LoggingManager.Instance.Log("Attempting to access the Dynamic Address.");
       dynamicAddress = IntPtr.Zero;
       var process = MemoryManager.GetMGS3Process();
       if (process == null)
       {
           LoggingManager.Instance.Log("MGS3 process not found.");
           return false;
       }
   
       IntPtr baseAddress = IntPtr.Zero;
       foreach (ProcessModule module in process.Modules)
       {
           if (module.ModuleName.Equals("METAL GEAR SOLID3.exe", StringComparison.OrdinalIgnoreCase))
           {
               baseAddress = module.BaseAddress;
               LoggingManager.Instance.Log($"METAL GEAR SOLID3.exe module found at: 0x{baseAddress.ToString("X")}");
               break;
           }
       }
   
       if (baseAddress == IntPtr.Zero)
       {
           LoggingManager.Instance.Log("METAL GEAR SOLID3.exe module not found.");
           LoggingManager.Instance.Log("Failed to find base address for scanning.");
           return false;
       }
   
       IntPtr processHandle = MemoryManager.OpenGameProcess(process);
       if (processHandle == IntPtr.Zero)
       {
           LoggingManager.Instance.Log("Failed to open process for scanning.");
           return false;
       }
   
       // Unique AOB pattern near the pointer address the address isn't always the same
       // so we use wildcards (?) for the bytes that change this way it can determine
       // what is actually 0x00 and what is a wildcard
       byte[] pattern = new byte[] { 0xC0, 0x37, 0x00, 0x00, 0x00, 0x7F, 0x00, 0x00, 0x20, 0x11, 0x00, 0x00, 0x00, 0x7F };
       
       string mask = "xx???xxxxx???x";
       IntPtr startAddress = IntPtr.Add(baseAddress, 0x1000000);
       long size = 0x3000000 - 0x1000000;
       IntPtr foundAddress = MemoryManager.Instance.ScanMemory(processHandle, startAddress, size, pattern, mask);
   
       if (foundAddress != IntPtr.Zero)
       {
           dynamicAddress = IntPtr.Subtract(foundAddress, 848);
           LoggingManager.Instance.Log($"Dynamic AOB address for Ocelot is: 0x{dynamicAddress.ToString("X")}");
           NativeMethods.CloseHandle(processHandle);
           return true;
       }
       else
       {
           LoggingManager.Instance.Log("AOB not found within specified range.");
           LoggingManager.Instance.Log($"First address searched: 0x{startAddress.ToString("X")}, Last address searched: 0x{size.ToString("X")}");
           NativeMethods.CloseHandle(processHandle);
           return false;
       }
   }
   
   private void CalculateOcelotAddress(IntPtr dynamicAddress, int offset, string addressType)
   {
       var process = MemoryManager.GetMGS3Process();
       if (process == null)
       {
           LoggingManager.Instance.Log("MGS3 process not found.");
           return;
       }
   
       if (dynamicAddress == IntPtr.Zero)
       {
           LoggingManager.Instance.Log("Dynamic address not set. Run AOB scan first.");
           return;
       }
   
       IntPtr processHandle = MemoryManager.OpenGameProcess(process);
       if (processHandle == IntPtr.Zero)
       {
           LoggingManager.Instance.Log("Failed to open process for scanning.");
           return;
       }
   
       IntPtr pointerAddress = MemoryManager.Instance.ReadIntPtr(processHandle, dynamicAddress);
       if (pointerAddress == IntPtr.Zero)
       {
           LoggingManager.Instance.Log("Failed to read pointer from dynamic address.");
           NativeMethods.CloseHandle(processHandle);
           return;
       }
       // This is the offset the pointer in Cheat Engine is using at the address
       IntPtr targetAddress = IntPtr.Add(pointerAddress, 0x5DC);
       LoggingManager.Instance.Log($"Target address is: 0x{targetAddress.ToString("X")}");
   
       IntPtr calculatedAddress = IntPtr.Subtract(targetAddress, offset);
       LoggingManager.Instance.Log($"{addressType} Address calculated and set: 0x{calculatedAddress.ToString("X")}");
   
       short value = ReadShortFromMemory(processHandle, calculatedAddress);
   
       if (value != -1)
       {
           LoggingManager.Instance.Log($"Short value at adjusted address (0x{calculatedAddress.ToString("X")}) is: {value}");
       }
       else
       {
           LoggingManager.Instance.Log($"Failed to read short value from adjusted address for {addressType}.");
       }
   
       NativeMethods.CloseHandle(processHandle);
   
       if (addressType == "Health")
       {
           OcelotHealthAddress = calculatedAddress;
       }
       else if (addressType == "Stamina")
       {
           OcelotStaminaAddress = calculatedAddress;
       }
   }
   
   // Backup way to find the AOB incase the user isn't on the latest version of the game
   public bool DynamicBackupToFindAndStoreOcelotAOB()
   {
       var process = GetMGS3Process();
       if (process == null)
       {
   
           return false;
       }
   
       IntPtr processHandle = OpenGameProcess(process);
       if (processHandle == IntPtr.Zero)
       {
   
           return false;
       }
   
       var (pattern, mask) = Constants.AOBs["Ocelot"];
       IntPtr startAddress = new IntPtr(0x10FFFFFFFFF); // Example start range
       IntPtr endAddress = new IntPtr(0x30000000000); // Example end range
       long size = endAddress.ToInt64() - startAddress.ToInt64();
   
       IntPtr foundAddress = MemoryManager.Instance.ScanWideMemory(processHandle, startAddress, size, pattern, mask);
       NativeMethods.CloseHandle(processHandle);
   
       if (foundAddress != IntPtr.Zero)
       {
           FoundOcelotAddress = foundAddress; // Store found address
           return true;
       }
   
       return false;
   }
   
   
   // This will work on The Fear, Pain and Volgin if our pointer way of doing things doesn't work
   public bool DynamicBackupToFindAndStoreTheFearAOB()
   {
       var process = GetMGS3Process();
       if (process == null)
       {
   
           return false;
       }
   
       IntPtr processHandle = OpenGameProcess(process);
       if (processHandle == IntPtr.Zero)
       {
   
           return false;
       }
   
       var (pattern, mask) = Constants.AOBs["TheFearAOB"];
       IntPtr startAddress = new IntPtr(0x10FFFFFFFFF); // Example start range
       IntPtr endAddress = new IntPtr(0x30000000000); // Example end range
       long size = endAddress.ToInt64() - startAddress.ToInt64();
   
       IntPtr foundAddress = MemoryManager.Instance.ScanWideMemory(processHandle, startAddress, size, pattern, mask);
       NativeMethods.CloseHandle(processHandle);
   
       if (foundAddress != IntPtr.Zero)
       {
           FoundTheFearAddress = foundAddress; // Store found address
           return true;
       }
   
       return false;
   }
   #endregion
   */
