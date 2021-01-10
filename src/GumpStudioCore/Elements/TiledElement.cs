using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Windows.Forms;
using GumpStudio.Classes;
using GumpStudio.Converters;
using GumpStudio.Editors;
using Ultima;

namespace GumpStudio.Elements
{
    [Serializable]
    public class TiledElement : ResizeableElement
    {
        private const int StartingGumpId = 30089;

        protected bool DoingRenderRetry;

        protected Bitmap ImageCache;

        protected int mGumpID;

        protected Hue mHue;

        protected Size mTileSize;

        [Editor(typeof(GumpIdPropEditor), typeof(UITypeEditor))]
        public virtual int GumpId
        {
            get => mGumpID;
            set
            {
                mGumpID = value;
                RefreshCache();
            }
        }

        [Editor(typeof(HuePropEditor), typeof(UITypeEditor))]
        [Browsable(true)]
        [TypeConverter(typeof(HuePropStringConverter))]
        public Hue Hue
        {
            get => mHue;
            set
            {
                mHue = value;
                RefreshCache();
            }
        }

        [Description("The size of the image being tiled")]
        public Size TileSize => mTileSize;

        public override string Type => "Tiled Image";

        public TiledElement() : this(StartingGumpId)
        {
            GumpId = StartingGumpId;
            RefreshCache();
        }

        public TiledElement(int gumpId)
        {
            DoingRenderRetry = false;
            mHue = Hues.GetHue(0);
            GumpId = gumpId;
            mSize = mTileSize;
        }

        public TiledElement(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DoingRenderRetry = false;
            mHue = Hues.GetHue(0);
            int @int = info.GetInt32("TiledElementVersion");
            GumpId = info.GetInt32("GumpID");
            mHue = @int < 2 ? Hues.GetHue(0) : Hues.GetHue(info.GetInt32("HueIndex"));
            RefreshCache();
        }

        public override void AddContextMenus(ref MenuItem groupMenu, ref MenuItem positionMenu, ref MenuItem orderMenu, ref MenuItem miscMenu)
        {
            base.AddContextMenus(ref groupMenu, ref positionMenu, ref orderMenu, ref miscMenu);
            if (positionMenu.MenuItems.Count > 1)
            {
                positionMenu.MenuItems.Add(new MenuItem("-"));
            }
            positionMenu.MenuItems.Add(new MenuItem("Reset Size", DoResetSizeMenu));
        }

        protected virtual void DoResetSizeMenu(object sender, EventArgs e)
        {
            mSize = mTileSize;
            RaiseUpdateEvent(this, clearSelected: false);
            GlobalObjects.DesignerForm.CreateUndoPoint();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("TiledElementVersion", 2);
            info.AddValue("GumpID", mGumpID);
            info.AddValue("HueIndex", mHue.Index);
        }

        public override void RefreshCache()
        {
            ImageCache?.Dispose();
            ImageCache = Gumps.GetGump(mGumpID);

            if (ImageCache == null)
            {
                GumpId = 0;
            }

            if (mHue.Index != 0)
            {
                mHue.ApplyTo(ImageCache, onlyHueGrayPixels: false);
            }

            mTileSize = ImageCache.Size;
        }

        public override void Render(Graphics target)
        {
            if (ImageCache != null)
            {
                Region clip = target.Clip;
                Region region2 = target.Clip = new Region(Bounds);
                int width = mTileSize.Width;
                int width2 = Width;
                for (int i = 0; ((width >> 31) ^ i) <= ((width >> 31) ^ width2); i += width)
                {
                    int height = mTileSize.Height;
                    int height2 = Height;
                    for (int j = 0; ((height >> 31) ^ j) <= ((height >> 31) ^ height2); j += height)
                    {
                        Point location = Location;
                        location.Offset(i, j);
                        target.DrawImage(ImageCache, location);
                    }
                }
                target.Clip = clip;
                region2.Dispose();
            }
            else if (!DoingRenderRetry)
            {
                DoingRenderRetry = true;
                GumpId = mGumpID;
                Render(target);
            }
            else
            {
                target.DrawLine(Pens.Red, Location.X, Location.Y, Location.X + Size.Width, Location.Y + Size.Height);
                target.DrawLine(Pens.Red, Location.X + Size.Width, Location.Y, Location.X, Location.Y + Size.Height);
            }
        }
    }
}
