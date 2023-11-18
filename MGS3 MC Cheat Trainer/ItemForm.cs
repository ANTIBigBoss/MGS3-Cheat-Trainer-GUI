using System.Diagnostics;
using System.Runtime.InteropServices;


namespace MGS3_MC_Cheat_Trainer
{
    public partial class ItemForm : Form
    {
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

        //Navigation and closing logic functions
        public ItemForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form2_FormClosing);
        }

        private void WeaponFormSwap_Click(object sender, EventArgs e) // Form1
        {
            WeaponForm form1 = new WeaponForm();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e) // Form3
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