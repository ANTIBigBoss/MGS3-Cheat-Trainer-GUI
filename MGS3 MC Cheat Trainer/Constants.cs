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
        public enum HudOptions
        {
            None = 0,
            Normal = 63,
            Shrunk = 64
        }
        public enum CameraOptions
        {
            Normal = 191,
            UpsideDown = 64
        }
        public enum AlertModes
        {
            Alert = 16,
            Caution = 32,
            Evasion = 128 // Doesn't trigger evasion but this is the value for it
        }
        public enum HealthType
        {
            CurrentHealth,
            MaxHealth,
            Stamina
            // Not sure if the max stamina exists or not or is modifiable
        }

        public const string PROCESS_NAME = "METAL GEAR SOLID3";
        internal const int HealthPointerOffset = 0x00AE49D8;
        internal const int CurrentHealthOffset = 0x684;
        internal const int MaxHealthOffset = 0x686;
        internal const int StaminaOffset = 0xA4A;
        internal static IntPtr HudOffset = (IntPtr)0xADB40F;
        internal static IntPtr CamOffset = (IntPtr)0xAE3B37;
        internal static IntPtr AlertStatusOffset = (IntPtr)0x1D9C3D8;


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
            public static readonly SnakeAnimation LongSleep = new("LongSleep", (IntPtr)0x1D4BCBA, 1);
            public static readonly SnakeAnimation QuickSleep = new("QuickSleep", (IntPtr)0x1E2C0BB, 2);
            public static readonly SnakeAnimation Puke = new("Puke", (IntPtr)0x1E2C0BC, 1);
            public static readonly SnakeAnimation OnFire = new("OnFire", (IntPtr)0x1E2C0BC, 200);
            public static readonly SnakeAnimation OnFirePuke = new("OnFirePuke", (IntPtr)0x1E2C0BC, 255);
            public static readonly SnakeAnimation BunnyHop = new("BunnyHop", (IntPtr)0x1E2C0C8, 3);
            public static readonly SnakeAnimation FakeDeath = new("FakeDeath", (IntPtr)0x1E2C0CA, 32);
        }

        public class MGS3AlertTimers : BaseMGS3Status
        {
            public IntPtr AlertTimerOffset;
            public byte Value { get; set; }

            public MGS3AlertTimers(string name, IntPtr memoryOffset) : base(name, memoryOffset)
            {
                AlertTimerOffset = memoryOffset;
            }
        }
        public class MGS3AlertModes
        {
            public static readonly MGS3AlertTimers Alert = new("Alert", (IntPtr)0x1D9C384);
            public static readonly MGS3AlertTimers Evasion = new("Evasion", (IntPtr)0x1D9C39C);
            public static readonly MGS3AlertTimers Caution = new("Caution", (IntPtr)0x1E36A5C);
            
        }

        
        
    }
}
