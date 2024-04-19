using System.Diagnostics;

namespace MGS3_MC_Cheat_Trainer
{
    public class DebugFunctionManager
    {
        private static DebugFunctionManager instance;
        private static readonly object lockObj = new object();

        private DebugFunctionManager()
        {
        }

        public static DebugFunctionManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new DebugFunctionManager();
                    }

                    return instance;
                }
            }
        }

        #region Misc Form Functions
        // Report the memory address of the piss filter
        // Should show the 7F address and the METAL GEAR SOLID3+exe address
        // IntPtr targetAddress = IntPtr.Subtract(aobResult, 5525); is needed to get the address of the piss filter
        public IntPtr GetPissFilterAddress()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null) return IntPtr.Zero;

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            if (processHandle == IntPtr.Zero) return IntPtr.Zero;

            IntPtr aobResult = MemoryManager.Instance.FindAob("PissFilter");
            if (aobResult == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to find Piss Filter AOB pattern.");
                return IntPtr.Zero;
            }

            IntPtr targetAddress = IntPtr.Subtract(aobResult, 5525);

            return targetAddress;
        }


        #endregion

    }
}