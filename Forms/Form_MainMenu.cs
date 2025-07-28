using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoBorderless
{
    public partial class Form_MainMenu : Form
    {
        public Form_MainMenu()
        {
            InitializeComponent();
        }
        private void HideHelp()
        {
            // Adjust the form to hide the help section.
            this.Size = new Size(347, 216);
            this.Group_Info.Visible = false;
            this.Group_Executable.Location = new Point(10,2);
            this.Group_SearchString.Location = new Point(10,56);
            this.CheckBox_AlwaysShow.Location = new Point(10,117);
            this.Button_Launch.Location = new Point(9,142);
            this.ButtonShortcut.Location = new Point(116,142);
            this.Button_Close.Location = new Point(224,142);
        }

        private void Form_MainMenu_Load(object sender, EventArgs e)
        {
            // Load INI values and apply changes to GUI.
            this.TextBox_Executable.Text = BorderlessINI.Executable;
            this.TextBox_SearchString.Text = BorderlessINI.SearchString;
            this.CheckBox_AlwaysShow.Checked = (BorderlessINI.AlwaysShow == "true");
            if (BorderlessINI.HideHelp == "true")
            {
                this.HideHelp(); 
                this.CenterToScreen();
            }
        }

        private void TextBox_Executable_TextChanged(object sender, EventArgs e)
        {
            string text = (sender as TextBox).Text;
            BorderlessINI.Executable = text;
        }

        private void TextBox_SearchString_TextChanged(object sender, EventArgs e)
        {
            string text = (sender as TextBox).Text;
            BorderlessINI.SearchString = text;
        }

        private void Check_AlwaysShow_CheckedChanged(object sender, EventArgs e)
        {
            bool check = (sender as CheckBox).Checked;
            BorderlessINI.AlwaysShow = check.ToString().ToLower();
        }

        private void Button_Launch_Click(object sender, EventArgs e)
        {
            Game.SetBorderless(true);
        }

        private void Button_Shortcut_Click(object sender, EventArgs e)
        {
            Forms.CreateShortcut();
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_HideHelp_Click(object sender, EventArgs e)
        {
            BorderlessINI.HideHelp = "true";
            this.HideHelp();
        }
    }
}
