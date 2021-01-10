namespace ImportRunUOPlugin
{
    partial class FrmInterpret : System.Windows.Forms.Form
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
            this.txtNewValue = new System.Windows.Forms.TextBox();
            this.lblLine = new System.Windows.Forms.Label();
            this.txtLines = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(680, 352);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // txtNewValue
            // 
            this.txtNewValue.Location = new System.Drawing.Point(481, 352);
            this.txtNewValue.Name = "txtNewValue";
            this.txtNewValue.Size = new System.Drawing.Size(183, 20);
            this.txtNewValue.TabIndex = 3;
            // 
            // lblLine
            // 
            this.lblLine.Location = new System.Drawing.Point(136, 352);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(122, 23);
            this.lblLine.TabIndex = 4;
            this.lblLine.Text = "Label1";
            // 
            // txtLines
            // 
            this.txtLines.Location = new System.Drawing.Point(8, 8);
            this.txtLines.Name = "txtLines";
            this.txtLines.ReadOnly = true;
            this.txtLines.Size = new System.Drawing.Size(744, 336);
            this.txtLines.TabIndex = 5;
            this.txtLines.Text = "RichTextBox1";
            // 
            // FrmInterpret
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(762, 382);
            this.Controls.Add(this.txtLines);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.txtNewValue);
            this.Controls.Add(this.cmdOK);
            this.Name = "FrmInterpret";
            this.Text = "frmInterpret";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNewValue;
        private System.Windows.Forms.RichTextBox txtLines;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label lblLine;
    }
}
