using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGS3_MC_Cheat_Trainer
{
    #region Base classes
    public abstract class GameObject
    {
        protected static string _name = "";
        protected static IntPtr _memoryOffset;
    }

    public abstract class BaseMGS3Object : GameObject
    {
        public static string Name { get { return _name; } }
        public static IntPtr Memory { get { return _memoryOffset; }}
    }

    public class SuppressableWeapon : ClippedWeapon
    {
        public static IntPtr SuppressorToggleOffset;
        public static IntPtr SuppressorCapacityOffset;

        public SuppressableWeapon(string name, IntPtr ammoOffset, IntPtr maxAmmoOffset, IntPtr clipOffset, IntPtr maxClipOffset, IntPtr suppressorToggleOffset, IntPtr suppressorCapacityOffset)
            : base(name, ammoOffset, maxAmmoOffset, clipOffset, maxClipOffset)
        {
            SuppressorToggleOffset = suppressorToggleOffset;
            SuppressorCapacityOffset = suppressorCapacityOffset;
        }
    }

    public class ClippedWeapon : AmmoWeapon
    {
        public static IntPtr ClipOffset;
        public static IntPtr MaxClipOffset;

        public ClippedWeapon(string name, IntPtr ammoOffset, IntPtr maxAmmoOffset, IntPtr clipOffset, IntPtr maxClipOffset) 
            : base(name, ammoOffset, maxAmmoOffset)
        {
            ClipOffset = clipOffset;
            MaxClipOffset = maxClipOffset;
        }
    }

    public class AmmoWeapon : Weapon
    {
        public static IntPtr MaxAmmoOffset;

        public AmmoWeapon(string name, IntPtr ammoOffset, IntPtr maxAmmoOffset) 
            : base(name, ammoOffset)
        {
            MaxAmmoOffset = maxAmmoOffset;
        }
    }

    public class Weapon : BaseMGS3Object
    {
        public Weapon(string name, IntPtr ammoOffset)
        {
            _name = name;
            _memoryOffset = ammoOffset;
        }
    }

    public abstract class Item : BaseMGS3Object
    {
        public Item(string name, IntPtr memoryOffset)
        {
            _name = name;
            _memoryOffset = memoryOffset;
        }
    }

    public abstract class Camo : BaseMGS3Object
    {
        public Camo(string name, IntPtr memoryOffset)
        {
            _name = name;
            _memoryOffset = memoryOffset;
        }
    }
    #endregion

    public static class MGS3Objects
    {
        #region Weapons
        #region Suppressable Weapons
        public static readonly SuppressableWeapon MK22 = new("MK22", (IntPtr)0x1D435CC, (IntPtr)0x1D425DE, (IntPtr)0x1D435D0, (IntPtr)0x1D435D2, (IntPtr)0x1D435DC, (IntPtr)0x1D4695C);
        public static readonly SuppressableWeapon M1911A1 = new("M1911A1", (IntPtr)0x1D4361C, (IntPtr)0x1D4262E, (IntPtr)0x1D43620, (IntPtr)0x1D43622, (IntPtr)0x1D4362C, (IntPtr)0x1D4690C);
        public static readonly SuppressableWeapon XM16E1 = new("XM16E1", (IntPtr)0x1D437AC, (IntPtr)0x1D427BE, (IntPtr)0x1D437B0, (IntPtr)0x1D437B2, (IntPtr)0x1D437BC, (IntPtr)0x1D469AC);
        #endregion
        #region Ammo & Clip Weapons
        public static readonly ClippedWeapon AK47 = new("AK47", (IntPtr)0x1D437FC, (IntPtr)0x1D4280E, (IntPtr)0x1D43800, (IntPtr)0x1D43802);
        //TODO: the rest
        #endregion
        #region Ammo Only Weapons
        //TODO: add
        #endregion
        #region True/False Weapons
        //TODO: add
        #endregion
        #endregion
    }
}
