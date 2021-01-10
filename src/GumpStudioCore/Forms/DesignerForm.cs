using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using GumpStudio.Classes;
using GumpStudio.Elements;
using GumpStudio.Enums;
using GumpStudio.Plugins;
using Ultima;
using HtmlElement = GumpStudio.Elements.HtmlElement;

namespace GumpStudio.Forms
{
    public partial class DesignerForm
    {
        private string _aboutElementAppend;
        private decimal _arrowKeyDelta;
        private readonly List<BasePlugin> _availablePlugins;
        private Bitmap _canvas;
        private ClipBoardMode _copyMode;
        private int _currentUndoPoint;
        private bool _elementChanged;
        private string _fileName;
        private TreeFolder _gumplingsFolder;
        private TreeFolder _gumplingTree;
        private Point _lastPos;
        private readonly List<BasePlugin> _loadedPlugins;
        private Point _mAnchor;
        private Size _mAnchorOffset;
        private int _maxUndoPoints;
        private int _moveCount;
        private MoveModeType _moveMode;

        private readonly List<Type> _registeredTypes;

        //private Point m_ScrollPos;
        private LinearGradientBrush _selBg;
        private Rectangle _selectionRect;

        private Pen _selFg;

        //private bool m_ShouldClearActiveElement;
        //private bool m_ShowGrid;
        private bool _showPage0;
        private bool _showSelectionRect;
        private readonly bool _suppressUndoPoints;
        private TreeFolder _uncategorizedFolder;
        private List<UndoPoint> _undoPoints;

        public BaseElement ActiveElement;
        public readonly string AppPath;
        public GroupElement ElementStack;
        public GumpProperties GumpProperties;
        public bool PluginClearsCanvas;
        public PluginInfo[] PluginTypesToLoad;
        public List<GroupElement> Stacks;

        public event HookKeyDownEventHandler HookKeyDown;
        public event HookPostRenderEventHandler HookPostRender;
        public event HookPreRenderEventHandler HookPreRender;

        // ReSharper disable once ConvertToAutoProperty
        public virtual TextBox CanvasFocus
        {
            get => txbCanvasFocus;
            set => txbCanvasFocus = value;
        }

        // ReSharper disable once ConvertToAutoProperty
        public virtual MenuItem MnuPlugins
        {
            get => mnuPlugins;
            set => mnuPlugins = value;
        }

        // ReSharper disable once ConvertToAutoProperty
        public virtual MenuItem MnuFileExport
        {
            get => mnuFileExport;
            set => mnuFileExport = value;
        }

        // ReSharper disable once ConvertToAutoProperty
        public virtual MenuItem MnuFileImport
        {
            get => mnuFileImport;
            set => mnuFileImport = value;
        }

        public virtual PictureBox PicCanvas
        {
            get => picCanvas;
            set
            {
                MouseEventHandler mouseEventHandler1 = PicCanvas_MouseUp;
                MouseEventHandler mouseEventHandler2 = PicCanvas_MouseMove;
                PaintEventHandler paintEventHandler = PicCanvas_Paint;
                MouseEventHandler mouseEventHandler3 = PicCanvas_MouseDown;

                if (picCanvas != null)
                {
                    picCanvas.MouseUp -= mouseEventHandler1;
                    picCanvas.MouseMove -= mouseEventHandler2;
                    picCanvas.Paint -= paintEventHandler;
                    picCanvas.MouseDown -= mouseEventHandler3;
                }

                picCanvas = value;

                if (picCanvas == null)
                {
                    return;
                }

                picCanvas.MouseUp += mouseEventHandler1;
                picCanvas.MouseMove += mouseEventHandler2;
                picCanvas.Paint += paintEventHandler;
                picCanvas.MouseDown += mouseEventHandler3;
            }
        }

        public virtual PropertyGrid ElementProperties
        {
            get => pgElementProperties;
            set
            {
                PropertyValueChangedEventHandler changedEventHandler = ElementProperties_PropertyValueChanged;

                if (pgElementProperties != null)
                {
                    pgElementProperties.PropertyValueChanged -= changedEventHandler;
                }

                pgElementProperties = value;

                if (pgElementProperties == null)
                {
                    return;
                }

                pgElementProperties.PropertyValueChanged += changedEventHandler;
            }
        }

        public delegate void HookKeyDownEventHandler(object sender, ref KeyEventArgs e);

        public delegate void HookPostRenderEventHandler(Bitmap target);

        public delegate void HookPreRenderEventHandler(Bitmap target);

        public DesignerForm()
        {
            AppPath = Application.StartupPath;
            ElementStack = new GroupElement(null, "CanvasStack", true);

            Stacks = new List<GroupElement>();
            _registeredTypes = new List<Type>();
            _availablePlugins = new List<BasePlugin>();
            _loadedPlugins = new List<BasePlugin>();
            _undoPoints = new List<UndoPoint>();

            //m_ShouldClearActiveElement = false;
            PluginClearsCanvas = false;
            _arrowKeyDelta = new decimal(1);
            _showSelectionRect = false;
            _moveMode = MoveModeType.None;
            //m_ShowGrid = false;
            _showPage0 = true;
            _elementChanged = false;
            _currentUndoPoint = -1;
            _maxUndoPoints = 25;
            _suppressUndoPoints = false;

            InitializeComponent();
        }

        public void AddElement(BaseElement element)
        {
            ElementStack.AddElement(element);
            element.Selected = true;
            SetActiveElement(element, true);
            picCanvas.Invalidate();
            CreateUndoPoint($"{element.Name} added");
        }

        private void AddPage()
        {
            Stacks.Add(new GroupElement(null, "CanvasStack", true));
            TabPager.TabPages.Add(new TabPage(Convert.ToString(Stacks.Count - 1)));
            TabPager.SelectedIndex = Stacks.Count - 1;
            ChangeActiveStack(Stacks.Count - 1);
        }

        private void BuildGumplingTree()
        {
            treGumplings.SuspendLayout();
            treGumplings.BeginUpdate();
            try
            {
                treGumplings.Nodes.Clear();
                BuildGumplingTree(_gumplingTree, null);
            }
            finally
            {
                treGumplings.EndUpdate();
                treGumplings.ResumeLayout();
            }
        }

        private void BuildGumplingTree(TreeFolder item, TreeNode node)
        {
            foreach (var treeItem in item.GetChildren())
            {
                var treeNode = new TreeNode
                {
                    Text = treeItem.Text,
                    Tag = treeItem
                };

                if (node == null)
                {
                    treGumplings.Nodes.Add(treeNode);
                }
                else
                {
                    node.Nodes.Add(treeNode);
                }

                if (treeItem is TreeFolder treeFolder)
                {
                    BuildGumplingTree(treeFolder, treeNode);
                }
            }
        }

        private void BuildToolbox()
        {
            pnlToolbox.Controls.Clear();
            pnlToolbox.SuspendLayout();

            try
            {
                int y = 0;
                foreach (var type in _registeredTypes)
                {
                    var instance = (BaseElement)Activator.CreateInstance(type);

                    var button = new Button
                    {
                        Text = instance.Type,
                        Location = new Point(0, y),
                        FlatStyle = FlatStyle.System,
                        Width = pnlToolbox.Width,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                        Tag = type
                    };

                    button.Click += CreateElementFromToolbox;
                    pnlToolbox.Controls.Add(button);

                    y += button.Height - 1;

                    if (instance.DisplayInAbout())
                    {
                        _aboutElementAppend = $"{_aboutElementAppend}\r\n\r\n{instance.Type}: {instance.GetAboutText()}";
                    }

                    foreach (var plugin in _loadedPlugins)
                    {
                        plugin.InitializeElementExtenders(instance);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                pnlToolbox.ResumeLayout();
            }

            BaseElement.ResetId();
            _gumplingTree = new TreeFolder("Root");
            _gumplingsFolder = new TreeFolder("My Gumplings");
            _uncategorizedFolder = new TreeFolder("Uncategorized");
            _gumplingTree.AddItem(_gumplingsFolder);
            _gumplingTree.AddItem(_uncategorizedFolder);
            BuildGumplingTree();
        }

        private void CboElements_Click(object sender, EventArgs e)
        {
            foreach (var baseElement in ElementStack.GetElements())
            {
                baseElement.Selected = false;
            }

            ActiveElement = null;
        }

        private void CboElements_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetActiveElement((BaseElement)cboElements.SelectedItem);
            picCanvas.Invalidate();
        }

        private void ChangeActiveElementEventHandler(BaseElement e, bool deselectOthers)
        {
            SetActiveElement(e, deselectOthers);
            picCanvas.Invalidate();
        }

        private void ChangeActiveStack(int stackId)
        {
            if (stackId > Stacks.Count - 1)
            {
                return;
            }

            SetActiveElement(null, true);

            if (ElementStack != null)
            {
                ElementStack.UpdateParent -= ChangeActiveElementEventHandler;
                ElementStack.Repaint -= RefreshView;
            }

            ElementStack = Stacks[stackId];
            ElementStack.UpdateParent += ChangeActiveElementEventHandler;
            ElementStack.Repaint += RefreshView;
            picCanvas.Invalidate();
        }

        private void ClearContextMenu(Menu menu)
        {
            int num = menu.MenuItems.Count - 1;

            for (int index = 0; index <= num; ++index)
            {
                menu.MenuItems.RemoveAt(0);
            }
        }

        public void ClearGump()
        {
            TabPager.TabPages.Clear();
            TabPager.TabPages.Add(new TabPage("0"));
            Stacks.Clear();
            BaseElement.ResetId();
            ElementStack = new GroupElement(null, "Element Stack", true);
            Stacks.Add(ElementStack);
            GumpProperties = new GumpProperties();
            ElementStack.UpdateParent += ChangeActiveElementEventHandler;
            ElementStack.Repaint += RefreshView;
            SetActiveElement(null);
            picCanvas.Invalidate();
            _fileName = "";
            Text = "Gump Studio (-Unsaved Gump-)";
            ChangeActiveStack(0);
            _undoPoints = new List<UndoPoint>();
            CreateUndoPoint("Blank");
            mnuEditUndo.Enabled = false;
            mnuEditRedo.Enabled = false;
        }

        private void Copy()
        {
            var elements = ElementStack.GetSelectedElements().ConvertAll(element => element.Clone());

            Clipboard.SetDataObject(elements);

            _copyMode = ClipBoardMode.Copy;
        }

        private void CreateElementFromToolbox(object sender, EventArgs e)
        {
            AddElement((BaseElement)Activator.CreateInstance((Type)((Control)sender).Tag));
            picCanvas.Invalidate();
            picCanvas.Focus();
        }

        public void CreateUndoPoint(string action = "Unknown Action")
        {
            if (_suppressUndoPoints)
            {
                return;
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (_currentUndoPoint < _undoPoints.Count - 1)
            {
                var undoPoint = _undoPoints[_currentUndoPoint + 1];
                _undoPoints.RemoveAt(_currentUndoPoint + 1);
                Debug.WriteLine($"Removing undo point {undoPoint.Text}");
            }

            var undoPoint1 = new UndoPoint(this)
            {
                Text = action
            };

            if (_undoPoints.Count > _maxUndoPoints)
            {
                _undoPoints.RemoveAt(0);
            }

            _undoPoints.Add(undoPoint1);
            _currentUndoPoint = _undoPoints.Count - 1;
            mnuEditUndo.Enabled = true;
            mnuEditRedo.Enabled = false;
            stopwatch.Stop();
            Debug.WriteLine($"Created undo point {_currentUndoPoint + 1} in {stopwatch.Elapsed}");
        }

        private void Cut()
        {
            var elements = ElementStack.GetSelectedElements().ToList();

            Clipboard.SetDataObject(elements);

            DeleteSelectedElements();

            _copyMode = ClipBoardMode.Cut;
        }

        private void DeleteSelectedElements()
        {
            var elements = new List<BaseElement>();
            elements.AddRange(ElementStack.GetElements());
            bool flag = false;

            foreach (var baseElement in elements)
            {
                flag = true;

                if (baseElement.Selected)
                {
                    ElementStack.RemoveElement(baseElement);
                }
            }

            SetActiveElement(GetLastSelectedControl());
            picCanvas.Invalidate();

            if (!flag)
            {
                return;
            }

            CreateUndoPoint("Delete Elements");
        }

        private void DesignerForm_KeyDown(object sender, KeyEventArgs e)
        {
            var hookKeyDown = HookKeyDown;
            hookKeyDown?.Invoke(ActiveControl, ref e);

            if (e.Handled || ActiveControl != CanvasFocus)
            {
                return;
            }

            bool flag = false;

            if ((e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back ? 1 : 0) != 0)
            {
                DeleteSelectedElements();
                e.Handled = true;
                flag = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                foreach (var baseElement in ElementStack.GetSelectedElements())
                {
                    var location = baseElement.Location;
                    location.Offset(0, -Convert.ToInt32(_arrowKeyDelta));
                    baseElement.Location = location;
                }

                _arrowKeyDelta = decimal.Multiply(_arrowKeyDelta, new decimal(106, 0, 0, false, 2));
                flag = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                foreach (var baseElement in ElementStack.GetSelectedElements())
                {
                    var location = baseElement.Location;
                    location.Offset(0, Convert.ToInt32(_arrowKeyDelta));
                    baseElement.Location = location;
                }

                _arrowKeyDelta = decimal.Multiply(_arrowKeyDelta, new decimal(106, 0, 0, false, 2));
                flag = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                foreach (var baseElement in ElementStack.GetSelectedElements())
                {
                    var location3 = baseElement.Location;
                    location3.Offset(-Convert.ToInt32(_arrowKeyDelta), 0);
                    baseElement.Location = location3;
                }

                _arrowKeyDelta = decimal.Multiply(_arrowKeyDelta, new decimal(106, 0, 0, false, 2));
                flag = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                foreach (var baseElement in ElementStack.GetSelectedElements())
                {
                    var location4 = baseElement.Location;
                    location4.Offset(Convert.ToInt32(_arrowKeyDelta), 0);
                    baseElement.Location = location4;
                }

                _arrowKeyDelta = decimal.Multiply(_arrowKeyDelta, new decimal(106, 0, 0, false, 2));
                flag = true;
            }
            else if (e.KeyCode == Keys.Next)
            {
                int index = (ActiveElement?.Z ?? ElementStack.GetElements().Count - 1) - 1;

                if (index < 0)
                {
                    index = ElementStack.GetElements().Count - 1;
                }

                if (index >= 0 && index <= ElementStack.GetElements().Count - 1)
                {
                    SetActiveElement(ElementStack.GetElements()[index], true);
                }
            }
            else if (e.KeyCode == Keys.Prior)
            {
                int index = (ActiveElement?.Z ?? ElementStack.GetElements().Count - 1) + 1;

                if (index > ElementStack.GetElements().Count - 1)
                {
                    index = 0;
                }

                SetActiveElement(ElementStack.GetElements()[index], true);
            }

            if (decimal.Compare(_arrowKeyDelta, new decimal(10)) > 0)
            {
                _arrowKeyDelta = new decimal(10);
            }

            if (flag)
            {
                picCanvas.Invalidate();
            }
        }

        private void DesignerForm_KeyUp(object sender, KeyEventArgs e)
        {
            // if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
            // {
            //     // TODO: what should be here?
            //     ;
            // }

            if ((e.KeyCode == Keys.Right ? 1 : 0) == 0)
            {
                return;
            }

            CreateUndoPoint("Move element");
            _arrowKeyDelta = new decimal(1);
        }

        private void DesignerForm_Load(object sender, EventArgs e)
        {
            if (AppSettings.Default.ClientPath?.Length == 0)
            {
                AppSettings.Default.Upgrade();
            }

            if (!File.Exists(Path.Combine(AppSettings.Default.ClientPath, "art.mul")))
            {
                var folderBrowserDialog = new FolderBrowserDialog
                {
                    SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                    Description = "Select the folder that contains the UO data (.mul) files you want to use."
                };

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(Path.Combine(folderBrowserDialog.SelectedPath, "art.mul")))
                    {
                        AppSettings.Default.ClientPath = folderBrowserDialog.SelectedPath;
                        AppSettings.Default.Save();
                    }
                    else
                    {
                        MessageBox.Show(
                            "This path does not contain a file named \"art.mul\", it is most likely not the correct path. Gump Studio can not run without valid client data files.",
                            "Data Files");
                        Close();

                        return;
                    }
                }
                else
                {
                    Close();

                    return;
                }
            }

            Client.Directories.Add(AppSettings.Default.ClientPath);
            Size = AppSettings.Default.DesignerFormSize;
            _maxUndoPoints = AppSettings.Default.UndoLevels;
            picCanvas.Width = 1024;
            picCanvas.Height = 768;
            CenterToScreen();
            SplashForm.DisplaySplash();
            EnumeratePlugins();
            _canvas = new Bitmap(picCanvas.Width, picCanvas.Height, PixelFormat.Format32bppRgb);
            Activate();
            GumpProperties = new GumpProperties();
            ElementStack.UpdateParent += ChangeActiveElementEventHandler;
            ElementStack.Repaint += RefreshView;
            Stacks.Clear();
            Stacks.Add(ElementStack);
            ChangeActiveStack(0);
            _registeredTypes.Clear();
            _registeredTypes.Add(typeof(LabelElement));
            _registeredTypes.Add(typeof(ImageElement));
            _registeredTypes.Add(typeof(TiledElement));
            _registeredTypes.Add(typeof(BackgroundElement));
            _registeredTypes.Add(typeof(AlphaElement));
            _registeredTypes.Add(typeof(CheckboxElement));
            _registeredTypes.Add(typeof(RadioElement));
            _registeredTypes.Add(typeof(ItemElement));
            _registeredTypes.Add(typeof(TextEntryElement));
            _registeredTypes.Add(typeof(ButtonElement));
            _registeredTypes.Add(typeof(HtmlElement));
            BuildToolbox();
            _selFg = new Pen(Color.Blue, 2f);

            _selBg = new LinearGradientBrush(new Rectangle(0, 0, 50, 50), Color.FromArgb(90, Color.Blue),
                Color.FromArgb(110, Color.Blue), LinearGradientMode.ForwardDiagonal)
            {
                WrapMode = WrapMode.TileFlipXY
            };
            CreateUndoPoint("Blank");
            mnuEditUndo.Enabled = false;
        }

        // TODO: unused?
        protected void DrawBoundingBox(Graphics target, BaseElement element)
        {
            var bounds = element.Bounds;
            target.DrawRectangle(Pens.Red, bounds);
            bounds.Offset(1, 1);
            target.DrawRectangle(Pens.Black, bounds);
        }

        private void ElementProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.PropertyDescriptor?.Name == "Name")
            {
                cboElements.Items.Clear();
                cboElements.Items.AddRange(ElementStack.GetElements().ToArray());
                cboElements.SelectedItem = RuntimeHelpers.GetObjectValue(pgElementProperties.SelectedObject);
            }

            picCanvas.Invalidate();
            CreateUndoPoint("Property Changed");
        }

        private void EnumeratePlugins()
        {
            if (!Directory.Exists($@"{Application.StartupPath}\Plugins"))
            {
                Directory.CreateDirectory($@"{Application.StartupPath}\Plugins");
            }

            PluginTypesToLoad = GetPluginsToLoad();
            Debug.WriteLine("Enumerating Plugins");
            Debug.Indent();

            foreach (string file in Directory.GetFiles($@"{Application.StartupPath}\Plugins", "*.dll"))
            {
                Debug.WriteLine(Path.GetFileName(file));
                var assembly = Assembly.LoadFile(file);
                Debug.Indent();

                foreach (var type in assembly.GetTypes())
                {
                    try
                    {
                        if (type.IsSubclassOf(typeof(BasePlugin)) && !type.IsAbstract)
                        {
                            var instance = (BasePlugin)Activator.CreateInstance(type);
                            var pluginInfo = instance.GetPluginInfo();

                            _aboutElementAppend =
                                $"{_aboutElementAppend}\r\n{pluginInfo.PluginName}: {pluginInfo.Description}\r\nAuthor: {pluginInfo.AuthorName}  ({pluginInfo.AuthorEmail})\r\nVersion: {pluginInfo.Version}\r\n";
                            _availablePlugins.Add(instance);
                        }

                        if (type.IsSubclassOf(typeof(BaseElement)))
                        {
                            _registeredTypes.Add(type);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading plugin: {type.Name}({file})\r\n\r\n{ex.Message}");
                    }
                }

                Debug.Unindent();
            }

            Debug.Unindent();
            Debug.WriteLine("Loading plugins");

            if (PluginTypesToLoad == null)
            {
                return;
            }

            foreach (var pluginInfo in PluginTypesToLoad)
            {
                foreach (var basePlugin in _availablePlugins)
                {
                    var availablePluginInfo = basePlugin.GetPluginInfo();
                    if (!pluginInfo.Equals(availablePluginInfo))
                    {
                        continue;
                    }

                    basePlugin.Load(this);

                    _loadedPlugins.Add(basePlugin);
                }
            }
        }

        private void GetContextMenu(ref BaseElement element, ContextMenu menu)
        {
            var groupMenu = new MenuItem("Grouping");
            var positionMenu = new MenuItem("Positioning");
            var orderMenu = new MenuItem("Order");
            var miscMenu = new MenuItem("Misc");
            var menuItem = new MenuItem("Edit");
            menuItem.MenuItems.Add(new MenuItem("Cut", MnuCut_Click));
            menuItem.MenuItems.Add(new MenuItem("Copy", MnuCopy_Click));
            menuItem.MenuItems.Add(new MenuItem("Paste", MnuPaste_Click));
            menuItem.MenuItems.Add(new MenuItem("Delete", MnuDelete_Click));
            menu.MenuItems.Add(menuItem);
            menu.MenuItems.Add(new MenuItem("-"));
            menu.MenuItems.Add(groupMenu);
            menu.MenuItems.Add(positionMenu);
            menu.MenuItems.Add(orderMenu);
            menu.MenuItems.Add(new MenuItem("-"));
            menu.MenuItems.Add(miscMenu);

            if (ElementStack.GetSelectedElements().Count >= 2)
            {
                groupMenu.MenuItems.Add(new MenuItem("Create Group", MnuGroupCreate_Click));
            }

            element?.AddContextMenus(ref groupMenu, ref positionMenu, ref orderMenu, ref miscMenu);

            if (groupMenu.MenuItems.Count == 0)
            {
                groupMenu.Enabled = false;
            }

            if (positionMenu.MenuItems.Count == 0)
            {
                positionMenu.Enabled = false;
            }

            if (orderMenu.MenuItems.Count == 0)
            {
                orderMenu.Enabled = false;
            }

            if (miscMenu.MenuItems.Count != 0)
            {
                return;
            }

            miscMenu.Enabled = false;
        }

        private BaseElement GetLastSelectedControl()
        {
            return ElementStack.GetElements().LastOrDefault();
        }

        private PluginInfo[] GetPluginsToLoad()
        {
            if (!File.Exists($@"{Application.StartupPath}\Plugins\LoadInfo"))
            {
                return null;
            }

            var fileStream = new FileStream($@"{Application.StartupPath}\Plugins\LoadInfo", FileMode.Open);
            var pluginInfoArray = (PluginInfo[])new BinaryFormatter().Deserialize(fileStream);
            fileStream.Close();

            return pluginInfoArray;
        }

        private static Rectangle GetPositiveRect(Rectangle rect)
        {
            if (rect.Height < 0)
            {
                rect.Height = Math.Abs(rect.Height);
                rect.Y -= rect.Height;
            }

            if (rect.Width < 0)
            {
                rect.Width = Math.Abs(rect.Width);
                rect.X -= rect.Width;
            }

            return rect;
        }

        private void LoadFrom(string path)
        {
            StatusBar.Text = "Loading gump...";
            FileStream fileStream = null;
            Stacks.Clear();
            TabPager.TabPages.Clear();

            try
            {
                fileStream = new FileStream(path, FileMode.Open);
                var binaryFormatter = new BinaryFormatter();
                Stacks = (List<GroupElement>)binaryFormatter.Deserialize(fileStream);

                try
                {
                    GumpProperties = (GumpProperties)binaryFormatter.Deserialize(fileStream);
                }
                catch (Exception ex)
                {
                    GumpProperties = new GumpProperties();

                    if (ex.InnerException != null)
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }
                }

                SetActiveElement(null, true);
                RefreshElementList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fileStream?.Close();
            }

            int num = 0;
            foreach (var _ in Stacks)
            {
                //object objectValue = RuntimeHelpers.GetObjectValue(groupElement);
                TabPager.TabPages.Add(new TabPage(num.ToString()));
                num++;
            }

            ChangeActiveStack(0);
            ElementStack.UpdateParent += ChangeActiveElementEventHandler;
            ElementStack.Repaint += RefreshView;
            StatusBar.Text = "";
        }

        private void MenuItem2_Click(object sender, EventArgs e)
        {
            OpenDialog.Filter = "Gumpling (*.gumpling)|*.gumpling|Gump (*.gump)|*.gump";

            if (OpenDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var fileStream = new FileStream(OpenDialog.FileName, FileMode.Open);
            var groupElement = (GroupElement)new BinaryFormatter().Deserialize(fileStream);
            groupElement.IsBaseWindow = false;
            groupElement.RecalculateBounds();
            groupElement.Location = new Point(0, 0);
            fileStream.Close();
            AddElement(groupElement);
        }

        private void MnuAddPage_Click(object sender, EventArgs e)
        {
            AddPage();
            CreateUndoPoint("Add page");
        }

        private void MnuCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void MnuCut_Click(object sender, EventArgs e)
        {
            Cut();
            CreateUndoPoint();
        }

        private void MnuDataFile_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Select the folder that contains the UO data (.mul) files you want to use."
            };

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (File.Exists(Path.Combine(folderBrowserDialog.SelectedPath, "art.mul")))
            {
                AppSettings.Default.ClientPath = folderBrowserDialog.SelectedPath;
                AppSettings.Default.Save();

                MessageBox.Show(
                   "New path set, please restart Gump Studio to activate your changes.",
                   "Data Files",
                   MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(
                    "This path does not contain a file named \"art.mul\", it is most likely not the correct path.",
                    "Data Files",
                    MessageBoxButtons.OK);
            }
        }

        private void MnuDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedElements();
        }

        private void MnuEditRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void MnuEditUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void MnuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MnuFileNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to start a new gump?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            {
                return;
            }

            ClearGump();
        }

        private void MnuFileOpen_Click(object sender, EventArgs e)
        {
            OpenDialog.CheckFileExists = true;
            OpenDialog.Filter = "Gump|*.gump";

            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                LoadFrom(OpenDialog.FileName);
                _fileName = Path.GetFileName(OpenDialog.FileName);
                Text = $"Gump Studio ({_fileName})";
            }

            picCanvas.Invalidate();
        }

        private void MnuFileSave_Click(object sender, EventArgs e)
        {
            SaveDialog.AddExtension = true;
            SaveDialog.DefaultExt = "gump";
            SaveDialog.Filter = "Gump|*.gump";

            if (SaveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            SaveTo(SaveDialog.FileName);
            _fileName = Path.GetFileName(SaveDialog.FileName);
            Text = $"Gump Studio ({_fileName})";
        }

        private void MnuGroupCreate_Click(object sender, EventArgs e)
        {
            var elements = new List<BaseElement>();

            foreach (var baseElement in ElementStack.GetElements().Where(baseElement => baseElement.Selected))
            {
                elements.Add(baseElement);
            }

            if (elements.Count >= 2)
            {
                var groupElement = new GroupElement(null, "New Group");

                foreach (var baseElement in elements)
                {
                    groupElement.AddElement(baseElement);
                    ElementStack.RemoveElement(baseElement);
                    ElementStack.RemoveEvents(baseElement);
                }

                AddElement(groupElement);
                picCanvas.Invalidate();
            }

            CreateUndoPoint();
        }

        private void MnuHelpAbout_Click(object sender, EventArgs e)
        {
            var frmAboutBox = new AboutBoxForm();
            frmAboutBox.SetText(_aboutElementAppend);
            frmAboutBox.ShowDialog();
        }

        private void MnuImportGumpling_Click(object sender, EventArgs e)
        {
            OpenDialog.Filter = "Gumpling (*.gumpling)|*.gumpling|Gump (*.gump)|*.gump";

            if (OpenDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var fileStream = new FileStream(OpenDialog.FileName, FileMode.Open);
            var gumpling = (GroupElement)new BinaryFormatter().Deserialize(fileStream);
            gumpling.IsBaseWindow = false;
            gumpling.RecalculateBounds();
            var point = new Point(0, 0);
            gumpling.Location = point;
            fileStream.Close();
            _uncategorizedFolder.AddItem(new TreeGumpling(Path.GetFileName(OpenDialog.FileName), gumpling));
            BuildGumplingTree();
        }

        private void MnuPageClear_Click(object sender, EventArgs e)
        {
            ElementStack = new GroupElement(null, "Element Stack", true);
            CreateUndoPoint("Clear Page");
        }

        private void MnuPageDelete_Click(object sender, EventArgs e)
        {
            if (TabPager.SelectedIndex == 0)
            {
                MessageBox.Show("Page 0  can not be deleted.");
            }
            else
            {
                int selectedIndex = TabPager.SelectedIndex;
                int num2 = TabPager.TabCount - 1;

                for (int index = selectedIndex + 1; index <= num2; ++index)
                {
                    TabPager.TabPages[index].Text = Convert.ToString(index - 1);
                }

                Stacks.RemoveAt(selectedIndex);
                TabPager.TabPages.RemoveAt(selectedIndex);
                ChangeActiveStack(selectedIndex - 1);
                CreateUndoPoint("Delete page");
            }
        }

        private void MnuPageInsert_Click(object sender, EventArgs e)
        {
            if (TabPager.SelectedIndex == 0)
            {
                MessageBox.Show("Page 0 may not be moved.");
            }
            else
            {
                int tabCount = TabPager.TabCount;
                int selectedIndex = TabPager.SelectedIndex;
                int num2 = TabPager.TabCount - 1;

                for (int index = selectedIndex; index <= num2; ++index)
                {
                    TabPager.TabPages.RemoveAt(selectedIndex);
                }

                TabPager.TabPages.Add(new TabPage(selectedIndex.ToString()));
                int num3 = tabCount;

                for (int index = selectedIndex + 1; index <= num3; ++index)
                {
                    TabPager.TabPages.Add(new TabPage(index.ToString()));
                }

                var groupElement = new GroupElement(null, "Element Stack", true);
                Stacks.Insert(selectedIndex, groupElement);
                ChangeActiveStack(selectedIndex);
                TabPager.SelectedIndex = selectedIndex;
                CreateUndoPoint("Insert page");
            }
        }

        private void MnuPaste_Click(object sender, EventArgs e)
        {
            Paste();
            CreateUndoPoint();
        }

        private void MnuPluginManager_Click(object sender, EventArgs e)
        {
            new PluginManager
            {
                AvailablePlugins = _availablePlugins,
                LoadedPlugins = _loadedPlugins,
                OrderList = PluginTypesToLoad,
                MainForm = this
            }.ShowDialog();
        }

        private void MnuSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void MnuShow0_Click(object sender, EventArgs e)
        {
            _showPage0 = !_showPage0;
            mnuShow0.Checked = _showPage0;
            picCanvas.Refresh();
        }

        private void Paste()
        {
            var dataObject = Clipboard.GetDataObject();
            var elements = (List<BaseElement>)dataObject?.GetData(typeof(List<BaseElement>));

            if (elements != null)
            {
                SetActiveElement(null, true);

                foreach (var baseElement in elements)
                {
                    if (_copyMode == ClipBoardMode.Copy)
                    {
                        baseElement.Name = "Copy of " + baseElement.Name;
                    }

                    baseElement.Selected = true;
                    ElementStack.AddElement(baseElement);
                }
            }

            picCanvas.Invalidate();
        }

        private void PicCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            CanvasFocus.Focus();
            var point = new Point(e.X, e.Y);
            _mAnchor = point;
            var element = ElementStack.GetElementFromPoint(point);

            if ((ActiveElement == null || ActiveElement.HitTest(point) == MoveModeType.None ? 0 : 1) != 0)
            {
                element = ActiveElement;
            }

            if (element != null)
            {
                _moveMode = element.HitTest(point);

                if ((ActiveElement == null || ActiveElement.HitTest(point) != MoveModeType.None ? 0 : 1) != 0)
                {
                    if (element.Selected)
                    {
                        if ((ModifierKeys & Keys.Control) > Keys.None)
                        {
                            element.Selected = false;
                        }
                        else
                        {
                            SetActiveElement(element);
                        }
                    }
                    else
                    {
                        SetActiveElement(element, (ModifierKeys & Keys.Control) <= Keys.None);
                    }
                }
                else if (ActiveElement == null)
                {
                    SetActiveElement(element);
                }
                else if (ActiveElement != null && (ModifierKeys & Keys.Control) > Keys.None)
                {
                    ActiveElement.Selected = false;

                    var selectedElements = ElementStack.GetSelectedElements();

                    if (selectedElements.Count > 0)
                    {
                        SetActiveElement(selectedElements[0]);
                    }
                    else
                    {
                        SetActiveElement(null, true);
                        _moveMode = MoveModeType.None;
                    }
                }
            }
            else
            {
                _moveMode = MoveModeType.None;

                if ((e.Button & MouseButtons.Left) > MouseButtons.None)
                {
                    SetActiveElement(null, (ModifierKeys & Keys.Control) <= Keys.None);
                }
            }

            picCanvas.Invalidate();
            _lastPos = point;

            if (ActiveElement != null)
            {
                _mAnchorOffset.Width = ActiveElement.X - point.X;
                _mAnchorOffset.Height = ActiveElement.Y - point.Y;
            }

            _elementChanged = false;
            _moveCount = 0;
        }

        private void PicCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            var mouseLocation = new Point(e.X, e.Y);

            int value = mouseLocation.X - _lastPos.X;
            int value2 = mouseLocation.Y - _lastPos.Y;

            var baseElement = ElementStack.GetElementFromPoint(mouseLocation);

            if ((ActiveElement == null || ActiveElement.HitTest(mouseLocation) == MoveModeType.None ? 0 : 1) != 0)
            {
                baseElement = ActiveElement;
            }

            if (_moveMode == MoveModeType.Move)
            {
                mouseLocation.Offset(_mAnchorOffset.Width, _mAnchorOffset.Height);
            }

            var mouseMoveHookEventArgs = new MouseMoveHookEventArgs
            {
                Keys = ModifierKeys,
                MouseButtons = e.Button,
                MouseLocation = mouseLocation,
                MoveMode = _moveMode
            };

            foreach (var basePlugin in _loadedPlugins)
            {
                basePlugin.MouseMoveHook(ref mouseMoveHookEventArgs);
                mouseLocation = mouseMoveHookEventArgs.MouseLocation;
            }

            if ((_moveMode != MoveModeType.None || Math.Abs(value) <= 0 || Math.Abs(value2) <= 0 ? 0 : 1) != 0)
            {
                _moveMode = MoveModeType.SelectionBox;
            }

            if (e.Button != MouseButtons.Left)
            {
                if (baseElement != null)
                {
                    switch (baseElement.HitTest(mouseLocation))
                    {
                        case MoveModeType.ResizeTopLeft:
                        case MoveModeType.ResizeBottomRight:
                            Cursor = Cursors.SizeNWSE;

                            break;
                        case MoveModeType.ResizeTopRight:
                        case MoveModeType.ResizeBottomLeft:
                            Cursor = Cursors.SizeNESW;

                            break;
                        case MoveModeType.Move:
                            Cursor = Cursors.SizeAll;

                            break;
                        case MoveModeType.ResizeLeft:
                        case MoveModeType.ResizeRight:
                            Cursor = Cursors.SizeWE;

                            break;
                        case MoveModeType.ResizeTop:
                        case MoveModeType.ResizeBottom:
                            Cursor = Cursors.SizeNS;

                            break;
                        default:
                            Cursor = Cursors.Default;

                            break;
                    }
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                ++_moveCount;

                if (_moveCount > 100)
                {
                    _moveCount = 2;
                }

                var rectangle = new Rectangle(0, 0, picCanvas.Width, picCanvas.Height);
                Cursor.Clip = picCanvas.RectangleToScreen(rectangle);

                if (_moveMode != MoveModeType.None)
                {
                    switch (_moveMode)
                    {
                        case MoveModeType.ResizeTopLeft:
                        case MoveModeType.ResizeBottomRight:
                            Cursor = Cursors.SizeNWSE;

                            break;
                        case MoveModeType.ResizeTopRight:
                        case MoveModeType.ResizeBottomLeft:
                            Cursor = Cursors.SizeNESW;

                            break;
                        case MoveModeType.Move:
                            Cursor = Cursors.SizeAll;

                            break;
                        case MoveModeType.ResizeLeft:
                        case MoveModeType.ResizeRight:
                            Cursor = Cursors.SizeWE;

                            break;
                        case MoveModeType.ResizeTop:
                        case MoveModeType.ResizeBottom:
                            Cursor = Cursors.SizeNS;

                            break;
                        case MoveModeType.None:
                            break;
                        case MoveModeType.SelectionBox:
                            break;
                        default:
                            Cursor = Cursors.Default;

                            break;
                    }

                    if (_moveCount >= 2)
                    {
                        _elementChanged = true;
                    }
                }

                switch (_moveMode)
                {
                    case MoveModeType.SelectionBox:
                        {
                            var size = new Size(mouseLocation.X - _mAnchor.X, mouseLocation.Y - _mAnchor.Y);
                            rectangle = new Rectangle(_mAnchor, size);
                            _selectionRect = GetPositiveRect(rectangle);
                            _showSelectionRect = true;
                            picCanvas.Invalidate();

                            break;
                        }
                    case MoveModeType.ResizeTopLeft:
                        {
                            mouseLocation.Offset(3, 0);

                            if (ActiveElement != null)
                            {
                                var point = new Point(ActiveElement.X + ActiveElement.Width, ActiveElement.Y + ActiveElement.Height);
                                ActiveElement.Location = mouseLocation;
                                var size = ActiveElement.Size;
                                var location = ActiveElement.Location;
                                size.Width = point.X - mouseLocation.X;
                                size.Height = point.Y - mouseLocation.Y;

                                if (size.Width < 1)
                                {
                                    location.X = point.X - 1;
                                    size.Width = 1;
                                }

                                if (size.Height < 1)
                                {
                                    location.Y = point.Y - 1;
                                    size.Height = 1;
                                }

                                ActiveElement.Size = size;
                                ActiveElement.Location = location;
                            }

                            picCanvas.Invalidate();

                            break;
                        }
                    case MoveModeType.ResizeTopRight:
                        {
                            mouseLocation.Offset(-3, 0);

                            if (ActiveElement != null)
                            {
                                var point = new Point(ActiveElement.X + ActiveElement.Width,ActiveElement.Y + ActiveElement.Height);
                                var location = ActiveElement.Location;
                                location.Y = mouseLocation.Y;
                                ActiveElement.Location = location;
                                var size = ActiveElement.Size;
                                size.Height = point.Y - mouseLocation.Y;
                                size.Width = mouseLocation.X - ActiveElement.X;

                                if (size.Height < 1)
                                {
                                    location.Y = point.Y - 1;
                                    size.Height = 1;
                                }

                                if (size.Width < 1)
                                {
                                    size.Width = 1;
                                }

                                location.X = ActiveElement.Location.X;
                                ActiveElement.Size = size;
                                ActiveElement.Location = location;
                            }

                            picCanvas.Invalidate();

                            break;
                        }
                    case MoveModeType.ResizeBottomRight:
                        {
                            mouseLocation.Offset(-3, -3);
                            if (ActiveElement != null)
                            {
                                var size = ActiveElement.Size;
                                size.Width = mouseLocation.X - ActiveElement.X;
                                size.Height = mouseLocation.Y - ActiveElement.Y;

                                if (size.Width < 1)
                                {
                                    size.Width = 1;
                                }

                                if (size.Height < 1)
                                {
                                    size.Height = 1;
                                }

                                ActiveElement.Size = size;
                            }

                            picCanvas.Invalidate();

                            break;
                        }
                    case MoveModeType.ResizeBottomLeft:
                        {
                            mouseLocation.Offset(0, -3);

                            if (ActiveElement != null)
                            {
                                var point = new Point(ActiveElement.X + ActiveElement.Width, ActiveElement.Y + ActiveElement.Height);
                                var location = ActiveElement.Location;
                                location.X = mouseLocation.X;
                                ActiveElement.Location = location;
                                var size = ActiveElement.Size;
                                size.Width = point.X - mouseLocation.X;
                                size.Height = mouseLocation.Y - ActiveElement.Y;

                                if (size.Width < 1)
                                {
                                    location.X = point.X - 1;
                                    size.Width = 1;
                                }

                                if (size.Height < 1)
                                {
                                    size.Height = 1;
                                }

                                location.Y = ActiveElement.Y;
                                ActiveElement.Size = size;
                                ActiveElement.Location = location;
                            }

                            picCanvas.Invalidate();

                            break;
                        }
                    case MoveModeType.Move:
                        {
                            if (ActiveElement != null)
                            {
                                var elementLocation = ActiveElement.Location;
                                ActiveElement.Location = mouseLocation;
                                int dx = ActiveElement.X - elementLocation.X;
                                int dy = ActiveElement.Y - elementLocation.Y;

                                foreach (var element in ElementStack.GetSelectedElements())
                                {
                                    if (element == ActiveElement)
                                    {
                                        continue;
                                    }

                                    var location = element.Location;
                                    location.Offset(dx, dy);
                                    element.Location = location;
                                }
                            }

                            picCanvas.Invalidate();

                            break;
                        }
                    case MoveModeType.ResizeLeft:
                        {
                            mouseLocation.Offset(3, 0);

                            if (ActiveElement != null)
                            {
                                var point = new Point(ActiveElement.X + ActiveElement.Width, ActiveElement.Y + ActiveElement.Height);
                                int y = ActiveElement.Y;
                                ActiveElement.Location = mouseLocation;
                                var size = ActiveElement.Size;
                                var location = ActiveElement.Location;
                                size.Width = point.X - mouseLocation.X;

                                if (size.Width < 1)
                                {
                                    location.X = point.X - 1;
                                    size.Width = 1;
                                }

                                location.Y = y;
                                ActiveElement.Size = size;
                                ActiveElement.Location = location;
                            }

                            picCanvas.Invalidate();

                            break;
                        }
                    case MoveModeType.ResizeTop:
                        {
                            mouseLocation.Offset(0, 3);

                            if (ActiveElement != null)
                            {
                                var point = new Point(ActiveElement.X + ActiveElement.Width, ActiveElement.Y + ActiveElement.Height);
                                int x = ActiveElement.X;
                                ActiveElement.Location = mouseLocation;
                                var size = ActiveElement.Size;
                                var location = ActiveElement.Location;
                                size.Height = point.Y - mouseLocation.Y;

                                if (size.Height < 1)
                                {
                                    location.Y = point.Y - 1;
                                    size.Height = 1;
                                }

                                location.X = x;
                                ActiveElement.Size = size;
                                ActiveElement.Location = location;
                            }

                            picCanvas.Invalidate();

                            break;
                        }
                    case MoveModeType.ResizeRight:
                        {
                            mouseLocation.Offset(-3, 0);
                            if (ActiveElement != null)
                            {
                                var size = ActiveElement.Size;
                                size.Width = mouseLocation.X - ActiveElement.X;

                                if (size.Width < 1)
                                {
                                    size.Width = 1;
                                }

                                ActiveElement.Size = size;
                            }

                            picCanvas.Invalidate();

                            break;
                        }
                    case MoveModeType.ResizeBottom:
                        {
                            mouseLocation.Offset(0, -3);
                            if (ActiveElement != null)
                            {
                                var size = ActiveElement.Size;
                                size.Height = mouseLocation.Y - ActiveElement.Y;

                                if (size.Height < 1)
                                {
                                    size.Height = 1;
                                }

                                ActiveElement.Size = size;
                            }

                            picCanvas.Invalidate();

                            break;
                        }
                }
            }

            _lastPos = mouseLocation;
        }

        private void PicCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            var rectangle = new Rectangle();
            var point = new Point(e.X, e.Y);
            ElementStack.GetElementFromPoint(point);
            _showSelectionRect = false;
            Cursor.Clip = rectangle;

            if (_moveMode == MoveModeType.SelectionBox)
            {
                BaseElement element = null;

                foreach (var baseElement in ElementStack.GetElements())
                {
                    if (baseElement.ContainsTest(_selectionRect))
                    {
                        baseElement.Selected = true;
                        element = baseElement;
                    }
                    else if ((ModifierKeys & Keys.Control) <= Keys.None)
                    {
                        baseElement.Selected = false;
                    }
                }

                SetActiveElement(element);
            }

            if ((_moveMode == MoveModeType.None || _moveMode == MoveModeType.SelectionBox || !_elementChanged ? 0 : 1) != 0)
            {
                CreateUndoPoint("Element Moved");
                _elementChanged = false;
            }

            if ((e.Button & MouseButtons.Right) > MouseButtons.None)
            {
                var mnuContextMenu = m_mnuContextMenu;
                GetContextMenu(ref ActiveElement, mnuContextMenu);
                mnuContextMenu.Show(picCanvas, point);
                ClearContextMenu(mnuContextMenu);
            }

            SetActiveElement(ActiveElement);
            picCanvas.Invalidate();
            _moveMode = MoveModeType.None;
            _mAnchorOffset = new Size(0, 0);
        }

        private void PicCanvas_Paint(object sender, PaintEventArgs e)
        {
            Render(e.Graphics);
        }

        private void PnlCanvasScroller_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void RebuildTabPages()
        {
            TabPager.TabPages.Clear();
            int pageIndex = -1;

            foreach (var groupElement in Stacks)
            {
                pageIndex++;
                TabPager.TabPages.Add(new TabPage(Convert.ToString(pageIndex)));

                if (ElementStack == groupElement)
                {
                    TabPager.SelectedIndex = pageIndex;
                }
            }
        }

        private void Redo()
        {
            if (_currentUndoPoint < _undoPoints.Count)
            {
                ++_currentUndoPoint;
                RevertToUndoPoint(_currentUndoPoint);
            }

            if (_currentUndoPoint == _undoPoints.Count - 1)
            {
                mnuEditRedo.Enabled = false;
            }

            mnuEditUndo.Enabled = true;
        }

        private void RefreshElementList()
        {
            cboElements.Items.Clear();
            cboElements.Items.AddRange(ElementStack.GetElements().ToArray<object>());
        }

        private void RefreshView(object sender)
        {
            RefreshElementList();
            cboElements.SelectedItem = ActiveElement;

            if (ElementStack.GetSelectedElements().Count > 1)
            {
                pgElementProperties.SelectedObjects = ElementStack.GetSelectedElements().ToArray<object>();
            }
            else
            {
                pgElementProperties.SelectedObject = ActiveElement;
            }
        }

        private void Render(Graphics target)
        {
            var graphics = Graphics.FromImage(_canvas);

            if (!PluginClearsCanvas)
            {
                graphics.Clear(Color.Black);
            }

            var hookPreRender = HookPreRender;
            hookPreRender?.Invoke(_canvas);

            if ((!_showPage0 || ElementStack == Stacks[0] ? 0 : 1) != 0)
            {
                Stacks[0].Render(graphics);
            }

            ElementStack.Render(graphics);

            foreach (var baseElement in ElementStack.GetElements())
            {
                if ((baseElement.Selected && baseElement != ActiveElement ? 1 : 0) != 0)
                {
                    baseElement.DrawBoundingBox(graphics, false);
                }
            }

            ActiveElement?.DrawBoundingBox(graphics, true);

            if (_showSelectionRect)
            {
                graphics.FillRectangle(_selBg, _selectionRect);
                graphics.DrawRectangle(_selFg, _selectionRect);
            }

            var hookPostRender = HookPostRender;
            hookPostRender?.Invoke(_canvas);

            graphics.Dispose();
            target.DrawImage(_canvas, 0, 0);
        }

        private void RevertToUndoPoint(int index)
        {
            var undoPoint = _undoPoints[index];
            GumpProperties = (GumpProperties)undoPoint.GumpProperties.Clone();
            Stacks = new List<GroupElement>();

            foreach (var groupElement in undoPoint.Stack)
            {
                var clone = (GroupElement)groupElement.Clone();
                Stacks.Add(clone);

                if (undoPoint.ElementStack == groupElement)
                {
                    ElementStack = clone;
                }
            }

            RebuildTabPages();
            Debug.WriteLine($"Restored to undo point: {Convert.ToString(index + 1)}");
            picCanvas.Invalidate();
            SetActiveElement(null, true);
            _currentUndoPoint = index;
        }

        private void SaveTo(string path)
        {
            StatusBar.Text = "Saving gump...";
            ElementStack.UpdateParent -= ChangeActiveElementEventHandler;
            ElementStack.Repaint -= RefreshView;

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, Stacks);
                binaryFormatter.Serialize(fileStream, GumpProperties);
            }

            ElementStack.UpdateParent += ChangeActiveElementEventHandler;
            ElementStack.Repaint += RefreshView;
            StatusBar.Text = "";
        }

        private void SelectAll()
        {
            foreach (var baseElement in ElementStack.GetElements())
            {
                baseElement.Selected = true;
            }

            picCanvas.Invalidate();
        }

        private void SetActiveElement(BaseElement element, bool deselectOthers = false)
        {
            if (deselectOthers)
            {
                foreach (var baseElement in ElementStack.GetElements())
                {
                    baseElement.Selected = false;
                }
            }

            if (ActiveElement != element)
            {
                RefreshElementList();
                ActiveElement = element;
                cboElements.SelectedItem = element;

                if (element != null)
                {
                    element.Selected = true;
                }
            }

            if (ElementStack.GetSelectedElements().Count > 1)
            {
                pgElementProperties.SelectedObjects = ElementStack.GetSelectedElements().ToArray<object>();
            }
            else if (element != null)
            {
                pgElementProperties.SelectedObject = element;
            }
            else
            {
                pgElementProperties.SelectedObject = GumpProperties;
            }
        }

        private void TabPager_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabPager.SelectedIndex != -1)
            {
                ChangeActiveStack(TabPager.SelectedIndex);
            }

            RefreshElementList();
        }

        private void TreGumplings_DoubleClick(object sender, EventArgs e)
        {
            if (!(treGumplings.SelectedNode.Tag is TreeGumpling))
            {
                return;
            }

            var groupElement = (GroupElement)((TreeGumpling)treGumplings.SelectedNode.Tag).Gumpling.Clone();
            groupElement.IsBaseWindow = false;
            groupElement.RecalculateBounds();
            groupElement.Location = new Point(0, 0);
            AddElement(groupElement);
        }

        private void TreGumplings_MouseUp(object sender, MouseEventArgs e)
        {
            treGumplings.SelectedNode = treGumplings.GetNodeAt(new Point(e.X, e.Y));
        }

        private void Undo()
        {
            --_currentUndoPoint;
            RevertToUndoPoint(_currentUndoPoint);

            if (_currentUndoPoint == 0)
            {
                mnuEditUndo.Enabled = false;
            }

            mnuEditRedo.Enabled = true;
        }

        public void WritePluginsToLoad()
        {
            if (PluginTypesToLoad != null)
            {
                var fileStream = new FileStream($@"{Application.StartupPath}\Plugins\LoadInfo", FileMode.Create);
                new BinaryFormatter().Serialize(fileStream, PluginTypesToLoad);
                fileStream.Close();
            }
            else
            {
                if (!File.Exists($@"{Application.StartupPath}\Plugins\LoadInfo"))
                {
                    return;
                }

                File.Delete($@"{Application.StartupPath}\Plugins\LoadInfo");
            }
        }

        private void DesignerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var plugin in _availablePlugins.Where(plugin => plugin.IsLoaded))
            {
                plugin.Unload();
            }
        }

        private void DesignerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _selFg?.Dispose();
            _selBg?.Dispose();

            WritePluginsToLoad();
        }
    }
}