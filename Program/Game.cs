using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace AutoBorderless
{
    internal class Game
    {
        // When waiting for the window to appear, this sets the number of attempts to check 
        // if the window is visible, and the duration between each iteration of the loop.
        private static int MaxLoops = 100;
        private static int LoopDelay = 50;

        public static void SetBorderless(bool forceRun = false)
        {
            // If neither values exist, then let the user know.
            if (BorderlessINI.Executable == "" & BorderlessINI.SearchString == "" & forceRun )
            {
                string Title = "Values Empty";
                string Message = "You must either enter a name of an executable to launch or an executable or window title to search for.";
                Forms.OkayDialog.Display(Title, Message, 260, 32, 24, 16, 10);
                return;
            }
            // If the menu was shown to the user, don't try to do any automated tasks.
            if (Config.MenuShown & forceRun == false)
                return;

            // Assemble where the executable should be.
            string pathToGame = Config.BasePath + "\\" + BorderlessINI.Executable + ".exe";

            // If an executable exists, launch it. If it doesn't, try to search for a string.
            if (pathToGame.TestPath())
                Game.LaunchExecutable(pathToGame);
            if (BorderlessINI.SearchString != "")
                Game.SearchForString(BorderlessINI.SearchString);
        }

        public static void LaunchExecutable(string gamePath)
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
            for (int i = 0; i < Game.MaxLoops; i++)
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

        public static void SearchForString(string searchString)
        {
            // If the dialog is visible disable it until the loop has ended.
            if (Forms.MainDialog != null)
                Forms.MainDialog.Enabled = false;

            // Attempt to set borderless via process name or window name.
            for (int i = 0; i < Game.MaxLoops; i++)
            {
                if (Window.SetBorderlessString(searchString))
                    break;
                if (Forms.MainDialog != null)
                    Application.DoEvents();
                Thread.Sleep(Game.LoopDelay);
            }
            // If the dialog is visible open it back up for the user.
            if (Forms.MainDialog != null)
                Forms.MainDialog.Enabled = true;
        }

    }
}
