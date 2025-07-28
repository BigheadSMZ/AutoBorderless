namespace AutoBorderless
{
    partial class Form_MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MainMenu));
            this.Group_Executable = new System.Windows.Forms.GroupBox();
            this.TextBox_Executable = new System.Windows.Forms.TextBox();
            this.Group_SearchString = new System.Windows.Forms.GroupBox();
            this.TextBox_SearchString = new System.Windows.Forms.TextBox();
            this.CheckBox_AlwaysShow = new System.Windows.Forms.CheckBox();
            this.Button_Launch = new System.Windows.Forms.Button();
            this.Button_Close = new System.Windows.Forms.Button();
            this.ButtonShortcut = new System.Windows.Forms.Button();
            this.Group_Info = new System.Windows.Forms.GroupBox();
            this.Button_HideHelp = new System.Windows.Forms.Button();
            this.Label_Info = new System.Windows.Forms.Label();
            this.MainTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.Group_Executable.SuspendLayout();
            this.Group_SearchString.SuspendLayout();
            this.Group_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // Group_Executable
            // 
            this.Group_Executable.Controls.Add(this.TextBox_Executable);
            this.Group_Executable.Location = new System.Drawing.Point(10, 190);
            this.Group_Executable.Name = "Group_Executable";
            this.Group_Executable.Size = new System.Drawing.Size(311, 51);
            this.Group_Executable.TabIndex = 0;
            this.Group_Executable.TabStop = false;
            this.Group_Executable.Text = "Executable";
            this.MainTooltip.SetToolTip(this.Group_Executable, resources.GetString("Group_Executable.ToolTip"));
            // 
            // TextBox_Executable
            // 
            this.TextBox_Executable.Location = new System.Drawing.Point(9, 19);
            this.TextBox_Executable.Name = "TextBox_Executable";
            this.TextBox_Executable.Size = new System.Drawing.Size(294, 20);
            this.TextBox_Executable.TabIndex = 0;
            this.TextBox_Executable.TextChanged += new System.EventHandler(this.TextBox_Executable_TextChanged);
            // 
            // Group_SearchString
            // 
            this.Group_SearchString.Controls.Add(this.TextBox_SearchString);
            this.Group_SearchString.Location = new System.Drawing.Point(10, 244);
            this.Group_SearchString.Name = "Group_SearchString";
            this.Group_SearchString.Size = new System.Drawing.Size(311, 51);
            this.Group_SearchString.TabIndex = 1;
            this.Group_SearchString.TabStop = false;
            this.Group_SearchString.Text = "Search String";
            this.MainTooltip.SetToolTip(this.Group_SearchString, resources.GetString("Group_SearchString.ToolTip"));
            // 
            // TextBox_SearchString
            // 
            this.TextBox_SearchString.Location = new System.Drawing.Point(9, 19);
            this.TextBox_SearchString.Name = "TextBox_SearchString";
            this.TextBox_SearchString.Size = new System.Drawing.Size(294, 20);
            this.TextBox_SearchString.TabIndex = 0;
            this.TextBox_SearchString.TextChanged += new System.EventHandler(this.TextBox_SearchString_TextChanged);
            // 
            // CheckBox_AlwaysShow
            // 
            this.CheckBox_AlwaysShow.AutoSize = true;
            this.CheckBox_AlwaysShow.Location = new System.Drawing.Point(12, 305);
            this.CheckBox_AlwaysShow.Name = "CheckBox_AlwaysShow";
            this.CheckBox_AlwaysShow.Size = new System.Drawing.Size(216, 17);
            this.CheckBox_AlwaysShow.TabIndex = 2;
            this.CheckBox_AlwaysShow.Text = "Always show this menu when launching.";
            this.MainTooltip.SetToolTip(this.CheckBox_AlwaysShow, resources.GetString("CheckBox_AlwaysShow.ToolTip"));
            this.CheckBox_AlwaysShow.UseVisualStyleBackColor = true;
            this.CheckBox_AlwaysShow.CheckedChanged += new System.EventHandler(this.Check_AlwaysShow_CheckedChanged);
            // 
            // Button_Launch
            // 
            this.Button_Launch.Location = new System.Drawing.Point(9, 330);
            this.Button_Launch.Name = "Button_Launch";
            this.Button_Launch.Size = new System.Drawing.Size(98, 26);
            this.Button_Launch.TabIndex = 3;
            this.Button_Launch.Text = "Set Borderless";
            this.MainTooltip.SetToolTip(this.Button_Launch, resources.GetString("Button_Launch.ToolTip"));
            this.Button_Launch.UseVisualStyleBackColor = true;
            this.Button_Launch.Click += new System.EventHandler(this.Button_Launch_Click);
            // 
            // Button_Close
            // 
            this.Button_Close.Location = new System.Drawing.Point(224, 330);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(98, 26);
            this.Button_Close.TabIndex = 4;
            this.Button_Close.Text = "Close";
            this.MainTooltip.SetToolTip(this.Button_Close, "Close this application.");
            this.Button_Close.UseVisualStyleBackColor = true;
            this.Button_Close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // ButtonShortcut
            // 
            this.ButtonShortcut.Location = new System.Drawing.Point(116, 330);
            this.ButtonShortcut.Name = "ButtonShortcut";
            this.ButtonShortcut.Size = new System.Drawing.Size(99, 26);
            this.ButtonShortcut.TabIndex = 5;
            this.ButtonShortcut.Text = "Create Shortcut";
            this.MainTooltip.SetToolTip(this.ButtonShortcut, "If the value set in \"Executable\" is valid, this \r\nbutton creates a shortcut in a " +
        "target location \r\nselected by the user. The shortcut points to \r\nAutoBorderless," +
        " not the original executable.");
            this.ButtonShortcut.UseVisualStyleBackColor = true;
            this.ButtonShortcut.Click += new System.EventHandler(this.Button_Shortcut_Click);
            // 
            // Group_Info
            // 
            this.Group_Info.Controls.Add(this.Button_HideHelp);
            this.Group_Info.Controls.Add(this.Label_Info);
            this.Group_Info.Location = new System.Drawing.Point(10, 2);
            this.Group_Info.Name = "Group_Info";
            this.Group_Info.Size = new System.Drawing.Size(311, 185);
            this.Group_Info.TabIndex = 6;
            this.Group_Info.TabStop = false;
            this.Group_Info.Text = "Information";
            // 
            // Button_HideHelp
            // 
            this.Button_HideHelp.Location = new System.Drawing.Point(284, 8);
            this.Button_HideHelp.Name = "Button_HideHelp";
            this.Button_HideHelp.Size = new System.Drawing.Size(24, 24);
            this.Button_HideHelp.TabIndex = 1;
            this.Button_HideHelp.Text = "-";
            this.MainTooltip.SetToolTip(this.Button_HideHelp, "Permanently hide the info pane.\r\nIt can be reenabled in the INI file.");
            this.Button_HideHelp.UseVisualStyleBackColor = true;
            this.Button_HideHelp.Click += new System.EventHandler(this.Button_HideHelp_Click);
            // 
            // Label_Info
            // 
            this.Label_Info.Location = new System.Drawing.Point(6, 17);
            this.Label_Info.Name = "Label_Info";
            this.Label_Info.Size = new System.Drawing.Size(298, 161);
            this.Label_Info.TabIndex = 0;
            this.Label_Info.Text = resources.GetString("Label_Info.Text");
            // 
            // Form_MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 365);
            this.Controls.Add(this.Group_Info);
            this.Controls.Add(this.ButtonShortcut);
            this.Controls.Add(this.Button_Close);
            this.Controls.Add(this.Button_Launch);
            this.Controls.Add(this.CheckBox_AlwaysShow);
            this.Controls.Add(this.Group_SearchString);
            this.Controls.Add(this.Group_Executable);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoBorderless v*.*.*";
            this.MainTooltip.SetToolTip(this, "If the value entered in the \r\n\"Executable\" field is valid, \r\ncreates Desktop shor" +
        "tcut.");
            this.Load += new System.EventHandler(this.Form_MainMenu_Load);
            this.Group_Executable.ResumeLayout(false);
            this.Group_Executable.PerformLayout();
            this.Group_SearchString.ResumeLayout(false);
            this.Group_SearchString.PerformLayout();
            this.Group_Info.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Group_Executable;
        private System.Windows.Forms.TextBox TextBox_Executable;
        private System.Windows.Forms.GroupBox Group_SearchString;
        private System.Windows.Forms.TextBox TextBox_SearchString;
        private System.Windows.Forms.CheckBox CheckBox_AlwaysShow;
        private System.Windows.Forms.Button Button_Launch;
        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.Button ButtonShortcut;
        private System.Windows.Forms.GroupBox Group_Info;
        private System.Windows.Forms.Label Label_Info;
        private System.Windows.Forms.ToolTip MainTooltip;
        private System.Windows.Forms.Button Button_HideHelp;
    }
}

