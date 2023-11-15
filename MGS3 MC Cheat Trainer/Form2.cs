using System.Diagnostics;
using System.Runtime.InteropServices;


namespace MGS3_MC_Cheat_Trainer
{
    public partial class Form2 : Form
    {
        const string PROCESS_NAME = "METAL GEAR SOLID3";
        static IntPtr PROCESS_BASE_ADDRESS = IntPtr.Zero;

        // PInvoke declarations
        public static class NativeMethods
        {
            // Declare OpenProcess
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

            // Declare WriteProcessMemory
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref short lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

            // Declare ReadProcessMemory
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, out short lpBuffer, uint size, out int lpNumberOfBytesRead);

            // Declare CloseHandle
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool CloseHandle(IntPtr hObject);
        }

        // Going to index by items with a capacity and then ones with a boolean
        static readonly IntPtr[] ITEM_OFFSETS = new IntPtr[]
        {
        // Backpack Items:

        (IntPtr)0x1D45D7C, // LIFE_MEDICINE
        (IntPtr)0x1D45D7E, // LIFE_MEDICINE_MAX
        (IntPtr)0x1D464AC, // BUG_JUICE
        (IntPtr)0x1D464AE, // BUG_JUICE_MAX
        (IntPtr)0x1D45E1C, // FAKE_DEATH_PILL
        (IntPtr)0x1D45E1E, // FAKE_DEATH_PILL_MAX
        (IntPtr)0x1D45DCC, // PENTAZEMIN
        (IntPtr)0x1D45DCE, // PENTAZEMIN_MAX

        // Medicinal "L2" Items:

        (IntPtr)0x1D4659C, // ANTIDOTE
        (IntPtr)0x1D4659E, // ANTIDOTE_MAX
        (IntPtr)0x1D465EC, // COLD_MEDICINE
        (IntPtr)0x1D465EE, // COLD_MEDICINE_MAX
        (IntPtr)0x1D4663C, // DIGESTIVE_MEDICINE
        (IntPtr)0x1D4663E, // DIGESTIVE_MEDICINE_MAX
        (IntPtr)0x1D4654C, // SERUM
        (IntPtr)0x1D4654E, // SERUM_MAX

        // Surgical "R2": Items:
        (IntPtr)0x1D467CC, // BANDAGE
        (IntPtr)0x1D467CE, // BANDAGE_MAX
        (IntPtr)0x1D4672C, // DISINFECTANT
        (IntPtr)0x1D4672E, // DISINFECTANT_MAX
        (IntPtr)0x1D4668C, // OINTMENT
        (IntPtr)0x1D4668E, // OINTMENT_MAX
        (IntPtr)0x1D466DC, // SPLINT
        (IntPtr)0x1D466DE, // SPLINT_MAX
        (IntPtr)0x1D4677C, // STYPTIC
        (IntPtr)0x1D4677E, // STYPTIC_MAX
        (IntPtr)0x1D4681C, // SUTURE_KIT
        (IntPtr)0x1D4681E, // SUTURE_KIT_MAX

        // No Capcity will just have a true 1 or false -1 to give/remove
        (IntPtr)0x1D45E6C, // REVIVAL_PILL
        (IntPtr)0x1D45EBC, // CIGAR
        (IntPtr)0x1D45F0C, // BINOCULARS
        (IntPtr)0x1D45F5C, // THERMAL_GOGGLES
        (IntPtr)0x1D45FAC, // NIGHT_VISION_GOGGLES
        (IntPtr)0x1D45FFC, // CAMERA
        (IntPtr)0x1D4604C, // MOTION_DETECTOR
        (IntPtr)0x1D4609C, // ACTIVE_SONAR
        (IntPtr)0x1D460EC, // MINE_DETECTOR
        (IntPtr)0x1D4613C, // ANTI_PERSONNEL_SENSOR
        //Boxes have slightly different logic and should be set to 25 or -1 as they have a durability
        (IntPtr)0x1D4618C, // CBOX_A
        (IntPtr)0x1D461DC, // CBOX_B
        (IntPtr)0x1D4622C, // CBOX_C
        (IntPtr)0x1D4627C, // CBOX_D
        (IntPtr)0x1D462CC, // CROC_CAP
        (IntPtr)0x1D4631C, // KEY_A
        (IntPtr)0x1D4636C, // KEY_B
        (IntPtr)0x1D463BC, // KEY_C
        (IntPtr)0x1D4640C, // BANDANA
        (IntPtr)0x1D4645C, // STEALTH_CAMO
        (IntPtr)0x1D464FC, // MONKEY_MASK
        };



        static readonly string[] ITEM_NAMES = new string[]
        {
        // Items and Medicinal Items with a capacity
        "LIFE MEDICINE",
        "LIFE MEDICINE MAX",
        "BUG JUICE",
        "BUG JUICE MAX",
        "FAKE DEATH PILL",
        "FAKE DEATH PILL MAX",
        "PENTAZEMIN",
        "PENTAZEMIN MAX",
        // Medical Items
        "ANTIDOTE",
        "ANTIDOTE MAX",
        "COLD MEDICINE",
        "COLD MEDICINE MAX",
        "DIGESTIVE MEDICINE",
        "DIGESTIVE MEDICINE MAX",
        "SERUM",
        "SERUM MAX",
        "BANDAGE",
        "BANDAGE MAX",
        "DISINFECTANT",
        "DISINFECTANT MAX",
        "OINTMENT",
        "OINTMENT MAX",
        "SPLINT",
        "SPLINT MAX",
        "STYPTIC",
        "STYPTIC MAX",
        "SUTURE KIT",
        "SUTURE KIT MAX",
        // Items with a boolean
        "REVIVAL PILL",
        "CIGAR",
        "BINOCULARS",
        "THERMAL GOGGLES",
        "NIGHT VISION GOGGLES",
        "CAMERA",
        "MOTION DETECTOR",
        "ACTIVE_SONAR",
        "MINE DETECTOR",
        "ANTI PERSONNEL SENSOR",
        "CBOX A",
        "CBOX B",
        "CBOX C",
        "CBOX D",
        "CROC CAP",
        "KEY A",
        "KEY B",
        "KEY C",
        "BANDANA",
        "STEALTH CAMO",
        "MONKEY MASK",
         };

        //Navigation and closing logic functions
        public Form2()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form2_FormClosing);
        }

        private void WeaponFormSwap_Click(object sender, EventArgs e) // Form1
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e) // Form3
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button70_Click(object sender, EventArgs e) // Load form4
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Closes the application(form1) if this form is closed
        }
        // End of navigation and closing logic functions



        private void Form2_Load(object sender, EventArgs e) // Accidently added this lmao
        {

        }

        private void ModifyItemCapacity(int itemIndex, string itemCountStr)
        {
            if (!short.TryParse(itemCountStr, out short itemCount))
            {
                MessageBox.Show("Invalid item count. Please enter a valid number.");
                return;
            }

            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            // Modify current item count
            IntPtr currentItemAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)ITEM_OFFSETS[itemIndex]);
            int bytesWritten;
            NativeMethods.WriteProcessMemory(processHandle, currentItemAddress, ref itemCount, sizeof(short), out bytesWritten);

            // Modify max item count
            IntPtr maxItemAddress = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)ITEM_OFFSETS[itemIndex + 1]);
            NativeMethods.WriteProcessMemory(processHandle, maxItemAddress, ref itemCount, sizeof(short), out bytesWritten);

            NativeMethods.CloseHandle(processHandle);
        }


        private void AddLifeMed_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(0, LifeMedtextBox.Text);
        }


        private void AddBugJuice_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(2, BugJuicetextBox.Text);
        }

        private void AddFDP_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(4, FDPtextBox.Text);
        }

        private void AddPentazemin_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(6, PentazemintextBox.Text);
        }

        private void AddAntidote_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(8, AntidotetextBox.Text);
        }

        private void AddCMed_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(10, CMedtextBox.Text);
        }

        private void AddDMed_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(12, DMedtextBox.Text);
        }

        private void AddSerum_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(14, SerumtextBox.Text);
        }

        private void AddBandage_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(16, BandagetextBox.Text);
        }

        private void AddDisinfectant_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(18, DisinfectanttextBox.Text);
        }

        private void AddOintment_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(20, OintmenttextBox.Text);
        }

        private void AddSplint_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(22, SplinttextBox.Text);
        }

        private void AddStyptic_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(24, StyptictextBox.Text);
        }

        private void AddSutureKit_Click(object sender, EventArgs e)
        {
            ModifyItemCapacity(26, SutureKittextBox.Text);
        }

        private void WriteItemValueToMemory(int index, short value)
        {
            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show($"Cannot find process: {PROCESS_NAME}");
                return;
            }

            PROCESS_BASE_ADDRESS = process.MainModule.BaseAddress;
            var processHandle = NativeMethods.OpenProcess(0x1F0FFF, false, process.Id);

            IntPtr address = IntPtr.Add(PROCESS_BASE_ADDRESS, (int)ITEM_OFFSETS[index]);
            int bytesWritten;

            bool success = NativeMethods.WriteProcessMemory(processHandle, address, ref value, sizeof(short), out bytesWritten);

            NativeMethods.CloseHandle(processHandle);
        }


        private void ToggleItemState(string itemName, bool addItem)
        {
            int itemIndex = Array.IndexOf(ITEM_NAMES, itemName);

            if (itemIndex != -1)
            {
                short valueToWrite = addItem ? (short)1 : (short)-1;
                WriteItemValueToMemory(itemIndex, valueToWrite);
            }
            else
            {
                MessageBox.Show($"{itemName} not found.");
            }
        }

        private void AddRPill_Click(object sender, EventArgs e)
        {
            ToggleItemState("REVIVAL PILL", true);
        }

        private void RemoveRPill_Click(object sender, EventArgs e)
        {
            ToggleItemState("REVIVAL PILL", false);
        }

        private void AddCigar_Click(object sender, EventArgs e)
        {
            ToggleItemState("CIGAR", true);
        }

        private void RemoveCigar_Click(object sender, EventArgs e)
        {
            ToggleItemState("CIGAR", false);
        }

        private void AddBinos_Click(object sender, EventArgs e)
        {
            ToggleItemState("BINOCULARS", true);
        }

        private void RemoveBinos_Click(object sender, EventArgs e)
        {
            ToggleItemState("BINOCULARS", false);
        }

        private void AddThermal_Click(object sender, EventArgs e)
        {
            ToggleItemState("THERMAL GOGGLES", true);
        }

        private void RemoveThermal_Click(object sender, EventArgs e)
        {
            ToggleItemState("THERMAL GOGGLES", false);
        }

        private void AddNVG_Click(object sender, EventArgs e)
        {
            ToggleItemState("NIGHT VISION GOGGLES", true);
        }

        private void RemoveNVG_Click(object sender, EventArgs e)
        {
            ToggleItemState("NIGHT VISION GOGGLES", false);
        }

        private void AddCamera_Click(object sender, EventArgs e)
        {
            ToggleItemState("CAMERA", true);
        }

        private void RemoveCamera_Click(object sender, EventArgs e)
        {
            ToggleItemState("CAMERA", false);
        }

        private void AddMotionD_Click(object sender, EventArgs e)
        {
            ToggleItemState("MOTION DETECTOR", true);
        }

        private void RemoveMotionD_Click(object sender, EventArgs e)
        {
            ToggleItemState("MOTION DETECTOR", false);
        }

        private void AddSonar_Click(object sender, EventArgs e)
        {
            ToggleItemState("ACTIVE_SONAR", true);
        }

        private void RemoveSonar_Click(object sender, EventArgs e)
        {
            ToggleItemState("ACTIVE_SONAR", false);
        }

        private void AddMineD_Click(object sender, EventArgs e)
        {
            ToggleItemState("MINE DETECTOR", true);
        }

        private void RemoveMineD_Click(object sender, EventArgs e)
        {
            ToggleItemState("MINE DETECTOR", false);
        }

        private void AddApSensor_Click(object sender, EventArgs e)
        {
            ToggleItemState("ANTI PERSONNEL SENSOR", true);
        }

        private void RemoveApSensor_Click(object sender, EventArgs e)
        {
            ToggleItemState("ANTI PERSONNEL SENSOR", false);
        }

        private void AddBoxA_Click(object sender, EventArgs e)
        {
            ToggleItemState("CBOX A", true);
        }

        private void RemoveBoxA_Click(object sender, EventArgs e)
        {
            ToggleItemState("CBOX A", false);
        }

        private void AddBoxB_Click(object sender, EventArgs e)
        {
            ToggleItemState("CBOX B", true);
        }

        private void RemoveBoxB_Click(object sender, EventArgs e)
        {
            ToggleItemState("CBOX B", false);
        }

        private void AddBoxC_Click(object sender, EventArgs e)
        {
            ToggleItemState("CBOX C", true);
        }

        private void RemoveBoxC_Click(object sender, EventArgs e)
        {
            ToggleItemState("CBOX C", false);
        }

        private void AddBoxD_Click(object sender, EventArgs e)
        {
            ToggleItemState("CBOX D", true);
        }

        private void RemoveBoxD_Click(object sender, EventArgs e)
        {
            ToggleItemState("CBOX D", false);
        }

        private void AddCrocCap_Click(object sender, EventArgs e)
        {
            ToggleItemState("CROC CAP", true);
        }

        private void RemoveCrocCap_Click(object sender, EventArgs e)
        {
            ToggleItemState("CROC CAP", false);
        }

        private void AddKeyA_Click(object sender, EventArgs e)
        {
            ToggleItemState("KEY A", true);
        }

        private void RemoveKeyA_Click(object sender, EventArgs e)
        {
            ToggleItemState("KEY A", false);
        }

        private void AddKeyB_Click(object sender, EventArgs e)
        {
            ToggleItemState("KEY B", true);
        }

        private void RemoveKeyB_Click(object sender, EventArgs e)
        {
            ToggleItemState("KEY B", false);
        }

        private void AddKeyC_Click(object sender, EventArgs e)
        {
            ToggleItemState("KEY C", true);
        }

        private void RemoveKeyC_Click(object sender, EventArgs e)
        {
            ToggleItemState("KEY C", false);
        }

        private void AddBandana_Click(object sender, EventArgs e)
        {
            ToggleItemState("BANDANA", true);
        }

        private void RemoveBandana_Click(object sender, EventArgs e)
        {
            ToggleItemState("BANDANA", false);
        }

        private void AddStealth_Click(object sender, EventArgs e)
        {
            ToggleItemState("STEALTH CAMO", true);
        }

        private void RemoveStealth_Click(object sender, EventArgs e)
        {
            ToggleItemState("STEALTH CAMO", false);
        }

        private void AddMonkey_Click(object sender, EventArgs e)
        {
            ToggleItemState("MONKEY MASK", true);
        }

        private void RemoveMonkey_Click(object sender, EventArgs e)
        {
            ToggleItemState("MONKEY MASK", false);
        }
    }
}