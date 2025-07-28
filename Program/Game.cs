using System;
using System.Diagnostics;
using System.Threading;

namespace AutoBorderless
{
    internal class Game
    {
        // When waiting for the window to appear, this sets the number of attempts to check 
        // if the window is visible, and the duration between each iteration of the loop.
        private static int MaxLoops = 200;
        private static int LoopDelay = 50;

        public static void SetBorderless(bool forceRun = false)
        {
            // If the menu was shown to the user, don't try to do any automated tasks.
            if (Config.MenuShown && forceRun == false)
                return;

            // Assemble where the executable should be.
            string pathToGame = Config.BasePath + "\\" + BorderlessINI.Executable + ".exe";

            // If an executable exists, launch it. If it doesn't, try to search for a string.
            if (pathToGame.TestPath())
                Game.LaunchExecutable(pathToGame);
            else if (BorderlessINI.SearchString != "")
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

            // Wait for the window to appear before trying to maximize.
            for (int i = 0; i < Game.MaxLoops; i++)
            {
                if (Window.IsVisible(gameProcess))
                    break;
                Thread.Sleep(Game.LoopDelay);
            }

            // Apply borderless to the window.
            IntPtr hWnd = gameProcess.MainWindowHandle;
            Window.SetBorderless(hWnd);
        }

        public static void SearchForString(string searchString)
        {
            // Attempt to set borderless via process name or window name.
            for (int i = 0; i < Game.MaxLoops; i++)
            {
                if (Window.SetBorderlessString(searchString))
                    break;
                Thread.Sleep(Game.LoopDelay);
            }
        }

    }
}
