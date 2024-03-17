using static MGS3_MC_Cheat_Trainer.StringManager;

// Much like BossManager this needs the same kind of rework to be more modular and less repetitive
namespace MGS3_MC_Cheat_Trainer
{
    public partial class BossForm : Form
    {
        #region Form Startup Boss Checks

        public BossForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(BossForm_FormClosing);
        }

        private void BossForm_Load(object sender, EventArgs e)
        {
            this.Location = MemoryManager.GetLastFormLocation();

            ConsistencyCheckTimer.Start();
            OcelotHealthSlider.Maximum = 3400; // Max health
            OcelotHealthSlider.Minimum = 1; // Min health
            OcelotStaminaSlider.Maximum = 3400; // Max stamina
            OcelotStaminaSlider.Minimum = 1; // Min stamina
            OcelotTextbox.Enabled = false;

            // Pain can be set to 0 but Ocelot can't therefore 1 for Ocelot and 0 for Pain for min values
            PainHealthSlider.Maximum = 30000;
            PainHealthSlider.Minimum = 0;
            PainStaminaSlider.Maximum = 21000;
            PainStaminaSlider.Minimum = 0;
            PainTextbox.Enabled = false;

            FearHealthSlider.Maximum = 30000;
            FearHealthSlider.Minimum = 0;
            FearStaminaSlider.Maximum = 21000;
            FearStaminaSlider.Minimum = 0;
            FearTextbox.Enabled = false;

            EndHealthSlider.Maximum = 10000;
            EndHealthSlider.Minimum = 1;
            EndStaminaSlider.Maximum = 10000;
            EndStaminaSlider.Minimum = 1;
            EndTextbox.Enabled = false;

            FuryHealthSlider.Maximum = 8500;
            FuryHealthSlider.Minimum = 1;
            FuryStaminaSlider.Maximum = 7500;
            FuryStaminaSlider.Minimum = 1;
            FuryTextbox.Enabled = false;

            VolginHealthSlider.Maximum = 30000;
            VolginHealthSlider.Minimum = 0;
            VolginStaminaSlider.Maximum = 21000;
            VolginStaminaSlider.Minimum = 0;
            VolginTextbox.Enabled = false;

            //Shagohod has no stamina so only health
            ShagohodHealthSlider.Maximum = 8000;
            ShagohodHealthSlider.Minimum = 0;
            ShagohodTextbox.Enabled = false;

            VolginOnShagohodHealthSlider.Maximum = 8000;
            VolginOnShagohodHealthSlider.Minimum = 0;
            VolginOnShagohodStaminaSlider.Maximum = 8000;
            VolginOnShagohodStaminaSlider.Minimum = 0;
            VolginOnShagohodTextbox.Enabled = false;

            BossHealthSlider.Maximum = 10000;
            BossHealthSlider.Minimum = 1;
            BossStaminaSlider.Maximum = 10000;
            BossStaminaSlider.Minimum = 1;
            TheBossTextbox.Enabled = false;


        }

        #endregion

        #region Consistency Check
        // Hold the switch case in a function instead to make it accessible outside of the form load
        private void SearchForBossAOB()
        {
            string result = StringManager.Instance.FindLocationStringDirectlyInRange();
            string locationString = StringManager.Instance.ExtractLocationStringFromResult(result);

            // Make a switch statement for the result string
            switch (locationString)
            {
                case "s023a":
                    LoggingManager.Instance.Log("Looking for Ocelot AOB");
                    OcelotTextbox.Text = "Looking for Ocelot Please wait...";
                    BossManager.FindOcelotAOB(); // Ocelot
                    OcelotHealthSlider.Enabled = true;
                    OcelotStaminaSlider.Enabled = true;
                    Ocelot0HP.Enabled = true;
                    Ocelot0Stam.Enabled = true;                    
                    OcelotTimer.Interval = 1000; // Update every second
                    OcelotTimer.Tick += new EventHandler(OcelotTimer_Tick);
                    OcelotTimer.Start();

                    LoggingManager.Instance.Log("Enabling Ocelot's control");
                    OcelotTextbox.Text = "Ocelot controls enabled";
                    break;

                case "s023a_0":
                    OcelotTextbox.Text = "Ocelot controls disabled during cutscene";
                    break;

                case "s032b":
                    // Set up a lot of logging for this with how tempermental the boss implementations are I
                    // can locate the issue easier than the user being like "lol trainer doesn't work"
                    LoggingManager.Instance.Log("Looking for The Pain's AOB");
                    PainTextbox.Text = "Looking for The Pain Please wait...";
                    BossManager.FindTheFearAOB(); // Pain and Fear share the same AOB
                    PainHealthSlider.Enabled = true;
                    PainStaminaSlider.Enabled = true;
                    Pain0HP.Enabled = true;
                    Pain0Stam.Enabled = true;
                    PainTimer.Interval = 1000; // Update every second
                    FearTimer.Tick += new EventHandler(PainTimer_Tick);
                    PainTimer.Start();
                    LoggingManager.Instance.Log("Every other boss control disabled except for The Pain");
                    PainTextbox.Text = "The Pain Controls enabled";
                    break;

                case "s032b_0":
                    PainTextbox.Text = "Pain controls disabled during cutscene";
                    break;

                case "s051b":
                    LoggingManager.Instance.Log("Looking for The Fear AOB");
                    FearTextbox.Text = "Looking for The Fear Please wait...";
                    BossManager.FindTheFearAOB(); // Actually The Fear
                    FearHealthSlider.Enabled = true;
                    FearStaminaSlider.Enabled = true;
                    Fear0HP.Enabled = true;
                    Fear0Stam.Enabled = true;
                    FearTimer.Interval = 1000; // Update every second
                    FearTimer.Tick += new EventHandler(FearTimer_Tick);
                    FearTimer.Start();
                    LoggingManager.Instance.Log("Every other boss control disabled except for The Fear");
                    FearTextbox.Text = "The Fear Controls enabled";
                    break;

                case "s051b_0":
                    FearTextbox.Text = "Fear controls disabled during cutscene";
                    break;

                // The End's first area where the boss fight starts in s063a
                case "s063a":
                    LoggingManager.Instance.Log("Looking for The End AOB");
                    BossManager.FindTheEnds063aAOB(); // The End
                    EndHealthSlider.Enabled = true;
                    EndStaminaSlider.Enabled = true;
                    End0HP.Enabled = true;
                    End0Stam.Enabled = true;
                    EndTimer.Interval = 1000; // Update every second
                    EndTimer.Tick += new EventHandler(EndTimer_Tick);
                    EndTimer.Start();                    
                    LoggingManager.Instance.Log("The End's controls are enabled");
                    EndTextbox.Text = "The End's Controls enabled";
                    break;

                case "s063a_0":
                    EndTextbox.Text = "The End controls disabled during cutscene";
                    break;

                // The End's boss arena where the river is
                case "s064a":
                    LoggingManager.Instance.Log("Looking for The End AOB");
                    EndTextbox.Text = "Looking for The End Please wait...";
                    BossManager.FindTheEnds065aAOB(); // The End
                    EndHealthSlider.Enabled = true;
                    EndStaminaSlider.Enabled = true;
                    End0HP.Enabled = true;
                    End0Stam.Enabled = true;
                    EndTimer.Interval = 1000; // Update every second
                    EndTimer.Tick += new EventHandler(EndTimer_Tick);
                    EndTimer.Start();
                    LoggingManager.Instance.Log("The End's controls are enabled");
                    EndTextbox.Text = "The End's Controls enabled";
                    break;

                case "s064a_0":
                    EndTextbox.Text = "The End controls disabled during cutscene";
                    break;

                // This area is the cutscene where The End dies
                case "s065a":
                    LoggingManager.Instance.Log("Looking for The End AOB");
                    EndTextbox.Text = "Looking for The End Please wait...";
                    BossManager.FindTheEnds065aAOB(); // The End
                    EndHealthSlider.Enabled = true;
                    EndStaminaSlider.Enabled = true;
                    End0HP.Enabled = true;
                    End0Stam.Enabled = true;
                    EndTimer.Interval = 1000; // Update every second
                    EndTimer.Tick += new EventHandler(EndTimer_Tick);
                    EndTimer.Start();
                    LoggingManager.Instance.Log("The End's controls are enabled");
                    EndTextbox.Text = "The End's Controls enabled";
                    break;

                case "s065a_0":
                    EndTextbox.Text = "The End controls disabled during cutscene";
                    break;

                // The Fury 
                case "s081a":
                    LoggingManager.Instance.Log("Looking for The Fury AOB");
                    BossManager.FindTheFuryAOB(); // The Fury
                    FuryHealthSlider.Enabled = true;
                    FuryStaminaSlider.Enabled = true;
                    Fury0HP.Enabled = true;
                    Fury0Stam.Enabled = true;
                    FuryTimer.Interval = 1000; // Update every second
                    FuryTimer.Tick += new EventHandler(FuryTimer_Tick);
                    FuryTimer.Start();
                    LoggingManager.Instance.Log("The Fury's controls are enabled");
                    FuryTextbox.Text = "The Fury's Controls enabled";
                    break;

                case "s081a_0":
                    FuryTextbox.Text = "The Fury controls disabled during cutscene";
                    break;


                case "s122a":
                    LoggingManager.Instance.Log("Looking for Volgin AOB");
                    BossManager.FindTheFearAOB(); // Volgin's first fight shares the same AOB as well
                    VolginHealthSlider.Enabled = true;
                    VolginStaminaSlider.Enabled = true;
                    Volgin0HP.Enabled = true;
                    Volgin0Stam.Enabled = true;
                    VolginTimer.Interval = 1000; // Update every second
                    FearTimer.Tick += new EventHandler(VolginTimer_Tick);
                    VolginTimer.Start();
                    LoggingManager.Instance.Log("Volgin's controls enabled");
                    VolginTextbox.Text = "Volgin Controls enabled";
                    break;

                case "s122a_1":
                    VolginTextbox.Text = "Volgin Controls disabled during cutscene";
                    break;

                case "s171b":
                    LoggingManager.Instance.Log("Looking for Shagohod AOB");
                    ShagohodTextbox.Text = "Looking for Shagohod Please wait...";
                    short shagohodResult = BossManager.FindShagohodAOB();
                    if ((shagohodResult != -1) && (BossManager.IsShagohodDead() == false))
                    {
                        ShagohodHealthSlider.Enabled = true;
                        Shagohod0HP.Enabled = true;
                        ShagohodTimer.Interval = 1000; // Update every second
                        ShagohodTimer.Tick += new EventHandler(ShagohodTimer_Tick);
                        ShagohodTimer.Start();
                    }
                    else if (BossManager.IsShagohodDead() == true)
                    {
                        LoggingManager.Instance.Log("Shagohod AOB found but Shagohod is dead. Looking for VolginOnShagohod AOB");
                        VolginOnShagohodTextbox.Text = "Looking for Volgin on Shagohod Please wait...";
                        short volginOnShagohodResult = BossManager.FindVolginOnShagohodAOB();
                        VolginOnShagohodHealthSlider.Enabled = true;
                        VolginOnShagohodStaminaSlider.Enabled = true;
                        VolginOnShagohog0HP.Enabled = true;
                        VolginOnShagohog0Stam.Enabled = true;
                        VolginOnShagohodTimer.Tick += new EventHandler(VolginOnShagohodTimer_Tick);
                        VolginOnShagohodTimer.Start();
                        LoggingManager.Instance.Log("Volgin on Shagohod's controls are enabled");
                        VolginOnShagohodTextbox.Text = "Volgin on Shagohod's Controls enabled";
                    }
                    else
                    {
                        LoggingManager.Instance.Log("Shagohod and Volgin on Shagohod AOB not found.");
                    }
                    break;

                case "s171b_0":
                    if (BossManager.IsShagohodDead() == false)
                    {
                        ShagohodTextbox.Text = "Shagohod controls disabled during cutscene";
                    }
                    else if (BossManager.IsShagohodDead() == true)
                    {
                        VolginOnShagohodTextbox.Text = "Volgin on Shagohod controls disabled during cutscene";
                    }
                    break;

                case "s201a":
                    LoggingManager.Instance.Log("Looking for The Boss AOB");
                    TheBossTextbox.Text = "Looking for The Boss Please wait...";
                    BossManager.FindTheBossAOB(); // The Boss
                    BossHealthSlider.Enabled = true;
                    BossStaminaSlider.Enabled = true;
                    Boss0HP.Enabled = true;
                    Boss0Stam.Enabled = true;
                    BossTimer.Interval = 1000; // Update every second
                    BossTimer.Tick += new EventHandler(BossTimer_Tick);
                    BossTimer.Start();      
                    LoggingManager.Instance.Log("The Boss's controls are enabled");
                    TheBossTextbox.Text = "The Boss's Controls enabled";
                    break;

                case "s201a_0":
                    TheBossTextbox.Text = "The Boss controls disabled during cutscene";
                    break;

                default:                   
                    break;
            }
        }

        private bool IsNotBossLocation(string locationString)
        {
            // Check if the current location string does not match any of the boss fight locations
            switch (locationString)
            {
                case "s023a": // Ocelot
                case "s032b": // The Pain
                case "s051b": // The Fear
                case "s063a": // The End First Area
                case "s064a": // The End River Area
                case "s065a": // The End Death Area
                case "s081a": // The Fury
                case "s122a": // Volgin
                case "s171b": // Shagohod and Volgin on the Shagohod need a way for it to recheck
                case "s201a": // The Boss
                    return false;
                default:
                    return true;
            }
        }

        private bool consistencyCheckNeeded = false;
        private string currentLocation = "";

        private void StartConsistencyCheck()
        {
            if (!consistencyCheckNeeded)
            {
                ConsistencyCheckTimer.Tick += ConsistencyCheckTimer_Tick;
                ConsistencyCheckTimer.Start();
                consistencyCheckNeeded = true;
            }
        }

        private void ConsistencyCheckTimer_Tick(object sender, EventArgs e)
        {
            string newLocation = StringManager.Instance.ExtractLocationStringFromResult(StringManager.Instance.FindLocationStringDirectlyInRange());

            // Check if the area has changed
            if (newLocation != currentLocation)
            {
                DisableAllBossControls();
                LoggingManager.Instance.Log("Disabling all boss control while checking for a new Boss AOB. This is mostly incase the user is fighting The End or a boss cutscene played like it does for Pain and Volgin");
                // Perform the load event logic specific to the new area
                currentLocation = newLocation; // Update currentLocation with the new area
                SearchForBossAOB(); // Re-initialize for the new area

                LoggingManager.Instance.Log($"Area changed to {currentLocation}. Re-initializing for new area.");
            }
            else if (IsBossDefeated() && IsNotBossLocation(currentLocation))
            {
                DisableAllBossControls();
                consistencyCheckNeeded = false;
            }
        }

        private bool IsBossDefeated()
        {
            // Return true if any of the bosses are dead or stunned
            return
            BossManager.IsOcelotDead() || BossManager.IsOcelotStunned() ||
            BossManager.IsThePainDead() || BossManager.IsThePainStunned() ||
            BossManager.IsTheFearDead() || BossManager.IsTheFearStunned() ||
            BossManager.IsTheEnds063aDead() || BossManager.IsTheEnds063aStunned() ||
            BossManager.IsTheEnds065aDead() || BossManager.IsTheEnds065aStunned() || // This would also apply to s064a
            BossManager.IsTheFuryDead() || BossManager.IsTheFuryStunned() ||
            BossManager.IsVolginDead() || BossManager.IsVolginStunned() ||
            BossManager.IsShagohodDead() || // No stamina for Shagohod so only check for dead
            BossManager.IsVolginOnShagohodDead() || BossManager.IsVolginOnShagohodStunned() ||
            BossManager.IsTheBossDead() || BossManager.IsTheBossStunned();
        }

        private void DisableAllBossControls()
        {
            // Ocelot
            OcelotHealthSlider.Enabled = false;
            OcelotStaminaSlider.Enabled = false;
            Ocelot0HP.Enabled = false;
            Ocelot0Stam.Enabled = false;
            OcelotTimer.Stop();
            OcelotTextbox.Text = "Ocelot Controls Disabled";
            

            // The Pain
            PainHealthSlider.Enabled = false;
            PainStaminaSlider.Enabled = false;
            Pain0HP.Enabled = false;
            Pain0Stam.Enabled = false;
            PainTimer.Stop();
            PainTextbox.Text = "The Pain Controls Disabled";

            // The Fear
            FearHealthSlider.Enabled = false;
            FearStaminaSlider.Enabled = false;
            Fear0HP.Enabled = false;
            Fear0Stam.Enabled = false;
            FearTimer.Stop();
            FearTextbox.Text = "The Fear Controls Disabled";

            // The End
            EndHealthSlider.Enabled = false;
            EndStaminaSlider.Enabled = false;
            End0HP.Enabled = false;
            End0Stam.Enabled = false;
            EndTimer.Stop();
            EndTextbox.Text = "The End Controls Disabled";

            // The Fury
            FuryHealthSlider.Enabled = false;
            FuryStaminaSlider.Enabled = false;
            Fury0HP.Enabled = false;
            Fury0Stam.Enabled = false;
            FuryTimer.Stop();
            FuryTextbox.Text = "The Fury Controls Disabled";

            // Volgin
            VolginHealthSlider.Enabled = false;
            VolginStaminaSlider.Enabled = false;
            Volgin0HP.Enabled = false;
            Volgin0Stam.Enabled = false;
            VolginTimer.Stop();
            VolginTextbox.Text = "Volgin Controls Disabled";

            // Shagohod
            ShagohodHealthSlider.Enabled = false;
            Shagohod0HP.Enabled = false;
            ShagohodTimer.Stop();
            ShagohodTextbox.Text = "Shagohod Controls Disabled";

            // Volgin on the Shagohod
            VolginOnShagohodHealthSlider.Enabled = false;
            VolginOnShagohodStaminaSlider.Enabled = false;
            VolginOnShagohog0HP.Enabled = false;
            VolginOnShagohog0Stam.Enabled = false;
            VolginOnShagohodTimer.Stop();
            VolginOnShagohodTextbox.Text = "Volgin/Shagohod Controls Disabled";

            // The Boss
            BossHealthSlider.Enabled = false;
            BossStaminaSlider.Enabled = false;
            Boss0HP.Enabled = false;
            Boss0Stam.Enabled = false;
            BossTimer.Stop();
            TheBossTextbox.Text = "The Boss Controls Disabled";
        }

        #endregion

        #region Form Swapping and Closing
        private void BossForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void WeaponFormSwap_Click(object sender, EventArgs e)
        {
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "WeaponForm");
            WeaponForm form1 = new WeaponForm();
            form1.Show();
            this.Hide();
            LoggingManager.Instance.Log("User is changing to the Weapon form from the Boss form.\n");
        }

        private void ItemFormSwap_Click(object sender, EventArgs e)
        {
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "ItemForm");
            ItemForm form2 = new ItemForm();
            form2.Show();
            this.Hide();
            LoggingManager.Instance.Log("User is changing to the Item form from the Boss form.\n");
        }

        private void CamoFormSwap_Click(object sender, EventArgs e)
        {
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "CamoForm");
            CamoForm form3 = new CamoForm();
            form3.Show();
            this.Hide();
            LoggingManager.Instance.Log("User is changing to the Camo form from the Boss form.\n");
        }

        private void MiscFormSwap_Click(object sender, EventArgs e)
        {
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "MiscForm");
            MiscForm form4 = new MiscForm();
            form4.Show();
            this.Hide();
            LoggingManager.Instance.Log("User is changing to the Misc form from the Boss form.\n");
        }

        private void HealthFormSwap_Click(object sender, EventArgs e)
        {
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "StatsAndAlertForm");
            StatsAndAlertForm form5 = new StatsAndAlertForm();
            form5.Show();
            this.Hide();
            LoggingManager.Instance.Log("User is changing to the Stats and Alert form from the Boss form.\n");
        }
        #endregion
                 
    private void Ocelot0HP_Click(object sender, EventArgs e)
    {

        BossManager.WriteOcelotHealth(0x0001);
        LoggingManager.Instance.Log("Ocelot's health was depleted via Health button click");
    }

    private void OcelotHealthSlider_Scroll(object sender, EventArgs e)
    {        
        short newHealthValue = (short)OcelotHealthSlider.Value;
        BossManager.WriteOcelotHealth(newHealthValue);
        LoggingManager.Instance.Log("Ocelot's health was set to " + newHealthValue + "via the slider");
        if (newHealthValue == 0)
        {
            LoggingManager.Instance.Log("Ocelot's health was depleted via slider.");
        }
    }

    private void Ocelot0Stam_Click(object sender, EventArgs e)
    {

        BossManager.WriteOcelotStamina(0x0001);
        LoggingManager.Instance.Log("Ocelot's stamina was depleted via Stamina button click");
    }

    private void OcelotStaminaSlider_Scroll(object sender, EventArgs e)
    {

        short newStaminaValue = (short)OcelotStaminaSlider.Value;
        BossManager.WriteOcelotStamina(newStaminaValue);
        LoggingManager.Instance.Log("Ocelot's stamina was set to " + newStaminaValue + "via the slider");
        if (newStaminaValue == 0)
        {
            LoggingManager.Instance.Log("Ocelot's stamina was depleted via slider.");
        }
    }

    private void OcelotTimer_Tick(object sender, EventArgs e)
    {
        OcelotHealthSlider.Scroll -= OcelotHealthSlider_Scroll;
        OcelotStaminaSlider.Scroll -= OcelotStaminaSlider_Scroll;

        try
        {
            short currentHealth = BossManager.ReadOcelotHealth();
            short currentStamina = BossManager.ReadOcelotStamina();

            OcelotHealthSlider.Value = Math.Clamp(currentHealth, OcelotHealthSlider.Minimum, OcelotHealthSlider.Maximum);
            OcelotStaminaSlider.Value = Math.Clamp(currentStamina, OcelotStaminaSlider.Minimum, OcelotStaminaSlider.Maximum);
        }
        finally
        {
            OcelotHealthSlider.Scroll += OcelotHealthSlider_Scroll;
            OcelotStaminaSlider.Scroll += OcelotStaminaSlider_Scroll;
        }
    }



    // s032b is The Pain's area
    private void Pain0HP_Click(object sender, EventArgs e)
    {

        BossManager.WriteThePainHealth(0x0001);
        LoggingManager.Instance.Log("The Pain's health was depleted via Health button click");

    }

    private void PainHealthSlider_Scroll(object sender, EventArgs e)
    {
 
        short newHealthValue = (short)PainHealthSlider.Value;
        BossManager.WriteThePainHealth(newHealthValue);
        LoggingManager.Instance.Log("The Pain's health was set to " + newHealthValue + "via the slider");
        if (newHealthValue == 0)
        {
            LoggingManager.Instance.Log("The Pain's health was depleted via slider.");
        }

    }

    private void Pain0Stam_Click(object sender, EventArgs e)
    {
 
        BossManager.WriteThePainStamina(0x0001);
        LoggingManager.Instance.Log("The Pain's stamina was depleted via Stamina button click");

    }

    private void PainStaminaSlider_Scroll(object sender, EventArgs e)
    {

        short newStaminaValue = (short)PainStaminaSlider.Value;
        BossManager.WriteThePainStamina(newStaminaValue);
        LoggingManager.Instance.Log("The Pain's stamina was set to " + newStaminaValue + "via the slider");
        if (newStaminaValue == 0)
        {
            LoggingManager.Instance.Log("The Pain's stamina was depleted via slider.");
        }
    }

    private void PainTimer_Tick(object sender, EventArgs e)
    {
        PainHealthSlider.Scroll -= PainHealthSlider_Scroll;
        PainStaminaSlider.Scroll -= PainStaminaSlider_Scroll;

        try
        {
            short currentHealth = BossManager.ReadThePainHealth();
            short currentStamina = BossManager.ReadThePainStamina();

            PainHealthSlider.Value = Math.Clamp(currentHealth, PainHealthSlider.Minimum, PainHealthSlider.Maximum);
            PainStaminaSlider.Value = Math.Clamp(currentStamina, PainStaminaSlider.Minimum, PainStaminaSlider.Maximum);
        }
        finally
        {
            PainHealthSlider.Scroll += PainHealthSlider_Scroll;
            PainStaminaSlider.Scroll += PainStaminaSlider_Scroll;
        }
    }

    private void Fear0HP_Click(object sender, EventArgs e)
    {

        BossManager.WriteTheFearHealth(0x0000);
        LoggingManager.Instance.Log("The Fear's health was depleted via Health button click");
    }


    private void FearHealthSlider_Scroll(object sender, EventArgs e)
    {

        short newHealthValue = (short)FearHealthSlider.Value;
        BossManager.WriteTheFearHealth(newHealthValue);
        LoggingManager.Instance.Log("The Fear's health was set to " + newHealthValue + "via the slider");
        if (newHealthValue == 0)
        {
            LoggingManager.Instance.Log("The Fear's health was depleted via slider.");
        }
    }

    private void FearStaminaSlider_Scroll(object sender, EventArgs e)
    {

        short newStaminaValue = (short)FearStaminaSlider.Value;
        BossManager.WriteTheFearStamina(newStaminaValue);
        LoggingManager.Instance.Log("The Fear's stamina was set to " + newStaminaValue + "via the slider");
        if (newStaminaValue == 0)
        {
            LoggingManager.Instance.Log("The Fear's stamina was depleted via slider.");
        }

    }

    private void Fear0Stam_Click(object sender, EventArgs e)
    {

        BossManager.WriteTheFearStamina(0x0000);
        LoggingManager.Instance.Log("The Fear's stamina was depleted via Stamina button click");

    }

    private void FearTimer_Tick(object sender, EventArgs e)
    {
        FearHealthSlider.Scroll -= FearHealthSlider_Scroll;
        FearStaminaSlider.Scroll -= FearStaminaSlider_Scroll;

        try
        {
            short currentHealth = BossManager.ReadTheFearHealth();
            short currentStamina = BossManager.ReadTheFearStamina();

            FearHealthSlider.Value = Math.Clamp(currentHealth, FearHealthSlider.Minimum, FearHealthSlider.Maximum);
            FearStaminaSlider.Value = Math.Clamp(currentStamina, FearStaminaSlider.Minimum, FearStaminaSlider.Maximum);
        }
        finally
        {
            FearHealthSlider.Scroll += FearHealthSlider_Scroll;
            FearStaminaSlider.Scroll += FearStaminaSlider_Scroll;
        }

    }

        #region The End

    private void End0HP_Click(object sender, EventArgs e)
        {

            string result = StringManager.Instance.FindLocationStringDirectlyInRange();
            string locationString = StringManager.Instance.ExtractLocationStringFromResult(result);
            if (locationString == "s063a")
            {
                
                BossManager.WriteTheEnds063aHealth(0x0001);
                LoggingManager.Instance.Log("The End's health was depleted via Health button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
            else if (locationString == "s064a")
            {
                BossManager.WriteTheEnds065aHealth(0x0001);
                LoggingManager.Instance.Log("The End's health was depleted via Health button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
            else if (locationString == "s065a")
            {
                BossManager.WriteTheEnds065aHealth(0x0001);
                LoggingManager.Instance.Log("The End's health was depleted via Health button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
        }

        private void End0Stam_Click(object sender, EventArgs e)
        {

            string result = StringManager.Instance.FindLocationStringDirectlyInRange();
            string locationString = StringManager.Instance.ExtractLocationStringFromResult(result);
            if (locationString == "s063a")
            {
                BossManager.WriteTheEnds063aStamina(0x0001);
                LoggingManager.Instance.Log("The End's stamina was depleted via Stamina button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
            else if (locationString == "s064a")
            {
                BossManager.WriteTheEnds065aStamina(0x0001);
                LoggingManager.Instance.Log("The End's stamina was depleted via Stamina button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
            else if (locationString == "s065a")
            {
                BossManager.WriteTheEnds065aStamina(0x0001);
                LoggingManager.Instance.Log("The End's stamina was depleted via Stamina button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
        }

        private void EndHealthSlider_Scroll(object sender, EventArgs e)
        {

            string result = StringManager.Instance.FindLocationStringDirectlyInRange();
            string locationString = StringManager.Instance.ExtractLocationStringFromResult(result);
            if (locationString == "s063a")
            {
                short newHealthValue = (short)EndHealthSlider.Value;
                BossManager.WriteTheEnds063aHealth(newHealthValue);

                if (newHealthValue == 0)
                {
                    LoggingManager.Instance.Log("The End's health was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }

            else if (locationString == "s064a")
            {
                short newHealthValue = (short)EndHealthSlider.Value;
                BossManager.WriteTheEnds065aHealth(newHealthValue);

                if (newHealthValue == 0)
                {
                    LoggingManager.Instance.Log("The End's health was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }

            else if (locationString == "s065a")
            {

                short newHealthValue = (short)EndHealthSlider.Value;
                BossManager.WriteTheEnds065aHealth(newHealthValue);

                if (newHealthValue == 0)
                {
                    LoggingManager.Instance.Log("The End's health was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }
        }

        private void EndStaminaSlider_Scroll(object sender, EventArgs e)
        {

            string result = StringManager.Instance.FindLocationStringDirectlyInRange();
            string locationString = StringManager.Instance.ExtractLocationStringFromResult(result);
            if (locationString == "s063a")
            {
                short newStaminaValue = (short)EndStaminaSlider.Value;
                BossManager.WriteTheEnds063aStamina(newStaminaValue);

                if (newStaminaValue == 0)
                {
                    LoggingManager.Instance.Log("The End's stamina was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }

            else if (locationString == "s064a")
            {
                short newStaminaValue = (short)EndStaminaSlider.Value;
                BossManager.WriteTheEnds065aStamina(newStaminaValue);

                if (newStaminaValue == 0)
                {
                    LoggingManager.Instance.Log("The End's stamina was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }

            else if (locationString == "s065a")
            {
                short newStaminaValue = (short)EndStaminaSlider.Value;
                BossManager.WriteTheEnds065aStamina(newStaminaValue);

                if (newStaminaValue == 0)
                {
                    LoggingManager.Instance.Log("The End's stamina was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }
        }

        private void EndTimer_Tick(object sender, EventArgs e)
        {

            string result = StringManager.Instance.FindLocationStringDirectlyInRange();
            string locationString = StringManager.Instance.ExtractLocationStringFromResult(result);
            if (locationString == "s063a")
            {
                EndHealthSlider.Scroll -= EndHealthSlider_Scroll;
                EndStaminaSlider.Scroll -= EndStaminaSlider_Scroll;

                try
                {
                    short currentHealth = BossManager.ReadTheEnds063aHealth();
                    short currentStamina = BossManager.ReadTheEnds063aStamina();

                    EndHealthSlider.Value = Math.Clamp(currentHealth, EndHealthSlider.Minimum, EndHealthSlider.Maximum);
                    EndStaminaSlider.Value = Math.Clamp(currentStamina, EndStaminaSlider.Minimum, EndStaminaSlider.Maximum);
                }
                finally
                {
                    EndHealthSlider.Scroll += EndHealthSlider_Scroll;
                    EndStaminaSlider.Scroll += EndStaminaSlider_Scroll;
                }
            }

            else if (locationString == "s064a")
            {
                EndHealthSlider.Scroll -= EndHealthSlider_Scroll;
                EndStaminaSlider.Scroll -= EndStaminaSlider_Scroll;

                try
                {
                    short currentHealth = BossManager.ReadTheEnds065aHealth();
                    short currentStamina = BossManager.ReadTheEnds065aStamina();

                    EndHealthSlider.Value = Math.Clamp(currentHealth, EndHealthSlider.Minimum, EndHealthSlider.Maximum);
                    EndStaminaSlider.Value = Math.Clamp(currentStamina, EndStaminaSlider.Minimum, EndStaminaSlider.Maximum);
                }
                finally
                {
                    EndHealthSlider.Scroll += EndHealthSlider_Scroll;
                    EndStaminaSlider.Scroll += EndStaminaSlider_Scroll;
                }
            }

            else if (locationString == "s065a")
            {
                EndHealthSlider.Scroll -= EndHealthSlider_Scroll;
                EndStaminaSlider.Scroll -= EndStaminaSlider_Scroll;

                try
                {
                    short currentHealth = BossManager.ReadTheEnds065aHealth();
                    short currentStamina = BossManager.ReadTheEnds065aStamina();

                    EndHealthSlider.Value = Math.Clamp(currentHealth, EndHealthSlider.Minimum, EndHealthSlider.Maximum);
                    EndStaminaSlider.Value = Math.Clamp(currentStamina, EndStaminaSlider.Minimum, EndStaminaSlider.Maximum);
                }
                finally
                {
                    EndHealthSlider.Scroll += EndHealthSlider_Scroll;
                    EndStaminaSlider.Scroll += EndStaminaSlider_Scroll;
                }
            }
        }

        #endregion

        #region The Fury
        private void Fury0HP_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheFuryHealth(0x0001);
            LoggingManager.Instance.Log("The Fury's health was depleted via Health button click");
            StartConsistencyCheck();
            LoggingManager.Instance.Log("Consistency check started for The Fury.");
        }

        private void Fury0Stam_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheFuryStamina(0x0001);
            LoggingManager.Instance.Log("The Fury's stamina was depleted via Stamina button click");
            StartConsistencyCheck();
            LoggingManager.Instance.Log("Consistency check started for The Fury.");
        }

        private void FuryHealthSlider_Scroll(object sender, EventArgs e)
        {
            short newHealthValue = (short)FuryHealthSlider.Value;
            BossManager.WriteTheFuryHealth(newHealthValue);

            if (newHealthValue == 0)
            {
                LoggingManager.Instance.Log("The Fury's health was depleted via slider.");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The Fury.");
            }
        }

        private void FuryStaminaSlider_Scroll(object sender, EventArgs e)
        {
            short newStaminaValue = (short)FuryStaminaSlider.Value;
            BossManager.WriteTheFuryStamina(newStaminaValue);

            if (newStaminaValue == 0)
            {
                LoggingManager.Instance.Log("The Fury's stamina was depleted via slider.");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The Fury.");
            }
        }

        private void FuryTimer_Tick(object sender, EventArgs e)
        {
            FuryHealthSlider.Scroll -= FuryHealthSlider_Scroll;
            FuryStaminaSlider.Scroll -= FuryStaminaSlider_Scroll;

            try
            {
                short currentHealth = BossManager.ReadTheFuryHealth();
                short currentStamina = BossManager.ReadTheFuryStamina();

                FuryHealthSlider.Value = Math.Clamp(currentHealth, FuryHealthSlider.Minimum, FuryHealthSlider.Maximum);
                FuryStaminaSlider.Value = Math.Clamp(currentStamina, FuryStaminaSlider.Minimum, FuryStaminaSlider.Maximum);
            }
            finally
            {
                FuryHealthSlider.Scroll += FuryHealthSlider_Scroll;
                FuryStaminaSlider.Scroll += FuryStaminaSlider_Scroll;
            }
        }
        #endregion

        #region Volgin

        private void Volgin0HP_Click(object sender, EventArgs e)
        {
            BossManager.WriteVolginHealth(0x0000);
            LoggingManager.Instance.Log("Volgin's health was depleted via Health button click");
            StartConsistencyCheck();
            LoggingManager.Instance.Log("Consistency check started for Volgin.");
        }

        private void Volgin0Stam_Click(object sender, EventArgs e)
        {
            BossManager.WriteVolginStamina(0x0000);
            LoggingManager.Instance.Log("Volgin's stamina was depleted via Stamina button click");
            StartConsistencyCheck();
            LoggingManager.Instance.Log("Consistency check started for Volgin.");
        }

        private void VolginTimer_Tick(object sender, EventArgs e)
        {
            VolginHealthSlider.Scroll -= VolginHealthSlider_Scroll;
            VolginStaminaSlider.Scroll -= VolginStaminaSlider_Scroll;

            try
            {
                short currentHealth = BossManager.ReadVolginHealth();
                short currentStamina = BossManager.ReadVolginStamina();

                VolginHealthSlider.Value =
                    Math.Clamp(currentHealth, VolginHealthSlider.Minimum, VolginHealthSlider.Maximum);
                VolginStaminaSlider.Value =
                    Math.Clamp(currentStamina, VolginStaminaSlider.Minimum, VolginStaminaSlider.Maximum);
            }
            finally
            {
                VolginHealthSlider.Scroll += VolginHealthSlider_Scroll;
                VolginStaminaSlider.Scroll += VolginStaminaSlider_Scroll;
            }

        }

        private void VolginHealthSlider_Scroll(object sender, EventArgs e)
        {
            short newHealthValue = (short)VolginHealthSlider.Value;
            BossManager.WriteVolginHealth(newHealthValue);

            if (newHealthValue == 0)
            {
                LoggingManager.Instance.Log("Volgin's health was depleted via slider.");
            }
        }

        private void VolginStaminaSlider_Scroll(object sender, EventArgs e)
        {
            short newStaminaValue = (short)VolginStaminaSlider.Value;
            BossManager.WriteVolginStamina(newStaminaValue);

            if (newStaminaValue == 0)
            {
                LoggingManager.Instance.Log("Volgin's stamina was depleted via slider.");
            }
        }

        #endregion

        #region Shagohod

        private void ShagohodHealthSlider_Scroll(object sender, EventArgs e)
        {
            short newHealthValue = (short)ShagohodHealthSlider.Value;
            BossManager.WriteShagohodHealth(newHealthValue);

            if (newHealthValue == 0)
            {
                LoggingManager.Instance.Log("Shagohod's health was depleted via slider.");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for Shagohod.");
            }
        }

        private void ShagohodTimer_Tick(object sender, EventArgs e)
        {
            ShagohodHealthSlider.Scroll -= ShagohodHealthSlider_Scroll;

            try
            {
                short currentHealth = BossManager.ReadShagohodHealth();
                ShagohodHealthSlider.Value = Math.Clamp(currentHealth, ShagohodHealthSlider.Minimum, ShagohodHealthSlider.Maximum);
            }
            finally
            {
                ShagohodHealthSlider.Scroll += ShagohodHealthSlider_Scroll;
            }
        }

        private void Shagohod0HP_Click_1(object sender, EventArgs e)
        {
            BossManager.WriteShagohodHealth(0x0000);
            LoggingManager.Instance.Log("Shagohod's health was depleted via Health button click");
            StartConsistencyCheck();
            LoggingManager.Instance.Log("Consistency check started for Shagohod.");
        }

        #endregion

        #region Volgin on Shagohod

        private void VolginOnShagohog0HP_Click(object sender, EventArgs e)
        {
            BossManager.WriteVolginOnShagohodHealth(0x0000);
            LoggingManager.Instance.Log("Volgin on Shagohod's health was depleted via Health button click");
            StartConsistencyCheck();
            LoggingManager.Instance.Log("Consistency check started for Volgin on Shagohod.");
        }

        private void VolginOnShagohog0Stam_Click(object sender, EventArgs e)
        {
            BossManager.WriteVolginOnShagohodStamina(0x0000);
            LoggingManager.Instance.Log("Volgin on Shagohod's stamina was depleted via Stamina button click");
            StartConsistencyCheck();
            LoggingManager.Instance.Log("Consistency check started for Volgin on Shagohod.");
        }

        private void VolginOnShagohodHealthSlider_Scroll(object sender, EventArgs e)
        {
            short newHealthValue = (short)VolginOnShagohodHealthSlider.Value;
            BossManager.WriteVolginOnShagohodHealth(newHealthValue);

            if (newHealthValue == 0)
            {
                LoggingManager.Instance.Log("Volgin on Shagohod's health was depleted via slider.");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for Volgin on Shagohod.");
            }
        }

        private void VolginOnShagohodStaminaSlider_Scroll(object sender, EventArgs e)
        {
            short newStaminaValue = (short)VolginOnShagohodStaminaSlider.Value;
            BossManager.WriteVolginOnShagohodStamina(newStaminaValue);

            if (newStaminaValue == 0)
            {
                LoggingManager.Instance.Log("Volgin on Shagohod's stamina was depleted via slider.");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for Volgin on Shagohod.");
            }
        }

        private void VolginOnShagohodTimer_Tick(object sender, EventArgs e)
        {
            VolginOnShagohodHealthSlider.Scroll -= VolginOnShagohodHealthSlider_Scroll;
            VolginOnShagohodStaminaSlider.Scroll -= VolginOnShagohodStaminaSlider_Scroll;

            try
            {
                short currentHealth = BossManager.ReadVolginOnShagohodHealth();
                short currentStamina = BossManager.ReadVolginOnShagohodStamina();

                VolginOnShagohodHealthSlider.Value = Math.Clamp(currentHealth, VolginOnShagohodHealthSlider.Minimum, VolginOnShagohodHealthSlider.Maximum);
                VolginOnShagohodStaminaSlider.Value = Math.Clamp(currentStamina, VolginOnShagohodStaminaSlider.Minimum, VolginOnShagohodStaminaSlider.Maximum);
            }
            finally
            {
                VolginOnShagohodHealthSlider.Scroll += VolginOnShagohodHealthSlider_Scroll;
                VolginOnShagohodStaminaSlider.Scroll += VolginOnShagohodStaminaSlider_Scroll;
            }
        }

        #endregion

        #region The Boss

        private void Boss0HP_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheBossHealth(0x0001);
            LoggingManager.Instance.Log("The Boss's health was depleted via Health button click");
            StartConsistencyCheck();
            LoggingManager.Instance.Log("Consistency check started for The Boss.");
        }

        private void Boss0Stam_Click(object sender, EventArgs e)
        {
            BossManager.WriteTheBossStamina(0x0001);
            LoggingManager.Instance.Log("The Boss's stamina was depleted via Stamina button click");
            StartConsistencyCheck();
            LoggingManager.Instance.Log("Consistency check started for The Boss.");
        }

        private void BossHealthSlider_Scroll(object sender, EventArgs e)
        {
            short newHealthValue = (short)BossHealthSlider.Value;
            BossManager.WriteTheBossHealth(newHealthValue);

            if (newHealthValue == 0)
            {
                LoggingManager.Instance.Log("The Boss's health was depleted via slider.");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The Boss.");
            }
        }

        private void BossStaminaSlider_Scroll(object sender, EventArgs e)
        {
            short newStaminaValue = (short)BossStaminaSlider.Value;
            BossManager.WriteTheBossStamina(newStaminaValue);

            if (newStaminaValue == 0)
            {
                LoggingManager.Instance.Log("The Boss's stamina was depleted via slider.");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The Boss.");
            }
        }

        private void BossTimer_Tick(object sender, EventArgs e)
        {
            BossHealthSlider.Scroll -= BossHealthSlider_Scroll;
            BossStaminaSlider.Scroll -= BossStaminaSlider_Scroll;

            try
            {
                short currentHealth = BossManager.ReadTheBossHealth();
                short currentStamina = BossManager.ReadTheBossStamina();

                BossHealthSlider.Value = Math.Clamp(currentHealth, BossHealthSlider.Minimum, BossHealthSlider.Maximum);
                BossStaminaSlider.Value = Math.Clamp(currentStamina, BossStaminaSlider.Minimum, BossStaminaSlider.Maximum);
            }
            finally
            {
                BossHealthSlider.Scroll += BossHealthSlider_Scroll;
                BossStaminaSlider.Scroll += BossStaminaSlider_Scroll;
            }

        }

        #endregion
                  
    }
}