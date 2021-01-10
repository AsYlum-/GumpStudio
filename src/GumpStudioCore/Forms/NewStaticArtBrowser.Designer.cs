using System.ComponentModel;
using System.Windows.Forms;

namespace GumpStudio.Forms
{
    partial class NewStaticArtBrowser : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewStaticArtBrowser));
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.VScrollBar = new System.Windows.Forms.VScrollBar();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.cmdCache = new System.Windows.Forms.Button();
            this.lblWait = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // picCanvas
            // 
            this.picCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.picCanvas.Location = new System.Drawing.Point(0, 0);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(488, 396);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.PicCanvas_Paint);
            this.picCanvas.DoubleClick += new System.EventHandler(this.PicCanvas_DoubleClick);
            this.picCanvas.MouseLeave += new System.EventHandler(this.PicCanvas_MouseLeave);
            this.picCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicCanvas_MouseMove);
            this.picCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicCanvas_MouseUp);
            // 
            // VScrollBar
            // 
            this.VScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.VScrollBar.Location = new System.Drawing.Point(488, 0);
            this.VScrollBar.Name = "VScrollBar";
            this.VScrollBar.Size = new System.Drawing.Size(17, 396);
            this.VScrollBar.TabIndex = 3;
            this.VScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VScroll_Scroll);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(50, 404);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 4;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Location = new System.Drawing.Point(156, 403);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(50, 21);
            this.cmdSearch.TabIndex = 5;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.Click += new System.EventHandler(this.CmdSearch_Click);
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(216, 405);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "Name:";
            // 
            // lblSize
            // 
            this.lblSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(409, 405);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(30, 13);
            this.lblSize.TabIndex = 7;
            this.lblSize.Text = "Size:";
            // 
            // cmdCache
            // 
            this.cmdCache.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCache.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCache.Image = ((System.Drawing.Image)(resources.GetObject("cmdCache.Image")));
            this.cmdCache.Location = new System.Drawing.Point(12, 403);
            this.cmdCache.Name = "cmdCache";
            this.cmdCache.Size = new System.Drawing.Size(33, 22);
            this.cmdCache.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.cmdCache, "Rebuild Art Cache");
            this.cmdCache.Click += new System.EventHandler(this.CmdCache_Click);
            // 
            // lblWait
            // 
            this.lblWait.BackColor = System.Drawing.Color.Transparent;
            this.lblWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWait.Location = new System.Drawing.Point(164, 159);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(184, 104);
            this.lblWait.TabIndex = 10;
            this.lblWait.Text = "Please Wait, Generating Static Art Cache...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWait.Visible = false;
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(216, 429);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 13);
            this.lblID.TabIndex = 11;
            this.lblID.Text = "ID:";
            // 
            // NewStaticArtBrowser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(505, 451);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.cmdCache);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.VScrollBar);
            this.Controls.Add(this.picCanvas);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(521, 900);
            this.MinimumSize = new System.Drawing.Size(521, 200);
            this.Name = "NewStaticArtBrowser";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Static Art Browser";
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.VScrollBar VScrollBar;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ToolTip ToolTip1;
        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Button cmdCache;
    }
}
