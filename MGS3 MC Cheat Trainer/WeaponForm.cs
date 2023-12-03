using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class WeaponForm : Form
    {
        public WeaponForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region Ammo Modifiers
        private void MK22Button_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.MK22, MK22TextBox.Text);
        }

        private void M1911A1Button_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.M1911A1, M1911A1TextBox.Text);
        }

        private void XM16E1Button_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.XM16E1, XM16E1TextBox.Text);
        }

        private void AK47Button_Click_1(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.AK47, AK47TextBox.Text);
        }

        private void SVDButton_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.SVD, SVDTextBox.Text);
        }

        private void M37Button_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.M37, M37TextBox.Text);
        }

        private void RPG7Button_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.RPG7, RPG7TextBox.Text);
        }

        private void M63Button_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.M63, M63TextBox.Text);
        }

        private void ScorpionButton_Click_1(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.Scorpion, ScorpionTextBox.Text);
        }

        private void MosinButton_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.Mosin, MosinTextBox.Text);
        }

        private void SAAButton_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.SAA, SAATextBox.Text);
        }

        private void CigSprayButton_Click_1(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.CigSpray, CigSprayTextBox.Text);
        }

        private void HandkerchiefButton_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.Handkerchief, HandkerchiefTextBox.Text);
        }

        private void GrenadeButton_Click_1(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.Grenade, GrenadeTextBox.Text);
        }

        private void WpGrenadeButton_Click_1(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.WpGrenade, WpGrenadeTextBox.Text);
        }

        private void ChaffGrenadeButton_Click_1(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.ChaffGrenade, ChaffGrenadeTextBox.Text);
        }

        private void SmokeGrenadeButton_Click_1(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.SmokeGrenade, SmokeGrenadeTextBox.Text);
        }

        private void StunGrenadeButton_Click_1(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.StunGrenade, StunGrenadeTextBox.Text);
        }

        private void MagazineButton_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.EmptyMag, MagazineTextBox.Text);
        }

        private void BookButton_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.Book, BookTextBox.Text);
        }

        private void ClaymoreButton_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.Claymore, ClaymoreTextBox.Text);
        }

        private void TNTButton_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.TNT, TNTTextBox.Text);
        }

        private void C3Button_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.C3, C3TextBox.Text);
        }

        private void MousetrapButton_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyAmmo(MGS3UsableObjects.Mousetrap, MousetrapTextbox.Text);
        }
        #endregion

        #region Weapon Toggles
        private void AddPatriot_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.Patriot, true);
        }

        private void RemovePatriot_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.Patriot, false);
        }

        private void AddEz_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.EzGun, true);
        }

        private void RemoveEz_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.EzGun, false);
        }

        private void AddKnife_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.SurvivalKnife, true);
        }

        private void RemoveKnife_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.SurvivalKnife, false);
        }

        private void AddFork_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.Fork, true);
        }

        private void RemoveFork_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.Fork, false);
        }

        private void AddTorch_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.Torch, true);
        }

        private void RemoveTorch_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.Torch, false);
        }

        private void AddDMic_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.DirectionalMic, true);
        }

        private void RemoveDMic_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleWeapon(MGS3UsableObjects.DirectionalMic, false);
        }
        #endregion

        #region Clip Modifiers
        private void MK22Clip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.MK22, MK22TextBox.Text);
        }

        private void M1911A1Clip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.M1911A1, M1911A1TextBox.Text);
        }

        private void XM16E1Clip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.XM16E1, XM16E1TextBox.Text);
        }

        private void AK47Clip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.AK47, AK47TextBox.Text);
        }

        private void SVDClip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.SVD, SVDTextBox.Text);
        }

        private void M37Clip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.M37, M37TextBox.Text);
        }

        private void RPG7Clip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.RPG7, RPG7TextBox.Text);
        }

        private void M63Clip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.M63, M63TextBox.Text);
        }

        private void ScorpionClip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.Scorpion, ScorpionTextBox.Text);
        }

        private void MosinClip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.Mosin, MosinTextBox.Text);
        }

        private void SAAClip_Click(object sender, EventArgs e)
        {
            MemoryManager.ModifyClipSize(MGS3UsableObjects.SAA, SAATextBox.Text);
        }
        #endregion

        #region Suppressor Toggles
        private void ToggleMK22Suppressor_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleSuppressor(MGS3UsableObjects.MK22);
        }

        private void ToggleM1911A1Suppressor_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleSuppressor(MGS3UsableObjects.M1911A1);
        }

        private void ToggleXM16E1Suppressor_Click(object sender, EventArgs e)
        {
            MemoryManager.ToggleSuppressor(MGS3UsableObjects.XM16E1);
        }
        #endregion

        #region Suppressor Capacity Modifiers
        private void Minus30MK22_Click(object sender, EventArgs e)
        {
            MemoryManager.AdjustSuppressorCapacity(MGS3UsableObjects.MK22Surpressor, false);
        }

        private void Plus30MK22_Click(object sender, EventArgs e)
        {
            MemoryManager.AdjustSuppressorCapacity(MGS3UsableObjects.MK22Surpressor, true);
        }

        private void Minus30M1911A1_Click(object sender, EventArgs e)
        {
            MemoryManager.AdjustSuppressorCapacity(MGS3UsableObjects.M1911A1Surpressor, false);
        }

        private void Plus30M1911A1_Click(object sender, EventArgs e)
        {
            MemoryManager.AdjustSuppressorCapacity(MGS3UsableObjects.M1911A1Surpressor, true);
        }

        private void Minus30XM16E1_Click(object sender, EventArgs e)
        {
            MemoryManager.AdjustSuppressorCapacity(MGS3UsableObjects.XM16E1Surpressor, false);
        }

        private void Plus30XM16E1_Click(object sender, EventArgs e)
        {
            MemoryManager.AdjustSuppressorCapacity(MGS3UsableObjects.XM16E1Surpressor, true);
        }
        #endregion

        // Function logics for navigating to other forms for items, camo and misc
        // Form2 is for items
        // Form3 is for camo
        // Form4 is for misc
        // Should rename these in the future for better clarity
        private void ItemFormSwap_Click(object sender, EventArgs e)
        {
            ItemForm form2 = new ItemForm();
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CamoForm form3 = new CamoForm();
            form3.Show();
            this.Hide();
        }

        private void button70_Click(object sender, EventArgs e) // Load form4
        {
            MiscForm form4 = new MiscForm();
            form4.Show();
            this.Hide();
        }
    }
}