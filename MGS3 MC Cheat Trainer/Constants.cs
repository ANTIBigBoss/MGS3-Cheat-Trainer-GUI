using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MGS3_MC_Cheat_Trainer
{
    public class Constants
    {
        public class GameStatus
        {
            internal IntPtr _memoryOffset;
        }

        public interface IMGS3Status
        {
            private static string name = "";
            public static string Name { get { return name; } }
        }

        public abstract class BaseMGS3Status : IMGS3Status
        {
            protected GameStatus gameStatus { get; set; }
            public string Name { get; set; }
            public IntPtr MemoryOffset { get { return gameStatus._memoryOffset; } }

            public BaseMGS3Status(string name, IntPtr memoryOffset)
            {
                Name = name;
                gameStatus = new GameStatus { _memoryOffset = memoryOffset };
            }
        }

        // Should go and find the float values for HUD and Camera for future use and make it a slider
        public enum HudOptions // 0xADB40F options
        {
            None = 0,
            Normal = 63,
            Shrunk = 64
        }
        public enum CameraOptions // 0xAE3B37 options
        {
            Normal = 191,
            UpsideDown = 64
        }
        public enum AlertModes // 0x1D9C3D8 options
        {
            Normal = 0,
            Alert = 16,
            Caution = 32,
            Evasion = 128 // Doesn't trigger evasion but this is the value for it
        }
        public enum HealthType // Can probably use these enums for boss/eva's stats
        {
            CurrentHealth,
            MaxHealth,
            Stamina
            // Not sure if the max stamina exists or not or is modifiable
        }

        public const string PROCESS_NAME = "METAL GEAR SOLID3";
        internal const int HealthPointerOffset = 0x00AEC9D8;
        internal const int CurrentHealthOffset = 0x684;
        internal const int MaxHealthOffset = 0x686;
        internal const int StaminaOffset = 0xA4A;
        internal static IntPtr HudOffset = (IntPtr)0xAE345F;
        internal static IntPtr CamOffset = (IntPtr)0xAE3B37;
        internal static IntPtr AlertStatusOffset = (IntPtr)0x1DA5848;

        // Cobra Unit and Bosses Health and Stamina


        public class SnakeAnimation : BaseMGS3Status
        {
            public IntPtr AnimationOffset;
            public byte Value { get; set; }

            public SnakeAnimation(string name, IntPtr memoryOffset, byte value): base(name, memoryOffset)
            {
                AnimationOffset = memoryOffset;
                Value = value;
            }
        }

        public class MGS3SnakeAnimations
        {
            public static readonly SnakeAnimation LongSleep = new("LongSleep", (IntPtr)0x1D54FBA, 1);
            public static readonly SnakeAnimation QuickSleep = new("QuickSleep", (IntPtr)0x1E3552B, 2);
            public static readonly SnakeAnimation Puke = new("Puke", (IntPtr)0x1E3552C, 1);
            public static readonly SnakeAnimation OnFire = new("OnFire", (IntPtr)0x1E3552C, 200);
            public static readonly SnakeAnimation OnFirePuke = new("OnFirePuke", (IntPtr)0x1E3552C, 255);
            public static readonly SnakeAnimation BunnyHop = new("BunnyHop", (IntPtr)0x1E35538, 3);
            public static readonly SnakeAnimation FakeDeath = new("FakeDeath", (IntPtr)0x1E3553A, 32);
        }

        public class MGS3Distortion : BaseMGS3Status
        {
            public IntPtr ModelManipulationOffset;
            public byte Value { get; set; }

            public MGS3Distortion(string name, IntPtr memoryOffset, byte value) : base(name, memoryOffset)
            {
                ModelManipulationOffset = memoryOffset;
                Value = value;
            }
        }

        public class MGS3DistortionEffects
        { 
            public static readonly MGS3Distortion Normal = new("Normal", (IntPtr)0x9E79F, 40);
        }

        public class MGS3AlertTimers : BaseMGS3Status
        {
            public IntPtr AlertTimerOffset;
            public short Value { get; set; }

            public MGS3AlertTimers(string name, IntPtr memoryOffset) : base(name, memoryOffset)
            {
                AlertTimerOffset = memoryOffset;
            }
        }
        public class MGS3AlertModes 
        {
            public static readonly MGS3AlertTimers Alert = new("Alert", (IntPtr)0x1DA57F4);
            public static readonly MGS3AlertTimers Evasion = new("Evasion", (IntPtr)0x1DA580C);
            // Caution is currently just a read only, writing to it has done nothing so far
            // Might have a jank way to write to it with messing around twith the evasion timer
            public static readonly MGS3AlertTimers Caution = new("Caution", (IntPtr)0x1DA57F8);
        }
    }
}