using System.Diagnostics;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public class HelperFunctions
    {
        private static HelperFunctions instance;
        private static readonly object lockObj = new object();

        private HelperFunctions()
        {
        }

        public static HelperFunctions Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new HelperFunctions();
                    }

                    return instance;
                }
            }
        }

        #region Helpers

        public IntPtr GetProcessHandle()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null) return IntPtr.Zero;

            return MemoryManager.OpenGameProcess(process);
        }

        public IntPtr GetTargetAddress(IntPtr processHandle, string patternName, int offset)
        {
            IntPtr address = MemoryManager.Instance.FindAob(patternName);
            if (address == IntPtr.Zero) return IntPtr.Zero;

            return IntPtr.Subtract(address, offset);
        }

        public bool VerifyMemory(IntPtr processHandle, IntPtr address, byte[] expectedBytes)
        {
            byte[] buffer = new byte[expectedBytes.Length];
            if (MemoryManager.ReadProcessMemory(processHandle, address, buffer, (uint)expectedBytes.Length, out _))
            {
                return buffer.SequenceEqual(expectedBytes);
            }

            return false;
        }

        public bool WriteMemory(IntPtr processHandle, IntPtr address, byte[] bytes)
        {
            return MemoryManager.WriteMemory(processHandle, address, bytes);
        }

        #endregion

    }
}