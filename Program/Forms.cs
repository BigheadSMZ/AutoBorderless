namespace AutoBorderless
{
    internal class Forms
    {
        public static Form_MainMenu MainDialog = null;

        public static void Initialize()
        {
            if (BorderlessINI.AlwaysShow == "true" || (BorderlessINI.Executable == "" && BorderlessINI.SearchString == ""))
            {
                // Create the dialog and show it.
                Config.MenuShown = true;
                Forms.MainDialog = new Form_MainMenu();
                Forms.MainDialog.Text = "AutoBorderless v" + Config.Version;
                Forms.MainDialog.ShowDialog();

                // Update the INI file with new values.
                BorderlessINI.WriteValues();
            }
        }
    }
}
