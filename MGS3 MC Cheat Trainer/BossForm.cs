namespace MGS3_MC_Cheat_Trainer
{
    public partial class BossForm : Form
    {
        public BossForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(BossForm_FormClosing);
        }

        private void BossForm_Load(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("Boss form loaded successfully.");
            string result = MemoryManager.Instance.FindLocationStringDirectlyInRange();
            string locationString = MemoryManager.Instance.ExtractLocationStringFromResult(result);
            // Use message box for debugging only
            //

            // Make a switch statement for the result string
            switch (locationString)
            {
                case "s032b":
                    MessageBox.Show("Looking for The Pain AOB", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoggingManager.Instance.Log("Looking for The Pain's AOB");
                    BossManager.FindTheFearAOB(); // Pain and Fear share the same AOB
                    LoggingManager.Instance.Log("The Pain AOB found at: 0x" + MemoryManager.Instance.FoundTheFearAddress.ToInt64().ToString("X"));
                    PainTimer.Interval = 1000; // Update every second
                    FearTimer.Tick += new EventHandler(PainTimer_Tick);
                    PainTimer.Start();
                    PainHealthSlider.Maximum = 30000; // Max health
                    PainHealthSlider.Minimum = 0; // Min health
                    PainStaminaSlider.Maximum = 21000; // Max stamina
                    PainStaminaSlider.Minimum = 0; // Min stamina
                    // Pretty heavyhanded way to disable these but it'll do for now
                    FearHealthSlider.Enabled = false;
                    FearStaminaSlider.Enabled = false;
                    Fear0HP.Enabled = false;
                    Fear0Stam.Enabled = false;
                    VolginHealthSlider.Enabled = false;
                    VolginStaminaSlider.Enabled = false;
                    Volgin0HP.Enabled = false;
                    Volgin0Stam.Enabled = false;
                    LoggingManager.Instance.Log("Every other boss control disabled except for The Pain");
                    break;


                case "s051b":
                    LoggingManager.Instance.Log("Looking for The Fear AOB");
                    MessageBox.Show("Looking for The Fear AOB", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BossManager.FindTheFearAOB(); // Actually The Fear
                    LoggingManager.Instance.Log("The Fear AOB found at: 0x" + MemoryManager.Instance.FoundTheFearAddress.ToInt64().ToString("X"));
                    FearTimer.Interval = 1000; // Update every second
                    FearTimer.Tick += new EventHandler(FearTimer_Tick);
                    FearTimer.Start();
                    FearHealthSlider.Maximum = 30000; // Max health
                    FearHealthSlider.Minimum = 0; // Min health
                    FearStaminaSlider.Maximum = 21000; // Max stamina
                    FearStaminaSlider.Minimum = 0; // Min stamina
                    PainHealthSlider.Enabled = false;
                    PainStaminaSlider.Enabled = false;
                    Pain0HP.Enabled = false;
                    Pain0Stam.Enabled = false;
                    VolginHealthSlider.Enabled = false;
                    VolginStaminaSlider.Enabled = false;
                    Volgin0HP.Enabled = false;
                    Volgin0Stam.Enabled = false;
                    LoggingManager.Instance.Log("Every other boss control disabled except for The Fear");
                    break;

                
                case "s122a":
                    MessageBox.Show("Looking for Volgin AOB", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BossManager.FindTheFearAOB(); // Volgin's first fight shares the same AOB as well
                    VolginTimer.Interval = 1000; // Update every second
                    FearTimer.Tick += new EventHandler(VolginTimer_Tick);
                    VolginTimer.Start();
                    VolginHealthSlider.Maximum = 30000; // Max health
                    VolginHealthSlider.Minimum = 0; // Min health
                    VolginStaminaSlider.Maximum = 21000; // Max stamina
                    VolginStaminaSlider.Minimum = 0; // Min stamina
                    FearHealthSlider.Enabled = false;
                    FearStaminaSlider.Enabled = false;
                    Fear0HP.Enabled = false;
                    Fear0Stam.Enabled = false;
                    PainHealthSlider.Enabled = false;
                    PainStaminaSlider.Enabled = false;
                    Pain0HP.Enabled = false;
                    Pain0Stam.Enabled = false;
                    LoggingManager.Instance.Log("Every other boss control disabled except for Volgin");
                    break;

                default:
                    MessageBox.Show("No Boss Stats not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(result, "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Log where the user is from their string result
                    LoggingManager.Instance.Log("No boss stats found. User is in: " + result);

                    break;
            }
        }

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

        private void Fear0HP_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheFearHealth(0x0000);
            LoggingManager.Instance.Log("The Fear's health was depleted via Health button click");
        }

        private void Fear0Stam_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheFearStamina(0x0000);
            LoggingManager.Instance.Log("The Fear's stamina was depleted via Stamina button click");
        }

        private void FearTimer_Tick(object sender, EventArgs e)
        {
            // Temporarily disable event handlers to prevent recursion
            FearHealthSlider.Scroll -= FearHealthSlider_Scroll;
            FearStaminaSlider.Scroll -= FearStaminaSlider_Scroll;

            try
            {
                short currentHealth = BossManager.ReadTheFearHealth();
                short currentStamina = BossManager.ReadTheFearStamina();

                // Update sliders with current values
                FearHealthSlider.Value = Math.Clamp(currentHealth, FearHealthSlider.Minimum, FearHealthSlider.Maximum);
                FearStaminaSlider.Value = Math.Clamp(currentStamina, FearStaminaSlider.Minimum, FearStaminaSlider.Maximum);
            }
            finally
            {
                // Re-enable event handlers after updating
                FearHealthSlider.Scroll += FearHealthSlider_Scroll;
                FearStaminaSlider.Scroll += FearStaminaSlider_Scroll;
            }
        }
        // Not gonna log the sliders since they'll flood the log file
        private void FearHealthSlider_Scroll(object sender, EventArgs e)
        {
            short newHealthValue = (short)FearHealthSlider.Value;
            BossManager.WriteTheFearHealth(newHealthValue);
        }

        private void FearStaminaSlider_Scroll(object sender, EventArgs e)
        {
            short newStaminaValue = (short)FearStaminaSlider.Value;
            BossManager.WriteTheFearStamina(newStaminaValue);
        }

        private void Pain0HP_Click(object sender, EventArgs e)
        {
            BossManager.WriteThePainHealth(0x0000);
            LoggingManager.Instance.Log("The Pain's health was depleted via Health button click");
        }

        private void Pain0Stam_Click(object sender, EventArgs e)
        {
            BossManager.WriteThePainStamina(0x0000);
            LoggingManager.Instance.Log("The Pain's stamina was depleted via Stamina button click");
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

        private void PainHealthSlider_Scroll(object sender, EventArgs e)
        {
            short newHealthValue = (short)PainHealthSlider.Value;
            BossManager.WriteThePainHealth(newHealthValue);
        }

        private void PainStaminaSlider_Scroll(object sender, EventArgs e)
        {
            short newStaminaValue = (short)PainStaminaSlider.Value;
            BossManager.WriteThePainStamina(newStaminaValue);
        }

        private void Volgin0HP_Click(object sender, EventArgs e)
        {
            BossManager.WriteVolginHealth(0x0000);
            LoggingManager.Instance.Log("Volgin's health was depleted via Health button click");
        }

        private void Volgin0Stam_Click(object sender, EventArgs e)
        {
            BossManager.WriteVolginStamina(0x0000);
            LoggingManager.Instance.Log("Volgin's stamina was depleted via Stamina button click");
        }

        private void VolginTimer_Tick(object sender, EventArgs e)
        {
            VolginHealthSlider.Scroll -= VolginHealthSlider_Scroll;
            VolginStaminaSlider.Scroll -= VolginStaminaSlider_Scroll;

            try
            {
                short currentHealth = BossManager.ReadVolginHealth();
                short currentStamina = BossManager.ReadVolginStamina();

                VolginHealthSlider.Value =
                    Math.Clamp(currentHealth, VolginHealthSlider.Minimum, VolginHealthSlider.Maximum);
                VolginStaminaSlider.Value =
                    Math.Clamp(currentStamina, VolginStaminaSlider.Minimum, VolginStaminaSlider.Maximum);
            }
            finally
            {
                VolginHealthSlider.Scroll += VolginHealthSlider_Scroll;
                VolginStaminaSlider.Scroll += VolginStaminaSlider_Scroll;
            }

        }

        private void VolginHealthSlider_Scroll(object sender, EventArgs e)
        {
            short newHealthValue = (short)VolginHealthSlider.Value;
            BossManager.WriteVolginHealth(newHealthValue);
        }

        private void VolginStaminaSlider_Scroll(object sender, EventArgs e)
        {
            short newStaminaValue = (short)VolginStaminaSlider.Value;
            BossManager.WriteVolginStamina(newStaminaValue);
        }

        
    }
}