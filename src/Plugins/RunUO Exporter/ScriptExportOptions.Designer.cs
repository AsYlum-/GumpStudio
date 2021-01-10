namespace RunUOExporter
{
    partial class ScriptExportOptions : System.Windows.Forms.Form
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
            this.components = new System.ComponentModel.Container();
            this.ExportTypeSphere99 = new System.Windows.Forms.RadioButton();
            this.ExportTypeRUVB = new System.Windows.Forms.RadioButton();
            this.ExportTypeRUCS = new System.Windows.Forms.RadioButton();
            this.ExportLabel = new System.Windows.Forms.Label();
            this.Generate = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.ExportType = new System.Windows.Forms.GroupBox();
            this.RunUOOptions = new System.Windows.Forms.GroupBox();
            this.ClipboardPanel = new System.Windows.Forms.Panel();
            this.CheckEnums = new System.Windows.Forms.CheckBox();
            this.ExtraOptions = new System.Windows.Forms.CheckBox();
            this.ClipSaveNameClass = new System.Windows.Forms.CheckBox();
            this.SaveTypeClip = new System.Windows.Forms.RadioButton();
            this.SaveTypeFile = new System.Windows.Forms.RadioButton();
            this.NameClassPanel = new System.Windows.Forms.Panel();
            this.NamespaceTxt = new System.Windows.Forms.TextBox();
            this.NamespaceLabel = new System.Windows.Forms.Label();
            this.ClassnameTxt = new System.Windows.Forms.TextBox();
            this.ClassnameLabel = new System.Windows.Forms.Label();
            this.OptionsGroup = new System.Windows.Forms.GroupBox();
            this.Y = new System.Windows.Forms.TextBox();
            this.LocLabelY = new System.Windows.Forms.Label();
            this.X = new System.Windows.Forms.TextBox();
            this.LocLabelX = new System.Windows.Forms.Label();
            this.CheckDisposable = new System.Windows.Forms.CheckBox();
            this.CheckResizable = new System.Windows.Forms.CheckBox();
            this.CheckClosable = new System.Windows.Forms.CheckBox();
            this.CheckMovable = new System.Windows.Forms.CheckBox();
            this.SaveScriptAs = new System.Windows.Forms.SaveFileDialog();
            this.CaseStyle = new System.Windows.Forms.GroupBox();
            this.IntStyle = new System.Windows.Forms.RadioButton();
            this.EnumStyle = new System.Windows.Forms.RadioButton();
            this.CommandCallCheck = new System.Windows.Forms.CheckBox();
            this.CommandCallTextBox = new System.Windows.Forms.TextBox();
            this.RuoVersionGroup = new System.Windows.Forms.GroupBox();
            this.Vinfo2_0 = new System.Windows.Forms.RadioButton();
            this.Vinfo1_0 = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ExportType.SuspendLayout();
            this.RunUOOptions.SuspendLayout();
            this.ClipboardPanel.SuspendLayout();
            this.NameClassPanel.SuspendLayout();
            this.OptionsGroup.SuspendLayout();
            this.CaseStyle.SuspendLayout();
            this.RuoVersionGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExportTypeSphere99
            // 
            this.ExportTypeSphere99.Enabled = false;
            this.ExportTypeSphere99.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExportTypeSphere99.Location = new System.Drawing.Point(10, 60);
            this.ExportTypeSphere99.Name = "ExportTypeSphere99";
            this.ExportTypeSphere99.Size = new System.Drawing.Size(115, 15);
            this.ExportTypeSphere99.TabIndex = 2;
            this.ExportTypeSphere99.Text = "Sphere 99+";
            this.toolTip1.SetToolTip(this.ExportTypeSphere99, "Currently unsupported");
            // 
            // ExportTypeRUVB
            // 
            this.ExportTypeRUVB.Enabled = false;
            this.ExportTypeRUVB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExportTypeRUVB.Location = new System.Drawing.Point(10, 39);
            this.ExportTypeRUVB.Name = "ExportTypeRUVB";
            this.ExportTypeRUVB.Size = new System.Drawing.Size(134, 15);
            this.ExportTypeRUVB.TabIndex = 1;
            this.ExportTypeRUVB.Text = "RunUO V&B.NET";
            this.toolTip1.SetToolTip(this.ExportTypeRUVB, "Currently unsupported");
            // 
            // ExportTypeRUCS
            // 
            this.ExportTypeRUCS.Checked = true;
            this.ExportTypeRUCS.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExportTypeRUCS.Location = new System.Drawing.Point(10, 20);
            this.ExportTypeRUCS.Name = "ExportTypeRUCS";
            this.ExportTypeRUCS.Size = new System.Drawing.Size(124, 15);
            this.ExportTypeRUCS.TabIndex = 0;
            this.ExportTypeRUCS.TabStop = true;
            this.ExportTypeRUCS.Text = "&Export to RunUo";
            this.toolTip1.SetToolTip(this.ExportTypeRUCS,
                "Should be checked currently by default, \r\nthis option needs to be checked in orde" +
                "r \r\nto export to a RunUo C# sytle script file.");
            this.ExportTypeRUCS.CheckedChanged += new System.EventHandler(this.ExportTypeRUCS_CheckedChanged);
            // 
            // ExportLabel
            // 
            this.ExportLabel.Location = new System.Drawing.Point(17, 9);
            this.ExportLabel.Name = "ExportLabel";
            this.ExportLabel.Size = new System.Drawing.Size(377, 19);
            this.ExportLabel.TabIndex = 1;
            this.ExportLabel.Text = "Please select your export options:";
            // 
            // Generate
            // 
            this.Generate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Generate.Location = new System.Drawing.Point(212, 335);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(182, 22);
            this.Generate.TabIndex = 3;
            this.Generate.Text = "&Generate";
            this.toolTip1.SetToolTip(this.Generate, "When ready push this button to \r\nGenerate your Script");
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Cancel.Location = new System.Drawing.Point(10, 335);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(175, 22);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "&Cancel";
            // 
            // ExportType
            // 
            this.ExportType.Controls.Add(this.ExportTypeSphere99);
            this.ExportType.Controls.Add(this.ExportTypeRUVB);
            this.ExportType.Controls.Add(this.ExportTypeRUCS);
            this.ExportType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExportType.Location = new System.Drawing.Point(10, 31);
            this.ExportType.Name = "ExportType";
            this.ExportType.Size = new System.Drawing.Size(153, 91);
            this.ExportType.TabIndex = 0;
            this.ExportType.TabStop = false;
            this.ExportType.Text = "Export Options";
            // 
            // RunUOOptions
            // 
            this.RunUOOptions.Controls.Add(this.ClipboardPanel);
            this.RunUOOptions.Controls.Add(this.SaveTypeClip);
            this.RunUOOptions.Controls.Add(this.SaveTypeFile);
            this.RunUOOptions.Controls.Add(this.NameClassPanel);
            this.RunUOOptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RunUOOptions.Location = new System.Drawing.Point(10, 125);
            this.RunUOOptions.Name = "RunUOOptions";
            this.RunUOOptions.Size = new System.Drawing.Size(384, 89);
            this.RunUOOptions.TabIndex = 2;
            this.RunUOOptions.TabStop = false;
            this.RunUOOptions.Text = "Main Control Options";
            // 
            // ClipboardPanel
            // 
            this.ClipboardPanel.Controls.Add(this.CheckEnums);
            this.ClipboardPanel.Controls.Add(this.ExtraOptions);
            this.ClipboardPanel.Controls.Add(this.ClipSaveNameClass);
            this.ClipboardPanel.Enabled = false;
            this.ClipboardPanel.Location = new System.Drawing.Point(230, 30);
            this.ClipboardPanel.Name = "ClipboardPanel";
            this.ClipboardPanel.Size = new System.Drawing.Size(135, 52);
            this.ClipboardPanel.TabIndex = 7;
            // 
            // CheckEnums
            // 
            this.CheckEnums.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckEnums.Location = new System.Drawing.Point(7, 34);
            this.CheckEnums.Name = "CheckEnums";
            this.CheckEnums.Size = new System.Drawing.Size(115, 15);
            this.CheckEnums.TabIndex = 2;
            this.CheckEnums.Text = "Enums";
            // 
            // ExtraOptions
            // 
            this.ExtraOptions.Checked = true;
            this.ExtraOptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ExtraOptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExtraOptions.Location = new System.Drawing.Point(7, 4);
            this.ExtraOptions.Name = "ExtraOptions";
            this.ExtraOptions.Size = new System.Drawing.Size(115, 15);
            this.ExtraOptions.TabIndex = 1;
            this.ExtraOptions.Text = "Options";
            this.ExtraOptions.CheckedChanged += new System.EventHandler(this.ExtraOptions_CheckedChanged);
            // 
            // ClipSaveNameClass
            // 
            this.ClipSaveNameClass.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ClipSaveNameClass.Location = new System.Drawing.Point(7, 19);
            this.ClipSaveNameClass.Name = "ClipSaveNameClass";
            this.ClipSaveNameClass.Size = new System.Drawing.Size(115, 15);
            this.ClipSaveNameClass.TabIndex = 0;
            this.ClipSaveNameClass.Text = "Namespace";
            this.ClipSaveNameClass.CheckedChanged += new System.EventHandler(this.ClipSaveNameClass_CheckedChanged);
            // 
            // SaveTypeClip
            // 
            this.SaveTypeClip.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SaveTypeClip.Location = new System.Drawing.Point(230, 15);
            this.SaveTypeClip.Name = "SaveTypeClip";
            this.SaveTypeClip.Size = new System.Drawing.Size(144, 15);
            this.SaveTypeClip.TabIndex = 5;
            this.SaveTypeClip.Text = "Copy to Clipboard";
            this.SaveTypeClip.CheckedChanged += new System.EventHandler(this.SaveTypeClip_CheckedChanged);
            // 
            // SaveTypeFile
            // 
            this.SaveTypeFile.Checked = true;
            this.SaveTypeFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SaveTypeFile.Location = new System.Drawing.Point(10, 15);
            this.SaveTypeFile.Name = "SaveTypeFile";
            this.SaveTypeFile.Size = new System.Drawing.Size(105, 15);
            this.SaveTypeFile.TabIndex = 4;
            this.SaveTypeFile.TabStop = true;
            this.SaveTypeFile.Text = "Save to File";
            // 
            // NameClassPanel
            // 
            this.NameClassPanel.Controls.Add(this.NamespaceTxt);
            this.NameClassPanel.Controls.Add(this.NamespaceLabel);
            this.NameClassPanel.Controls.Add(this.ClassnameTxt);
            this.NameClassPanel.Controls.Add(this.ClassnameLabel);
            this.NameClassPanel.Location = new System.Drawing.Point(10, 30);
            this.NameClassPanel.Name = "NameClassPanel";
            this.NameClassPanel.Size = new System.Drawing.Size(211, 52);
            this.NameClassPanel.TabIndex = 6;
            // 
            // NamespaceTxt
            // 
            this.NamespaceTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NamespaceTxt.Location = new System.Drawing.Point(86, 7);
            this.NamespaceTxt.Name = "NamespaceTxt";
            this.NamespaceTxt.Size = new System.Drawing.Size(116, 21);
            this.NamespaceTxt.TabIndex = 1;
            this.NamespaceTxt.Text = "Server.Gumps";
            // 
            // NamespaceLabel
            // 
            this.NamespaceLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.NamespaceLabel.Location = new System.Drawing.Point(0, 7);
            this.NamespaceLabel.Name = "NamespaceLabel";
            this.NamespaceLabel.Size = new System.Drawing.Size(86, 15);
            this.NamespaceLabel.TabIndex = 0;
            this.NamespaceLabel.Text = "Namespace:";
            // 
            // ClassnameTxt
            // 
            this.ClassnameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ClassnameTxt.Location = new System.Drawing.Point(86, 30);
            this.ClassnameTxt.Name = "ClassnameTxt";
            this.ClassnameTxt.Size = new System.Drawing.Size(116, 21);
            this.ClassnameTxt.TabIndex = 3;
            this.ClassnameTxt.Text = "MyGump";
            // 
            // ClassnameLabel
            // 
            this.ClassnameLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ClassnameLabel.Location = new System.Drawing.Point(0, 30);
            this.ClassnameLabel.Name = "ClassnameLabel";
            this.ClassnameLabel.Size = new System.Drawing.Size(86, 15);
            this.ClassnameLabel.TabIndex = 2;
            this.ClassnameLabel.Text = "Class Name:";
            // 
            // OptionsGroup
            // 
            this.OptionsGroup.Controls.Add(this.Y);
            this.OptionsGroup.Controls.Add(this.LocLabelY);
            this.OptionsGroup.Controls.Add(this.X);
            this.OptionsGroup.Controls.Add(this.LocLabelX);
            this.OptionsGroup.Controls.Add(this.CheckDisposable);
            this.OptionsGroup.Controls.Add(this.CheckResizable);
            this.OptionsGroup.Controls.Add(this.CheckClosable);
            this.OptionsGroup.Controls.Add(this.CheckMovable);
            this.OptionsGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.OptionsGroup.Location = new System.Drawing.Point(174, 31);
            this.OptionsGroup.Name = "OptionsGroup";
            this.OptionsGroup.Size = new System.Drawing.Size(220, 91);
            this.OptionsGroup.TabIndex = 1;
            this.OptionsGroup.TabStop = false;
            this.OptionsGroup.Text = "Gump Options";
            // 
            // Y
            // 
            this.Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Y.Location = new System.Drawing.Point(144, 15);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(38, 21);
            this.Y.TabIndex = 7;
            this.Y.Text = "0";
            // 
            // LocLabelY
            // 
            this.LocLabelY.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LocLabelY.Location = new System.Drawing.Point(115, 17);
            this.LocLabelY.Name = "LocLabelY";
            this.LocLabelY.Size = new System.Drawing.Size(29, 15);
            this.LocLabelY.TabIndex = 6;
            this.LocLabelY.Text = "Y:";
            this.toolTip1.SetToolTip(this.LocLabelY, "Y starting position of your gump");
            // 
            // X
            // 
            this.X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.X.Location = new System.Drawing.Point(38, 15);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(39, 21);
            this.X.TabIndex = 5;
            this.X.Text = "0";
            // 
            // LocLabelX
            // 
            this.LocLabelX.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LocLabelX.Location = new System.Drawing.Point(10, 17);
            this.LocLabelX.Name = "LocLabelX";
            this.LocLabelX.Size = new System.Drawing.Size(28, 15);
            this.LocLabelX.TabIndex = 4;
            this.LocLabelX.Text = "X:";
            this.toolTip1.SetToolTip(this.LocLabelX, "X starting position of your gump");
            // 
            // CheckDisposable
            // 
            this.CheckDisposable.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckDisposable.Location = new System.Drawing.Point(115, 60);
            this.CheckDisposable.Name = "CheckDisposable";
            this.CheckDisposable.Size = new System.Drawing.Size(96, 15);
            this.CheckDisposable.TabIndex = 3;
            this.CheckDisposable.Text = "Disposable";
            // 
            // CheckResizable
            // 
            this.CheckResizable.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckResizable.Location = new System.Drawing.Point(115, 37);
            this.CheckResizable.Name = "CheckResizable";
            this.CheckResizable.Size = new System.Drawing.Size(96, 15);
            this.CheckResizable.TabIndex = 1;
            this.CheckResizable.Text = "Resizable";
            this.toolTip1.SetToolTip(this.CheckResizable, "Currently Unsupported");
            // 
            // CheckClosable
            // 
            this.CheckClosable.Checked = true;
            this.CheckClosable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckClosable.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckClosable.Location = new System.Drawing.Point(10, 60);
            this.CheckClosable.Name = "CheckClosable";
            this.CheckClosable.Size = new System.Drawing.Size(96, 15);
            this.CheckClosable.TabIndex = 2;
            this.CheckClosable.Text = "Closable";
            // 
            // CheckMovable
            // 
            this.CheckMovable.Checked = true;
            this.CheckMovable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckMovable.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckMovable.Location = new System.Drawing.Point(10, 37);
            this.CheckMovable.Name = "CheckMovable";
            this.CheckMovable.Size = new System.Drawing.Size(86, 15);
            this.CheckMovable.TabIndex = 0;
            this.CheckMovable.Text = "Movable";
            // 
            // SaveScriptAs
            // 
            this.SaveScriptAs.Title = "Save as...";
            this.SaveScriptAs.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk);
            // 
            // CaseStyle
            // 
            this.CaseStyle.Controls.Add(this.IntStyle);
            this.CaseStyle.Controls.Add(this.EnumStyle);
            this.CaseStyle.Location = new System.Drawing.Point(13, 220);
            this.CaseStyle.Name = "CaseStyle";
            this.CaseStyle.Size = new System.Drawing.Size(172, 64);
            this.CaseStyle.TabIndex = 5;
            this.CaseStyle.TabStop = false;
            this.CaseStyle.Text = "RunUo case style";
            // 
            // IntStyle
            // 
            this.IntStyle.AutoSize = true;
            this.IntStyle.Location = new System.Drawing.Point(11, 37);
            this.IntStyle.Name = "IntStyle";
            this.IntStyle.Size = new System.Drawing.Size(102, 19);
            this.IntStyle.TabIndex = 1;
            this.IntStyle.TabStop = true;
            this.IntStyle.Text = "(int) case style";
            this.toolTip1.SetToolTip(this.IntStyle,
                "Check here if you want your gump to use \r\nthe regular (int) style of case stateme" +
                "nts, \r\nie... case 1:");
            this.IntStyle.UseVisualStyleBackColor = true;
            this.IntStyle.CheckedChanged += new System.EventHandler(this.IntStyle_CheckedChanged);
            // 
            // EnumStyle
            // 
            this.EnumStyle.AutoSize = true;
            this.EnumStyle.Location = new System.Drawing.Point(11, 17);
            this.EnumStyle.Name = "EnumStyle";
            this.EnumStyle.Size = new System.Drawing.Size(113, 19);
            this.EnumStyle.TabIndex = 0;
            this.EnumStyle.TabStop = true;
            this.EnumStyle.Text = "enum case style";
            this.toolTip1.SetToolTip(this.EnumStyle,
                "Check here if you want your gump to use \r\nthe Enum Style button names and case \r\n" +
                "statements.  ie... case (int)Buttons.Button1:");
            this.EnumStyle.UseVisualStyleBackColor = true;
            this.EnumStyle.CheckedChanged += new System.EventHandler(this.EnumStyle_CheckedChanged);
            // 
            // CommandCallCheck
            // 
            this.CommandCallCheck.AutoSize = true;
            this.CommandCallCheck.Location = new System.Drawing.Point(13, 310);
            this.CommandCallCheck.Name = "CommandCallCheck";
            this.CommandCallCheck.Size = new System.Drawing.Size(150, 19);
            this.CommandCallCheck.TabIndex = 6;
            this.CommandCallCheck.Text = "RunUo Command Call";
            this.toolTip1.SetToolTip(this.CommandCallCheck,
                "Check the Box to insert code for calling \r\nyour custom gump from using an in-game" +
                " \r\ncommand.  You must provide a command \r\nname in the textbox.");
            this.CommandCallCheck.UseVisualStyleBackColor = true;
            this.CommandCallCheck.CheckedChanged += new System.EventHandler(this.CommandCallCheck_CheckedChanged);
            // 
            // CommandCallTextBox
            // 
            this.CommandCallTextBox.Location = new System.Drawing.Point(187, 307);
            this.CommandCallTextBox.Name = "CommandCallTextBox";
            this.CommandCallTextBox.Size = new System.Drawing.Size(204, 21);
            this.CommandCallTextBox.TabIndex = 7;
            this.CommandCallTextBox.Text = "MyGump";
            this.CommandCallTextBox.TextChanged += new System.EventHandler(this.CommandCallTextBox_TextChanged);
            // 
            // RuoVersionGroup
            // 
            this.RuoVersionGroup.Controls.Add(this.Vinfo2_0);
            this.RuoVersionGroup.Controls.Add(this.Vinfo1_0);
            this.RuoVersionGroup.Location = new System.Drawing.Point(212, 220);
            this.RuoVersionGroup.Name = "RuoVersionGroup";
            this.RuoVersionGroup.Size = new System.Drawing.Size(179, 64);
            this.RuoVersionGroup.TabIndex = 8;
            this.RuoVersionGroup.TabStop = false;
            this.RuoVersionGroup.Text = "RunUo Version info";
            // 
            // Vinfo2_0
            // 
            this.Vinfo2_0.AutoSize = true;
            this.Vinfo2_0.Location = new System.Drawing.Point(7, 33);
            this.Vinfo2_0.Name = "Vinfo2_0";
            this.Vinfo2_0.Size = new System.Drawing.Size(98, 19);
            this.Vinfo2_0.TabIndex = 1;
            this.Vinfo2_0.TabStop = true;
            this.Vinfo2_0.Text = "RunUo 2_0 +";
            this.toolTip1.SetToolTip(this.Vinfo2_0, "Check here if you are using \r\nRunUo 2.0 Rc0 or higher.");
            this.Vinfo2_0.UseVisualStyleBackColor = true;
            // 
            // Vinfo1_0
            // 
            this.Vinfo1_0.AutoSize = true;
            this.Vinfo1_0.Location = new System.Drawing.Point(7, 15);
            this.Vinfo1_0.Name = "Vinfo1_0";
            this.Vinfo1_0.Size = new System.Drawing.Size(84, 19);
            this.Vinfo1_0.TabIndex = 0;
            this.Vinfo1_0.TabStop = true;
            this.Vinfo1_0.Text = "RunUo 1.0";
            this.toolTip1.SetToolTip(this.Vinfo1_0, "Check here if you are \r\nusing RunUo 1.0");
            this.Vinfo1_0.UseVisualStyleBackColor = true;
            // 
            // ScriptExportOptions
            // 
            this.AcceptButton = this.Generate;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(429, 373);
            this.Controls.Add(this.RuoVersionGroup);
            this.Controls.Add(this.CommandCallTextBox);
            this.Controls.Add(this.CommandCallCheck);
            this.Controls.Add(this.CaseStyle);
            this.Controls.Add(this.ExportType);
            this.Controls.Add(this.OptionsGroup);
            this.Controls.Add(this.RunUOOptions);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.ExportLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ScriptExportOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Script Export Options";
            this.TransparencyKey = System.Drawing.Color.Tan;
            this.ExportType.ResumeLayout(false);
            this.RunUOOptions.ResumeLayout(false);
            this.ClipboardPanel.ResumeLayout(false);
            this.NameClassPanel.ResumeLayout(false);
            this.NameClassPanel.PerformLayout();
            this.OptionsGroup.ResumeLayout(false);
            this.OptionsGroup.PerformLayout();
            this.CaseStyle.ResumeLayout(false);
            this.CaseStyle.PerformLayout();
            this.RuoVersionGroup.ResumeLayout(false);
            this.RuoVersionGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.GroupBox OptionsGroup;
        private System.Windows.Forms.GroupBox RunUOOptions;
        private System.Windows.Forms.GroupBox ExportType;
        private System.Windows.Forms.Label ExportLabel;
        private System.Windows.Forms.Label NamespaceLabel;
        private System.Windows.Forms.Label ClassnameLabel;
        private System.Windows.Forms.Label LocLabelX;
        private System.Windows.Forms.Label LocLabelY;
        private System.Windows.Forms.Panel ClipboardPanel;
        private System.Windows.Forms.Panel NameClassPanel;
        private System.Windows.Forms.SaveFileDialog SaveScriptAs;
        private System.Windows.Forms.GroupBox CaseStyle;
        private System.Windows.Forms.RadioButton EnumStyle;
        private System.Windows.Forms.RadioButton IntStyle;
        private System.Windows.Forms.CheckBox CommandCallCheck;
        private System.Windows.Forms.TextBox CommandCallTextBox;
        private System.Windows.Forms.GroupBox RuoVersionGroup;
        private System.Windows.Forms.RadioButton Vinfo2_0;
        private System.Windows.Forms.RadioButton Vinfo1_0;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton ExportTypeSphere99;
        private System.Windows.Forms.RadioButton ExportTypeRUVB;
        private System.Windows.Forms.RadioButton ExportTypeRUCS;
        internal System.Windows.Forms.CheckBox CheckClosable;
        internal System.Windows.Forms.CheckBox CheckDisposable;
        internal System.Windows.Forms.CheckBox CheckEnums;
        internal System.Windows.Forms.CheckBox CheckMovable;
        internal System.Windows.Forms.CheckBox CheckResizable;
        internal System.Windows.Forms.CheckBox ClipSaveNameClass;
        internal System.Windows.Forms.CheckBox ExtraOptions;
        internal System.Windows.Forms.RadioButton SaveTypeClip;
        internal System.Windows.Forms.RadioButton SaveTypeFile;
        internal System.Windows.Forms.TextBox ClassnameTxt;
        internal System.Windows.Forms.TextBox NamespaceTxt;
        internal System.Windows.Forms.TextBox X;
        internal System.Windows.Forms.TextBox Y;
    }
}
