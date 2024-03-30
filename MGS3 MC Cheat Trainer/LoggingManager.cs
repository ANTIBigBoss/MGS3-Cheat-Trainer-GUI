using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    internal class LoggingManager
    {
        private static LoggingManager instance;
        private static readonly object padlock = new object();
        private static string logFolderPath;
        private static string logFileName = "MGS3_MC_Cheat_Trainer_Log.txt";
        private static string logPath;

        // Static constructor to initialize logFolderPath and logPath
        static LoggingManager()
        {
            // Use the Documents folder for storing logs, with a specific subfolder for the application
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string appLogFolder = "MGS3 CT Logs"; // Specific name for the log folder

            logFolderPath = Path.Combine(documentsFolder, appLogFolder);
            logPath = Path.Combine(logFolderPath, logFileName);

            EnsureLogFileExists();
        }

        private LoggingManager()
        {
            // Private constructor to prevent instance creation outside of this class
        }

        public static LoggingManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LoggingManager();
                    }

                    return instance;
                }
            }
        }

        private static void EnsureLogFileExists()
        {
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            if (!File.Exists(logPath))
            {
                using (var stream = File.Create(logPath))
                {
                    // Immediately close the stream to release the file
                }
            }
        }

        public void Log(string message)
        {
            try
            {
                using (var writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while trying to log: {ex.Message}");
            }
        }

        // This is mostly to help me stay ahead of game updates so I can log the offset differences quicker if anything gets shifted around
        // Currently only logs the AOB addresses, but could be expanded to log other addresses as well
        public void LogAOBAddresses()
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                LoggingManager.Instance.Log("Process not found.");
                return;
            }

            IntPtr processHandle = OpenGameProcess(process);
            if (processHandle == IntPtr.Zero)
            {
                LoggingManager.Instance.Log("Failed to open process.");
                return;
            }

            IntPtr baseAddress = process.MainModule.BaseAddress;
            long moduleSize = process.MainModule.ModuleMemorySize;

            foreach (var aobEntry in AobManager.AOBs)
            {
                string name = aobEntry.Key;
                if (string.IsNullOrEmpty(name)) continue; // Skip empty placeholder entries

                byte[] pattern = aobEntry.Value.Pattern;
                string mask = aobEntry.Value.Mask;

                var foundAddresses =
                    MemoryManager.Instance.ScanForAllAobInstances(processHandle, baseAddress, moduleSize, pattern,
                        mask);
                if (foundAddresses.Count > 0)
                {
                    foreach (var address in foundAddresses)
                    {
                        long offset = address.ToInt64() - baseAddress.ToInt64();
                        // Read the AOB bytes from the found address
                        byte[] aobBytes = new byte[pattern.Length];
                        if (NativeMethods.ReadProcessMemory(processHandle, address, aobBytes, (uint)aobBytes.Length,
                                out _))
                        {
                            string aobHexString = BitConverter.ToString(aobBytes).Replace("-", " ");
                            LoggingManager.Instance.Log(
                                $"{name}: Instance found at: {address.ToString("X")}, METAL GEAR SOLID3.exe+{offset:X}, AOB: {aobHexString}");
                        }
                        else
                        {
                            LoggingManager.Instance.Log(
                                $"{name}: Instance found at: {address.ToString("X")}, METAL GEAR SOLID3.exe+{offset:X}, but failed to read AOB bytes.");
                        }
                    }
                }
                else
                {
                    LoggingManager.Instance.Log($"{name}: AOB not found.");
                }
            }

            NativeMethods.CloseHandle(processHandle);
        }
    }
}


/* Button to implement later to locate the log file/folder

   private void btnOpenLogFolder_Click(object sender, EventArgs e)
   {
       // Use the Process.Start method to open the log folder in File Explorer
       try
       {
           Process.Start(new ProcessStartInfo
           {
               FileName = LoggingManager.LogFolderPath,
               UseShellExecute = true,
               Verb = "open"
           });
       }
       catch (Exception ex)
       {
           MessageBox.Show($"Failed to open log folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }
   }
*/