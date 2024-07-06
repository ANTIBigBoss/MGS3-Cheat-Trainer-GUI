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

        private void WeaponForm_Load(object sender, EventArgs e)
        {
            this.Location = MemoryManager.GetLastFormLocation();

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

            M1911A1TextBox.Text = "999";
            MK22TextBox.Text = "999";
            XM16E1TextBox.Text = "999";
            SAATextBox.Text = "999";
            M37TextBox.Text = "999";
            SVDTextBox.Text = "999";
            AK47TextBox.Text = "999";
            MosinTextBox.Text = "999";
            RPG7TextBox.Text = "999";
            M63TextBox.Text = "999";
            ScorpionTextBox.Text = "999";
            GrenadeTextBox.Text = "999";
            WpGrenadeTextBox.Text = "999";
            SmokeGrenadeTextBox.Text = "999";
            StunGrenadeTextBox.Text = "999";
            ChaffGrenadeTextBox.Text = "999";
            MagazineTextBox.Text = "999";
            HandkerchiefTextBox.Text = "999";
            CigSprayTextBox.Text = "999";
            C3TextBox.Text = "999";
            TNTTextBox.Text = "999";
            BookTextBox.Text = "999";
            ClaymoreTextBox.Text = "999";
            MousetrapTextbox.Text = "999";
            AllTextbox.Text = "999";

            CheckInfiniteAmmoStatus();
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
            LoggingManager.Instance.Log("Added Patriot");
        }

        private void RemovePatriot_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Patriot, false);
            LoggingManager.Instance.Log("Removed Patriot");
        }

        private void AddEz_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.EzGun, true);
            LoggingManager.Instance.Log("Added EZ Gun");
        }

        private void RemoveEz_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.EzGun, false);
            LoggingManager.Instance.Log("Removed EZ Gun");
        }

        private void AddKnife_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.SurvivalKnife, true);
            LoggingManager.Instance.Log("Added Survival Knife");
        }

        private void RemoveKnife_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.SurvivalKnife, false);
            LoggingManager.Instance.Log("Removed Survival Knife");
        }

        private void AddFork_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Fork, true);
            LoggingManager.Instance.Log("Added Fork");
        }

        private void RemoveFork_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Fork, false);
            LoggingManager.Instance.Log("Removed Fork");
        }

        private void AddTorch_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Torch, true);
            LoggingManager.Instance.Log("Added Torch");
        }

        private void RemoveTorch_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.Torch, false);
            LoggingManager.Instance.Log("Removed Torch");
        }

        private void AddDMic_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.DirectionalMic, true);
            LoggingManager.Instance.Log("Added Directional Mic");
        }

        private void RemoveDMic_Click(object sender, EventArgs e)
        {
            ItemWeaponManager.ToggleWeapon(MGS3UsableObjects.DirectionalMic, false);
            LoggingManager.Instance.Log("Removed Directional Mic");
        }
        #endregion

        private void M1911A1Dropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed M1911A1 Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.M1911A1, textBoxValue);
                    LoggingManager.Instance.Log("Changed M1911A1 Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.M1911A1, textBoxValue);
                    LoggingManager.Instance.Log("Changed M1911A1 Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.M1911A1, textBoxValue);
                    LoggingManager.Instance.Log("Changed M1911A1 Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.M1911A1, textBoxValue);
                    LoggingManager.Instance.Log("Changed M1911A1 Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.M1911A1, textBoxValue);
                    LoggingManager.Instance.Log("Changed M1911A1 Max Clip Size to: " + textBoxValue);
                    break;
                case SuppressorCount:
                    ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.M1911A1Surpressor, textBoxValue);
                    LoggingManager.Instance.Log("Changed M1911A1 Suppressor Count to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the M1911A1.");
                    break;
            }
        }

        // Assuming you have a ComboBox named MK22Dropdown and a TextBox named MK22TextBox in your form
        private void MK22Dropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed MK22 Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.MK22, textBoxValue);
                    LoggingManager.Instance.Log("Changed MK22 Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.MK22, textBoxValue);
                    LoggingManager.Instance.Log("Changed MK22 Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.MK22, textBoxValue);
                    LoggingManager.Instance.Log("Changed MK22 Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.MK22, textBoxValue);
                    LoggingManager.Instance.Log("Changed MK22 Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.MK22, textBoxValue);
                    LoggingManager.Instance.Log("Changed MK22 Max Clip Size to: " + textBoxValue);
                    break;
                case SuppressorCount:
                    ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.MK22Surpressor, textBoxValue);
                    LoggingManager.Instance.Log("Changed MK22 Suppressor Count to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the MK22.");
                    break;
            }
        }

        private void XM16E1Dropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed XM16E1 Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.XM16E1, textBoxValue);
                    LoggingManager.Instance.Log("Changed XM16E1 Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.XM16E1, textBoxValue);
                    LoggingManager.Instance.Log("Changed XM16E1 Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.XM16E1, textBoxValue);
                    LoggingManager.Instance.Log("Changed XM16E1 Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.XM16E1, textBoxValue);
                    LoggingManager.Instance.Log("Changed XM16E1 Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.XM16E1, textBoxValue);
                    LoggingManager.Instance.Log("Changed XM16E1 Max Clip Size to: " + textBoxValue);
                    break;
                case SuppressorCount:
                    // Assuming there is a suppressor item for the XM16E1
                    ItemWeaponManager.ModifyItemCapacity(MGS3UsableObjects.XM16E1Surpressor, textBoxValue);
                    LoggingManager.Instance.Log("Changed XM16E1 Suppressor Count to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the XM16E1.");
                    break;
            }
        }

        private void SAADropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed SAA Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.SAA, textBoxValue);
                    LoggingManager.Instance.Log("Changed SAA Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.SAA, textBoxValue);
                    LoggingManager.Instance.Log("Changed SAA Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.SAA, textBoxValue);
                    LoggingManager.Instance.Log("Changed SAA Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.SAA, textBoxValue);
                    LoggingManager.Instance.Log("Changed SAA Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.SAA, textBoxValue);
                    LoggingManager.Instance.Log("Changed SAA Max Clip Size to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the SAA.");
                    break;
            }
        }

        private void M37Dropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed M37 Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.M37, textBoxValue);
                    LoggingManager.Instance.Log("Changed M37 Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.M37, textBoxValue);
                    LoggingManager.Instance.Log("Changed M37 Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.M37, textBoxValue);
                    LoggingManager.Instance.Log("Changed M37 Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.M37, textBoxValue);
                    LoggingManager.Instance.Log("Changed M37 Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.M37, textBoxValue);
                    LoggingManager.Instance.Log("Changed M37 Max Clip Size to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the M37.");
                    break;
            }
        }

        private void SVDDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedSVDOption = (string)comboBox.SelectedItem;
        }

        private void ChangeSVD_Click(object sender, EventArgs e)
        {
            string textBoxValue = SVDTextBox.Text; // Assuming you have a TextBox for SVD user input

            switch (selectedSVDOption)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.SVD, textBoxValue);
                    LoggingManager.Instance.Log("Changed SVD Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.SVD, textBoxValue);
                    LoggingManager.Instance.Log("Changed SVD Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.SVD, textBoxValue);
                    LoggingManager.Instance.Log("Changed SVD Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.SVD, textBoxValue);
                    LoggingManager.Instance.Log("Changed SVD Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.SVD, textBoxValue);
                    LoggingManager.Instance.Log("Changed SVD Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.SVD, textBoxValue);
                    LoggingManager.Instance.Log("Changed SVD Max Clip Size to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the SVD.");
                    break;
            }
        }

        private void MosinDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Mosin Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Mosin, textBoxValue);
                    LoggingManager.Instance.Log("Changed Mosin Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Mosin, textBoxValue);
                    LoggingManager.Instance.Log("Changed Mosin Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.Mosin, textBoxValue);
                    LoggingManager.Instance.Log("Changed Mosin Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.Mosin, textBoxValue);
                    LoggingManager.Instance.Log("Changed Mosin Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.Mosin, textBoxValue);
                    LoggingManager.Instance.Log("Changed Mosin Max Clip Size to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Mosin.");
                    break;
            }
        }

        private void RPG7Dropdown_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            selectedRPG7Option = (string)comboBox.SelectedItem;
        }

        private void ChangeRPG_Click(object sender, EventArgs e)
        {
            string textBoxValue = RPG7TextBox.Text; // Assuming you have a TextBox for RPG-7 user input

            switch (selectedRPG7Option)
            {
                case CurrentAndMax:
                    ItemWeaponManager.ModifyCurrentAndMaxAmmo(MGS3UsableObjects.RPG7, textBoxValue);
                    LoggingManager.Instance.Log("Changed RPG7 Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.RPG7, textBoxValue);
                    LoggingManager.Instance.Log("Changed RPG7 Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.RPG7, textBoxValue);
                    LoggingManager.Instance.Log("Changed RPG7 Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.RPG7, textBoxValue);
                    LoggingManager.Instance.Log("Changed RPG7 Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.RPG7, textBoxValue);
                    LoggingManager.Instance.Log("Changed RPG7 Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.RPG7, textBoxValue);
                    LoggingManager.Instance.Log("Changed RPG7 Max Clip Size to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the RPG7.");
                    break;
            }
        }

        private void AK47Dropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed AK47 Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.AK47, textBoxValue);
                    LoggingManager.Instance.Log("Changed AK47 Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.AK47, textBoxValue);
                    LoggingManager.Instance.Log("Changed AK47 Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.AK47, textBoxValue);
                    LoggingManager.Instance.Log("Changed AK47 Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.AK47, textBoxValue);
                    LoggingManager.Instance.Log("Changed AK47 Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.AK47, textBoxValue);
                    LoggingManager.Instance.Log("Changed AK47 Max Clip Size to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the AK47.");
                    break;
            }
        }

        private void M63Dropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed M63 Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.M63, textBoxValue);
                    LoggingManager.Instance.Log("Changed M63 Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.M63, textBoxValue);
                    LoggingManager.Instance.Log("Changed M63 Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.M63, textBoxValue);
                    LoggingManager.Instance.Log("Changed M63 Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.M63, textBoxValue);
                    LoggingManager.Instance.Log("Changed M63 Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.M63, textBoxValue);
                    LoggingManager.Instance.Log("Changed M63 Max Clip Size to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the M63.");
                    break;
            }
        }

        private void ScorpionDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Scorpion Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Scorpion, textBoxValue);
                    LoggingManager.Instance.Log("Changed Scorpion Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Scorpion, textBoxValue);
                    LoggingManager.Instance.Log("Changed Scorpion Max Ammo to: " + textBoxValue);
                    break;
                case MaxAndCurrentClipSize:
                    ItemWeaponManager.ModifyCurrentAndMaxClipSize(MGS3UsableObjects.Scorpion, textBoxValue);
                    LoggingManager.Instance.Log("Changed Scorpion Current and Max Clip Size to: " + textBoxValue);
                    break;
                case ClipSize:
                    ItemWeaponManager.ModifyClipSize(MGS3UsableObjects.Scorpion, textBoxValue);
                    LoggingManager.Instance.Log("Changed Scorpion Clip Size to: " + textBoxValue);
                    break;
                case MaxClipSize:
                    ItemWeaponManager.ModifyMaxClipSize(MGS3UsableObjects.Scorpion, textBoxValue);
                    LoggingManager.Instance.Log("Changed Scorpion Max Clip Size to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Scorpion.");
                    break;
            }
        }

        private void GrenadeDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Grenade Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Grenade, textBoxValue);
                    LoggingManager.Instance.Log("Changed Grenade Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Grenade, textBoxValue);
                    LoggingManager.Instance.Log("Changed Grenade Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Grenade.");
                    break;
            }

        }

        private void WpGrenadeDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Wp Grenade Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.WpGrenade, textBoxValue);
                    LoggingManager.Instance.Log("Changed Wp Grenade Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.WpGrenade, textBoxValue);
                    LoggingManager.Instance.Log("Changed Wp Grenade Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Wp Grenade.");
                    break;
            }

        }

        private void SmokeGrenadeDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Smoke Grenade Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.SmokeGrenade, textBoxValue);
                    LoggingManager.Instance.Log("Changed Smoke Grenade Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.SmokeGrenade, textBoxValue);
                    LoggingManager.Instance.Log("Changed Smoke Grenade Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Smoke Grenade.");
                    break;
            }

        }

        private void StunGrenadeDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Stun Grenade Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.StunGrenade, textBoxValue);
                    LoggingManager.Instance.Log("Changed Stun Grenade Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.StunGrenade, textBoxValue);
                    LoggingManager.Instance.Log("Changed Stun Grenade Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Stun Grenade.");
                    break;
            }

        }

        private void ChaffGrenadeDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Chaff Grenade Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.ChaffGrenade, textBoxValue);
                    LoggingManager.Instance.Log("Changed Chaff Grenade Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.ChaffGrenade, textBoxValue);
                    LoggingManager.Instance.Log("Changed Chaff Grenade Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Chaff Grenade.");
                    break;
            }

        }

        private void MagazineDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Empty Magazine Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.EmptyMag, textBoxValue);
                    LoggingManager.Instance.Log("Changed Empty Magazine Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.EmptyMag, textBoxValue);
                    LoggingManager.Instance.Log("Changed Empty Magazine Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Empty Magazine.");
                    break;
            }

        }

        private void HandkerchiefDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Handkerchief Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Handkerchief, textBoxValue);
                    LoggingManager.Instance.Log("Changed Handkerchief Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Handkerchief, textBoxValue);
                    LoggingManager.Instance.Log("Changed Handkerchief Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Handkerchief.");
                    break;
            }
        }

        private void CigSprayDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Cig Spray Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.CigSpray, textBoxValue);
                    LoggingManager.Instance.Log("Changed Cig Spray Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.CigSpray, textBoxValue);
                    LoggingManager.Instance.Log("Changed Cig Spray Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Cig Spray.");
                    break;
            }
        }

        private void C3Dropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed C3 Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.C3, textBoxValue);
                    LoggingManager.Instance.Log("Changed C3 Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.C3, textBoxValue);
                    LoggingManager.Instance.Log("Changed C3 Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the C3.");
                    break;
            }
        }

        private void TNTDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed TNT Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.TNT, textBoxValue);
                    LoggingManager.Instance.Log("Changed TNT Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.TNT, textBoxValue);
                    LoggingManager.Instance.Log("Changed TNT Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the TNT.");
                    break;
            }
        }

        private void BookDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Book Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Book, textBoxValue);
                    LoggingManager.Instance.Log("Changed Book Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Book, textBoxValue);
                    LoggingManager.Instance.Log("Changed Book Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Book.");
                    break;
            }
        }

        private void ClaymoreDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Claymore Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Claymore, textBoxValue);
                    LoggingManager.Instance.Log("Changed Claymore Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Claymore, textBoxValue);
                    LoggingManager.Instance.Log("Changed Claymore Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Claymore.");
                    break;
            }
        }

        private void MousetrapDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    LoggingManager.Instance.Log("Changed Mousetrap Current and Max Ammo to: " + textBoxValue);
                    break;
                case CurrentAmmo:
                    ItemWeaponManager.ModifyAmmo(MGS3UsableObjects.Mousetrap, textBoxValue);
                    LoggingManager.Instance.Log("Changed Mousetrap Current Ammo to: " + textBoxValue);
                    break;
                case MaxAmmo:
                    ItemWeaponManager.ModifyMaxAmmo(MGS3UsableObjects.Mousetrap, textBoxValue);
                    LoggingManager.Instance.Log("Changed Mousetrap Max Ammo to: " + textBoxValue);
                    break;
                default:
                    MessageBox.Show("Invalid option selected or no option selected.");
                    LoggingManager.Instance.Log("Invalid option selected or no option selected for the Mousetrap.");
                    break;
            }
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
                    LoggingManager.Instance.Log("Changed " + weaponName + " Current Ammo to: " + ammoValue);
                    ItemWeaponManager.ModifyMaxAmmo(weapon, ammoValue.ToString());
                    LoggingManager.Instance.Log("Changed " + weaponName + " Max Ammo to: " + ammoValue);
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

        private void button6_Click(object sender, EventArgs e)
        {
            ItemForm form2 = new ItemForm();
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "ItemForm");
            form2.Show();
            this.Hide();
            LoggingManager.Instance.Log("Navigating to Item Form from the Weapon Form");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CamoForm form3 = new CamoForm();
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "CamoForm");
            form3.Show();
            this.Hide();
            LoggingManager.Instance.Log("Navigating to Camo Form from the Weapon Form");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MiscForm form4 = new MiscForm();
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "MiscForm");
            form4.Show();
            this.Hide();
            LoggingManager.Instance.Log("Navigating to Misc Form from the Weapon Form");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StatsAndAlertForm form5 = new();
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "StatsAndAlertForm");
            form5.Show();
            this.Hide();
            LoggingManager.Instance.Log("Navigating to Stats and Alert Form from the Weapon Form");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BossForm form6 = new();
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "BossForm");
            form6.Show();
            this.Hide();
            LoggingManager.Instance.Log("Navigating to Boss Form from the Weapon Form");
        }

        private void InfAmmoNoReloadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (InfAmmoNoReloadCheckBox.Checked)
            {
                MiscManager.Instance.EnableInfAmmoAndReload(); 
            }

            else
            {
                MiscManager.Instance.DisableInfAmmoAndReload();
            }
        }

        public void CheckInfiniteAmmoStatus()
        {
            if (!MiscManager.Instance.IsAmmoAndReloadFinite())
            {
                
                InfAmmoNoReloadCheckBox.Checked = true;
            }

            else
            {
                InfAmmoNoReloadCheckBox.Checked = false;
            }

        }
    }
}