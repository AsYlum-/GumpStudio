using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using GumpStudio.Elements;
using GumpStudio.Enums;
using GumpStudio.Forms;
using GumpStudio.Plugins;

namespace SnapToGrid
{
    public class SnapToGrid : BasePlugin
    {
        private DesignerForm _designer;
        private SnapToGridExtender _extender;
        private MenuItem _showGridMenu;
        private GridConfiguration _config;

        public SnapToGrid()
        {
            _config = new GridConfiguration(new Size(8, 8), Color.LightGray, false);
        }

        public override PluginInfo GetPluginInfo()
        {
            return new PluginInfo
            {
                AuthorEmail = "buffner@tkpups.com",
                AuthorName = "Bradley Uffner",
                Description = "Allows elements to be snapped to a grid. Use shift and mouse or arrow keys snap to grid.",
                PluginName = nameof(SnapToGrid),
                Version =
                    $"{Assembly.GetExecutingAssembly().GetName().Version.Major}.{Assembly.GetExecutingAssembly().GetName().Version.Minor}"
            };
        }

        public override void Load(DesignerForm frmDesigner)
        {
            _designer = frmDesigner;
            LoadConfig();
            if (_extender == null)
            {
                _extender = new SnapToGridExtender(_designer);
            }

            _extender.Config = _config;
            MenuItem menuItem1 = new MenuItem("Snap to Grid");
            _showGridMenu = new MenuItem("Show Grid", DoToggleGridMenu)
            {
                Checked = _config.ShowGrid
            };

            MenuItem menuItem2 = new MenuItem("Configure Grid...", DoConfigGridMenu);
            menuItem1.MenuItems.Add(_showGridMenu);
            menuItem1.MenuItems.Add(menuItem2);
            frmDesigner.MnuPlugins.MenuItems.Add(menuItem1);

            _designer.HookPreRender += RenderGrid;
            _designer.HookKeyDown += KeyDownHook;

            base.Load(frmDesigner);
        }

        public override string Name => GetPluginInfo().PluginName;

        public override void InitializeElementExtenders(BaseElement element)
        {
            element.AddExtender(_extender);
        }

        public override void MouseMoveHook(ref MouseMoveHookEventArgs e)
        {
            if (e.MoveMode != MoveModeType.Move || (uint)(e.Keys & Keys.Shift) <= 0U)
            {
                return;
            }

            e.MouseLocation = _extender.SnapToGrid(e.MouseLocation);
        }

        private void DoToggleGridMenu(object sender, EventArgs e)
        {
            _config.ShowGrid = !_config.ShowGrid;
            _showGridMenu.Checked = _config.ShowGrid;
            _designer.PicCanvas.Refresh();
        }

        private void RenderGrid(Bitmap target)
        {
            // TODO: rewrite this to something simpler
            bool showGrid = _config.ShowGrid;
            if (!showGrid)
            {
                return;
            }

            Rectangle rect = new Rectangle(0, 0, target.Width, target.Height);
            BitmapData bitmapData = target.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int width = _extender.Config.GridSize.Width;
            int horizontalPosition = 0;
            for (; ; )
            {
                int num5 = width >> 31 ^ horizontalPosition;
                int num6 = width >> 31 ^ target.Width - 1;
                if (num5 > num6)
                {
                    break;
                }

                int num8 = target.Height - 1;
                int height = _extender.Config.GridSize.Height;
                int num9 = num8;
                int num10 = 0;
                for (; ; )
                {
                    int num11 = height >> 31 ^ num10;
                    num6 = (height >> 31 ^ num9);
                    if (num11 > num6)
                    {
                        break;
                    }

                    int ofs = (bitmapData.Stride * num10) + (4 * horizontalPosition);

                    Marshal.WriteByte(bitmapData.Scan0, ofs + 0, _config.GridColor.B);
                    Marshal.WriteByte(bitmapData.Scan0, ofs + 1, _config.GridColor.G);
                    Marshal.WriteByte(bitmapData.Scan0, ofs + 2, _config.GridColor.R);
                    Marshal.WriteByte(bitmapData.Scan0, ofs + 3, byte.MaxValue);
                    num10 += height;
                }

                horizontalPosition += width;
            }

            target.UnlockBits(bitmapData);
        }

        private void DoConfigGridMenu(object sender, EventArgs e)
        {
            FrmGridConfig frmGridConfig = new FrmGridConfig
            {
                Config = _config
            };

            if (frmGridConfig.ShowDialog() == DialogResult.OK)
            {
                _config = frmGridConfig.Config;
                _extender.Config = _config;
                SaveConfig();
            }

            _designer.PicCanvas.Refresh();
        }

        public override void Unload()
        {
            SaveConfig();
        }

        private void LoadConfig()
        {
            if (!File.Exists($@"{_designer.AppPath}\Plugins\SnapToGrid.config"))
            {
                return;
            }

            using (var fileStream = new FileStream($@"{_designer.AppPath}\Plugins\SnapToGrid.config", FileMode.Open))
            {
                var formatter = new BinaryFormatter
                {
                    Binder = new GridConfiguration()
                };

                var obj = formatter.Deserialize(fileStream);
                _config = (GridConfiguration)obj;
            }
        }

        private void SaveConfig()
        {
            using (var fileStream = new FileStream($@"{_designer.AppPath}\Plugins\SnapToGrid.config", FileMode.Create))
            {
                new BinaryFormatter().Serialize(fileStream, _config);
            }
        }

        private void KeyDownHook(object sender, ref KeyEventArgs e)
        {
            if ((uint)(Control.ModifierKeys & Keys.Shift) <= 0U || _designer.ActiveElement == null || sender != _designer.CanvasFocus)
            {
                return;
            }

            bool flag = false;
            switch (e.KeyCode)
            {
                case Keys.Up:
                {
                    foreach (BaseElement baseElement in _designer.ElementStack.GetSelectedElements())
                    {
                        BaseElement baseElement2 = baseElement;
                        baseElement2.Y -= _config.GridSize.Height;
                        baseElement.Y = _extender.SnapYToGrid(baseElement.Y);
                    }
                    flag = true;
                    _designer.CreateUndoPoint();

                    break;
                }
                case Keys.Down:
                {
                    foreach (BaseElement baseElement3 in _designer.ElementStack.GetSelectedElements())
                    {
                        BaseElement baseElement2 = baseElement3;
                        baseElement2.Y += _config.GridSize.Height;
                        baseElement3.Y = _extender.SnapYToGrid(baseElement3.Y);
                    }
                    flag = true;
                    _designer.CreateUndoPoint();

                    break;
                }
                case Keys.Left:
                {
                    foreach (BaseElement baseElement4 in _designer.ElementStack.GetSelectedElements())
                    {
                        BaseElement baseElement2 = baseElement4;
                        baseElement2.X -= _config.GridSize.Width;
                        baseElement4.X = _extender.SnapXToGrid(baseElement4.X);
                    }
                    flag = true;
                    _designer.CreateUndoPoint();

                    break;
                }
                case Keys.Right:
                {
                    foreach (BaseElement baseElement5 in _designer.ElementStack.GetSelectedElements())
                    {
                        BaseElement baseElement2 = baseElement5;
                        baseElement2.X += _config.GridSize.Width;
                        baseElement5.X = _extender.SnapXToGrid(baseElement5.X);
                    }
                    flag = true;
                    _designer.CreateUndoPoint();

                    break;
                }
            }

            e.Handled = true;

            if (flag)
            {
                _designer.PicCanvas.Invalidate();
            }
        }
    }
}
