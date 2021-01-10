using System.ComponentModel;
using System.Windows.Forms;

namespace GumpStudio.Forms
{
    partial class AboutBoxForm : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBoxForm));
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtAbout = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblHomepage = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(0, 0);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(454, 158);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // txtAbout
            // 
            this.txtAbout.Location = new System.Drawing.Point(192, 80);
            this.txtAbout.Multiline = true;
            this.txtAbout.Name = "txtAbout";
            this.txtAbout.ReadOnly = true;
            this.txtAbout.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAbout.Size = new System.Drawing.Size(248, 152);
            this.txtAbout.TabIndex = 1;
            this.txtAbout.Text = "Gump Studio was written by Bradley Uffner in January of 2003.";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(368, 240);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 0;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(8, 168);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(176, 23);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "(C) Bradley Uffner, 2004";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(8, 248);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "Version";
            // 
            // lblHomepage
            // 
            this.lblHomepage.Location = new System.Drawing.Point(8, 192);
            this.lblHomepage.Name = "lblHomepage";
            this.lblHomepage.Size = new System.Drawing.Size(168, 23);
            this.lblHomepage.TabIndex = 5;
            this.lblHomepage.TabStop = true;
            this.lblHomepage.Text = "http://www.gumpstudio.com";
            this.lblHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblHomepage_LinkClicked);
            // 
            // AboutBoxForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(454, 276);
            this.Controls.Add(this.lblHomepage);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.txtAbout);
            this.Controls.Add(this.PictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutBoxForm";
            this.Text = "About Gump Studio.NET";
            this.Load += new System.EventHandler(this.AboutBoxForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAbout;
        private System.Windows.Forms.PictureBox PictureBox1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.LinkLabel lblHomepage;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button cmdClose;
    }
}
