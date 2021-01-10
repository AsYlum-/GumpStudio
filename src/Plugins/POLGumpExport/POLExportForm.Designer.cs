using System.ComponentModel;
using System.Windows.Forms;

namespace POLGumpExport
{
    public partial class PolExportForm : Form
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
            if (disposing && this.components != null)
            {
                this.components.Dispose();
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
            this.lbl_Help = new System.Windows.Forms.Label();
            this.grp_Gumptype = new System.Windows.Forms.GroupBox();
            this.rbt_Bare = new System.Windows.Forms.RadioButton();
            this.rbt_Distro = new System.Windows.Forms.RadioButton();
            this.grp_Properties = new System.Windows.Forms.GroupBox();
            this.chk_DefaultTexts = new System.Windows.Forms.CheckBox();
            this.chk_comments = new System.Windows.Forms.CheckBox();
            this.chk_names = new System.Windows.Forms.CheckBox();
            this.lbl_Gumpname = new System.Windows.Forms.Label();
            this.txt_Gumpname = new System.Windows.Forms.TextBox();
            this.txt_Savefile = new System.Windows.Forms.TextBox();
            this.grp_Saveas = new System.Windows.Forms.GroupBox();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grp_Gumptype.SuspendLayout();
            this.grp_Properties.SuspendLayout();
            this.grp_Saveas.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Help
            // 
            this.lbl_Help.BackColor = System.Drawing.SystemColors.Info;
            this.lbl_Help.Location = new System.Drawing.Point(0, 0);
            this.lbl_Help.Name = "lbl_Help";
            this.lbl_Help.Size = new System.Drawing.Size(336, 32);
            this.lbl_Help.TabIndex = 0;
            this.lbl_Help.Text = "Select your gump name and where to save the gump.";
            this.lbl_Help.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grp_Gumptype
            // 
            this.grp_Gumptype.Controls.Add(this.rbt_Bare);
            this.grp_Gumptype.Controls.Add(this.rbt_Distro);
            this.grp_Gumptype.Location = new System.Drawing.Point(0, 40);
            this.grp_Gumptype.Name = "grp_Gumptype";
            this.grp_Gumptype.Size = new System.Drawing.Size(160, 80);
            this.grp_Gumptype.TabIndex = 1;
            this.grp_Gumptype.TabStop = false;
            this.grp_Gumptype.Text = "Gump type";
            // 
            // rbt_Bare
            // 
            this.rbt_Bare.Checked = true;
            this.rbt_Bare.Location = new System.Drawing.Point(16, 24);
            this.rbt_Bare.Name = "rbt_Bare";
            this.rbt_Bare.Size = new System.Drawing.Size(128, 16);
            this.rbt_Bare.TabIndex = 3;
            this.rbt_Bare.TabStop = true;
            this.rbt_Bare.Text = "Bare gump";
            this.toolTip1.SetToolTip(this.rbt_Bare, "Create the old and trusty SendDialogGump gump :)");
            // 
            // rbt_Distro
            // 
            this.rbt_Distro.Location = new System.Drawing.Point(16, 48);
            this.rbt_Distro.Name = "rbt_Distro";
            this.rbt_Distro.Size = new System.Drawing.Size(128, 24);
            this.rbt_Distro.TabIndex = 3;
            this.rbt_Distro.Text = "Distro Gump pkg";
            this.toolTip1.SetToolTip(this.rbt_Distro, "Creates the gump using the very nice gump pkg.");
            this.rbt_Distro.CheckedChanged += new System.EventHandler(this.Distro_CheckedChanged);
            // 
            // grp_Properties
            // 
            this.grp_Properties.Controls.Add(this.chk_DefaultTexts);
            this.grp_Properties.Controls.Add(this.chk_comments);
            this.grp_Properties.Controls.Add(this.chk_names);
            this.grp_Properties.Controls.Add(this.lbl_Gumpname);
            this.grp_Properties.Controls.Add(this.txt_Gumpname);
            this.grp_Properties.Location = new System.Drawing.Point(168, 40);
            this.grp_Properties.Name = "grp_Properties";
            this.grp_Properties.Size = new System.Drawing.Size(160, 142);
            this.grp_Properties.TabIndex = 2;
            this.grp_Properties.TabStop = false;
            this.grp_Properties.Text = "Properties";
            // 
            // chk_DefaultTexts
            // 
            this.chk_DefaultTexts.AutoSize = true;
            this.chk_DefaultTexts.Checked = true;
            this.chk_DefaultTexts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_DefaultTexts.Enabled = false;
            this.chk_DefaultTexts.Location = new System.Drawing.Point(16, 68);
            this.chk_DefaultTexts.Name = "chk_DefaultTexts";
            this.chk_DefaultTexts.Size = new System.Drawing.Size(123, 17);
            this.chk_DefaultTexts.TabIndex = 4;
            this.chk_DefaultTexts.Text = "Create Default Texts";
            this.toolTip1.SetToolTip(this.chk_DefaultTexts, "Create default texts for controls with no text. Example: HtmlElement, TextEntry, " +
        "etc...");
            this.chk_DefaultTexts.UseVisualStyleBackColor = true;
            // 
            // chk_comments
            // 
            this.chk_comments.AutoSize = true;
            this.chk_comments.Enabled = false;
            this.chk_comments.Location = new System.Drawing.Point(16, 114);
            this.chk_comments.Name = "chk_comments";
            this.chk_comments.Size = new System.Drawing.Size(105, 17);
            this.chk_comments.TabIndex = 3;
            this.chk_comments.Text = "Show Comments";
            this.toolTip1.SetToolTip(this.chk_comments, "Show element comment over it. If Show Element Names is checked, both are shown on" +
        " the same comment line.");
            this.chk_comments.UseVisualStyleBackColor = true;
            // 
            // chk_names
            // 
            this.chk_names.AutoSize = true;
            this.chk_names.Enabled = false;
            this.chk_names.Location = new System.Drawing.Point(16, 91);
            this.chk_names.Name = "chk_names";
            this.chk_names.Size = new System.Drawing.Size(130, 17);
            this.chk_names.TabIndex = 2;
            this.chk_names.Text = "Show Element Names";
            this.toolTip1.SetToolTip(this.chk_names, "Show element name with a comment over the function that creates it.");
            this.chk_names.UseVisualStyleBackColor = true;
            // 
            // lbl_Gumpname
            // 
            this.lbl_Gumpname.Location = new System.Drawing.Point(13, 16);
            this.lbl_Gumpname.Name = "lbl_Gumpname";
            this.lbl_Gumpname.Size = new System.Drawing.Size(136, 16);
            this.lbl_Gumpname.TabIndex = 1;
            this.lbl_Gumpname.Text = "Gump name:";
            // 
            // txt_Gumpname
            // 
            this.txt_Gumpname.Location = new System.Drawing.Point(16, 35);
            this.txt_Gumpname.Name = "txt_Gumpname";
            this.txt_Gumpname.Size = new System.Drawing.Size(136, 20);
            this.txt_Gumpname.TabIndex = 0;
            this.txt_Gumpname.Text = "gump";
            // 
            // txt_Savefile
            // 
            this.txt_Savefile.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txt_Savefile.Enabled = false;
            this.txt_Savefile.Location = new System.Drawing.Point(8, 24);
            this.txt_Savefile.Name = "txt_Savefile";
            this.txt_Savefile.Size = new System.Drawing.Size(224, 20);
            this.txt_Savefile.TabIndex = 3;
            // 
            // grp_Saveas
            // 
            this.grp_Saveas.Controls.Add(this.btn_Browse);
            this.grp_Saveas.Controls.Add(this.txt_Savefile);
            this.grp_Saveas.Location = new System.Drawing.Point(0, 177);
            this.grp_Saveas.Name = "grp_Saveas";
            this.grp_Saveas.Size = new System.Drawing.Size(328, 56);
            this.grp_Saveas.TabIndex = 4;
            this.grp_Saveas.TabStop = false;
            this.grp_Saveas.Text = "Save As...";
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(240, 21);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(80, 24);
            this.btn_Browse.TabIndex = 4;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(146, 238);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(88, 24);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Location = new System.Drawing.Point(240, 239);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(88, 23);
            this.btn_Export.TabIndex = 6;
            this.btn_Export.Text = "Export";
            this.btn_Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // PolExportForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(337, 275);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.grp_Properties);
            this.Controls.Add(this.grp_Gumptype);
            this.Controls.Add(this.lbl_Help);
            this.Controls.Add(this.grp_Saveas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(256, 184);
            this.Name = "PolExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "POL Export Options";
            this.grp_Gumptype.ResumeLayout(false);
            this.grp_Properties.ResumeLayout(false);
            this.grp_Properties.PerformLayout();
            this.grp_Saveas.ResumeLayout(false);
            this.grp_Saveas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Help;
        private System.Windows.Forms.GroupBox grp_Gumptype;
        private System.Windows.Forms.RadioButton rbt_Bare;
        private System.Windows.Forms.RadioButton rbt_Distro;
        private System.Windows.Forms.GroupBox grp_Properties;
        private System.Windows.Forms.TextBox txt_Gumpname;
        private System.Windows.Forms.Label lbl_Gumpname;
        private System.Windows.Forms.TextBox txt_Savefile;
        private System.Windows.Forms.GroupBox grp_Saveas;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.CheckBox chk_comments;
        private System.Windows.Forms.CheckBox chk_names;
        private System.Windows.Forms.CheckBox chk_DefaultTexts;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
