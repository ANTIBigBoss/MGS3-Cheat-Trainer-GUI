using System.Diagnostics;
using System.Runtime.InteropServices;


namespace MGS3_MC_Cheat_Trainer
{
    public partial class ItemForm : Form
    {

        //Navigation and closing logic functions
        public ItemForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form2_FormClosing);
        }

        private void WeaponFormSwap_Click(object sender, EventArgs e) // Switch to weapons form
        {
            WeaponForm form1 = new();
            form1.Show();
            this.Hide();
        }

        private void MiscFormSwap_Click(object sender, EventArgs e) // Load Misc form
        {
            MiscForm form4 = new();
            form4.Show();
            this.Hide();
        }

        private void HealthFormSwap_Click(object sender, EventArgs e) // Load Health and Alert form
        {
            StatsAndAlertForm form5 = new();
            form5.Show();
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

        private void AddLifeMed_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.LifeMed, LifeMedtextBox.Text);
        }


        private void AddBugJuice_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.BugJuice, BugJuicetextBox.Text);
        }

        private void AddFDP_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.FakeDeathPill, FDPtextBox.Text);
        }

        private void AddPentazemin_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.Pentazemin, PentazemintextBox.Text);
        }

        private void AddAntidote_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.Antidote, AntidotetextBox.Text);
        }

        private void AddCMed_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.ColdMedicine, CMedtextBox.Text);
        }

        private void AddDMed_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.DigestiveMedicine, DMedtextBox.Text);
        }

        private void AddSerum_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.Serum, SerumtextBox.Text);
        }

        private void AddBandage_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.Bandage, BandagetextBox.Text);
        }

        private void AddDisinfectant_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.Disinfectant, DisinfectanttextBox.Text);
        }

        private void AddOintment_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.Ointment, OintmenttextBox.Text);
        }

        private void AddSplint_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.Splint, SplinttextBox.Text);
        }

        private void AddStyptic_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.Styptic, StyptictextBox.Text);
        }

        private void AddSutureKit_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.SutureKit, SutureKittextBox.Text);
        }

        private void AddRPill_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.RevivalPill, true);
        }

        private void RemoveRPill_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.RevivalPill, false);
        }

        private void AddCigar_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.Cigar, true);
        }

        private void RemoveCigar_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.Cigar, false);
        }

        private void AddBinos_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.Binoculars, true);
        }

        private void RemoveBinos_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.Binoculars, false);
        }

        private void AddThermal_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.ThermalGoggles, true);
        }

        private void RemoveThermal_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.ThermalGoggles, false);
        }

        private void AddNVG_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.NightVisionGoggles, true);
        }

        private void RemoveNVG_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.NightVisionGoggles, false);
        }

        private void AddCamera_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.Camera, true);
        }

        private void RemoveCamera_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.Camera, false);
        }

        private void AddMotionD_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.MotionDetector, true);
        }

        private void RemoveMotionD_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.MotionDetector, false);
        }

        private void AddSonar_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.ActiveSonar, true);
        }

        private void RemoveSonar_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.ActiveSonar, false);
        }

        private void AddMineD_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.MineDetector, true);
        }

        private void RemoveMineD_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.MineDetector, false);
        }

        private void AddApSensor_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.AntiPersonnelSensor, true);
        }

        private void RemoveApSensor_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.AntiPersonnelSensor, false);
        }

        private void AddBoxA_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.CBoxA, true);
        }

        private void RemoveBoxA_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.CBoxA, false);
        }

        private void AddBoxB_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.CBoxB, true);
        }

        private void RemoveBoxB_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.CBoxB, false);
        }

        private void AddBoxC_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.CBoxC, true);
        }

        private void RemoveBoxC_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.CBoxC, false);
        }

        private void AddBoxD_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.CBoxD, true);
        }

        private void RemoveBoxD_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.CBoxD, false);
        }

        private void AddCrocCap_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.CrocCap, true);
        }

        private void RemoveCrocCap_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.CrocCap, false);
        }

        private void AddKeyA_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.KeyA, true);
        }

        private void RemoveKeyA_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.KeyA, false);
        }

        private void AddKeyB_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.KeyB, true);
        }

        private void RemoveKeyB_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.KeyB, false);
        }

        private void AddKeyC_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.KeyC, true);
        }

        private void RemoveKeyC_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.KeyC, false);
        }

        private void AddBandana_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.Bandana, true);
        }

        private void RemoveBandana_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.Bandana, false);
        }

        private void AddStealth_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.StealthCamo, true);
        }

        private void RemoveStealth_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.StealthCamo, false);
        }

        private void AddMonkey_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.MonkeyMask, true);
        }

        private void RemoveMonkey_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleItemState(MGS3UsableObjects.MonkeyMask, false);
        }

        private void ItemFormSwap_Click(object sender, EventArgs e)
        {
            CamoForm form3 = new();
            form3.Show();
            this.Hide();
        }
    }
}