namespace MGS3_MC_Cheat_Trainer
{
    partial class DebugForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugForm));
            CheatEngineDebugButton = new Button();
            GenerateLogButton = new Button();
            txtProcessName = new TextBox();
            txtCurrentAddress = new TextBox();
            txtRecomputedAddress = new TextBox();
            txtOffset = new TextBox();
            txtCheatEngineString = new TextBox();
            LogAllGuardPositions = new Button();
            LogAreaAddress = new Button();
            parseGeomFile = new Button();
            editGeomFile = new Button();
            SwapToMiscForm = new Button();
            SwapToBossForm = new Button();
            SwapToHealthAndAlertsForm = new Button();
            SwapToCamoForm = new Button();
            SwapToItemsForm = new Button();
            SwapToWeaponsForm = new Button();
            SwapToGameStatsForm = new Button();
            textBox29 = new TextBox();
            SuspendLayout();
            // 
            // CheatEngineDebugButton
            // 
            CheatEngineDebugButton.BackgroundImage = (Image)resources.GetObject("CheatEngineDebugButton.BackgroundImage");
            CheatEngineDebugButton.Cursor = Cursors.Hand;
            CheatEngineDebugButton.FlatStyle = FlatStyle.Flat;
            CheatEngineDebugButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CheatEngineDebugButton.ImageAlign = ContentAlignment.TopCenter;
            CheatEngineDebugButton.ImeMode = ImeMode.NoControl;
            CheatEngineDebugButton.Location = new Point(19, 85);
            CheatEngineDebugButton.Name = "CheatEngineDebugButton";
            CheatEngineDebugButton.Size = new Size(222, 32);
            CheatEngineDebugButton.TabIndex = 565;
            CheatEngineDebugButton.Text = "Cheat Engine Debug";
            CheatEngineDebugButton.UseVisualStyleBackColor = true;
            CheatEngineDebugButton.Click += CheatEngineDebugButton_Click;
            // 
            // GenerateLogButton
            // 
            GenerateLogButton.BackgroundImage = (Image)resources.GetObject("GenerateLogButton.BackgroundImage");
            GenerateLogButton.Cursor = Cursors.Hand;
            GenerateLogButton.FlatStyle = FlatStyle.Flat;
            GenerateLogButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            GenerateLogButton.ImageAlign = ContentAlignment.TopCenter;
            GenerateLogButton.ImeMode = ImeMode.NoControl;
            GenerateLogButton.Location = new Point(257, 85);
            GenerateLogButton.Name = "GenerateLogButton";
            GenerateLogButton.Size = new Size(222, 32);
            GenerateLogButton.TabIndex = 566;
            GenerateLogButton.Text = "Generate a Log .txt File";
            GenerateLogButton.UseVisualStyleBackColor = true;
            GenerateLogButton.Click += GenerateLogButton_Click;
            // 
            // txtProcessName
            // 
            txtProcessName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtProcessName.Location = new Point(19, 123);
            txtProcessName.Name = "txtProcessName";
            txtProcessName.Size = new Size(222, 29);
            txtProcessName.TabIndex = 567;
            // 
            // txtCurrentAddress
            // 
            txtCurrentAddress.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtCurrentAddress.Location = new Point(19, 158);
            txtCurrentAddress.Name = "txtCurrentAddress";
            txtCurrentAddress.Size = new Size(222, 29);
            txtCurrentAddress.TabIndex = 568;
            // 
            // txtRecomputedAddress
            // 
            txtRecomputedAddress.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtRecomputedAddress.Location = new Point(19, 228);
            txtRecomputedAddress.Name = "txtRecomputedAddress";
            txtRecomputedAddress.Size = new Size(222, 29);
            txtRecomputedAddress.TabIndex = 570;
            // 
            // txtOffset
            // 
            txtOffset.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtOffset.Location = new Point(19, 193);
            txtOffset.Name = "txtOffset";
            txtOffset.Size = new Size(222, 29);
            txtOffset.TabIndex = 569;
            // 
            // txtCheatEngineString
            // 
            txtCheatEngineString.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtCheatEngineString.Location = new Point(19, 263);
            txtCheatEngineString.Name = "txtCheatEngineString";
            txtCheatEngineString.Size = new Size(222, 29);
            txtCheatEngineString.TabIndex = 571;
            // 
            // LogAllGuardPositions
            // 
            LogAllGuardPositions.BackgroundImage = (Image)resources.GetObject("LogAllGuardPositions.BackgroundImage");
            LogAllGuardPositions.Cursor = Cursors.Hand;
            LogAllGuardPositions.FlatStyle = FlatStyle.Flat;
            LogAllGuardPositions.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            LogAllGuardPositions.ImageAlign = ContentAlignment.TopCenter;
            LogAllGuardPositions.ImeMode = ImeMode.NoControl;
            LogAllGuardPositions.Location = new Point(257, 161);
            LogAllGuardPositions.Name = "LogAllGuardPositions";
            LogAllGuardPositions.Size = new Size(222, 131);
            LogAllGuardPositions.TabIndex = 572;
            LogAllGuardPositions.Text = "Write all Guard's Current Positions to the Log";
            LogAllGuardPositions.UseVisualStyleBackColor = true;
            LogAllGuardPositions.Click += LogAllGuardPositions_Click;
            // 
            // LogAreaAddress
            // 
            LogAreaAddress.BackgroundImage = (Image)resources.GetObject("LogAreaAddress.BackgroundImage");
            LogAreaAddress.Cursor = Cursors.Hand;
            LogAreaAddress.FlatStyle = FlatStyle.Flat;
            LogAreaAddress.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            LogAreaAddress.ImageAlign = ContentAlignment.TopCenter;
            LogAreaAddress.ImeMode = ImeMode.NoControl;
            LogAreaAddress.Location = new Point(257, 123);
            LogAreaAddress.Name = "LogAreaAddress";
            LogAreaAddress.Size = new Size(222, 32);
            LogAreaAddress.TabIndex = 574;
            LogAreaAddress.Text = "Log Address of Area";
            LogAreaAddress.UseVisualStyleBackColor = true;
            LogAreaAddress.Click += LogAreaAddress_Click;
            // 
            // parseGeomFile
            // 
            parseGeomFile.BackgroundImage = (Image)resources.GetObject("parseGeomFile.BackgroundImage");
            parseGeomFile.Cursor = Cursors.Hand;
            parseGeomFile.FlatStyle = FlatStyle.Flat;
            parseGeomFile.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            parseGeomFile.ImageAlign = ContentAlignment.TopCenter;
            parseGeomFile.ImeMode = ImeMode.NoControl;
            parseGeomFile.Location = new Point(503, 85);
            parseGeomFile.Name = "parseGeomFile";
            parseGeomFile.Size = new Size(182, 90);
            parseGeomFile.TabIndex = 575;
            parseGeomFile.Text = "Parse Geom File Header and ROUTES Chunk";
            parseGeomFile.UseVisualStyleBackColor = true;
            parseGeomFile.Click += parseGeomFile_Click;
            // 
            // editGeomFile
            // 
            editGeomFile.BackgroundImage = (Image)resources.GetObject("editGeomFile.BackgroundImage");
            editGeomFile.Cursor = Cursors.Hand;
            editGeomFile.FlatStyle = FlatStyle.Flat;
            editGeomFile.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            editGeomFile.ImageAlign = ContentAlignment.TopCenter;
            editGeomFile.ImeMode = ImeMode.NoControl;
            editGeomFile.Location = new Point(503, 181);
            editGeomFile.Name = "editGeomFile";
            editGeomFile.Size = new Size(182, 111);
            editGeomFile.TabIndex = 576;
            editGeomFile.Text = "Edit guard routes and behaviour in the Geom File";
            editGeomFile.UseVisualStyleBackColor = true;
            editGeomFile.Click += editGeomFile_Click;
            // 
            // SwapToMiscForm
            // 
            SwapToMiscForm.BackgroundImage = (Image)resources.GetObject("SwapToMiscForm.BackgroundImage");
            SwapToMiscForm.Cursor = Cursors.Hand;
            SwapToMiscForm.FlatStyle = FlatStyle.Flat;
            SwapToMiscForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToMiscForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToMiscForm.ImeMode = ImeMode.NoControl;
            SwapToMiscForm.Location = new Point(519, 12);
            SwapToMiscForm.Name = "SwapToMiscForm";
            SwapToMiscForm.Size = new Size(130, 31);
            SwapToMiscForm.TabIndex = 600;
            SwapToMiscForm.Text = "Misc";
            SwapToMiscForm.UseVisualStyleBackColor = true;
            SwapToMiscForm.Click += SwapToMiscForm_Click;
            // 
            // SwapToBossForm
            // 
            SwapToBossForm.BackgroundImage = (Image)resources.GetObject("SwapToBossForm.BackgroundImage");
            SwapToBossForm.Cursor = Cursors.Hand;
            SwapToBossForm.FlatStyle = FlatStyle.Flat;
            SwapToBossForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToBossForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToBossForm.ImeMode = ImeMode.NoControl;
            SwapToBossForm.Location = new Point(791, 12);
            SwapToBossForm.Name = "SwapToBossForm";
            SwapToBossForm.Size = new Size(130, 31);
            SwapToBossForm.TabIndex = 599;
            SwapToBossForm.Text = "Bosses";
            SwapToBossForm.UseVisualStyleBackColor = true;
            SwapToBossForm.Click += SwapToBossForm_Click;
            // 
            // SwapToHealthAndAlertsForm
            // 
            SwapToHealthAndAlertsForm.BackgroundImage = (Image)resources.GetObject("SwapToHealthAndAlertsForm.BackgroundImage");
            SwapToHealthAndAlertsForm.Cursor = Cursors.Hand;
            SwapToHealthAndAlertsForm.FlatStyle = FlatStyle.Flat;
            SwapToHealthAndAlertsForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToHealthAndAlertsForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToHealthAndAlertsForm.ImeMode = ImeMode.NoControl;
            SwapToHealthAndAlertsForm.Location = new Point(655, 12);
            SwapToHealthAndAlertsForm.Name = "SwapToHealthAndAlertsForm";
            SwapToHealthAndAlertsForm.Size = new Size(130, 31);
            SwapToHealthAndAlertsForm.TabIndex = 598;
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
            SwapToCamoForm.Location = new Point(383, 12);
            SwapToCamoForm.Name = "SwapToCamoForm";
            SwapToCamoForm.Size = new Size(130, 31);
            SwapToCamoForm.TabIndex = 597;
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
            SwapToItemsForm.Location = new Point(247, 12);
            SwapToItemsForm.Name = "SwapToItemsForm";
            SwapToItemsForm.Size = new Size(130, 31);
            SwapToItemsForm.TabIndex = 596;
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
            SwapToWeaponsForm.Location = new Point(111, 12);
            SwapToWeaponsForm.Name = "SwapToWeaponsForm";
            SwapToWeaponsForm.Size = new Size(130, 31);
            SwapToWeaponsForm.TabIndex = 595;
            SwapToWeaponsForm.Text = "Weapons";
            SwapToWeaponsForm.UseVisualStyleBackColor = true;
            SwapToWeaponsForm.Click += SwapToWeaponsForm_Click;
            // 
            // SwapToGameStatsForm
            // 
            SwapToGameStatsForm.BackgroundImage = (Image)resources.GetObject("SwapToGameStatsForm.BackgroundImage");
            SwapToGameStatsForm.Cursor = Cursors.Hand;
            SwapToGameStatsForm.FlatStyle = FlatStyle.Flat;
            SwapToGameStatsForm.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SwapToGameStatsForm.ImageAlign = ContentAlignment.TopCenter;
            SwapToGameStatsForm.ImeMode = ImeMode.NoControl;
            SwapToGameStatsForm.Location = new Point(927, 12);
            SwapToGameStatsForm.Name = "SwapToGameStatsForm";
            SwapToGameStatsForm.Size = new Size(130, 31);
            SwapToGameStatsForm.TabIndex = 649;
            SwapToGameStatsForm.Text = "Game Stats";
            SwapToGameStatsForm.UseVisualStyleBackColor = true;
            SwapToGameStatsForm.Click += SwapToGameStatsForm_Click;
            // 
            // textBox29
            // 
            textBox29.BackColor = SystemColors.ActiveCaptionText;
            textBox29.BorderStyle = BorderStyle.None;
            textBox29.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBox29.ForeColor = SystemColors.ActiveCaptionText;
            textBox29.Location = new Point(19, 49);
            textBox29.Multiline = true;
            textBox29.Name = "textBox29";
            textBox29.PlaceholderText = "WARNING: If you're not familiar with how Modding and Cheat Engine work I wouldn't use these features";
            textBox29.ReadOnly = true;
            textBox29.Size = new Size(1111, 30);
            textBox29.TabIndex = 650;
            textBox29.TextAlign = HorizontalAlignment.Center;
            // 
            // DebugForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1166, 707);
            Controls.Add(textBox29);
            Controls.Add(SwapToGameStatsForm);
            Controls.Add(SwapToMiscForm);
            Controls.Add(SwapToBossForm);
            Controls.Add(SwapToHealthAndAlertsForm);
            Controls.Add(SwapToCamoForm);
            Controls.Add(SwapToItemsForm);
            Controls.Add(SwapToWeaponsForm);
            Controls.Add(editGeomFile);
            Controls.Add(parseGeomFile);
            Controls.Add(LogAreaAddress);
            Controls.Add(LogAllGuardPositions);
            Controls.Add(txtCheatEngineString);
            Controls.Add(txtRecomputedAddress);
            Controls.Add(txtOffset);
            Controls.Add(txtCurrentAddress);
            Controls.Add(txtProcessName);
            Controls.Add(GenerateLogButton);
            Controls.Add(CheatEngineDebugButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DebugForm";
            Text = "MGS3 Cheat Trainer - Debug - ANTIBigBoss - Version 2.8";
            Load += DebugForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button CheatEngineDebugButton;
        private Button GenerateLogButton;
        private TextBox txtProcessName;
        private TextBox txtCurrentAddress;
        private TextBox txtRecomputedAddress;
        private TextBox txtOffset;
        private TextBox txtCheatEngineString;
        private Button LogAllGuardPositions;
        private Button LogAreaAddress;
        private Button parseGeomFile;
        private Button editGeomFile;
        private Button SwapToMiscForm;
        private Button SwapToBossForm;
        private Button SwapToHealthAndAlertsForm;
        private Button SwapToCamoForm;
        private Button SwapToItemsForm;
        private Button SwapToWeaponsForm;
        private Button SwapToGameStatsForm;
        private TextBox textBox29;
    }
}