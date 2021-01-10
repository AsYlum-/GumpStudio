using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GumpStudio.Elements
{
    [Serializable]
    public class AlphaElement : ResizeableElement
    {
        public override string Type => "Alpha Area";

        public AlphaElement()
        {
            mSize = new Size(100, 50);
        }

        protected AlphaElement(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            info.GetInt32("AlphaElementVersion");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("AlphaElementVersion", 1);
        }

        public override void RefreshCache()
        {
        }

        public override void Render(Graphics target)
        {
            using (var solidBrush = new SolidBrush(Color.FromArgb(50, Color.Red)))
            {
                target.FillRectangle(solidBrush, Bounds);
                target.DrawRectangle(Pens.Red, Bounds);
            }
        }
    }
}
