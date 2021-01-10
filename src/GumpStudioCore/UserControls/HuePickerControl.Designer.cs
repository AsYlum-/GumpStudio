using System.ComponentModel;
using System.Windows.Forms;

namespace GumpStudio.UserControls
{
    partial class HuePickerControl : UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstHue = new System.Windows.Forms.ListBox();
            this.StatusBar = new System.Windows.Forms.StatusBar();
            this.cboQuick = new System.Windows.Forms.ComboBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lstHue
            // 
            this.lstHue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstHue.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstHue.IntegralHeight = false;
            this.lstHue.Location = new System.Drawing.Point(0, 0);
            this.lstHue.Name = "lstHue";
            this.lstHue.Size = new System.Drawing.Size(210, 245);
            this.lstHue.TabIndex = 0;
            this.lstHue.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstHue_DrawItem);
            this.lstHue.SelectedIndexChanged += new System.EventHandler(this.LstHue_SelectedIndexChanged);
            this.lstHue.DoubleClick += new System.EventHandler(this.LstHue_DoubleClick);
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 243);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(210, 22);
            this.StatusBar.SizingGrip = false;
            this.StatusBar.TabIndex = 1;
            // 
            // cboQuick
            // 
            this.cboQuick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQuick.DropDownWidth = 120;
            this.cboQuick.Items.AddRange(new object[] {
            "Animals",
            "Birds",
            "Blues",
            "Colors",
            "Elemental Weapons",
            "Elemental Wear",
            "Greens",
            "Hair",
            "Interesting #1",
            "Interesting #2",
            "Metals",
            "Neutrals",
            "Oranges",
            "Pinks",
            "Reds",
            "Skin",
            "Slimes",
            "Snakes",
            "Yellows"});
            this.cboQuick.Location = new System.Drawing.Point(124, 243);
            this.cboQuick.Name = "cboQuick";
            this.cboQuick.Size = new System.Drawing.Size(84, 21);
            this.cboQuick.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.cboQuick, "Bookmarks");
            this.cboQuick.SelectedIndexChanged += new System.EventHandler(this.CboQuick_SelectedIndexChanged);
            // 
            // HuePickerControl
            // 
            this.Controls.Add(this.cboQuick);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.lstHue);
            this.DoubleBuffered = true;
            this.Name = "HuePickerControl";
            this.Size = new System.Drawing.Size(210, 265);
            this.Load += new System.EventHandler(this.HuePickerControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ToolTip1;
        private System.Windows.Forms.StatusBar StatusBar;
        private System.Windows.Forms.ListBox lstHue;
        private System.Windows.Forms.ComboBox cboQuick;
    }
}
