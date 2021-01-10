using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using System.Windows.Forms;
using GumpStudio.Converters;
using GumpStudio.Editors;
using Ultima;
using UOFont;

namespace GumpStudio.Elements
{
    [Serializable]
    public class LabelElement : BaseElement
    {
        protected Bitmap mCache;

        protected bool mCropped;

        protected int mFontIndex;

        protected Hue mHue;

        protected string mText;

        protected bool mUnicode;

        protected bool mPartialHue;

        [MergableProperty(true)]
        public bool Cropped
        {
            get => mCropped;
            set
            {
                mCropped = value;
                RefreshCache();
            }
        }

        [MergableProperty(true)]
        public bool Unicode
        {
            get => mUnicode;
            set
            {
                mUnicode = value;
                if (!value && mFontIndex > 12)
                {
                    mFontIndex = 12;
                }

                RefreshCache();
            }
        }

        [Browsable(true)]
        [MergableProperty(true)]
        [Editor(typeof(FontPropEditor), typeof(UITypeEditor))]
        public int Font
        {
            get => mFontIndex;
            set
            {
                if (value >= 0 && value < (mUnicode ? 13 : 10))
                {
                    mFontIndex = value;
                    RefreshCache();
                }
                else
                {
                    MessageBox.Show("Font must be between 0 and 10 for ANSI and up to 12 for Unicode.");
                }
            }
        }

        [MergableProperty(true)]
        public bool PartialHue
        {
            get => mPartialHue;
            set
            {
                mPartialHue = value;
                RefreshCache();
            }
        }

        [MergableProperty(true)]
        [Browsable(true)]
        [Editor(typeof(HuePropEditor), typeof(UITypeEditor))]
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

        [Browsable(true)]
        [MergableProperty(true)]
        public override Size Size
        {
            get => base.Size;
            set
            {
                if (!mCropped)
                {
                    throw new ArgumentException("Size may only be changed if the label is cropped.");
                }

                mSize = value;
            }
        }

        [MergableProperty(true)]
        public string Text
        {
            get => mText;
            set
            {
                mText = value;
                RefreshCache();
            }
        }

        public override string Type => "Label";

        public LabelElement()
        {
            mFontIndex = 2;
            mCropped = false;
            mPartialHue = true;
            mUnicode = true;
            mHue = Hues.GetHue(0);
            mText = "New Label";
            try
            {
                RefreshCache();
            }
            catch
            {
                // ignored
            }
        }

        public LabelElement(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            mFontIndex = 2;
            int @int = info.GetInt32("LabelElementVersion");
            mText = info.GetString("Text");
            mHue = Hues.GetHue(info.GetInt32("HueIndex"));
            if (@int >= 3)
            {
                mPartialHue = info.GetBoolean("PartialHue");
                mUnicode = info.GetBoolean("Unicode");
            }
            else
            {
                mPartialHue = true;
                mUnicode = true;
            }

            mFontIndex = info.GetInt32("FontIndex");
            if (@int <= 2)
            {
                mFontIndex--;
            }

            if (@int >= 2)
            {
                mCropped = info.GetBoolean("Cropped");
                mSize = (Size) info.GetValue("Size", typeof(Size));
            }
            else
            {
                mCropped = false;
                using (Bitmap stringImage = UnicodeFonts.GetStringImage(mFontIndex, mText + " "))
                {
                    mSize = stringImage.Size;
                }
            }

            RefreshCache();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("LabelElementVersion", 3);
            info.AddValue("Text", mText);
            info.AddValue("HueIndex", mHue.Index);
            info.AddValue("PartialHue", mPartialHue);
            info.AddValue("Unicode", mUnicode);
            info.AddValue("FontIndex", mFontIndex);
            info.AddValue("Cropped", mCropped);
        }

        public override void RefreshCache()
        {
            mCache?.Dispose();
            mCache = mUnicode
                ? UnicodeFonts.GetStringImage(mFontIndex, mText + " ")
                : Fonts.GetStringImage(mFontIndex, mText + " ");
            if (mHue != null && mHue.Index != 0)
            {
                mHue.ApplyTo(mCache, mPartialHue);
            }

            if (mCropped)
            {
                Bitmap image = new Bitmap(mSize.Width, mSize.Height, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.Clear(Color.Transparent);
                    graphics.DrawImage(mCache, 0, 0);
                }

                mCache.Dispose();
                mCache = image;
            }

            mSize = mCache.Size;
        }

        public override void Render(Graphics target)
        {
            if (mCache == null)
            {
                RefreshCache();
            }

            target.DrawImage(mCache, Location);
        }
    }
}