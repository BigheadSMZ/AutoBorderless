using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace AutoBorderless
{
    internal class Game
    {
        private static int LoopCount;
        private static int LoopDelay;

        public static void Initialize()
        {
            // Use the user defined values if applicable. Fall back to defaults if unavailable.
            if (BorderlessINI.LoopCount != null && BorderlessINI.LoopCount != "")
                Game.LoopCount = Convert.ToInt32(BorderlessINI.LoopCount);
            else
                Game.LoopCount = 100;
            if (BorderlessINI.LoopDelay != null && BorderlessINI.LoopDelay != "")
                Game.LoopDelay = Convert.ToInt32(BorderlessINI.LoopDelay);
            else
                Game.LoopDelay = 50;
        }

        private static bool ValidateSetBorderless(string exePath, bool forceRun)
        {
            // GUI was shown then closed. This check skips the launch in "Initialization".
            if (Config.MenuShown & forceRun == false)
                return false;

            // There is a search string, so just don't try to launch the executable.
            if (BorderlessINI.Executable == "" & BorderlessINI.SearchString != "")
                return false;

            // Executable path set but not valid and search string is not empty.
            if (BorderlessINI.Executable != "" & !exePath.TestPath() & BorderlessINI.SearchString != "")
                return false;

            // Launched from GUI and both fields are empty.
            if (forceRun & BorderlessINI.Executable == "" & BorderlessINI.SearchString == "")
            {
                string Title = "Values Empty";
                string Message = "You must either enter the name of an executable to launch or an executable/window title to search for.";
                Forms.OkayDialog.Display(Title, Message, 260, 32, 24, 16, 10);
                return false;
            }
            // Executable path set but not valid and search string is empty.
            if (BorderlessINI.Executable != "" & !exePath.TestPath() & BorderlessINI.SearchString == "")
            {
                string Title = "Executable Not Found";
                string Message = "The value entered for \"Executable\" was not found. It must exist in the same path as this application.";
                Forms.OkayDialog.Display(Title, Message, 256, 32, 24, 16, 10);
                return false;
            }
            // The path to the executable is this application.
            if (exePath == Config.AppPath)
            {
                string Title = "Nope Not Happening";
                string Message = "Nice try. AutoBorderless can not launch itself.";
                Forms.OkayDialog.Display(Title, Message, 256, 32, 38, 26, 10);
                return false;
            }
            // If none of that happened try to apply borderless.
            return true;
        }

        public static void SetBorderless(bool forceRun = false)
        {
            // Assemble where the executable should be.
            string exePath = Config.BasePath + "\\" + BorderlessINI.Executable + ".exe";

            // Get the validation state of launching an executable.
            bool Validate = (Game.ValidateSetBorderless(exePath, forceRun));

            // If the "Executable" check passes validation attempt to launch it.
            if (Validate)
                Game.LaunchExecutable(exePath);

            // If the "Search String" is not empty then attempt to find it.
            if (!Validate & BorderlessINI.SearchString != "")
                Game.SearchForString(BorderlessINI.SearchString);
        }

        private static void LaunchExecutable(string gamePath)
        {
            // Create and start the game process.
            FileItem gameItem = new FileItem(gamePath);
            Process gameProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo 
            {
                WorkingDirectory = gameItem.DirectoryName,
                FileName = gameItem.FullName,
                Arguments = "",
                UseShellExecute = false,
                RedirectStandardOutput = false,
                WindowStyle = ProcessWindowStyle.Normal
            };
            gameProcess.StartInfo = startInfo;
            gameProcess.Start();

            // If the dialog is visible disable it until the loop has ended.
            if (Forms.MainDialog != null)
                Forms.MainDialog.Enabled = false;

            // Wait for the window to appear before trying to maximize.
            for (int i = 0; i < Game.LoopCount; i++)
            {
                if (Window.IsVisible(gameProcess))
                    break;
                if (Forms.MainDialog != null)
                    Application.DoEvents();
                Thread.Sleep(Game.LoopDelay);
            }
            // Apply borderless to the window.
            IntPtr hWnd = gameProcess.MainWindowHandle;
            Window.SetBorderless(hWnd);

            // If the dialog is visible open it back up for the user.
            if (Forms.MainDialog != null)
                Forms.MainDialog.Enabled = true;
        }

        private static void SearchForString(string searchString)
        {
            // Define the handle pointer.
            IntPtr hWnd = IntPtr.Zero;

            // Loop through all processes found and find by executable name or window title.
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName == searchString)
                    hWnd = process.MainWindowHandle;
                else if (process.MainWindowTitle == searchString)
                    hWnd = process.MainWindowHandle;
                if (hWnd != IntPtr.Zero)
                    break;
            }
            // If the process exists try to set borderless window.
            if (hWnd != IntPtr.Zero)
            {
                Window.SetBorderless(hWnd);
            }
            else
            {
                string Title = "Process Not Found";
                string Message = "A running process matching executable name or window title entered in \"Search String\" not found.";
                Forms.OkayDialog.Display(Title, Message, 256, 32, 24, 16, 10);
            }
        }
    }
}
