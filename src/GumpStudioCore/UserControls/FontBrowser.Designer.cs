using System.ComponentModel;
using System.Windows.Forms;

namespace GumpStudio.UserControls
{
    partial class FontBrowser : UserControl
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.lstFont = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstFont
            // 
            this.lstFont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFont.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstFont.IntegralHeight = false;
            this.lstFont.Location = new System.Drawing.Point(0, 0);
            this.lstFont.Name = "lstFont";
            this.lstFont.Size = new System.Drawing.Size(173, 183);
            this.lstFont.TabIndex = 0;
            this.lstFont.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstFont_DrawItem);
            this.lstFont.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.LstFont_MeasureItem);
            this.lstFont.DoubleClick += new System.EventHandler(this.LstFont_DoubleClick);
            // 
            // FontBrowser
            // 
            this.Controls.Add(this.lstFont);
            this.Name = "FontBrowser";
            this.Size = new System.Drawing.Size(173, 183);
            this.Load += new System.EventHandler(this.FontBrowser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstFont;
    }
}
