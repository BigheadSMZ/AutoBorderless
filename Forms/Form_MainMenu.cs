using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBorderless
{
    public partial class Form_MainMenu : Form
    {
        public Form_MainMenu()
        {
            InitializeComponent();
        }

        private void Form_MainMenu_Load(object sender, EventArgs e)
        {
            this.TextBox_Executable.Text = BorderlessINI.Executable;
            this.TextBox_SearchString.Text = BorderlessINI.SearchString;
            this.CheckBox_AlwaysShow.Checked = (BorderlessINI.AlwaysShow == "true");
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
            Game.ButtonLaunch();
        }

        private void Button_Search_Click(object sender, EventArgs e)
        {
            Game.ButtonSearch();
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
