using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using GumpStudio.Converters;
using GumpStudio.Editors;
using Ultima;

namespace GumpStudio.Elements
{
    [Serializable]
    public class ImageElement : BaseElement
    {
        protected Bitmap ImageCache;

        protected int MGumpId;

        protected Hue MHue;

        [Editor(typeof(GumpIdPropEditor), typeof(UITypeEditor))]
        public int GumpId
        {
            get => MGumpId;
            set
            {
                MGumpId = value;
                RefreshCache();
            }
        }

        [Editor(typeof(HuePropEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(HuePropStringConverter))]
        [Browsable(true)]
        public Hue Hue
        {
            get => MHue;
            set
            {
                MHue = value;
                RefreshCache();
            }
        }

        public override string Type => "Image";

        public ImageElement()
            : this(1)
        {
        }

        public ImageElement(int gumpId)
        {
            MHue = Hues.GetHue(0);
            GumpId = gumpId;
        }

        public ImageElement(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            MHue = Hues.GetHue(0);
            int imageElementVersion = info.GetInt32("ImageElementVersion");
            MGumpId = info.GetInt32("GumpID");
            MHue = Hues.GetHue(imageElementVersion >= 2 ? info.GetInt32("HueIndex") : 0);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ImageElementVersion", 2);
            info.AddValue("GumpID", MGumpId);
            info.AddValue("HueIndex", MHue.Index);
        }

        public override void RefreshCache()
        {
            ImageCache?.Dispose();
            ImageCache = Gumps.GetGump(MGumpId);
            if (ImageCache == null)
            {
                GumpId = 0;
            }

            if (MHue.Index != 0)
            {
                MHue.ApplyTo(ImageCache, onlyHueGrayPixels: false);
            }
            mSize = ImageCache.Size;
        }

        public override void Render(Graphics target)
        {
            if (ImageCache == null)
            {
                RefreshCache();
            }

            if (ImageCache != null)
            {
                target.DrawImage(ImageCache, Location);
                return;
            }
            target.DrawLine(Pens.Red, X, Y, X + 30, Y + 30);
            target.DrawLine(Pens.Red, X + 30, Y, X, Y + 30);
        }
    }
}
