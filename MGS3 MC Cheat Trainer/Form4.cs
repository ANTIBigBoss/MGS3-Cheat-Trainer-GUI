using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class Form4 : Form
    {

        const string PROCESS_NAME = "METAL GEAR SOLID3";
        static IntPtr PROCESS_BASE_ADDRESS = IntPtr.Zero;
        IntPtr processHandle; // Ensure this is correctly initialized
        private const int HealthPointerOffset = 0x00AE49D8;
        private const int HealthOffset = 0x684;
        private const int MaxHealthOffset = 0x686;
        private const int StaminaOffset = 0xA4A;

        // PInvoke declarations
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref short lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]

        public static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        uint nSize,
        out int lpNumberOfBytesRead);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        private IntPtr ResolvePointerAddress(IntPtr baseAddress, IntPtr pointerOffset, IntPtr finalOffset)
        {
            byte[] buffer = new byte[IntPtr.Size]; // Corrected here
            ReadProcessMemory(processHandle, IntPtr.Add(baseAddress, (int)pointerOffset), buffer, (uint)buffer.Length, out _);

            IntPtr pointerAddress;
            if (IntPtr.Size == 8) // 64-bit
            {
                pointerAddress = (IntPtr)BitConverter.ToInt64(buffer, 0);
            }
            else // 32-bit
            {
                pointerAddress = (IntPtr)BitConverter.ToInt32(buffer, 0);
            }

            return IntPtr.Add(pointerAddress, (int)finalOffset);
        }



        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        uint nSize,
        out int lpNumberOfBytesWritten);

        public Form4()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);

            // Initialize these here
            IntPtr baseAddress = PROCESS_BASE_ADDRESS; // Assuming this is already set to the base address of the process
            IntPtr pointerOffset = (IntPtr)0x00AE49D8;
            IntPtr finalOffset = (IntPtr)0x684;
            IntPtr finalDataAddress = ResolvePointerAddress(baseAddress, pointerOffset, finalOffset);

            // Now you can read or write to finalDataAddress
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void WeaponFormSwap_Click(object sender, EventArgs e) // Weapon Form Swap
        {
            WeaponForm f1 = new WeaponForm();
            f1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) // Item Form Swap
        {
            ItemForm f2 = new ItemForm();
            f2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) // Camo Form Swap
        {
            CamoForm f3 = new CamoForm();
            f3.Show();
            this.Hide();
        }

        // Form Closing
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        static readonly IntPtr[] SNAKES_ANIMATIONS = new IntPtr[]
        {

            (IntPtr)0x1D4BCBA, // Value of 1 for a long sleep
            (IntPtr)0x1E2C0BB, // Value of 2 for a quick sleep
            (IntPtr)0x1E2C0BC, // Value of 1 for puke
            (IntPtr)0x1E2C0BC, // Value of 200 for fire
            (IntPtr)0x1E2C0BC, // Value of 255 for puke and fire
            (IntPtr)0x1E2C0C8, // Value of 3 for a bunny hop
            (IntPtr)0x1E2C0CA, // Value of 32 to fake death
        };

        static readonly IntPtr[] HUDandCAMERA = new IntPtr[]
        {
            // HUD
            (IntPtr)0xADB40F, // Value of 63 for normal HUD
            (IntPtr)0xADB40F, // Value of 64 to shrink HUD
            (IntPtr)0xADB40F, // Value of 0 to remove HUD

            // CAMERA
            (IntPtr)0xAE3B37, // Value of 191 for normal camera
            (IntPtr)0xAE3B37, // Value of 64 for upside down camera
            // For Camera and HUD I should find the floats for the values and see if I can make a slider for them

        };

        static readonly IntPtr[] ALERT_STATUS = new IntPtr[]
        {
            (IntPtr)0x1D9C3D8, // Value of 16 for ALERT trigger
            // Value of 128 during EVASION but not able to trigger on command just leaving here for dictionary for if I find a way to trigger it in the future
            (IntPtr)0x1D9C3D8,
            (IntPtr)0x1D9C3D8, // Value of 32 for CAUTION trigger
        };

        private void WriteByteToMemory(byte value, IntPtr[] addressArray, int arrayIndex)
        {
            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = OpenProcess(0x1F0FFF, false, process.Id);

            byte[] buffer = new byte[] { value }; // Value to write
            IntPtr targetAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)addressArray[arrayIndex]); // Adjusted to add base address
            int bytesWritten;

            bool success = WriteProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out bytesWritten);

            if (!success || bytesWritten != buffer.Length)
            {
                MessageBox.Show($"Failed to write memory at index {arrayIndex} with value {value}.");
            }

            CloseHandle(processHandle);
        }

        private void button3_Click(object sender, EventArgs e) // Alert Mode Trigger
        {
            WriteByteToMemory(16, ALERT_STATUS, 0); // 16 is the value for ALERT MODE, 0 is the index in ALERT_STATUS
        }

        private void button9_Click(object sender, EventArgs e) // Caution Mode Trigger
        {
            WriteByteToMemory(32, ALERT_STATUS, 2); // 32 is the value for CAUTION MODE, 2 is the index in ALERT_STATUS
        }

        private void SnakeNapQuick_Click(object sender, EventArgs e)
        {
            WriteByteToMemory(2, SNAKES_ANIMATIONS, 1);
        }

        private void SnakeLongNap_Click(object sender, EventArgs e)
        {
            WriteByteToMemory(1, SNAKES_ANIMATIONS, 0);
        }

        private void SnakeFakesDeath_Click(object sender, EventArgs e)
        {
            WriteByteToMemory(32, SNAKES_ANIMATIONS, 6);
        }

        private void SnakePukes_Click(object sender, EventArgs e)
        {
            WriteByteToMemory(1, SNAKES_ANIMATIONS, 2);
        }

        private void button12_Click(object sender, EventArgs e) // Snake on fire
        {
            WriteByteToMemory(200, SNAKES_ANIMATIONS, 3);
        }

        private void SnakePukeFire_Click(object sender, EventArgs e)
        {
            WriteByteToMemory(255, SNAKES_ANIMATIONS, 4);
        }

        private void button23_Click(object sender, EventArgs e) // Bunny hop
        {
            WriteByteToMemory(3, SNAKES_ANIMATIONS, 5);
        }

        private void NormalHUD_Click(object sender, EventArgs e)
        {
            WriteByteToMemory(63, HUDandCAMERA, 0);
        }

        private void ShrinkHUD_Click(object sender, EventArgs e)
        {
            WriteByteToMemory(64, HUDandCAMERA, 1);
        }

        private void NoHUD_Click(object sender, EventArgs e)
        {
            WriteByteToMemory(0, HUDandCAMERA, 2);
        }

        private void NormalCam_Click(object sender, EventArgs e)
        {
            WriteByteToMemory(191, HUDandCAMERA, 3);
        }

        private void UpsideDownCam_Click(object sender, EventArgs e)
        {
            WriteByteToMemory(64, HUDandCAMERA, 4);
        }

        // Health and Stamina along with pointer logic

        private enum HealthType
        {
            CurrentHealth,
            MaxHealth,
            Stamina
        }

        private void ModifyHealthOrStamina(int baseOffset, int valueOffset, int value, HealthType healthType, bool setExactValue = false)
        {
            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            var processHandle = OpenProcess(0x1F0FFF, false, process.Id);
            var baseAddress = process.MainModule.BaseAddress;

            IntPtr pointerBase = (IntPtr)baseOffset;
            IntPtr pointerAddress = IntPtr.Add(baseAddress, (int)pointerBase);
            byte[] pointerBuffer = new byte[IntPtr.Size];
            ReadProcessMemory(processHandle, pointerAddress, pointerBuffer, (uint)pointerBuffer.Length, out _);
            IntPtr valuePointer = (IntPtr.Size == 8) ? (IntPtr)BitConverter.ToInt64(pointerBuffer, 0) : (IntPtr)BitConverter.ToInt32(pointerBuffer, 0);

            // Adjust the valueOffset based on the healthType
            switch (healthType)
            {
                case HealthType.MaxHealth:
                    valueOffset = MaxHealthOffset;
                    break;
                case HealthType.Stamina:
                    valueOffset = StaminaOffset;
                    break;
                    // Default is CurrentHealth, so no change needed
            }

            IntPtr valueAddress = IntPtr.Add(valuePointer, valueOffset);
            byte[] valueBuffer = new byte[sizeof(short)];
            ReadProcessMemory(processHandle, valueAddress, valueBuffer, (uint)valueBuffer.Length, out _);
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
            WriteProcessMemory(processHandle, valueAddress, newValueBuffer, (uint)newValueBuffer.Length, out _);

            CloseHandle(processHandle);
        }



        private void Plus100HpValue_Click(object sender, EventArgs e)
        {
            ModifyHealthOrStamina(HealthPointerOffset, HealthOffset, 100, HealthType.CurrentHealth);
        }

        private void Minus100HpValue_Click(object sender, EventArgs e)
        {
            ModifyHealthOrStamina(HealthPointerOffset, HealthOffset, -100, HealthType.CurrentHealth);
        }

        private void CurrentHpTo1_Click(object sender, EventArgs e)
        {
            ModifyHealthOrStamina(HealthPointerOffset, HealthOffset, 1, HealthType.CurrentHealth, true);
        }

        private void MaxHpTo1_Click(object sender, EventArgs e)
        {

            ModifyHealthOrStamina(HealthPointerOffset, MaxHealthOffset, 1, HealthType.MaxHealth, true);
        }

        private void ZeroHP_Click(object sender, EventArgs e)
        {
            ModifyHealthOrStamina(HealthPointerOffset, HealthOffset, 0, HealthType.CurrentHealth, true);
        }

        private void SetStaminaToZero_Click(object sender, EventArgs e)
        {
            ModifyHealthOrStamina(HealthPointerOffset, StaminaOffset, 0, HealthType.Stamina, true);
        }

        // I thought 10000 was a bar at first but it's actually 7500 per bar 
        // so this function name is misleading LOL will fix in V2
        private void Plus10000StaminaValue_Click(object sender, EventArgs e)
        {
            ModifyHealthOrStamina(HealthPointerOffset, StaminaOffset, 7500, HealthType.Stamina);
        }

        private void Minus10000StaminaValue_Click(object sender, EventArgs e)
        {
            ModifyHealthOrStamina(HealthPointerOffset, StaminaOffset, -7500, HealthType.Stamina);
        }

        private void FullStamina30000Value_Click(object sender, EventArgs e)
        {
            ModifyHealthOrStamina(HealthPointerOffset, StaminaOffset, 30000, HealthType.Stamina, true);
        }

        private void Plus100MaxHpValue_Click(object sender, EventArgs e)
        {
            ModifyHealthOrStamina(HealthPointerOffset, MaxHealthOffset, 100, HealthType.MaxHealth);
        }

        private void Minus100MaxHpValue_Click(object sender, EventArgs e)
        {
            ModifyHealthOrStamina(HealthPointerOffset, MaxHealthOffset, -100, HealthType.MaxHealth);
        }
    }
}