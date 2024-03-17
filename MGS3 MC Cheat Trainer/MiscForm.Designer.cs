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
            button1 = new Button();
            textBox29 = new TextBox();
            button2 = new Button();
            WeaponFormSwap = new Button();
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
            HealthFormSwap = new Button();
            SwapToBossForm = new Button();
            TeleportGuardsToSnake = new Button();
            textBox1 = new TextBox();
            SnakeJump = new Button();
            SnakesXYZaob = new Button();
            NopCamo = new Button();
            RestoreCamo = new Button();
            LogAOBs = new Button();
            CamoIndexSlider = new TrackBar();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
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
            ((System.ComponentModel.ISupportInitialize)ModelSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CamoIndexSlider).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ImageAlign = ContentAlignment.TopCenter;
            button1.ImeMode = ImeMode.NoControl;
            button1.Location = new Point(487, 12);
            button1.Name = "button1";
            button1.Size = new Size(203, 32);
            button1.TabIndex = 481;
            button1.Text = "Switch to Camo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox29
            // 
            textBox29.BackColor = SystemColors.ActiveCaptionText;
            textBox29.BorderStyle = BorderStyle.None;
            textBox29.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBox29.ForeColor = SystemColors.ActiveCaptionText;
            textBox29.Location = new Point(20, 64);
            textBox29.Multiline = true;
            textBox29.Name = "textBox29";
            textBox29.PlaceholderText = "Random things for fun (Warning may crash game)    ";
            textBox29.ReadOnly = true;
            textBox29.Size = new Size(514, 30);
            textBox29.TabIndex = 480;
            textBox29.TextAlign = HorizontalAlignment.Center;
            // 
            // button2
            // 
            button2.BackgroundImage = (Image)resources.GetObject("button2.BackgroundImage");
            button2.Cursor = Cursors.Hand;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ImageAlign = ContentAlignment.TopCenter;
            button2.ImeMode = ImeMode.NoControl;
            button2.Location = new Point(255, 12);
            button2.Name = "button2";
            button2.Size = new Size(203, 32);
            button2.TabIndex = 479;
            button2.Text = "Switch to Items";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
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
            WeaponFormSwap.TabIndex = 478;
            WeaponFormSwap.Text = "Switch to Weapons";
            WeaponFormSwap.UseVisualStyleBackColor = true;
            WeaponFormSwap.Click += WeaponFormSwap_Click;
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
            textBox4.Size = new Size(241, 36);
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
            NormalCam.Size = new Size(241, 29);
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
            UpsideDownCam.Size = new Size(241, 29);
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
            ModelCurrentValue.Location = new Point(845, 256);
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
            textBox8.Location = new Point(622, 251);
            textBox8.Multiline = true;
            textBox8.Name = "textBox8";
            textBox8.PlaceholderText = "Current Model # value:";
            textBox8.ReadOnly = true;
            textBox8.Size = new Size(218, 33);
            textBox8.TabIndex = 512;
            textBox8.TextAlign = HorizontalAlignment.Center;
            // 
            // HealthFormSwap
            // 
            HealthFormSwap.BackgroundImage = (Image)resources.GetObject("HealthFormSwap.BackgroundImage");
            HealthFormSwap.Cursor = Cursors.Hand;
            HealthFormSwap.FlatStyle = FlatStyle.Flat;
            HealthFormSwap.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            HealthFormSwap.ImageAlign = ContentAlignment.TopCenter;
            HealthFormSwap.ImeMode = ImeMode.NoControl;
            HealthFormSwap.Location = new Point(717, 12);
            HealthFormSwap.Name = "HealthFormSwap";
            HealthFormSwap.Size = new Size(203, 32);
            HealthFormSwap.TabIndex = 513;
            HealthFormSwap.Text = "Switch Health/Alerts";
            HealthFormSwap.UseVisualStyleBackColor = true;
            HealthFormSwap.Click += HealthFormSwap_Click;
            // 
            // SwapToBossForm
            // 
            SwapToBossForm.BackgroundImage = (Image)resources.GetObject("SwapToBossForm.BackgroundImage");
            SwapToBossForm.Cursor = Cursors.Hand;
            SwapToBossForm.FlatStyle = FlatStyle.Flat;
            SwapToBossForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToBossForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToBossForm.ImeMode = ImeMode.NoControl;
            SwapToBossForm.Location = new Point(936, 12);
            SwapToBossForm.Name = "SwapToBossForm";
            SwapToBossForm.Size = new Size(203, 32);
            SwapToBossForm.TabIndex = 552;
            SwapToBossForm.Text = "Bosses";
            SwapToBossForm.UseVisualStyleBackColor = true;
            SwapToBossForm.Click += SwapToBossForm_Click;
            // 
            // TeleportGuardsToSnake
            // 
            TeleportGuardsToSnake.BackgroundImage = (Image)resources.GetObject("TeleportGuardsToSnake.BackgroundImage");
            TeleportGuardsToSnake.Cursor = Cursors.Hand;
            TeleportGuardsToSnake.FlatStyle = FlatStyle.Flat;
            TeleportGuardsToSnake.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            TeleportGuardsToSnake.ImageAlign = ContentAlignment.TopCenter;
            TeleportGuardsToSnake.Location = new Point(28, 273);
            TeleportGuardsToSnake.Name = "TeleportGuardsToSnake";
            TeleportGuardsToSnake.Size = new Size(241, 64);
            TeleportGuardsToSnake.TabIndex = 553;
            TeleportGuardsToSnake.Text = "Teleport Guards to Snake (Takes like 20ish Seconds)";
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
            SnakeJump.Location = new Point(28, 226);
            SnakeJump.Name = "SnakeJump";
            SnakeJump.Size = new Size(241, 41);
            SnakeJump.TabIndex = 555;
            SnakeJump.Text = "Make Snake Jump";
            SnakeJump.UseVisualStyleBackColor = true;
            SnakeJump.Click += SnakeJump_Click;
            // 
            // SnakesXYZaob
            // 
            SnakesXYZaob.BackgroundImage = (Image)resources.GetObject("SnakesXYZaob.BackgroundImage");
            SnakesXYZaob.Cursor = Cursors.Hand;
            SnakesXYZaob.FlatStyle = FlatStyle.Flat;
            SnakesXYZaob.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SnakesXYZaob.ImageAlign = ContentAlignment.TopCenter;
            SnakesXYZaob.Location = new Point(28, 140);
            SnakesXYZaob.Name = "SnakesXYZaob";
            SnakesXYZaob.Size = new Size(241, 80);
            SnakesXYZaob.TabIndex = 556;
            SnakesXYZaob.Text = "Report Snake's Position (Run this if other buttons not working)";
            SnakesXYZaob.UseVisualStyleBackColor = true;
            SnakesXYZaob.Click += SnakesXYZaob_Click;
            // 
            // NopCamo
            // 
            NopCamo.BackgroundImage = (Image)resources.GetObject("NopCamo.BackgroundImage");
            NopCamo.Cursor = Cursors.Hand;
            NopCamo.FlatStyle = FlatStyle.Flat;
            NopCamo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NopCamo.ImageAlign = ContentAlignment.TopCenter;
            NopCamo.Location = new Point(304, 140);
            NopCamo.Name = "NopCamo";
            NopCamo.Size = new Size(289, 110);
            NopCamo.TabIndex = 557;
            NopCamo.Text = "Enable Camo Index changes the game will stop updating your camo index with this and Camo will be set to 0% when an area changes";
            NopCamo.UseVisualStyleBackColor = true;
            NopCamo.Click += NopCamo_Click;
            // 
            // RestoreCamo
            // 
            RestoreCamo.BackgroundImage = (Image)resources.GetObject("RestoreCamo.BackgroundImage");
            RestoreCamo.Cursor = Cursors.Hand;
            RestoreCamo.FlatStyle = FlatStyle.Flat;
            RestoreCamo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            RestoreCamo.ImageAlign = ContentAlignment.TopCenter;
            RestoreCamo.Location = new Point(304, 299);
            RestoreCamo.Name = "RestoreCamo";
            RestoreCamo.Size = new Size(289, 38);
            RestoreCamo.TabIndex = 558;
            RestoreCamo.Text = "Restore Camo Index to normal";
            RestoreCamo.UseVisualStyleBackColor = true;
            RestoreCamo.Click += RestoreCamo_Click;
            // 
            // LogAOBs
            // 
            LogAOBs.BackgroundImage = (Image)resources.GetObject("LogAOBs.BackgroundImage");
            LogAOBs.Cursor = Cursors.Hand;
            LogAOBs.FlatStyle = FlatStyle.Flat;
            LogAOBs.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LogAOBs.ImageAlign = ContentAlignment.TopCenter;
            LogAOBs.Location = new Point(904, 65);
            LogAOBs.Name = "LogAOBs";
            LogAOBs.Size = new Size(241, 29);
            LogAOBs.TabIndex = 560;
            LogAOBs.Text = "Log Data for Debug";
            LogAOBs.UseVisualStyleBackColor = true;
            LogAOBs.Click += LogAOBs_Click;
            // 
            // CamoIndexSlider
            // 
            CamoIndexSlider.BackColor = Color.FromArgb(36, 44, 36);
            CamoIndexSlider.Location = new Point(304, 256);
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
            // textBox3
            // 
            textBox3.BackColor = SystemColors.ActiveCaptionText;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBox3.ForeColor = SystemColors.ActiveCaptionText;
            textBox3.Location = new Point(522, 64);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Click here to generate information for a log ->";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(380, 30);
            textBox3.TabIndex = 565;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // LadderSkip
            // 
            LadderSkip.BackgroundImage = (Image)resources.GetObject("LadderSkip.BackgroundImage");
            LadderSkip.Cursor = Cursors.Hand;
            LadderSkip.FlatStyle = FlatStyle.Flat;
            LadderSkip.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LadderSkip.ImageAlign = ContentAlignment.TopCenter;
            LadderSkip.Location = new Point(28, 343);
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
            textBox6.Location = new Point(283, 478);
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
            TextBoxSnakeX.Location = new Point(286, 613);
            TextBoxSnakeX.Name = "TextBoxSnakeX";
            TextBoxSnakeX.Size = new Size(116, 23);
            TextBoxSnakeX.TabIndex = 574;
            // 
            // TextBoxSnakeY
            // 
            TextBoxSnakeY.Location = new Point(408, 613);
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
            textBox9.Location = new Point(407, 478);
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
            TextBoxSnakeZ.Location = new Point(533, 613);
            TextBoxSnakeZ.Name = "TextBoxSnakeZ";
            TextBoxSnakeZ.Size = new Size(126, 23);
            TextBoxSnakeZ.TabIndex = 578;
            // 
            // textBox12
            // 
            textBox12.BackColor = SystemColors.ActiveCaptionText;
            textBox12.BorderStyle = BorderStyle.None;
            textBox12.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox12.ForeColor = SystemColors.ActiveCaptionText;
            textBox12.Location = new Point(530, 478);
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
            ParseTextBoxesPositions.Location = new Point(286, 637);
            ParseTextBoxesPositions.Name = "ParseTextBoxesPositions";
            ParseTextBoxesPositions.Size = new Size(376, 53);
            ParseTextBoxesPositions.TabIndex = 579;
            ParseTextBoxesPositions.Text = "Use textboxes position to change Snake's location";
            ParseTextBoxesPositions.UseVisualStyleBackColor = true;
            ParseTextBoxesPositions.Click += ParseTextBoxesPositions_Click;
            // 
            // textBox7
            // 
            textBox7.BackColor = SystemColors.ActiveCaptionText;
            textBox7.BorderStyle = BorderStyle.None;
            textBox7.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBox7.ForeColor = SystemColors.ActiveCaptionText;
            textBox7.Location = new Point(283, 577);
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
            ReadTextBoxSnakeZ.Location = new Point(530, 548);
            ReadTextBoxSnakeZ.Name = "ReadTextBoxSnakeZ";
            ReadTextBoxSnakeZ.Size = new Size(129, 23);
            ReadTextBoxSnakeZ.TabIndex = 583;
            // 
            // ReadTextBoxSnakeY
            // 
            ReadTextBoxSnakeY.Location = new Point(405, 548);
            ReadTextBoxSnakeY.Name = "ReadTextBoxSnakeY";
            ReadTextBoxSnakeY.Size = new Size(119, 23);
            ReadTextBoxSnakeY.TabIndex = 582;
            // 
            // ReadTextBoxSnakeX
            // 
            ReadTextBoxSnakeX.Location = new Point(283, 548);
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
            textBox14.Location = new Point(283, 514);
            textBox14.Multiline = true;
            textBox14.Name = "textBox14";
            textBox14.PlaceholderText = "These textboxes only read Snake's location";
            textBox14.ReadOnly = true;
            textBox14.Size = new Size(376, 30);
            textBox14.TabIndex = 584;
            textBox14.TextAlign = HorizontalAlignment.Center;
            // 
            // MiscForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1166, 707);
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
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(CamoIndexSlider);
            Controls.Add(LogAOBs);
            Controls.Add(RestoreCamo);
            Controls.Add(NopCamo);
            Controls.Add(SnakesXYZaob);
            Controls.Add(SnakeJump);
            Controls.Add(textBox1);
            Controls.Add(TeleportGuardsToSnake);
            Controls.Add(SwapToBossForm);
            Controls.Add(HealthFormSwap);
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
            Controls.Add(button1);
            Controls.Add(textBox29);
            Controls.Add(button2);
            Controls.Add(WeaponFormSwap);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MiscForm";
            Text = "MGS3 Cheat Trainer - Stats and Misc - ANTIBigBoss";
            Load += Form4_Load;
            ((System.ComponentModel.ISupportInitialize)ModelSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)CamoIndexSlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private TextBox textBox29;
        private Button button2;
        private Button WeaponFormSwap;
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
        private Button HealthFormSwap;
        private Button SwapToBossForm;
        private Button TeleportGuardsToSnake;
        private TextBox textBox1;
        private Button SnakeJump;
        private Button SnakesXYZaob;
        private Button NopCamo;
        private Button RestoreCamo;
        private Button LogAOBs;
        private TrackBar CamoIndexSlider;
        private TextBox textBox2;
        private TextBox textBox3;
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
    }
}