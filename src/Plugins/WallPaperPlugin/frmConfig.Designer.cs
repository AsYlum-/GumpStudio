namespace WallPaperPlugin
{
    partial class FrmConfig : System.Windows.Forms.Form
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
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblBGColor = new System.Windows.Forms.Label();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.chkImage = new System.Windows.Forms.CheckBox();
            this.ColorPicker = new System.Windows.Forms.ColorDialog();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdOK.Location = new System.Drawing.Point(206, 110);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCancel.Location = new System.Drawing.Point(125, 110);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(94, 13);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Background color:";
            // 
            // lblBGColor
            // 
            this.lblBGColor.BackColor = System.Drawing.Color.Black;
            this.lblBGColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBGColor.Location = new System.Drawing.Point(108, 8);
            this.lblBGColor.Name = "lblBGColor";
            this.lblBGColor.Size = new System.Drawing.Size(48, 42);
            this.lblBGColor.TabIndex = 3;
            this.lblBGColor.Click += new System.EventHandler(this.LblBGColor_Click);
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(11, 83);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(208, 20);
            this.txtImagePath.TabIndex = 5;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdBrowse.Location = new System.Drawing.Point(225, 81);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(56, 23);
            this.cmdBrowse.TabIndex = 6;
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.Click += new System.EventHandler(this.CmdBrowse_Click);
            // 
            // chkImage
            // 
            this.chkImage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkImage.Location = new System.Drawing.Point(11, 53);
            this.chkImage.Name = "chkImage";
            this.chkImage.Size = new System.Drawing.Size(208, 24);
            this.chkImage.TabIndex = 7;
            this.chkImage.Text = "Use Image";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.ShowReadOnly = true;
            // 
            // FrmConfig
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(293, 145);
            this.Controls.Add(this.chkImage);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.lblBGColor);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Wallpaper";
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label lblBGColor;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.CheckBox chkImage;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.ColorDialog ColorPicker;
        private System.Windows.Forms.Button cmdCancel;
    }
}
