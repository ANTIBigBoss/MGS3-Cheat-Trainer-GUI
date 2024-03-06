using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class DebugDevForm : Form
    {

        public DebugDevForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(BossForm_FormClosing);

        }

        private void BossFormRefactor_Load(object sender, EventArgs e)
        {

        }

        #region Debugging Buttons
        private void button3_Click(object sender, EventArgs e)
        {


            if (MemoryManager.Instance.FoundSnakePositionAddress != IntPtr.Zero)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    float[] snakePosition = MemoryManager.Instance.ReadSnakePosition(processHandle);
                    MessageBox.Show($"Snake's Position: \nX={snakePosition[0]}, \nY={snakePosition[1]}, \nZ={snakePosition[2]}", "Snake Position", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    MessageBox.Show("Failed to open process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Snake position AOB not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string result = MemoryManager.Instance.FindLocationStringDirectlyInRange();
                MessageBox.Show(result, "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Read The Fury's health value from boss manager
        private void button5_Click(object sender, EventArgs e)
        {
            var guardsAddresses = MemoryManager.Instance.FindAllGuardsPositionAOBs();
            if (guardsAddresses.Count > 0)
            {
                IntPtr processHandle = MemoryManager.OpenGameProcess(MemoryManager.GetMGS3Process());
                if (processHandle != IntPtr.Zero)
                {
                    var guardsPositions = MemoryManager.Instance.ReadGuardsPositions(processHandle, guardsAddresses);
                    StringBuilder sb = new StringBuilder();
                    foreach (var position in guardsPositions)
                    {
                        sb.AppendLine($"Guard Position: \nX={position[0]}, \nY={position[1]}, \nZ={position[2]}\n");
                    }
                    MessageBox.Show(sb.ToString(), "Guards Positions", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    NativeMethods.CloseHandle(processHandle);
                }
                else
                {
                    MessageBox.Show("Failed to open process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No guards found.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // This was more of a debug button to see the values of the alert timers so I could determine the offsets of the timers
        private void button6_Click(object sender, EventArgs e)
        {
            MemoryManager memoryManager = new MemoryManager();
            var aobPattern = Constants.AOBs["AlertMemoryRegion"].Pattern;
            var mask = Constants.AOBs["AlertMemoryRegion"].Mask;

            IntPtr alertMemoryRegion = memoryManager.FindAlertMemoryRegion(aobPattern, mask);
            if (alertMemoryRegion == IntPtr.Zero)
            {
                MessageBox.Show("Failed to find alert memory region.");
                return;
            }

            short alertTimerValue = AlertManager.ReadAlertTimerValue(alertMemoryRegion);
            short evasionTimerValue = AlertManager.ReadEvasionTimerValue(alertMemoryRegion);
            short cautionTimerValue = AlertManager.ReadCautionTimerValue(alertMemoryRegion);

            MessageBox.Show($"Alert Timer: {alertTimerValue}, \nEvasion Timer: {evasionTimerValue}, \nCaution Timer: {cautionTimerValue}");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MemoryManager.Instance.FindAndStoreSnakesPositionAOB();

        }

        private IntPtr dynamicAddress = IntPtr.Zero;

        private void button9_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("Starting AOB scan for Ocelot.");
            var process = MemoryManager.GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("MGS3 process not found.");
                return;
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
                return;
            }
            LoggingManager.Instance.Log("Opening process for scanning.");
            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            LoggingManager.Instance.Log($"Process handle: {processHandle}");
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process for scanning.");
                return;
            }

            byte[] pattern = new byte[] { 0xC0, 0x37, 0x00, 0x00, 0x00, 0x7F, 0x00, 0x00, 0x20, 0x11, 0x00, 0x00, 0x00, 0x7F };
            string mask = "xx???xxxxx???x";
            LoggingManager.Instance.Log("Using pattern of length: " + pattern.Length);
            LoggingManager.Instance.Log("Using mask pattern of length: " + mask.Length);
            LoggingManager.Instance.Log("Scanning in range: 0x1D00000 - 0x1E00000");
            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1D00000);
            long size = 0x1E00000 - 0x1D00000;

            IntPtr foundAddress = MemoryManager.Instance.ScanMemory(processHandle, startAddress, size, pattern, mask);
            if (foundAddress != IntPtr.Zero)
            {
                LoggingManager.Instance.Log($"AOB found at: 0x{foundAddress.ToString("X")}");
                // Adjust foundAddress by subtracting 848 bytes to get the actual target address
                dynamicAddress = IntPtr.Subtract(foundAddress, 848);
                LoggingManager.Instance.Log("Going back 848 bytes to get the actual target address.");
            }
            else
            {
                LoggingManager.Instance.Log("AOB not found within specified range.");
            }
            LoggingManager.Instance.Log($"Dynamic AOB address is: 0x{dynamicAddress.ToString("X")}");
            NativeMethods.CloseHandle(processHandle);
        }



        private void button8_Click(object sender, EventArgs e)
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
                return;
            }

            IntPtr dynamicTargetAddress = IntPtr.Add(pointerAddress, 0x5DC);
            IntPtr targetAddress = IntPtr.Subtract(dynamicTargetAddress, 916);

            short valueBefore = MemoryManager.ReadShortFromMemory(processHandle, targetAddress);
            LoggingManager.Instance.Log($"Value before writing: {valueBefore} at 0x{targetAddress.ToString("X")}");

            short valueToWrite = 0;
            int bytesWritten = MemoryManager.WriteShortToMemory(processHandle, targetAddress, valueToWrite);

            if (bytesWritten == sizeof(short))
            {
                LoggingManager.Instance.Log($"Successfully wrote {valueToWrite} to 0x{targetAddress.ToString("X")}");
            }
            else
            {
                LoggingManager.Instance.Log("Failed to write to memory.");
            }

            short valueAfter = MemoryManager.ReadShortFromMemory(processHandle, targetAddress);
            LoggingManager.Instance.Log($"Value after writing: {valueAfter} at 0x{targetAddress.ToString("X")}");

            NativeMethods.CloseHandle(processHandle);
        }


        private void button7_Click(object sender, EventArgs e)
        {
            MemoryManager.Instance.MoveAllGuardsToSnake();
        }

        #endregion

        #region Form Swapping and Closing
        private void BossForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void WeaponFormSwap_Click(object sender, EventArgs e)
        {
            WeaponForm form1 = new WeaponForm();
            form1.Show();
            this.Hide();
            LoggingManager.Instance.Log("User is changing to the Weapon form from the Boss form.\n");
        }

        private void ItemFormSwap_Click(object sender, EventArgs e)
        {
            ItemForm form2 = new ItemForm();
            form2.Show();
            this.Hide();
            LoggingManager.Instance.Log("User is changing to the Item form from the Boss form.\n");
        }

        private void CamoFormSwap_Click(object sender, EventArgs e)
        {
            CamoForm form3 = new CamoForm();
            form3.Show();
            this.Hide();
            LoggingManager.Instance.Log("User is changing to the Camo form from the Boss form.\n");
        }

        private void MiscFormSwap_Click(object sender, EventArgs e)
        {
            MiscForm form4 = new MiscForm();
            form4.Show();
            this.Hide();
            LoggingManager.Instance.Log("User is changing to the Misc form from the Boss form.\n");
        }

        private void HealthFormSwap_Click(object sender, EventArgs e)
        {
            StatsAndAlertForm form5 = new StatsAndAlertForm();
            form5.Show();
            this.Hide();
            LoggingManager.Instance.Log("User is changing to the Stats and Alert form from the Boss form.\n");
        }
        #endregion
    }
}