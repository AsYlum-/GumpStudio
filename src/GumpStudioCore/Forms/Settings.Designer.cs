using System.ComponentModel;
using System.Windows.Forms;

namespace GumpStudio.Forms
{
    public partial class Settings : Form
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
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.OK_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.txtClientPath = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.NumericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Label2 = new System.Windows.Forms.Label();
            this.TrackBar1 = new System.Windows.Forms.TrackBar();
            this.Label3 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.TableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.NumericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.TrackBar1)).BeginInit();
            this.SuspendLayout();

            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 2;

            this.TableLayoutPanel1.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            this.TableLayoutPanel1.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this.OK_Button, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.Cancel_Button, 1, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(323, 316);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;

            this.TableLayoutPanel1.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(170, 33);
            this.TableLayoutPanel1.TabIndex = 0;
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OK_Button.Location = new System.Drawing.Point(3, 3);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(78, 27);
            this.OK_Button.TabIndex = 0;
            this.OK_Button.Text = "OK";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(88, 3);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(78, 27);
            this.Cancel_Button.TabIndex = 1;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);

            // 
            // txtClientPath
            // 
            this.txtClientPath.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientPath.Location = new System.Drawing.Point(99, 14);
            this.txtClientPath.Name = "txtClientPath";
            this.txtClientPath.Size = new System.Drawing.Size(355, 23);
            this.txtClientPath.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(14, 17);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(65, 15);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Client Path";
            // 
            // NumericUpDown1
            // 
            this.NumericUpDown1.Location = new System.Drawing.Point(99, 50);
            this.NumericUpDown1.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.NumericUpDown1.Name = "NumericUpDown1";
            this.NumericUpDown1.Size = new System.Drawing.Size(61, 23);
            this.NumericUpDown1.TabIndex = 3;
            this.NumericUpDown1.Value = new decimal(new int[] {1, 0, 0, 0});
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(14, 52);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(71, 15);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "Undo Levels";

            // 
            // TrackBar1
            // 
            this.TrackBar1.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.TrackBar1.Location = new System.Drawing.Point(17, 108);
            this.TrackBar1.Maximum = 100;
            this.TrackBar1.Name = "TrackBar1";
            this.TrackBar1.Size = new System.Drawing.Size(472, 45);
            this.TrackBar1.TabIndex = 5;
            this.TrackBar1.Value = 6;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(14, 90);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(127, 15);
            this.Label3.TabIndex = 6;
            this.Label3.Text = "Arrow key acceleration";

            // 
            // Button1
            // 
            this.Button1.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.Location = new System.Drawing.Point(462, 14);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(31, 23);
            this.Button1.TabIndex = 7;
            this.Button1.Text = "...";
            this.Button1.UseVisualStyleBackColor = true;
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(17, 156);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(142, 19);
            this.CheckBox1.TabIndex = 8;
            this.CheckBox1.Text = "Pixel Perfect Selection";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AcceptButton = this.OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(507, 363);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.TrackBar1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.NumericUpDown1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtClientPath);
            this.Controls.Add(this.TableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.TableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.NumericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.TrackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtClientPath;
        private System.Windows.Forms.TrackBar TrackBar1;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        private System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.NumericUpDown NumericUpDown1;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.CheckBox CheckBox1;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.Button Button1;
    }
}
