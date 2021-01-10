namespace RunUOExporter
{
    partial class ClipNotice : System.Windows.Forms.Form
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
            this.OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dontshow = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OK.Location = new System.Drawing.Point(244, 53);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(105, 29);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "Your gump has been copied to the clipboard. You can now paste this gump into any " +
                               "Windows program.";
            // 
            // dontshow
            // 
            this.dontshow.Location = new System.Drawing.Point(13, 59);
            this.dontshow.Name = "dontshow";
            this.dontshow.Size = new System.Drawing.Size(202, 20);
            this.dontshow.TabIndex = 2;
            this.dontshow.Text = "Do not show this next time";
            // 
            // ClipNotice
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.CancelButton = this.OK;
            this.ClientSize = new System.Drawing.Size(358, 94);
            this.Controls.Add(this.dontshow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OK);
            this.Name = "ClipNotice";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Copy to Clipboard";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.CheckBox dontshow;
    }
}
