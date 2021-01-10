using System.ComponentModel;
using System.Windows.Forms;

namespace GumpStudio.Forms
{
    partial class PluginManager : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginManager));
            this.Label1 = new System.Windows.Forms.Label();
            this.cmdMoveUp = new System.Windows.Forms.Button();
            this.cmdMoveDown = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.lstAvailable = new System.Windows.Forms.ListBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.lstLoaded = new System.Windows.Forms.ListBox();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(80, 13);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Loaded Plugins";
            // 
            // cmdMoveUp
            // 
            this.cmdMoveUp.Enabled = false;
            this.cmdMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoveUp.Image")));
            this.cmdMoveUp.Location = new System.Drawing.Point(152, 24);
            this.cmdMoveUp.Name = "cmdMoveUp";
            this.cmdMoveUp.Size = new System.Drawing.Size(24, 32);
            this.cmdMoveUp.TabIndex = 3;
            this.cmdMoveUp.Click += new System.EventHandler(this.CmdMoveUp_Click);
            // 
            // cmdMoveDown
            // 
            this.cmdMoveDown.Enabled = false;
            this.cmdMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoveDown.Image")));
            this.cmdMoveDown.Location = new System.Drawing.Point(152, 104);
            this.cmdMoveDown.Name = "cmdMoveDown";
            this.cmdMoveDown.Size = new System.Drawing.Size(24, 32);
            this.cmdMoveDown.TabIndex = 4;
            this.cmdMoveDown.Click += new System.EventHandler(this.CmdMoveDown_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdOK.Location = new System.Drawing.Point(186, 384);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(72, 23);
            this.cmdOK.TabIndex = 6;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox1.Controls.Add(this.txtDescription);
            this.GroupBox1.Controls.Add(this.txtVersion);
            this.GroupBox1.Controls.Add(this.txtEmail);
            this.GroupBox1.Controls.Add(this.txtAuthor);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Location = new System.Drawing.Point(8, 224);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(328, 152);
            this.GroupBox1.TabIndex = 7;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(72, 88);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(248, 56);
            this.txtDescription.TabIndex = 7;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(72, 64);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(248, 20);
            this.txtVersion.TabIndex = 6;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(72, 40);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(248, 20);
            this.txtEmail.TabIndex = 5;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(72, 16);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.ReadOnly = true;
            this.txtAuthor.Size = new System.Drawing.Size(248, 20);
            this.txtAuthor.TabIndex = 4;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(8, 88);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(60, 13);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "Description";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(8, 64);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(42, 13);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Version";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(8, 40);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(32, 13);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "Email";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(8, 16);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(38, 13);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Author";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(264, 384);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(64, 23);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Enabled = false;
            this.cmdAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd.Image")));
            this.cmdAdd.Location = new System.Drawing.Point(152, 56);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(40, 23);
            this.cmdAdd.TabIndex = 9;
            this.cmdAdd.Click += new System.EventHandler(this.CmdAdd_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Enabled = false;
            this.cmdRemove.Image = ((System.Drawing.Image)(resources.GetObject("cmdRemove.Image")));
            this.cmdRemove.Location = new System.Drawing.Point(152, 80);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(40, 23);
            this.cmdRemove.TabIndex = 10;
            this.cmdRemove.Click += new System.EventHandler(this.CmdRemove_Click);
            // 
            // lstAvailable
            // 
            this.lstAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstAvailable.IntegralHeight = false;
            this.lstAvailable.Location = new System.Drawing.Point(192, 24);
            this.lstAvailable.Name = "lstAvailable";
            this.lstAvailable.Size = new System.Drawing.Size(144, 192);
            this.lstAvailable.TabIndex = 11;
            this.lstAvailable.SelectedIndexChanged += new System.EventHandler(this.Plugins_SelectedIndexChanged);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(184, 8);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(87, 13);
            this.Label6.TabIndex = 12;
            this.Label6.Text = "Available Plugins";
            // 
            // lstLoaded
            // 
            this.lstLoaded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstLoaded.IntegralHeight = false;
            this.lstLoaded.Location = new System.Drawing.Point(8, 24);
            this.lstLoaded.Name = "lstLoaded";
            this.lstLoaded.Size = new System.Drawing.Size(144, 192);
            this.lstLoaded.TabIndex = 13;
            this.lstLoaded.SelectedIndexChanged += new System.EventHandler(this.Plugins_SelectedIndexChanged);
            // 
            // PluginManager
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(336, 416);
            this.Controls.Add(this.lstLoaded);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.lstAvailable);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.cmdMoveDown);
            this.Controls.Add(this.cmdMoveUp);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(352, 1200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(352, 370);
            this.Name = "PluginManager";
            this.Text = "Plugin Manager";
            this.Load += new System.EventHandler(this.PluginManager_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.ListBox lstLoaded;
        private System.Windows.Forms.ListBox lstAvailable;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdMoveUp;
        private System.Windows.Forms.Button cmdMoveDown;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdAdd;
    }
}
