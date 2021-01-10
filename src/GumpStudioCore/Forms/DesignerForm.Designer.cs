using System.ComponentModel;
using System.Windows.Forms;

namespace GumpStudio.Forms
{
    partial class DesignerForm : Form
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
            this.pnlToolboxHolder = new System.Windows.Forms.Panel();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.tabToolbox = new System.Windows.Forms.TabControl();
            this.tpgStandard = new System.Windows.Forms.TabPage();
            this.pnlToolbox = new System.Windows.Forms.Panel();
            this.tpgCustom = new System.Windows.Forms.TabPage();
            this.treGumplings = new System.Windows.Forms.TreeView();
            this.Label1 = new System.Windows.Forms.Label();
            this.StatusBar = new System.Windows.Forms.StatusBar();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.pnlCanvasScroller = new System.Windows.Forms.Panel();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.TabPager = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Splitter2 = new System.Windows.Forms.Splitter();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.cboElements = new System.Windows.Forms.ComboBox();
            this.pgElementProperties = new System.Windows.Forms.PropertyGrid();
            this.txbCanvasFocus = new System.Windows.Forms.TextBox();
            this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_mnuContextMenu = new System.Windows.Forms.ContextMenu();
            this.m_MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuFileNew = new System.Windows.Forms.MenuItem();
            this.m_MenuItem9 = new System.Windows.Forms.MenuItem();
            this.mnuFileOpen = new System.Windows.Forms.MenuItem();
            this.mnuFileSave = new System.Windows.Forms.MenuItem();
            this.mnuFileImport = new System.Windows.Forms.MenuItem();
            this.mnuFileExport = new System.Windows.Forms.MenuItem();
            this.m_MenuItem5 = new System.Windows.Forms.MenuItem();
            this.mnuFileExit = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuEditUndo = new System.Windows.Forms.MenuItem();
            this.mnuEditRedo = new System.Windows.Forms.MenuItem();
            this.m_MenuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuCut = new System.Windows.Forms.MenuItem();
            this.m_mnuCopy = new System.Windows.Forms.MenuItem();
            this.mnuPaste = new System.Windows.Forms.MenuItem();
            this.mnuDelete = new System.Windows.Forms.MenuItem();
            this.m_MenuItem4 = new System.Windows.Forms.MenuItem();
            this.mnuSelectAll = new System.Windows.Forms.MenuItem();
            this.mnuMisc = new System.Windows.Forms.MenuItem();
            this.mnuMiscLoadGumpling = new System.Windows.Forms.MenuItem();
            this.mnuImportGumpling = new System.Windows.Forms.MenuItem();
            this.mnuDataFile = new System.Windows.Forms.MenuItem();
            this.mnuPage = new System.Windows.Forms.MenuItem();
            this.mnuPageAdd = new System.Windows.Forms.MenuItem();
            this.mnuPageInsert = new System.Windows.Forms.MenuItem();
            this.mnuPageDelete = new System.Windows.Forms.MenuItem();
            this.mnuPageClear = new System.Windows.Forms.MenuItem();
            this.m_MenuItem10 = new System.Windows.Forms.MenuItem();
            this.mnuShow0 = new System.Windows.Forms.MenuItem();
            this.mnuPlugins = new System.Windows.Forms.MenuItem();
            this.mnuPluginManager = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
            this.mnuGumplingContext = new System.Windows.Forms.ContextMenu();
            this.mnuGumplingRename = new System.Windows.Forms.MenuItem();
            this.mnuGumplingMove = new System.Windows.Forms.MenuItem();
            this.mnuGumplingDelete = new System.Windows.Forms.MenuItem();
            this.m_MenuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuGumplingAddGumpling = new System.Windows.Forms.MenuItem();
            this.mnuGumplingAddFolder = new System.Windows.Forms.MenuItem();
            this.pnlToolboxHolder.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.tabToolbox.SuspendLayout();
            this.tpgStandard.SuspendLayout();
            this.tpgCustom.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.pnlCanvasScroller.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.TabPager.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolboxHolder
            // 
            this.pnlToolboxHolder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlToolboxHolder.Controls.Add(this.Panel4);
            this.pnlToolboxHolder.Controls.Add(this.Label1);
            this.pnlToolboxHolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlToolboxHolder.Location = new System.Drawing.Point(0, 0);
            this.pnlToolboxHolder.Name = "pnlToolboxHolder";
            this.pnlToolboxHolder.Size = new System.Drawing.Size(128, 643);
            this.pnlToolboxHolder.TabIndex = 0;
            // 
            // Panel4
            // 
            this.Panel4.Controls.Add(this.tabToolbox);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel4.Location = new System.Drawing.Point(0, 16);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(124, 623);
            this.Panel4.TabIndex = 1;
            // 
            // tabToolbox
            // 
            this.tabToolbox.Controls.Add(this.tpgStandard);
            this.tabToolbox.Controls.Add(this.tpgCustom);
            this.tabToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabToolbox.Location = new System.Drawing.Point(0, 0);
            this.tabToolbox.Multiline = true;
            this.tabToolbox.Name = "tabToolbox";
            this.tabToolbox.SelectedIndex = 0;
            this.tabToolbox.Size = new System.Drawing.Size(124, 623);
            this.tabToolbox.TabIndex = 1;
            // 
            // tpgStandard
            // 
            this.tpgStandard.Controls.Add(this.pnlToolbox);
            this.tpgStandard.Location = new System.Drawing.Point(4, 22);
            this.tpgStandard.Name = "tpgStandard";
            this.tpgStandard.Size = new System.Drawing.Size(116, 597);
            this.tpgStandard.TabIndex = 0;
            this.tpgStandard.Text = "Standard";
            // 
            // pnlToolbox
            // 
            this.pnlToolbox.AutoScroll = true;
            this.pnlToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolbox.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbox.Name = "pnlToolbox";
            this.pnlToolbox.Size = new System.Drawing.Size(116, 597);
            this.pnlToolbox.TabIndex = 1;
            // 
            // tpgCustom
            // 
            this.tpgCustom.Controls.Add(this.treGumplings);
            this.tpgCustom.Location = new System.Drawing.Point(4, 22);
            this.tpgCustom.Name = "tpgCustom";
            this.tpgCustom.Size = new System.Drawing.Size(116, 597);
            this.tpgCustom.TabIndex = 1;
            this.tpgCustom.Text = "Gumplings";
            // 
            // treGumplings
            // 
            this.treGumplings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treGumplings.Location = new System.Drawing.Point(0, 0);
            this.treGumplings.Name = "treGumplings";
            this.treGumplings.Size = new System.Drawing.Size(116, 597);
            this.treGumplings.TabIndex = 1;
            this.treGumplings.DoubleClick += new System.EventHandler(this.TreGumplings_DoubleClick);
            this.treGumplings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreGumplings_MouseUp);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.Location = new System.Drawing.Point(0, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(124, 16);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Toolbox";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 643);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(904, 22);
            this.StatusBar.TabIndex = 0;
            // 
            // Splitter1
            // 
            this.Splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Splitter1.Location = new System.Drawing.Point(128, 0);
            this.Splitter1.MinSize = 80;
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(3, 643);
            this.Splitter1.TabIndex = 1;
            this.Splitter1.TabStop = false;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Panel2);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(131, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(773, 643);
            this.Panel1.TabIndex = 2;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.pnlCanvasScroller);
            this.Panel2.Controls.Add(this.TabPager);
            this.Panel2.Controls.Add(this.Splitter2);
            this.Panel2.Controls.Add(this.Panel3);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(773, 643);
            this.Panel2.TabIndex = 0;
            // 
            // pnlCanvasScroller
            // 
            this.pnlCanvasScroller.AutoScroll = true;
            this.pnlCanvasScroller.AutoScrollMargin = new System.Drawing.Size(1, 1);
            this.pnlCanvasScroller.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.pnlCanvasScroller.BackColor = System.Drawing.Color.Silver;
            this.pnlCanvasScroller.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCanvasScroller.Controls.Add(this.picCanvas);
            this.pnlCanvasScroller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCanvasScroller.Location = new System.Drawing.Point(0, 24);
            this.pnlCanvasScroller.Name = "pnlCanvasScroller";
            this.pnlCanvasScroller.Size = new System.Drawing.Size(503, 619);
            this.pnlCanvasScroller.TabIndex = 2;
            this.pnlCanvasScroller.MouseLeave += new System.EventHandler(this.PnlCanvasScroller_MouseLeave);
            // 
            // picCanvas
            // 
            this.picCanvas.BackColor = System.Drawing.Color.Black;
            this.picCanvas.Location = new System.Drawing.Point(0, 0);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(800, 600);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.PicCanvas_Paint);
            this.picCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicCanvas_MouseDown);
            this.picCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicCanvas_MouseMove);
            this.picCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicCanvas_MouseUp);
            // 
            // TabPager
            // 
            this.TabPager.Controls.Add(this.TabPage1);
            this.TabPager.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabPager.HotTrack = true;
            this.TabPager.Location = new System.Drawing.Point(0, 0);
            this.TabPager.Name = "TabPager";
            this.TabPager.SelectedIndex = 0;
            this.TabPager.Size = new System.Drawing.Size(503, 24);
            this.TabPager.TabIndex = 3;
            this.TabPager.SelectedIndexChanged += new System.EventHandler(this.TabPager_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(495, 0);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "0";
            // 
            // Splitter2
            // 
            this.Splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.Splitter2.Location = new System.Drawing.Point(503, 0);
            this.Splitter2.Name = "Splitter2";
            this.Splitter2.Size = new System.Drawing.Size(22, 643);
            this.Splitter2.TabIndex = 1;
            this.Splitter2.TabStop = false;
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.cboElements);
            this.Panel3.Controls.Add(this.pgElementProperties);
            this.Panel3.Controls.Add(this.txbCanvasFocus);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.Panel3.Location = new System.Drawing.Point(525, 0);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(248, 643);
            this.Panel3.TabIndex = 0;
            // 
            // cboElements
            // 
            this.cboElements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboElements.Location = new System.Drawing.Point(0, 8);
            this.cboElements.Name = "cboElements";
            this.cboElements.Size = new System.Drawing.Size(240, 21);
            this.cboElements.TabIndex = 1;
            this.cboElements.SelectedIndexChanged += new System.EventHandler(this.CboElements_SelectedIndexChanged);
            this.cboElements.Click += new System.EventHandler(this.CboElements_Click);
            // 
            // pgElementProperties
            // 
            this.pgElementProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgElementProperties.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.pgElementProperties.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pgElementProperties.Location = new System.Drawing.Point(0, 32);
            this.pgElementProperties.Name = "pgElementProperties";
            this.pgElementProperties.Size = new System.Drawing.Size(240, 608);
            this.pgElementProperties.TabIndex = 0;
            this.pgElementProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.ElementProperties_PropertyValueChanged);
            // 
            // txbCanvasFocus
            // 
            this.txbCanvasFocus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txbCanvasFocus.Location = new System.Drawing.Point(16, 592);
            this.txbCanvasFocus.Name = "txbCanvasFocus";
            this.txbCanvasFocus.Size = new System.Drawing.Size(100, 20);
            this.txbCanvasFocus.TabIndex = 1;
            this.txbCanvasFocus.Text = "TextBox1";
            // 
            // m_MainMenu
            // 
            this.m_MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuMisc,
            this.mnuPage,
            this.mnuPlugins,
            this.mnuHelp});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFileNew,
            this.m_MenuItem9,
            this.mnuFileOpen,
            this.mnuFileSave,
            this.mnuFileImport,
            this.mnuFileExport,
            this.m_MenuItem5,
            this.mnuFileExit});
            this.mnuFile.Text = "File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Index = 0;
            this.mnuFileNew.Text = "New";
            this.mnuFileNew.Click += new System.EventHandler(this.MnuFileNew_Click);
            // 
            // m_MenuItem9
            // 
            this.m_MenuItem9.Index = 1;
            this.m_MenuItem9.Text = "-";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Index = 2;
            this.mnuFileOpen.Text = "Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.MnuFileOpen_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Index = 3;
            this.mnuFileSave.Text = "Save";
            this.mnuFileSave.Click += new System.EventHandler(this.MnuFileSave_Click);
            // 
            // mnuFileImport
            // 
            this.mnuFileImport.Enabled = false;
            this.mnuFileImport.Index = 4;
            this.mnuFileImport.Text = "Import";
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Enabled = false;
            this.mnuFileExport.Index = 5;
            this.mnuFileExport.Text = "Export";
            // 
            // m_MenuItem5
            // 
            this.m_MenuItem5.Index = 6;
            this.m_MenuItem5.Text = "-";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Index = 7;
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.MnuFileExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuEditUndo,
            this.mnuEditRedo,
            this.m_MenuItem3,
            this.mnuCut,
            this.m_mnuCopy,
            this.mnuPaste,
            this.mnuDelete,
            this.m_MenuItem4,
            this.mnuSelectAll});
            this.mnuEdit.Text = "Edit";
            // 
            // mnuEditUndo
            // 
            this.mnuEditUndo.Enabled = false;
            this.mnuEditUndo.Index = 0;
            this.mnuEditUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.mnuEditUndo.Text = "Undo";
            this.mnuEditUndo.Click += new System.EventHandler(this.MnuEditUndo_Click);
            // 
            // mnuEditRedo
            // 
            this.mnuEditRedo.Enabled = false;
            this.mnuEditRedo.Index = 1;
            this.mnuEditRedo.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.mnuEditRedo.Text = "Redo";
            this.mnuEditRedo.Click += new System.EventHandler(this.MnuEditRedo_Click);
            // 
            // m_MenuItem3
            // 
            this.m_MenuItem3.Index = 2;
            this.m_MenuItem3.Text = "-";
            // 
            // mnuCut
            // 
            this.mnuCut.Index = 3;
            this.mnuCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.mnuCut.Text = "Cu&t";
            this.mnuCut.Click += new System.EventHandler(this.MnuCut_Click);
            // 
            // m_mnuCopy
            // 
            this.m_mnuCopy.Index = 4;
            this.m_mnuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.m_mnuCopy.Text = "&Copy";
            this.m_mnuCopy.Click += new System.EventHandler(this.MnuCopy_Click);
            // 
            // mnuPaste
            // 
            this.mnuPaste.Index = 5;
            this.mnuPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.mnuPaste.Text = "&Paste";
            this.mnuPaste.Click += new System.EventHandler(this.MnuPaste_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Index = 6;
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Click += new System.EventHandler(this.MnuDelete_Click);
            // 
            // m_MenuItem4
            // 
            this.m_MenuItem4.Index = 7;
            this.m_MenuItem4.Text = "-";
            // 
            // mnuSelectAll
            // 
            this.mnuSelectAll.Index = 8;
            this.mnuSelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.mnuSelectAll.Text = "Select &All";
            this.mnuSelectAll.Click += new System.EventHandler(this.MnuSelectAll_Click);
            // 
            // mnuMisc
            // 
            this.mnuMisc.Index = 2;
            this.mnuMisc.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuMiscLoadGumpling,
            this.mnuImportGumpling,
            this.mnuDataFile});
            this.mnuMisc.Text = "Misc";
            // 
            // mnuMiscLoadGumpling
            // 
            this.mnuMiscLoadGumpling.Index = 0;
            this.mnuMiscLoadGumpling.Text = "Load gumpling";
            this.mnuMiscLoadGumpling.Click += new System.EventHandler(this.MenuItem2_Click);
            // 
            // mnuImportGumpling
            // 
            this.mnuImportGumpling.Index = 1;
            this.mnuImportGumpling.Text = "Import Gumpling";
            this.mnuImportGumpling.Click += new System.EventHandler(this.MnuImportGumpling_Click);
            // 
            // mnuDataFile
            // 
            this.mnuDataFile.Index = 2;
            this.mnuDataFile.Text = "Data File Path";
            this.mnuDataFile.Click += new System.EventHandler(this.MnuDataFile_Click);
            // 
            // mnuPage
            // 
            this.mnuPage.Index = 3;
            this.mnuPage.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuPageAdd,
            this.mnuPageInsert,
            this.mnuPageDelete,
            this.mnuPageClear,
            this.m_MenuItem10,
            this.mnuShow0});
            this.mnuPage.Text = "Page";
            // 
            // mnuPageAdd
            // 
            this.mnuPageAdd.Index = 0;
            this.mnuPageAdd.Text = "Add Page";
            this.mnuPageAdd.Click += new System.EventHandler(this.MnuAddPage_Click);
            // 
            // mnuPageInsert
            // 
            this.mnuPageInsert.Index = 1;
            this.mnuPageInsert.Text = "Insert Page";
            this.mnuPageInsert.Click += new System.EventHandler(this.MnuPageInsert_Click);
            // 
            // mnuPageDelete
            // 
            this.mnuPageDelete.Index = 2;
            this.mnuPageDelete.Text = "Delete Page";
            this.mnuPageDelete.Click += new System.EventHandler(this.MnuPageDelete_Click);
            // 
            // mnuPageClear
            // 
            this.mnuPageClear.Index = 3;
            this.mnuPageClear.Text = "Clear Page";
            this.mnuPageClear.Click += new System.EventHandler(this.MnuPageClear_Click);
            // 
            // m_MenuItem10
            // 
            this.m_MenuItem10.Index = 4;
            this.m_MenuItem10.Text = "-";
            // 
            // mnuShow0
            // 
            this.mnuShow0.Checked = true;
            this.mnuShow0.Index = 5;
            this.mnuShow0.Text = "Always Show Page 0";
            this.mnuShow0.Click += new System.EventHandler(this.MnuShow0_Click);
            // 
            // mnuPlugins
            // 
            this.mnuPlugins.Index = 4;
            this.mnuPlugins.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuPluginManager});
            this.mnuPlugins.Text = "Plug-Ins";
            // 
            // mnuPluginManager
            // 
            this.mnuPluginManager.Index = 0;
            this.mnuPluginManager.Text = "Manager";
            this.mnuPluginManager.Click += new System.EventHandler(this.MnuPluginManager_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 5;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Index = 0;
            this.mnuHelpAbout.Text = "About...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.MnuHelpAbout_Click);
            // 
            // mnuGumplingContext
            // 
            this.mnuGumplingContext.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuGumplingRename,
            this.mnuGumplingMove,
            this.mnuGumplingDelete,
            this.m_MenuItem1,
            this.mnuGumplingAddGumpling,
            this.mnuGumplingAddFolder});
            // 
            // mnuGumplingRename
            // 
            this.mnuGumplingRename.Index = 0;
            this.mnuGumplingRename.Text = "Rename";
            // 
            // mnuGumplingMove
            // 
            this.mnuGumplingMove.Index = 1;
            this.mnuGumplingMove.Text = "Move";
            // 
            // mnuGumplingDelete
            // 
            this.mnuGumplingDelete.Index = 2;
            this.mnuGumplingDelete.Text = "Delete";
            // 
            // m_MenuItem1
            // 
            this.m_MenuItem1.Index = 3;
            this.m_MenuItem1.Text = "-";
            // 
            // mnuGumplingAddGumpling
            // 
            this.mnuGumplingAddGumpling.Index = 4;
            this.mnuGumplingAddGumpling.Text = "Add Gumpling";
            // 
            // mnuGumplingAddFolder
            // 
            this.mnuGumplingAddFolder.Index = 5;
            this.mnuGumplingAddFolder.Text = "Add Folder";
            // 
            // DesignerForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(904, 665);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.pnlToolboxHolder);
            this.Controls.Add(this.StatusBar);
            this.KeyPreview = true;
            this.Menu = this.m_MainMenu;
            this.Name = "DesignerForm";
            this.Text = "Gump Studio (-Unsaved Gump-)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesignerForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DesignerForm_FormClosed);
            this.Load += new System.EventHandler(this.DesignerForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DesignerForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DesignerForm_KeyUp);
            this.pnlToolboxHolder.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.tabToolbox.ResumeLayout(false);
            this.tpgStandard.ResumeLayout(false);
            this.tpgCustom.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.pnlCanvasScroller.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.TabPager.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treGumplings;
        private System.Windows.Forms.TabPage tpgStandard;
        private System.Windows.Forms.TabPage tpgCustom;
        private System.Windows.Forms.TabControl tabToolbox;
        private System.Windows.Forms.TabControl TabPager;
        private System.Windows.Forms.TabPage TabPage1;
        private System.Windows.Forms.StatusBar StatusBar;
        private System.Windows.Forms.Splitter Splitter2;
        private System.Windows.Forms.Splitter Splitter1;
        private System.Windows.Forms.SaveFileDialog SaveDialog;
        private System.Windows.Forms.Panel pnlToolboxHolder;
        private System.Windows.Forms.Panel pnlToolbox;
        private System.Windows.Forms.Panel pnlCanvasScroller;
        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Panel Panel4;
        private System.Windows.Forms.Panel Panel3;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.OpenFileDialog OpenDialog;
        private System.Windows.Forms.MenuItem mnuShow0;
        private System.Windows.Forms.MenuItem mnuSelectAll;
        private System.Windows.Forms.MenuItem mnuPlugins;
        private System.Windows.Forms.MenuItem mnuPluginManager;
        private System.Windows.Forms.MenuItem mnuPaste;
        private System.Windows.Forms.MenuItem mnuPageInsert;
        private System.Windows.Forms.MenuItem mnuPageDelete;
        private System.Windows.Forms.MenuItem mnuPageClear;
        private System.Windows.Forms.MenuItem mnuPageAdd;
        private System.Windows.Forms.MenuItem mnuPage;
        private System.Windows.Forms.MenuItem mnuMiscLoadGumpling;
        private System.Windows.Forms.MenuItem mnuMisc;
        private System.Windows.Forms.MenuItem mnuImportGumpling;
        private System.Windows.Forms.MenuItem mnuHelpAbout;
        private System.Windows.Forms.MenuItem mnuHelp;
        private System.Windows.Forms.MenuItem mnuGumplingRename;
        private System.Windows.Forms.MenuItem mnuGumplingMove;
        private System.Windows.Forms.MenuItem mnuGumplingDelete;
        private System.Windows.Forms.ContextMenu mnuGumplingContext;
        private System.Windows.Forms.MenuItem mnuGumplingAddGumpling;
        private System.Windows.Forms.MenuItem mnuGumplingAddFolder;
        private System.Windows.Forms.MenuItem mnuFileSave;
        private System.Windows.Forms.MenuItem mnuFileOpen;
        private System.Windows.Forms.MenuItem mnuFileNew;
        private System.Windows.Forms.MenuItem mnuFileImport;
        private System.Windows.Forms.MenuItem mnuFileExport;
        private System.Windows.Forms.MenuItem mnuFileExit;
        private System.Windows.Forms.MenuItem mnuFile;
        private System.Windows.Forms.MenuItem mnuEditUndo;
        private System.Windows.Forms.MenuItem mnuEditRedo;
        private System.Windows.Forms.MenuItem mnuEdit;
        private System.Windows.Forms.MenuItem mnuDelete;
        private System.Windows.Forms.MenuItem mnuDataFile;
        private System.Windows.Forms.MenuItem mnuCut;
        private System.Windows.Forms.MenuItem m_mnuCopy;
        private System.Windows.Forms.ContextMenu m_mnuContextMenu;
        private System.Windows.Forms.MenuItem m_MenuItem9;
        private System.Windows.Forms.MenuItem m_MenuItem5;
        private System.Windows.Forms.MenuItem m_MenuItem4;
        private System.Windows.Forms.MenuItem m_MenuItem3;
        private System.Windows.Forms.MenuItem m_MenuItem10;
        private System.Windows.Forms.MenuItem m_MenuItem1;
        private System.Windows.Forms.MainMenu m_MainMenu;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.PropertyGrid pgElementProperties;
        private System.Windows.Forms.ComboBox cboElements;
        private System.Windows.Forms.TextBox txbCanvasFocus;
    }
}
