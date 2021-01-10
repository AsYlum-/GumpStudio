using System.ComponentModel;
using System.Windows.Forms;

namespace GumpStudio.Forms
{
    partial class GumpArtBrowser : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GumpArtBrowser));
            this.lstGump = new System.Windows.Forms.ListBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.picFullSize = new System.Windows.Forms.PictureBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblWait = new System.Windows.Forms.Label();
            this.cmdCache = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdOK = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFullSize)).BeginInit();
            this.SuspendLayout();
            // 
            // lstGump
            // 
            this.lstGump.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstGump.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstGump.IntegralHeight = false;
            this.lstGump.Location = new System.Drawing.Point(8, 8);
            this.lstGump.Name = "lstGump";
            this.lstGump.Size = new System.Drawing.Size(231, 458);
            this.lstGump.TabIndex = 0;
            this.lstGump.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstGump_DrawItem);
            this.lstGump.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.LstGump_MeasureItem);
            this.lstGump.SelectedIndexChanged += new System.EventHandler(this.LstGump_SelectedIndexChanged);
            this.lstGump.DoubleClick += new System.EventHandler(this.LstGump_DoubleClick);
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.AutoScroll = true;
            this.Panel1.BackColor = System.Drawing.Color.Black;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel1.Controls.Add(this.picFullSize);
            this.Panel1.Location = new System.Drawing.Point(245, 8);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(382, 432);
            this.Panel1.TabIndex = 1;
            // 
            // picFullSize
            // 
            this.picFullSize.Location = new System.Drawing.Point(0, 0);
            this.picFullSize.Name = "picFullSize";
            this.picFullSize.Size = new System.Drawing.Size(100, 50);
            this.picFullSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picFullSize.TabIndex = 0;
            this.picFullSize.TabStop = false;
            // 
            // lblSize
            // 
            this.lblSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(244, 453);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(103, 13);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = "Gump width / height";
            // 
            // lblWait
            // 
            this.lblWait.BackColor = System.Drawing.Color.Transparent;
            this.lblWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWait.Location = new System.Drawing.Point(215, 157);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(220, 115);
            this.lblWait.TabIndex = 1;
            this.lblWait.Text = "Please Wait, Generating Art Cache...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWait.Visible = false;
            // 
            // cmdCache
            // 
            this.cmdCache.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCache.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCache.Image = ((System.Drawing.Image)(resources.GetObject("cmdCache.Image")));
            this.cmdCache.Location = new System.Drawing.Point(595, 448);
            this.cmdCache.Name = "cmdCache";
            this.cmdCache.Size = new System.Drawing.Size(32, 23);
            this.cmdCache.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.cmdCache, "Rebuild Cache");
            this.cmdCache.Click += new System.EventHandler(this.CmdCache_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdOK.Location = new System.Drawing.Point(515, 448);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // GumpArtBrowser
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(635, 478);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCache);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.lstGump);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GumpArtBrowser";
            this.Text = "Gump Browser";
            this.Load += new System.EventHandler(this.GumpArtBrowser_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFullSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ToolTip1;
        private System.Windows.Forms.PictureBox picFullSize;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.ListBox lstGump;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCache;
    }
}
