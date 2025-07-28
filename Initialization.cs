using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBorderless
{
    internal static class Initialization
    {
        [STAThread]
        static void Main()
        {
            // Set here for easy editing.
            Config.Version = "1.0.0";

            // Enable form visual styles.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set the initial values.
            Config.SetApplicationValues();

            // Initialize.
            BorderlessINI.Initialize();
            Forms.Initialize();
            Game.Initialize();
        }
    }
}
