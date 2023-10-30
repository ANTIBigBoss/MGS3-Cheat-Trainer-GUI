using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class Form1 : Form
    {
        // Same constants and fields from the CLI version
        const string PROCESS_NAME = "METAL GEAR SOLID3";
        static IntPtr PROCESS_BASE_ADDRESS = IntPtr.Zero;
        static readonly IntPtr[] WEAPON_OFFSETS = new IntPtr[]
        {
            (IntPtr)0x1D425DC,  // MK
            (IntPtr)0x1D4262C,  // M1911A1
            (IntPtr)0x1D4280C,  // AK47
            (IntPtr)0x1D427BC,  // XM16E1
            (IntPtr)0x1D428FC,  // SVD
            (IntPtr)0x1D428AC,  // M37
            (IntPtr)0x1D4299C,  // RPG7
            (IntPtr)0x1D4285C,  // M63
            (IntPtr)0x1D4253C   // CIGSPRAY
        };

        static readonly string[] WEAPON_NAMES = new string[]
        {
            "MK",
            "M1911A1",
            "AK47",
            "XM16E1",
            "SVD",
            "M37",
            "RPG7",
            "M63",
            "CIGSPRAY"
        };

        // PInvoke declarations
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref short lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
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
            ModifyAmmo(0, MK22TextBox.Text);
        }

        private void M1911A1Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(1, M1911A1TextBox.Text);
        }

        /* Need to get image of the AK47 button
        
        private void AK47Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(2, AK47TextBox.Text);
        }
        */

        private void XM16E1Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(3, XM16E1TextBox.Text);
        }

        private void SVDButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(4, SVDTextBox.Text);
        }

        private void M37Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(5, M37TextBox.Text);
        }

        private void RPG7Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(6, RPG7TextBox.Text);
        }

        private void M63Button_Click(object sender, EventArgs e)
        {
            ModifyAmmo(7, M63TextBox.Text);
        }

        private void CIGSPRAYButton_Click(object sender, EventArgs e)
        {
            ModifyAmmo(8, CIGSPRAYTextBox.Text);
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

            IntPtr ammoAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)WEAPON_OFFSETS[weaponIndex]);

            int bytesWritten;
            WriteProcessMemory(processHandle, ammoAddress, ref ammoCount, sizeof(short), out bytesWritten);
            if (bytesWritten > 0)
                MessageBox.Show($"Ammo for {WEAPON_NAMES[weaponIndex]} set to {ammoCount}!");
            else
                MessageBox.Show("Failed to write memory.");

            CloseHandle(processHandle);
        }
    }
}