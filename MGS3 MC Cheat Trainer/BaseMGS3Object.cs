using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MGS3_MC_Cheat_Trainer
{
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

    public class SuppressableWeapon : ClippedWeapon
    {
        public IntPtr SuppressorToggleOffset;
        public IntPtr SuppressorCapacityOffset;

        public SuppressableWeapon(string name, IntPtr ammoOffset, IntPtr maxAmmoOffset, IntPtr clipOffset, IntPtr maxClipOffset, IntPtr suppressorToggleOffset, IntPtr suppressorCapacityOffset)
            : base(name, ammoOffset, maxAmmoOffset, clipOffset, maxClipOffset)
        {
            SuppressorToggleOffset = suppressorToggleOffset;
            SuppressorCapacityOffset = suppressorCapacityOffset;
        }
    }

    public class ClippedWeapon : AmmoWeapon
    {
        public IntPtr ClipOffset;
        public IntPtr MaxClipOffset;

        public ClippedWeapon(string name, IntPtr ammoOffset, IntPtr maxAmmoOffset, IntPtr clipOffset, IntPtr maxClipOffset) 
            : base(name, ammoOffset, maxAmmoOffset)
        {
            ClipOffset = clipOffset;
            MaxClipOffset = maxClipOffset;
        }
    }

    public class AmmoWeapon : Weapon
    {
        public IntPtr MaxAmmoOffset;

        public AmmoWeapon(string name, IntPtr ammoOffset, IntPtr maxAmmoOffset) 
            : base(name, ammoOffset)
        {
            MaxAmmoOffset = maxAmmoOffset;
        }
    }

    public class Weapon : BaseMGS3Object
    {
        public Weapon(string name, IntPtr ammoOffset) : base(name, ammoOffset)
        {
        }
    }

    public class Item : BaseMGS3Object
    {
        public IntPtr MaxCapacityOffset;

        public Item(string name, IntPtr memoryOffset, IntPtr maxCapacityOffset = default) : base(name, memoryOffset)
        {
            MaxCapacityOffset = maxCapacityOffset;
        }
    }

    public abstract class Camo : BaseMGS3Object
    {
        public Camo(string name, IntPtr memoryOffset) : base(name, memoryOffset) { }
    }

    public class FacePaint : Camo
    {
        public FacePaint(string name, IntPtr memoryOffset) : base(name, memoryOffset) { }
    }
    public class Uniform : Camo
    {
        public Uniform(string name, IntPtr memoryOffset) : base(name, memoryOffset) { }
    }
    #endregion

    public class MGS3UsableObjects
    {
        #region Weapons
        #region Suppressable Weapons
        public static readonly SuppressableWeapon MK22 = new("MK22", (IntPtr)0x1D435CC, (IntPtr)0x1D425DE, (IntPtr)0x1D435D0, (IntPtr)0x1D435D2, (IntPtr)0x1D435DC, (IntPtr)0x1D4695C);
        public static readonly SuppressableWeapon M1911A1 = new("M1911A1", (IntPtr)0x1D4361C, (IntPtr)0x1D4262E, (IntPtr)0x1D43620, (IntPtr)0x1D43622, (IntPtr)0x1D4362C, (IntPtr)0x1D4690C);
        public static readonly SuppressableWeapon XM16E1 = new("XM16E1", (IntPtr)0x1D437AC, (IntPtr)0x1D427BE, (IntPtr)0x1D437B0, (IntPtr)0x1D437B2, (IntPtr)0x1D437BC, (IntPtr)0x1D469AC);
        #endregion
        #region Ammo & Clip Weapons
        public static readonly ClippedWeapon AK47 = new("AK47", (IntPtr)0x1D437FC, (IntPtr)0x1D4280E, (IntPtr)0x1D43800, (IntPtr)0x1D43802);
        public static readonly ClippedWeapon SVD = new("SVD", (IntPtr)0x1D438EC, (IntPtr)0x1D428FE, (IntPtr)0x1D438F0, (IntPtr)0x1D438F2);
        public static readonly ClippedWeapon M37 = new("M37", (IntPtr)0x1D4389C, (IntPtr)0x1D428AE, (IntPtr)0x1D438A0, (IntPtr)0x1D438A2);
        public static readonly ClippedWeapon RPG7 = new("RPG7", (IntPtr)0x1D4398C, (IntPtr)0x1D4299E, (IntPtr)0x1D43990, (IntPtr)0x1D43992);
        public static readonly ClippedWeapon M63 = new("M63", (IntPtr)0x1D4384C, (IntPtr)0x1D4285E, (IntPtr)0x1D43850, (IntPtr)0x1D43852);
        public static readonly ClippedWeapon Scorpion = new("Scorpion", (IntPtr)0x1D4375C, (IntPtr)0x1D4276E, (IntPtr)0x1D43760, (IntPtr)0x1D43762);
        public static readonly ClippedWeapon Mosin = new("Mosin", (IntPtr)0x1D4393C, (IntPtr)0x1D4294E, (IntPtr)0x1D43940, (IntPtr)0x1D43942);
        public static readonly ClippedWeapon SAA = new("SAA", (IntPtr)0x1D436BC, (IntPtr)0x1D426CE, (IntPtr)0x1D436C0, (IntPtr)0x1D436C2);
        #endregion
        #region Ammo Only Weapons
        public static readonly AmmoWeapon CigSpray = new("Cigspray", (IntPtr)0x1D4352C, (IntPtr)0x1D4253E);
        public static readonly AmmoWeapon Handkerchief = new("Handkerchief", (IntPtr)0x1D4357C, (IntPtr)0x1D4258E);
        public static readonly AmmoWeapon Grenade = new("Grenade", (IntPtr)0x1D43A2C, (IntPtr)0x1D42A3E);
        public static readonly AmmoWeapon WpGrenade = new("Wp Grenade", (IntPtr)0x1D43A7C, (IntPtr)0x1D42A8E);
        public static readonly AmmoWeapon ChaffGrenade = new("Chaff Grenade", (IntPtr)0x1D43B1C, (IntPtr)0x1D42B2E);
        public static readonly AmmoWeapon SmokeGrenade = new("Smoke Grenade", (IntPtr)0x1D43B6C, (IntPtr)0x1D42B7E);
        public static readonly AmmoWeapon StunGrenade = new("Stun Grenade", (IntPtr)0x1D43ACC, (IntPtr)0x1D42ADE);
        public static readonly AmmoWeapon EmptyMag = new("Empty Magazine", (IntPtr)0x1D43BBC, (IntPtr)0x1D42BCE);
        public static readonly AmmoWeapon Book = new("Book", (IntPtr)0x1D43CFC, (IntPtr)0x1D42D0E);
        public static readonly AmmoWeapon Claymore = new("Claymore", (IntPtr)0x1D43CAC, (IntPtr)0x1D42CBE);
        public static readonly AmmoWeapon TNT = new("TNT", (IntPtr)0x1D43C0C, (IntPtr)0x1D42C1E);
        public static readonly AmmoWeapon C3 = new("C3", (IntPtr)0x1D43C5C, (IntPtr)0x1D42C6E);
        public static readonly AmmoWeapon Mousetrap = new("Mousetrap", (IntPtr)0x1D43D4C, (IntPtr)0x1D42D5E);
        #endregion
        #region True/False Weapons
        public static readonly Weapon Patriot = new("Patriot", (IntPtr)0x1D4370C);
        public static readonly Weapon EzGun = new("Ez Gun", (IntPtr)0x1D4366C);
        public static readonly Weapon SurvivalKnife = new("Survival Knife", (IntPtr)0x1D4348C);
        public static readonly Weapon Fork = new("Fork", (IntPtr)0x1D434DC);
        public static readonly Weapon Torch = new("Torch", (IntPtr)0x1D439DC);
        public static readonly Weapon DirectionalMic = new("Directional Microphone", (IntPtr)0x1D43D9C);
        #endregion
        #endregion

        #region Items
        #region Backpack Items
        public static readonly Item LifeMed = new("LIFE MEDICINE", (IntPtr)0x1D45D7C, (IntPtr)0x1D45D7E);
        public static readonly Item BugJuice = new("BUG JUICE", (IntPtr)0x1D464AC, (IntPtr)0x1D464AE);
        public static readonly Item FakeDeathPill = new("FAKE DEATH PILL", (IntPtr)0x1D45E1C, (IntPtr)0x1D45E1E);
        public static readonly Item Pentazemin = new("PENTAZEMIN", (IntPtr)0x1D45DCC, (IntPtr)0x1D45DCE);
        #endregion
        #region Medicinal "L2" Items
        public static readonly Item Antidote = new("ANTIDOTE", (IntPtr)0x1D4659C, (IntPtr)0x1D4659E);
        public static readonly Item ColdMedicine = new("COLD MEDICINE", (IntPtr)0x1D465EC, (IntPtr)0x1D465EE);
        public static readonly Item DigestiveMedicine = new("DIGESTIVE MEDICINE", (IntPtr)0x1D4663C, (IntPtr)0x1D4663E);
        public static readonly Item Serum = new("SERUM", (IntPtr)0x1D4654C, (IntPtr)0x1D4654E);
        #endregion
        #region Surgical "R2" Items
        public static readonly Item Bandage = new("BANDAGE", (IntPtr)0x1D467CC, (IntPtr)0x1D467CE);
        public static readonly Item Disinfectant = new("DISINFECTANT", (IntPtr)0x1D4672C, (IntPtr)0x1D4672E);
        public static readonly Item Ointment = new("OINTMENT", (IntPtr)0x1D4668C, (IntPtr)0x1D4668E);
        public static readonly Item Splint = new("SPLINT", (IntPtr)0x1D466DC, (IntPtr)0x1D466DE);
        public static readonly Item Styptic = new("STYPTIC", (IntPtr)0x1D4677C, (IntPtr)0x1D4677E);
        public static readonly Item SutureKit = new("SUTURE KIT", (IntPtr)0x1D4681C, (IntPtr)0x1D4681E);
        #region Boolean Items
        // No Capacity, will just have a true 1 or false -1 to give/remove
        public static readonly Item RevivalPill = new("REVIVAL PILL", (IntPtr)0x1D45E6C);
        public static readonly Item Cigar = new("CIGAR", (IntPtr)0x1D45EBC);
        public static readonly Item Binoculars = new("BINOCULARS", (IntPtr)0x1D45F0C);
        public static readonly Item ThermalGoggles = new("THERMAL GOGGLES", (IntPtr)0x1D45F5C);
        public static readonly Item NightVisionGoggles = new("NIGHT VISION GOGGLES", (IntPtr)0x1D45FAC);
        public static readonly Item Camera = new("CAMERA", (IntPtr)0x1D45FFC);
        public static readonly Item MotionDetector = new("MOTION DETECTOR", (IntPtr)0x1D4604C);
        public static readonly Item ActiveSonar = new("ACTIVE SONAR", (IntPtr)0x1D4609C);
        public static readonly Item MineDetector = new("MINE DETECTOR", (IntPtr)0x1D460EC);
        public static readonly Item AntiPersonnelSensor = new("ANTI PERSONNEL SENSOR", (IntPtr)0x1D4613C);
        //Boxes have slightly different logic and should be set to 25 or -1 as they have a durability
        public static readonly Item CBoxA = new("CBOX A", (IntPtr)0x1D4618C);
        public static readonly Item CBoxB = new("CBOX B", (IntPtr)0x1D461DC);
        public static readonly Item CBoxC = new("CBOX C", (IntPtr)0x1D4622C);
        public static readonly Item CBoxD = new("CBOX D", (IntPtr)0x1D4627C);
        public static readonly Item CrocCap = new("CROC CAP", (IntPtr)0x1D462CC);
        public static readonly Item KeyA = new("KEY A", (IntPtr)0x1D4631C);
        public static readonly Item KeyB = new("KEY B", (IntPtr)0x1D4636C);
        public static readonly Item KeyC = new("KEY C", (IntPtr)0x1D463BC);
        public static readonly Item Bandana = new("BANDANA", (IntPtr)0x1D4640C);
        public static readonly Item StealthCamo = new("STEALTH CAMO", (IntPtr)0x1D4645C);
        public static readonly Item MonkeyMask = new("MONKEY MASK", (IntPtr)0x1D464FC);
        #endregion
        #endregion
        #endregion

        #region Camos
        #region Face Paint
        public static readonly FacePaint Woodland = new("Woodland", (IntPtr)0x1D4749C);
        public static readonly FacePaint BlackFace = new("Black", (IntPtr)0x1D474EC);
        public static readonly FacePaint WaterFace = new("Water", (IntPtr)0x1D4753C);
        public static readonly FacePaint Desert = new("Desert", (IntPtr)0x1D4758C);
        public static readonly FacePaint SplitterFace = new("Splitter", (IntPtr)0x1D475DC);
        public static readonly FacePaint SnowFace = new("Snow", (IntPtr)0x1D4762C);
        public static readonly FacePaint Kabuki = new("Kabuki", (IntPtr)0x1D4767C);
        public static readonly FacePaint Zombie = new("Zombie", (IntPtr)0x1D476CC);
        public static readonly FacePaint Oyama = new("Oyama", (IntPtr)0x1D4771C);
        public static readonly FacePaint Mask = new("Mask", (IntPtr)0x1D4776C);
        public static readonly FacePaint Green = new("Green", (IntPtr)0x1D477BC);
        public static readonly FacePaint Brown = new("Brown", (IntPtr)0x1D4780C);
        public static readonly FacePaint Infinity = new("Infinity", (IntPtr)0x1D4785C);
        public static readonly FacePaint SovietUnion = new("Soviet Union", (IntPtr)0x1D478AC);
        public static readonly FacePaint UK = new("UK", (IntPtr)0x1D478FC);
        public static readonly FacePaint France = new("France", (IntPtr)0x1D4794C);
        public static readonly FacePaint Germany = new("Germany", (IntPtr)0x1D4799C);
        public static readonly FacePaint Italy = new("Italy", (IntPtr)0x1D479EC);
        public static readonly FacePaint Spain = new("Spain", (IntPtr)0x1D47A3C);
        public static readonly FacePaint Sweden = new("Sweden", (IntPtr)0x1D47A8C);
        public static readonly FacePaint Japan = new("Japan", (IntPtr)0x1D47ADC);
        public static readonly FacePaint USA = new("USA", (IntPtr)0x1D47B2C);
        #endregion
        #region Uniform
        public static readonly Uniform OliveDrab = new("Olive Drab", (IntPtr)0x1D469FC);
        public static readonly Uniform TigerStripe = new("Tiger Stripe", (IntPtr)0x1D46A4C);
        public static readonly Uniform Leaf = new("Leaf", (IntPtr)0x1D46A9C);
        public static readonly Uniform TreeBark = new("Tree Bark", (IntPtr)0x1D46AEC);
        public static readonly Uniform ChocoChip = new("Choco Chip", (IntPtr)0x1D46B3C);
        public static readonly Uniform Splitter = new("Splitter", (IntPtr)0x1D46B8C);
        public static readonly Uniform Raindrop = new("Raindrop", (IntPtr)0x1D46BDC);
        public static readonly Uniform Squares = new("Squares", (IntPtr)0x1D46C2C);
        public static readonly Uniform Water = new("Water", (IntPtr)0x1D46C7C);
        public static readonly Uniform Black = new("Black", (IntPtr)0x1D46CCC);
        public static readonly Uniform Snow = new("Snow", (IntPtr)0x1D46D1C);
        public static readonly Uniform SneakingSuit = new("Sneaking Suit", (IntPtr)0x1D46DBC);
        public static readonly Uniform Scientist = new("Scientist", (IntPtr)0x1D46E0C);
        public static readonly Uniform Officer = new("Officer", (IntPtr)0x1D46E5C);
        public static readonly Uniform Maintenance = new("Maintenance", (IntPtr)0x1D46EAC);
        public static readonly Uniform Tuxedo = new("Tuxedo", (IntPtr)0x1D46EFC);
        public static readonly Uniform HornetStripe = new("Hornet Stripe", (IntPtr)0x1D46F4C);
        public static readonly Uniform Spider = new("Spider", (IntPtr)0x1D46F9C);
        public static readonly Uniform Moss = new("Moss", (IntPtr)0x1D46FEC);
        public static readonly Uniform Fire = new("Fire", (IntPtr)0x1D4703C);
        public static readonly Uniform Spirit = new("Spirit", (IntPtr)0x1D4708C);
        public static readonly Uniform ColdWar = new("Cold War", (IntPtr)0x1D470DC);
        public static readonly Uniform Snake = new("Snake", (IntPtr)0x1D4712C);
        public static readonly Uniform GaKo = new("Ga-Ko", (IntPtr)0x1D4717C);
        public static readonly Uniform DesertTiuger = new("DesertTiger", (IntPtr)0x1D471CC);
        public static readonly Uniform DPM = new("DPM", (IntPtr)0x1D4721C);
        public static readonly Uniform Flecktarn = new("Flecktarn", (IntPtr)0x1D4726C);
        public static readonly Uniform Auscam = new("Auscam", (IntPtr)0x1D472BC);
        public static readonly Uniform Animals = new("Animals", (IntPtr)0x1D4730C);
        public static readonly Uniform Fly = new("Fly", (IntPtr)0x1D4735C);
        public static readonly Uniform Banana = new("Banana Camo", (IntPtr)0x1D473AC); //(to be checked)
        #endregion
        #endregion
    }
}
