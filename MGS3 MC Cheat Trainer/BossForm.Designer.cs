namespace MGS3_MC_Cheat_Trainer
{
    partial class BossForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BossForm));
            WeaponFormSwap = new Button();
            HealthFormSwap = new Button();
            MiscFormSwap = new Button();
            CamoFormSwap = new Button();
            ItemFormSwap = new Button();
            FearHealthSlider = new TrackBar();
            Fear0HP = new Button();
            FearStaminaSlider = new TrackBar();
            Fear0Stam = new Button();
            FearTimer = new System.Windows.Forms.Timer(components);
            Pain0Stam = new Button();
            PainStaminaSlider = new TrackBar();
            Pain0HP = new Button();
            PainHealthSlider = new TrackBar();
            PainTimer = new System.Windows.Forms.Timer(components);
            Volgin0Stam = new Button();
            VolginStaminaSlider = new TrackBar();
            Volgin0HP = new Button();
            VolginHealthSlider = new TrackBar();
            VolginTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)FearHealthSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FearStaminaSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PainStaminaSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PainHealthSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)VolginStaminaSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)VolginHealthSlider).BeginInit();
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
            WeaponFormSwap.Location = new Point(36, 7);
            WeaponFormSwap.Name = "WeaponFormSwap";
            WeaponFormSwap.Size = new Size(203, 35);
            WeaponFormSwap.TabIndex = 558;
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
            HealthFormSwap.Location = new Point(925, 7);
            HealthFormSwap.Name = "HealthFormSwap";
            HealthFormSwap.Size = new Size(203, 36);
            HealthFormSwap.TabIndex = 557;
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
            MiscFormSwap.Location = new Point(707, 7);
            MiscFormSwap.Name = "MiscFormSwap";
            MiscFormSwap.Size = new Size(203, 36);
            MiscFormSwap.TabIndex = 556;
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
            CamoFormSwap.Location = new Point(485, 9);
            CamoFormSwap.Name = "CamoFormSwap";
            CamoFormSwap.Size = new Size(203, 33);
            CamoFormSwap.TabIndex = 555;
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
            ItemFormSwap.Location = new Point(262, 9);
            ItemFormSwap.Name = "ItemFormSwap";
            ItemFormSwap.Size = new Size(203, 33);
            ItemFormSwap.TabIndex = 554;
            ItemFormSwap.Text = "Items";
            ItemFormSwap.UseVisualStyleBackColor = true;
            ItemFormSwap.Click += ItemFormSwap_Click;
            // 
            // FearHealthSlider
            // 
            FearHealthSlider.BackColor = Color.FromArgb(36, 44, 36);
            FearHealthSlider.Location = new Point(192, 576);
            FearHealthSlider.Name = "FearHealthSlider";
            FearHealthSlider.Size = new Size(169, 45);
            FearHealthSlider.TabIndex = 593;
            FearHealthSlider.Scroll += FearHealthSlider_Scroll;
            // 
            // Fear0HP
            // 
            Fear0HP.BackgroundImage = (Image)resources.GetObject("Fear0HP.BackgroundImage");
            Fear0HP.Cursor = Cursors.Hand;
            Fear0HP.FlatStyle = FlatStyle.Flat;
            Fear0HP.Font = new Font("Roboto", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Fear0HP.ImageAlign = ContentAlignment.TopCenter;
            Fear0HP.ImeMode = ImeMode.NoControl;
            Fear0HP.Location = new Point(192, 598);
            Fear0HP.Name = "Fear0HP";
            Fear0HP.Size = new Size(169, 29);
            Fear0HP.TabIndex = 594;
            Fear0HP.Text = "The Fear 0 HP";
            Fear0HP.UseVisualStyleBackColor = true;
            Fear0HP.Click += Fear0HP_Click;
            // 
            // FearStaminaSlider
            // 
            FearStaminaSlider.BackColor = Color.FromArgb(36, 44, 36);
            FearStaminaSlider.Location = new Point(192, 639);
            FearStaminaSlider.Name = "FearStaminaSlider";
            FearStaminaSlider.Size = new Size(169, 45);
            FearStaminaSlider.TabIndex = 596;
            FearStaminaSlider.Scroll += FearStaminaSlider_Scroll;
            // 
            // Fear0Stam
            // 
            Fear0Stam.BackgroundImage = (Image)resources.GetObject("Fear0Stam.BackgroundImage");
            Fear0Stam.Cursor = Cursors.Hand;
            Fear0Stam.FlatStyle = FlatStyle.Flat;
            Fear0Stam.Font = new Font("Roboto", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Fear0Stam.ImageAlign = ContentAlignment.TopCenter;
            Fear0Stam.ImeMode = ImeMode.NoControl;
            Fear0Stam.Location = new Point(192, 661);
            Fear0Stam.Name = "Fear0Stam";
            Fear0Stam.Size = new Size(169, 29);
            Fear0Stam.TabIndex = 597;
            Fear0Stam.Text = "The Fear 0 Stamina";
            Fear0Stam.UseVisualStyleBackColor = true;
            Fear0Stam.Click += Fear0Stam_Click;
            // 
            // FearTimer
            // 
            FearTimer.Tick += FearTimer_Tick;
            // 
            // Pain0Stam
            // 
            Pain0Stam.BackgroundImage = (Image)resources.GetObject("Pain0Stam.BackgroundImage");
            Pain0Stam.Cursor = Cursors.Hand;
            Pain0Stam.FlatStyle = FlatStyle.Flat;
            Pain0Stam.Font = new Font("Roboto", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Pain0Stam.ImageAlign = ContentAlignment.TopCenter;
            Pain0Stam.ImeMode = ImeMode.NoControl;
            Pain0Stam.Location = new Point(192, 458);
            Pain0Stam.Name = "Pain0Stam";
            Pain0Stam.Size = new Size(169, 29);
            Pain0Stam.TabIndex = 601;
            Pain0Stam.Text = "The Pain 0 Stamina";
            Pain0Stam.UseVisualStyleBackColor = true;
            Pain0Stam.Click += Pain0Stam_Click;
            // 
            // PainStaminaSlider
            // 
            PainStaminaSlider.BackColor = Color.FromArgb(36, 44, 36);
            PainStaminaSlider.Location = new Point(192, 436);
            PainStaminaSlider.Name = "PainStaminaSlider";
            PainStaminaSlider.Size = new Size(169, 45);
            PainStaminaSlider.TabIndex = 600;
            PainStaminaSlider.Scroll += PainStaminaSlider_Scroll;
            // 
            // Pain0HP
            // 
            Pain0HP.BackgroundImage = (Image)resources.GetObject("Pain0HP.BackgroundImage");
            Pain0HP.Cursor = Cursors.Hand;
            Pain0HP.FlatStyle = FlatStyle.Flat;
            Pain0HP.Font = new Font("Roboto", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Pain0HP.ImageAlign = ContentAlignment.TopCenter;
            Pain0HP.ImeMode = ImeMode.NoControl;
            Pain0HP.Location = new Point(192, 395);
            Pain0HP.Name = "Pain0HP";
            Pain0HP.Size = new Size(169, 29);
            Pain0HP.TabIndex = 599;
            Pain0HP.Text = "The Pain 0 HP";
            Pain0HP.UseVisualStyleBackColor = true;
            Pain0HP.Click += Pain0HP_Click;
            // 
            // PainHealthSlider
            // 
            PainHealthSlider.BackColor = Color.FromArgb(36, 44, 36);
            PainHealthSlider.Location = new Point(192, 373);
            PainHealthSlider.Name = "PainHealthSlider";
            PainHealthSlider.Size = new Size(169, 45);
            PainHealthSlider.TabIndex = 598;
            PainHealthSlider.Scroll += PainHealthSlider_Scroll;
            // 
            // PainTimer
            // 
            PainTimer.Tick += PainTimer_Tick;
            // 
            // Volgin0Stam
            // 
            Volgin0Stam.BackgroundImage = (Image)resources.GetObject("Volgin0Stam.BackgroundImage");
            Volgin0Stam.Cursor = Cursors.Hand;
            Volgin0Stam.FlatStyle = FlatStyle.Flat;
            Volgin0Stam.Font = new Font("Roboto", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Volgin0Stam.ImageAlign = ContentAlignment.TopCenter;
            Volgin0Stam.ImeMode = ImeMode.NoControl;
            Volgin0Stam.Location = new Point(580, 661);
            Volgin0Stam.Name = "Volgin0Stam";
            Volgin0Stam.Size = new Size(169, 29);
            Volgin0Stam.TabIndex = 605;
            Volgin0Stam.Text = "The Pain 0 Stamina";
            Volgin0Stam.UseVisualStyleBackColor = true;
            Volgin0Stam.Click += Volgin0Stam_Click;
            // 
            // VolginStaminaSlider
            // 
            VolginStaminaSlider.BackColor = Color.FromArgb(36, 44, 36);
            VolginStaminaSlider.Location = new Point(580, 639);
            VolginStaminaSlider.Name = "VolginStaminaSlider";
            VolginStaminaSlider.Size = new Size(169, 45);
            VolginStaminaSlider.TabIndex = 604;
            VolginStaminaSlider.Scroll += VolginStaminaSlider_Scroll;
            // 
            // Volgin0HP
            // 
            Volgin0HP.BackgroundImage = (Image)resources.GetObject("Volgin0HP.BackgroundImage");
            Volgin0HP.Cursor = Cursors.Hand;
            Volgin0HP.FlatStyle = FlatStyle.Flat;
            Volgin0HP.Font = new Font("Roboto", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Volgin0HP.ImageAlign = ContentAlignment.TopCenter;
            Volgin0HP.ImeMode = ImeMode.NoControl;
            Volgin0HP.Location = new Point(580, 598);
            Volgin0HP.Name = "Volgin0HP";
            Volgin0HP.Size = new Size(169, 29);
            Volgin0HP.TabIndex = 603;
            Volgin0HP.Text = "Volgin 0 HP";
            Volgin0HP.UseVisualStyleBackColor = true;
            Volgin0HP.Click += Volgin0HP_Click;
            // 
            // VolginHealthSlider
            // 
            VolginHealthSlider.BackColor = Color.FromArgb(36, 44, 36);
            VolginHealthSlider.Location = new Point(580, 576);
            VolginHealthSlider.Name = "VolginHealthSlider";
            VolginHealthSlider.Size = new Size(169, 45);
            VolginHealthSlider.TabIndex = 602;
            VolginHealthSlider.Scroll += VolginHealthSlider_Scroll;
            // 
            // VolginTimer
            // 
            VolginTimer.Tick += VolginTimer_Tick;
            // 
            // BossForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1185, 702);
            Controls.Add(Volgin0Stam);
            Controls.Add(VolginStaminaSlider);
            Controls.Add(Volgin0HP);
            Controls.Add(VolginHealthSlider);
            Controls.Add(Pain0Stam);
            Controls.Add(PainStaminaSlider);
            Controls.Add(Pain0HP);
            Controls.Add(PainHealthSlider);
            Controls.Add(Fear0Stam);
            Controls.Add(FearStaminaSlider);
            Controls.Add(Fear0HP);
            Controls.Add(FearHealthSlider);
            Controls.Add(WeaponFormSwap);
            Controls.Add(HealthFormSwap);
            Controls.Add(MiscFormSwap);
            Controls.Add(CamoFormSwap);
            Controls.Add(ItemFormSwap);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BossForm";
            Text = "MGS3 Cheat Trainer - Bosses - ANTIBigBoss";
            Load += BossForm_Load;
            ((System.ComponentModel.ISupportInitialize)FearHealthSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)FearStaminaSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)PainStaminaSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)PainHealthSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)VolginStaminaSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)VolginHealthSlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button WeaponFormSwap;
        private Button HealthFormSwap;
        private Button MiscFormSwap;
        private Button CamoFormSwap;
        private Button ItemFormSwap;
        private TrackBar FearHealthSlider;
        private Button Fear0HP;
        private TrackBar FearStaminaSlider;
        private Button Fear0Stam;
        private System.Windows.Forms.Timer FearTimer;
        private Button Pain0Stam;
        private TrackBar PainStaminaSlider;
        private Button Pain0HP;
        private TrackBar PainHealthSlider;
        private System.Windows.Forms.Timer PainTimer;
        private Button Volgin0Stam;
        private TrackBar VolginStaminaSlider;
        private Button Volgin0HP;
        private TrackBar VolginHealthSlider;
        private System.Windows.Forms.Timer VolginTimer;
    }
}