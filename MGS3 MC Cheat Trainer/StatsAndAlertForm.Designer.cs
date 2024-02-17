namespace MGS3_MC_Cheat_Trainer
{
    partial class StatsAndAlertForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private ColouredProgressBar AlertProgressBar;
        private ColouredProgressBar EvasionProgressBar;
        private ColouredProgressBar CautionProgressBar;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatsAndAlertForm));
            MaxHpTo1 = new Button();
            Minus100MaxHpValue = new Button();
            Plus100MaxHpValue = new Button();
            textBox1 = new TextBox();
            FullStamina30000Value = new Button();
            SetStaminaToZero = new Button();
            Minus10000StaminaValue = new Button();
            Plus10000StaminaValue = new Button();
            CurrentHpTo1 = new Button();
            ZeroHP = new Button();
            Minus100HpValue = new Button();
            Plus100HpValue = new Button();
            InfiniteCaution = new CheckBox();
            InfiniteAlert = new CheckBox();
            CautionButton = new Button();
            textBox2 = new TextBox();
            AlertButton = new Button();
            AlertProgressBar = new ColouredProgressBar();
            EvasionProgressBar = new ColouredProgressBar();
            CautionProgressBar = new ColouredProgressBar();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            EvasionButton = new Button();
            InfiniteEvasion = new CheckBox();
            HealthFormSwap = new Button();
            MiscFormSwap = new Button();
            CamoFormSwap = new Button();
            WeaponFormSwap = new Button();
            ClearCautionAndEvasion = new Button();
            textBox3 = new TextBox();
            BurnInjury = new Button();
            CutInjury = new Button();
            GunshotRifleInjury = new Button();
            GunshotShotgunInjury = new Button();
            BoneFractureInjury = new Button();
            BulletBeeInjury = new Button();
            LeechesInjury = new Button();
            ArrowInjury = new Button();
            TranqInjury = new Button();
            VenomPoisoningInjury = new Button();
            FoodPoisoningInjury = new Button();
            CommonColdInjury = new Button();
            RemoveInjuries = new Button();
            SwapToBossForm = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // MaxHpTo1
            // 
            MaxHpTo1.BackgroundImage = (Image)resources.GetObject("MaxHpTo1.BackgroundImage");
            MaxHpTo1.FlatStyle = FlatStyle.Flat;
            MaxHpTo1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            MaxHpTo1.Location = new Point(13, 323);
            MaxHpTo1.Name = "MaxHpTo1";
            MaxHpTo1.Size = new Size(228, 31);
            MaxHpTo1.TabIndex = 517;
            MaxHpTo1.Text = "Max HP to 1";
            MaxHpTo1.UseVisualStyleBackColor = true;
            MaxHpTo1.Click += MaxHpTo1_Click;
            // 
            // Minus100MaxHpValue
            // 
            Minus100MaxHpValue.BackgroundImage = (Image)resources.GetObject("Minus100MaxHpValue.BackgroundImage");
            Minus100MaxHpValue.FlatStyle = FlatStyle.Flat;
            Minus100MaxHpValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            Minus100MaxHpValue.Location = new Point(13, 251);
            Minus100MaxHpValue.Name = "Minus100MaxHpValue";
            Minus100MaxHpValue.Size = new Size(228, 31);
            Minus100MaxHpValue.TabIndex = 516;
            Minus100MaxHpValue.Text = "Decrease Max Health by 100";
            Minus100MaxHpValue.UseVisualStyleBackColor = true;
            Minus100MaxHpValue.Click += Minus100MaxHpValue_Click;
            // 
            // Plus100MaxHpValue
            // 
            Plus100MaxHpValue.BackgroundImage = (Image)resources.GetObject("Plus100MaxHpValue.BackgroundImage");
            Plus100MaxHpValue.FlatStyle = FlatStyle.Flat;
            Plus100MaxHpValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            Plus100MaxHpValue.Location = new Point(13, 215);
            Plus100MaxHpValue.Name = "Plus100MaxHpValue";
            Plus100MaxHpValue.Size = new Size(228, 31);
            Plus100MaxHpValue.TabIndex = 515;
            Plus100MaxHpValue.Text = "Increase Max HP by 100";
            Plus100MaxHpValue.UseVisualStyleBackColor = true;
            Plus100MaxHpValue.Click += Plus100MaxHpValue_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ActiveCaptionText;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox1.ForeColor = SystemColors.ActiveCaptionText;
            textBox1.Location = new Point(14, 60);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Snake's stats";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(227, 34);
            textBox1.TabIndex = 514;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // FullStamina30000Value
            // 
            FullStamina30000Value.BackgroundImage = (Image)resources.GetObject("FullStamina30000Value.BackgroundImage");
            FullStamina30000Value.FlatStyle = FlatStyle.Flat;
            FullStamina30000Value.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            FullStamina30000Value.Location = new Point(13, 467);
            FullStamina30000Value.Name = "FullStamina30000Value";
            FullStamina30000Value.Size = new Size(228, 31);
            FullStamina30000Value.TabIndex = 511;
            FullStamina30000Value.Text = "Max Stamina";
            FullStamina30000Value.UseVisualStyleBackColor = true;
            FullStamina30000Value.Click += FullStamina30000Value_Click;
            // 
            // SetStaminaToZero
            // 
            SetStaminaToZero.BackgroundImage = (Image)resources.GetObject("SetStaminaToZero.BackgroundImage");
            SetStaminaToZero.FlatStyle = FlatStyle.Flat;
            SetStaminaToZero.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SetStaminaToZero.Location = new Point(13, 359);
            SetStaminaToZero.Name = "SetStaminaToZero";
            SetStaminaToZero.Size = new Size(228, 31);
            SetStaminaToZero.TabIndex = 510;
            SetStaminaToZero.Text = "0 Stamina";
            SetStaminaToZero.UseVisualStyleBackColor = true;
            SetStaminaToZero.Click += SetStaminaToZero_Click;
            // 
            // Minus10000StaminaValue
            // 
            Minus10000StaminaValue.BackgroundImage = (Image)resources.GetObject("Minus10000StaminaValue.BackgroundImage");
            Minus10000StaminaValue.FlatStyle = FlatStyle.Flat;
            Minus10000StaminaValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            Minus10000StaminaValue.Location = new Point(13, 431);
            Minus10000StaminaValue.Name = "Minus10000StaminaValue";
            Minus10000StaminaValue.Size = new Size(228, 31);
            Minus10000StaminaValue.TabIndex = 509;
            Minus10000StaminaValue.Text = "Decrease Stamina by 100";
            Minus10000StaminaValue.UseVisualStyleBackColor = true;
            Minus10000StaminaValue.Click += Minus10000StaminaValue_Click;
            // 
            // Plus10000StaminaValue
            // 
            Plus10000StaminaValue.BackgroundImage = (Image)resources.GetObject("Plus10000StaminaValue.BackgroundImage");
            Plus10000StaminaValue.FlatStyle = FlatStyle.Flat;
            Plus10000StaminaValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            Plus10000StaminaValue.Location = new Point(13, 395);
            Plus10000StaminaValue.Name = "Plus10000StaminaValue";
            Plus10000StaminaValue.Size = new Size(228, 31);
            Plus10000StaminaValue.TabIndex = 508;
            Plus10000StaminaValue.Text = "Increase Stamina";
            Plus10000StaminaValue.UseVisualStyleBackColor = true;
            Plus10000StaminaValue.Click += Plus10000StaminaValue_Click;
            // 
            // CurrentHpTo1
            // 
            CurrentHpTo1.BackgroundImage = (Image)resources.GetObject("CurrentHpTo1.BackgroundImage");
            CurrentHpTo1.FlatStyle = FlatStyle.Flat;
            CurrentHpTo1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentHpTo1.Location = new Point(13, 287);
            CurrentHpTo1.Name = "CurrentHpTo1";
            CurrentHpTo1.Size = new Size(228, 31);
            CurrentHpTo1.TabIndex = 506;
            CurrentHpTo1.Text = "Current HP to 1";
            CurrentHpTo1.UseVisualStyleBackColor = true;
            CurrentHpTo1.Click += CurrentHpTo1_Click;
            // 
            // ZeroHP
            // 
            ZeroHP.BackgroundImage = (Image)resources.GetObject("ZeroHP.BackgroundImage");
            ZeroHP.FlatStyle = FlatStyle.Flat;
            ZeroHP.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ZeroHP.Location = new Point(12, 109);
            ZeroHP.Name = "ZeroHP";
            ZeroHP.Size = new Size(228, 31);
            ZeroHP.TabIndex = 505;
            ZeroHP.Text = "Kill Snake (0 HP)";
            ZeroHP.UseVisualStyleBackColor = true;
            ZeroHP.Click += ZeroHP_Click;
            // 
            // Minus100HpValue
            // 
            Minus100HpValue.BackgroundImage = (Image)resources.GetObject("Minus100HpValue.BackgroundImage");
            Minus100HpValue.FlatStyle = FlatStyle.Flat;
            Minus100HpValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            Minus100HpValue.Location = new Point(13, 180);
            Minus100HpValue.Name = "Minus100HpValue";
            Minus100HpValue.Size = new Size(228, 31);
            Minus100HpValue.TabIndex = 504;
            Minus100HpValue.Text = "Decrease Health by 100";
            Minus100HpValue.UseVisualStyleBackColor = true;
            Minus100HpValue.Click += Minus100HpValue_Click;
            // 
            // Plus100HpValue
            // 
            Plus100HpValue.BackgroundImage = (Image)resources.GetObject("Plus100HpValue.BackgroundImage");
            Plus100HpValue.FlatStyle = FlatStyle.Flat;
            Plus100HpValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            Plus100HpValue.Location = new Point(13, 145);
            Plus100HpValue.Name = "Plus100HpValue";
            Plus100HpValue.Size = new Size(228, 31);
            Plus100HpValue.TabIndex = 503;
            Plus100HpValue.Text = "Increase HP by 100";
            Plus100HpValue.UseVisualStyleBackColor = true;
            Plus100HpValue.Click += Plus100HpValue_Click;
            // 
            // InfiniteCaution
            // 
            InfiniteCaution.AutoSize = true;
            InfiniteCaution.BackgroundImage = (Image)resources.GetObject("InfiniteCaution.BackgroundImage");
            InfiniteCaution.FlatStyle = FlatStyle.Flat;
            InfiniteCaution.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            InfiniteCaution.Location = new Point(859, 573);
            InfiniteCaution.MaximumSize = new Size(241, 55);
            InfiniteCaution.MinimumSize = new Size(241, 55);
            InfiniteCaution.Name = "InfiniteCaution";
            InfiniteCaution.Size = new Size(241, 55);
            InfiniteCaution.TabIndex = 522;
            InfiniteCaution.Text = "Infinite Caution Mode";
            InfiniteCaution.UseVisualStyleBackColor = true;
            InfiniteCaution.CheckedChanged += InfiniteCaution_CheckedChanged;
            // 
            // InfiniteAlert
            // 
            InfiniteAlert.AutoSize = true;
            InfiniteAlert.BackgroundImage = (Image)resources.GetObject("InfiniteAlert.BackgroundImage");
            InfiniteAlert.FlatStyle = FlatStyle.Flat;
            InfiniteAlert.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            InfiniteAlert.Location = new Point(861, 212);
            InfiniteAlert.MaximumSize = new Size(241, 55);
            InfiniteAlert.MinimumSize = new Size(241, 55);
            InfiniteAlert.Name = "InfiniteAlert";
            InfiniteAlert.Size = new Size(241, 55);
            InfiniteAlert.TabIndex = 521;
            InfiniteAlert.Text = "Infinite Alert Mode";
            InfiniteAlert.UseVisualStyleBackColor = true;
            InfiniteAlert.CheckedChanged += InfiniteAlert_CheckedChanged;
            // 
            // CautionButton
            // 
            CautionButton.BackgroundImage = (Image)resources.GetObject("CautionButton.BackgroundImage");
            CautionButton.Cursor = Cursors.Hand;
            CautionButton.FlatStyle = FlatStyle.Flat;
            CautionButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            CautionButton.ImageAlign = ContentAlignment.TopCenter;
            CautionButton.Location = new Point(859, 515);
            CautionButton.Name = "CautionButton";
            CautionButton.Size = new Size(241, 55);
            CautionButton.TabIndex = 520;
            CautionButton.Text = "Caution On - Doesn't Work during Alert or Evasion";
            CautionButton.UseVisualStyleBackColor = true;
            CautionButton.Click += button9_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.ActiveCaptionText;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox2.ForeColor = SystemColors.ActiveCaptionText;
            textBox2.Location = new Point(861, 60);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Alert Statuses";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(241, 34);
            textBox2.TabIndex = 519;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // AlertButton
            // 
            AlertButton.BackgroundImage = (Image)resources.GetObject("AlertButton.BackgroundImage");
            AlertButton.Cursor = Cursors.Hand;
            AlertButton.FlatStyle = FlatStyle.Flat;
            AlertButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            AlertButton.ImageAlign = ContentAlignment.TopCenter;
            AlertButton.Location = new Point(861, 159);
            AlertButton.Name = "AlertButton";
            AlertButton.Size = new Size(241, 50);
            AlertButton.TabIndex = 518;
            AlertButton.Text = "Alert On - Should work regardless of alert state";
            AlertButton.UseVisualStyleBackColor = true;
            AlertButton.Click += button3_Click;
            // 
            // AlertProgressBar
            // 
            AlertProgressBar.BackColor = SystemColors.ButtonHighlight;
            AlertProgressBar.Location = new Point(861, 132);
            AlertProgressBar.Name = "AlertProgressBar";
            AlertProgressBar.ProgressBarColour = Color.Red;
            AlertProgressBar.Size = new Size(241, 23);
            AlertProgressBar.TabIndex = 2;
            // 
            // EvasionProgressBar
            // 
            EvasionProgressBar.Location = new Point(861, 306);
            EvasionProgressBar.Name = "EvasionProgressBar";
            EvasionProgressBar.ProgressBarColour = Color.Orange;
            EvasionProgressBar.Size = new Size(241, 23);
            EvasionProgressBar.TabIndex = 1;
            // 
            // CautionProgressBar
            // 
            CautionProgressBar.Location = new Point(861, 488);
            CautionProgressBar.Name = "CautionProgressBar";
            CautionProgressBar.ProgressBarColour = Color.Yellow;
            CautionProgressBar.Size = new Size(239, 23);
            CautionProgressBar.TabIndex = 0;
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.Location = new Point(860, 100);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(242, 33);
            pictureBox3.TabIndex = 523;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackgroundImage = (Image)resources.GetObject("pictureBox4.BackgroundImage");
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox4.Location = new Point(861, 273);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(241, 33);
            pictureBox4.TabIndex = 524;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.BackgroundImage = (Image)resources.GetObject("pictureBox5.BackgroundImage");
            pictureBox5.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox5.Location = new Point(861, 454);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(239, 33);
            pictureBox5.TabIndex = 525;
            pictureBox5.TabStop = false;
            // 
            // EvasionButton
            // 
            EvasionButton.BackgroundImage = (Image)resources.GetObject("EvasionButton.BackgroundImage");
            EvasionButton.Cursor = Cursors.Hand;
            EvasionButton.FlatStyle = FlatStyle.Flat;
            EvasionButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            EvasionButton.ImageAlign = ContentAlignment.TopCenter;
            EvasionButton.Location = new Point(859, 334);
            EvasionButton.Name = "EvasionButton";
            EvasionButton.Size = new Size(243, 55);
            EvasionButton.TabIndex = 526;
            EvasionButton.Text = "Evasion On - Doesn't Work during Alert";
            EvasionButton.UseVisualStyleBackColor = true;
            EvasionButton.Click += EvasionButton_Click;
            // 
            // InfiniteEvasion
            // 
            InfiniteEvasion.AutoSize = true;
            InfiniteEvasion.BackgroundImage = (Image)resources.GetObject("InfiniteEvasion.BackgroundImage");
            InfiniteEvasion.FlatStyle = FlatStyle.Flat;
            InfiniteEvasion.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            InfiniteEvasion.Location = new Point(861, 393);
            InfiniteEvasion.MaximumSize = new Size(241, 55);
            InfiniteEvasion.MinimumSize = new Size(241, 55);
            InfiniteEvasion.Name = "InfiniteEvasion";
            InfiniteEvasion.Size = new Size(241, 55);
            InfiniteEvasion.TabIndex = 527;
            InfiniteEvasion.Text = "Infinite Evasion Mode (Will trigger alert in a building) Alert overrides this effect";
            InfiniteEvasion.UseVisualStyleBackColor = true;
            InfiniteEvasion.CheckedChanged += InfiniteEvasion_CheckedChanged;
            // 
            // HealthFormSwap
            // 
            HealthFormSwap.BackgroundImage = (Image)resources.GetObject("HealthFormSwap.BackgroundImage");
            HealthFormSwap.Cursor = Cursors.Hand;
            HealthFormSwap.FlatStyle = FlatStyle.Flat;
            HealthFormSwap.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            HealthFormSwap.ImageAlign = ContentAlignment.TopCenter;
            HealthFormSwap.ImeMode = ImeMode.NoControl;
            HealthFormSwap.Location = new Point(254, 12);
            HealthFormSwap.Name = "HealthFormSwap";
            HealthFormSwap.Size = new Size(203, 32);
            HealthFormSwap.TabIndex = 534;
            HealthFormSwap.Text = "Items";
            HealthFormSwap.UseVisualStyleBackColor = true;
            HealthFormSwap.Click += HealthFormSwap_Click;
            // 
            // MiscFormSwap
            // 
            MiscFormSwap.BackgroundImage = (Image)resources.GetObject("MiscFormSwap.BackgroundImage");
            MiscFormSwap.Cursor = Cursors.Hand;
            MiscFormSwap.FlatStyle = FlatStyle.Flat;
            MiscFormSwap.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            MiscFormSwap.ImageAlign = ContentAlignment.TopCenter;
            MiscFormSwap.ImeMode = ImeMode.NoControl;
            MiscFormSwap.Location = new Point(710, 12);
            MiscFormSwap.Name = "MiscFormSwap";
            MiscFormSwap.Size = new Size(203, 32);
            MiscFormSwap.TabIndex = 533;
            MiscFormSwap.Text = "Stats and Misc";
            MiscFormSwap.UseVisualStyleBackColor = true;
            MiscFormSwap.Click += MiscFormSwap_Click;
            // 
            // CamoFormSwap
            // 
            CamoFormSwap.BackgroundImage = (Image)resources.GetObject("CamoFormSwap.BackgroundImage");
            CamoFormSwap.Cursor = Cursors.Hand;
            CamoFormSwap.FlatStyle = FlatStyle.Flat;
            CamoFormSwap.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CamoFormSwap.ImageAlign = ContentAlignment.TopCenter;
            CamoFormSwap.ImeMode = ImeMode.NoControl;
            CamoFormSwap.Location = new Point(482, 12);
            CamoFormSwap.Name = "CamoFormSwap";
            CamoFormSwap.Size = new Size(203, 32);
            CamoFormSwap.TabIndex = 532;
            CamoFormSwap.Text = "Camo";
            CamoFormSwap.UseVisualStyleBackColor = true;
            CamoFormSwap.Click += CamoFormSwap_Click;
            // 
            // WeaponFormSwap
            // 
            WeaponFormSwap.BackgroundImage = (Image)resources.GetObject("WeaponFormSwap.BackgroundImage");
            WeaponFormSwap.Cursor = Cursors.Hand;
            WeaponFormSwap.FlatStyle = FlatStyle.Flat;
            WeaponFormSwap.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            WeaponFormSwap.ImageAlign = ContentAlignment.TopCenter;
            WeaponFormSwap.ImeMode = ImeMode.NoControl;
            WeaponFormSwap.Location = new Point(28, 12);
            WeaponFormSwap.Name = "WeaponFormSwap";
            WeaponFormSwap.Size = new Size(203, 32);
            WeaponFormSwap.TabIndex = 531;
            WeaponFormSwap.Text = "Weapons";
            WeaponFormSwap.UseVisualStyleBackColor = true;
            WeaponFormSwap.Click += WeaponFormSwap_Click;
            // 
            // ClearCautionAndEvasion
            // 
            ClearCautionAndEvasion.BackgroundImage = (Image)resources.GetObject("ClearCautionAndEvasion.BackgroundImage");
            ClearCautionAndEvasion.Cursor = Cursors.Hand;
            ClearCautionAndEvasion.FlatStyle = FlatStyle.Flat;
            ClearCautionAndEvasion.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ClearCautionAndEvasion.ImageAlign = ContentAlignment.TopCenter;
            ClearCautionAndEvasion.Location = new Point(859, 646);
            ClearCautionAndEvasion.Name = "ClearCautionAndEvasion";
            ClearCautionAndEvasion.Size = new Size(241, 31);
            ClearCautionAndEvasion.TabIndex = 535;
            ClearCautionAndEvasion.Text = "Remove Evasion/Caution State";
            ClearCautionAndEvasion.UseVisualStyleBackColor = true;
            ClearCautionAndEvasion.Click += ClearCautionAndEvasion_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.ActiveCaptionText;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox3.ForeColor = SystemColors.ActiveCaptionText;
            textBox3.Location = new Point(430, 60);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Serious Injuries";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(228, 34);
            textBox3.TabIndex = 536;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // BurnInjury
            // 
            BurnInjury.BackgroundImage = (Image)resources.GetObject("BurnInjury.BackgroundImage");
            BurnInjury.Cursor = Cursors.Hand;
            BurnInjury.FlatStyle = FlatStyle.Flat;
            BurnInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            BurnInjury.ImageAlign = ContentAlignment.TopCenter;
            BurnInjury.Location = new Point(430, 100);
            BurnInjury.Name = "BurnInjury";
            BurnInjury.Size = new Size(228, 31);
            BurnInjury.TabIndex = 537;
            BurnInjury.Text = "Serious Burn";
            BurnInjury.UseVisualStyleBackColor = true;
            BurnInjury.Click += BurnInjury_Click;
            // 
            // CutInjury
            // 
            CutInjury.BackgroundImage = (Image)resources.GetObject("CutInjury.BackgroundImage");
            CutInjury.Cursor = Cursors.Hand;
            CutInjury.FlatStyle = FlatStyle.Flat;
            CutInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            CutInjury.ImageAlign = ContentAlignment.TopCenter;
            CutInjury.Location = new Point(430, 139);
            CutInjury.Name = "CutInjury";
            CutInjury.Size = new Size(228, 31);
            CutInjury.TabIndex = 538;
            CutInjury.Text = "Deep Cut";
            CutInjury.UseVisualStyleBackColor = true;
            CutInjury.Click += CutInjury_Click;
            // 
            // GunshotRifleInjury
            // 
            GunshotRifleInjury.BackgroundImage = (Image)resources.GetObject("GunshotRifleInjury.BackgroundImage");
            GunshotRifleInjury.Cursor = Cursors.Hand;
            GunshotRifleInjury.FlatStyle = FlatStyle.Flat;
            GunshotRifleInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            GunshotRifleInjury.ImageAlign = ContentAlignment.TopCenter;
            GunshotRifleInjury.Location = new Point(430, 178);
            GunshotRifleInjury.Name = "GunshotRifleInjury";
            GunshotRifleInjury.Size = new Size(228, 31);
            GunshotRifleInjury.TabIndex = 539;
            GunshotRifleInjury.Text = "Gunshot Wound (Rifle)";
            GunshotRifleInjury.UseVisualStyleBackColor = true;
            GunshotRifleInjury.Click += GunshotRifleInjury_Click;
            // 
            // GunshotShotgunInjury
            // 
            GunshotShotgunInjury.BackgroundImage = (Image)resources.GetObject("GunshotShotgunInjury.BackgroundImage");
            GunshotShotgunInjury.Cursor = Cursors.Hand;
            GunshotShotgunInjury.FlatStyle = FlatStyle.Flat;
            GunshotShotgunInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            GunshotShotgunInjury.ImageAlign = ContentAlignment.TopCenter;
            GunshotShotgunInjury.Location = new Point(430, 218);
            GunshotShotgunInjury.Name = "GunshotShotgunInjury";
            GunshotShotgunInjury.Size = new Size(228, 31);
            GunshotShotgunInjury.TabIndex = 540;
            GunshotShotgunInjury.Text = "Gunshot Wound (Shotgun)";
            GunshotShotgunInjury.UseVisualStyleBackColor = true;
            GunshotShotgunInjury.Click += GunshotShotgunInjury_Click;
            // 
            // BoneFractureInjury
            // 
            BoneFractureInjury.BackgroundImage = (Image)resources.GetObject("BoneFractureInjury.BackgroundImage");
            BoneFractureInjury.Cursor = Cursors.Hand;
            BoneFractureInjury.FlatStyle = FlatStyle.Flat;
            BoneFractureInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            BoneFractureInjury.ImageAlign = ContentAlignment.TopCenter;
            BoneFractureInjury.Location = new Point(430, 258);
            BoneFractureInjury.Name = "BoneFractureInjury";
            BoneFractureInjury.Size = new Size(228, 31);
            BoneFractureInjury.TabIndex = 541;
            BoneFractureInjury.Text = "Bone Fracture";
            BoneFractureInjury.UseVisualStyleBackColor = true;
            BoneFractureInjury.Click += BoneFractureInjury_Click;
            // 
            // BulletBeeInjury
            // 
            BulletBeeInjury.BackgroundImage = (Image)resources.GetObject("BulletBeeInjury.BackgroundImage");
            BulletBeeInjury.Cursor = Cursors.Hand;
            BulletBeeInjury.FlatStyle = FlatStyle.Flat;
            BulletBeeInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            BulletBeeInjury.ImageAlign = ContentAlignment.TopCenter;
            BulletBeeInjury.Location = new Point(430, 299);
            BulletBeeInjury.Name = "BulletBeeInjury";
            BulletBeeInjury.Size = new Size(228, 31);
            BulletBeeInjury.TabIndex = 542;
            BulletBeeInjury.Text = "Bullet Bee";
            BulletBeeInjury.UseVisualStyleBackColor = true;
            BulletBeeInjury.Click += BulletBeeInjury_Click;
            // 
            // LeechesInjury
            // 
            LeechesInjury.BackgroundImage = (Image)resources.GetObject("LeechesInjury.BackgroundImage");
            LeechesInjury.Cursor = Cursors.Hand;
            LeechesInjury.FlatStyle = FlatStyle.Flat;
            LeechesInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LeechesInjury.ImageAlign = ContentAlignment.TopCenter;
            LeechesInjury.Location = new Point(430, 340);
            LeechesInjury.Name = "LeechesInjury";
            LeechesInjury.Size = new Size(228, 31);
            LeechesInjury.TabIndex = 543;
            LeechesInjury.Text = "Leeches";
            LeechesInjury.UseVisualStyleBackColor = true;
            LeechesInjury.Click += LeechesInjury_Click;
            // 
            // ArrowInjury
            // 
            ArrowInjury.BackgroundImage = (Image)resources.GetObject("ArrowInjury.BackgroundImage");
            ArrowInjury.Cursor = Cursors.Hand;
            ArrowInjury.FlatStyle = FlatStyle.Flat;
            ArrowInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ArrowInjury.ImageAlign = ContentAlignment.TopCenter;
            ArrowInjury.Location = new Point(430, 381);
            ArrowInjury.Name = "ArrowInjury";
            ArrowInjury.Size = new Size(228, 31);
            ArrowInjury.TabIndex = 544;
            ArrowInjury.Text = "Arrow Wound";
            ArrowInjury.UseVisualStyleBackColor = true;
            ArrowInjury.Click += ArrowInjury_Click;
            // 
            // TranqInjury
            // 
            TranqInjury.BackgroundImage = (Image)resources.GetObject("TranqInjury.BackgroundImage");
            TranqInjury.Cursor = Cursors.Hand;
            TranqInjury.FlatStyle = FlatStyle.Flat;
            TranqInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            TranqInjury.ImageAlign = ContentAlignment.TopCenter;
            TranqInjury.Location = new Point(430, 423);
            TranqInjury.Name = "TranqInjury";
            TranqInjury.Size = new Size(228, 31);
            TranqInjury.TabIndex = 545;
            TranqInjury.Text = "Tranqulizer Dart";
            TranqInjury.UseVisualStyleBackColor = true;
            TranqInjury.Click += TranqInjury_Click;
            // 
            // VenomPoisoningInjury
            // 
            VenomPoisoningInjury.BackgroundImage = (Image)resources.GetObject("VenomPoisoningInjury.BackgroundImage");
            VenomPoisoningInjury.Cursor = Cursors.Hand;
            VenomPoisoningInjury.FlatStyle = FlatStyle.Flat;
            VenomPoisoningInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            VenomPoisoningInjury.ImageAlign = ContentAlignment.TopCenter;
            VenomPoisoningInjury.Location = new Point(430, 465);
            VenomPoisoningInjury.Name = "VenomPoisoningInjury";
            VenomPoisoningInjury.Size = new Size(228, 31);
            VenomPoisoningInjury.TabIndex = 546;
            VenomPoisoningInjury.Text = "Venom Poison";
            VenomPoisoningInjury.UseVisualStyleBackColor = true;
            VenomPoisoningInjury.Click += VenomPoisoningInjury_Click;
            // 
            // FoodPoisoningInjury
            // 
            FoodPoisoningInjury.BackgroundImage = (Image)resources.GetObject("FoodPoisoningInjury.BackgroundImage");
            FoodPoisoningInjury.Cursor = Cursors.Hand;
            FoodPoisoningInjury.FlatStyle = FlatStyle.Flat;
            FoodPoisoningInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            FoodPoisoningInjury.ImageAlign = ContentAlignment.TopCenter;
            FoodPoisoningInjury.Location = new Point(430, 506);
            FoodPoisoningInjury.Name = "FoodPoisoningInjury";
            FoodPoisoningInjury.Size = new Size(228, 31);
            FoodPoisoningInjury.TabIndex = 547;
            FoodPoisoningInjury.Text = "Food Poisoning";
            FoodPoisoningInjury.UseVisualStyleBackColor = true;
            FoodPoisoningInjury.Click += FoodPoisoningInjury_Click;
            // 
            // CommonColdInjury
            // 
            CommonColdInjury.BackgroundImage = (Image)resources.GetObject("CommonColdInjury.BackgroundImage");
            CommonColdInjury.Cursor = Cursors.Hand;
            CommonColdInjury.FlatStyle = FlatStyle.Flat;
            CommonColdInjury.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            CommonColdInjury.ImageAlign = ContentAlignment.TopCenter;
            CommonColdInjury.Location = new Point(430, 548);
            CommonColdInjury.Name = "CommonColdInjury";
            CommonColdInjury.Size = new Size(228, 31);
            CommonColdInjury.TabIndex = 548;
            CommonColdInjury.Text = "Cold";
            CommonColdInjury.UseVisualStyleBackColor = true;
            CommonColdInjury.Click += CommonColdInjury_Click;
            // 
            // RemoveInjuries
            // 
            RemoveInjuries.BackgroundImage = (Image)resources.GetObject("RemoveInjuries.BackgroundImage");
            RemoveInjuries.Cursor = Cursors.Hand;
            RemoveInjuries.FlatStyle = FlatStyle.Flat;
            RemoveInjuries.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            RemoveInjuries.ImageAlign = ContentAlignment.TopCenter;
            RemoveInjuries.Location = new Point(430, 590);
            RemoveInjuries.Name = "RemoveInjuries";
            RemoveInjuries.Size = new Size(228, 31);
            RemoveInjuries.TabIndex = 550;
            RemoveInjuries.Text = "Remove All Injuries";
            RemoveInjuries.UseVisualStyleBackColor = true;
            RemoveInjuries.Click += RemoveInjuries_Click;
            // 
            // SwapToBossForm
            // 
            SwapToBossForm.BackgroundImage = (Image)resources.GetObject("SwapToBossForm.BackgroundImage");
            SwapToBossForm.Cursor = Cursors.Hand;
            SwapToBossForm.FlatStyle = FlatStyle.Flat;
            SwapToBossForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToBossForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToBossForm.ImeMode = ImeMode.NoControl;
            SwapToBossForm.Location = new Point(941, 12);
            SwapToBossForm.Name = "SwapToBossForm";
            SwapToBossForm.Size = new Size(203, 32);
            SwapToBossForm.TabIndex = 551;
            SwapToBossForm.Text = "Bosses";
            SwapToBossForm.UseVisualStyleBackColor = true;
            SwapToBossForm.Click += SwapToBossForm_Click;
            // 
            // StatsAndAlertForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1166, 707);
            Controls.Add(SwapToBossForm);
            Controls.Add(RemoveInjuries);
            Controls.Add(CommonColdInjury);
            Controls.Add(FoodPoisoningInjury);
            Controls.Add(VenomPoisoningInjury);
            Controls.Add(TranqInjury);
            Controls.Add(ArrowInjury);
            Controls.Add(LeechesInjury);
            Controls.Add(BulletBeeInjury);
            Controls.Add(BoneFractureInjury);
            Controls.Add(GunshotShotgunInjury);
            Controls.Add(GunshotRifleInjury);
            Controls.Add(CutInjury);
            Controls.Add(BurnInjury);
            Controls.Add(textBox3);
            Controls.Add(ClearCautionAndEvasion);
            Controls.Add(HealthFormSwap);
            Controls.Add(MiscFormSwap);
            Controls.Add(CamoFormSwap);
            Controls.Add(WeaponFormSwap);
            Controls.Add(InfiniteEvasion);
            Controls.Add(EvasionButton);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(AlertProgressBar);
            Controls.Add(EvasionProgressBar);
            Controls.Add(CautionProgressBar);
            Controls.Add(InfiniteCaution);
            Controls.Add(InfiniteAlert);
            Controls.Add(CautionButton);
            Controls.Add(textBox2);
            Controls.Add(AlertButton);
            Controls.Add(MaxHpTo1);
            Controls.Add(Minus100MaxHpValue);
            Controls.Add(Plus100MaxHpValue);
            Controls.Add(textBox1);
            Controls.Add(FullStamina30000Value);
            Controls.Add(SetStaminaToZero);
            Controls.Add(Minus10000StaminaValue);
            Controls.Add(Plus10000StaminaValue);
            Controls.Add(CurrentHpTo1);
            Controls.Add(ZeroHP);
            Controls.Add(Minus100HpValue);
            Controls.Add(Plus100HpValue);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "StatsAndAlertForm";
            Text = "MGS3 Cheat Trainer - Stats and Alert - ANTIBigBoss";
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button MaxHpTo1;
        private Button Minus100MaxHpValue;
        private Button Plus100MaxHpValue;
        private TextBox textBox1;
        private Button FullStamina30000Value;
        private Button SetStaminaToZero;
        private Button Minus10000StaminaValue;
        private Button Plus10000StaminaValue;
        private Button CurrentHpTo1;
        private Button ZeroHP;
        private Button Minus100HpValue;
        private Button Plus100HpValue;
        private CheckBox InfiniteCaution;
        private CheckBox InfiniteAlert;
        private Button CautionButton;
        private TextBox textBox2;
        private Button AlertButton;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private Button EvasionButton;
        private CheckBox InfiniteEvasion;
        private Button HealthFormSwap;
        private Button MiscFormSwap;
        private Button CamoFormSwap;
        private Button WeaponFormSwap;
        private Button ClearCautionAndEvasion;
        private TextBox textBox3;
        private Button BurnInjury;
        private Button CutInjury;
        private Button GunshotRifleInjury;
        private Button GunshotShotgunInjury;
        private Button BoneFractureInjury;
        private Button BulletBeeInjury;
        private Button LeechesInjury;
        private Button ArrowInjury;
        private Button TranqInjury;
        private Button VenomPoisoningInjury;
        private Button FoodPoisoningInjury;
        private Button CommonColdInjury;
        private Button TransmitterInjury;
        private Button FakeDeathPillInjury;
        private Button RemoveInjuries;
        private Button SwapToBossForm;
    }
}