using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    internal class RandomizerManager
    {
        private static RandomizerManager instance;
        private static readonly object lockObj = new object();

        // Property to store the randomization type
        public string RandomizationType { get; set; }

        private RandomizerManager()
        {
            // Default to empty or guards, based on your initial use case
            RandomizationType = "guards";
        }

        public static RandomizerManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new RandomizerManager();
                    }

                    return instance;
                }
            }
        }

        // This method checks which area to randomize
        public void SearchForRandomizerArea()
        {
            string locationString = StringManager.Instance.GetCurrentMapLocation();

            // Make a switch statement for the result string
            switch (locationString)
            {
                case "v001a": // Dremuchji South - Virtuous Mission
                              // We'll decide player starting gear here
                    break;

                case "v003a": // Dremuchji Swampland - Virtuous Mission
                              // Not much to do here until time of day randomness is added
                    break;

                case "v004a": // Dremuchji North - Virtuous Mission
                    if (RandomizationType == "guards")
                    {
                        XyzManager.Instance.RandomizeGuardLocations(locationString);
                    }
                    break;

                case "v005a": // Dolinovodno Rope Bridge - Virtuous Mission
                    if (RandomizationType == "guards")
                    {
                        XyzManager.Instance.RandomizeGuardLocations(locationString);
                    }
                    break;

                case "v006a": // Rassvet - Virtuous Mission
                    if (RandomizationType == "guards")
                    {
                        XyzManager.Instance.RandomizeGuardLocations(locationString);
                    }
                    break;

                case "v006b": // Rassvet - Virtuous Mission
                              // Nothing to randomize here since Ocelot and others are knocked out
                    break;

                default:
                    LoggingManager.Instance.Log("No randomization applied.");
                    break;
            }
        }
    }
}