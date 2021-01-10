using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using GumpStudio.Classes;
using Ultima;

namespace GumpStudio.Forms
{
    public partial class GumpArtBrowser
    {
        private static List<CacheEntry> _cache;
        public int GumpId;
        private bool _buildingCache;

        public GumpArtBrowser()
        {
            InitializeComponent();
        }

        private void BuildCache()
        {
            _buildingCache = true;

            Show();

            lstGump.BeginUpdate();
            lstGump.Items.Clear();
            lstGump.EndUpdate();

            lblWait.Text = "Please Wait, Generating Art Cache...";
            lblWait.Show();
            lblSize.Hide();

            _cache = new List<CacheEntry>(ushort.MaxValue);
            Application.DoEvents();

            int index = 0;
            int maxValue = Gumps.FileIndex.Index.Length;

            try
            {
                do
                {
                    if (index % 2000 == 0)
                    {
                        lblWait.Text = $"Please Wait, Generating Art Cache... {(int)(100 * (index / (double)maxValue)):f}%";
                        Application.DoEvents();
                    }

                    var isValid = Gumps.IsValidIndex(index);
                    if (isValid)
                    {
                        var extra = Gumps.FileIndex.Index[index].Extra;
                        short width = (short)((extra >> 16) & 0xFFFF);
                        short height = (short)(extra & 0xFFFF);

                        _cache.Add(new CacheEntry
                        {
                            Id = index,
                            Size = new Size(width, height)
                        });
                    }

                    ++index;
                }
                while (index <= maxValue);

                using (var fileStream = new FileStream($"{Application.StartupPath}/GumpArt.cache", FileMode.Create))
                {
                    new BinaryFormatter().Serialize(fileStream, _cache);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating cache file:\r\n{ex.Message}");
            }
            finally
            {
                lblWait.Hide();
                lblSize.Show();
                _buildingCache = false;
                Application.DoEvents();
            }
        }

        private void CmdCache_Click(object sender, EventArgs e)
        {
            const string msg = "Rebuilding the cache may take several minutes depending on the speed of your computer.\r\nAre you sure you want to continue?";
            cmdOK.Enabled = false;

            if (MessageBox.Show(msg, "Rebuild Cache", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                BuildCache();
                PopulateListbox();
            }
            cmdOK.Enabled = true;
        }

        private void CmdOK_Click(object sender, EventArgs e)
        {
            GumpId = Convert.ToInt32(lstGump.SelectedItem);
            DialogResult = DialogResult.OK;
        }

        private void GumpArtBrowser_Load(object sender, EventArgs e)
        {
            if (_cache == null)
            {
                FileStream fileStream = null;
                if (!File.Exists($"{Application.StartupPath}/GumpArt.cache"))
                {
                    BuildCache();
                }
                else
                {
                    try
                    {
                        fileStream = new FileStream($"{Application.StartupPath}/GumpArt.cache", FileMode.Open);
                        _cache = (List<CacheEntry>)new BinaryFormatter().Deserialize(fileStream);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error Reading cache file:\r\n{ex.Message}");
                    }
                    finally
                    {
                        fileStream?.Close();
                    }
                }
            }

            PopulateListbox();
        }

        private void LstGump_DoubleClick(object sender, EventArgs e)
        {
            GumpId = Convert.ToInt32(lstGump.SelectedItem);
            DialogResult = DialogResult.OK;
        }

        private void LstGump_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (_buildingCache)
            {
                return;
            }

            try
            {
                if (e.Index == -1)
                {
                    return;
                }

                Size size1 = new Size();
                Graphics graphics = e.Graphics;
                using (Bitmap gump = Gumps.GetGump(_cache[e.Index].Id))
                {
                    Size size2 = _cache[e.Index].Size;
                    size1.Width = size2.Width <= 100 ? size2.Width : 100;
                    size1.Height = size2.Height <= 100 ? size2.Height : 100;
                    Rectangle rect = new Rectangle(e.Bounds.Location, size1);
                    rect.Offset(45, 3);
                    graphics.FillRectangle(
                        (e.State & DrawItemState.Selected) > DrawItemState.None
                            ? SystemBrushes.Highlight
                            : SystemBrushes.Window, e.Bounds);

                    graphics.DrawString($"0x{_cache[e.Index].Id:X}", Font, SystemBrushes.WindowText, e.Bounds.X, e.Bounds.Y);
                    graphics.DrawImage(gump, rect);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error rendering the gump art, try rebuilding the cache.\r\n\r\n{ex.Message}");
            }
        }

        private void LstGump_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (_buildingCache)
            {
                return;
            }

            int height = _cache[e.Index].Size.Height;
            int num = height <= 100 ? height >= 15 ? height : 15 : 100;
            e.ItemHeight = num + 5;
        }

        private void LstGump_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_buildingCache)
            {
                return;
            }

            picFullSize.Image?.Dispose();

            picFullSize.Image = Gumps.GetGump(Convert.ToInt32(lstGump.SelectedItem));
            lblSize.Text = $"Width: {picFullSize.Image.Width}   Height: {picFullSize.Image.Height}";
        }

        private void PopulateListbox()
        {
            if (_buildingCache)
            {
                return;
            }

            lstGump.BeginUpdate();
            try
            {
                lstGump.Items.Clear();
                foreach (CacheEntry gumpCacheEntry in _cache)
                {
                    lstGump.Items.Add(gumpCacheEntry.Id);
                }

                lstGump.SelectedItem = GumpId;
            }
            finally
            {
                lstGump.EndUpdate();
            }
        }
    }
}
