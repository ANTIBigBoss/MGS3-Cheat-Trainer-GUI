// No usings needed?

namespace MGS3_MC_Cheat_Trainer
{
    public partial class CamoForm : Form
    {

        const string PROCESS_NAME = "METAL GEAR SOLID3";
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

        public CamoForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form3_FormClosing);
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void WeaponFormSwap_Click(object sender, EventArgs e)
        {
            WeaponForm form1 = new WeaponForm();
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ItemForm form2 = new ItemForm();
            form2.Show();
            this.Hide();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e) // Load form4
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        // We should do the same boolean logic for camo only difference is that 1 means camo is obtained and 0 means it is not
        private void WriteValueToMemory(int index, short value)
        {
            var process = System.Diagnostics.Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = OpenProcess(0x1F0FFF, false, process.Id);

            IntPtr address = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)CAMO_OFFSETS[index]);
            int bytesWritten;

            bool success = WriteProcessMemory(processHandle, address, ref value, sizeof(short), out bytesWritten);

            CloseHandle(processHandle);
        }

        private void ToggleItemState(string itemName, bool addItem)
        {
            int itemIndex = Array.IndexOf(CAMO_NAMES, itemName);

            if (itemIndex != -1)
            {
                short valueToWrite = addItem ? (short)1 : (short)0;
                WriteValueToMemory(itemIndex, valueToWrite);
            }
            else
            {
                MessageBox.Show($"{itemName} not found.");
            }
        }

        private void AddWoodland_Click(object sender, EventArgs e)
        {
            ToggleItemState("Woodland", true);
        }

        private void RemoveWoodland_Click(object sender, EventArgs e)
        {
            ToggleItemState("Woodland", false);
        }

        private void AddBlackPaint_Click(object sender, EventArgs e)
        {
            ToggleItemState("Black Paint", true);
        }

        private void RemoveBlackPaint_Click(object sender, EventArgs e)
        {
            ToggleItemState("Black Paint", false);
        }

        private void AddWater_Click(object sender, EventArgs e)
        {
            ToggleItemState("Water Paint", true);
        }

        private void RemoveWaterPaint_Click(object sender, EventArgs e)
        {
            ToggleItemState("Water Paint", false);
        }

        private void AddDesert_Click(object sender, EventArgs e)
        {
            ToggleItemState("Desert", true);
        }

        private void RemoveDesert_Click(object sender, EventArgs e)
        {
            ToggleItemState("Desert", false);
        }

        private void AddSplitterPaint_Click(object sender, EventArgs e)
        {
            ToggleItemState("Splitter Paint", true);
        }

        private void RemoveSplitterPaint_Click(object sender, EventArgs e)
        {
            ToggleItemState("Splitter Paint", false);
        }

        private void AddSnowPaint_Click(object sender, EventArgs e)
        {
            ToggleItemState("Snow Paint", true);
        }

        private void RemoveSnowPaint_Click(object sender, EventArgs e)
        {
            ToggleItemState("Snow Paint", false);
        }

        private void AddKabuki_Click(object sender, EventArgs e)
        {
            ToggleItemState("Kabuki", true);
        }

        private void RemoveKabuki_Click(object sender, EventArgs e)
        {
            ToggleItemState("Kabuki", false);
        }

        private void AddZombie_Click(object sender, EventArgs e)
        {
            ToggleItemState("Zombie", true);
        }

        private void RemoveZombie_Click(object sender, EventArgs e)
        {
            ToggleItemState("Zombie", false);
        }

        private void AddOyama_Click(object sender, EventArgs e)
        {
            ToggleItemState("Oyama", true);
        }

        private void RemoveOyama_Click(object sender, EventArgs e)
        {
            ToggleItemState("Oyama", false);
        }

        private void AddMask_Click(object sender, EventArgs e)
        {
            ToggleItemState("Mask", true);
        }

        private void RemoveMask_Click(object sender, EventArgs e)
        {
            ToggleItemState("Mask", false);
        }

        private void AddGreen_Click(object sender, EventArgs e)
        {
            ToggleItemState("Green", true);
        }

        private void RemoveGreen_Click(object sender, EventArgs e)
        {
            ToggleItemState("Green", false);
        }

        private void AddBrown_Click(object sender, EventArgs e)
        {
            ToggleItemState("Brown", true);
        }

        private void RemoveBrown_Click(object sender, EventArgs e)
        {
            ToggleItemState("Brown", false);
        }

        private void AddInfinity_Click(object sender, EventArgs e)
        {
            ToggleItemState("Infinity", true);
        }

        private void RemoveInfinity_Click(object sender, EventArgs e)
        {
            ToggleItemState("Infinity", false);
        }

        private void AddSovietUnion_Click(object sender, EventArgs e)
        {
            ToggleItemState("Soviet Union", true);
        }

        private void RemoveSovietUnion_Click(object sender, EventArgs e)
        {
            ToggleItemState("Soviet Union", false);
        }

        private void AddUK_Click(object sender, EventArgs e)
        {
            ToggleItemState("UK", true);
        }

        private void RemoveUK_Click(object sender, EventArgs e)
        {
            ToggleItemState("UK", false);
        }

        private void AddFrance_Click(object sender, EventArgs e)
        {
            ToggleItemState("France", true);
        }

        private void RemoveFrance_Click(object sender, EventArgs e)
        {
            ToggleItemState("France", false);
        }

        private void AddGermany_Click(object sender, EventArgs e)
        {
            ToggleItemState("Germany", true);
        }

        private void RemoveGermany_Click(object sender, EventArgs e)
        {
            ToggleItemState("Germany", false);
        }

        private void AddItaly_Click(object sender, EventArgs e)
        {
            ToggleItemState("Italy", true);
        }

        private void RemoveItaly_Click(object sender, EventArgs e)
        {
            ToggleItemState("Italy", false);
        }

        private void AddSpain_Click(object sender, EventArgs e)
        {
            ToggleItemState("Spain", true);
        }

        private void RemoveSpain_Click(object sender, EventArgs e)
        {
            ToggleItemState("Spain", false);
        }

        private void AddSweden_Click(object sender, EventArgs e)
        {
            ToggleItemState("Sweden", true);
        }

        private void RemoveSweden_Click(object sender, EventArgs e)
        {
            ToggleItemState("Sweden", false);
        }

        private void AddJapan_Click(object sender, EventArgs e)
        {
            ToggleItemState("Japan", true);
        }

        private void RemoveJapan_Click(object sender, EventArgs e)
        {
            ToggleItemState("Japan", false);
        }

        private void AddUSA_Click(object sender, EventArgs e)
        {
            ToggleItemState("USA", true);
        }

        private void RemoveUSA_Click(object sender, EventArgs e)
        {
            ToggleItemState("USA", false);
        }

        private void AddOliveDrab_Click(object sender, EventArgs e)
        {
            ToggleItemState("Olive Drab", true);
        }

        private void RemoveOliveDrab_Click(object sender, EventArgs e)
        {
            ToggleItemState("Olive Drab", false);
        }

        private void AddTigerStripe_Click(object sender, EventArgs e)
        {
            ToggleItemState("Tiger Stripe", true);
        }

        private void RemoveTigerStripe_Click(object sender, EventArgs e)
        {
            ToggleItemState("Tiger Stripe", false);
        }

        private void AddLeaf_Click(object sender, EventArgs e)
        {
            ToggleItemState("Leaf", true);
        }

        private void RemoveLeaf_Click(object sender, EventArgs e)
        {
            ToggleItemState("Leaf", false);
        }

        private void AddTreeBark_Click(object sender, EventArgs e)
        {
            ToggleItemState("Tree Bark", true);
        }

        private void RemoveTreeBark_Click(object sender, EventArgs e)
        {
            ToggleItemState("Tree Bark", false);
        }

        private void AddChocoChip_Click(object sender, EventArgs e)
        {
            ToggleItemState("Choco Chip", true);
        }

        private void RemoveChocoChip_Click(object sender, EventArgs e)
        {
            ToggleItemState("Choco Chip", false);
        }

        private void AddSplitterBody_Click(object sender, EventArgs e)
        {
            ToggleItemState("Splitter Uniform", true);
        }

        private void RemoveSplitterBody_Click(object sender, EventArgs e)
        {
            ToggleItemState("Splitter Uniform", false);
        }

        private void AddRaindrop_Click(object sender, EventArgs e)
        {
            ToggleItemState("Raindrop", true);
        }

        private void RemoveRaindrop_Click(object sender, EventArgs e)
        {
            ToggleItemState("Raindrop", false);
        }

        private void AddSquare_Click(object sender, EventArgs e)
        {
            ToggleItemState("Squares", true);
        }

        private void RemoveSquares_Click(object sender, EventArgs e)
        {
            ToggleItemState("Squares", false);
        }

        private void AddWaterBody_Click(object sender, EventArgs e)
        {
            ToggleItemState("Water Uniform", true);
        }

        private void RemoveWaterBody_Click(object sender, EventArgs e)
        {
            ToggleItemState("Water Uniform", false);
        }

        private void AddBlackBody_Click(object sender, EventArgs e)
        {
            ToggleItemState("Black Uniform", true);
        }

        private void RemoveBlackBody_Click(object sender, EventArgs e)
        {
            ToggleItemState("Black Uniform", false);
        }

        private void AddSnowBody_Click(object sender, EventArgs e)
        {
            ToggleItemState("Snow Uniform", true);
        }

        private void RemoveSnowBody_Click(object sender, EventArgs e)
        {
            ToggleItemState("Snow Uniform", false);
        }

        private void AddSneakingSuit_Click(object sender, EventArgs e)
        {
            ToggleItemState("Sneaking Suit", true);
        }

        private void RemoveSneakingSuit_Click(object sender, EventArgs e)
        {
            ToggleItemState("Sneaking Suit", false);
        }

        private void AddScientist_Click(object sender, EventArgs e)
        {
            ToggleItemState("Scientist", true);
        }

        private void RemoveScientist_Click(object sender, EventArgs e)
        {
            ToggleItemState("Scientist", false);
        }

        private void AddOfficer_Click(object sender, EventArgs e)
        {
            ToggleItemState("Officer", true);
        }

        private void RemoveOfficer_Click(object sender, EventArgs e)
        {
            ToggleItemState("Officer", false);
        }

        private void AddMaintenance_Click(object sender, EventArgs e)
        {
            ToggleItemState("Maintenance", true);
        }

        private void RemoveMaintenance_Click(object sender, EventArgs e)
        {
            ToggleItemState("Maintenance", false);
        }

        private void AddTuxedo_Click(object sender, EventArgs e)
        {
            ToggleItemState("Tuxedo", true);
        }

        private void RemoveTuxedo_Click(object sender, EventArgs e)
        {
            ToggleItemState("Tuxedo", false);
        }

        private void AddHornetStripe_Click(object sender, EventArgs e)
        {
            ToggleItemState("Hornet Stripe", true);
        }

        private void RemoveHornetStripe_Click(object sender, EventArgs e)
        {
            ToggleItemState("Hornet Stripe", false);
        }

        private void AddMoss_Click(object sender, EventArgs e)
        {
            ToggleItemState("Moss", true);
        }

        private void RemoveMoss_Click(object sender, EventArgs e)
        {
            ToggleItemState("Moss", false);
        }

        private void Addfire_Click(object sender, EventArgs e)
        {
            ToggleItemState("Fire", true);
        }

        private void RemoveFire_Click(object sender, EventArgs e)
        {
            ToggleItemState("Fire", false);
        }

        private void AddSpirit_Click(object sender, EventArgs e)
        {
            ToggleItemState("Spirit", true);
        }

        private void RemoveSpirit_Click(object sender, EventArgs e)
        {
            ToggleItemState("Spirit", false);
        }

        private void AddColdWar_Click(object sender, EventArgs e)
        {
            ToggleItemState("Cold War", true);
        }

        private void RemoveColdWar_Click(object sender, EventArgs e)
        {
            ToggleItemState("Cold War", false);
        }

        private void AddSnake_Click(object sender, EventArgs e)
        {
            ToggleItemState("Snake", true);
        }

        private void RemoveSnake_Click(object sender, EventArgs e)
        {
            ToggleItemState("Snake", false);
        }

        private void AddGaKo_Click(object sender, EventArgs e)
        {
            ToggleItemState("Ga-Ko", true);
        }

        private void RemoveGaKo_Click(object sender, EventArgs e)
        {
            ToggleItemState("Ga-Ko", false);
        }

        private void AddDesertTiger_Click(object sender, EventArgs e)
        {
            ToggleItemState("Desert Tiger", true);
        }

        private void RemoveDesertTiger_Click(object sender, EventArgs e)
        {
            ToggleItemState("Desert Tiger", false);
        }

        private void AddDPM_Click(object sender, EventArgs e)
        {
            ToggleItemState("DPM", true);
        }

        private void RemoveDPM_Click(object sender, EventArgs e)
        {
            ToggleItemState("DPM", false);
        }

        private void AddFlecktarn_Click(object sender, EventArgs e)
        {
            ToggleItemState("Flecktarn", true);
        }

        private void RemoveFlecktarn_Click(object sender, EventArgs e)
        {
            ToggleItemState("Flecktarn", false);
        }

        private void AddAuscam_Click(object sender, EventArgs e)
        {
            ToggleItemState("Auscam", true);
        }

        private void RemoveAuscam_Click(object sender, EventArgs e)
        {
            ToggleItemState("Auscam", false);
        }

        private void AddAnimals_Click(object sender, EventArgs e)
        {
            ToggleItemState("Animals", true);
        }

        private void RemoveAnimals_Click(object sender, EventArgs e)
        {
            ToggleItemState("Animals", false);
        }

        private void AddFly_Click(object sender, EventArgs e)
        {
            ToggleItemState("Fly", true);
        }

        private void RemoveFly_Click(object sender, EventArgs e)
        {
            ToggleItemState("Fly", false);
        }

        private void AddSpider_Click(object sender, EventArgs e)
        {
            ToggleItemState("Spider", true);
        }

        private void RemoveSpider_Click(object sender, EventArgs e)
        {
            ToggleItemState("Spider", false);
        }

        private void AddBanana_Click(object sender, EventArgs e)
        {
            ToggleItemState("Banana Camo", true);
        }

        private void RemoveBanana_Click(object sender, EventArgs e)
        {
            ToggleItemState("Banana Camo", false);
        }
    }
}