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
        private static string logFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs");
        private static string logFileName = "MGS3_MC_Cheat_Trainer_Log.txt";
        private static string logPath = Path.Combine(logFolderPath, logFileName);

        private LoggingManager()
        {
            EnsureLogFileExists(); // Ensure the log folder and file exist when the instance is created
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

        private void EnsureLogFileExists()
        {
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }
            if (!File.Exists(logPath))
            {
                // Create the file or do nothing if it already exists
                File.Create(logPath).Dispose();
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
                Debug.WriteLine($"\nAn error occurred while trying to log: {ex.Message}");
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