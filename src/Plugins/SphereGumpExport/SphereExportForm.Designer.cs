namespace SphereGumpExport
{
    partial class SphereExportForm : System.Windows.Forms.Form
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
            this.lbl_Help = new System.Windows.Forms.Label();
            this.grp_Sphereversion = new System.Windows.Forms.GroupBox();
            this.rbt_Sphere56 = new System.Windows.Forms.RadioButton();
            this.rbt_Sphere1 = new System.Windows.Forms.RadioButton();
            this.grp_Properties = new System.Windows.Forms.GroupBox();
            this.lbl_Gumpname = new System.Windows.Forms.Label();
            this.txt_Gumpname = new System.Windows.Forms.TextBox();
            this.txt_Savefile = new System.Windows.Forms.TextBox();
            this.grp_Saveas = new System.Windows.Forms.GroupBox();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.grp_Sphereversion.SuspendLayout();
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
            this.lbl_Help.Text = "Select your Sphere version, a gump name and where to save the gump.";
            this.lbl_Help.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grp_Sphereversion
            // 
            this.grp_Sphereversion.Controls.Add(this.rbt_Sphere56);
            this.grp_Sphereversion.Controls.Add(this.rbt_Sphere1);
            this.grp_Sphereversion.Location = new System.Drawing.Point(0, 40);
            this.grp_Sphereversion.Name = "grp_Sphereversion";
            this.grp_Sphereversion.Size = new System.Drawing.Size(160, 80);
            this.grp_Sphereversion.TabIndex = 1;
            this.grp_Sphereversion.TabStop = false;
            this.grp_Sphereversion.Text = "Sphere Version";
            // 
            // rbt_Sphere56
            // 
            this.rbt_Sphere56.Checked = true;
            this.rbt_Sphere56.Location = new System.Drawing.Point(16, 24);
            this.rbt_Sphere56.Name = "rbt_Sphere56";
            this.rbt_Sphere56.Size = new System.Drawing.Size(128, 16);
            this.rbt_Sphere56.TabIndex = 3;
            this.rbt_Sphere56.TabStop = true;
            this.rbt_Sphere56.Text = "0.56 ( and Revision )";
            // 
            // rbt_Sphere1
            // 
            this.rbt_Sphere1.Location = new System.Drawing.Point(16, 48);
            this.rbt_Sphere1.Name = "rbt_Sphere1";
            this.rbt_Sphere1.Size = new System.Drawing.Size(128, 24);
            this.rbt_Sphere1.TabIndex = 3;
            this.rbt_Sphere1.Text = "0.99 and 1.0";
            // 
            // grp_Properties
            // 
            this.grp_Properties.Controls.Add(this.lbl_Gumpname);
            this.grp_Properties.Controls.Add(this.txt_Gumpname);
            this.grp_Properties.Location = new System.Drawing.Point(168, 40);
            this.grp_Properties.Name = "grp_Properties";
            this.grp_Properties.Size = new System.Drawing.Size(160, 80);
            this.grp_Properties.TabIndex = 2;
            this.grp_Properties.TabStop = false;
            this.grp_Properties.Text = "Properties";
            // 
            // lbl_Gumpname
            // 
            this.lbl_Gumpname.Location = new System.Drawing.Point(16, 24);
            this.lbl_Gumpname.Name = "lbl_Gumpname";
            this.lbl_Gumpname.Size = new System.Drawing.Size(136, 16);
            this.lbl_Gumpname.TabIndex = 1;
            this.lbl_Gumpname.Text = "Gump name:";
            // 
            // txt_Gumpname
            // 
            this.txt_Gumpname.Location = new System.Drawing.Point(16, 48);
            this.txt_Gumpname.Name = "txt_Gumpname";
            this.txt_Gumpname.Size = new System.Drawing.Size(136, 20);
            this.txt_Gumpname.TabIndex = 0;
            this.txt_Gumpname.Text = "d_default";
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
            this.grp_Saveas.Location = new System.Drawing.Point(0, 128);
            this.grp_Saveas.Name = "grp_Saveas";
            this.grp_Saveas.Size = new System.Drawing.Size(328, 56);
            this.grp_Saveas.TabIndex = 4;
            this.grp_Saveas.TabStop = false;
            this.grp_Saveas.Text = "Save As...";
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(240, 24);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(80, 24);
            this.btn_Browse.TabIndex = 4;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.Click += new System.EventHandler(this.Btn_Browse_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(136, 200);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(88, 24);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Location = new System.Drawing.Point(232, 200);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(88, 23);
            this.btn_Export.TabIndex = 6;
            this.btn_Export.Text = "Export";
            this.btn_Export.Click += new System.EventHandler(this.Btn_Export_Click);
            // 
            // SphereExportForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(334, 234);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.grp_Properties);
            this.Controls.Add(this.grp_Sphereversion);
            this.Controls.Add(this.lbl_Help);
            this.Controls.Add(this.grp_Saveas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(256, 184);
            this.Name = "SphereExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sphere Export Options";
            this.grp_Sphereversion.ResumeLayout(false);
            this.grp_Properties.ResumeLayout(false);
            this.grp_Properties.PerformLayout();
            this.grp_Saveas.ResumeLayout(false);
            this.grp_Saveas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Help;
        private System.Windows.Forms.GroupBox grp_Sphereversion;
        private System.Windows.Forms.RadioButton rbt_Sphere56;
        private System.Windows.Forms.RadioButton rbt_Sphere1;
        private System.Windows.Forms.GroupBox grp_Properties;
        private System.Windows.Forms.TextBox txt_Gumpname;
        private System.Windows.Forms.Label lbl_Gumpname;
        private System.Windows.Forms.TextBox txt_Savefile;
        private System.Windows.Forms.GroupBox grp_Saveas;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Export;
    }
}
