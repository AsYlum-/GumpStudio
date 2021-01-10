using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using GumpStudio.Editors;
using GumpStudio.Enums;
using Ultima;

namespace GumpStudio.Elements
{
    [Serializable]
    public class HtmlElement : ResizeableElement
    {
        protected Bitmap ImgBack;

        protected Bitmap ImgDown;

        protected Bitmap ImgLoc;

        protected Bitmap ImgUp;

        protected BackgroundElement BackgroundElement;

        //protected Bitmap Cache;

        protected Font Font;

        [Description("The ID of the localized message to display as the text of the Html Element. Only Valid if TextType is set to Localized.")]
        [Editor(typeof(ClilocPropEditor), typeof(UITypeEditor))]
        public int CliLocId { get; set; }

        [Editor(typeof(LargeTextPropEditor), typeof(UITypeEditor))]
        [Description("The Html to display in the Element.  Only valid if TextType is set to Html.")]
        public string Html { get; set; }

        [Description("Display a background behind the text of the element.")]
        public bool ShowBackground { get; set; }

        [Description("Display scrollbars along the right side of the element.")]
        public bool ShowScrollbar { get; set; }

        [Description("Switches between custom Html, and Localized messages")]
        public HtmlElementType TextType { get; set; }

        public override string Type => "Html";

        public HtmlElement()
        {
            Html = "";
            CliLocId = 0;
            ShowScrollbar = true;
            ShowBackground = true;
            TextType = HtmlElementType.Html;
            Html = "";
            mSize = new Size(200, 100);
            Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point);
            RefreshCache();
        }

        public HtmlElement(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            info.GetInt32("HtmlElementVersion");
            Html = info.GetString("Html");
            CliLocId = info.GetInt32("ClilocId");
            ShowScrollbar = info.GetBoolean("Scrollbar");
            ShowBackground = info.GetBoolean("Background");
            TextType = (HtmlElementType)info.GetInt32("TextType");

            Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point);

            RefreshCache();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("HtmlElementVersion", 1);
            info.AddValue("Html", Html);
            info.AddValue("ClilocId", CliLocId);
            info.AddValue("Scrollbar", ShowScrollbar);
            info.AddValue("Background", ShowBackground);
            info.AddValue("TextType", (int)TextType);
        }

        public override void RefreshCache()
        {
            ImgUp = Gumps.GetGump(250);
            ImgDown = Gumps.GetGump(252);
            ImgLoc = Gumps.GetGump(254);
            ImgBack = Gumps.GetGump(256);
            BackgroundElement = new BackgroundElement
            {
                GumpId = 3000
            };
        }

        public override void Render(Graphics target)
        {
            using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(70, Color.White)))
            {
                if (!ShowBackground)
                {
                    target.FillRectangle(solidBrush, Bounds);
                    target.DrawRectangle(Pens.DarkGray, Bounds);
                }

                if (ShowScrollbar)
                {
                    target.DrawImage(ImgUp, X + Width - ImgUp.Width, Y);
                    target.DrawImage(ImgLoc, X + Width - ImgLoc.Width, Y + ImgUp.Height);
                    Region clip = target.Clip;
                    target.Clip = new Region(new Rectangle(X + Width - ImgBack.Width, Y + ImgUp.Height + ImgLoc.Height, ImgBack.Width, Height - ImgDown.Height - ImgUp.Height - ImgLoc.Height));
                    int height = ImgBack.Height;
                    int num = Y + Height - ImgDown.Height;
                    for (int i = Y + ImgUp.Height + ImgLoc.Height; ((height >> 31) ^ i) <= ((height >> 31) ^ num); i += height)
                    {
                        target.DrawImage(ImgBack, X + Width - ImgBack.Width, i);
                    }
                    target.Clip = clip;
                    target.DrawImage(ImgDown, X + Width - ImgDown.Width, Y + Height - ImgDown.Height);
                }

                Rectangle rectangle;
                if (ShowBackground)
                {
                    BackgroundElement.Location = Location;
                    rectangle = ((!ShowScrollbar) ? new Rectangle(Location, Size) : new Rectangle(Location, new Size(Width - ImgBack.Width, Height)));
                    BackgroundElement.Size = rectangle.Size;
                    BackgroundElement.Render(target);
                }
                else
                {
                    rectangle = Bounds;
                }

                target.DrawString(Html, Font, Brushes.Black, new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
            }
        }
    }
}
