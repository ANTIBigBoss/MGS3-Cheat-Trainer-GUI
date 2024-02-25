using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class BossFormRefactor : Form
    {

        public BossFormRefactor()
        {
            InitializeComponent();
            ConsistencyCheckTimer.Interval = 1000;
        }

        private void BossFormRefactor_Load(object sender, EventArgs e)
        {
            ConsistencyCheckTimer.Start();
            OcelotHealthSlider.Maximum = 3400; // Max health
            OcelotHealthSlider.Minimum = 1; // Min health
            OcelotStaminaSlider.Maximum = 3400; // Max stamina
            OcelotStaminaSlider.Minimum = 1; // Min stamina

            // Pain can be set to 0 but Ocelot can't therefore 1 for Ocelot and 0 for Pain for min values
            PainHealthSlider.Maximum = 30000;
            PainHealthSlider.Minimum = 0;
            PainStaminaSlider.Maximum = 21000;
            PainStaminaSlider.Minimum = 0;
        }

        private string currentArea = "";

        private void ConsistencyCheckTimer_Tick(object sender, EventArgs e)
        {
            currentArea = BossManager.UpdateBossStateBasedOnLocation();

            if (currentArea == "s023a")
            {
                short ocelotHealth = BossManager.ReadOcelotHealth();
                short ocelotStamina = BossManager.ReadOcelotStamina();
                if (ocelotHealth != -1)
                {
                    OcelotHealthSlider.Value = Math.Min(OcelotHealthSlider.Maximum, Math.Max(ocelotHealth, OcelotHealthSlider.Minimum));
                    OcelotStaminaSlider.Value = Math.Min(OcelotStaminaSlider.Maximum, Math.Max(ocelotStamina, OcelotStaminaSlider.Minimum));
                }
            }

            if (currentArea == "s032b")
            {
                short painHealth = BossManager.ReadThePainHealth();
                short painStamina = BossManager.ReadThePainStamina();
                if (painHealth != -1)
                {
                    PainHealthSlider.Value = Math.Min(PainHealthSlider.Maximum, Math.Max(painHealth, PainHealthSlider.Minimum));
                    PainStaminaSlider.Value = Math.Min(PainStaminaSlider.Maximum, Math.Max(painStamina, PainStaminaSlider.Minimum));
                }
            }
        }

        private void BossAreaFound(object sender, EventArgs e)
        {
            string currentArea = BossManager.UpdateBossStateBasedOnLocation();

            switch (currentArea)
            {
                case "s023a":
                    SetupOcelotControls();
                    break;

                case "s032b":
                    SetupThePainControls();
                    break;

                default:
                    // Nothing needs to happen if the player is not in a boss area so break and check again
                    break;
            }
        }

        private void SetupOcelotControls()
        {
            OcelotTimer.Start();
            LoggingManager.Instance.Log("Looking for Ocelot AOB");
            BossManager.FindOcelotAOB();

            if (currentArea != "s023a")
            {
                OcelotTimer.Stop();
                return;
            }
        }

        private void SetupThePainControls()
        {
            PainTimer.Start();
            LoggingManager.Instance.Log("Looking for The Pain AOB");
            BossManager.FindThePainAOB();

            if (currentArea != "s032b")
            {
                PainTimer.Stop();
                return;
            }
        }

        private void Ocelot0HP_Click(object sender, EventArgs e)
        {
            if (currentArea != "s023a")
            {
                LoggingManager.Instance.Log("Ocelot's health was attempted to be depleted via Health button click, but the player is not in the correct area. Messagebox error sent to the player.");
                MessageBox.Show("You are not in the correct area to deplete Ocelot's health.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BossManager.WriteOcelotHealth(0x0001);
            LoggingManager.Instance.Log("Ocelot's health was depleted via Health button click");
        }

        private void OcelotHealthSlider_Scroll(object sender, EventArgs e)
        {
            if (currentArea != "s023a")
            {
                LoggingManager.Instance.Log("Ocelot's health was attempted to be depleted via Health button click, but the player is not in the correct area. Messagebox error sent to the player.");
                MessageBox.Show("You are not in the correct area to deplete Ocelot's health.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            short newHealthValue = (short)OcelotHealthSlider.Value;
            BossManager.WriteOcelotHealth(newHealthValue);
            LoggingManager.Instance.Log("Ocelot's health was set to " + newHealthValue + "via the slider");
            if (newHealthValue == 0)
            {
                LoggingManager.Instance.Log("Ocelot's health was depleted via slider.");
            }
        }

        private void Ocelot0Stam_Click(object sender, EventArgs e)
        {
            if (currentArea != "s023a")
            {
                LoggingManager.Instance.Log("Ocelot's stamina was attempted to be depleted via Stamina button click, but the player is not in the correct area. Messagebox error sent to the player.");
                MessageBox.Show("You are not in the correct area to deplete Ocelot's stamina.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BossManager.WriteOcelotStamina(0x0001);
            LoggingManager.Instance.Log("Ocelot's stamina was depleted via Stamina button click");
        }

        private void OcelotStaminaSlider_Scroll(object sender, EventArgs e)
        {
            if (currentArea != "s023a")
            {
                LoggingManager.Instance.Log("Ocelot's stamina was attempted to be depleted via Stamina button click, but the player is not in the correct area. Messagebox error sent to the player.");
                MessageBox.Show("You are not in the correct area to deplete Ocelot's stamina.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            short newStaminaValue = (short)OcelotStaminaSlider.Value;
            BossManager.WriteOcelotStamina(newStaminaValue);
            LoggingManager.Instance.Log("Ocelot's stamina was set to " + newStaminaValue + "via the slider");
            if (newStaminaValue == 0)
            {
                LoggingManager.Instance.Log("Ocelot's stamina was depleted via slider.");
            }
        }

        private void OcelotTimer_Tick(object sender, EventArgs e)
        {
            OcelotHealthSlider.Scroll -= OcelotHealthSlider_Scroll;
            OcelotStaminaSlider.Scroll -= OcelotStaminaSlider_Scroll;

            try
            {
                short currentHealth = BossManager.ReadOcelotHealth();
                short currentStamina = BossManager.ReadOcelotStamina();

                OcelotHealthSlider.Value = Math.Clamp(currentHealth, OcelotHealthSlider.Minimum, OcelotHealthSlider.Maximum);
                OcelotStaminaSlider.Value = Math.Clamp(currentStamina, OcelotStaminaSlider.Minimum, OcelotStaminaSlider.Maximum);
            }
            finally
            {
                OcelotHealthSlider.Scroll += OcelotHealthSlider_Scroll;
                OcelotStaminaSlider.Scroll += OcelotStaminaSlider_Scroll;
            }
        }

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

        // s032b is The Pain's area
        private void Pain0HP_Click(object sender, EventArgs e)
        {
            if (currentArea != "s032b")
            {
                LoggingManager.Instance.Log("The Pain's health was attempted to be depleted via Health button click, but the player is not in the correct area. Messagebox error sent to the player.");
                MessageBox.Show("You are not in the correct area to deplete The Pain's health.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BossManager.WriteThePainHealth(0x0001);
            LoggingManager.Instance.Log("The Pain's health was depleted via Health button click");

        }

        private void PainHealthSlider_Scroll(object sender, EventArgs e)
        {
            if (currentArea != "s032b")
            {
                LoggingManager.Instance.Log("The Pain's health was attempted to be depleted via Health button click, but the player is not in the correct area. Messagebox error sent to the player.");
                MessageBox.Show("You are not in the correct area to deplete The Pain's health.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            short newHealthValue = (short)PainHealthSlider.Value;
            BossManager.WriteThePainHealth(newHealthValue);
            LoggingManager.Instance.Log("The Pain's health was set to " + newHealthValue + "via the slider");
            if (newHealthValue == 0)
            {
                LoggingManager.Instance.Log("The Pain's health was depleted via slider.");
            }

        }

        private void Pain0Stam_Click(object sender, EventArgs e)
        {
            if (currentArea != "s032b")
            {
                LoggingManager.Instance.Log("The Pain's stamina was attempted to be depleted via Stamina button click, but the player is not in the correct area. Messagebox error sent to the player.");
                MessageBox.Show("You are not in the correct area to deplete The Pain's stamina.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BossManager.WriteThePainStamina(0x0001);
            LoggingManager.Instance.Log("The Pain's stamina was depleted via Stamina button click");

        }

        private void PainStaminaSlider_Scroll(object sender, EventArgs e)
        {
            if (currentArea != "s032b")
            {
                LoggingManager.Instance.Log("The Pain's stamina was attempted to be depleted via Stamina button click, but the player is not in the correct area. Messagebox error sent to the player.");
                MessageBox.Show("You are not in the correct area to deplete The Pain's stamina.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            short newStaminaValue = (short)PainStaminaSlider.Value;
            BossManager.WriteThePainStamina(newStaminaValue);
            LoggingManager.Instance.Log("The Pain's stamina was set to " + newStaminaValue + "via the slider");
            if (newStaminaValue == 0)
            {
                LoggingManager.Instance.Log("The Pain's stamina was depleted via slider.");
            }
        }

        private void PainTimer_Tick(object sender, EventArgs e)
        {
            PainHealthSlider.Scroll -= PainHealthSlider_Scroll;
            PainStaminaSlider.Scroll -= PainStaminaSlider_Scroll;

            try
            {
                short currentHealth = BossManager.ReadThePainHealth();
                short currentStamina = BossManager.ReadThePainStamina();

                PainHealthSlider.Value = Math.Clamp(currentHealth, PainHealthSlider.Minimum, PainHealthSlider.Maximum);
                PainStaminaSlider.Value = Math.Clamp(currentStamina, PainStaminaSlider.Minimum, PainStaminaSlider.Maximum);
            }
            finally
            {
                PainHealthSlider.Scroll += PainHealthSlider_Scroll;
                PainStaminaSlider.Scroll += PainStaminaSlider_Scroll;
            }
        }
    }
}