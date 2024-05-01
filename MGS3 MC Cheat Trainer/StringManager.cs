using System.Diagnostics;
using System.Text;
using static MGS3_MC_Cheat_Trainer.MemoryManager;

namespace MGS3_MC_Cheat_Trainer
{
    public class StringManager
    {
        private static StringManager instance;
        private static readonly object lockObj = new object();

        private StringManager()
        {
        }

        public static StringManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new StringManager();
                    }

                    return instance;
                }
            }
        }

        /* List of strings for areas of the game that can be read to use for conditional checks
           A few things to note:
           1. _0 attached to these strings means a cutscene is playing
           2. v = Virtuous Mission, s = Snake Eater
           3. A lot of areas in memory have these strings so we can use any consistent one to run a check for the area
           4. Some spots in memory use this string + a file path probably to tell the game to load a specific scenario, etc
           i.e (fr/stage/s023a/manifest.txt, assets/gcx/fr/scenerio_stage_s023a.gcx, fr/stage/s023a/cache/00180720.gcx)
        */
        public enum LocationString
        {
            kyle_op, // Opening Snake Eater video also appears when Snake Easter video plays after the Virtuous Mission
            title, // CQC Start screen and or Main Menu
            theater, // Demo Theater Mode
            v001a, // Dremuchji South - Starting Area in Virtuous Mission
            s001a, // Opening Cutscenes to Operation Snake Eater
            s002a, // Dremuchji East - OSE Starting Area
            v003a, // Dremuchji Swampland - Croc area in V mission
            s003a, // Dremuchji Swampland - Croc area in V mission
            v004a, // Dremuchji North - First KGB Troop in V and where Boss breaks Snake's gun in SE
            s004a, // Dremuchji North - First KGB Troop in V and where Boss breaks Snake's gun in SE
            v005a, // Dolinovodno Rope Bridge - Rope Bridge
            s005a, // Dolinovodno Rope Bridge - Rope Bridge
            v006a, // Rassvet - Sokolov/Ocelot Unit
            v006b, // Rassvet - Sokolov/Ocelot Unit
            s006a, // Rassvet - Sokolov/Ocelot Unit
            s006b, // Rassvet - Sokolov/Ocelot Unit
            s007a, // VM Bridge Fall Aftermath Zone
            v007a, // Dolinovodno Riverbank - The Boss she betrayed me Major...
            s012a, // Chyornyj Prud - Swamp
            s021a, // Bolshaya Past South
            s022a, // Bolshaya Past North
            s023a, // Bolshaya Past Crevice - Ocelot Battle Area
            s031a, // Chyornaya Peschera Cave Branch - Cave After Ocelot
            s032a, // Chyornaya Peschera Cave - Before and After Pain boss fight
            s032b, // Chyornaya Peschera Cave - Pain Boss Battle
            s033a, // Chyornaya Peschera Cave Entrance
            s041a, // Ponizovje South - Water Area with the hovercrafts enemies
            s042a, // Ponizovje Armory - Where you get the SVD early
            s043a, // Ponizovje Warehouse Exterior - Outside where you can kill the end early
            s044a, // Ponizovje Warehouse - Interior of the warehouse
            s051a, // Graniny Gorki South - Fear Boss fight
            s051b, // Graniny Gorki South - Fear Boss fight
            s052a, // Graniny Gorki Lab Exterior: Outside Walls - Area after all traps/where you fight Fear later
            s052b, // Graniny Gorki Lab Exterior: Inside Walls 
            s053a, // Graniny Gorki Lab 1F/2F
            s054a, // Graniny Gorki Lab Interior
            s055a, // Graniny Gorki Lab B1 (Prison Cells)
            s056a, // Graniny Gorki Lab B1 (Granin Basement Area)
            s045a, // Svyatogornyj South - Just after Ponizovje Warehouse
            s061a, // Svyatogornyj West
            s062a, // Svyatogornyj East - M63 Area with the house
            s063a, // Sokrovenno South - The End Fight
            s064a, // Sokrovenno West - The End fight area with the river
            s065a, // Sokrovenno North - Area where The End dies and you head to the ladder area
            s066a, // Krasnogorje Tunnel - What a thrill...
            s071a, // Krasnogorje Mountain Base - Start of the mountain area
            s072b, // Krasnogorje Mountainside - s072a might be hovercrafts instead of Hind-D?
            s073a, // Krasnogorje Mountaintop - Before cutscene with Eva
            s074a, // Krasnogorje Mountaintop Ruins - Eva Cutscene
            s075a, // Krasnogorje Mountaintop: Behind Ruins
            s081a, // Groznyj Grad Underground Tunnel - Before/During/After the fight with The Fury
            s091a, // Groznyj Grad Southwest - Area after fight with The Fury
            s091b, // Groznyj Grad Southwest - During Escape
            s091c, // Groznyj Grad Southwest - Back after escape
            s092a, // Groznyj Grad Northwest - Armory/ where you escape into the tunnels
            s092b, // Groznyj Grad Northwest - During Escape
            s092c, // Groznyj Grad Northwest - Back after escape
            s093a, // Groznyj Grad Northeast - Has the Enterance to the weapon's lab East Wing
            s093b, // Groznyj Grad Northeast - During Escape
            s093c, // Groznyj Grad Northeast - Back after escape
            s094a, // Groznyj Grad Southeast - Area with Enterance to the prison cells
            s094b, // Groznyj Grad Southeast - During Escape
            s094c, // Groznyj Grad Southeast - Back after escape
            s101a, // Groznyj Grad Weapon's Lab: East Wing - Where you take Raikov's outfit
            s101b, // Groznyj Grad Weapon's Lab: East Wing - After taking Raikov's outfit/Also when back to plant the C3
            s113a, // Groznyj Grad Sewers
            s112a, // Groznyj Grad Torture Room always this regardless of before, during, or after torture
            s111a, // Groznyj Grad Weapon's Lab: West Wing Corridor - Before you trigger Sokolov cutscene
            s121a, // Groznyj Grad Weapon's Lab: Main Wing
            s122a, // Groznyj Grad Weapon's Lab: Main Wing B1 - Volgin Fight both parts
            s141a, // Unsure on name of this area - Sorrow Boss Fight
            s151a, // Tikhogornyj - After Sorrow Fight regardless of if Ocelot Unit is there or not
            s152a, // Tikhogornyj - Behind Waterfall
            s161a, // Groznyj Grad - 1st Part of bike chase
            s162a, // Groznyj Grad Runway South - 2nd Part of bike chase
            s163a, // Groznyj Grad Runway - 3rd part of bike chase
            s163b, // Groznyj Grad Runway - 4th path of chase with shagohod only
            s171a, // Groznyj Grad Rail Bridge - Shooting the C3
            s171b, // Groznyj Grad Rail Bridge - Fighting the Shagohod on and off the bike
            s181a, // Groznyj Grad Rail Bridge North - 1st Escape after beating Volgin
            s182a, // Lazorevo South - Part of the chase with Hovercrafts
            s183a, // Lazorevo North - Final part of chase before on foot with Eva
            s191a, // Zaozyorje West - On Foot Area
            s192a, // Zaozyorje East - Final area before The Boss
            s201a, // Rokovj Bereg - Boss Arena
            s211a, // Wig: Interior - When you pick which SAA
            ending // Credits, etc
        }

        public static readonly Dictionary<LocationString, string> LocationAreaNames =
            new Dictionary<LocationString, string>
            {
                { LocationString.kyle_op, "Opening Snake Eater video" },
                { LocationString.title, "CQC Intro or Main Menu" },
                { LocationString.theater, "Demo Theater Mode" },
                { LocationString.v001a, "Dremuchji South - Virtuous Mission" },
                { LocationString.s001a, "Dremuchji South - Snake Eater" },
                { LocationString.s002a, "Dremuchji East" },
                { LocationString.v003a, "Dremuchji Swampland - Virtuous Mission" },
                { LocationString.s003a, "Dremuchji Swampland - " },
                { LocationString.v004a, "Dremuchji North - Virtuous Mission" },
                { LocationString.s004a, "Dremuchji North - " },
                { LocationString.v005a, "Dolinovodno Rope Bridge - Virtuous Mission" },
                { LocationString.s005a, "Dolinovodno Rope Bridge - Snake Eater" },
                { LocationString.v006a, "Rassvet - Virtuous Mission" },
                { LocationString.v006b, "Rassvet - Virtuous Mission" },
                { LocationString.s006a, "Rassvet - Snake Eater" },
                { LocationString.s006b, "Rassvet - Snake Eater" },
                { LocationString.s007a, "VM Bridge Fall Aftermath Zone" }, // Don't actually know the name of this area
                { LocationString.v007a, "Dolinovodno Riverbank - Virtuous Mission" },
                // Virtuous Mission doesn't go past this point so no need to state the differences
                { LocationString.s012a, "Chyornyj Prud" },
                { LocationString.s021a, "Bolshaya Past South" },
                { LocationString.s022a, "Bolshaya Past North" },
                { LocationString.s023a, "Bolshaya Past Crevice" },
                { LocationString.s031a, "Chyornaya Peschera Cave Branch" },
                { LocationString.s032a, "Chyornaya Peschera Cave" },
                { LocationString.s032b, "Chyornaya Peschera Cave" },
                { LocationString.s033a, "Chyornaya Peschera Cave Entrance" },
                { LocationString.s041a, "Ponizovje South" },
                { LocationString.s042a, "Ponizovje Armory" },
                { LocationString.s043a, "Ponizovje Warehouse Exterior" },
                { LocationString.s044a, "Ponizovje Warehouse" },
                { LocationString.s051a, "Graniny Gorki South" },
                { LocationString.s051b, "Graniny Gorki South" },
                { LocationString.s052a, "Graniny Gorki Lab Exterior: Outside Walls" },
                { LocationString.s052b, "Graniny Gorki Lab Exterior: Inside Walls" },
                { LocationString.s053a, "Graniny Gorki Lab 1F/2F" },
                { LocationString.s054a, "Graniny Gorki Lab Interior" },
                { LocationString.s055a, "Graniny Gorki Lab B1" },
                { LocationString.s056a, "Graniny Gorki Lab B1" },
                { LocationString.s045a, "Svyatogornyj South" },
                { LocationString.s061a, "Svyatogornyj West" },
                { LocationString.s062a, "Svyatogornyj East" },
                { LocationString.s063a, "Sokrovenno South" },
                { LocationString.s064a, "Sokrovenno West" },
                { LocationString.s065a, "Sokrovenno North" },
                { LocationString.s066a, "Krasnogorje Tunnel" },
                { LocationString.s071a, "Krasnogorje Mountain Base" },
                { LocationString.s072b, "Krasnogorje Mountainside" },
                { LocationString.s073a, "Krasnogorje Mountaintop" },
                { LocationString.s074a, "Krasnogorje Mountaintop Ruins" },
                { LocationString.s075a, "Krasnogorje Mountaintop: Behind Ruins" },
                { LocationString.s081a, "Groznyj Grad Underground Tunnel" },
                { LocationString.s091a, "Groznyj Grad Southwest" },
                { LocationString.s091b, "Groznyj Grad Southwest - During Escape" },
                { LocationString.s091c, "Groznyj Grad Southwest - C3 Mission" },
                { LocationString.s092a, "Groznyj Grad Northwest" },
                { LocationString.s092b, "Groznyj Grad Northwest - During Escape" },
                { LocationString.s092c, "Groznyj Grad Northwest - C3 Mission" },
                { LocationString.s093a, "Groznyj Grad Northeast" },
                { LocationString.s093b, "Groznyj Grad Northeast - During Escape" },
                { LocationString.s093c, "Groznyj Grad Northeast - C3 Mission" },
                { LocationString.s094a, "Groznyj Grad Southeast" },
                { LocationString.s094b, "Groznyj Grad Southeast - During Escape" },
                { LocationString.s094c, "Groznyj Grad Southeast - C3 Mission" },
                { LocationString.s101a, "Groznyj Grad Weapon's Lab: East Wing" },
                { LocationString.s101b, "Groznyj Grad Weapon's Lab: East Wing - C3 Mission" },
                { LocationString.s113a, "Groznyj Grad Sewers" },
                { LocationString.s112a, "Groznyj Grad Torture Room" },
                { LocationString.s111a, "Groznyj Grad Weapon's Lab: West Wing Corridor" },
                { LocationString.s121a, "Groznyj Grad Weapon's Lab: Main Wing" },
                { LocationString.s122a, "Groznyj Grad Weapon's Lab: Main Wing B1" },
                { LocationString.s141a, "Sorrow Boss Fight" },
                { LocationString.s151a, "Tikhogornyj" },
                { LocationString.s152a, "Tikhogornyj: Behind Waterfall" },
                { LocationString.s161a, "Groznyj Grad" }, // Literally just named this at start of bike chase lol
                { LocationString.s162a, "Groznyj Grad Runway South" },
                { LocationString.s163a, "Groznyj Grad Runway" },
                { LocationString.s163b, "Groznyj Grad Runway - Shagohod Fight" },
                { LocationString.s171a, "Groznyj Grad Rail Bridge" },
                { LocationString.s171b, "Groznyj Grad Rail Bridge - Shagohod Fight" },
                { LocationString.s181a, "Groznyj Grad Rail Bridge North" },
                { LocationString.s182a, "Lazorevo South" },
                { LocationString.s183a, "Lazorevo North" },
                { LocationString.s191a, "Zaozyorje West" },
                { LocationString.s192a, "Zaozyorje East" },
                { LocationString.s201a, "Rokovj Bereg" },
                { LocationString.s211a, "Wig: Interior" },
                { LocationString.ending, "Credits" }
            };

        // Similar to an AOB scan but using the strings above instead
        public string FindLocationStringDirectlyInRange()
        {
            Process process = GetMGS3Process();
            if (process == null)
            {
                return "Game process not found.";
            }

            IntPtr processHandle = OpenGameProcess(process);
            IntPtr baseAddress = process.MainModule.BaseAddress;
            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1C00000);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1D00000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            foreach (StringManager.LocationString location in Enum.GetValues(typeof(StringManager.LocationString)))
            {
                var locationString = location.ToString();
                byte[] pattern = Encoding.ASCII.GetBytes(locationString);
                string mask = new string('x', pattern.Length);

                IntPtr foundAddress =
                    MemoryManager.Instance.ScanMemory(processHandle, startAddress, size, pattern, mask);
                if (foundAddress != IntPtr.Zero)
                {
                    string areaName = StringManager.LocationAreaNames.TryGetValue(location, out var name)
                        ? name
                        : "Unknown Area";

                    // Checking for cutscene indicators
                    foreach (var suffix in new[] { "_0", "_1" })
                    {
                        byte[] cutscenePattern = Encoding.ASCII.GetBytes(locationString + suffix);
                        IntPtr cutsceneFoundAddress = MemoryManager.Instance.ScanMemory(processHandle, startAddress,
                            size, cutscenePattern, mask + "x" + "x");

                        if (cutsceneFoundAddress != IntPtr.Zero)
                        {
                            NativeMethods.CloseHandle(processHandle);
                            return
                                $"Location String: {locationString}{suffix} (Cutscene) \nArea Name: {areaName} \nMemory Address: {cutsceneFoundAddress.ToString("X")}";
                        }
                    }

                    NativeMethods.CloseHandle(processHandle);
                    return
                        $"Location String: {locationString} \nArea Name: {areaName} \nMemory Address: {foundAddress.ToString("X")}";
                }
            }

            NativeMethods.CloseHandle(processHandle);
            return "No Location String found in specified range.";
        }

        /* Same as the above function but only to find the location string it
           currently is only used in the BossForm use the player's area to
           know which boss they can tamper with for the UI elements */
        public string ExtractLocationStringFromResult(string result)
        {
            string[] parts = result.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                string locationStringPart = parts[0];
                string[] locationStringParts =
                    locationStringPart.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (locationStringParts.Length > 1)
                {
                    string locationString = locationStringParts[1].Trim();

                    if (locationString.EndsWith("_0") || locationString.EndsWith("_1"))
                    {
                        return locationString + " (Cutscene)";
                    }
                    else
                    {
                        return locationString;
                    }
                }
            }

            return "Unknown";
        }

        public string FindLocationStringFollowingR_Sna01()
        {
            Process process = GetMGS3Process();
            if (process == null) return "Game process not found.";

            IntPtr processHandle = OpenGameProcess(process);
            IntPtr baseAddress = process.MainModule.BaseAddress;
            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1B00000);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1F00000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            byte[] r_sna01PatternAscii = Encoding.ASCII.GetBytes("r_sna01");
            byte[] r_sna01PatternUtf16 = Encoding.Unicode.GetBytes("r_sna01");
            string r_sna01MaskAscii = new string('x', r_sna01PatternAscii.Length);
            string r_sna01MaskUtf16 = new string('x', r_sna01PatternUtf16.Length / 2);

            IntPtr r_sna01Address = MemoryManager.Instance.ScanMemory(processHandle, startAddress, size,
                r_sna01PatternAscii, r_sna01MaskAscii);
            if (r_sna01Address == IntPtr.Zero)
            {
                r_sna01Address = MemoryManager.Instance.ScanMemory(processHandle, startAddress, size,
                    r_sna01PatternUtf16, r_sna01MaskUtf16);
            }

            if (r_sna01Address == IntPtr.Zero)
            {
                NativeMethods.CloseHandle(processHandle);
                return "r_sna01 not found in specified range.";
            }

            // Start searching for LocationString within a reasonable distance after finding r_sna01
            IntPtr searchStartAddress = r_sna01Address;
            long searchSize = 100; // Increased search size for flexibility

            foreach (var location in Enum.GetValues(typeof(LocationString)))
            {
                var locationString = location.ToString();
                byte[] locationPatternAscii = Encoding.ASCII.GetBytes(locationString);
                byte[] locationPatternUtf16 = Encoding.Unicode.GetBytes(locationString);
                string locationMaskAscii = new string('x', locationPatternAscii.Length);
                string locationMaskUtf16 = new string('x', locationPatternUtf16.Length / 2);

                IntPtr foundAddress = MemoryManager.Instance.ScanMemory(processHandle, searchStartAddress, searchSize,
                    locationPatternAscii, locationMaskAscii);
                if (foundAddress == IntPtr.Zero)
                {
                    foundAddress = MemoryManager.Instance.ScanMemory(processHandle, searchStartAddress, searchSize,
                        locationPatternUtf16, locationMaskUtf16);
                }

                if (foundAddress != IntPtr.Zero)
                {
                    long offsetForR_Sna01 = r_sna01Address.ToInt64() - baseAddress.ToInt64();
                    long offsetForFoundLocation = foundAddress.ToInt64() - baseAddress.ToInt64();
                    string areaName = LocationAreaNames.TryGetValue((LocationString)location, out var name)
                        ? name
                        : "Unknown Area";

                    NativeMethods.CloseHandle(processHandle);
                    return
                        $"Location String: {locationString} ({areaName}) found at: {foundAddress.ToString("X")} \nMETAL GEAR SOLID3.exe+{offsetForFoundLocation:X}";
                }
            }

            NativeMethods.CloseHandle(processHandle);
            return "r_sna01 found, but no Location String found within the search range.";
        }

        public List<string> FindAllR_Sna01AndLocationStringInstances()
        {
            Process process = MemoryManager.GetMGS3Process();
            if (process == null) return new List<string> { "Game process not found." };

            IntPtr processHandle = MemoryManager.OpenGameProcess(process);
            IntPtr baseAddress = process.MainModule.BaseAddress;
            IntPtr startAddress = IntPtr.Add(baseAddress, 0x1B00000);
            IntPtr endAddress = IntPtr.Add(baseAddress, 0x1F00000);
            long size = endAddress.ToInt64() - startAddress.ToInt64();

            List<string> results = new List<string>();

            byte[] r_sna01Pattern = Encoding.ASCII.GetBytes("r_sna01");
            string r_sna01Mask = "xxxxxx";

            while (startAddress.ToInt64() < endAddress.ToInt64() && size > 0)
            {
                IntPtr r_sna01Address = MemoryManager.Instance.ScanForStringMemory(processHandle, startAddress, size, r_sna01Pattern);
                // Rest of the function remains unchanged.

                if (r_sna01Address == IntPtr.Zero) break;

                bool locationFound = false;
                foreach (LocationString location in Enum.GetValues(typeof(LocationString)))
                {
                    string locationString = location.ToString();
                    byte[] locationPattern = Encoding.ASCII.GetBytes(locationString);
                    string locationMask = new string('x', locationPattern.Length);

                    IntPtr locationAddress = MemoryManager.Instance.ScanForStringMemory(processHandle, startAddress, size, r_sna01Pattern);
                    // Rest of the function remains unchanged.
                    // Search within 100 bytes range
                    if (locationAddress != IntPtr.Zero)
                    {
                        string result =
                            $"r_sna01 found at {r_sna01Address.ToString("X")}, followed by {locationString} at {locationAddress.ToString("X")}";
                        results.Add(result);
                        locationFound = true;
                        break; // Assuming only one LocationString is relevant per r_sna01 instance.
                    }
                }

                if (!locationFound)
                {
                    string result =
                        $"r_sna01 found at {r_sna01Address.ToString("X")}, but no LocationString found nearby.";
                    results.Add(result);
                }

                // Prepare for next iteration
                startAddress = IntPtr.Add(r_sna01Address, 1);
                size = endAddress.ToInt64() - startAddress.ToInt64();
            }

            MemoryManager.NativeMethods.CloseHandle(processHandle);

            return results.Count > 0
                ? results
                : new List<string> { "No r_sna01 and LocationString combinations found within the range." };
        }
    }
}