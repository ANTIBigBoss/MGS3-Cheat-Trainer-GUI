using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class WeaponForm : Form
    {
        public WeaponForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < AllWeaponsChecklist.Items.Count; i++)
            {
                AllWeaponsChecklist.SetItemChecked(i, true);
            }

            M1911A1Dropdown.SelectedIndex = 0;
            MK22Dropdown.SelectedIndex = 0;
            XM16E1Dropdown.SelectedIndex = 0;
            SAADropdown.SelectedIndex = 0;
            M37Dropdown.SelectedIndex = 0;
            SVDDropdown.SelectedIndex = 0;
            AK47Dropdown.SelectedIndex = 0;
            MosinDropdown.SelectedIndex = 0;
            RPG7Dropdown.SelectedIndex = 0;
            M63Dropdown.SelectedIndex = 0;
            ScorpionDropdown.SelectedIndex = 0;
            GrenadeDropdown.SelectedIndex = 0;
            WpGrenadeDropdown.SelectedIndex = 0;
            SmokeGrenadeDropdown.SelectedIndex = 0;
            StunGrenadeDropdown.SelectedIndex = 0;
            ChaffGrenadeDropdown.SelectedIndex = 0;
            MagazineDropdown.SelectedIndex = 0;
            HandkerchiefDropdown.SelectedIndex = 0;
            CigSprayDropdown.SelectedIndex = 0;
            C3Dropdown.SelectedIndex = 0;
            TNTDropdown.SelectedIndex = 0;
            BookDropdown.SelectedIndex = 0;
            ClaymoreDropdown.SelectedIndex = 0;
            MousetrapDropdown.SelectedIndex = 0;

            M1911A1TextBox.Text = "1234";
            MK22TextBox.Text = "1234";
            XM16E1TextBox.Text = "1234";
            SAATextBox.Text = "1234";
            M37TextBox.Text = "1234";
            SVDTextBox.Text = "1234";
            AK47TextBox.Text = "1234";
            MosinTextBox.Text = "1234";
            RPG7TextBox.Text = "1234";
            M63TextBox.Text = "1234";
            ScorpionTextBox.Text = "1234";
            GrenadeTextBox.Text = "1234";
            WpGrenadeTextBox.Text = "1234";
            SmokeGrenadeTextBox.Text = "1234";
            StunGrenadeTextBox.Text = "1234";
            ChaffGrenadeTextBox.Text = "1234";
            MagazineTextBox.Text = "1234";
            HandkerchiefTextBox.Text = "1234";
            CigSprayTextBox.Text = "1234";
            C3TextBox.Text = "1234";
            TNTTextBox.Text = "1234";
            BookTextBox.Text = "1234";
            ClaymoreTextBox.Text = "1234";
            MousetrapTextbox.Text = "1234";
            AllTextbox.Text = "1234";
        }

        private const string CurrentAndMax = "Current/Max Ammo";
        private const string CurrentAmmo = "Current Ammo";
        private const string MaxAndCurrentClipSize = "Current/Max Clip";
        private const string MaxAmmo = "Max Ammo";
        private const string ClipSize = "Clip Size";
        private const string MaxClipSize = "Max Clip Size";
        private const string SuppressorCount = "Suppressor Count";

        // Variables to store the selected option from dropdown menu and associate it to the button
        private string selectedM1911A1Option;
        private string selectedMK22Option;
        private string selectedXM16E1Option;
        private string selectedSAAOption;
        private string selectedM37Option;
        private string selectedSVDOption;
        private string selectedAK47Option;
        private string selectedMosinOption;
        private string selectedRPG7Option;
        private string selectedM63Option;
        private string selectedScorpionOption;
        private string selectedGrenadeOption;
        private string selectedWpGrenadeOption;
        private string selectedSmokeGrenadeOption;
        private string selectedStunGrenadeOption;
        private string selectedChaffGrenadeOption;
        private string selectedMagazineOption;
        private string selectedHandkerchiefOption;
        private string selectedCigSprayOption;
        private string selectedC3Option;
        private string selectedTNTOption;
        private string selectedMousetrapOption;
        private string selectedBookOption;
        private string selectedClaymoreOption;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Closes the application(form1) if this form is closed
        }

        #region Weapon Toggles
        private void AddPatriot_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Patriot, true);
        }

        private void RemovePatriot_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Patriot, false);
        }

        private void AddEz_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.EzGun, true);
        }

        private void RemoveEz_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.EzGun, false);
        }

        private void AddKnife_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.SurvivalKnife, true);
        }

        private void RemoveKnife_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.SurvivalKnife, false);
        }

        private void AddFork_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Fork, true);
        }

        private void RemoveFork_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Fork, false);
        }

        private void AddTorch_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Torch, true);
        }

        private void RemoveTorch_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Torch, false);
        }

        private void AddDMic_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.DirectionalMic, true);
        }

        private void RemoveDMic_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.DirectionalMic, false);
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

        private void HealthFormSwap_Click(object sender, EventArgs e)
        {
            StatsAndAlertForm form5 = new();
            form5.Show();
            this.Hide();
        }

        private void BossFormSwap_Click(object sender, EventArgs e)
        {
            BossForm form6 = new();
            form6.Show();
            this.Hide();
        }

        private void M1911A1Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedM1911A1Option = (string)comboBox.SelectedItem;
        }

        private void ChangeM1911A1_Click(object sender, EventArgs e)
        {
            string textBoxValue = M1911A1TextBox.Text;

            switch (selectedM1911A1Option)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.M1911A1, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.M1911A1, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.M1911A1, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.M1911A1, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.M1911A1, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.M1911A1, textBoxValue);
                    break;
                case SuppressorCount:
                    ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.M1911A1Surpressor, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        // Assuming you have a ComboBox named MK22Dropdown and a TextBox named MK22TextBox in your form
        private void MK22Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedMK22Option = (string)comboBox.SelectedItem;
        }

        private void ChangeMK22_Click(object sender, EventArgs e)
        {
            string textBoxValue = MK22TextBox.Text; // Get the value from the MK.22 TextBox

            switch (selectedMK22Option)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.MK22, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.MK22, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.MK22, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.MK22, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.MK22, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.MK22, textBoxValue);
                    break;
                case SuppressorCount:
                    ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.MK22Surpressor, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void XM16E1Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedXM16E1Option = (string)comboBox.SelectedItem;
        }

        private void ChangeXM16E1_Click(object sender, EventArgs e)
        {
            string textBoxValue = XM16E1TextBox.Text; // Assuming you have a TextBox for XM16E1 user input

            switch (selectedXM16E1Option)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.XM16E1, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.XM16E1, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.XM16E1, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.XM16E1, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.XM16E1, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.XM16E1, textBoxValue);
                    break;
                case SuppressorCount:
                    // Assuming there is a suppressor item for the XM16E1
                    ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.XM16E1Surpressor, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void SAADropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedSAAOption = (string)comboBox.SelectedItem;
        }

        private void ChangeSAA_Click(object sender, EventArgs e)
        {
            string textBoxValue = SAATextBox.Text; // Assuming you have a TextBox for SAA user input

            switch (selectedSAAOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.SAA, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.SAA, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.SAA, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.SAA, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.SAA, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.SAA, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void M37Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedM37Option = (string)comboBox.SelectedItem;
        }

        private void ChangeM37_Click(object sender, EventArgs e)
        {
            string textBoxValue = M37TextBox.Text; // Assuming you have a TextBox for M37 user input

            switch (selectedM37Option)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.M37, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.M37, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.M37, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.M37, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.M37, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.M37, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void SVDDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedSVDOption = (string)comboBox.SelectedItem;
        }

        private void ChangeSVD_Click_1(object sender, EventArgs e)
        {
            string textBoxValue = SVDTextBox.Text; // Assuming you have a TextBox for SVD user input

            switch (selectedSVDOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.SVD, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.SVD, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.SVD, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.SVD, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.SVD, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.SVD, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void MosinDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedMosinOption = (string)comboBox.SelectedItem;
        }

        private void ChangeMosin_Click(object sender, EventArgs e)
        {
            string textBoxValue = MosinTextBox.Text; // Assuming you have a TextBox for Mosin-Nagant user input

            switch (selectedMosinOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.Mosin, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Mosin, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Mosin, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.Mosin, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.Mosin, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.Mosin, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void RPG7Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedRPG7Option = (string)comboBox.SelectedItem;
        }

        private void ChangeRPG7_Click(object sender, EventArgs e)
        {
            string textBoxValue = RPG7TextBox.Text; // Assuming you have a TextBox for RPG-7 user input

            switch (selectedRPG7Option)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.RPG7, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.RPG7, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.RPG7, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.RPG7, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.RPG7, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.RPG7, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void AK47Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedAK47Option = (string)comboBox.SelectedItem;
        }

        private void ChangeAK47_Click(object sender, EventArgs e)
        {
            string textBoxValue = AK47TextBox.Text; // Assuming you have a TextBox for AK-47 user input

            switch (selectedAK47Option)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.AK47, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.AK47, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.AK47, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.AK47, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.AK47, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.AK47, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void M63Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedM63Option = (string)comboBox.SelectedItem;
        }

        private void ChangeM63_Click(object sender, EventArgs e)
        {
            string textBoxValue = M63TextBox.Text;

            switch (selectedM63Option)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.M63, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.M63, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.M63, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.M63, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.M63, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.M63, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void ScorpionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedScorpionOption = (string)comboBox.SelectedItem;
        }

        private void ChangeScorpion_Click(object sender, EventArgs e)
        {
            string textBoxValue = ScorpionTextBox.Text;

            switch (selectedScorpionOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.Scorpion, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Scorpion, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Scorpion, textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.Scorpion, textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.Scorpion, textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.Scorpion, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void GrenadeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedGrenadeOption = (string)comboBox.SelectedItem;
        }

        private void ChangeGrenade_Click(object sender, EventArgs e)
        {
            string textBoxValue = GrenadeTextBox.Text;

            switch (selectedGrenadeOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.Grenade, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Grenade, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Grenade, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }

        }

        private void WpGrenadeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedWpGrenadeOption = (string)comboBox.SelectedItem;
        }

        private void ChangeWp_Click(object sender, EventArgs e)
        {
            string textBoxValue = WpGrenadeTextBox.Text;

            switch (selectedWpGrenadeOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.WpGrenade, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.WpGrenade, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.WpGrenade, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }

        }

        private void SmokeGrenadeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedSmokeGrenadeOption = (string)comboBox.SelectedItem;
        }

        private void ChangeSmoke_Click(object sender, EventArgs e)
        {
            string textBoxValue = SmokeGrenadeTextBox.Text;

            switch (selectedSmokeGrenadeOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.SmokeGrenade, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.SmokeGrenade, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.SmokeGrenade, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }

        }

        private void StunGrenadeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedStunGrenadeOption = (string)comboBox.SelectedItem;
        }

        private void ChangeStun_Click(object sender, EventArgs e)
        {
            string textBoxValue = StunGrenadeTextBox.Text;

            switch (selectedStunGrenadeOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.StunGrenade, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.StunGrenade, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.StunGrenade, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }

        }

        private void ChaffGrenadeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedChaffGrenadeOption = (string)comboBox.SelectedItem;
        }

        private void ChangeChaff_Click(object sender, EventArgs e)
        {
            string textBoxValue = ChaffGrenadeTextBox.Text;

            switch (selectedChaffGrenadeOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.ChaffGrenade, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.ChaffGrenade, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.ChaffGrenade, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }

        }

        private void MagazineDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedMagazineOption = (string)comboBox.SelectedItem;
        }

        private void ChangeMagazine_Click(object sender, EventArgs e)
        {
            string textBoxValue = MagazineTextBox.Text;

            switch (selectedMagazineOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.EmptyMag, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.EmptyMag, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.EmptyMag, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }

        }

        private void HandkerchiefDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedHandkerchiefOption = (string)comboBox.SelectedItem;
        }

        private void ChangeHandkerchief_Click(object sender, EventArgs e)
        {
            string textBoxValue = HandkerchiefTextBox.Text;

            switch (selectedHandkerchiefOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.Handkerchief, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Handkerchief, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Handkerchief, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void CigSprayDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedCigSprayOption = (string)comboBox.SelectedItem;
        }

        private void ChangeCigSpray_Click(object sender, EventArgs e)
        {
            string textBoxValue = CigSprayTextBox.Text;

            switch (selectedCigSprayOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.CigSpray, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.CigSpray, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.CigSpray, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void C3Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedC3Option = (string)comboBox.SelectedItem;
        }

        private void ChangeC3_Click(object sender, EventArgs e)
        {
            string textBoxValue = C3TextBox.Text;

            switch (selectedC3Option)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.C3, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.C3, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.C3, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void TNTDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedTNTOption = (string)comboBox.SelectedItem;
        }

        private void ChangeTNT_Click(object sender, EventArgs e)
        {
            string textBoxValue = TNTTextBox.Text;

            switch (selectedTNTOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.TNT, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.TNT, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.TNT, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void BookDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedBookOption = (string)comboBox.SelectedItem;
        }

        private void ChangeBook_Click(object sender, EventArgs e)
        {
            string textBoxValue = BookTextBox.Text;

            switch (selectedBookOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.Book, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Book, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Book, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void ClaymoreDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedClaymoreOption = (string)comboBox.SelectedItem;
        }

        private void ChangeClaymore_Click(object sender, EventArgs e)
        {
            string textBoxValue = ClaymoreTextBox.Text;

            switch (selectedClaymoreOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.Claymore, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Claymore, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Claymore, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void MousetrapDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedMousetrapOption = (string)comboBox.SelectedItem;
        }

        private void ChangeMousetrap_Click(object sender, EventArgs e)
        {
            string textBoxValue = MousetrapTextbox.Text;

            switch (selectedMousetrapOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.Mousetrap, textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Mousetrap, textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Mousetrap, textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    break;
            }
        }

        private void AllWeaponsChecklist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ChangeAllChecked_Click(object sender, EventArgs e)
        {
            // Ensure the ammo value is valid
            if (!short.TryParse(AllTextbox.Text, out short ammoValue))
            {
                MessageBox.Show("Invalid ammo value.");
                return;
            }

            // Iterate over each checked item in the checklist
            foreach (var itemChecked in AllWeaponsChecklist.CheckedItems)
            {
                var weaponName = itemChecked.ToString();
                var weapon = GetWeaponByName(weaponName);

                if (weapon != null)
                {
                    // Apply ammo and max ammo changes to the selected weapon
                    ItemWeaponManager.ModifyAmmo(weapon, ammoValue.ToString());
                    ItemWeaponManager.ModifyMaxAmmo(weapon, ammoValue.ToString());
                }
            }
        }

        private Weapon GetWeaponByName(string name)
        {
            // Example implementation mapping string names to Weapon objects
            switch (name)
            {
                case "M1911A1": return MGS3UsableObjects.M1911A1;
                case "MK22": return MGS3UsableObjects.MK22;
                case "XM16E1": return MGS3UsableObjects.XM16E1;
                case "SAA": return MGS3UsableObjects.SAA;
                case "M37": return MGS3UsableObjects.M37;
                case "SVD": return MGS3UsableObjects.SVD;
                case "AK-47": return MGS3UsableObjects.AK47;
                case "Mosin Nagant": return MGS3UsableObjects.Mosin;
                case "RPG-7": return MGS3UsableObjects.RPG7;
                case "M63": return MGS3UsableObjects.M63;
                case "Scorpion": return MGS3UsableObjects.Scorpion;
                case "Grenade": return MGS3UsableObjects.Grenade;
                case "Wp Grenade": return MGS3UsableObjects.WpGrenade;
                case "Smoke Grenade": return MGS3UsableObjects.SmokeGrenade;
                case "Stun Grenade": return MGS3UsableObjects.StunGrenade;
                case "Chaff Grenade": return MGS3UsableObjects.ChaffGrenade;
                case "Empty Magazine": return MGS3UsableObjects.EmptyMag;
                case "Handkerchief": return MGS3UsableObjects.Handkerchief;
                case "Cig Spray": return MGS3UsableObjects.CigSpray;
                case "C3": return MGS3UsableObjects.C3;
                case "TNT": return MGS3UsableObjects.TNT;
                case "Book": return MGS3UsableObjects.Book;
                case "Claymore": return MGS3UsableObjects.Claymore;
                case "Mousetrap": return MGS3UsableObjects.Mousetrap;
                default: return null;
            }
        }

        
    }
}