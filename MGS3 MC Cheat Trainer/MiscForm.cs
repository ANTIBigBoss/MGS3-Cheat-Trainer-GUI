using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class MiscForm : Form
    {
        IntPtr processHandle; // Ensure this is correctly initialized
        
        

        public MiscForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);

            // Initialize these here
            /* From what I can tell, doing this doesn't actually... do anything at all.
            IntPtr pointerOffset = (IntPtr)0x00AE49D8;
            IntPtr finalOffset = (IntPtr)0x684;
            IntPtr finalDataAddress = ResolvePointerAddress(baseAddress, pointerOffset, finalOffset);
            */
            // Now you can read or write to finalDataAddress
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void WeaponFormSwap_Click(object sender, EventArgs e) // Weapon Form Swap
        {
            WeaponForm f1 = new WeaponForm();
            f1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) // Item Form Swap
        {
            ItemForm f2 = new ItemForm();
            f2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) // Camo Form Swap
        {
            CamoForm f3 = new CamoForm();
            f3.Show();
            this.Hide();
        }

        // Form Closing
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e) // Alert Mode Trigger
        {
            MemoryManager.ChangeAlertMode((int) Constants.AlertModes.Alert);
        }

        private void button9_Click(object sender, EventArgs e) // Caution Mode Trigger
        {
            MemoryManager.ChangeAlertMode((int) Constants.AlertModes.Caution);
        }

        private void SnakeNapQuick_Click(object sender, EventArgs e)
        {
            MemoryManager.TriggerSnakeAnimation(Constants.MGS3SnakeAnimations.QuickSleep);
        }

        private void SnakeLongNap_Click(object sender, EventArgs e)
        {
            MemoryManager.TriggerSnakeAnimation(Constants.MGS3SnakeAnimations.LongSleep);
        }

        private void SnakeFakesDeath_Click(object sender, EventArgs e)
        {
            MemoryManager.TriggerSnakeAnimation(Constants.MGS3SnakeAnimations.FakeDeath);
        }

        private void SnakePukes_Click(object sender, EventArgs e)
        {
            MemoryManager.TriggerSnakeAnimation(Constants.MGS3SnakeAnimations.Puke);
        }

        private void button12_Click(object sender, EventArgs e) // Snake on fire
        {
            MemoryManager.TriggerSnakeAnimation(Constants.MGS3SnakeAnimations.OnFire);
        }

        private void SnakePukeFire_Click(object sender, EventArgs e)
        {
            MemoryManager.TriggerSnakeAnimation(Constants.MGS3SnakeAnimations.OnFirePuke);
        }

        private void button23_Click(object sender, EventArgs e) // Bunny hop
        {
            MemoryManager.TriggerSnakeAnimation(Constants.MGS3SnakeAnimations.BunnyHop);
        }

        private void NormalHUD_Click(object sender, EventArgs e)
        {
            MemoryManager.ChangeHud((int) Constants.HudOptions.Normal);
        }

        private void ShrinkHUD_Click(object sender, EventArgs e)
        {
            MemoryManager.ChangeHud((int)Constants.HudOptions.Shrunk);
        }

        private void NoHUD_Click(object sender, EventArgs e)
        {
            MemoryManager.ChangeHud((int)Constants.HudOptions.None);
        }

        private void NormalCam_Click(object sender, EventArgs e)
        {
            MemoryManager.ChangeHud((int)Constants.CameraOptions.Normal);
        }

        private void UpsideDownCam_Click(object sender, EventArgs e)
        {
            MemoryManager.ChangeHud((int)Constants.CameraOptions.UpsideDown);
        }

        // Health and Stamina along with pointer logic
        private void Plus100HpValue_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.CurrentHealth, 100);
        }

        private void Minus100HpValue_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.CurrentHealth, -100);
        }

        private void CurrentHpTo1_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.CurrentHealth, 1, true);
        }

        private void MaxHpTo1_Click(object sender, EventArgs e)
        {

            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.MaxHealth, 1, true);
        }

        private void ZeroHP_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.CurrentHealth, 0, true);
        }

        private void SetStaminaToZero_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.Stamina, 0, true);
        }

        // I thought 10000 was a bar at first but it's actually 7500 per bar 
        // so this function name is misleading LOL will fix in V2
        private void Plus10000StaminaValue_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.Stamina, 7500);
        }

        private void Minus10000StaminaValue_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.Stamina, -7500);
        }

        private void FullStamina30000Value_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.Stamina, 30000, true);
        }

        private void Plus100MaxHpValue_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.MaxHealth, 100);
        }

        private void Minus100MaxHpValue_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyHealthOrStamina(Constants.HealthType.MaxHealth, -100);
        }
    }
}