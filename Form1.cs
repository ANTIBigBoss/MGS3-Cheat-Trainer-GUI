using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class Form1 : Form
    {
        // Same constants and fields from the CLI version
        const string PROCESS_NAME = "METAL GEAR SOLID3";
        static IntPtr PROCESS_BASE_ADDRESS = IntPtr.Zero;
        static readonly IntPtr[] WEAPON_AMMO_OFFSETS = new IntPtr[]
        {
            // Weapons With Surpressors/Ammo/Clip Options
            (IntPtr)0x1D425DC,  // MK22
            (IntPtr)0x1D4262C,  // M1911A1         
            (IntPtr)0x1D427BC,  // XM16E1

            // Weapons with Ammo/Clip Options
            (IntPtr)0x1D4280C,  // AK47
            (IntPtr)0x1D428FC,  // SVD
            (IntPtr)0x1D428AC,  // M37
            (IntPtr)0x1D4299C,  // RPG7
            (IntPtr)0x1D4285C,  // M63
            (IntPtr)0x1D4276C,  // Scorpion
            (IntPtr)0x1D4294C,  // Mosin
            (IntPtr)0x1D426CC,  // SAA
            
            // Weapons with Ammo Options
            (IntPtr)0x1D4253C, // Cigspray
            (IntPtr)0x1D4258C, // Handkerchief
            (IntPtr)0x1D42A3C, // Greande
            (IntPtr)0x1D42A8C, // WpGrenade
            (IntPtr)0x1D42B2C, // ChaffGrenade
            (IntPtr)0x1D42B7C, // SmokeGrenade
            (IntPtr)0x1D42ADC, // StunGrenade
            (IntPtr)0x1D42BCC, // EmptyMagazine
            (IntPtr)0x1D42D0E, // Book
            (IntPtr)0x1D42CBC, // Claymore
            (IntPtr)0x1D42C1C, // TNT
            (IntPtr)0x1D42C6C, // C3
            (IntPtr)0x1D42D5C, // Mousetrap

            //Weapons with a True or False value Decimal 1 = on -1 = off
            (IntPtr)0x1D4271C, //PATRIOT
            (IntPtr)0x1D4267C, //EZGUN
            (IntPtr)0x1D4249C, //SURVIVAL KNIFE
            (IntPtr)0x1D424EC, //FORK
            (IntPtr)0x1D429EC, //TORCH
            (IntPtr)0x1D42DAC, //DIRECTIONAL_MIC
     
        };

        static readonly IntPtr[] WEAPON_MAX_AMMO_OFFSETS = new IntPtr[]
            {
            (IntPtr)0x1D425DE,  // MK22
            (IntPtr)0x1D4262E,  // M1911A1         
            (IntPtr)0x1D427BE,  // XM16E1

            // Weapons with Ammo/Clip Options
            (IntPtr)0x1D4280E,  // AK47
            (IntPtr)0x1D428FE,  // SVD
            (IntPtr)0x1D428AE,  // M37
            (IntPtr)0x1D4299E,  // RPG7
            (IntPtr)0x1D4285E,  // M63
            (IntPtr)0x1D4276E,  // Scorpion
            (IntPtr)0x1D4294E,  // Mosin
            (IntPtr)0x1D426CE,  // SAA
            
            // Weapons with Ammo Options
            (IntPtr)0x1D4253E, // Cigspray
            (IntPtr)0x1D4258E, // Handkerchief
            (IntPtr)0x1D42A3E, // Greande
            (IntPtr)0x1D42AA6, // WpGrenade
            (IntPtr)0x1D42B2E, // ChaffGrenade
            (IntPtr)0x1D42B7E, // SmokeGrenade
            (IntPtr)0x1D42ADE, // StunGrenade
            (IntPtr)0x1D42BCE, // EmptyMagazine
            (IntPtr)0x1D42D0E, // Book
            (IntPtr)0x1D42CBE, // Claymore
            (IntPtr)0x1D42C1E, // TNT
            (IntPtr)0x1D42C6E, // C3
            (IntPtr)0x1D42D5E, // Mousetrap


            };

        static readonly IntPtr[] WEAPON_CLIP_OFFSETS = new IntPtr[]
            {
            (IntPtr)0x1D425E0,  // MK22
            (IntPtr)0x1D42630,  // M1911A1         
            (IntPtr)0x1D427C0,  // XM16E1

            // Weapons with Ammo/Clip Options
            (IntPtr)0x1D42810,  // AK47
            (IntPtr)0x1D42900,  // SVD
            (IntPtr)0x1D428B0,  // M37
            (IntPtr)0x1D429A0,  // RPG7
            (IntPtr)0x1D42860,  // M63
            (IntPtr)0x1D42770,  // Scorpion
            (IntPtr)0x1D42950,  // Mosin
            (IntPtr)0x1D426D0,  // SAA
            };

        static readonly IntPtr[] WEAPON_MAX_CLIP_OFFSETS = new IntPtr[]
        {
            (IntPtr)0x1D425E2,  // MK
            (IntPtr)0x1D42632,  // M1911A1         
            (IntPtr)0x1D427C2,  // XM16E1

            // Weapons with Ammo/Clip Options
            (IntPtr)0x1D42812,  // AK47
            (IntPtr)0x1D42902,  // SVD
            (IntPtr)0x1D428B2,  // M37
            (IntPtr)0x1D429A2,  // RPG7
            (IntPtr)0x1D42862,  // M63
            (IntPtr)0x1D42772,  // Scorpion
            (IntPtr)0x1D42952,  // Mosin
            (IntPtr)0x1D426D2,  // SAA
            };


        static readonly string[] WEAPON_NAMES = new string[]
        {
            // Weapons With Suppressors/Ammo/Clip Options
            "MK22",            // Name for MK.22
            "M1911A1",       // Name for M1911A1
            "XM16E1",        // Name for XM16E1

            // Weapons with Ammo/Clip Options
            "AK47",          // Name for AK47
            "SVD",           // Name for SVD
            "M37",           // Name for M37
            "RPG7",          // Name for RPG7
            "M63",           // Name for M63
            "Scorpion",      // Name for SCORPION
            "Mosin",         // Name for Mosin
            "SAA",           // Name for SAA

            // Weapons with Ammo Options
            "Cigspray",      // Name for CIGSPRAY
            "Hankerchief",   // Name for HANKERCHIEF
            "Grenade",       // Name for GRENADE
            "Wp Greande",     // Name for WP_GRENADE
            "Chaff Grenade",  // Name for CHAFF_GRENADE
            "Smoke Grenade",  // Name for SMOKE_GRENADE
            "Stun Grenade",   // Name for STUN_GRENADE
            "Empty Magazine", // Name for EMPTY_MAGAZINE
            "Book",          // Name for BOOK
            "Claymore",      // Name for CLAYMORE
            "TNT",           // Name for TNT
            "C3",            // Name for C3
            "Mousetrap",     // Name for MOUSETRAP

            // Weapons with a True or False value Decimal 1 = on -1 = off
            "Patriot",       // Name for PATRIOT
            "Ez Gun",         // Name for EZGUN
            "Survival Knife",// Name for SURVIVAL KNIFE
            "Fork",          // Name for FORK
            "Torch",         // Name for TORCH
            "Directional Microphone",// Name for DIRECTIONAL_MIC    
        };

        static readonly IntPtr[] SURPRESSOR_TOGGLE = new IntPtr[]

            { // Value of 16 = on 0 = off
            (IntPtr)0x1D425EC,  // MK
            (IntPtr)0x1D4263C,  // M1911A1         
            (IntPtr)0x1D427CC,  // XM16E1
            };

        static readonly IntPtr[] SURPRESSOR_CAPACITY = new IntPtr[]
        { // We should implement this in a way that increments a value of 30 each way as that is how much a surpressor can hold
            (IntPtr)0x1D4596C,  // MK
            (IntPtr)0x1D4591C,  // M1911A1         
            (IntPtr)0x1D459BC,  // XM16E1
        };

        // PInvoke declarations
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref short lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]

        public static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        out short lpBuffer,
        uint size,
        out int lpNumberOfBytesRead);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MK22Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(0, MK22TextBox.Text); // Index 0 for MK22

        }

        private void M1911A1Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(1, M1911A1TextBox.Text); // Index 1 for M1911A1
        }

        private void XM16E1Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(2, XM16E1TextBox.Text); // Index 2 for XM16E1
        }

        private void AK47Button_Click_1(object sender, EventArgs e)
        {
            ModifyAmmo(3, AK47TextBox.Text); // Index 3 for AK47
        }

        private void SVDButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(4, SVDTextBox.Text); // Index 4 for SVD
        }

        private void M37Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(5, M37TextBox.Text); // Index 5 for M37
        }

        private void RPG7Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(6, RPG7TextBox.Text); // Index 6 for RPG7
        }

        private void M63Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(7, M63TextBox.Text); // Index 7 for M63
        }

        private void ScorpionButton_Click_1(object sender, EventArgs e)
        {
            ModifyAmmo(8, ScorpionTextBox.Text); // Index 8 for SCORPION
        }

        private void MosinButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(9, MosinTextBox.Text);
        }

        private void SAAButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(10, SAATextBox.Text); // Index 9 for SAA
        }

        private void CigSprayButton_Click_1(object sender, EventArgs e)
        {
            ModifyAmmo(11, CigSprayTextBox.Text); // Index 8 for CIGSPRAY
        }

        private void HandkerchiefButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(12, HandkerchiefTextBox.Text); // Index 9 for HANKERCHIEF
        }

        private void GrenadeButton_Click_1(object sender, EventArgs e)
        {
            ModifyAmmo(13, GrenadeTextBox.Text); // Index 10 for GRENADE
        }

        private void WpGrenadeButton_Click_1(object sender, EventArgs e)
        {
            ModifyAmmo(14, WpGrenadeTextBox.Text); // Index 11 for WP_GRENADE
        }

        private void ChaffGrenadeButton_Click_1(object sender, EventArgs e)
        {
            ModifyAmmo(15, ChaffGrenadeTextBox.Text); // Index 12 for CHAFF_GRENADE
        }

        private void SmokeGrenadeButton_Click_1(object sender, EventArgs e)
        {
            ModifyAmmo(16, SmokeGrenadeTextBox.Text); // Index 13 for SMOKE_GRENADE
        }

        private void StunGrenadeButton_Click_1(object sender, EventArgs e)
        {
            ModifyAmmo(17, StunGrenadeTextBox.Text); // Index 14 for STUN_GRENADE
        }

        private void MagazineButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(18, MagazineTextBox.Text); // Index 15 for EMPTY_MAGAZINE
        }

        private void BookButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(19, BookTextBox.Text); // Index 16 for BOOK
        }

        private void ClaymoreButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(20, ClaymoreTextBox.Text); // Index 17 for CLAYMORE
        }

        private void TNTButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(21, TNTTextBox.Text); // Index 18 for TNT
        }

        private void C3Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(22, C3TextBox.Text); // Index 19 for C3
        }

        private void MousetrapButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(23, MousetrapTextbox.Text); // Index 20 for MOUSETRAP
        }

        private void AddPatriot_Click(object sender, EventArgs e)
        {
            ToggleItemState("Patriot", true);
        }

        private void RemovePatriot_Click(object sender, EventArgs e)
        {
            ToggleItemState("Patriot", false);
        }

        private void AddEz_Click(object sender, EventArgs e)
        {
            ToggleItemState("Ez Gun", true);
        }

        private void RemoveEz_Click(object sender, EventArgs e)
        {
            ToggleItemState("Ez Gun", false);
        }

        private void AddKnife_Click(object sender, EventArgs e)
        {
            ToggleItemState("Survival Knife", true);
        }

        private void RemoveKnife_Click(object sender, EventArgs e)
        {
            ToggleItemState("Survival Knife", false);
        }

        private void AddFork_Click(object sender, EventArgs e)
        {
            ToggleItemState("Fork", true);
        }

        private void RemoveFork_Click(object sender, EventArgs e)
        {
            ToggleItemState("Fork", false);
        }

        private void AddTorch_Click(object sender, EventArgs e)
        {
            ToggleItemState("Torch", true);
        }

        private void RemoveTorch_Click(object sender, EventArgs e)
        {
            ToggleItemState("Torch", false);
        }

        private void AddDMic_Click(object sender, EventArgs e)
        {
            ToggleItemState("Directional Microphone", true);
        }

        private void RemoveDMic_Click(object sender, EventArgs e)
        {
            ToggleItemState("Directional Microphone", false);
        }

        private void MK22Clip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(0, MK22TextBox.Text);
        }

        private void M1911A1Clip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(1, M1911A1TextBox.Text);
        }

        private void XM16E1Clip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(2, XM16E1TextBox.Text);
        }

        private void AK47Clip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(3, AK47TextBox.Text);
        }

        private void SVDClip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(4, SVDTextBox.Text);
        }

        private void M37Clip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(5, M37TextBox.Text);
        }

        private void RPG7Clip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(6, RPG7TextBox.Text);
        }

        private void M63Clip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(7, M63TextBox.Text);
        }

        private void ScorpionClip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(8, ScorpionTextBox.Text);
        }

        private void MosinClip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(9, MosinTextBox.Text);
        }

        private void SAAClip_Click(object sender, EventArgs e)
        {
            ModifyClipSize(10, SAATextBox.Text);
        }

        private void ModifyAmmo(int weaponIndex, string ammoCountStr)
        {
            if (!short.TryParse(ammoCountStr, out short ammoCount))
            {
                MessageBox.Show("Invalid ammo count. Please enter a valid number.");
                return;
            }

            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = OpenProcess(0x1F0FFF, false, process.Id);

            // Modify current ammo
            IntPtr currentAmmoAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)WEAPON_AMMO_OFFSETS[weaponIndex]);
            int bytesWritten;
            WriteProcessMemory(processHandle, currentAmmoAddress, ref ammoCount, sizeof(short), out bytesWritten);

            // Modify max ammo
            IntPtr maxAmmoAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)WEAPON_MAX_AMMO_OFFSETS[weaponIndex]);
            WriteProcessMemory(processHandle, maxAmmoAddress, ref ammoCount, sizeof(short), out bytesWritten);

            if (bytesWritten > 0)
                MessageBox.Show($"Ammo for {WEAPON_NAMES[weaponIndex]} set to {ammoCount}!");
            else
                MessageBox.Show("Failed to write memory.");

            CloseHandle(processHandle);
        }

        private void ModifyClipSize(int weaponIndex, string clipSizeStr)
        {
            if (!short.TryParse(clipSizeStr, out short clipSize))
            {
                MessageBox.Show("Invalid clip size. Please enter a valid number.");
                return;
            }

            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = OpenProcess(0x1F0FFF, false, process.Id);

            // Modify current clip size
            IntPtr currentClipAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)WEAPON_CLIP_OFFSETS[weaponIndex]);
            int bytesWritten;
            WriteProcessMemory(processHandle, currentClipAddress, ref clipSize, sizeof(short), out bytesWritten);

            // Modify max clip size
            IntPtr maxClipAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)WEAPON_MAX_CLIP_OFFSETS[weaponIndex]);
            WriteProcessMemory(processHandle, maxClipAddress, ref clipSize, sizeof(short), out bytesWritten);

            if (bytesWritten > 0)
                MessageBox.Show($"Clip size for {WEAPON_NAMES[weaponIndex]} set to {clipSize}.");
            else
                MessageBox.Show("Failed to write memory.");

            CloseHandle(processHandle);
        }

        private void WriteValueToMemory(int index, short value)
        {
            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = OpenProcess(0x1F0FFF, false, process.Id);

            IntPtr address = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)WEAPON_AMMO_OFFSETS[index]);
            int bytesWritten;

            bool success = WriteProcessMemory(processHandle, address, ref value, sizeof(short), out bytesWritten);
            if (success && bytesWritten > 0)
            {
                string action = value == 1 ? "added" : "removed";
                MessageBox.Show($"Weapon {WEAPON_NAMES[index]} has been {action}.");
            }
            else
            {
                MessageBox.Show("Failed to write memory.");
            }

            CloseHandle(processHandle);
        }

        private void ToggleItemState(string itemName, bool addItem)
        {
            int itemIndex = Array.IndexOf(WEAPON_NAMES, itemName);

            if (itemIndex != -1)
            {
                short valueToWrite = addItem ? (short)1 : (short)-1;
                WriteValueToMemory(itemIndex, valueToWrite);
            }
            else
            {
                MessageBox.Show($"{itemName} not found.");
            }
        }
        private short? ReadValueFromMemory(IntPtr address)
        {
            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return null; // Use null to indicate an error
            }

            var processHandle = OpenProcess(0x1F0FFF, false, process.Id);
            bool success = ReadProcessMemory(processHandle, address, out short value, sizeof(short), out int bytesRead);
            CloseHandle(processHandle);

            if (success && bytesRead == sizeof(short))
            {
                return value;
            }
            else
            {
                MessageBox.Show("Failed to read memory.");
                return null; // Use null to indicate an error
            }
        }

        private void ToggleSuppressor(string weaponName)
        {
            int weaponIndex = Array.IndexOf(WEAPON_NAMES, weaponName);
            if (weaponIndex == -1)
            {
                MessageBox.Show($"{weaponName} not found.");
                return;
            }

            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = OpenProcess(0x1F0FFF, false, process.Id);
            if (processHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open process for reading/writing.");
                return;
            }

            IntPtr suppressorAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)SURPRESSOR_TOGGLE[weaponIndex]);
            if (!ReadWriteToggleValue(processHandle, suppressorAddress))
            {
                MessageBox.Show("Failed to toggle suppressor.");
            }

            CloseHandle(processHandle);
        }

        private bool ReadWriteToggleValue(IntPtr processHandle, IntPtr address)
        {
            bool success = ReadProcessMemory(processHandle, address, out short currentValue, sizeof(short), out int bytesRead);
            if (!success || bytesRead != sizeof(short))
            {
                return false;
            }

            short valueToWrite = (currentValue == 16) ? (short)0 : (short)16;
            success = WriteProcessMemory(processHandle, address, ref valueToWrite, sizeof(short), out int bytesWritten);
            return success && bytesWritten == sizeof(short);
        }



        private void WriteValueToMemory(IntPtr address, short value)
        {
            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = OpenProcess(0x1F0FFF, false, process.Id);

            int bytesWritten;
            bool success = WriteProcessMemory(processHandle, address, ref value, sizeof(short), out bytesWritten);
            if (success && bytesWritten > 0)
            {
                MessageBox.Show($"Suppressor has been {(value == 16 ? "enabled" : "disabled")}.");
            }
            else
            {
                MessageBox.Show("Failed to write memory.");
            }

            CloseHandle(processHandle);
        }

        private void AdjustSuppressorCapacity(string weaponName, bool increase)
        {
            int weaponIndex = Array.IndexOf(WEAPON_NAMES, weaponName);
            if (weaponIndex == -1)
            {
                MessageBox.Show($"{weaponName} not found.");
                return;
            }

            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = OpenProcess(0x1F0FFF, false, process.Id);
            if (processHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open process for reading/writing.");
                return;
            }

            IntPtr suppressorCapacityAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)SURPRESSOR_CAPACITY[weaponIndex]);
            short currentValue = ReadShortFromMemory(processHandle, suppressorCapacityAddress);

            // Ensure the value stays within ushort bounds
            ushort newValue = (ushort)(increase ? Math.Min((ushort)currentValue + 30, ushort.MaxValue) : Math.Max((ushort)currentValue - 30, ushort.MinValue));
            if (newValue == currentValue)
            {
                MessageBox.Show(increase ? "Suppressor capacity is already at maximum." : "Suppressor capacity is already at minimum.");
            }
            else
            {
                WriteShortToMemory(processHandle, suppressorCapacityAddress, (short)newValue);
                MessageBox.Show($"Suppressor capacity for {weaponName} set to {newValue}.");
            }

            CloseHandle(processHandle);
        }

        private short ReadShortFromMemory(IntPtr processHandle, IntPtr address)
        {
            ReadProcessMemory(processHandle, address, out short value, sizeof(short), out int bytesRead);
            return value;
        }

        private void WriteShortToMemory(IntPtr processHandle, IntPtr address, short value)
        {
            WriteProcessMemory(processHandle, address, ref value, sizeof(short), out int bytesWritten);
        }



        private void button12_Click(object sender, EventArgs e)
        {
            ToggleSuppressor("MK22");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            ToggleSuppressor("M1911A1");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            ToggleSuppressor("XM16E1");
        }

        private void Minus30MK22_Click(object sender, EventArgs e)
        {
            AdjustSuppressorCapacity("MK22", false);

        }

        private void Plus30MK22_Click(object sender, EventArgs e)
        {
            AdjustSuppressorCapacity("MK22", true);
        }

        private void Minus30M1911A1_Click(object sender, EventArgs e)
        {
            AdjustSuppressorCapacity("M1911A1", false);
        }

        private void Plus30M1911A1_Click(object sender, EventArgs e)
        {
            AdjustSuppressorCapacity("M1911A1", true);
        }

        private void Minus30XM16E1_Click(object sender, EventArgs e)
        {
            AdjustSuppressorCapacity("XM16E1", false);
        }

        private void Plus30XM16E1_Click(object sender, EventArgs e)
        {
            AdjustSuppressorCapacity("XM16E1", true);
        }
    }
}

//static readonly IntPtr[] ITEM_OFFSETS = new IntPtr[]
// {
//(IntPtr)0x, // ACTIVE_SONAR
//(IntPtr)0x, // ANTI_PERSONNEL_SENSOR
//(IntPtr)0x, // BINOCULARS
//(IntPtr)0x, // BUG_JUICE
//(IntPtr)0x, // CAMERA
//(IntPtr)0x, // CBOX_A
//(IntPtr)0x, // CBOX_B
//(IntPtr)0x, // CBOX_C
//(IntPtr)0x, // CIGAR
//(IntPtr)0x, // CROC_CAP
//(IntPtr)0x, // FAKE_DEATH_PILL
//(IntPtr)0x, // KEY_A
//(IntPtr)0x, // KEY_B
//(IntPtr)0x, // KEY_C
//(IntPtr)0x, // LIFE_MEDICINE
//(IntPtr)0x, // MINE_DETECTOR
//(IntPtr)0x, // MOTION_DETECTOR
//(IntPtr)0x, // NIGHT_VISION_GOGGLES
//(IntPtr)0x, // REVIVAL_PILL
//(IntPtr)0x, // STEALTH_CAMO
//(IntPtr)0x, // THERMAL_GOGGLES
// };

//static readonly IntPtr[] MEDICAL_ITEM_OFFSETS = new IntPtr[]
//{
// Medicinal "L2": Items:
//(IntPtr)0x, // ANTIDOTE
//(IntPtr)0x, // COLD_MEDICINE
//(IntPtr)0x, // DIGESTIVE_MEDICINE
//(IntPtr)0x, // SERUM

// Surgical "R2": Items:
//(IntPtr)0x, // BANDAGE
//(IntPtr)0x, // CIGAR
//(IntPtr)0x, // DISINFECTANT
//(IntPtr)0x, // FORK
//(IntPtr)0x, // OINTMENT
//(IntPtr)0x, // SPLINT
//(IntPtr)0x, // SUTURE_KIT
//(IntPtr)0x, // STYPTIC
//(IntPtr)0x, // SURVIVAL_KNIFE
//};

//static readonly IntPtr[] SNAKES_STATS_OFFSETS = new IntPtr[]
//{
//(IntPtr)0x, // HEALTH
//(IntPtr)0x, // MAX_HEALTH
//(IntPtr)0x, // STAMINA
//(IntPtr)0x, // MAX_STAMINA
//(IntPtr)0x, // GRIP
//(IntPtr)0x, // OXYGEN_02
//(IntPtr)0x, // FACEPAINT
//(IntPtr)0x, // BODY_CAMO
//(IntPtr)0x1E2B0CC, // SNAKE_PUKE
//(IntPtr)0x, // SNAKE_SLEEP
//(IntPtr)0x, // SNAKE_COUGH
//(IntPtr)0x, // SNAKE_FIRE
// };

//static readonly IntPtr[] ALERT_STATUS_OFFSETS = new IntPtr[]
//{
//(IntPtr)0x, // ALERT_TRIGGER
//(IntPtr)0x, // ALERT_TIMER
//(IntPtr)0x, // EVASION_TRIGGER
//(IntPtr)0x, // EVASION_TIMER
//(IntPtr)0x, // CAUTION_TRIGGER
//(IntPtr)0x, // CAUTION_TIMER
//};

//static readonly string[] ITEM_NAMES = new string[]
//{
//"ACTIVE_SONAR",
//"ANTI_PERSONNEL_SENSOR",
//"BINOCULARS",
//"BUG_JUICE",
//"CAMERA",
//"CBOX_A",
//"CBOX_B",
//"CBOX_C",
//"CIGAR",
//"CROC_CAP",
//"FAKE_DEATH_PILL",
//"KEY_A",
//"KEY_B",
//"KEY_C",
//"LIFE_MEDICINE",
//"MINE_DETECTOR",
//"MOTION_DETECTOR",
//"NIGHT_VISION_GOGGLES",
//"REVIVAL_PILL",
//"STEALTH_CAMO",
//"THERMAL_GOGGLES",
// };

//static readonly string[] MEDICAL_ITEM_NAMES = new string[]
// {
//"ANTIDOTE",
//"COLD_MEDICINE",
//"DIGESTIVE_MEDICINE",
//"SERUM",
//"BANDAGE",
//"CIGAR",
//"DISINFECTANT",
//"FORK",
//"OINTMENT",
//"SPLINT",
//"SUTURE_KIT",
//"STYPTIC",
//"SURVIVAL_KNIFE",
//};

//static readonly string[] SNAKES_STATS_NAMES = new string[]
//{
//"HEALTH",
//"MAX_HEALTH",
//"STAMINA",
//"MAX_STAMINA",
//"GRIP",
//"OXYGEN 02",
//"FACEPAINT",
//"BODY_CAMO",
//"SNAKE_PUKE",
//"SNAKE_SLEEP",
//"SNAKE_COUGH",
//};

//static readonly string[] ALERT_STATUS_NAMES = new string[]
//{
//"ALERT_TRIGGER",
//"ALERT_TIMER",
//"EVASION_TRIGGER",
//"EVASION_TIMER",
//"CAUTION_TRIGGER",
//"CAUTION_TIMER",
//};

