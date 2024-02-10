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
            BossManager.FindTheFearAOB();
            // Setup the timer
            FearTimer.Interval = 1000; // Update every second
            FearTimer.Tick += new EventHandler(FearTimer_Tick);
            FearTimer.Start();
            // Example of setting up in Form Load or Designer
            FearHealthSlider.Maximum = 30000; // Max health
            FearHealthSlider.Minimum = 0; // Min health
            FearStaminaSlider.Maximum = 21000; // Max stamina
            FearStaminaSlider.Minimum = 0; // Min stamina


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
        }

        private void ItemFormSwap_Click(object sender, EventArgs e)
        {
            ItemForm form2 = new ItemForm();
            form2.Show();
            this.Hide();
        }

        private void CamoFormSwap_Click(object sender, EventArgs e)
        {
            CamoForm form3 = new CamoForm();
            form3.Show();
            this.Hide();
        }

        private void MiscFormSwap_Click(object sender, EventArgs e)
        {
            MiscForm form4 = new MiscForm();
            form4.Show();
            this.Hide();
        }

        private void HealthFormSwap_Click(object sender, EventArgs e)
        {
            StatsAndAlertForm form5 = new StatsAndAlertForm();
            form5.Show();
            this.Hide();
        }

        private void Fear0HP_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheFearHealth(0x0000);
        }

        private void Fear0Stam_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheFearStamina(0x0000);
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


    }
}
