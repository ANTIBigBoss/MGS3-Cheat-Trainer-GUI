﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            {
                InitializeComponent();
                this.FormClosing += new FormClosingEventHandler(MainMenuForm_FormClosing);

            }
        }

        private void WeaponFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Weapon form from the Main Menu form.\n");
            WeaponForm form1 = new();
            form1.Show();
            this.Hide();
        }

        private void ItemFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Item form from the Main Menu form.\n");
            ItemForm form2 = new();
            form2.Show();
            this.Hide();
        }

        private void CamoFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Camo form from the Main Menu form.\n");
            CamoForm form3 = new();
            form3.Show();
            this.Hide();
        }

        private void MiscFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Misc form from the Main Menu form.\n");
            MiscForm form4 = new();
            form4.Show();
            this.Hide();
        }

        private void HealthFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Stats and Alert form from the Main Menu form.\n");
            StatsAndAlertForm form5 = new();
            form5.Show();
            this.Hide();
        }

        private void BossFormSwap_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Boss form from the Main Menu form.\n");
            BossForm form6 = new();
            form6.Show();
            this.Hide();
        }
        private void MainMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoggingManager.Instance.Log("User exiting the trainer.\nEnd of log for this session.\n\n\n\n");
            Application.Exit(); // If we close this form we close the whole application so the user isn't running this without knowing
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("Application started successfully.\n");
        }

    }
}
