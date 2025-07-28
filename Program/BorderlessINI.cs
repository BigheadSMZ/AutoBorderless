using System.Diagnostics;

namespace AutoBorderless
{
    internal class BorderlessINI
    {
        public static string  Path;
        public static IniFile File;
        public static string  Executable;
        public static string  SearchString;
        public static string  AlwaysShow;

        public static void Initialize()
        {
            // Get the name of the executable as the user may have changed it.
            string exeName = Process.GetCurrentProcess().ProcessName;

            // Set up the INI path and class.
            BorderlessINI.Path = Config.BasePath + "\\" + exeName + ".ini";
            BorderlessINI.File = new IniFile(BorderlessINI.Path);

            // If the INI exists load it, if not create a new one.
            if (BorderlessINI.Path.TestPath())
                BorderlessINI.LoadValues();
            else
                BorderlessINI.CreateNew();
        }
        public static void CreateNew()
        {
            BorderlessINI.Executable = "";
            BorderlessINI.SearchString = "";
            BorderlessINI.AlwaysShow = "false";
            BorderlessINI.WriteValues();
        }

        public static void WriteValues()
        {
            BorderlessINI.File.Write("Executable", BorderlessINI.Executable, "Application");
            BorderlessINI.File.Write("SearchString", BorderlessINI.SearchString, "Application");
            BorderlessINI.File.Write("AlwaysShow", BorderlessINI.AlwaysShow, "Application");
        }

        public static void LoadValues()
        {
            BorderlessINI.Executable = BorderlessINI.File.Read("Executable", "Application");
            BorderlessINI.SearchString = BorderlessINI.File.Read("SearchString", "Application");
            BorderlessINI.AlwaysShow = BorderlessINI.File.Read("AlwaysShow", "Application");
        }
    }
}
