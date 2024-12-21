namespace MGS3_MC_Cheat_Trainer
{
    partial class MiscForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiscForm));
            textBox29 = new TextBox();
            textBox4 = new TextBox();
            NormalCam = new Button();
            UpsideDownCam = new Button();
            ModelSlider = new TrackBar();
            textBox5 = new TextBox();
            ResetModelsToNormal = new Button();
            ModelChangeValue = new TextBox();
            ChangeModelNumber = new Button();
            Minus1ModelValue = new Button();
            Plus1ModelValue = new Button();
            ModelCurrentValue = new TextBox();
            textBox8 = new TextBox();
            TeleportGuardsToSnake = new Button();
            textBox1 = new TextBox();
            SnakeJump = new Button();
            CamoIndexSlider = new TrackBar();
            textBox2 = new TextBox();
            LadderSkip = new Button();
            textBox6 = new TextBox();
            SnakesPosition = new System.Windows.Forms.Timer(components);
            TextBoxSnakeX = new TextBox();
            TextBoxSnakeY = new TextBox();
            textBox9 = new TextBox();
            TextBoxSnakeZ = new TextBox();
            textBox12 = new TextBox();
            ParseTextBoxesPositions = new Button();
            textBox7 = new TextBox();
            ReadTextBoxSnakeZ = new TextBox();
            ReadTextBoxSnakeY = new TextBox();
            ReadTextBoxSnakeX = new TextBox();
            textBox14 = new TextBox();
            CamoIndexTimer = new System.Windows.Forms.Timer(components);
            CamoIndexChanges = new CheckBox();
            CamoIndexTextbox = new TextBox();
            FovSlider = new TrackBar();
            textBox10 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            PissFilterCheckBox = new CheckBox();
            CopySnakesLocationToTextboxes = new Button();
            MinimalHudcheckbox = new CheckBox();
            RealTimeItemSwapCheckbox = new CheckBox();
            DayChange = new Button();
            NightChange = new Button();
            noHudcheckbox = new CheckBox();
            SwapToBossForm = new Button();
            SwapToGameStatsForm = new Button();
            SwapToHealthAndAlertsForm = new Button();
            SwapToCamoForm = new Button();
            SwapToItemsForm = new Button();
            SwapToWeaponsForm = new Button();
            Plus1MultiValue = new Button();
            Minus1MultiValue = new Button();
            ChangeDamageMultiNumberButton = new Button();
            DamageToSnakeMultiTextbox = new TextBox();
            ActivateDamageMulti = new Button();
            textBox15 = new TextBox();
            DisableDamageMulti = new Button();
            ((System.ComponentModel.ISupportInitialize)ModelSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CamoIndexSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FovSlider).BeginInit();
            SuspendLayout();
            // 
            // textBox29
            // 
            textBox29.BackColor = SystemColors.ActiveCaptionText;
            textBox29.BorderStyle = BorderStyle.None;
            textBox29.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBox29.ForeColor = SystemColors.ActiveCaptionText;
            textBox29.Location = new Point(28, 50);
            textBox29.Multiline = true;
            textBox29.Name = "textBox29";
            textBox29.PlaceholderText = "Things that didn't fit into their own category are here";
            textBox29.ReadOnly = true;
            textBox29.Size = new Size(1111, 30);
            textBox29.TabIndex = 480;
            textBox29.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            textBox4.BackColor = SystemColors.ActiveCaptionText;
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox4.ForeColor = SystemColors.ActiveCaptionText;
            textBox4.Location = new Point(904, 100);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "HUD/Camera";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(256, 36);
            textBox4.TabIndex = 486;
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // NormalCam
            // 
            NormalCam.BackgroundImage = (Image)resources.GetObject("NormalCam.BackgroundImage");
            NormalCam.Cursor = Cursors.Hand;
            NormalCam.FlatStyle = FlatStyle.Flat;
            NormalCam.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NormalCam.ImageAlign = ContentAlignment.TopCenter;
            NormalCam.Location = new Point(904, 142);
            NormalCam.Name = "NormalCam";
            NormalCam.Size = new Size(256, 29);
            NormalCam.TabIndex = 489;
            NormalCam.Text = "Normal Camera";
            NormalCam.UseVisualStyleBackColor = true;
            NormalCam.Click += NormalCam_Click;
            // 
            // UpsideDownCam
            // 
            UpsideDownCam.BackgroundImage = (Image)resources.GetObject("UpsideDownCam.BackgroundImage");
            UpsideDownCam.Cursor = Cursors.Hand;
            UpsideDownCam.FlatStyle = FlatStyle.Flat;
            UpsideDownCam.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            UpsideDownCam.ImageAlign = ContentAlignment.TopCenter;
            UpsideDownCam.Location = new Point(904, 177);
            UpsideDownCam.Name = "UpsideDownCam";
            UpsideDownCam.Size = new Size(256, 29);
            UpsideDownCam.TabIndex = 490;
            UpsideDownCam.Text = "Upside Down Camera";
            UpsideDownCam.UseVisualStyleBackColor = true;
            UpsideDownCam.Click += UpsideDownCam_Click;
            // 
            // ModelSlider
            // 
            ModelSlider.BackColor = Color.FromArgb(36, 44, 36);
            ModelSlider.Location = new Point(663, 202);
            ModelSlider.Name = "ModelSlider";
            ModelSlider.Size = new Size(181, 45);
            ModelSlider.TabIndex = 504;
            ModelSlider.Scroll += ModelSlider_Scroll;
            // 
            // textBox5
            // 
            textBox5.BackColor = Color.Black;
            textBox5.BorderStyle = BorderStyle.None;
            textBox5.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox5.ForeColor = SystemColors.ButtonHighlight;
            textBox5.Location = new Point(622, 100);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.PlaceholderText = "Player/Enemy Models";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(260, 34);
            textBox5.TabIndex = 505;
            textBox5.TextAlign = HorizontalAlignment.Center;
            // 
            // ResetModelsToNormal
            // 
            ResetModelsToNormal.BackgroundImage = (Image)resources.GetObject("ResetModelsToNormal.BackgroundImage");
            ResetModelsToNormal.Cursor = Cursors.Hand;
            ResetModelsToNormal.FlatStyle = FlatStyle.Flat;
            ResetModelsToNormal.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ResetModelsToNormal.ImageAlign = ContentAlignment.TopCenter;
            ResetModelsToNormal.Location = new Point(622, 170);
            ResetModelsToNormal.Name = "ResetModelsToNormal";
            ResetModelsToNormal.Size = new Size(260, 29);
            ResetModelsToNormal.TabIndex = 506;
            ResetModelsToNormal.Text = "Normal Models (40)";
            ResetModelsToNormal.UseVisualStyleBackColor = true;
            ResetModelsToNormal.Click += ResetModelsToNormal_Click;
            // 
            // ModelChangeValue
            // 
            ModelChangeValue.Cursor = Cursors.IBeam;
            ModelChangeValue.Location = new Point(623, 143);
            ModelChangeValue.Name = "ModelChangeValue";
            ModelChangeValue.Size = new Size(32, 23);
            ModelChangeValue.TabIndex = 507;
            // 
            // ChangeModelNumber
            // 
            ChangeModelNumber.BackgroundImage = (Image)resources.GetObject("ChangeModelNumber.BackgroundImage");
            ChangeModelNumber.Cursor = Cursors.Hand;
            ChangeModelNumber.FlatStyle = FlatStyle.Flat;
            ChangeModelNumber.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ChangeModelNumber.ImageAlign = ContentAlignment.TopCenter;
            ChangeModelNumber.Location = new Point(663, 139);
            ChangeModelNumber.Name = "ChangeModelNumber";
            ChangeModelNumber.Size = new Size(219, 29);
            ChangeModelNumber.TabIndex = 508;
            ChangeModelNumber.Text = "Change to this number";
            ChangeModelNumber.UseVisualStyleBackColor = true;
            ChangeModelNumber.Click += ChangeModelNumber_Click;
            // 
            // Minus1ModelValue
            // 
            Minus1ModelValue.BackgroundImage = (Image)resources.GetObject("Minus1ModelValue.BackgroundImage");
            Minus1ModelValue.Cursor = Cursors.Hand;
            Minus1ModelValue.FlatStyle = FlatStyle.Flat;
            Minus1ModelValue.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Minus1ModelValue.ImageAlign = ContentAlignment.TopCenter;
            Minus1ModelValue.Location = new Point(622, 207);
            Minus1ModelValue.Name = "Minus1ModelValue";
            Minus1ModelValue.Size = new Size(37, 32);
            Minus1ModelValue.TabIndex = 509;
            Minus1ModelValue.Text = "- 1";
            Minus1ModelValue.UseVisualStyleBackColor = true;
            Minus1ModelValue.Click += Minus1ModelValue_Click;
            // 
            // Plus1ModelValue
            // 
            Plus1ModelValue.BackgroundImage = (Image)resources.GetObject("Plus1ModelValue.BackgroundImage");
            Plus1ModelValue.Cursor = Cursors.Hand;
            Plus1ModelValue.FlatStyle = FlatStyle.Flat;
            Plus1ModelValue.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Plus1ModelValue.ImageAlign = ContentAlignment.TopCenter;
            Plus1ModelValue.Location = new Point(846, 207);
            Plus1ModelValue.Name = "Plus1ModelValue";
            Plus1ModelValue.Size = new Size(36, 32);
            Plus1ModelValue.TabIndex = 510;
            Plus1ModelValue.Text = "+ 1";
            Plus1ModelValue.UseVisualStyleBackColor = true;
            Plus1ModelValue.Click += Plus1ModelValue_Click;
            // 
            // ModelCurrentValue
            // 
            ModelCurrentValue.Cursor = Cursors.IBeam;
            ModelCurrentValue.Enabled = false;
            ModelCurrentValue.Location = new Point(846, 250);
            ModelCurrentValue.Name = "ModelCurrentValue";
            ModelCurrentValue.Size = new Size(34, 23);
            ModelCurrentValue.TabIndex = 511;
            // 
            // textBox8
            // 
            textBox8.BackColor = SystemColors.ActiveCaptionText;
            textBox8.BorderStyle = BorderStyle.None;
            textBox8.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            textBox8.ForeColor = SystemColors.ActiveCaptionText;
            textBox8.Location = new Point(623, 245);
            textBox8.Multiline = true;
            textBox8.Name = "textBox8";
            textBox8.PlaceholderText = "Current Model # value:";
            textBox8.ReadOnly = true;
            textBox8.Size = new Size(218, 33);
            textBox8.TabIndex = 512;
            textBox8.TextAlign = HorizontalAlignment.Center;
            // 
            // TeleportGuardsToSnake
            // 
            TeleportGuardsToSnake.BackgroundImage = (Image)resources.GetObject("TeleportGuardsToSnake.BackgroundImage");
            TeleportGuardsToSnake.Cursor = Cursors.Hand;
            TeleportGuardsToSnake.FlatStyle = FlatStyle.Flat;
            TeleportGuardsToSnake.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            TeleportGuardsToSnake.ImageAlign = ContentAlignment.TopCenter;
            TeleportGuardsToSnake.Location = new Point(28, 187);
            TeleportGuardsToSnake.Name = "TeleportGuardsToSnake";
            TeleportGuardsToSnake.Size = new Size(241, 64);
            TeleportGuardsToSnake.TabIndex = 553;
            TeleportGuardsToSnake.Text = "Teleport Guards to Snake";
            TeleportGuardsToSnake.UseVisualStyleBackColor = true;
            TeleportGuardsToSnake.Click += TeleportGuardsToSnake_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ActiveCaptionText;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox1.ForeColor = SystemColors.ActiveCaptionText;
            textBox1.Location = new Point(28, 100);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Position Moving";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(241, 34);
            textBox1.TabIndex = 554;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // SnakeJump
            // 
            SnakeJump.BackgroundImage = (Image)resources.GetObject("SnakeJump.BackgroundImage");
            SnakeJump.Cursor = Cursors.Hand;
            SnakeJump.FlatStyle = FlatStyle.Flat;
            SnakeJump.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SnakeJump.ImageAlign = ContentAlignment.TopCenter;
            SnakeJump.Location = new Point(28, 140);
            SnakeJump.Name = "SnakeJump";
            SnakeJump.Size = new Size(241, 41);
            SnakeJump.TabIndex = 555;
            SnakeJump.Text = "Make Snake Jump";
            SnakeJump.UseVisualStyleBackColor = true;
            SnakeJump.Click += SnakeJump_Click;
            // 
            // CamoIndexSlider
            // 
            CamoIndexSlider.BackColor = Color.FromArgb(36, 44, 36);
            CamoIndexSlider.Location = new Point(304, 170);
            CamoIndexSlider.Name = "CamoIndexSlider";
            CamoIndexSlider.Size = new Size(289, 45);
            CamoIndexSlider.TabIndex = 561;
            CamoIndexSlider.Scroll += CamoIndexSlider_Scroll;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Black;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox2.ForeColor = SystemColors.ButtonHighlight;
            textBox2.Location = new Point(304, 100);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Camo Index";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(289, 34);
            textBox2.TabIndex = 564;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // LadderSkip
            // 
            LadderSkip.BackgroundImage = (Image)resources.GetObject("LadderSkip.BackgroundImage");
            LadderSkip.Cursor = Cursors.Hand;
            LadderSkip.FlatStyle = FlatStyle.Flat;
            LadderSkip.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LadderSkip.ImageAlign = ContentAlignment.TopCenter;
            LadderSkip.Location = new Point(28, 257);
            LadderSkip.Name = "LadderSkip";
            LadderSkip.Size = new Size(241, 179);
            LadderSkip.TabIndex = 567;
            LadderSkip.Text = resources.GetString("LadderSkip.Text");
            LadderSkip.UseVisualStyleBackColor = true;
            LadderSkip.Click += LadderSkip_Click_1;
            // 
            // textBox6
            // 
            textBox6.BackColor = SystemColors.ActiveCaptionText;
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox6.ForeColor = SystemColors.ActiveCaptionText;
            textBox6.Location = new Point(275, 285);
            textBox6.Multiline = true;
            textBox6.Name = "textBox6";
            textBox6.PlaceholderText = "Snake's X ";
            textBox6.ReadOnly = true;
            textBox6.Size = new Size(116, 34);
            textBox6.TabIndex = 571;
            textBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // SnakesPosition
            // 
            SnakesPosition.Tick += SnakesPosition_Tick;
            // 
            // TextBoxSnakeX
            // 
            TextBoxSnakeX.Location = new Point(275, 455);
            TextBoxSnakeX.Name = "TextBoxSnakeX";
            TextBoxSnakeX.Size = new Size(119, 23);
            TextBoxSnakeX.TabIndex = 574;
            // 
            // TextBoxSnakeY
            // 
            TextBoxSnakeY.Location = new Point(400, 455);
            TextBoxSnakeY.Name = "TextBoxSnakeY";
            TextBoxSnakeY.Size = new Size(119, 23);
            TextBoxSnakeY.TabIndex = 576;
            // 
            // textBox9
            // 
            textBox9.BackColor = SystemColors.ActiveCaptionText;
            textBox9.BorderStyle = BorderStyle.None;
            textBox9.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox9.ForeColor = SystemColors.ActiveCaptionText;
            textBox9.Location = new Point(399, 285);
            textBox9.Multiline = true;
            textBox9.Name = "textBox9";
            textBox9.PlaceholderText = "Snake's Y ";
            textBox9.ReadOnly = true;
            textBox9.Size = new Size(119, 34);
            textBox9.TabIndex = 575;
            textBox9.TextAlign = HorizontalAlignment.Center;
            // 
            // TextBoxSnakeZ
            // 
            TextBoxSnakeZ.Location = new Point(525, 455);
            TextBoxSnakeZ.Name = "TextBoxSnakeZ";
            TextBoxSnakeZ.Size = new Size(125, 23);
            TextBoxSnakeZ.TabIndex = 578;
            // 
            // textBox12
            // 
            textBox12.BackColor = SystemColors.ActiveCaptionText;
            textBox12.BorderStyle = BorderStyle.None;
            textBox12.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox12.ForeColor = SystemColors.ActiveCaptionText;
            textBox12.Location = new Point(522, 285);
            textBox12.Multiline = true;
            textBox12.Name = "textBox12";
            textBox12.PlaceholderText = "Snake's Z";
            textBox12.ReadOnly = true;
            textBox12.Size = new Size(129, 34);
            textBox12.TabIndex = 577;
            textBox12.TextAlign = HorizontalAlignment.Center;
            // 
            // ParseTextBoxesPositions
            // 
            ParseTextBoxesPositions.BackgroundImage = (Image)resources.GetObject("ParseTextBoxesPositions.BackgroundImage");
            ParseTextBoxesPositions.Cursor = Cursors.Hand;
            ParseTextBoxesPositions.FlatStyle = FlatStyle.Flat;
            ParseTextBoxesPositions.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ParseTextBoxesPositions.ImageAlign = ContentAlignment.TopCenter;
            ParseTextBoxesPositions.Location = new Point(274, 479);
            ParseTextBoxesPositions.Name = "ParseTextBoxesPositions";
            ParseTextBoxesPositions.Size = new Size(376, 37);
            ParseTextBoxesPositions.TabIndex = 579;
            ParseTextBoxesPositions.Text = "Change Snake's location";
            ParseTextBoxesPositions.UseVisualStyleBackColor = true;
            ParseTextBoxesPositions.Click += ParseTextBoxesPositions_Click;
            // 
            // textBox7
            // 
            textBox7.BackColor = SystemColors.ActiveCaptionText;
            textBox7.BorderStyle = BorderStyle.None;
            textBox7.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBox7.ForeColor = SystemColors.ActiveCaptionText;
            textBox7.Location = new Point(275, 419);
            textBox7.Multiline = true;
            textBox7.Name = "textBox7";
            textBox7.PlaceholderText = "Edit these to change Snake's location";
            textBox7.ReadOnly = true;
            textBox7.Size = new Size(376, 30);
            textBox7.TabIndex = 580;
            textBox7.TextAlign = HorizontalAlignment.Center;
            // 
            // ReadTextBoxSnakeZ
            // 
            ReadTextBoxSnakeZ.Enabled = false;
            ReadTextBoxSnakeZ.Location = new Point(522, 355);
            ReadTextBoxSnakeZ.Name = "ReadTextBoxSnakeZ";
            ReadTextBoxSnakeZ.Size = new Size(128, 23);
            ReadTextBoxSnakeZ.TabIndex = 583;
            // 
            // ReadTextBoxSnakeY
            // 
            ReadTextBoxSnakeY.Enabled = false;
            ReadTextBoxSnakeY.Location = new Point(397, 355);
            ReadTextBoxSnakeY.Name = "ReadTextBoxSnakeY";
            ReadTextBoxSnakeY.Size = new Size(119, 23);
            ReadTextBoxSnakeY.TabIndex = 582;
            // 
            // ReadTextBoxSnakeX
            // 
            ReadTextBoxSnakeX.Enabled = false;
            ReadTextBoxSnakeX.Location = new Point(276, 355);
            ReadTextBoxSnakeX.Name = "ReadTextBoxSnakeX";
            ReadTextBoxSnakeX.Size = new Size(116, 23);
            ReadTextBoxSnakeX.TabIndex = 581;
            // 
            // textBox14
            // 
            textBox14.BackColor = SystemColors.ActiveCaptionText;
            textBox14.BorderStyle = BorderStyle.None;
            textBox14.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBox14.ForeColor = SystemColors.ActiveCaptionText;
            textBox14.Location = new Point(275, 321);
            textBox14.Multiline = true;
            textBox14.Name = "textBox14";
            textBox14.PlaceholderText = "These textboxes only read Snake's location";
            textBox14.ReadOnly = true;
            textBox14.Size = new Size(376, 30);
            textBox14.TabIndex = 584;
            textBox14.TextAlign = HorizontalAlignment.Center;
            // 
            // CamoIndexTimer
            // 
            CamoIndexTimer.Tick += CamoIndexTimer_Tick;
            // 
            // CamoIndexChanges
            // 
            CamoIndexChanges.BackgroundImage = Properties.Resources.Selected_MGS3_Menu_without_button;
            CamoIndexChanges.FlatStyle = FlatStyle.Flat;
            CamoIndexChanges.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            CamoIndexChanges.Image = (Image)resources.GetObject("CamoIndexChanges.Image");
            CamoIndexChanges.Location = new Point(304, 192);
            CamoIndexChanges.Name = "CamoIndexChanges";
            CamoIndexChanges.Size = new Size(289, 87);
            CamoIndexChanges.TabIndex = 586;
            CamoIndexChanges.Text = "Enable Camo Index changes the game\r\n will stop updating your camo index \r\nwith this and Camo will be set to 0% \r\nwhen an area changes";
            CamoIndexChanges.UseVisualStyleBackColor = true;
            CamoIndexChanges.CheckedChanged += CamoIndexChanges_CheckedChanged;
            // 
            // CamoIndexTextbox
            // 
            CamoIndexTextbox.Enabled = false;
            CamoIndexTextbox.Location = new Point(304, 139);
            CamoIndexTextbox.Name = "CamoIndexTextbox";
            CamoIndexTextbox.Size = new Size(289, 23);
            CamoIndexTextbox.TabIndex = 588;
            // 
            // FovSlider
            // 
            FovSlider.BackColor = Color.FromArgb(36, 44, 36);
            FovSlider.Location = new Point(861, 615);
            FovSlider.Name = "FovSlider";
            FovSlider.Size = new Size(299, 45);
            FovSlider.TabIndex = 591;
            FovSlider.Scroll += FovSlider_Scroll;
            // 
            // textBox10
            // 
            textBox10.BackColor = SystemColors.ActiveCaptionText;
            textBox10.BorderStyle = BorderStyle.None;
            textBox10.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox10.ForeColor = SystemColors.ActiveCaptionText;
            textBox10.Location = new Point(861, 573);
            textBox10.Multiline = true;
            textBox10.Name = "textBox10";
            textBox10.PlaceholderText = "FOV Slider";
            textBox10.ReadOnly = true;
            textBox10.Size = new Size(299, 36);
            textBox10.TabIndex = 592;
            textBox10.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(36, 44, 36);
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(860, 643);
            label1.Name = "label1";
            label1.Size = new Size(31, 20);
            label1.TabIndex = 593;
            label1.Text = "0.5";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(36, 44, 36);
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(1132, 643);
            label2.Name = "label2";
            label2.Size = new Size(31, 20);
            label2.TabIndex = 594;
            label2.Text = "1.5";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(36, 44, 36);
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(987, 643);
            label3.Name = "label3";
            label3.Size = new Size(31, 20);
            label3.TabIndex = 595;
            label3.Text = "1.0";
            // 
            // PissFilterCheckBox
            // 
            PissFilterCheckBox.BackgroundImage = Properties.Resources.Selected_MGS3_Menu_without_button;
            PissFilterCheckBox.FlatStyle = FlatStyle.Flat;
            PissFilterCheckBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            PissFilterCheckBox.Image = (Image)resources.GetObject("PissFilterCheckBox.Image");
            PissFilterCheckBox.Location = new Point(904, 211);
            PissFilterCheckBox.Name = "PissFilterCheckBox";
            PissFilterCheckBox.Size = new Size(256, 126);
            PissFilterCheckBox.TabIndex = 596;
            PissFilterCheckBox.Text = "Disable/Enable the Piss Filter \r\n\r\nUnchecked = Enabled (Default)\r\n\r\nChecked = Disabled\r\n";
            PissFilterCheckBox.UseVisualStyleBackColor = true;
            PissFilterCheckBox.CheckedChanged += PissFilterCheckBox_CheckedChanged;
            // 
            // CopySnakesLocationToTextboxes
            // 
            CopySnakesLocationToTextboxes.BackgroundImage = (Image)resources.GetObject("CopySnakesLocationToTextboxes.BackgroundImage");
            CopySnakesLocationToTextboxes.Cursor = Cursors.Hand;
            CopySnakesLocationToTextboxes.FlatStyle = FlatStyle.Flat;
            CopySnakesLocationToTextboxes.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            CopySnakesLocationToTextboxes.ImageAlign = ContentAlignment.TopCenter;
            CopySnakesLocationToTextboxes.Location = new Point(275, 379);
            CopySnakesLocationToTextboxes.Name = "CopySnakesLocationToTextboxes";
            CopySnakesLocationToTextboxes.Size = new Size(375, 37);
            CopySnakesLocationToTextboxes.TabIndex = 597;
            CopySnakesLocationToTextboxes.Text = "Copy Snake's Current location to the textboxes";
            CopySnakesLocationToTextboxes.UseVisualStyleBackColor = true;
            CopySnakesLocationToTextboxes.Click += CopySnakesLocationToTextboxes_Click;
            // 
            // MinimalHudcheckbox
            // 
            MinimalHudcheckbox.BackgroundImage = Properties.Resources.Selected_MGS3_Menu_without_button;
            MinimalHudcheckbox.FlatStyle = FlatStyle.Flat;
            MinimalHudcheckbox.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            MinimalHudcheckbox.Image = (Image)resources.GetObject("MinimalHudcheckbox.Image");
            MinimalHudcheckbox.Location = new Point(904, 461);
            MinimalHudcheckbox.Name = "MinimalHudcheckbox";
            MinimalHudcheckbox.Size = new Size(256, 40);
            MinimalHudcheckbox.TabIndex = 598;
            MinimalHudcheckbox.Text = "Minimal HUD";
            MinimalHudcheckbox.UseVisualStyleBackColor = true;
            MinimalHudcheckbox.CheckedChanged += MinimalHudcheckbox_CheckedChanged;
            // 
            // RealTimeItemSwapCheckbox
            // 
            RealTimeItemSwapCheckbox.BackgroundImage = Properties.Resources.Selected_MGS3_Menu_without_button;
            RealTimeItemSwapCheckbox.FlatStyle = FlatStyle.Flat;
            RealTimeItemSwapCheckbox.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            RealTimeItemSwapCheckbox.Image = (Image)resources.GetObject("RealTimeItemSwapCheckbox.Image");
            RealTimeItemSwapCheckbox.Location = new Point(275, 522);
            RealTimeItemSwapCheckbox.Name = "RealTimeItemSwapCheckbox";
            RealTimeItemSwapCheckbox.Size = new Size(376, 124);
            RealTimeItemSwapCheckbox.TabIndex = 599;
            RealTimeItemSwapCheckbox.Text = "Real Time Item/Weapon Swap\r\n\r\nChecked = Time doesn't stop when holding down L2/R2 weapon and item windows\r\n\r\nUnchecked = Time stops normally\r\n";
            RealTimeItemSwapCheckbox.UseVisualStyleBackColor = true;
            RealTimeItemSwapCheckbox.Visible = false;
            RealTimeItemSwapCheckbox.CheckedChanged += RealTimeItemSwapCheckbox_CheckedChanged;
            // 
            // DayChange
            // 
            DayChange.BackgroundImage = (Image)resources.GetObject("DayChange.BackgroundImage");
            DayChange.Cursor = Cursors.Hand;
            DayChange.FlatStyle = FlatStyle.Flat;
            DayChange.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            DayChange.ImageAlign = ContentAlignment.TopCenter;
            DayChange.Location = new Point(904, 357);
            DayChange.Name = "DayChange";
            DayChange.Size = new Size(256, 37);
            DayChange.TabIndex = 600;
            DayChange.Text = "Switch to Day Time";
            DayChange.UseVisualStyleBackColor = true;
            DayChange.Click += DayChange_Click;
            // 
            // NightChange
            // 
            NightChange.BackgroundImage = (Image)resources.GetObject("NightChange.BackgroundImage");
            NightChange.Cursor = Cursors.Hand;
            NightChange.FlatStyle = FlatStyle.Flat;
            NightChange.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NightChange.ImageAlign = ContentAlignment.TopCenter;
            NightChange.Location = new Point(904, 407);
            NightChange.Name = "NightChange";
            NightChange.Size = new Size(256, 37);
            NightChange.TabIndex = 601;
            NightChange.Text = "Switch to Night Time";
            NightChange.UseVisualStyleBackColor = true;
            NightChange.Click += NightChange_Click;
            // 
            // noHudcheckbox
            // 
            noHudcheckbox.BackgroundImage = Properties.Resources.Selected_MGS3_Menu_without_button;
            noHudcheckbox.FlatStyle = FlatStyle.Flat;
            noHudcheckbox.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            noHudcheckbox.Image = (Image)resources.GetObject("noHudcheckbox.Image");
            noHudcheckbox.Location = new Point(904, 517);
            noHudcheckbox.Name = "noHudcheckbox";
            noHudcheckbox.Size = new Size(256, 40);
            noHudcheckbox.TabIndex = 602;
            noHudcheckbox.Text = "No HUD";
            noHudcheckbox.UseVisualStyleBackColor = true;
            noHudcheckbox.CheckedChanged += noHudcheckbox_CheckedChanged;
            // 
            // SwapToBossForm
            // 
            SwapToBossForm.BackgroundImage = (Image)resources.GetObject("SwapToBossForm.BackgroundImage");
            SwapToBossForm.Cursor = Cursors.Hand;
            SwapToBossForm.FlatStyle = FlatStyle.Flat;
            SwapToBossForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToBossForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToBossForm.ImeMode = ImeMode.NoControl;
            SwapToBossForm.Location = new Point(740, 12);
            SwapToBossForm.Name = "SwapToBossForm";
            SwapToBossForm.Size = new Size(130, 31);
            SwapToBossForm.TabIndex = 650;
            SwapToBossForm.Text = "Bosses";
            SwapToBossForm.UseVisualStyleBackColor = true;
            SwapToBossForm.Click += SwapToBossForm_Click;
            // 
            // SwapToGameStatsForm
            // 
            SwapToGameStatsForm.BackgroundImage = (Image)resources.GetObject("SwapToGameStatsForm.BackgroundImage");
            SwapToGameStatsForm.Cursor = Cursors.Hand;
            SwapToGameStatsForm.FlatStyle = FlatStyle.Flat;
            SwapToGameStatsForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToGameStatsForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToGameStatsForm.ImeMode = ImeMode.NoControl;
            SwapToGameStatsForm.Location = new Point(876, 12);
            SwapToGameStatsForm.Name = "SwapToGameStatsForm";
            SwapToGameStatsForm.Size = new Size(130, 31);
            SwapToGameStatsForm.TabIndex = 648;
            SwapToGameStatsForm.Text = "Game Stats";
            SwapToGameStatsForm.UseVisualStyleBackColor = true;
            SwapToGameStatsForm.Click += SwapToGameStatsForm_Click;
            // 
            // SwapToHealthAndAlertsForm
            // 
            SwapToHealthAndAlertsForm.BackgroundImage = (Image)resources.GetObject("SwapToHealthAndAlertsForm.BackgroundImage");
            SwapToHealthAndAlertsForm.Cursor = Cursors.Hand;
            SwapToHealthAndAlertsForm.FlatStyle = FlatStyle.Flat;
            SwapToHealthAndAlertsForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToHealthAndAlertsForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToHealthAndAlertsForm.ImeMode = ImeMode.NoControl;
            SwapToHealthAndAlertsForm.Location = new Point(604, 12);
            SwapToHealthAndAlertsForm.Name = "SwapToHealthAndAlertsForm";
            SwapToHealthAndAlertsForm.Size = new Size(130, 31);
            SwapToHealthAndAlertsForm.TabIndex = 647;
            SwapToHealthAndAlertsForm.Text = "Health/Alerts";
            SwapToHealthAndAlertsForm.UseVisualStyleBackColor = true;
            SwapToHealthAndAlertsForm.Click += SwapToHealthAndAlertsForm_Click;
            // 
            // SwapToCamoForm
            // 
            SwapToCamoForm.BackgroundImage = (Image)resources.GetObject("SwapToCamoForm.BackgroundImage");
            SwapToCamoForm.Cursor = Cursors.Hand;
            SwapToCamoForm.FlatStyle = FlatStyle.Flat;
            SwapToCamoForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToCamoForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToCamoForm.ImeMode = ImeMode.NoControl;
            SwapToCamoForm.Location = new Point(468, 12);
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
            SwapToItemsForm.Location = new Point(332, 12);
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
            SwapToWeaponsForm.Location = new Point(196, 12);
            SwapToWeaponsForm.Name = "SwapToWeaponsForm";
            SwapToWeaponsForm.Size = new Size(130, 31);
            SwapToWeaponsForm.TabIndex = 644;
            SwapToWeaponsForm.Text = "Weapons";
            SwapToWeaponsForm.UseVisualStyleBackColor = true;
            SwapToWeaponsForm.Click += SwapToWeaponsForm_Click;
            // 
            // Plus1MultiValue
            // 
            Plus1MultiValue.BackgroundImage = (Image)resources.GetObject("Plus1MultiValue.BackgroundImage");
            Plus1MultiValue.Cursor = Cursors.Hand;
            Plus1MultiValue.FlatStyle = FlatStyle.Flat;
            Plus1MultiValue.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Plus1MultiValue.ImageAlign = ContentAlignment.TopCenter;
            Plus1MultiValue.Location = new Point(143, 615);
            Plus1MultiValue.Name = "Plus1MultiValue";
            Plus1MultiValue.Size = new Size(126, 32);
            Plus1MultiValue.TabIndex = 657;
            Plus1MultiValue.Text = "+ 1";
            Plus1MultiValue.UseVisualStyleBackColor = true;
            Plus1MultiValue.Click += Plus1MultiValue_Click;
            // 
            // Minus1MultiValue
            // 
            Minus1MultiValue.BackgroundImage = (Image)resources.GetObject("Minus1MultiValue.BackgroundImage");
            Minus1MultiValue.Cursor = Cursors.Hand;
            Minus1MultiValue.FlatStyle = FlatStyle.Flat;
            Minus1MultiValue.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Minus1MultiValue.ImageAlign = ContentAlignment.TopCenter;
            Minus1MultiValue.Location = new Point(9, 615);
            Minus1MultiValue.Name = "Minus1MultiValue";
            Minus1MultiValue.Size = new Size(112, 32);
            Minus1MultiValue.TabIndex = 656;
            Minus1MultiValue.Text = "- 1";
            Minus1MultiValue.UseVisualStyleBackColor = true;
            Minus1MultiValue.Click += Minus1MultiValue_Click;
            // 
            // ChangeDamageMultiNumberButton
            // 
            ChangeDamageMultiNumberButton.BackgroundImage = (Image)resources.GetObject("ChangeDamageMultiNumberButton.BackgroundImage");
            ChangeDamageMultiNumberButton.Cursor = Cursors.Hand;
            ChangeDamageMultiNumberButton.FlatStyle = FlatStyle.Flat;
            ChangeDamageMultiNumberButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ChangeDamageMultiNumberButton.ImageAlign = ContentAlignment.TopCenter;
            ChangeDamageMultiNumberButton.Location = new Point(49, 516);
            ChangeDamageMultiNumberButton.Name = "ChangeDamageMultiNumberButton";
            ChangeDamageMultiNumberButton.Size = new Size(219, 29);
            ChangeDamageMultiNumberButton.TabIndex = 655;
            ChangeDamageMultiNumberButton.Text = "Change to this number";
            ChangeDamageMultiNumberButton.UseVisualStyleBackColor = true;
            ChangeDamageMultiNumberButton.Click += ChangeDamageMultiNumberButton_Click;
            // 
            // DamageToSnakeMultiTextbox
            // 
            DamageToSnakeMultiTextbox.Cursor = Cursors.IBeam;
            DamageToSnakeMultiTextbox.Location = new Point(9, 520);
            DamageToSnakeMultiTextbox.Name = "DamageToSnakeMultiTextbox";
            DamageToSnakeMultiTextbox.Size = new Size(32, 23);
            DamageToSnakeMultiTextbox.TabIndex = 654;
            // 
            // ActivateDamageMulti
            // 
            ActivateDamageMulti.BackgroundImage = (Image)resources.GetObject("ActivateDamageMulti.BackgroundImage");
            ActivateDamageMulti.Cursor = Cursors.Hand;
            ActivateDamageMulti.FlatStyle = FlatStyle.Flat;
            ActivateDamageMulti.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ActivateDamageMulti.ImageAlign = ContentAlignment.TopCenter;
            ActivateDamageMulti.Location = new Point(8, 547);
            ActivateDamageMulti.Name = "ActivateDamageMulti";
            ActivateDamageMulti.Size = new Size(260, 29);
            ActivateDamageMulti.TabIndex = 653;
            ActivateDamageMulti.Text = "Enable Damage Multi to Snake";
            ActivateDamageMulti.UseVisualStyleBackColor = true;
            ActivateDamageMulti.Click += ActivateDamageMulti_Click;
            // 
            // textBox15
            // 
            textBox15.BackColor = Color.Black;
            textBox15.BorderStyle = BorderStyle.None;
            textBox15.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            textBox15.ForeColor = SystemColors.ButtonHighlight;
            textBox15.Location = new Point(8, 477);
            textBox15.Multiline = true;
            textBox15.Name = "textBox15";
            textBox15.PlaceholderText = "Damage to Snake Multiplier";
            textBox15.ReadOnly = true;
            textBox15.Size = new Size(260, 34);
            textBox15.TabIndex = 652;
            textBox15.TextAlign = HorizontalAlignment.Center;
            // 
            // DisableDamageMulti
            // 
            DisableDamageMulti.BackgroundImage = (Image)resources.GetObject("DisableDamageMulti.BackgroundImage");
            DisableDamageMulti.Cursor = Cursors.Hand;
            DisableDamageMulti.FlatStyle = FlatStyle.Flat;
            DisableDamageMulti.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            DisableDamageMulti.ImageAlign = ContentAlignment.TopCenter;
            DisableDamageMulti.Location = new Point(7, 580);
            DisableDamageMulti.Name = "DisableDamageMulti";
            DisableDamageMulti.Size = new Size(260, 29);
            DisableDamageMulti.TabIndex = 660;
            DisableDamageMulti.Text = "Disable Damage Multi to Snake";
            DisableDamageMulti.UseVisualStyleBackColor = true;
            DisableDamageMulti.Click += DisableDamageMulti_Click;
            // 
            // MiscForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1166, 707);
            Controls.Add(DisableDamageMulti);
            Controls.Add(Plus1MultiValue);
            Controls.Add(Minus1MultiValue);
            Controls.Add(ChangeDamageMultiNumberButton);
            Controls.Add(DamageToSnakeMultiTextbox);
            Controls.Add(ActivateDamageMulti);
            Controls.Add(textBox15);
            Controls.Add(SwapToBossForm);
            Controls.Add(SwapToGameStatsForm);
            Controls.Add(SwapToHealthAndAlertsForm);
            Controls.Add(SwapToCamoForm);
            Controls.Add(SwapToItemsForm);
            Controls.Add(SwapToWeaponsForm);
            Controls.Add(noHudcheckbox);
            Controls.Add(NightChange);
            Controls.Add(DayChange);
            Controls.Add(RealTimeItemSwapCheckbox);
            Controls.Add(MinimalHudcheckbox);
            Controls.Add(CopySnakesLocationToTextboxes);
            Controls.Add(PissFilterCheckBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox10);
            Controls.Add(FovSlider);
            Controls.Add(CamoIndexTextbox);
            Controls.Add(CamoIndexChanges);
            Controls.Add(textBox14);
            Controls.Add(ReadTextBoxSnakeZ);
            Controls.Add(ReadTextBoxSnakeY);
            Controls.Add(ReadTextBoxSnakeX);
            Controls.Add(textBox7);
            Controls.Add(ParseTextBoxesPositions);
            Controls.Add(TextBoxSnakeZ);
            Controls.Add(textBox12);
            Controls.Add(TextBoxSnakeY);
            Controls.Add(textBox9);
            Controls.Add(TextBoxSnakeX);
            Controls.Add(textBox6);
            Controls.Add(LadderSkip);
            Controls.Add(textBox2);
            Controls.Add(CamoIndexSlider);
            Controls.Add(SnakeJump);
            Controls.Add(textBox1);
            Controls.Add(TeleportGuardsToSnake);
            Controls.Add(textBox8);
            Controls.Add(ModelCurrentValue);
            Controls.Add(Plus1ModelValue);
            Controls.Add(Minus1ModelValue);
            Controls.Add(ChangeModelNumber);
            Controls.Add(ModelChangeValue);
            Controls.Add(ResetModelsToNormal);
            Controls.Add(textBox5);
            Controls.Add(ModelSlider);
            Controls.Add(UpsideDownCam);
            Controls.Add(NormalCam);
            Controls.Add(textBox4);
            Controls.Add(textBox29);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MiscForm";
            Text = "MGS3 Cheat Trainer - Stats and Misc - ANTIBigBoss - Version 2.7";
            Load += Form4_Load;
            ((System.ComponentModel.ISupportInitialize)ModelSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)CamoIndexSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)FovSlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox29;
        private TextBox textBox4;
        private Button NormalCam;
        private Button UpsideDownCam;
        private TrackBar ModelSlider;
        private TextBox textBox5;
        private Button ResetModelsToNormal;
        private TextBox ModelChangeValue;
        private Button ChangeModelNumber;
        private Button Minus1ModelValue;
        private Button Plus1ModelValue;
        private TextBox ModelCurrentValue;
        private TextBox textBox8;
        private Button TeleportGuardsToSnake;
        private TextBox textBox1;
        private Button SnakeJump;
        private TrackBar CamoIndexSlider;
        private TextBox textBox2;
        private Button LadderSkip;
        private TextBox textBox6;
        private System.Windows.Forms.Timer SnakesPosition;
        private TextBox TextBoxSnakeX;
        private TextBox TextBoxSnakeY;
        private TextBox textBox9;
        private TextBox TextBoxSnakeZ;
        private TextBox textBox12;
        private Button ParseTextBoxesPositions;
        private TextBox textBox7;
        private TextBox ReadTextBoxSnakeZ;
        private TextBox ReadTextBoxSnakeY;
        private TextBox ReadTextBoxSnakeX;
        private TextBox textBox14;
        private System.Windows.Forms.Timer CamoIndexTimer;
        private CheckBox CamoIndexChanges;
        private TextBox CamoIndexTextbox;
        private TrackBar FovSlider;
        private TextBox textBox10;
        private Label label1;
        private Label label2;
        private Label label3;
        private CheckBox checkBox1;
        private CheckBox PissFilterCheckBox;
        private Button CopySnakesLocationToTextboxes;
        private CheckBox MinimalHudcheckbox;
        private CheckBox RealTimeItemSwapCheckbox;
        private Button DayChange;
        private Button NightChange;
        private CheckBox noHudcheckbox;
        private Button SwapToBossForm;
        private Button SwapToGameStatsForm;
        private Button SwapToHealthAndAlertsForm;
        private Button SwapToCamoForm;
        private Button SwapToItemsForm;
        private Button SwapToWeaponsForm;
        private Button Plus1MultiValue;
        private Button Minus1MultiValue;
        private Button ChangeDamageMultiNumberButton;
        private TextBox DamageToSnakeMultiTextbox;
        private Button ActivateDamageMulti;
        private TextBox textBox15;
        private Button DisableDamageMulti;
    }
}