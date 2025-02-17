﻿using static MGS3_MC_Cheat_Trainer.StringManager;

// Much like BossManager this needs the same kind of rework to be more modular and less repetitive
namespace MGS3_MC_Cheat_Trainer
{
    public partial class BossForm : Form
    {
        public static BossForm Instance { get; private set; }

        #region Form Startup Boss Checks

        public BossForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(BossForm_FormClosing);
        }

        private void BossForm_Load(object sender, EventArgs e)
        {


            // Set Instance so TimerManager can call AttemptReFindAOB
            Instance = this;

            this.Location = MemoryManager.GetLastFormLocation();

            ConsistencyCheckTimer.Start();
            OcelotHealthSlider.Maximum = 3400; // Max health
            OcelotHealthSlider.Minimum = 1; // Min health
            OcelotStaminaSlider.Maximum = 3400; // Max stamina
            OcelotStaminaSlider.Minimum = 1; // Min stamina
            OcelotTextbox.Enabled = false;

            // Pain
            PainHealthSlider.Maximum = 30000;
            PainHealthSlider.Minimum = 0;
            PainStaminaSlider.Maximum = 21000;
            PainStaminaSlider.Minimum = 0;
            PainTextbox.Enabled = false;

            // Fear
            FearHealthSlider.Maximum = 30000;
            FearHealthSlider.Minimum = 0;
            FearStaminaSlider.Maximum = 21000;
            FearStaminaSlider.Minimum = 0;
            FearTextbox.Enabled = false;

            // The End
            EndHealthSlider.Maximum = 10000;
            EndHealthSlider.Minimum = 1;
            EndStaminaSlider.Maximum = 10000;
            EndStaminaSlider.Minimum = 1;
            EndTextbox.Enabled = false;

            // The Fury
            FuryHealthSlider.Maximum = 8500;
            FuryHealthSlider.Minimum = 1;
            FuryStaminaSlider.Maximum = 7500;
            FuryStaminaSlider.Minimum = 1;
            FuryTextbox.Enabled = false;

            // Volgin
            VolginHealthSlider.Maximum = 30000;
            VolginHealthSlider.Minimum = 0;
            VolginStaminaSlider.Maximum = 21000;
            VolginStaminaSlider.Minimum = 0;
            VolginTextbox.Enabled = false;

            // Shagohod
            ShagohodHealthSlider.Maximum = 8000;
            ShagohodHealthSlider.Minimum = 0;
            ShagohodTextbox.Enabled = false;

            // Volgin on Shagohod
            VolginOnShagohodHealthSlider.Maximum = 8000;
            VolginOnShagohodHealthSlider.Minimum = 0;
            VolginOnShagohodStaminaSlider.Maximum = 8000;
            VolginOnShagohodStaminaSlider.Minimum = 0;
            VolginOnShagohodTextbox.Enabled = false;

            // The Boss
            BossHealthSlider.Maximum = 10000;
            BossHealthSlider.Minimum = 1;
            BossStaminaSlider.Maximum = 10000;
            BossStaminaSlider.Minimum = 1;
            TheBossTextbox.Enabled = false;
        }

        #endregion

        private bool volginOnShagohodPhaseActive = false;
        private bool isInCutscene = false;

        private void EnableOcelotControls()
        {
            OcelotHealthSlider.Enabled = true;
            OcelotStaminaSlider.Enabled = true;
            Ocelot0HP.Enabled = true;
            Ocelot0Stam.Enabled = true;
            OcelotTimer.Interval = 1000;
            OcelotTimer.Tick += OcelotTimer_Tick;
            OcelotTimer.Start();
            LoggingManager.Instance.Log("Ocelot controls enabled");
            OcelotTextbox.Text = "Ocelot Controls Enabled";
        }

        private void EnablePainControls()
        {
            PainHealthSlider.Enabled = true;
            PainStaminaSlider.Enabled = true;
            Pain0HP.Enabled = true;
            Pain0Stam.Enabled = true;
            PainTimer.Interval = 1000;
            FearTimer.Tick += PainTimer_Tick;
            PainTimer.Start();
            LoggingManager.Instance.Log("The Pain controls enabled");
            PainTextbox.Text = "The Pain Controls Enabled";
        }

        private void EnableFearControls()
        {
            FearHealthSlider.Enabled = true;
            FearStaminaSlider.Enabled = true;
            Fear0HP.Enabled = true;
            Fear0Stam.Enabled = true;
            FearTimer.Interval = 1000;
            FearTimer.Tick += FearTimer_Tick;
            FearTimer.Start();
            LoggingManager.Instance.Log("The Fear controls enabled");
            FearTextbox.Text = "The Fear Controls Enabled";
        }

        private void EnableEndControls()
        {
            EndHealthSlider.Enabled = true;
            EndStaminaSlider.Enabled = true;
            End0HP.Enabled = true;
            End0Stam.Enabled = true;
            EndTimer.Interval = 1000;
            EndTimer.Tick += EndTimer_Tick;
            EndTimer.Start();
            LoggingManager.Instance.Log("The End's controls are enabled");
            EndTextbox.Text = "The End Controls Enabled";
        }

        private void EnableFuryControls()
        {
            FuryHealthSlider.Enabled = true;
            FuryStaminaSlider.Enabled = true;
            Fury0HP.Enabled = true;
            Fury0Stam.Enabled = true;
            FuryTimer.Interval = 1000;
            FuryTimer.Tick += FuryTimer_Tick;
            FuryTimer.Start();
            LoggingManager.Instance.Log("The Fury's controls are enabled");
            FuryTextbox.Text = "The Fury Controls Enabled";
        }

        private void EnableVolginControls()
        {
            VolginHealthSlider.Enabled = true;
            VolginStaminaSlider.Enabled = true;
            Volgin0HP.Enabled = true;
            Volgin0Stam.Enabled = true;
            VolginTimer.Interval = 1000;
            FearTimer.Tick += VolginTimer_Tick;
            VolginTimer.Start();
            LoggingManager.Instance.Log("Volgin's controls enabled");
            VolginTextbox.Text = "Volgin Controls Enabled";
        }

        private void EnableShagohodControls()
        {
            ShagohodHealthSlider.Enabled = true;
            Shagohod0HP.Enabled = true;
            ShagohodTimer.Interval = 1000;
            ShagohodTimer.Tick += ShagohodTimer_Tick;
            ShagohodTimer.Start();
            LoggingManager.Instance.Log("Shagohod controls enabled");
            ShagohodTextbox.Text = "Shagohod Controls Enabled";
        }

        private void EnableVolginOnShagohodControls()
        {
            VolginOnShagohodHealthSlider.Enabled = true;
            VolginOnShagohodStaminaSlider.Enabled = true;
            VolginOnShagohog0HP.Enabled = true;
            VolginOnShagohog0Stam.Enabled = true;
            VolginOnShagohodTimer.Interval = 1000;
            VolginOnShagohodTimer.Tick += VolginOnShagohodTimer_Tick;
            VolginOnShagohodTimer.Start();
            LoggingManager.Instance.Log("Volgin on Shagohod's controls enabled");
            VolginOnShagohodTextbox.Text = "Volgin on Shagohod Controls Enabled";
        }

        private void EnableBossControls()
        {
            BossHealthSlider.Enabled = true;
            BossStaminaSlider.Enabled = true;
            Boss0HP.Enabled = true;
            Boss0Stam.Enabled = true;
            BossTimer.Interval = 1000;
            BossTimer.Tick += BossTimer_Tick;
            BossTimer.Start();
            LoggingManager.Instance.Log("The Boss's controls are enabled");
            TheBossTextbox.Text = "The Boss Controls Enabled";
        }

        #region Consistency Check

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
            string newLocation = StringManager.Instance.GetCurrentMapLocation();
            bool newCutsceneState = StringManager.Instance.IsInCutscene();

            // Extract just the map string without area name
            string oldMap = ExtractMapString(currentLocation);
            string newMap = ExtractMapString(newLocation);

            bool locationChanged = newLocation != currentLocation || newCutsceneState != isInCutscene;

            if (locationChanged)
            {
                // Check if we are dealing with s171b scenario and staying within it
                bool wasIn171bScenario = oldMap == "s171b" || oldMap == "s171b_0";
                bool nowIn171bScenario = newMap == "s171b" || newMap == "s171b_0";

                currentLocation = newLocation;
                isInCutscene = newCutsceneState;

                // If we are staying within the s171b scenario (like going from s171b to s171b_0 or vice versa),
                // we skip disabling controls to avoid flicker:
                if (wasIn171bScenario && nowIn171bScenario)
                {
                    LoggingManager.Instance.Log("Staying within s171b scenario, not disabling controls to avoid flicker.");
                    // Just call SearchForBossAOB again if needed:
                    SearchForBossAOB();
                    LoggingManager.Instance.Log($"Area changed to {currentLocation}. Re-initializing for new area.");
                }
                else
                {
                    // For other scenarios, we disable all controls first:
                    DisableAllBossControls();
                    LoggingManager.Instance.Log("Disabling all boss controls while checking for a new Boss AOB.");
                    SearchForBossAOB();
                    LoggingManager.Instance.Log($"Area changed to {currentLocation}. Re-initializing for new area.");
                }
            }
            else if (IsBossDefeated() && IsNotBossLocation(ExtractMapString(currentLocation)))
            {
                // If a boss is defeated and we're not in a boss location, disable controls once:
                DisableAllBossControls();
                consistencyCheckNeeded = false;
            }
        }

        private string ExtractMapString(string fullLocation)
        {
            if (string.IsNullOrEmpty(fullLocation)) return "Unknown";
            if (fullLocation.Contains("not found")) return "Unknown";

            var parts = fullLocation.Split('-');
            if (parts.Length > 0)
            {
                string mapPart = parts[0].Trim();
                return mapPart;
            }

            return "Unknown";
        }

        private void SearchForBossAOB()
        {
            string locationString = StringManager.Instance.GetCurrentMapString();
            bool isCutscene = StringManager.Instance.IsInCutscene();

            // Handle The End fallback logic
            if (locationString == "s063a" || locationString == "s063a_0" ||
                locationString == "s064a" || locationString == "s064a_0" ||
                locationString == "s065a" || locationString == "s065a_0")
            {
                string bossMapString = StringManager.Instance.GetCurrentBossMapString();
                bool dynamicWorked = bossMapString != "Unknown" && !string.IsNullOrEmpty(bossMapString);

                if (!dynamicWorked)
                {
                    string directResult = StringManager.Instance.FindLocationStringDirectlyInRange();
                    string fallbackLocation = StringManager.Instance.ExtractLocationStringFromResult(directResult);
                    fallbackLocation = fallbackLocation.Replace(" (Cutscene)", "").Trim();

                    if (fallbackLocation != "Unknown")
                    {
                        locationString = fallbackLocation;
                    }
                    else
                    {
                        LoggingManager.Instance.Log("Unable to determine The End's location using both dynamic and fallback methods.");
                    }
                }
            }

            switch (locationString)
            {
                case "s023a":
                    LoggingManager.Instance.Log("Looking for Ocelot AOB");
                    BossManager.FindOcelotAOB();
                    EnableOcelotControls();
                    break;

                case "s023a_0":
                    OcelotTextbox.Text = "Ocelot controls disabled during cutscene";
                    break;

                case "s032b":
                    LoggingManager.Instance.Log("Looking for The Pain's AOB");
                    BossManager.FindTheFearAOB();
                    EnablePainControls();
                    break;

                case "s032b_0":
                    PainTextbox.Text = "Pain controls disabled during cutscene";
                    break;

                case "s051b":
                    LoggingManager.Instance.Log("Looking for The Fear AOB");
                    BossManager.FindTheFearAOB();
                    EnableFearControls();
                    break;

                case "s051b_0":
                    FearTextbox.Text = "Fear controls disabled during cutscene";
                    break;

                case "s063a":
                    LoggingManager.Instance.Log("Looking for The End AOB");
                    BossManager.FindTheEnds063aAOB();
                    EnableEndControls();
                    break;

                case "s063a_0":
                    EndTextbox.Text = "The End controls disabled during cutscene";
                    break;

                case "s064a":
                    LoggingManager.Instance.Log("Looking for The End AOB");
                    BossManager.FindTheEnds065aAOB();
                    EnableEndControls();
                    break;

                case "s064a_0":
                    EndTextbox.Text = "The End controls disabled during cutscene";
                    break;

                case "s065a":
                    LoggingManager.Instance.Log("Looking for The End AOB");
                    BossManager.FindTheEnds065aAOB();
                    EnableEndControls();
                    break;

                case "s065a_0":
                    EndTextbox.Text = "The End controls disabled during cutscene";
                    break;

                case "s081a":
                    LoggingManager.Instance.Log("Looking for The Fury AOB");
                    BossManager.FindTheFuryAOB();
                    EnableFuryControls();
                    break;

                case "s081a_0":
                    FuryTextbox.Text = "The Fury controls disabled during cutscene";
                    break;

                case "s122a":
                    LoggingManager.Instance.Log("Looking for Volgin AOB");
                    BossManager.FindTheFearAOB();
                    EnableVolginControls();
                    break;

                case "s122a_1":
                    VolginTextbox.Text = "Volgin Controls disabled during cutscene";
                    break;

                case "s171b":
                    LoggingManager.Instance.Log("Looking for Shagohod AOB");
                    HandleShagohodVolginOnShagohod();
                    break;

                case "s171b_0":
                    // If Shagohod not dead, show disabled message
                    // If Shagohod dead, VolginOnShagohod disabled message
                    if (!BossManager.IsShagohodDead())
                        ShagohodTextbox.Text = "Shagohod controls disabled during cutscene";
                    else
                        VolginOnShagohodTextbox.Text = "Volgin on Shagohod controls disabled during cutscene";
                    break;

                case "s201a":
                    LoggingManager.Instance.Log("Looking for The Boss AOB");
                    BossManager.FindTheBossAOB();
                    EnableBossControls();
                    break;

                case "s201a_0":
                    TheBossTextbox.Text = "The Boss controls disabled during cutscene";
                    break;

                default:
                    // Non-boss or unknown location: do nothing
                    break;
            }
        }

        public void AttemptReFindAOB(string mapString)
        {
            switch (mapString)
            {
                case "s023a":
                    LoggingManager.Instance.Log("Reattempting Ocelot AOB");
                    BossManager.FindOcelotAOB();
                    EnableOcelotControls();
                    break;

                case "s023a_0":
                    OcelotTextbox.Text = "Ocelot controls disabled during cutscene";
                    break;

                case "s032b":
                    LoggingManager.Instance.Log("Reattempting The Pain's AOB");
                    BossManager.FindTheFearAOB();
                    EnablePainControls();
                    break;

                case "s032b_0":
                    PainTextbox.Text = "Pain controls disabled during cutscene";
                    break;

                case "s051b":
                    LoggingManager.Instance.Log("Reattempting The Fear AOB");
                    BossManager.FindTheFearAOB();
                    EnableFearControls();
                    break;

                case "s051b_0":
                    FearTextbox.Text = "Fear controls disabled during cutscene";
                    break;

                case "s063a":
                    LoggingManager.Instance.Log("Reattempting The End AOB");
                    BossManager.FindTheEnds063aAOB();
                    EnableEndControls();
                    break;

                case "s063a_0":
                    EndTextbox.Text = "The End controls disabled during cutscene";
                    break;

                case "s064a":
                    LoggingManager.Instance.Log("Reattempting The End AOB");
                    BossManager.FindTheEnds065aAOB();
                    EnableEndControls();
                    break;

                case "s064a_0":
                    EndTextbox.Text = "The End controls disabled during cutscene";
                    break;

                case "s065a":
                    LoggingManager.Instance.Log("Reattempting The End AOB");
                    BossManager.FindTheEnds065aAOB();
                    EnableEndControls();
                    break;

                case "s065a_0":
                    EndTextbox.Text = "The End controls disabled during cutscene";
                    break;

                case "s081a":
                    LoggingManager.Instance.Log("Reattempting The Fury AOB");
                    BossManager.FindTheFuryAOB();
                    EnableFuryControls();
                    break;

                case "s081a_0":
                    FuryTextbox.Text = "The Fury controls disabled during cutscene";
                    break;

                case "s122a":
                    LoggingManager.Instance.Log("Reattempting Volgin AOB");
                    BossManager.FindTheFearAOB();
                    EnableVolginControls();
                    break;

                case "s122a_1":
                    VolginTextbox.Text = "Volgin Controls disabled during cutscene";
                    break;

                case "s171b":
                    LoggingManager.Instance.Log("Looking for Shagohod AOB");
                    HandleShagohodVolginOnShagohod();
                    break;

                case "s171b_0":
                    // If Shagohod not dead, show disabled message
                    // If Shagohod dead, VolginOnShagohod disabled message
                    if (!BossManager.IsShagohodDead())
                        ShagohodTextbox.Text = "Shagohod controls disabled during cutscene";
                    else
                        VolginOnShagohodTextbox.Text = "Volgin on Shagohod controls disabled during cutscene";
                    break;

                case "s201a":
                    LoggingManager.Instance.Log("Looking for The Boss AOB");
                    BossManager.FindTheBossAOB();
                    EnableBossControls();
                    break;

                case "s201a_0":
                    TheBossTextbox.Text = "The Boss controls disabled during cutscene";
                    break;

                default:
                    break;
            }
        }

        private void HandleShagohodVolginOnShagohod()
        {
            short shagohodResult = BossManager.FindShagohodAOB();
            if (shagohodResult != -1 && !BossManager.IsShagohodDead())
            {
                // Shagohod alive
                EnableShagohodControls();
                LoggingManager.Instance.Log("Shagohod's controls enabled");
            }
            else if (BossManager.IsShagohodDead())
            {
                LoggingManager.Instance.Log("Shagohod AOB found but Shagohod is dead. Looking for VolginOnShagohod AOB");
                VolginOnShagohodTextbox.Text = "Looking for Volgin on Shagohod Please wait...";
                short volginOnShagohodResult = BossManager.FindVolginOnShagohodAOB();
                if (volginOnShagohodResult != -1)
                {
                    EnableVolginOnShagohodControls();
                    LoggingManager.Instance.Log("Volgin on Shagohod's controls are enabled");
                }
                else
                {
                    LoggingManager.Instance.Log("Shagohod and Volgin on Shagohod AOB not found.");
                }
            }
            else
            {
                // Not found at all
                LoggingManager.Instance.Log("Shagohod and Volgin on Shagohod AOB not found.");
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

        #endregion

        private bool IsBossDefeated()
        {
            return
            BossManager.IsOcelotDead() || BossManager.IsOcelotStunned() ||
            BossManager.IsThePainDead() || BossManager.IsThePainStunned() ||
            BossManager.IsTheFearDead() || BossManager.IsTheFearStunned() ||
            BossManager.IsTheEnds063aDead() || BossManager.IsTheEnds063aStunned() ||
            BossManager.IsTheEnds065aDead() || BossManager.IsTheEnds065aStunned() ||
            BossManager.IsTheFuryDead() || BossManager.IsTheFuryStunned() ||
            BossManager.IsVolginDead() || BossManager.IsVolginStunned() ||
            BossManager.IsShagohodDead() ||
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

            // Pain
            PainHealthSlider.Enabled = false;
            PainStaminaSlider.Enabled = false;
            Pain0HP.Enabled = false;
            Pain0Stam.Enabled = false;
            PainTimer.Stop();
            PainTextbox.Text = "The Pain Controls Disabled";

            // Fear
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

        #region Form Swapping and Closing

        private void BossForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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
            // Dynamic-first fallback logic
            string bossMapString = StringManager.Instance.GetCurrentBossMapString();
            bool dynamicWorked = bossMapString != "Unknown" && !string.IsNullOrEmpty(bossMapString);

            if (!dynamicWorked)
            {
                string directResult = StringManager.Instance.FindLocationStringDirectlyInRange();
                string fallbackLocation = StringManager.Instance.ExtractLocationStringFromResult(directResult);
                fallbackLocation = fallbackLocation.Replace(" (Cutscene)", "").Trim();
                bossMapString = fallbackLocation;
            }

            // Now use bossMapString to determine which area The End is in
            if (bossMapString == "s063a")
            {
                BossManager.WriteTheEnds063aHealth(0x0001);
                LoggingManager.Instance.Log("The End's health was depleted via Health button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
            else if (bossMapString == "s064a")
            {
                BossManager.WriteTheEnds065aHealth(0x0001);
                LoggingManager.Instance.Log("The End's health was depleted via Health button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
            else if (bossMapString == "s065a")
            {
                BossManager.WriteTheEnds065aHealth(0x0001);
                LoggingManager.Instance.Log("The End's health was depleted via Health button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
            else
            {
                LoggingManager.Instance.Log("Unable to determine The End's location using both dynamic and fallback methods.");
            }
        }

        private void End0Stam_Click(object sender, EventArgs e)
        {
            string bossMapString = StringManager.Instance.GetCurrentBossMapString();
            bool dynamicWorked = bossMapString != "Unknown" && !string.IsNullOrEmpty(bossMapString);

            if (!dynamicWorked)
            {
                string directResult = StringManager.Instance.FindLocationStringDirectlyInRange();
                string fallbackLocation = StringManager.Instance.ExtractLocationStringFromResult(directResult);
                fallbackLocation = fallbackLocation.Replace(" (Cutscene)", "").Trim();
                bossMapString = fallbackLocation;
            }

            if (bossMapString == "s063a")
            {
                BossManager.WriteTheEnds063aStamina(0x0001);
                LoggingManager.Instance.Log("The End's stamina was depleted via Stamina button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
            else if (bossMapString == "s064a")
            {
                BossManager.WriteTheEnds065aStamina(0x0001);
                LoggingManager.Instance.Log("The End's stamina was depleted via Stamina button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
            else if (bossMapString == "s065a")
            {
                BossManager.WriteTheEnds065aStamina(0x0001);
                LoggingManager.Instance.Log("The End's stamina was depleted via Stamina button click");
                StartConsistencyCheck();
                LoggingManager.Instance.Log("Consistency check started for The End.");
            }
            else
            {
                LoggingManager.Instance.Log("Unable to determine The End's location using both dynamic and fallback methods.");
            }
        }

        private void EndHealthSlider_Scroll(object sender, EventArgs e)
        {
            string bossMapString = StringManager.Instance.GetCurrentBossMapString();
            bool dynamicWorked = bossMapString != "Unknown" && !string.IsNullOrEmpty(bossMapString);

            if (!dynamicWorked)
            {
                string directResult = StringManager.Instance.FindLocationStringDirectlyInRange();
                string fallbackLocation = StringManager.Instance.ExtractLocationStringFromResult(directResult);
                fallbackLocation = fallbackLocation.Replace(" (Cutscene)", "").Trim();
                bossMapString = fallbackLocation;
            }

            short newHealthValue = (short)EndHealthSlider.Value;

            if (bossMapString == "s063a")
            {
                BossManager.WriteTheEnds063aHealth(newHealthValue);
                if (newHealthValue == 0)
                {
                    LoggingManager.Instance.Log("The End's health was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }
            else if (bossMapString == "s064a")
            {
                BossManager.WriteTheEnds065aHealth(newHealthValue);
                if (newHealthValue == 0)
                {
                    LoggingManager.Instance.Log("The End's health was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }
            else if (bossMapString == "s065a")
            {
                BossManager.WriteTheEnds065aHealth(newHealthValue);
                if (newHealthValue == 0)
                {
                    LoggingManager.Instance.Log("The End's health was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }
            else
            {
                LoggingManager.Instance.Log("Unable to determine The End's location using both dynamic and fallback methods.");
            }
        }

        private void EndStaminaSlider_Scroll(object sender, EventArgs e)
        {
            string bossMapString = StringManager.Instance.GetCurrentBossMapString();
            bool dynamicWorked = bossMapString != "Unknown" && !string.IsNullOrEmpty(bossMapString);

            if (!dynamicWorked)
            {
                string directResult = StringManager.Instance.FindLocationStringDirectlyInRange();
                string fallbackLocation = StringManager.Instance.ExtractLocationStringFromResult(directResult);
                fallbackLocation = fallbackLocation.Replace(" (Cutscene)", "").Trim();
                bossMapString = fallbackLocation;
            }

            short newStaminaValue = (short)EndStaminaSlider.Value;

            if (bossMapString == "s063a")
            {
                BossManager.WriteTheEnds063aStamina(newStaminaValue);
                if (newStaminaValue == 0)
                {
                    LoggingManager.Instance.Log("The End's stamina was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }
            else if (bossMapString == "s064a")
            {
                BossManager.WriteTheEnds065aStamina(newStaminaValue);
                if (newStaminaValue == 0)
                {
                    LoggingManager.Instance.Log("The End's stamina was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }
            else if (bossMapString == "s065a")
            {
                BossManager.WriteTheEnds065aStamina(newStaminaValue);
                if (newStaminaValue == 0)
                {
                    LoggingManager.Instance.Log("The End's stamina was depleted via slider.");
                    StartConsistencyCheck();
                    LoggingManager.Instance.Log("Consistency check started for The End.");
                }
            }
            else
            {
                LoggingManager.Instance.Log("Unable to determine The End's location using both dynamic and fallback methods.");
            }
        }

        private void EndTimer_Tick(object sender, EventArgs e)
        {
            string bossMapString = StringManager.Instance.GetCurrentBossMapString();
            bool dynamicWorked = bossMapString != "Unknown" && !string.IsNullOrEmpty(bossMapString);

            if (!dynamicWorked)
            {
                string directResult = StringManager.Instance.FindLocationStringDirectlyInRange();
                string fallbackLocation = StringManager.Instance.ExtractLocationStringFromResult(directResult);
                fallbackLocation = fallbackLocation.Replace(" (Cutscene)", "").Trim();
                bossMapString = fallbackLocation;
            }

            EndHealthSlider.Scroll -= EndHealthSlider_Scroll;
            EndStaminaSlider.Scroll -= EndStaminaSlider_Scroll;

            try
            {
                if (bossMapString == "s063a")
                {
                    short currentHealth = BossManager.ReadTheEnds063aHealth();
                    short currentStamina = BossManager.ReadTheEnds063aStamina();
                    EndHealthSlider.Value = Math.Clamp(currentHealth, EndHealthSlider.Minimum, EndHealthSlider.Maximum);
                    EndStaminaSlider.Value = Math.Clamp(currentStamina, EndStaminaSlider.Minimum, EndStaminaSlider.Maximum);
                }
                else if (bossMapString == "s064a")
                {
                    short currentHealth = BossManager.ReadTheEnds065aHealth();
                    short currentStamina = BossManager.ReadTheEnds065aStamina();
                    EndHealthSlider.Value = Math.Clamp(currentHealth, EndHealthSlider.Minimum, EndHealthSlider.Maximum);
                    EndStaminaSlider.Value = Math.Clamp(currentStamina, EndStaminaSlider.Minimum, EndStaminaSlider.Maximum);
                }
                else if (bossMapString == "s065a")
                {
                    short currentHealth = BossManager.ReadTheEnds065aHealth();
                    short currentStamina = BossManager.ReadTheEnds065aStamina();
                    EndHealthSlider.Value = Math.Clamp(currentHealth, EndHealthSlider.Minimum, EndHealthSlider.Maximum);
                    EndStaminaSlider.Value = Math.Clamp(currentStamina, EndStaminaSlider.Minimum, EndStaminaSlider.Maximum);
                }
                else
                {
                    LoggingManager.Instance.Log("Unable to determine The End's location using both dynamic and fallback methods.");
                }
            }
            finally
            {
                EndHealthSlider.Scroll += EndHealthSlider_Scroll;
                EndStaminaSlider.Scroll += EndStaminaSlider_Scroll;
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


        private void SwapToWeaponsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Weapon form from the Boss form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "WeaponForm");
            WeaponForm form1 = new();
            form1.Show();
            this.Hide();
        }

        private void SwapToItemsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Item form from the Boss form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "ItemForm");
            ItemForm form2 = new();
            form2.Show();
            this.Hide();
        }

        private void SwapToCamoForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Camo form from the Boss form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "CamoForm");
            CamoForm form3 = new();
            form3.Show();
            this.Hide();
        }

        private void SwapToMiscForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Misc form from the Boss form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "MiscForm");
            MiscForm form4 = new();
            form4.Show();
            this.Hide();
        }

        private void SwapToHealthAndAlertsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Stats and Alert form from the Boss form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "StatsAndAlertForm");
            StatsAndAlertForm form5 = new();
            form5.Show();
            this.Hide();
        }

        private void SwapToGameStatsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Game Stats form from the Boss form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "GameStatsForm");
            GameStatsForm form7 = new();
            form7.Show();
            this.Hide();
        }

        private void SwapToDebugForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Debug form from the Boss form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "DebugForm");
            DebugForm form8 = new();
            form8.Show();
            this.Hide();

        }
    }
}