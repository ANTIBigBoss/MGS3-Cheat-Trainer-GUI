using System;
using System.Windows.Forms;

namespace MGS3_MC_Cheat_Trainer
{
    public partial class GameStatsForm : Form
    {
        private MainPointerManager pointerManager;
        private Dictionary<string, PictureBox> rankPictureMapping;
        private string lastDisplayedRank = null;

        public GameStatsForm()
        {
            InitializeComponent();
            pointerManager = new MainPointerManager();
            this.FormClosing += new FormClosingEventHandler(GameStatsForm_FormClosing);

            rankPictureMapping = new Dictionary<string, PictureBox>
        {
            { "Alligator", rankAlligatorPicturebox },
            { "Bat", rankBatPicturebox },
            { "Capybara", rankCapybaraPicturebox },
            { "Cat", rankCatPicturebox },
            { "Centipede", rankCentipedePicturebox },
            { "Chameleon", rankChameleonPicturebox },
            { "Chicken", rankChickenPicturebox },
            { "Cow", rankCowPicturebox },
            { "Crocodile", rankCrocodilePicturebox },
            { "Deer", rankDeerPicturebox },
            { "Doberman", rankDobermanPicturebox },
            { "Eagle", rankEaglePicturebox },
            { "Elephant", rankElephantPicturebox },
            { "Falcon", rankFalconPicturebox },
            { "Flying Fox", rankFlyingFoxPicturebox },
            { "Flying Squirrel", rankFlyingSquirrelPicturebox },
            { "Fox", rankFoxPicturebox },
            { "FoxHound", rankFoxHoundPicturebox },
            { "Giant Panda", rankGiantPandaPicturebox },
            { "Hawk", rankHawkPicturebox },
            { "Hippopotamus", rankHippopotamusPicturebox },
            { "Hound", rankHoundPicturebox },
            { "Hyena", rankHyenaPicturebox },
            { "Iguana", rankIguanaPicturebox },
            { "Jaguar", rankJaguarPicturebox },
            { "Jaws", rankJawsPicturebox },
            { "Kerotan", rankKerotanPicturebox },
            { "Koala", rankKoalaPicturebox },
            { "Komodo Dragon", rankKomodoDragonPicturebox },
            { "Leech", rankLeechPicturebox },
            { "Leopard", rankLeopardPicturebox },
            { "Mammoth", rankMammothPicturebox },
            { "Markhor", rankMarkhorPicturebox },
            { "Mongoose", rankMongoosePicturebox },
            { "Mouse", rankMousePicturebox },
            { "Night Owl", rankNightOwlPicturebox },
            { "Orca", rankOrcaPicturebox },
            { "Ostrich", rankOstrichPicturebox },
            { "Panther", rankPantherPicturebox },
            { "Pig", rankPigPicturebox },
            { "Pigeon", rankPigeonPicturebox },
            { "Piranha", rankPiranhaPicturebox },
            { "Puma", rankPumaPicturebox },
            { "Rabbit", rankRabbitPicturebox },
            { "Scorpion", rankScorpionPicturebox },
            { "Shark", rankSharkPicturebox },
            { "Sloth", rankSlothPicturebox },
            { "Spider", rankSpiderPicturebox },
            { "Swallow", rankSwallowPicturebox },
            { "Tarantula", rankTarantulaPicturebox },
            { "Tasmanian Devil", rankTasmanianDevilPicturebox },
            { "Tsuchinoko", rankTsuchinokoPicturebox },
            { "Whale", rankWhalePicturebox },
            { "Zebra", rankZebraPicturebox }
        };
        }

        private void GameStatsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoggingManager.Instance.Log("User has closed the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "GameStatsForm");
            Application.Exit();
        }

        private void LoadStats()
        {
            try
            {
                difficultyTextbox.Text = pointerManager.ReadDifficulty();
                playTimeTextbox.Text = pointerManager.ReadPlayTime();
                saveCountTextbox.Text = pointerManager.ReadSaves();
                continuesUsedTextbox.Text = pointerManager.ReadContinues();
                alertsTriggeredTextbox.Text = pointerManager.ReadAlertsTriggered();
                humansKilledTextbox.Text = pointerManager.ReadHumansKilled();
                timesSeriouslyInjuredTextbox.Text = pointerManager.ReadTimesSeriouslyInjured();
                mealsEatenTextbox.Text = pointerManager.ReadMealsEaten();
                totalDamageTakenTextbox.Text = pointerManager.ReadTotalDamageTaken();
                lifeMedUsedTextbox.Text = pointerManager.ReadLifeMedsUsed();
                plantsAndAnimalsCapturedTextbox.Text = pointerManager.ReadPlantsAndAnimalsCaptured();
                specialItemUsedTextbox.Text = pointerManager.ReadSpecialItemsUsed();

                string difficulty = pointerManager.ReadDifficulty();
                string projectedRank = pointerManager.ProjectedRank(difficulty);
                projectedRankTextbox.Text = projectedRank;

                UpdateRankImage(projectedRank);
            }
            catch (Exception ex)
            {
                LoggingManager.Instance.Log("Failed to load stats: " + ex.Message + "\n");
            }
        }

        private void UpdateRankImage(string rank)
        {
            if (rank == lastDisplayedRank)
            {
                return;
            }

            foreach (var picBox in rankPictureMapping.Values)
            {
                picBox.Visible = false;
            }

            if (rankPictureMapping.TryGetValue(rank, out PictureBox selectedPictureBox))
            {
                selectedPictureBox.Visible = true;
            }

            lastDisplayedRank = rank;
        }

        private void EditStatsButton_Click(object sender, EventArgs e)
        {
            CustomMessageBoxManager.ShowEditStatsDialog(pointerManager);
            LoadStats();
        }

        private void SwapToWeaponsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Weapon form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "WeaponForm");
            WeaponForm form1 = new();
            form1.Show();
            this.Hide();
        }

        private void SwapToItemsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Item form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "ItemForm");
            ItemForm form2 = new();
            form2.Show();
            this.Hide();
        }

        private void SwapToCamoForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Camo form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "CamoForm");
            CamoForm form3 = new();
            form3.Show();
            this.Hide();
        }

        private void SwapToMiscForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Misc form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "MiscForm");
            MiscForm form4 = new();
            form4.Show();
            this.Hide();
        }

        private void SwapToHealthAndAlertsForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Stats and Alert form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "StatsAndAlertForm");
            StatsAndAlertForm form5 = new();
            form5.Show();
            this.Hide();
        }

        private void SwapToBossForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Boss form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "BossForm");
            BossForm form6 = new();
            form6.Show();
            this.Hide();

        }

        private void SwapToDebugForm_Click(object sender, EventArgs e)
        {
            LoggingManager.Instance.Log("User is changing to the Debug form from the Game Stats form.\n");
            MemoryManager.UpdateLastFormLocation(this.Location);
            MemoryManager.LogFormLocation(this, "DebugForm");
            DebugForm form8 = new();
            form8.Show();
            this.Hide();

        }

        private void StatsUpdatingTimer_Tick(object sender, EventArgs e)
        {
            LoadStats();
        }

        private void GameStatsForm_Load(object sender, EventArgs e)
        {
            this.Location = MemoryManager.GetLastFormLocation();
            StatsUpdatingTimer.Start();
        }
    }
}