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

        // For HUD and Camera can we find the floats for the values and see if we can make a slider for them?
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
            Evasion = 128
        }
        public enum HealthType
        {
            CurrentHealth,
            MaxHealth,
            Stamina
        }
        public const string PROCESS_NAME = "METAL GEAR SOLID3";


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

        public class Hud : BaseMGS3Status
        {
            public static HudOptions HudOption = HudOptions.Normal;
            public static IntPtr HudStatusOffset { get; set; }

            Hud(string name, HudOptions hudOption) : base(name, (IntPtr)0xADB40F)
            {
                HudOption = hudOption;
                HudStatusOffset = MemoryOffset;
            }
        }

        public class Camera : BaseMGS3Status
        {
            public static CameraOptions CameraOption = CameraOptions.Normal;
            public static IntPtr CameraStatusOffset { get; set; }

            Camera(string name, CameraOptions cameraOption) : base(name, (IntPtr)0xAE3B37)
            {
                CameraOption = cameraOption;
                CameraStatusOffset = MemoryOffset;
            }
        }

        public class AlertStatus : BaseMGS3Status
        {
            public static AlertModes AlertLevel;
            public static IntPtr AlertStatusOffset { get; set; }

            AlertStatus(string name, AlertModes alertLevel) : base(name, (IntPtr)0x1D9C3D8)
            {
                AlertLevel = alertLevel;
                AlertStatusOffset = MemoryOffset;
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
        
    }
}
