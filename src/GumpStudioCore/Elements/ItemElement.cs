using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Windows.Forms;
using GumpStudio.Converters;
using GumpStudio.Editors;
using Ultima;

namespace GumpStudio.Elements
{
    [Serializable]
    public class ItemElement : BaseElement
    {
        protected Image ImageCache;

        protected Hue MHue;

        protected int ItemId;

        [TypeConverter(typeof(HuePropStringConverter))]
        [Browsable(true)]
        [Editor(typeof(HuePropEditor), typeof(UITypeEditor))]
        public Hue Hue
        {
            get => MHue;
            set => MHue = value;
        }

        [Editor(typeof(ItemIdPropEditor), typeof(UITypeEditor))]
        public int ItemID
        {
            get => ItemId;
            set
            {
                ImageCache = Art.GetStatic(value);
                if (ImageCache == null)
                {
                    ImageCache = Art.GetStatic(ItemId);
                    MessageBox.Show("Invalid ItemId");
                }
                else
                {
                    ItemId = value;
                    mSize = ImageCache.Size;
                }
            }
        }

        public override string Type => "Item";

        public ItemElement()
        {
            mSize = new Size(50, 50);
            ItemID = 0;
            MHue = Hues.GetHue(0);
        }

        public ItemElement(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            info.GetInt32("ItemElementVersion");
            ItemId = info.GetInt32("ItemID");
            MHue = Hues.GetHue(info.GetInt32("HueIndex"));
            RefreshCache();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ItemElementVersion", 1);
            info.AddValue("ItemID", ItemId);
            info.AddValue("HueIndex", MHue.Index);
        }

        public override void RefreshCache()
        {
            if (ImageCache == null)
            {
                ImageCache = Art.GetStatic(ItemId);
            }
        }

        public override void Render(Graphics target)
        {
            try
            {
                if (MHue.Index != 0)
                {
                    if (ImageCache != null)
                    {
                        using (var bitmap = new Bitmap(ImageCache))
                        {
                            MHue.ApplyTo(bitmap, onlyHueGrayPixels: false);
                            target.DrawImage(bitmap, Location);
                        }
                    }
                    else
                    {
                        target.DrawLine(Pens.Red, X, Y, X + 30, Y + 30);
                        target.DrawLine(Pens.Red, X + 30, Y, X, Y + 30);
                    }
                }
                else
                {
                    if (ImageCache == null)
                    {
                        RefreshCache();
                    }
                    if (ImageCache != null)
                    {
                        target.DrawImage(ImageCache, Location);
                    }
                    else
                    {
                        target.DrawLine(Pens.Red, X, Y, X + 30, Y + 30);
                        target.DrawLine(Pens.Red, X + 30, Y, X, Y + 30);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Error drawing itemID: {ItemId} it has been replaced with the \"no draw\" item.");
                ItemId = 1;
            }
        }
    }
}
