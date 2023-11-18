using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class WeaponForm : Form
    {
        // Same constants and fields from the CLI version
        static IntPtr PROCESS_BASE_ADDRESS = IntPtr.Zero;

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

        public WeaponForm()
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

        // Should move these in the future to where the other functions are too lazy atm
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

        // Function logics for navigating to other forms for items, camo and misc
        // Form2 is for items
        // Form3 is for camo
        // Form4 is for misc
        // Should rename these in the future for better clarity
        private void ItemFormSwap_Click(object sender, EventArgs e)
        {
            ItemForm form2 = new ItemForm();
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CamoForm form3 = new CamoForm();
            form3.Show();
            this.Hide();
        }

        private void button70_Click(object sender, EventArgs e) // Load form4
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }
    }
}