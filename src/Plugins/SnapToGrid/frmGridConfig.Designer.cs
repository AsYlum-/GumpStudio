namespace SnapToGrid
{
    partial class FrmGridConfig : System.Windows.Forms.Form
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
            this.Label2 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.lblGridColor = new System.Windows.Forms.Label();
            this.ColorPicker = new System.Windows.Forms.ColorDialog();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(124, 100);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(72, 23);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(46, 100);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(72, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(6, 48);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(38, 13);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Height";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(6, 22);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 13);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Width";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.txtHeight);
            this.GroupBox1.Controls.Add(this.txtWidth);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Location = new System.Drawing.Point(12, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(106, 82);
            this.GroupBox1.TabIndex = 4;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Grid Size";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(47, 45);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(44, 20);
            this.txtHeight.TabIndex = 5;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(47, 19);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(44, 20);
            this.txtWidth.TabIndex = 4;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.lblGridColor);
            this.GroupBox2.Location = new System.Drawing.Point(124, 12);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(72, 82);
            this.GroupBox2.TabIndex = 5;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Grid Color";
            // 
            // lblGridColor
            // 
            this.lblGridColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGridColor.Location = new System.Drawing.Point(6, 16);
            this.lblGridColor.Name = "lblGridColor";
            this.lblGridColor.Size = new System.Drawing.Size(60, 58);
            this.lblGridColor.TabIndex = 0;
            this.lblGridColor.Click += new System.EventHandler(this.LblGridColor_Click);
            // 
            // ColorPicker
            // 
            this.ColorPicker.FullOpen = true;
            // 
            // FrmGridConfig
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(207, 132);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGridConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Snap to Grid Configuration";
            this.Load += new System.EventHandler(this.FrmGridConfig_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.Label lblGridColor;
        private System.Windows.Forms.ColorDialog ColorPicker;
    }
}
