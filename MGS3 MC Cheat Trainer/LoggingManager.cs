using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

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
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            if (string.IsNullOrEmpty(assemblyLocation))
            {
                // Fallback to a default path, such as the current directory
                logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            }
            else
            {
                logFolderPath = Path.Combine(Path.GetDirectoryName(assemblyLocation) ?? "", "Logs");
            }
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