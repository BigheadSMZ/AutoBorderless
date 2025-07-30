using System.Diagnostics;
using System.Collections.Generic;

namespace AutoBorderless
{
    internal class BorderlessINI
    {
        // Basic values.
        public static string  Path;
        public static IniFile File;

        // Application section.
        public static string Executable;
        public static string SearchString;
        public static string AlwaysShow;
        public static string ScreenMethod;
        public static string HideHelp;
        public static List<string> ApplicationList = new List<string>
        {
            "Executable", 
            "SearchString", 
            "AlwaysShow", 
            "ScreenMethod", 
            "HideHelp"
        };
        // Advanced Section.
        public static string LoopCount;
        public static string LoopDelay;
        public static List<string> AdvancedList = new List<string>
        {
            "LoopCount", 
            "LoopDelay"
        };

        public static void Initialize()
        {
            // Get the name of the executable as the user may have changed it.
            string exeName = Process.GetCurrentProcess().ProcessName;

            // Set up the INI path, class, and defaults.
            BorderlessINI.Path = Config.BasePath + "\\" + exeName + ".ini";
            BorderlessINI.File = new IniFile(BorderlessINI.Path);
            BorderlessINI.SetDefaults();

            // If the INI exists, verfiy all values are there and load it.
            if (BorderlessINI.Path.TestPath())
            {
                // Get the keys currently within the INI file by section.
                List<string> appKeys = BorderlessINI.File.GetSectionKeys("Application");
                List<string> advKeys = BorderlessINI.File.GetSectionKeys("Advanced");

                // Use the list of known keys to check if there are missing keys.
                foreach (string Key in BorderlessINI.ApplicationList) 
                    if (!appKeys.Contains(Key))
                        BorderlessINI.File.Write(Key, typeof(BorderlessINI).GetField(Key).GetValue(null).ToString(), "Application");
                foreach (string Key in BorderlessINI.AdvancedList) 
                    if (!advKeys.Contains(Key))
                        BorderlessINI.File.Write(Key, typeof(BorderlessINI).GetField(Key).GetValue(null).ToString(), "Advanced");

                // Load the values.
                BorderlessINI.LoadValues();
            }
            // If the INI file doesn't exist write a new one.
            else
            {
                BorderlessINI.WriteValues();
            }
        }

        public static void SetDefaults()
        {
            BorderlessINI.Executable = "";
            BorderlessINI.SearchString = "";
            BorderlessINI.AlwaysShow = "true";
            BorderlessINI.ScreenMethod = "0";
            BorderlessINI.HideHelp = "false";
            BorderlessINI.LoopCount = "100";
            BorderlessINI.LoopDelay = "50";
        }

        public static void LoadValues()
        {
            BorderlessINI.Executable = BorderlessINI.File.Read("Executable", "Application");
            BorderlessINI.SearchString = BorderlessINI.File.Read("SearchString", "Application");
            BorderlessINI.AlwaysShow = BorderlessINI.File.Read("AlwaysShow", "Application");
            BorderlessINI.ScreenMethod = BorderlessINI.File.Read("ScreenMethod", "Application");
            BorderlessINI.HideHelp = BorderlessINI.File.Read("HideHelp", "Application");
            BorderlessINI.LoopCount = BorderlessINI.File.Read("LoopCount", "Advanced");
            BorderlessINI.LoopDelay = BorderlessINI.File.Read("LoopDelay", "Advanced");
        }

        public static void WriteValues()
        {
            BorderlessINI.File.Write("Executable", BorderlessINI.Executable, "Application");
            BorderlessINI.File.Write("SearchString", BorderlessINI.SearchString, "Application");
            BorderlessINI.File.Write("AlwaysShow", BorderlessINI.AlwaysShow, "Application");
            BorderlessINI.File.Write("ScreenMethod", BorderlessINI.ScreenMethod, "Application");
            BorderlessINI.File.Write("HideHelp", BorderlessINI.HideHelp, "Application");
            BorderlessINI.File.Write("LoopCount", BorderlessINI.LoopCount, "Advanced");
            BorderlessINI.File.Write("LoopDelay", BorderlessINI.LoopDelay, "Advanced");
        }

    }
}
