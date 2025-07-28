using System.IO;
using System.Reflection;

namespace AutoBorderless
{
    internal class Config
    {
        public static string  AppName;
        public static string  AppPath;
        public static string  Version;
        public static string  BasePath;
        public static bool    MenuShown;

        public static void SetApplicationValues()
        {
            // Get the folder this app is in.
            Config.AppPath  = Assembly.GetExecutingAssembly().Location;
            Config.AppName  = Assembly.GetExecutingAssembly().GetName().Name;
            Config.BasePath = Path.GetDirectoryName(Config.AppPath);
        }
    }
}
