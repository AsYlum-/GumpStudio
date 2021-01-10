using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using GumpStudio.Converters;
using GumpStudio.Editors;
using Ultima;
using UOFont;

namespace GumpStudio.Elements
{
    [Serializable]
    public class TextEntryElement : ResizeableElement
    {
        protected Bitmap Cache;

        protected Hue mHue;

        protected int mID;

        protected string mInitialText;

        protected int mMaxLength;

        [TypeConverter(typeof(HuePropStringConverter))]
        [Description("The Hue of the text, Only the right-most color of the Hue is used.")]
        [Browsable(true)]
        [Editor(typeof(HuePropEditor), typeof(UITypeEditor))]
        public Hue Hue
        {
            get => mHue;
            set
            {
                mHue = value;
                RefreshCache();
            }
        }

        [Description("The ID of this text entry element returned to script.")]
        [MergableProperty(false)]
        public int Id
        {
            get => mID;
            set => mID = value;
        }

        [Description("The text in the text entry area when the gump is initially opened.")]
        public string InitialText
        {
            get => mInitialText;
            set
            {
                mInitialText = value;
                RefreshCache();
            }
        }

        [Description("MaxLength sets the maximum number of characters allowed in this TextEntry element. Set to 0 for no limit.")]
        [MergableProperty(true)]
        public int MaxLength
        {
            get => mMaxLength;
            set => mMaxLength = value;
        }

        public override string Type => "Text Entry";

        public TextEntryElement()
        {
            mSize = new Size(200, 20);
            mHue = Hues.GetHue(0);
        }

        public TextEntryElement(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            int @int = info.GetInt32("TextEntryElementVersion");
            mInitialText = info.GetString("Text");
            mHue = Hues.GetHue(info.GetInt32("HueIndex"));
            if (@int >= 2)
            {
                mID = info.GetInt32("ID");
                mMaxLength = info.GetInt32("MaxLength");
            }
            RefreshCache();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("TextEntryElementVersion", 2);
            info.AddValue("Text", mInitialText);
            info.AddValue("HueIndex", mHue.Index);
            info.AddValue("ID", mID);
            info.AddValue("MaxLength", mMaxLength);
        }

        public override void RefreshCache()
        {
            if (mHue == null)
            {
                mHue = Hues.GetHue(0);
            }

            Cache?.Dispose();
            Cache = UnicodeFonts.GetStringImage(2, mInitialText + " ");
            if (mHue != null && mHue.Index != 0)
            {
                mHue.ApplyTo(Cache, onlyHueGrayPixels: false);
            }
        }

        public override void Render(Graphics target)
        {
            if (Cache == null)
            {
                RefreshCache();
            }
            Region clip = target.Clip;
            Region region2 = target.Clip = new Region(Bounds);
            using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(50, Color.Yellow)))
            {
                target.FillRectangle(solidBrush, Bounds);
                target.DrawImage(Cache, Location);
            }

            target.Clip = clip;
            region2.Dispose();
        }
    }
}
