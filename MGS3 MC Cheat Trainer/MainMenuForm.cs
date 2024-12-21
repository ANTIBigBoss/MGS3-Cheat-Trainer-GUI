using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            {
                InitializeComponent();
                this.FormClosing += new FormClosingEventHandler(MainMenuForm_FormClosing);
            }
        }

        private void WeaponFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Weapon form from the Main Menu form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "WeaponForm");
            WeaponForm form1 = new();
            form1.Show();
            this.Hide();
        }

        private void ItemFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Item form from the Main Menu form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "ItemForm");
            ItemForm form2 = new();
            form2.Show();
            this.Hide();
        }

        private void CamoFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Camo form from the Main Menu form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "CamoForm");
            CamoForm form3 = new();
            form3.Show();
            this.Hide();
        }

        private void MiscFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Misc form from the Main Menu form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "MiscForm");
            MiscForm form4 = new();
            form4.Show();
            this.Hide();
        }

        private void HealthFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Stats and Alert form from the Main Menu form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "StatsAndAlertForm");
            StatsAndAlertForm form5 = new();
            form5.Show();
            this.Hide();
        }

        private void BossFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Boss form from the Main Menu form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "BossForm");
            BossForm form6 = new();
            form6.Show();
            this.Hide();
        }

        private void GameStatsFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Game Stats form from the Main Menu form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "GameStatsForm");
            GameStatsForm form7 = new();
            form7.Show();
            this.Hide();

        }

        private void DebugFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Debug form from the Main Menu form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "DebugForm");
            DebugForm form8 = new();
            form8.Show();
            this.Hide();
        }

        

        private void MainMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoggingManager.Instance.Log("User exiting the trainer.\nEnd of log for this session.\n\n\n\n");
            Application.Exit(); // If we close this form we close the whole application so the user isn't running this without knowing
        }

        private bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

            if (!IsAdministrator())
            {
                // Attempt to restart the application with administrative privileges by checking if the user is an administrator
                try
                {
                    ProcessStartInfo proc = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        WorkingDirectory = Application.StartupPath,
                        FileName = Application.ExecutablePath,
                        Verb = "runas"
                    };
                    Process.Start(proc);
                    Application.Exit();
                    return;
                }
                catch (Exception ex)
                {
                    // If an error occurs this will display in the log and if they submit it in a Discord ticket we can troubleshoot
                    LoggingManager.Instance.Log("This application must be run as Administrator.\n\nExiting now and rebooting in Administrator.");
                    Application.Exit();
                    return;
                }
            }

            // Check if the game is running before starting the application if the game isn't running then close the application
            //MemoryManager.Instance.TrainerLooksForGame(sender, e);

            TimerManager.StartLocationTracking();
            LoggingManager.Instance.Log("Now Tracking the player's map location.\n");
        }

        private void LogAOBs_Click(object sender, EventArgs e)
        {
            LoggingManager.LogAllWeaponsAndItemsAddresses();
            LoggingManager.Instance.LogAOBAddresses();
            LoggingManager.Instance.LogAllMemoryAddressesandValues();
            StringManager.Instance.DisplayEntirePointer();
            CustomMessageBoxManager.CustomMessageBox("Information written to log file in: C:\\Users\\YourUserNameHere\\Documents\\MGS3 CT Log\\MGS3_MC_Cheat_Trainer_Log.txt", "Log Generated");
        }

        
    }
}
