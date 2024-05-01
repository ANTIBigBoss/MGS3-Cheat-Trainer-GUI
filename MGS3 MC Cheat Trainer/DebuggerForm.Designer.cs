namespace MGS3_MC_Cheat_Trainer
{
    partial class DebuggerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebuggerForm));
            WeaponFormSwap = new Button();
            HealthFormSwap = new Button();
            MiscFormSwap = new Button();
            CamoFormSwap = new Button();
            ItemFormSwap = new Button();
            FearTimer = new System.Windows.Forms.Timer(components);
            PainTimer = new System.Windows.Forms.Timer(components);
            VolginTimer = new System.Windows.Forms.Timer(components);
            FuryTimer = new System.Windows.Forms.Timer(components);
            ConsistencyCheckTimer = new System.Windows.Forms.Timer(components);
            OcelotTimer = new System.Windows.Forms.Timer(components);
            EndTimer = new System.Windows.Forms.Timer(components);
            BossTimer = new System.Windows.Forms.Timer(components);
            ShagohodTimer = new System.Windows.Forms.Timer(components);
            VolginOnShagohodTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // WeaponFormSwap
            // 
            WeaponFormSwap.BackgroundImage = (Image)resources.GetObject("WeaponFormSwap.BackgroundImage");
            WeaponFormSwap.Cursor = Cursors.Hand;
            WeaponFormSwap.FlatStyle = FlatStyle.Flat;
            WeaponFormSwap.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            WeaponFormSwap.ImageAlign = ContentAlignment.TopCenter;
            WeaponFormSwap.ImeMode = ImeMode.NoControl;
            WeaponFormSwap.Location = new Point(26, 11);
            WeaponFormSwap.Name = "WeaponFormSwap";
            WeaponFormSwap.Size = new Size(203, 35);
            WeaponFormSwap.TabIndex = 632;
            WeaponFormSwap.Text = "Weapons";
            WeaponFormSwap.UseVisualStyleBackColor = true;
            WeaponFormSwap.Click += WeaponFormSwap_Click;
            // 
            // HealthFormSwap
            // 
            HealthFormSwap.BackgroundImage = (Image)resources.GetObject("HealthFormSwap.BackgroundImage");
            HealthFormSwap.Cursor = Cursors.Hand;
            HealthFormSwap.FlatStyle = FlatStyle.Flat;
            HealthFormSwap.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            HealthFormSwap.ImageAlign = ContentAlignment.TopCenter;
            HealthFormSwap.ImeMode = ImeMode.NoControl;
            HealthFormSwap.Location = new Point(915, 11);
            HealthFormSwap.Name = "HealthFormSwap";
            HealthFormSwap.Size = new Size(203, 36);
            HealthFormSwap.TabIndex = 631;
            HealthFormSwap.Text = "Health/Alerts";
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
            MiscFormSwap.Location = new Point(697, 11);
            MiscFormSwap.Name = "MiscFormSwap";
            MiscFormSwap.Size = new Size(203, 36);
            MiscFormSwap.TabIndex = 630;
            MiscFormSwap.Text = "Misc";
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
            CamoFormSwap.Location = new Point(475, 13);
            CamoFormSwap.Name = "CamoFormSwap";
            CamoFormSwap.Size = new Size(203, 33);
            CamoFormSwap.TabIndex = 629;
            CamoFormSwap.Text = "Camo";
            CamoFormSwap.UseVisualStyleBackColor = true;
            CamoFormSwap.Click += CamoFormSwap_Click;
            // 
            // ItemFormSwap
            // 
            ItemFormSwap.BackgroundImage = (Image)resources.GetObject("ItemFormSwap.BackgroundImage");
            ItemFormSwap.Cursor = Cursors.Hand;
            ItemFormSwap.FlatStyle = FlatStyle.Flat;
            ItemFormSwap.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ItemFormSwap.ImageAlign = ContentAlignment.TopCenter;
            ItemFormSwap.ImeMode = ImeMode.NoControl;
            ItemFormSwap.Location = new Point(252, 13);
            ItemFormSwap.Name = "ItemFormSwap";
            ItemFormSwap.Size = new Size(203, 33);
            ItemFormSwap.TabIndex = 628;
            ItemFormSwap.Text = "Items";
            ItemFormSwap.UseVisualStyleBackColor = true;
            ItemFormSwap.Click += ItemFormSwap_Click;
            // 
            // DebuggerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1191, 717);
            Controls.Add(WeaponFormSwap);
            Controls.Add(HealthFormSwap);
            Controls.Add(MiscFormSwap);
            Controls.Add(CamoFormSwap);
            Controls.Add(ItemFormSwap);
            Name = "DebuggerForm";
            Text = "Form1";
            Load += BossFormRefactor_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button WeaponFormSwap;
        private Button HealthFormSwap;
        private Button MiscFormSwap;
        private Button CamoFormSwap;
        private Button ItemFormSwap;
        private System.Windows.Forms.Timer FearTimer;
        private System.Windows.Forms.Timer PainTimer;
        private System.Windows.Forms.Timer VolginTimer;
        private System.Windows.Forms.Timer FuryTimer;
        private System.Windows.Forms.Timer ConsistencyCheckTimer;
        private System.Windows.Forms.Timer OcelotTimer;
        private System.Windows.Forms.Timer EndTimer;
        private System.Windows.Forms.Timer BossTimer;
        private System.Windows.Forms.Timer ShagohodTimer;
        private System.Windows.Forms.Timer VolginOnShagohodTimer;
    }
}