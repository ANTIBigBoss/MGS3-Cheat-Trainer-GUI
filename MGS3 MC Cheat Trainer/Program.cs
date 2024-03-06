namespace MGS3_MC_Cheat_Trainer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // Comment out main menu and uncomment debug form to run the debugger form at launch
            Application.Run(new MainMenuForm());
            //Application.Run(new DebugDevForm());
        }
    }
}

