using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{

    /*
    We can probably just have two base classes one for weapons and one for items
    based on how the game data is structured the weapons go from Survival Knife to Directional Microphone
    and the items go from Life Medicine to USA face paint.
    */

    #region Base classes
    public class GameObject
    {
        internal string _name = "";
        internal IntPtr _memoryOffset;
    }

    public interface IMGS3Object
    {
        private static string name = "";

        //internal GameObject gameObject { get; set; }

        public static string Name { get { return name; } }
    }

    public static class WeaponAddresses
    {
        public const int WeaponOffset = 80; // The offset between weapons
        public const int CurrentAmmoOffset = 0; // The offset for the current ammo couldn't get this to work in an AOB as the WeaponOffset so this was my less than ideal workaround
        public const int MaxAmmoOffset = 2; // The offset for the max ammo
        public const int ClipOffset = 4;    // The offset for the clip
        public const int MaxClipOffset = 6; // The offset for the max clip
        public const int SuppressorToggleOffset = 16; // The offset for the suppressor toggle
                                               // Suppressor capacity is actually considered an item and not a weapon
        public static IntPtr GetAddress(int index, MemoryManager memoryManager)
        {
            IntPtr aobResult = memoryManager.FindAOBInWeaponAndItemTableRange(Constants.AOBs["WeaponsTable"].Pattern, Constants.AOBs["WeaponsTable"].Mask);
            if (aobResult != IntPtr.Zero)
            {
                // Calculate the specific weapon address
                return IntPtr.Add(aobResult, Constants.AOBs["WeaponsTable"].Pattern.Length + 12 + (WeaponOffset * index));
            }
            else
            {
                MessageBox.Show("Weapons table AOB not found in memory.");
                return IntPtr.Zero;
            }
        }

            public static IntPtr GetMaxAmmoAddress(IntPtr baseAddress)
            {
                return baseAddress + MaxAmmoOffset;
            }

            public static IntPtr GetClipAddress(IntPtr baseAddress)
            {
                return baseAddress + ClipOffset;
            }

            public static IntPtr GetMaxClipAddress(IntPtr baseAddress)
            {
                return baseAddress + MaxClipOffset;
            }

            public static IntPtr GetSuppressorToggleAddress(IntPtr baseAddress)
            {
                return baseAddress + SuppressorToggleOffset;
            }
        }

        public static class ItemAddresses
        {
            public const int ItemOffset = 80; // The offset between items
            public const int CurrentCapacityOffset = 0; // The offset for the current capacity
            public const int MaxOffset = 2;   // The offset for the max value

        public static IntPtr GetAddress(int index, MemoryManager memoryManager)
        {
            IntPtr aobResult = memoryManager.FindAOBInWeaponAndItemTableRange(Constants.AOBs["ItemsTable"].Pattern, Constants.AOBs["ItemsTable"].Mask);
            if (aobResult != IntPtr.Zero)
            {
                // Calculate the specific item address
                return IntPtr.Add(aobResult, Constants.AOBs["ItemsTable"].Pattern.Length + 12 + (ItemOffset * index));
            }
            else
            {
                MessageBox.Show("Items table AOB not found in memory.");
                return IntPtr.Zero;
            }
        }

        public static IntPtr GetMaxAddress(IntPtr baseAddress)
            {
                return baseAddress + MaxOffset;
            }
        }

    public class Item : BaseMGS3Object
    {
        public IntPtr MaxCapacityOffset { get; private set; }
        public string AobKey { get; private set; } // A key to look up AOBs in Constants
        public int Index { get; private set; } // Add this line

        // Constructor for items without a max capacity
        public Item(string name, int index, string aobKey, bool hasMaxCapacity = false)
    : base(name, IntPtr.Zero) // Initially, we don't have the address
        {
            Index = index; // Set the index
            AobKey = aobKey;

            if (hasMaxCapacity)
            {
                MaxCapacityOffset = ItemAddresses.GetMaxAddress(this.MemoryOffset);
            }
        }
    }



        public abstract class BaseMGS3Object : IMGS3Object
        {
            protected GameObject gameObject { get; set; }
            public string Name { get { return gameObject._name; } }
            public IntPtr MemoryOffset { get { return gameObject._memoryOffset; } }

            public BaseMGS3Object(string name, IntPtr memoryOffset)
            {
                gameObject = new GameObject { _name = name, _memoryOffset = memoryOffset };
            }
        }

        public class Weapon : BaseMGS3Object
        {
            public IntPtr MaxAmmoOffset { get; private set; } = IntPtr.Zero;
            public IntPtr ClipOffset { get; private set; } = IntPtr.Zero;
            public IntPtr MaxClipOffset { get; private set; } = IntPtr.Zero;
            public IntPtr SuppressorToggleOffset { get; private set; } = IntPtr.Zero;
            public string AobKey { get; private set; } // A key to look up AOBs in Constants
            public int Index { get; private set; } // Add this line


        // Constructor for a weapon
        public Weapon(string name, int index, string aobKey, bool hasAmmo = false, bool hasClip = false, bool hasSuppressorToggle = false)
    : base(name, IntPtr.Zero) // Initially, we don't have the address
        {
            Index = index; // Set the index
            AobKey = aobKey;

            if (hasAmmo)
                {
                    MaxAmmoOffset = WeaponAddresses.GetMaxAmmoAddress(this.MemoryOffset);
                }

                if (hasClip)
                {
                    ClipOffset = WeaponAddresses.GetClipAddress(this.MemoryOffset);
                    MaxClipOffset = WeaponAddresses.GetMaxClipAddress(this.MemoryOffset);
                }

                if (hasSuppressorToggle)
                {
                    SuppressorToggleOffset = WeaponAddresses.GetSuppressorToggleAddress(this.MemoryOffset);
                }
            }
        }

        #endregion

        public class MGS3UsableObjects
        {
            /* It's a little messier but I thinking listing based on the table's data structure is better in the long run
            Here's how the table works:
            The Index is our sequential 80 bytes the 3 bools are for Max Ammo, Clip, Max Clip and Suppressor Toggle
            */
            #region Weapons
            private static MemoryManager memoryManager = new MemoryManager();
        

        public static readonly Weapon SurvivalKnife = new("Survival Knife", 0, "WeaponsTable", false, false, false);
            public static readonly Weapon Fork = new("Fork", 1, "WeaponsTable", false, false, false);
            public static readonly Weapon CigSpray = new("Cigspray", 2, "WeaponsTable", true, false, false); // Has ammo but no clip or suppressor
            public static readonly Weapon Handkerchief = new("Handkerchief", 3, "WeaponsTable", true, false, false);
            public static readonly Weapon MK22 = new("MK22", 4, "WeaponsTable", true, true, true);
            public static readonly Weapon M1911A1 = new("M1911A1", 5, "WeaponsTable", true, true, true);
            public static readonly Weapon EzGun = new("Ez Gun", 6, "WeaponsTable", false, false, false);
            public static readonly Weapon SAA = new("SAA", 7, "WeaponsTable", true, true, false); // Has ammo and clips but no suppressor
            public static readonly Weapon Patriot = new("Patriot", 8, "WeaponsTable", false, false, false);
            public static readonly Weapon Scorpion = new("Scorpion", 9, "WeaponsTable", true, true, false);
            public static readonly Weapon XM16E1 = new("XM16E1", 10, "WeaponsTable", true, true, true);
            public static readonly Weapon AK47 = new("AK47", 11, "WeaponsTable", true, true, false);
            public static readonly Weapon M63 = new("M63", 12, "WeaponsTable", true, true, false);
            public static readonly Weapon M37 = new("M37", 13, "WeaponsTable", true, true, false);
            public static readonly Weapon SVD = new("SVD", 14, "WeaponsTable", true, true, false);
            public static readonly Weapon Mosin = new("Mosin", 15, "WeaponsTable", true, true, false);
            public static readonly Weapon RPG7 = new("RPG7", 16, "WeaponsTable", true, true, false);
            public static readonly Weapon Torch = new("Torch", 17, "WeaponsTable", false, false, false);
            public static readonly Weapon Grenade = new("Grenade", 18, "WeaponsTable", true, false, false);
            public static readonly Weapon WpGrenade = new("Wp Grenade", 19, "WeaponsTable", true, false, false);
            public static readonly Weapon StunGrenade = new("Stun Grenade", 20, "WeaponsTable", true, false, false);
            public static readonly Weapon ChaffGrenade = new("Chaff Grenade", 21, "WeaponsTable", true, false, false);
            public static readonly Weapon SmokeGrenade = new("Smoke Grenade", 22, "WeaponsTable", true, false, false);
            public static readonly Weapon EmptyMag = new("Empty Magazine", 23, "WeaponsTable", true, false, false);
            public static readonly Weapon TNT = new("TNT", 24, "WeaponsTable", true, false, false);
            public static readonly Weapon C3 = new("C3", 25, "WeaponsTable", true, false, false);
            public static readonly Weapon Claymore = new("Claymore", 26, "WeaponsTable", true, false, false);
            public static readonly Weapon Book = new("Book", 27, "WeaponsTable", true, false, false);
            public static readonly Weapon Mousetrap = new("Mousetrap", 28, "WeaponsTable", true, false, false);
            public static readonly Weapon DirectionalMic = new("Directional Microphone", 29, "WeaponsTable", false, false, false);
        // When I learn more on how to force certain food items to be in the 19 slots affter will implement logic here
        #endregion

        #region Items
        #region Backpack Items

            public static readonly Item LifeMed = new("LIFE MEDICINE", 0, "ItemsTable", true);
            public static readonly Item Pentazemin = new("PENTAZEMIN", 1, "ItemsTable", true);
            public static readonly Item FakeDeathPill = new("FAKE DEATH PILL", 2, "ItemsTable", true);
        // No Capacity, will just have a true 1 or false -1 to give/remove
        public static readonly Item RevivalPill = new("REVIVAL PILL", 3, "ItemsTable", false);
            public static readonly Item Cigar = new("CIGAR", 4, "ItemsTable", false);
            public static readonly Item Binoculars = new("BINOCULARS", 5, "ItemsTable", false);
            public static readonly Item ThermalGoggles = new("THERMAL GOGGLES", 6, "ItemsTable", false);
            public static readonly Item NightVisionGoggles = new("NIGHT VISION GOGGLES", 7, "ItemsTable", false);
            public static readonly Item Camera = new("CAMERA", 8, "ItemsTable", false);
            public static readonly Item MotionDetector = new("MOTION DETECTOR", 9, "ItemsTable", false);
            public static readonly Item ActiveSonar = new("ACTIVE SONAR", 10, "ItemsTable", false);
            public static readonly Item MineDetector = new("MINE DETECTOR", 11, "ItemsTable", false);
            public static readonly Item AntiPersonnelSensor = new("ANTI PERSONNEL SENSOR", 12, "ItemsTable", false);
            //Boxes have slightly different logic and should be set to 25 or -1 as they have a durability
            public static readonly Item CBoxA = new("CBOX A", 13, "ItemsTable", false);
            public static readonly Item CBoxB = new("CBOX B", 14, "ItemsTable", false);
            public static readonly Item CBoxC = new("CBOX C", 15, "ItemsTable", false);
            public static readonly Item CBoxD = new("CBOX D", 16, "ItemsTable", false);
            public static readonly Item CrocCap = new("CROC CAP", 17, "ItemsTable", false);
            public static readonly Item KeyA = new("KEY A", 18, "ItemsTable", false);
            public static readonly Item KeyB = new("KEY B", 19, "ItemsTable", false);
            public static readonly Item KeyC = new("KEY C", 20, "ItemsTable", false);
            public static readonly Item Bandana = new("BANDANA", 21, "ItemsTable", false);
            public static readonly Item StealthCamo = new("STEALTH CAMO", 22, "ItemsTable", false);
            public static readonly Item BugJuice = new("BUG JUICE", 23, "ItemsTable", true);
            public static readonly Item MonkeyMask = new("MONKEY MASK", 24, "ItemsTable", false);
            #endregion
            #endregion

            #region Medicinal "L2" Items
            public static readonly Item Serum = new("SERUM", 25, "ItemsTable", true);
            public static readonly Item Antidote = new("ANTIDOTE", 26, "ItemsTable", true);
            public static readonly Item ColdMedicine = new("COLD MEDICINE", 27, "ItemsTable", true);
            public static readonly Item DigestiveMedicine = new("DIGESTIVE MEDICINE", 28, "ItemsTable", true);
            #endregion
            #region Surgical "R2" Items
            public static readonly Item Ointment = new("OINTMENT", 29, "ItemsTable", true);
            public static readonly Item Splint = new("SPLINT", 30, "ItemsTable", true);
            public static readonly Item Disinfectant = new("DISINFECTANT", 31, "ItemsTable", true);
            public static readonly Item Styptic = new("STYPTIC", 32, "ItemsTable", true);
            public static readonly Item Bandage = new("BANDAGE", 33, "ItemsTable", true);
            public static readonly Item SutureKit = new("SUTURE KIT", 34, "ItemsTable", true);
            #endregion

            // 35,36,37,38, 39 are other items to be implemented in this part of code but
            // camos are preceding them in the pattern so this code is a placeholder until
            // I put in 35-39 i.e. Knife, Battery and Surpressor quantities

            #region Other Items
            // Battery may have a max capacity and need to be changed to true unless address is max capacity
            // Was never able to find a static address for current battery in Cheat Engine
            public static readonly Item Knife = new("KNIFE", 35, "ItemsTable", false); // No use but listing for completeness of table
            public static readonly Item Battery = new("BATTERY", 36, "ItemsTable", false); // Need to look into exactly what this controls
                                                                             // Need to shift around logic for suppressor toggle since I think somewhere in code still thinks it's a weapon
            public static readonly Item M1911A1Surpressor = new("M1911A1 SURPRESSOR", 37, "ItemsTable", false);
            public static readonly Item MK22Surpressor = new("MK22 SURPRESSOR", 38, "ItemsTable", false);
            public static readonly Item XM16E1Surpressor = new("XM16E1 SURPRESSOR", 39, "ItemsTable", false);
            #endregion

            #region Camos

            #region Uniform
            // Moved uniform addresses above face paint to align better with the data structure MGS3 has

            // Naked camo for some reason is just not in the table if for some reason we want to add it

            public static readonly Item OliveDrab = new("Olive Drab", 40, "ItemsTable", false);
            public static readonly Item TigerStripe = new("Tiger Stripe", 41, "ItemsTable", false);
            public static readonly Item Leaf = new("Leaf", 42, "ItemsTable", false);
            public static readonly Item TreeBark = new("Tree Bark", 43, "ItemsTable", false);
            public static readonly Item ChocoChip = new("Choco Chip", 44, "ItemsTable", false);
            public static readonly Item Splitter = new("Splitter", 45, "ItemsTable", false);
            public static readonly Item Raindrop = new("Raindrop", 46, "ItemsTable", false);
            public static readonly Item Squares = new("Squares", 47, "ItemsTable", false);
            public static readonly Item Water = new("Water", 48, "ItemsTable", false);
            public static readonly Item Black = new("Black", 49, "ItemsTable", false);
            public static readonly Item Snow = new("Snow", 50, "ItemsTable", false);
            public static readonly Item Naked = new("Naked", 51, "ItemsTable", false);
            public static readonly Item SneakingSuit = new("Sneaking Suit", 52, "ItemsTable", false);
            public static readonly Item Scientist = new("Scientist", 53, "ItemsTable", false);
            public static readonly Item Officer = new("Officer", 54, "ItemsTable", false);
            public static readonly Item Maintenance = new("Maintenance", 55, "ItemsTable", false);
            public static readonly Item Tuxedo = new("Tuxedo", 56, "ItemsTable", false);
            public static readonly Item HornetStripe = new("Hornet Stripe", 57, "ItemsTable", false);
            public static readonly Item Spider = new("Spider", 58, "ItemsTable", false);
            public static readonly Item Moss = new("Moss", 59, "ItemsTable", false);
            public static readonly Item Fire = new("Fire", 60, "ItemsTable", false);
            public static readonly Item Spirit = new("Spirit", 61, "ItemsTable", false);
            public static readonly Item ColdWar = new("Cold War", 62, "ItemsTable", false);
            public static readonly Item Snake = new("Snake", 63, "ItemsTable", false);
            public static readonly Item GaKo = new("Ga-Ko", 64, "ItemsTable", false);
            public static readonly Item DesertTiger = new("DesertTiger", 65, "ItemsTable", false);
            public static readonly Item DPM = new("DPM", 66, "ItemsTable", false);
            public static readonly Item Flecktarn = new("Flecktarn", 67, "ItemsTable", false);
            public static readonly Item Auscam = new("Auscam", 68, "ItemsTable", false);
            public static readonly Item Animals = new("Animals", 69, "ItemsTable", false);
            public static readonly Item Fly = new("Fly", 70, "ItemsTable", false);
            public static readonly Item Banana = new("Banana Camo", 71, "ItemsTable", false); // Works but no textures without the Banana Camo mod
            public static readonly Item Downloaded = new("Downloaded", 72, "ItemsTable", false);
            // This doesn't work to unlock the downloaded camos but it's in the table in IDA                                                                   


            #region Face Paint


            public static readonly Item NoPaint = new("No Paint", 73, "ItemsTable", false);
            public static readonly Item Woodland = new("Woodland", 74, "ItemsTable", false);
            public static readonly Item BlackFace = new("Black", 75, "ItemsTable", false);
            public static readonly Item WaterFace = new("Water", 76, "ItemsTable", false);
            public static readonly Item Desert = new("Desert", 77, "ItemsTable", false);
            public static readonly Item SplitterFace = new("Splitter", 78, "ItemsTable", false);
            public static readonly Item SnowFace = new("Snow", 79, "ItemsTable", false);
            public static readonly Item Kabuki = new("Kabuki", 80, "ItemsTable", false);
            public static readonly Item Zombie = new("Zombie", 81, "ItemsTable", false);
            public static readonly Item Oyama = new("Oyama", 82, "ItemsTable", false);
            public static readonly Item Mask = new("Mask", 83, "ItemsTable", false);
            public static readonly Item Green = new("Green", 84, "ItemsTable", false);
            public static readonly Item Brown = new("Brown", 85, "ItemsTable", false);
            public static readonly Item Infinity = new("Infinity", 86, "ItemsTable", false);
            public static readonly Item SovietUnion = new("Soviet Union", 87, "ItemsTable", false);
            public static readonly Item UK = new("UK", 88, "ItemsTable", false);
            public static readonly Item France = new("France", 89, "ItemsTable", false);
            public static readonly Item Germany = new("Germany", 90, "ItemsTable", false);
            public static readonly Item Italy = new("Italy", 91, "ItemsTable", false);
            public static readonly Item Spain = new("Spain", 92, "ItemsTable", false);
            public static readonly Item Sweden = new("Sweden", 93, "ItemsTable", false);
            public static readonly Item Japan = new("Japan", 94, "ItemsTable", false);
            public static readonly Item USA = new("USA", 95, "ItemsTable", false);

            #endregion
            #endregion
            #endregion
        }
    }