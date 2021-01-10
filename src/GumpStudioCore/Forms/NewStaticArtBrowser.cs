using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using GumpStudio.Classes;
using Ultima;

namespace GumpStudio.Forms
{
    public partial class NewStaticArtBrowser
    {
        private bool _searchSomething;
        private Bitmap _blankCache;
        private bool _buildingCache;
        private List<CacheEntry> _cache;
        private Size _displaySize;
        private Point _hoverPos;
        private int _numX;
        private int _numY;
        private Bitmap[] _rowCache;
        private int _selectedIndex;
        private int _startIndex;

        public int ItemId
        {
            get => _cache[_selectedIndex].Id;
            set
            {
                if (_cache == null)
                {
                    return;
                }

                for (int index = 0; index < _cache.Count; ++index)
                {
                    if (_cache[index].Id != value)
                    {
                        continue;
                    }

                    _selectedIndex = index;
                    lblName.Text = $"Name: {TileData.ItemTable[ItemId].Name}";
                    lblSize.Text = $"Size: {Art.GetStatic(ItemId).Width} x {Art.GetStatic(ItemId).Height}";
                }
            }
        }

        public NewStaticArtBrowser()
        {
            Load += NewStaticArtBrowser_Load;
            Resize += NewStaticArtBrowser_Resize;

            _displaySize = new Size(45, 45);
            _hoverPos = new Point(-1, -1);
            _selectedIndex = 0;
            _buildingCache = false;

            InitializeComponent();
        }

        private void BuildCache()
        {
            if (_buildingCache)
            {
                return;
            }

            _buildingCache = true;
            lblWait.Text = "Please Wait, Generating Art Cache...";
            Show();
            FileStream fileStream = null;

            try
            {
                lblWait.Visible = true;
                Application.DoEvents();

                int upperBound = TileData.ItemTable.GetUpperBound(0);
                _cache = new List<CacheEntry>(upperBound);
                for (int index = 0; index <= upperBound; ++index)
                {
                    if (index % 1000 == 0)
                    {
                        lblWait.Text = $"Please Wait, Generating Static Art Cache...  \r\n{index / (double)TileData.ItemTable.GetUpperBound(0) * 100.0:F}%";
                        Application.DoEvents();
                    }

                    if (Art.IsValidStatic(index, out byte[] artSize))
                    {
                        _cache.Add(new CacheEntry
                        {
                            Id = index,
                            Size = new Size(artSize[0], artSize[1]),
                            Name = TileData.ItemTable[index].Name
                        });
                    }
                }

                fileStream = new FileStream($"{Application.StartupPath}/StaticArt.cache", FileMode.Create);
                new BinaryFormatter().Serialize(fileStream, _cache);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating cache file:\r\n{ex.Message}");
            }
            finally
            {
                fileStream?.Close();
                lblWait.Visible = false;
                Application.DoEvents();
                _buildingCache = false;
            }
        }

        private void CmdCache_Click(object sender, EventArgs e)
        {
            const string msg = "Rebuilding the cache may take several minutes depending on the speed of your computer.\r\nAre you sure you want to continue?";
            if (MessageBox.Show(msg, "Rebuild Cache", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            {
                return;
            }

            BuildCache();

            _rowCache = new Bitmap[(int)Math.Round(_cache.Count / (double)_numX) + 1 + 1];
            ItemId = 0;
            picCanvas.Invalidate();
        }

        private void CmdSearch_Click(object sender, EventArgs e)
        {
            int index1 = -1;
            int index2 = _selectedIndex == -1 ? 0 : _selectedIndex;
            while (index1 == -1 && index2 < _cache.Count - 1)
            {
                ++index2;
                // TODO: may be broken check this condition
                if (_cache[index2].Name.Contains(txtSearch.Text))
                {
                    index1 = index2;
                }
            }

            if (index1 != -1)
            {
                ItemId = _cache[index1].Id;
            }

            if (index1 == -1 && index2 > 0 && !_searchSomething)
            {
                _selectedIndex = 0;
                _searchSomething = true;
                CmdSearch_Click(RuntimeHelpers.GetObjectValue(sender), e);
            }

            VScrollBar.Value = _selectedIndex / _numX;
            VScroll_Scroll(VScroll, null);
            _searchSomething = false;
        }

        private void DrawGrid(Graphics g)
        {
            int numX = _numX;
            for (int index = 0; index <= numX; ++index)
            {
                g.DrawLine(Pens.Black, index * _displaySize.Width, 0, index * _displaySize.Width, (_numY + 1) * _displaySize.Height);
            }

            int num = _numY + 1;
            for (int index = 0; index <= num; ++index)
            {
                g.DrawLine(Pens.Black, 0, index * _displaySize.Height, _numX * _displaySize.Width, index * _displaySize.Height);
            }
        }

        private void DrawHover(Graphics g)
        {
            if (_buildingCache)
            {
                return;
            }

            int x = _hoverPos.X;
            int y = _hoverPos.Y;
            int index = _startIndex + x + (y * _numX);
            if (index >= _cache.Count)
            {
                return;
            }

            int id = _cache[index].Id;
            using (Bitmap bitmap = Art.GetStatic(id, false))
            {
                Point point = new Point
                {
                    X = (int)Math.Round((x * _displaySize.Width) + (_displaySize.Width / 2.0)) - (int)Math.Round(bitmap.Width / 2.0) - 3
                };
                if (point.X < 0)
                {
                    point.X = 0;
                }

                if (point.X + bitmap.Width > picCanvas.Width)
                {
                    point.X = picCanvas.Width - bitmap.Width - 3;
                }

                point.Y = (int)Math.Round((y * _displaySize.Height) + (_displaySize.Height / 2.0)) - (int)Math.Round(bitmap.Height / 2.0) - 3;
                if (point.Y < 0)
                {
                    point.Y = 0;
                }

                if (point.Y + bitmap.Height > picCanvas.Height)
                {
                    point.Y = picCanvas.Height - bitmap.Height - 3;
                }

                Rectangle rect = new Rectangle(point, bitmap.Size);
                using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(sbyte.MaxValue, Color.Black)))
                {
                    g.FillRectangle(solidBrush, point.X + 5, point.Y + 5, rect.Width, rect.Height);
                    g.FillRectangle(Brushes.White, rect);
                    g.DrawRectangle(Pens.Black, rect);
                    g.DrawImage(bitmap, point);

                    lblName.Text = $"Name: {TileData.ItemTable[id].Name}";
                    lblSize.Text = $"Size: {bitmap.Width} x {bitmap.Height}";
                    lblID.Text = $"ID: {id} - hex:0x{id:X}";
                }
            }
        }

        private Bitmap GetRowImage(int row)
        {
            if (row >= _rowCache.Length)
            {
                if (_blankCache != null)
                {
                    return _blankCache;
                }

                Bitmap bitmap = new Bitmap(_numX * _displaySize.Width, _displaySize.Height, PixelFormat.Format16bppRgb565);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.Gray);
                }

                _blankCache = bitmap;
                return bitmap;
            }

            if (_rowCache[row] != null)
            {
                return _rowCache[row];
            }

            Bitmap bitmap1 = new Bitmap(_numX * _displaySize.Width, _displaySize.Height, PixelFormat.Format16bppRgb565);
            using (Graphics graphics1 = Graphics.FromImage(bitmap1))
            {
                graphics1.Clear(Color.Gray);
                Region clip = graphics1.Clip;

                Rectangle rect = new Rectangle(0, 0, _numX * _displaySize.Width, _numY * _displaySize.Height);

                graphics1.Clip = new Region(rect);
                int num = _numX - 1;
                for (int index1 = 0; index1 <= num; ++index1)
                {
                    int index2 = (row * _numX) + index1;
                    if (index2 >= _cache.Count)
                    {
                        continue;
                    }

                    using (Bitmap bitmap2 = Art.GetStatic(_cache[index2].Id, false))
                    {
                        rect = new Rectangle(index1 * _displaySize.Width, 0, _displaySize.Width, _displaySize.Height);
                        using (Region region2 = new Region(rect))
                        {
                            graphics1.Clip = region2;
                            graphics1.FillRectangle(Brushes.White, index1 * _displaySize.Width, 0, _displaySize.Width, _displaySize.Height);
                            graphics1.DrawImage(bitmap2, (index1 * _displaySize.Width) + 1, 0);
                        }
                    }
                }

                graphics1.Clip = clip;
            }

            _rowCache[row] = bitmap1;
            return bitmap1;
        }

        private void NewStaticArtBrowser_Load(object sender, EventArgs e)
        {
            if (_cache == null)
            {
                FileStream fileStream = null;
                if (!File.Exists($"{Application.StartupPath}/StaticArt.cache"))
                {
                    BuildCache();
                }
                else
                {
                    try
                    {
                        fileStream = new FileStream($"{Application.StartupPath}/StaticArt.cache", FileMode.Open);
                        _cache = (List<CacheEntry>)new BinaryFormatter().Deserialize(fileStream);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error reading cache file:\r\n{ex.Message}");
                    }
                    finally
                    {
                        fileStream?.Close();
                    }
                }
            }
            picCanvas.Width = ClientSize.Width - VScrollBar.Width;
            Show();
            VScrollBar.Maximum = (int)Math.Round(_cache.Count / (double)_numX) + 1;
            VScrollBar.LargeChange = _numY - 1;
            if (_rowCache == null)
            {
                _rowCache = new Bitmap[(int)Math.Round(_cache.Count / (double)_numX) + 1 + 1];
            }

            VScrollBar.Value = _selectedIndex / _numX;
            VScroll_Scroll(VScroll, null);
            lblName.Text = $"Name: {TileData.ItemTable[_cache[_selectedIndex].Id].Name}";
            lblSize.Text = $"Size: {_cache[_selectedIndex].Size.Width} x {_cache[_selectedIndex].Size.Height}";
        }

        private void NewStaticArtBrowser_Resize(object sender, EventArgs e)
        {
            const int num1 = 11;
            int num2 = picCanvas.Height / _displaySize.Height;
            if (!(num1 != _numX || num2 != _numY))
            {
                return;
            }

            _numX = num1;
            _numY = num2;

            if (_cache == null)
            {
                return;
            }

            VScrollBar.Maximum = (int)Math.Round(_cache.Count / (double)_numX) + 1;
            VScrollBar.LargeChange = _numY - 1;
            picCanvas.Invalidate();
        }

        private void PicCanvas_DoubleClick(object sender, EventArgs e)
        {
            if (_buildingCache)
            {
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void PicCanvas_MouseLeave(object sender, EventArgs e)
        {
            if (_buildingCache)
            {
                return;
            }

            _hoverPos = new Point(-1, -1);
            picCanvas.Invalidate();
            lblName.Text = $"Name: {TileData.ItemTable[_cache[_selectedIndex].Id].Name}";
            lblSize.Text = $"Size: {_cache[_selectedIndex].Size.Width} x {_cache[_selectedIndex].Size.Height}";
            lblID.Text = $"ID: {_cache[_selectedIndex].Id}(0x{_cache[_selectedIndex].Id:X})";
        }

        private void PicCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_buildingCache)
            {
                return;
            }

            Point point = new Point(e.X / _displaySize.Width, e.Y / _displaySize.Height);
            if (point.X >= 11 || !(point.X != _hoverPos.X || point.Y != _hoverPos.Y))
            {
                return;
            }

            _hoverPos = point;
            picCanvas.Invalidate();
        }

        private void PicCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_buildingCache)
            {
                return;
            }

            int index = (e.X / _displaySize.Width) + (e.Y / _displaySize.Height * _numX) + _startIndex;
            if (index >= _cache.Count)
            {
                return;
            }

            ItemId = _cache[index].Id;
            picCanvas.Invalidate();
        }

        private void PicCanvas_Paint(object sender, PaintEventArgs e)
        {
            Render(e.Graphics);
            if (_hoverPos.Equals(new Point(-1, -1)))
            {
                return;
            }

            DrawHover(e.Graphics);
        }

        private void Render(Graphics g)
        {
            if (_cache == null || _rowCache == null)
            {
                return;
            }

            Rectangle rect = new Rectangle();
            g.Clear(Color.Gray);
            DrawGrid(g);
            Region clip = g.Clip;
            int num = _startIndex / _numX;
            bool isSelected = false;
            int numY = _numY;
            for (int index = 0; index <= numY; ++index)
            {
                g.DrawImage(GetRowImage(index + num), 0, index * _displaySize.Height);
                if ((isSelected || index + num != _selectedIndex / _numX ? 0 : 1) == 0)
                {
                    continue;
                }

                isSelected = true;
                rect = new Rectangle(_selectedIndex % _numX * _displaySize.Width, index * _displaySize.Height, _displaySize.Width, _displaySize.Height);
            }

            DrawGrid(g);

            if (isSelected)
            {
                using (var region = new Region(rect))
                {
                    rect.Inflate(5, 5);
                    using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(sbyte.MaxValue, Color.Blue)))
                    {
                        g.FillRectangle(solidBrush, rect);
                        g.DrawRectangle(Pens.Blue, rect);
                    }

                    rect.Inflate(-5, -5);

                    g.Clip = region;
                    g.DrawImage(Art.GetStatic(_cache[_selectedIndex].Id, false), rect.Location);
                    g.Clip = clip;
                }
            }
            g.Clip = clip;
        }

        private void VScroll_Scroll(object sender, ScrollEventArgs e)
        {
            _startIndex = VScrollBar.Value * _numX;
            picCanvas.Invalidate();
        }
    }
}
