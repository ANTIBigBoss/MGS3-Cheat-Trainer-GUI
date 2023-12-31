using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            const int WeaponOffset = 80; // The offset between weapons
            const int MaxAmmoOffset = 2; // The offset for the max ammo
            const int ClipOffset = 4;    // The offset for the clip
            const int MaxClipOffset = 6; // The offset for the max clip
            const int SuppressorToggleOffset = 16; // The offset for the suppressor toggle
                                                   // Suppressor capacity is actually considered an item and not a weapon

            public static IntPtr GetAddress(int index)
            {
                /* 
                Starting address for Survival Knife i.e first weapon in backpack
                the last "weapon" is the Directional Microphone but I think the table 
                goes down 19 more addresses for the food items that in the backpack are 
                also considered weapons 3 live (Caged) foods and 16 non-living foods 
                */
                const int StartingAddress = 0x1D4C78C;
                return (IntPtr)(StartingAddress + WeaponOffset * index);

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
        const int ItemOffset = 80; // The offset between items
        const int MaxOffset = 2;   // The offset for the max value

        public static IntPtr GetAddress(int index)
        {
            // Starting address for Life Medicine i.e first item in backpack
            const int StartingAddress = 0x1D4F07C;
            return (IntPtr)(StartingAddress + ItemOffset * index);
            // For reference the last item is the USA face paint
        }

        public static IntPtr GetMaxAddress(IntPtr baseAddress)
        {
            return baseAddress + MaxOffset;
        }
    }

    public class Item : BaseMGS3Object
    {
        public IntPtr MaxCapacityOffset { get; private set; }

        // Constructor for items without a max capacity
        public Item(string name, IntPtr memoryOffset) : base(name, memoryOffset)
        {
            MaxCapacityOffset = IntPtr.Zero; // No max capacity
        }

        // Constructor for items with a max capacity
        public Item(string name, int index, bool hasMaxCapacity = false) : base(name, ItemAddresses.GetAddress(index))
        {
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

        // Constructor for a weapon
        public Weapon(string name, int index, bool hasAmmo = false, bool hasClip = false, bool hasSuppressorToggle = false)
            : base(name, WeaponAddresses.GetAddress(index))
        {
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
        I think we are missing surpressor toggle which is 16 bytes after the main index
        */
        #region Weapons
        public static readonly Weapon SurvivalKnife = new ("Survival Knife", 0, false, false, false);
        public static readonly Weapon Fork = new ("Fork", 1, false, false, false);
        public static readonly Weapon CigSpray = new ("Cigspray", 2, true, false, false); // Has ammo but no clip or suppressor
        public static readonly Weapon Handkerchief = new("Handkerchief", 3, true, false, false);
        public static readonly Weapon MK22 = new("MK22", 4, true, true, true); // I think missing MK22's suppressor toggle
        public static readonly Weapon M1911A1 = new("M1911A1", 5, true, true, true);
        public static readonly Weapon EzGun = new("Ez Gun", 6, false, false, false);
        public static readonly Weapon SAA = new("SAA", 7, true, true, false); // Has ammo and clips but no suppressor
        public static readonly Weapon Patriot = new("Patriot", 8, false, false, false);
        public static readonly Weapon Scorpion = new("Scorpion", 9, true, true, false);
        public static readonly Weapon XM16E1 = new("XM16E1", 10, true, true, true);
        public static readonly Weapon AK47 = new("AK47", 11, true, true, false);
        public static readonly Weapon M63 = new("M63", 12, true, true, false);
        public static readonly Weapon M37 = new("M37", 13, true, true, false);
        public static readonly Weapon SVD = new("SVD", 14, true, true, false);
        public static readonly Weapon Mosin = new("Mosin", 15, true, true, false);
        public static readonly Weapon RPG7 = new("RPG7", 16, true, true, false);
        public static readonly Weapon Torch = new("Torch", 17, false, false, false);
        public static readonly Weapon Grenade = new("Grenade", 18, true, false, false);
        public static readonly Weapon WpGrenade = new("Wp Grenade", 19, true, false, false);
        public static readonly Weapon StunGrenade = new("Stun Grenade", 20, true, false, false);
        public static readonly Weapon ChaffGrenade = new("Chaff Grenade", 21, true, false, false);
        public static readonly Weapon SmokeGrenade = new("Smoke Grenade", 22, true, false, false);
        public static readonly Weapon EmptyMag = new("Empty Magazine", 23, true, false, false);
        public static readonly Weapon TNT = new("TNT", 24, true, false, false);
        public static readonly Weapon C3 = new("C3", 25, true, false, false);
        public static readonly Weapon Claymore = new("Claymore", 26, true, false, false);
        public static readonly Weapon Book = new("Book", 27, true, false, false);
        public static readonly Weapon Mousetrap = new("Mousetrap", 28, true, false, false);
        public static readonly Weapon DirectionalMic = new("Directional Microphone", 29, false, false, false);
        // When I learn more on how to force certain food items to be in the 19 slots affter will implement logic here
        #endregion

        #region Items
        #region Backpack Items
        public static readonly Item LifeMed = new("LIFE MEDICINE", 0, true);
        public static readonly Item Pentazemin = new("PENTAZEMIN", 1, true);
        public static readonly Item FakeDeathPill = new("FAKE DEATH PILL", 2, true);
        // No Capacity, will just have a true 1 or false -1 to give/remove
        public static readonly Item RevivalPill = new("REVIVAL PILL", 3, false);
        public static readonly Item Cigar = new("CIGAR", 4, false);
        public static readonly Item Binoculars = new("BINOCULARS", 5, false);
        public static readonly Item ThermalGoggles = new("THERMAL GOGGLES", 6, false);
        public static readonly Item NightVisionGoggles = new("NIGHT VISION GOGGLES", 7, false);
        public static readonly Item Camera = new("CAMERA", 8, false);
        public static readonly Item MotionDetector = new("MOTION DETECTOR", 9, false);
        public static readonly Item ActiveSonar = new("ACTIVE SONAR", 10, false);
        public static readonly Item MineDetector = new("MINE DETECTOR", 11, false);
        public static readonly Item AntiPersonnelSensor = new("ANTI PERSONNEL SENSOR", 12, false);
        //Boxes have slightly different logic and should be set to 25 or -1 as they have a durability
        public static readonly Item CBoxA = new("CBOX A", 13, false);
        public static readonly Item CBoxB = new("CBOX B", 14, false);
        public static readonly Item CBoxC = new("CBOX C", 15, false);
        public static readonly Item CBoxD = new("CBOX D", 16, false);
        public static readonly Item CrocCap = new("CROC CAP", 17, false);
        public static readonly Item KeyA = new("KEY A", 18, false);
        public static readonly Item KeyB = new("KEY B", 19, false);
        public static readonly Item KeyC = new("KEY C", 20, false);
        public static readonly Item Bandana = new("BANDANA", 21, false);
        public static readonly Item StealthCamo = new("STEALTH CAMO", 22, false);
        public static readonly Item BugJuice = new("BUG JUICE", 23, true);
        public static readonly Item MonkeyMask = new("MONKEY MASK", 24, false);
        #endregion
        #endregion

        #region Medicinal "L2" Items
        public static readonly Item Serum = new("SERUM", 25, true);
        public static readonly Item Antidote = new("ANTIDOTE", 26, true);
        public static readonly Item ColdMedicine = new("COLD MEDICINE", 27, true);
        public static readonly Item DigestiveMedicine = new("DIGESTIVE MEDICINE", 28, true);
        #endregion
        #region Surgical "R2" Items
        public static readonly Item Ointment = new("OINTMENT", 29, true);
        public static readonly Item Splint = new("SPLINT", 30, true);
        public static readonly Item Disinfectant = new("DISINFECTANT", 31, true);
        public static readonly Item Styptic = new("STYPTIC", 32, true);
        public static readonly Item Bandage = new("BANDAGE", 33, true);
        public static readonly Item SutureKit = new("SUTURE KIT", 34, true);
        #endregion

        // 35,36,37,38, 39 are other items to be implemented in this part of code but
        // camos are preceding them in the pattern so this code is a placeholder until
        // I put in 35-39 i.e. Knife, Battery and Surpressor quantities

        #region Other Items
        // Battery may have a max capacity and need to be changed to true unless address is max capacity
        // Was never able to find a static address for current battery in Cheat Engine
        public static readonly Item Knife = new("KNIFE", 35, false); // No use but listing for completeness of table
        public static readonly Item Battery = new("BATTERY", 36, false); // Need to look into exactly what this controls
        // Need to shift around logic for suppressor toggle since I think somewhere in code still thinks it's a weapon
        public static readonly Item M1911A1Surpressor = new("M1911A1 SURPRESSOR", 37, false);
        public static readonly Item MK22Surpressor = new("MK22 SURPRESSOR", 38, false);
        public static readonly Item XM16E1Surpressor = new("XM16E1 SURPRESSOR", 39, false);
        #endregion

        #region Camos

        #region Uniform
        // Moved uniform addresses above face paint to align better with the data structure MGS3 has

        // Naked camo for some reason is just not in the table if for some reason we want to add it
        
        public static readonly Item OliveDrab = new("Olive Drab", 40, false);
        public static readonly Item TigerStripe = new("Tiger Stripe", 41, false);
        public static readonly Item Leaf = new("Leaf", 42, false);
        public static readonly Item TreeBark = new("Tree Bark", 43, false);
        public static readonly Item ChocoChip = new("Choco Chip", 44, false);
        public static readonly Item Splitter = new("Splitter", 45, false);
        public static readonly Item Raindrop = new("Raindrop", 46, false);
        public static readonly Item Squares = new("Squares", 47, false);
        public static readonly Item Water = new("Water", 48, false);
        public static readonly Item Black = new("Black", 49, false);
        public static readonly Item Snow = new("Snow", 50, false);
        public static readonly Item Naked = new("Naked", 51, false);
        public static readonly Item SneakingSuit = new("Sneaking Suit", 52, false);
        public static readonly Item Scientist = new("Scientist", 53, false);
        public static readonly Item Officer = new("Officer", 54, false);
        public static readonly Item Maintenance = new("Maintenance", 55, false);
        public static readonly Item Tuxedo = new("Tuxedo", 56, false);
        public static readonly Item HornetStripe = new("Hornet Stripe", 57, false);
        public static readonly Item Spider = new("Spider", 58, false);
        public static readonly Item Moss = new("Moss", 59, false);
        public static readonly Item Fire = new("Fire", 60, false);
        public static readonly Item Spirit = new("Spirit", 61, false);
        public static readonly Item ColdWar = new("Cold War", 62, false);
        public static readonly Item Snake = new("Snake", 63, false);
        public static readonly Item GaKo = new("Ga-Ko", 64, false);
        public static readonly Item DesertTiger = new("DesertTiger", 65, false);
        public static readonly Item DPM = new("DPM", 66, false);
        public static readonly Item Flecktarn = new("Flecktarn", 67, false);
        public static readonly Item Auscam = new("Auscam", 68, false);
        public static readonly Item Animals = new("Animals", 69, false);
        public static readonly Item Fly = new("Fly", 70, false);
        public static readonly Item Banana = new("Banana Camo", 71, false); // Works but no textures without the Banana Camo mod
        public static readonly Item Downloaded = new("Downloaded", 72, false);
        // Downloaded appears as one entry in the table so this should in theory unlock all downloaded camos                                                                    


        #region Face Paint


        public static readonly Item NoPaint = new("No Paint", 73, false);
        public static readonly Item Woodland = new("Woodland", 74, false);
        public static readonly Item BlackFace = new("Black", 75, false);
        public static readonly Item WaterFace = new("Water", 76, false);
        public static readonly Item Desert = new("Desert", 77, false);
        public static readonly Item SplitterFace = new("Splitter", 78, false);
        public static readonly Item SnowFace = new("Snow", 79, false);
        public static readonly Item Kabuki = new("Kabuki", 80, false);
        public static readonly Item Zombie = new("Zombie", 81, false);
        public static readonly Item Oyama = new("Oyama", 82, false);
        public static readonly Item Mask = new("Mask", 83, false);
        public static readonly Item Green = new("Green", 84, false);
        public static readonly Item Brown = new("Brown", 85, false);
        public static readonly Item Infinity = new("Infinity", 86, false);
        public static readonly Item SovietUnion = new("Soviet Union", 87, false);
        public static readonly Item UK = new("UK", 88, false);
        public static readonly Item France = new("France", 89, false);
        public static readonly Item Germany = new("Germany", 90, false);
        public static readonly Item Italy = new("Italy", 91, false);
        public static readonly Item Spain = new("Spain", 92, false);
        public static readonly Item Sweden = new("Sweden", 93, false);
        public static readonly Item Japan = new("Japan", 94, false);
        public static readonly Item USA = new("USA", 95, false);

        #endregion


        #endregion
        #endregion
    }
}
