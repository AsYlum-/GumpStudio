using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using GumpStudio.Forms;
using GumpStudio.Plugins;

namespace WallPaperPlugin
{
    public class WallPaperPlugin : BasePlugin
    {
        private DesignerForm _designer;
        private Image _image;
        private string _imagePath;
        private Color _color;
        private bool _displayImage;

        public WallPaperPlugin()
        {
            _color = Color.Black;
            _displayImage = false;
        }

        public override PluginInfo GetPluginInfo()
        {
            return new PluginInfo
            {
                AuthorEmail = "buffner@tkpups.com",
                AuthorName = "Bradley Uffner",
                Description = "Displays a solid color, or image as the background in Gump Studio.",
                PluginName = Name,
                Version = $"{Assembly.GetExecutingAssembly().GetName().Version.Major}.{Assembly.GetExecutingAssembly().GetName().Version.Minor}"
            };
        }

        public override string Name => "WallPaper";

        public override void Load(DesignerForm frmDesigner)
        {
            _designer = frmDesigner;
            var menuItem = new MenuItem("Configure Wallpaper", ConfigureWallPaper);
            frmDesigner.MnuPlugins.MenuItems.Add(menuItem);
            _designer.HookPreRender += Render;

            _designer.PluginClearsCanvas = true;

            if (File.Exists(_designer.AppPath + @"\Plugins\Wallpaper.config"))
            {
                using (var fileStream = new FileStream(_designer.AppPath + @"\Plugins\Wallpaper.config", FileMode.Open))
                using (var streamReader = new StreamReader(fileStream))
                {
                    _imagePath = streamReader.ReadLine();
                    _color = Color.FromArgb(Convert.ToInt32(streamReader.ReadLine()));
                    _displayImage = streamReader.ReadLine() == "1";
                }
            }

            if (File.Exists(_imagePath))
            {
                _image = Image.FromFile(_imagePath);
            }

            base.Load(frmDesigner);
        }

        private void ConfigureWallPaper(object sender, EventArgs e)
        {
            FrmConfig frmConfig = new FrmConfig
            {
                BackGroundColor = _color,
                ImagePath = _imagePath,
                ChkImage = { Checked = _displayImage }
            };

            if (frmConfig.ShowDialog() == DialogResult.OK)
            {
                _color = frmConfig.BackGroundColor;
                _imagePath = frmConfig.ImagePath;
                _designer.PicCanvas.Refresh();

                if (frmConfig.ChkImage.Checked)
                {
                    _image?.Dispose();

                    _image = Image.FromFile(frmConfig.ImagePath);
                    _displayImage = true;
                }
                else
                {
                    _displayImage = false;
                }

                using(var fileStream = new FileStream($@"{_designer.AppPath}\Plugins\Wallpaper.config", FileMode.Create))
                using(var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(_imagePath);
                    streamWriter.WriteLine(_color.ToArgb());
                    streamWriter.WriteLine(_displayImage ? "1" : "0");
                }
            }

            _designer.PicCanvas.Refresh();
        }

        private void Render(Bitmap canvas)
        {
            var graphics = Graphics.FromImage(canvas);
            if (_displayImage && _image != null)
            {
                graphics.DrawImage(_image, 0, 0);

                if (_image.Height < _designer.PicCanvas.Height)
                {
                    using (var solidBrush = new SolidBrush(_color))
                    {
                        graphics.FillRectangle(solidBrush, 0, _image.Height, _designer.PicCanvas.Width, _designer.PicCanvas.Height - _image.Height);
                    }
                }

                if (_image.Width >= _designer.PicCanvas.Width)
                {
                    return;
                }

                using (var solidBrush = new SolidBrush(_color))
                {
                    graphics.FillRectangle(solidBrush, _image.Width, 0, _designer.PicCanvas.Width - _image.Width, _image.Height);
                }
            }
            else
            {
                graphics.Clear(_color);
            }
        }
    }
}
