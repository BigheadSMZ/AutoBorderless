using System;
using IWshRuntimeLibrary;

namespace AutoBorderless
{
    internal class Forms
    {
        public static Form_MainMenu MainDialog = null;
        public static Form_OkayMenu OkayDialog = null;

        public static void Initialize()
        {
            if (BorderlessINI.AlwaysShow == "true" || (BorderlessINI.Executable == "" && BorderlessINI.SearchString == ""))
            {
                // Create the dialog and show it.
                Config.MenuShown = true;
                Forms.MainDialog = new Form_MainMenu();
                Forms.OkayDialog = new Form_OkayMenu();
                Forms.MainDialog.Text = "AutoBorderless v" + Config.Version;
                Forms.MainDialog.ShowDialog();

                // Update the INI file with new values.
                BorderlessINI.WriteValues();
            }
        }

        public static void CreateShortcut() 
        {
            // Assemble where the executable should be.
            string pathToGame = Config.BasePath + "\\" + BorderlessINI.Executable + ".exe";

            // Check to see if the path to the game is valid based on what is entered.
            if (!pathToGame.TestPath())
            {
                string Title = "Executable Not Found";
                string Message = "The name entered in the \"Executable\" field is invalid. Please check the spelling and make sure it exists.";
                Forms.OkayDialog.Display(Title, Message, 260, 32, 24, 16, 10);
                return;
            }
            // Create a new openfolder dialog.
            FolderSelectDialog folderDialog = new FolderSelectDialog();
            folderDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            folderDialog.Title = "Select Shortcut Destination";
            folderDialog.Show();

            // Store the file that was returned.
            string recievedFolder = folderDialog.FileName;

            // Make sure the folder has been set.
            if (recievedFolder != "")
            {
                // Get the file as an item.
                FileItem GameItem = new FileItem(pathToGame);

                // Set the path to the shortcut.
                string ShortcutPath = recievedFolder + "\\" + GameItem.BaseName + ".lnk";

                // Use windows script host to create a shortcut.
                WshShell WShell = new WshShell();
                IWshShortcut WShortCut = (IWshShortcut)WShell.CreateShortcut(ShortcutPath);
                WShortCut.TargetPath = Config.AppPath;
                WShortCut.Arguments = "";
                WShortCut.WorkingDirectory = Config.BasePath;
                WShortCut.IconLocation = pathToGame;
                WShortCut.Save();
            }
        }
    }
}
