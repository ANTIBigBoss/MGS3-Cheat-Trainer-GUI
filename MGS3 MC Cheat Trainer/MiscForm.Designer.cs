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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiscForm));
            button1 = new Button();
            textBox29 = new TextBox();
            button2 = new Button();
            WeaponFormSwap = new Button();
            textBox4 = new TextBox();
            NormalHUD = new Button();
            ShrinkHUD = new Button();
            NoHUD = new Button();
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
            Read4ByteBeforeCamoAOB = new Button();
            LogAOBs = new Button();
            Plus50Camo = new Button();
            Minus50Camo = new Button();
            CamoIndexSlider = new TrackBar();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            btnReadCamoIndex = new Button();
            LadderSkip = new Button();
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
            textBox4.Location = new Point(904, 117);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "HUD/Camera";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(241, 34);
            textBox4.TabIndex = 486;
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // NormalHUD
            // 
            NormalHUD.BackgroundImage = (Image)resources.GetObject("NormalHUD.BackgroundImage");
            NormalHUD.Cursor = Cursors.Hand;
            NormalHUD.FlatStyle = FlatStyle.Flat;
            NormalHUD.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NormalHUD.ImageAlign = ContentAlignment.TopCenter;
            NormalHUD.Location = new Point(904, 157);
            NormalHUD.Name = "NormalHUD";
            NormalHUD.Size = new Size(241, 29);
            NormalHUD.TabIndex = 485;
            NormalHUD.Text = "Normal HUD";
            NormalHUD.UseVisualStyleBackColor = true;
            NormalHUD.Click += NormalHUD_Click;
            // 
            // ShrinkHUD
            // 
            ShrinkHUD.BackgroundImage = (Image)resources.GetObject("ShrinkHUD.BackgroundImage");
            ShrinkHUD.Cursor = Cursors.Hand;
            ShrinkHUD.FlatStyle = FlatStyle.Flat;
            ShrinkHUD.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            ShrinkHUD.ImageAlign = ContentAlignment.TopCenter;
            ShrinkHUD.Location = new Point(904, 192);
            ShrinkHUD.Name = "ShrinkHUD";
            ShrinkHUD.Size = new Size(241, 29);
            ShrinkHUD.TabIndex = 487;
            ShrinkHUD.Text = "Shrink HUD";
            ShrinkHUD.UseVisualStyleBackColor = true;
            ShrinkHUD.Click += ShrinkHUD_Click;
            // 
            // NoHUD
            // 
            NoHUD.BackgroundImage = (Image)resources.GetObject("NoHUD.BackgroundImage");
            NoHUD.Cursor = Cursors.Hand;
            NoHUD.FlatStyle = FlatStyle.Flat;
            NoHUD.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NoHUD.ImageAlign = ContentAlignment.TopCenter;
            NoHUD.Location = new Point(904, 226);
            NoHUD.Name = "NoHUD";
            NoHUD.Size = new Size(241, 29);
            NoHUD.TabIndex = 488;
            NoHUD.Text = "No HUD";
            NoHUD.UseVisualStyleBackColor = true;
            NoHUD.Click += NoHUD_Click;
            // 
            // NormalCam
            // 
            NormalCam.BackgroundImage = (Image)resources.GetObject("NormalCam.BackgroundImage");
            NormalCam.Cursor = Cursors.Hand;
            NormalCam.FlatStyle = FlatStyle.Flat;
            NormalCam.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            NormalCam.ImageAlign = ContentAlignment.TopCenter;
            NormalCam.Location = new Point(904, 261);
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
            UpsideDownCam.Location = new Point(904, 296);
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
            ModelSlider.Location = new Point(926, 578);
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
            textBox5.Location = new Point(885, 476);
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
            ResetModelsToNormal.Location = new Point(885, 546);
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
            ModelChangeValue.Location = new Point(886, 519);
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
            ChangeModelNumber.Location = new Point(926, 515);
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
            Minus1ModelValue.Location = new Point(885, 583);
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
            Plus1ModelValue.Location = new Point(1109, 583);
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
            ModelCurrentValue.Location = new Point(1108, 632);
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
            textBox8.Location = new Point(885, 627);
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
            TeleportGuardsToSnake.Location = new Point(28, 329);
            TeleportGuardsToSnake.Name = "TeleportGuardsToSnake";
            TeleportGuardsToSnake.Size = new Size(241, 80);
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
            textBox1.Location = new Point(28, 117);
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
            SnakeJump.Location = new Point(28, 243);
            SnakeJump.Name = "SnakeJump";
            SnakeJump.Size = new Size(241, 80);
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
            SnakesXYZaob.Location = new Point(28, 157);
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
            NopCamo.Location = new Point(566, 473);
            NopCamo.Name = "NopCamo";
            NopCamo.Size = new Size(289, 110);
            NopCamo.TabIndex = 557;
            NopCamo.Text = "Enable Camo Index changes the game will stop updating your camo index with this (Enables slider and buttons) Camo will be set to 0% when an area changes";
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
            RestoreCamo.Location = new Point(566, 632);
            RestoreCamo.Name = "RestoreCamo";
            RestoreCamo.Size = new Size(289, 61);
            RestoreCamo.TabIndex = 558;
            RestoreCamo.Text = "Restore Camo Index to normal (Locks the slider and buttons)";
            RestoreCamo.UseVisualStyleBackColor = true;
            RestoreCamo.Click += RestoreCamo_Click;
            // 
            // Read4ByteBeforeCamoAOB
            // 
            Read4ByteBeforeCamoAOB.BackgroundImage = (Image)resources.GetObject("Read4ByteBeforeCamoAOB.BackgroundImage");
            Read4ByteBeforeCamoAOB.Cursor = Cursors.Hand;
            Read4ByteBeforeCamoAOB.FlatStyle = FlatStyle.Flat;
            Read4ByteBeforeCamoAOB.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            Read4ByteBeforeCamoAOB.ImageAlign = ContentAlignment.TopCenter;
            Read4ByteBeforeCamoAOB.Location = new Point(566, 347);
            Read4ByteBeforeCamoAOB.Name = "Read4ByteBeforeCamoAOB";
            Read4ByteBeforeCamoAOB.Size = new Size(289, 80);
            Read4ByteBeforeCamoAOB.TabIndex = 559;
            Read4ByteBeforeCamoAOB.Text = "Read 4 Bytes Before Camo Index AOB";
            Read4ByteBeforeCamoAOB.UseVisualStyleBackColor = true;
            Read4ByteBeforeCamoAOB.Click += Read4ByteBeforeCamoAOB_Click;
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
            // Plus50Camo
            // 
            Plus50Camo.BackgroundImage = (Image)resources.GetObject("Plus50Camo.BackgroundImage");
            Plus50Camo.Cursor = Cursors.Hand;
            Plus50Camo.FlatStyle = FlatStyle.Flat;
            Plus50Camo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Plus50Camo.ImageAlign = ContentAlignment.TopCenter;
            Plus50Camo.Location = new Point(806, 594);
            Plus50Camo.Name = "Plus50Camo";
            Plus50Camo.Size = new Size(49, 32);
            Plus50Camo.TabIndex = 563;
            Plus50Camo.Text = "+ 1";
            Plus50Camo.UseVisualStyleBackColor = true;
            Plus50Camo.Click += Plus50Camo_Click;
            // 
            // Minus50Camo
            // 
            Minus50Camo.BackgroundImage = (Image)resources.GetObject("Minus50Camo.BackgroundImage");
            Minus50Camo.Cursor = Cursors.Hand;
            Minus50Camo.FlatStyle = FlatStyle.Flat;
            Minus50Camo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Minus50Camo.ImageAlign = ContentAlignment.TopCenter;
            Minus50Camo.Location = new Point(566, 594);
            Minus50Camo.Name = "Minus50Camo";
            Minus50Camo.Size = new Size(49, 32);
            Minus50Camo.TabIndex = 562;
            Minus50Camo.Text = "- 1";
            Minus50Camo.UseVisualStyleBackColor = true;
            Minus50Camo.Click += Minus50Camo_Click;
            // 
            // CamoIndexSlider
            // 
            CamoIndexSlider.BackColor = Color.FromArgb(36, 44, 36);
            CamoIndexSlider.Location = new Point(619, 589);
            CamoIndexSlider.Name = "CamoIndexSlider";
            CamoIndexSlider.Size = new Size(181, 45);
            CamoIndexSlider.TabIndex = 561;
            CamoIndexSlider.Scroll += CamoIndexSlider_Scroll;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Black;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            textBox2.ForeColor = SystemColors.ButtonHighlight;
            textBox2.Location = new Point(566, 433);
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
            // btnReadCamoIndex
            // 
            btnReadCamoIndex.BackgroundImage = (Image)resources.GetObject("btnReadCamoIndex.BackgroundImage");
            btnReadCamoIndex.Cursor = Cursors.Hand;
            btnReadCamoIndex.FlatStyle = FlatStyle.Flat;
            btnReadCamoIndex.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnReadCamoIndex.ImageAlign = ContentAlignment.TopCenter;
            btnReadCamoIndex.Location = new Point(566, 261);
            btnReadCamoIndex.Name = "btnReadCamoIndex";
            btnReadCamoIndex.Size = new Size(289, 80);
            btnReadCamoIndex.TabIndex = 566;
            btnReadCamoIndex.Text = "Read Camo Index Value";
            btnReadCamoIndex.UseVisualStyleBackColor = true;
            btnReadCamoIndex.Click += btnReadCamoIndex_Click;
            // 
            // LadderSkip
            // 
            LadderSkip.BackgroundImage = (Image)resources.GetObject("LadderSkip.BackgroundImage");
            LadderSkip.Cursor = Cursors.Hand;
            LadderSkip.FlatStyle = FlatStyle.Flat;
            LadderSkip.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LadderSkip.ImageAlign = ContentAlignment.TopCenter;
            LadderSkip.Location = new Point(28, 415);
            LadderSkip.Name = "LadderSkip";
            LadderSkip.Size = new Size(241, 179);
            LadderSkip.TabIndex = 567;
            LadderSkip.Text = resources.GetString("LadderSkip.Text");
            LadderSkip.UseVisualStyleBackColor = true;
            LadderSkip.Click += LadderSkip_Click_1;
            // 
            // MiscForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1166, 707);
            Controls.Add(LadderSkip);
            Controls.Add(btnReadCamoIndex);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(Plus50Camo);
            Controls.Add(Minus50Camo);
            Controls.Add(CamoIndexSlider);
            Controls.Add(LogAOBs);
            Controls.Add(Read4ByteBeforeCamoAOB);
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
            Controls.Add(NoHUD);
            Controls.Add(ShrinkHUD);
            Controls.Add(textBox4);
            Controls.Add(NormalHUD);
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
        private Button NormalHUD;
        private Button ShrinkHUD;
        private Button NoHUD;
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
        private Button Read4ByteBeforeCamoAOB;
        private Button LogAOBs;
        private Button Plus50Camo;
        private Button Minus50Camo;
        private TrackBar CamoIndexSlider;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button btnReadCamoIndex;
        private Button LadderSkip;
    }
}