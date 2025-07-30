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
        private static Thread Running;
        public static IntPtr gprocID = IntPtr.Zero;

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
            // Silently fail on below errors.
            if (Config.MenuShown & forceRun == false)
                return false;
            if (BorderlessINI.Executable == "" & BorderlessINI.SearchString != "")
                return false;
            if (BorderlessINI.Executable != "" & BorderlessINI.SearchString != "" & !exePath.TestPath() )
                return false;

            // Fail with error message on below errors.
            if (BorderlessINI.Executable == "" & BorderlessINI.SearchString == "" & forceRun)
            {
                string Title = "Values Empty";
                string Message = "You must either enter the name of an executable to launch or an executable/window title to search for.";
                Forms.OkayDialog.Display(Title, Message, 260, 32, 24, 16, 10);
                return false;
            }
            if (BorderlessINI.Executable != "" & BorderlessINI.SearchString == "" & !exePath.TestPath() )
            {
                string Title = "Executable Not Found";
                string Message = "The value entered for \"Executable\" was not found. It must exist in the same path as this application.";
                Forms.OkayDialog.Display(Title, Message, 256, 32, 24, 16, 10);
                return false;
            }
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
            // Assemble potential executable path.
            string exePath = Config.BasePath + "\\" + BorderlessINI.Executable + ".exe";

            // Store validation result.
            bool Validate = (Game.ValidateSetBorderless(exePath, forceRun));

            // If validation passed, launch executable.
            if (Validate)
                Game.LaunchExecutable(exePath);

            // If validation failed and search string is set, find matching process.
            if (!Validate & BorderlessINI.SearchString != "")
                Game.SearchForString(BorderlessINI.SearchString);
        }

        private static void LaunchExecutable(string gamePath)
        {
            // Starts the game in a new thread.
            Game.Running = new Thread(() => StartThread(gamePath));
            Game.Running.Start();

            // Hang around until the game actually starts.
            for (int i = 0; i < Game.LoopCount; i++)
            {
                if (Game.gprocID != IntPtr.Zero)
                    break;
                Thread.Sleep(Game.LoopDelay);
            }
            // If the dialog is visible disable it until the loop has ended.
            if (Forms.MainDialog != null)
                Forms.MainDialog.Enabled = false;

            if (BorderlessINI.ScreenMethod == "1")
            {
                Forms.CopyDialog = new Form_CopyMenu(Game.gprocID);
                Forms.CopyDialog.ShowDialog();
            }
            else
            {
                Window.SetBorderless(Game.gprocID);
            }
            // If the dialog is visible open it back up for the user.
            if (Forms.MainDialog != null)
                Forms.MainDialog.Enabled = true;
        }

        private static void StartThread(string gamePath)
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

            // Wait for the window to appear before trying to maximize.
            for (int i = 0; i < Game.LoopCount; i++)
            {
                if (Window.IsVisible(gameProcess))
                    break;
                Thread.Sleep(Game.LoopDelay);
            }
            // Get the handle only while the game is running.
            Game.gprocID = gameProcess.MainWindowHandle;
            gameProcess.WaitForExit();
            Game.gprocID = IntPtr.Zero;
            Game.Running = null;
        }

        private static void SearchForString(string searchString)
        {
            // Loop through all processes found and find by executable name or window title.
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName == searchString)
                    Game.gprocID = process.MainWindowHandle;
                else if (process.MainWindowTitle == searchString)
                    Game.gprocID = process.MainWindowHandle;
                if (Game.gprocID != IntPtr.Zero)
                    break;
            }
            // If the process exists try to set borderless window.
            if (Game.gprocID != IntPtr.Zero)
            {
                Window.SetBorderless(Game.gprocID);
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
