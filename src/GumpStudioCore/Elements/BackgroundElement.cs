using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Windows.Forms;
using GumpStudio.Editors;
using Ultima;

namespace GumpStudio.Elements
{
    [Serializable]
    public class BackgroundElement : ResizeableElement, IDisposable
    {
        private int _gumpId;

        private Image[] _multImageCache;

        [Editor(typeof(GumpIdPropEditor), typeof(UITypeEditor))]
        public int GumpId
        {
            get => _gumpId;
            set
            {
                bool flag = true;
                int backgroundPartId = 0;
                do
                {
                    Bitmap gump = Gumps.GetGump(backgroundPartId + value);
                    if (gump is null)
                    {
                        flag = false;
                    }
                    gump?.Dispose();

                    backgroundPartId++;
                }
                while (backgroundPartId <= 8);

                if (!flag)
                {
                    MessageBox.Show("Invalid GumpId");
                    return;
                }

                _gumpId = value;
                RefreshCache();
            }
        }

        public override string Type => "Background";

        public BackgroundElement()
        {
            _multImageCache = new Image[9];
            mSize = new Size(100, 100);
            _gumpId = 9200;
            RefreshCache();
        }

        public BackgroundElement(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _multImageCache = new Image[9];
            info.GetInt32("BackgroundElementVersion");
            GumpId = info.GetInt32("GumpID");
        }

        public void Dispose()
        {
            int num = 0;
            int num2;
            do
            {
                _multImageCache[num]?.Dispose();
                num++;
                num2 = 8;
            }
            while (num <= num2);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("BackgroundElementVersion", 1);
            info.AddValue("GumpID", _gumpId);
        }

        public override void RefreshCache()
        {
            if (_multImageCache == null)
            {
                _multImageCache = new Image[9];
            }

            int num = 0;
            do
            {
                _multImageCache[num]?.Dispose();
                _multImageCache[num] = Gumps.GetGump(num + _gumpId);
                num++;
            }
            while (num <= 8);
        }

        public override void Render(Graphics target)
        {
            if (_multImageCache == null)
            {
                RefreshCache();
            }

            int num = 0;
            do
            {
                if (_multImageCache[num] == null)
                {
                    RefreshCache();
                }
                num++;
            }
            while (num <= 8);

            Region clip = target.Clip;
            Rectangle rect = new Rectangle(X, Y, _multImageCache[0].Width, _multImageCache[0].Height);
            Region region2 = target.Clip = new Region(rect);
            target.DrawImage(_multImageCache[0], Location);
            region2.Dispose();
            rect = new Rectangle(X, Y, Width - _multImageCache[2].Width, Height);
            Region region4 = target.Clip = new Region(rect);
            int width = _multImageCache[1].Width;
            int num3 = Width - _multImageCache[2].Width;
            for (int i = _multImageCache[0].Width; ((width >> 31) ^ i) <= ((width >> 31) ^ num3); i += width)
            {
                target.DrawImage(image: _multImageCache[1], point: new Point(X + i, Y));
            }
            region4.Dispose();
            rect = new Rectangle(X + Width - _multImageCache[0].Width, Y, _multImageCache[0].Width, Height);
            Region region6 = target.Clip = new Region(rect);
            target.DrawImage(image: _multImageCache[2], point: new Point(X + Width - _multImageCache[2].Width, Y));
            region6.Dispose();
            rect = new Rectangle(X, Y, _multImageCache[0].Width, Height - _multImageCache[6].Height);
            Region region8 = target.Clip = new Region(rect);
            int height = _multImageCache[3].Height;
            int num4 = Height - _multImageCache[6].Height;
            for (int j = _multImageCache[0].Height; ((height >> 31) ^ j) <= ((height >> 31) ^ num4); j += height)
            {
                target.DrawImage(image: _multImageCache[3], point: new Point(X, Y + j));
            }
            region8.Dispose();
            rect = new Rectangle(X, Y + Height - _multImageCache[6].Height, _multImageCache[6].Width, _multImageCache[6].Height);
            Region region10 = target.Clip = new Region(rect);
            target.DrawImage(image: _multImageCache[6], point: new Point(X, Y + Height - _multImageCache[6].Height));
            region10.Dispose();
            rect = new Rectangle(X, Y + Height - _multImageCache[7].Height, Width - _multImageCache[6].Width, _multImageCache[7].Height);
            Region region12 = target.Clip = new Region(rect);
            int width2 = _multImageCache[7].Width;
            int num5 = Width - _multImageCache[8].Width;
            for (int k = _multImageCache[6].Width; ((width2 >> 31) ^ k) <= ((width2 >> 31) ^ num5); k += width2)
            {
                target.DrawImage(image: _multImageCache[7], point: new Point(X + k, Y + Height - _multImageCache[7].Height));
            }
            region12.Dispose();
            rect = new Rectangle(X + Width - _multImageCache[8].Width, Y + Height - _multImageCache[8].Height, _multImageCache[8].Width, _multImageCache[8].Height);
            Region region14 = target.Clip = new Region(rect);
            target.DrawImage(image: _multImageCache[8], point: new Point(X + Width - _multImageCache[8].Width, Y + Height - _multImageCache[8].Height));
            region14.Dispose();
            rect = new Rectangle(X + Width - _multImageCache[5].Width, Y + _multImageCache[2].Height, _multImageCache[5].Width, Height - _multImageCache[8].Height - _multImageCache[2].Height);
            Region region16 = target.Clip = new Region(rect);
            int height2 = _multImageCache[5].Height;
            int num6 = Height - _multImageCache[6].Height;
            for (int l = _multImageCache[0].Height; ((height2 >> 31) ^ l) <= ((height2 >> 31) ^ num6); l += height2)
            {
                target.DrawImage(image: _multImageCache[5], point: new Point(X + Width - _multImageCache[5].Width, Y + l));
            }
            region16.Dispose();
            rect = new Rectangle(X + _multImageCache[3].Width, Y + _multImageCache[1].Height, Width - _multImageCache[3].Width - _multImageCache[5].Width, Height - _multImageCache[7].Height - _multImageCache[1].Height);
            Region region18 = target.Clip = new Region(rect);
            int width3 = _multImageCache[4].Width;
            int num7 = Width - _multImageCache[3].Width;
            for (int m = _multImageCache[3].Width; ((width3 >> 31) ^ m) <= ((width3 >> 31) ^ num7); m += width3)
            {
                int height3 = _multImageCache[4].Height;
                int num8 = Height - _multImageCache[7].Height;
                for (int n = _multImageCache[1].Height; ((height3 >> 31) ^ n) <= ((height3 >> 31) ^ num8); n += height3)
                {
                    target.DrawImage(_multImageCache[4], new Point(X + m, Y + n));
                }
            }
            region18.Dispose();
            target.Clip = clip;
        }
    }
}
