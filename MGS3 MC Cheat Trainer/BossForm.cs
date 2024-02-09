namespace MGS3_MC_Cheat_Trainer
{
    public partial class BossForm : Form
    {
        public BossForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(BossForm_FormClosing);
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
            BossManager.WriteTheFearHealth(0x0100);
        }

        private void Fear1HP_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheFearHealth(0x0000);
        }

        private void Fear0Stam_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheFearStamina(0x0000);
        }

        private void Fear1Stam_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheFearStamina(0x0100);
        }
    
    }
}
