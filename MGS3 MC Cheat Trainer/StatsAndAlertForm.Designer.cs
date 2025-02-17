﻿namespace MGS3_MC_Cheat_Trainer
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
            textBox4 = new TextBox();
            LethalGroupBox = new GroupBox();
            OneShotKillLethalRadio = new RadioButton();
            VeryWeakLethalRadio = new RadioButton();
            NormalLethalRadio = new RadioButton();
            VeryStrongLethalRadio = new RadioButton();
            NeckSnapLethalRadio = new RadioButton();
            groupBox2 = new GroupBox();
            OneShotSleepZzzRadio = new RadioButton();
            VeryWeakZzzRadio = new RadioButton();
            NormalZzzRadio = new RadioButton();
            VeryStrongZzzRadio = new RadioButton();
            InvincibleZzzRadio = new RadioButton();
            groupBox3 = new GroupBox();
            OneShotStunStunRadio = new RadioButton();
            VeryWeakStunRadio = new RadioButton();
            NormalStunRadio = new RadioButton();
            VeryStrongStunRadio = new RadioButton();
            NeckSnapStunRadio = new RadioButton();
            BatteryDrainCheckBox = new CheckBox();
            SwapToBossForm = new Button();
            SwapToMiscForm = new Button();
            SwapToGameStatsForm = new Button();
            SwapToCamoForm = new Button();
            SwapToItemsForm = new Button();
            SwapToWeaponsForm = new Button();
            SwapToDebugForm = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            LethalGroupBox.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
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
            ZeroHP.Location = new Point(13, 109);
            ZeroHP.Name = "ZeroHP";
            ZeroHP.Size = new Size(227, 31);
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
            textBox3.Location = new Point(254, 60);
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
            BurnInjury.Location = new Point(254, 100);
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
            CutInjury.Location = new Point(254, 139);
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
            GunshotRifleInjury.Location = new Point(254, 178);
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
            GunshotShotgunInjury.Location = new Point(254, 218);
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
            BoneFractureInjury.Location = new Point(254, 258);
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
            BulletBeeInjury.Location = new Point(254, 299);
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
            LeechesInjury.Location = new Point(254, 340);
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
            ArrowInjury.Location = new Point(254, 381);
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
            TranqInjury.Location = new Point(254, 423);
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
            VenomPoisoningInjury.Location = new Point(254, 465);
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
            FoodPoisoningInjury.Location = new Point(254, 506);
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
            CommonColdInjury.Location = new Point(254, 548);
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
            RemoveInjuries.Location = new Point(254, 590);
            RemoveInjuries.Name = "RemoveInjuries";
            RemoveInjuries.Size = new Size(228, 31);
            RemoveInjuries.TabIndex = 550;
            RemoveInjuries.Text = "Remove All Injuries";
            RemoveInjuries.UseVisualStyleBackColor = true;
            RemoveInjuries.Click += RemoveInjuries_Click;
            // 
            // textBox4
            // 
            textBox4.BackColor = SystemColors.ActiveCaptionText;
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox4.ForeColor = SystemColors.ActiveCaptionText;
            textBox4.Location = new Point(619, 60);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "Guard Strength";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(228, 34);
            textBox4.TabIndex = 552;
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // LethalGroupBox
            // 
            LethalGroupBox.BackColor = Color.FromArgb(156, 156, 124);
            LethalGroupBox.BackgroundImageLayout = ImageLayout.None;
            LethalGroupBox.Controls.Add(OneShotKillLethalRadio);
            LethalGroupBox.Controls.Add(VeryWeakLethalRadio);
            LethalGroupBox.Controls.Add(NormalLethalRadio);
            LethalGroupBox.Controls.Add(VeryStrongLethalRadio);
            LethalGroupBox.Controls.Add(NeckSnapLethalRadio);
            LethalGroupBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LethalGroupBox.Location = new Point(619, 102);
            LethalGroupBox.Name = "LethalGroupBox";
            LethalGroupBox.Size = new Size(228, 156);
            LethalGroupBox.TabIndex = 553;
            LethalGroupBox.TabStop = false;
            LethalGroupBox.Text = "Lethal";
            // 
            // OneShotKillLethalRadio
            // 
            OneShotKillLethalRadio.AutoSize = true;
            OneShotKillLethalRadio.BackColor = Color.FromArgb(156, 156, 124);
            OneShotKillLethalRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            OneShotKillLethalRadio.Location = new Point(6, 128);
            OneShotKillLethalRadio.Name = "OneShotKillLethalRadio";
            OneShotKillLethalRadio.Size = new Size(117, 24);
            OneShotKillLethalRadio.TabIndex = 556;
            OneShotKillLethalRadio.TabStop = true;
            OneShotKillLethalRadio.Text = "One Shot Kill";
            OneShotKillLethalRadio.UseVisualStyleBackColor = false;
            OneShotKillLethalRadio.CheckedChanged += OneShotKillLethalRadio_CheckedChanged;
            // 
            // VeryWeakLethalRadio
            // 
            VeryWeakLethalRadio.AutoSize = true;
            VeryWeakLethalRadio.BackColor = Color.FromArgb(156, 156, 124);
            VeryWeakLethalRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            VeryWeakLethalRadio.Location = new Point(6, 101);
            VeryWeakLethalRadio.Name = "VeryWeakLethalRadio";
            VeryWeakLethalRadio.Size = new Size(102, 24);
            VeryWeakLethalRadio.TabIndex = 555;
            VeryWeakLethalRadio.TabStop = true;
            VeryWeakLethalRadio.Text = "Very Weak";
            VeryWeakLethalRadio.UseVisualStyleBackColor = false;
            VeryWeakLethalRadio.CheckedChanged += VeryWeakLethalRadio_CheckedChanged;
            // 
            // NormalLethalRadio
            // 
            NormalLethalRadio.AutoSize = true;
            NormalLethalRadio.BackColor = Color.FromArgb(156, 156, 124);
            NormalLethalRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NormalLethalRadio.Location = new Point(6, 76);
            NormalLethalRadio.Name = "NormalLethalRadio";
            NormalLethalRadio.Size = new Size(80, 24);
            NormalLethalRadio.TabIndex = 554;
            NormalLethalRadio.TabStop = true;
            NormalLethalRadio.Text = "Normal";
            NormalLethalRadio.UseVisualStyleBackColor = false;
            NormalLethalRadio.CheckedChanged += NormalLethalRadio_CheckedChanged;
            // 
            // VeryStrongLethalRadio
            // 
            VeryStrongLethalRadio.AutoSize = true;
            VeryStrongLethalRadio.BackColor = Color.FromArgb(156, 156, 124);
            VeryStrongLethalRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            VeryStrongLethalRadio.Location = new Point(6, 48);
            VeryStrongLethalRadio.Name = "VeryStrongLethalRadio";
            VeryStrongLethalRadio.Size = new Size(110, 24);
            VeryStrongLethalRadio.TabIndex = 1;
            VeryStrongLethalRadio.TabStop = true;
            VeryStrongLethalRadio.Text = "Very Strong";
            VeryStrongLethalRadio.UseVisualStyleBackColor = false;
            VeryStrongLethalRadio.CheckedChanged += VeryStrongLethalRadio_CheckedChanged;
            // 
            // NeckSnapLethalRadio
            // 
            NeckSnapLethalRadio.AutoSize = true;
            NeckSnapLethalRadio.BackColor = Color.FromArgb(156, 156, 124);
            NeckSnapLethalRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NeckSnapLethalRadio.Location = new Point(6, 21);
            NeckSnapLethalRadio.Name = "NeckSnapLethalRadio";
            NeckSnapLethalRadio.Size = new Size(151, 24);
            NeckSnapLethalRadio.TabIndex = 0;
            NeckSnapLethalRadio.TabStop = true;
            NeckSnapLethalRadio.Text = "Almost Unkillable";
            NeckSnapLethalRadio.UseVisualStyleBackColor = false;
            NeckSnapLethalRadio.CheckedChanged += NeckSnapLethalRadio_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.FromArgb(156, 156, 124);
            groupBox2.BackgroundImageLayout = ImageLayout.None;
            groupBox2.Controls.Add(OneShotSleepZzzRadio);
            groupBox2.Controls.Add(VeryWeakZzzRadio);
            groupBox2.Controls.Add(NormalZzzRadio);
            groupBox2.Controls.Add(VeryStrongZzzRadio);
            groupBox2.Controls.Add(InvincibleZzzRadio);
            groupBox2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.Location = new Point(619, 273);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(228, 156);
            groupBox2.TabIndex = 557;
            groupBox2.TabStop = false;
            groupBox2.Text = "Sleep";
            // 
            // OneShotSleepZzzRadio
            // 
            OneShotSleepZzzRadio.AutoSize = true;
            OneShotSleepZzzRadio.BackColor = Color.FromArgb(156, 156, 124);
            OneShotSleepZzzRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            OneShotSleepZzzRadio.Location = new Point(6, 128);
            OneShotSleepZzzRadio.Name = "OneShotSleepZzzRadio";
            OneShotSleepZzzRadio.Size = new Size(118, 24);
            OneShotSleepZzzRadio.TabIndex = 556;
            OneShotSleepZzzRadio.TabStop = true;
            OneShotSleepZzzRadio.Text = "One Shot Zzz";
            OneShotSleepZzzRadio.UseVisualStyleBackColor = false;
            OneShotSleepZzzRadio.CheckedChanged += OneShotSleepZzzRadio_CheckedChanged;
            // 
            // VeryWeakZzzRadio
            // 
            VeryWeakZzzRadio.AutoSize = true;
            VeryWeakZzzRadio.BackColor = Color.FromArgb(156, 156, 124);
            VeryWeakZzzRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            VeryWeakZzzRadio.Location = new Point(6, 101);
            VeryWeakZzzRadio.Name = "VeryWeakZzzRadio";
            VeryWeakZzzRadio.Size = new Size(102, 24);
            VeryWeakZzzRadio.TabIndex = 555;
            VeryWeakZzzRadio.TabStop = true;
            VeryWeakZzzRadio.Text = "Very Weak";
            VeryWeakZzzRadio.UseVisualStyleBackColor = false;
            VeryWeakZzzRadio.CheckedChanged += VeryWeakZzzRadio_CheckedChanged;
            // 
            // NormalZzzRadio
            // 
            NormalZzzRadio.AutoSize = true;
            NormalZzzRadio.BackColor = Color.FromArgb(156, 156, 124);
            NormalZzzRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NormalZzzRadio.Location = new Point(6, 76);
            NormalZzzRadio.Name = "NormalZzzRadio";
            NormalZzzRadio.Size = new Size(80, 24);
            NormalZzzRadio.TabIndex = 554;
            NormalZzzRadio.TabStop = true;
            NormalZzzRadio.Text = "Normal";
            NormalZzzRadio.UseVisualStyleBackColor = false;
            NormalZzzRadio.CheckedChanged += NormalZzzRadio_CheckedChanged;
            // 
            // VeryStrongZzzRadio
            // 
            VeryStrongZzzRadio.AutoSize = true;
            VeryStrongZzzRadio.BackColor = Color.FromArgb(156, 156, 124);
            VeryStrongZzzRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            VeryStrongZzzRadio.Location = new Point(6, 48);
            VeryStrongZzzRadio.Name = "VeryStrongZzzRadio";
            VeryStrongZzzRadio.Size = new Size(110, 24);
            VeryStrongZzzRadio.TabIndex = 1;
            VeryStrongZzzRadio.TabStop = true;
            VeryStrongZzzRadio.Text = "Very Strong";
            VeryStrongZzzRadio.UseVisualStyleBackColor = false;
            VeryStrongZzzRadio.CheckedChanged += VeryStrongZzzRadio_CheckedChanged;
            // 
            // InvincibleZzzRadio
            // 
            InvincibleZzzRadio.AutoSize = true;
            InvincibleZzzRadio.BackColor = Color.FromArgb(156, 156, 124);
            InvincibleZzzRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            InvincibleZzzRadio.Location = new Point(6, 21);
            InvincibleZzzRadio.Name = "InvincibleZzzRadio";
            InvincibleZzzRadio.Size = new Size(201, 24);
            InvincibleZzzRadio.TabIndex = 0;
            InvincibleZzzRadio.TabStop = true;
            InvincibleZzzRadio.Text = "Almost Immune to Sleep";
            InvincibleZzzRadio.UseVisualStyleBackColor = false;
            InvincibleZzzRadio.CheckedChanged += InvincibleZzzRadio_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.FromArgb(156, 156, 124);
            groupBox3.BackgroundImageLayout = ImageLayout.None;
            groupBox3.Controls.Add(OneShotStunStunRadio);
            groupBox3.Controls.Add(VeryWeakStunRadio);
            groupBox3.Controls.Add(NormalStunRadio);
            groupBox3.Controls.Add(VeryStrongStunRadio);
            groupBox3.Controls.Add(NeckSnapStunRadio);
            groupBox3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.Location = new Point(619, 458);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(228, 156);
            groupBox3.TabIndex = 557;
            groupBox3.TabStop = false;
            groupBox3.Text = "Stun";
            groupBox3.Visible = false;
            // 
            // OneShotStunStunRadio
            // 
            OneShotStunStunRadio.AutoSize = true;
            OneShotStunStunRadio.BackColor = Color.FromArgb(156, 156, 124);
            OneShotStunStunRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            OneShotStunStunRadio.Location = new Point(6, 128);
            OneShotStunStunRadio.Name = "OneShotStunStunRadio";
            OneShotStunStunRadio.Size = new Size(127, 24);
            OneShotStunStunRadio.TabIndex = 556;
            OneShotStunStunRadio.TabStop = true;
            OneShotStunStunRadio.Text = "One Shot Stun";
            OneShotStunStunRadio.UseVisualStyleBackColor = false;
            OneShotStunStunRadio.CheckedChanged += OneShotStunStunRadio_CheckedChanged;
            // 
            // VeryWeakStunRadio
            // 
            VeryWeakStunRadio.AutoSize = true;
            VeryWeakStunRadio.BackColor = Color.FromArgb(156, 156, 124);
            VeryWeakStunRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            VeryWeakStunRadio.Location = new Point(6, 101);
            VeryWeakStunRadio.Name = "VeryWeakStunRadio";
            VeryWeakStunRadio.Size = new Size(102, 24);
            VeryWeakStunRadio.TabIndex = 555;
            VeryWeakStunRadio.TabStop = true;
            VeryWeakStunRadio.Text = "Very Weak";
            VeryWeakStunRadio.UseVisualStyleBackColor = false;
            VeryWeakStunRadio.CheckedChanged += VeryWeakStunRadio_CheckedChanged;
            // 
            // NormalStunRadio
            // 
            NormalStunRadio.AutoSize = true;
            NormalStunRadio.BackColor = Color.FromArgb(156, 156, 124);
            NormalStunRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NormalStunRadio.Location = new Point(6, 76);
            NormalStunRadio.Name = "NormalStunRadio";
            NormalStunRadio.Size = new Size(80, 24);
            NormalStunRadio.TabIndex = 554;
            NormalStunRadio.TabStop = true;
            NormalStunRadio.Text = "Normal";
            NormalStunRadio.UseVisualStyleBackColor = false;
            NormalStunRadio.CheckedChanged += NormalStunRadio_CheckedChanged;
            // 
            // VeryStrongStunRadio
            // 
            VeryStrongStunRadio.AutoSize = true;
            VeryStrongStunRadio.BackColor = Color.FromArgb(156, 156, 124);
            VeryStrongStunRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            VeryStrongStunRadio.Location = new Point(6, 48);
            VeryStrongStunRadio.Name = "VeryStrongStunRadio";
            VeryStrongStunRadio.Size = new Size(110, 24);
            VeryStrongStunRadio.TabIndex = 1;
            VeryStrongStunRadio.TabStop = true;
            VeryStrongStunRadio.Text = "Very Strong";
            VeryStrongStunRadio.UseVisualStyleBackColor = false;
            VeryStrongStunRadio.CheckedChanged += VeryStrongStunRadio_CheckedChanged;
            // 
            // NeckSnapStunRadio
            // 
            NeckSnapStunRadio.AutoSize = true;
            NeckSnapStunRadio.BackColor = Color.FromArgb(156, 156, 124);
            NeckSnapStunRadio.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NeckSnapStunRadio.Location = new Point(6, 21);
            NeckSnapStunRadio.Name = "NeckSnapStunRadio";
            NeckSnapStunRadio.Size = new Size(171, 24);
            NeckSnapStunRadio.TabIndex = 0;
            NeckSnapStunRadio.TabStop = true;
            NeckSnapStunRadio.Text = "Almost Unstunnable";
            NeckSnapStunRadio.UseVisualStyleBackColor = false;
            NeckSnapStunRadio.CheckedChanged += NeckSnapStunRadio_CheckedChanged;
            // 
            // BatteryDrainCheckBox
            // 
            BatteryDrainCheckBox.BackgroundImage = Properties.Resources.Selected_MGS3_Menu_without_button;
            BatteryDrainCheckBox.FlatStyle = FlatStyle.Flat;
            BatteryDrainCheckBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            BatteryDrainCheckBox.Image = (Image)resources.GetObject("BatteryDrainCheckBox.Image");
            BatteryDrainCheckBox.Location = new Point(14, 504);
            BatteryDrainCheckBox.Name = "BatteryDrainCheckBox";
            BatteryDrainCheckBox.Size = new Size(226, 67);
            BatteryDrainCheckBox.TabIndex = 597;
            BatteryDrainCheckBox.Text = "Stop Battery Power From Draining";
            BatteryDrainCheckBox.UseVisualStyleBackColor = true;
            BatteryDrainCheckBox.Visible = false;
            BatteryDrainCheckBox.CheckedChanged += BatteryDrainCheckBox_CheckedChanged;
            // 
            // SwapToBossForm
            // 
            SwapToBossForm.BackgroundImage = (Image)resources.GetObject("SwapToBossForm.BackgroundImage");
            SwapToBossForm.Cursor = Cursors.Hand;
            SwapToBossForm.FlatStyle = FlatStyle.Flat;
            SwapToBossForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToBossForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToBossForm.ImeMode = ImeMode.NoControl;
            SwapToBossForm.Location = new Point(667, 12);
            SwapToBossForm.Name = "SwapToBossForm";
            SwapToBossForm.Size = new Size(130, 31);
            SwapToBossForm.TabIndex = 650;
            SwapToBossForm.Text = "Bosses";
            SwapToBossForm.UseVisualStyleBackColor = true;
            SwapToBossForm.Click += SwapToBossForm_Click_1;
            // 
            // SwapToMiscForm
            // 
            SwapToMiscForm.BackgroundImage = (Image)resources.GetObject("SwapToMiscForm.BackgroundImage");
            SwapToMiscForm.Cursor = Cursors.Hand;
            SwapToMiscForm.FlatStyle = FlatStyle.Flat;
            SwapToMiscForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToMiscForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToMiscForm.ImeMode = ImeMode.NoControl;
            SwapToMiscForm.Location = new Point(531, 12);
            SwapToMiscForm.Name = "SwapToMiscForm";
            SwapToMiscForm.Size = new Size(130, 31);
            SwapToMiscForm.TabIndex = 649;
            SwapToMiscForm.Text = "Misc";
            SwapToMiscForm.UseVisualStyleBackColor = true;
            SwapToMiscForm.Click += SwapToMiscForm_Click;
            // 
            // SwapToGameStatsForm
            // 
            SwapToGameStatsForm.BackgroundImage = (Image)resources.GetObject("SwapToGameStatsForm.BackgroundImage");
            SwapToGameStatsForm.Cursor = Cursors.Hand;
            SwapToGameStatsForm.FlatStyle = FlatStyle.Flat;
            SwapToGameStatsForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToGameStatsForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToGameStatsForm.ImeMode = ImeMode.NoControl;
            SwapToGameStatsForm.Location = new Point(803, 12);
            SwapToGameStatsForm.Name = "SwapToGameStatsForm";
            SwapToGameStatsForm.Size = new Size(130, 31);
            SwapToGameStatsForm.TabIndex = 648;
            SwapToGameStatsForm.Text = "Game Stats";
            SwapToGameStatsForm.UseVisualStyleBackColor = true;
            SwapToGameStatsForm.Click += SwapToGameStatsForm_Click;
            // 
            // SwapToCamoForm
            // 
            SwapToCamoForm.BackgroundImage = (Image)resources.GetObject("SwapToCamoForm.BackgroundImage");
            SwapToCamoForm.Cursor = Cursors.Hand;
            SwapToCamoForm.FlatStyle = FlatStyle.Flat;
            SwapToCamoForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToCamoForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToCamoForm.ImeMode = ImeMode.NoControl;
            SwapToCamoForm.Location = new Point(395, 12);
            SwapToCamoForm.Name = "SwapToCamoForm";
            SwapToCamoForm.Size = new Size(130, 31);
            SwapToCamoForm.TabIndex = 646;
            SwapToCamoForm.Text = "Camo";
            SwapToCamoForm.UseVisualStyleBackColor = true;
            SwapToCamoForm.Click += SwapToCamoForm_Click;
            // 
            // SwapToItemsForm
            // 
            SwapToItemsForm.BackgroundImage = (Image)resources.GetObject("SwapToItemsForm.BackgroundImage");
            SwapToItemsForm.Cursor = Cursors.Hand;
            SwapToItemsForm.FlatStyle = FlatStyle.Flat;
            SwapToItemsForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToItemsForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToItemsForm.ImeMode = ImeMode.NoControl;
            SwapToItemsForm.Location = new Point(259, 12);
            SwapToItemsForm.Name = "SwapToItemsForm";
            SwapToItemsForm.Size = new Size(130, 31);
            SwapToItemsForm.TabIndex = 645;
            SwapToItemsForm.Text = "Items";
            SwapToItemsForm.UseVisualStyleBackColor = true;
            SwapToItemsForm.Click += SwapToItemsForm_Click;
            // 
            // SwapToWeaponsForm
            // 
            SwapToWeaponsForm.BackgroundImage = (Image)resources.GetObject("SwapToWeaponsForm.BackgroundImage");
            SwapToWeaponsForm.Cursor = Cursors.Hand;
            SwapToWeaponsForm.FlatStyle = FlatStyle.Flat;
            SwapToWeaponsForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToWeaponsForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToWeaponsForm.ImeMode = ImeMode.NoControl;
            SwapToWeaponsForm.Location = new Point(123, 12);
            SwapToWeaponsForm.Name = "SwapToWeaponsForm";
            SwapToWeaponsForm.Size = new Size(130, 31);
            SwapToWeaponsForm.TabIndex = 644;
            SwapToWeaponsForm.Text = "Weapons";
            SwapToWeaponsForm.UseVisualStyleBackColor = true;
            SwapToWeaponsForm.Click += SwapToWeaponsForm_Click;
            // 
            // SwapToDebugForm
            // 
            SwapToDebugForm.BackgroundImage = (Image)resources.GetObject("SwapToDebugForm.BackgroundImage");
            SwapToDebugForm.Cursor = Cursors.Hand;
            SwapToDebugForm.FlatStyle = FlatStyle.Flat;
            SwapToDebugForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToDebugForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToDebugForm.ImeMode = ImeMode.NoControl;
            SwapToDebugForm.Location = new Point(939, 12);
            SwapToDebugForm.Name = "SwapToDebugForm";
            SwapToDebugForm.Size = new Size(130, 31);
            SwapToDebugForm.TabIndex = 651;
            SwapToDebugForm.Text = "Debug";
            SwapToDebugForm.UseVisualStyleBackColor = true;
            SwapToDebugForm.Click += SwapToDebugForm_Click;
            // 
            // StatsAndAlertForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1166, 707);
            Controls.Add(SwapToDebugForm);
            Controls.Add(SwapToBossForm);
            Controls.Add(SwapToMiscForm);
            Controls.Add(SwapToGameStatsForm);
            Controls.Add(SwapToCamoForm);
            Controls.Add(SwapToItemsForm);
            Controls.Add(SwapToWeaponsForm);
            Controls.Add(BatteryDrainCheckBox);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(LethalGroupBox);
            Controls.Add(textBox4);
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
            Text = "MGS3 Cheat Trainer - Stats and Alert - ANTIBigBoss - Version 2.8";
            Load += StatsAndAlertForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            LethalGroupBox.ResumeLayout(false);
            LethalGroupBox.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
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
        private TextBox textBox4;
        private GroupBox LethalGroupBox;
        private RadioButton NeckSnapLethalRadio;
        private RadioButton OneShotKillLethalRadio;
        private RadioButton VeryWeakLethalRadio;
        private RadioButton NormalLethalRadio;
        private RadioButton VeryStrongLethalRadio;
        private GroupBox groupBox2;
        private RadioButton OneShotSleepZzzRadio;
        private RadioButton VeryWeakZzzRadio;
        private RadioButton NormalZzzRadio;
        private RadioButton VeryStrongZzzRadio;
        private RadioButton InvincibleZzzRadio;
        private GroupBox groupBox3;
        private RadioButton OneShotStunStunRadio;
        private RadioButton VeryWeakStunRadio;
        private RadioButton NormalStunRadio;
        private RadioButton VeryStrongStunRadio;
        private RadioButton NeckSnapStunRadio;
        private CheckBox BatteryDrainCheckBox;
        private Button SwapToBossForm;
        private Button SwapToMiscForm;
        private Button SwapToGameStatsForm;
        private Button SwapToCamoForm;
        private Button SwapToItemsForm;
        private Button SwapToWeaponsForm;
        private Button SwapToDebugForm;
    }
}