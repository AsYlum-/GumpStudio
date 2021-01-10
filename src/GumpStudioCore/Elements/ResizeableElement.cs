using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using GumpStudio.Enums;

namespace GumpStudio.Elements
{
    [Serializable]
    public abstract class ResizeableElement : BaseElement
    {
        [Browsable(false)]
        public override int Height
        {
            get => mSize.Height;
            set => mSize.Height = value;
        }

        [Browsable(true)]
        public override Size Size
        {
            get => mSize;
            set => mSize = value;
        }

        [Browsable(false)]
        public override int Width
        {
            get => mSize.Width;
            set => mSize.Width = value;
        }

        protected ResizeableElement()
        {
        }

        protected ResizeableElement(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            info.GetInt32("ResizableElementVersion");
        }

        public override void DrawBoundingBox(Graphics target, bool active)
        {
            Rectangle bounds = Bounds;
            bounds.Inflate(3, 3);
            Brush brush;
            Pen pen;
            if (active)
            {
                brush = new SolidBrush(Color.LightGray);
                pen = new Pen(Color.White);
            }
            else
            {
                brush = new SolidBrush(Color.Gray);
                pen = new Pen(Color.DarkGray);
            }
            base.DrawBoundingBox(target, active);

            bounds.Inflate(1, 1);

            target.FillRectangle(brush, bounds.X - 2, bounds.Y - 2, 6, 6);
            target.FillRectangle(brush, bounds.X + bounds.Width - 3, bounds.Y + bounds.Height - 3, 6, 6);
            target.FillRectangle(brush, bounds.X + bounds.Width - 3, bounds.Y - 2, 6, 6);
            target.FillRectangle(brush, bounds.X - 2, bounds.Y + bounds.Height - 3, 6, 6);
            target.FillRectangle(brush, bounds.X - 2, (int)Math.Round(bounds.Y + bounds.Height / 2.0 - 2.0), 6, 6);
            target.FillRectangle(brush, (int)Math.Round(bounds.X + bounds.Width / 2.0 - 2.0), bounds.Y - 2, 6, 6);
            target.FillRectangle(brush, bounds.X + bounds.Width - 3, (int)Math.Round(bounds.Y + bounds.Height / 2.0 - 2.0), 6, 6);
            target.FillRectangle(brush, (int)Math.Round(bounds.X + bounds.Width / 2.0 - 2.0), bounds.Y + bounds.Height - 3, 6, 6);

            target.DrawRectangle(pen, bounds.X - 2, bounds.Y - 2, 6, 6);
            target.DrawRectangle(pen, bounds.X + bounds.Width - 3, bounds.Y + bounds.Height - 3, 6, 6);
            target.DrawRectangle(pen, bounds.X + bounds.Width - 3, bounds.Y - 2, 6, 6);
            target.DrawRectangle(pen, bounds.X - 2, bounds.Y + bounds.Height - 3, 6, 6);
            target.DrawRectangle(pen, bounds.X - 2, (int)Math.Round(bounds.Y + bounds.Height / 2.0 - 2.0), 6, 6);
            target.DrawRectangle(pen, (int)Math.Round(bounds.X + bounds.Width / 2.0 - 2.0), bounds.Y - 2, 6, 6);
            target.DrawRectangle(pen, bounds.X + bounds.Width - 3, (int)Math.Round(bounds.Y + bounds.Height / 2.0 - 2.0), 6, 6);
            target.DrawRectangle(pen, (int)Math.Round(bounds.X + bounds.Width / 2.0 - 2.0), bounds.Y + bounds.Height - 3, 6, 6);

            brush.Dispose();
            pen.Dispose();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ResizableElementVersion", 1);
        }

        public override MoveModeType HitTest(Point location)
        {
            Rectangle rectangle = Rectangle.Inflate(Bounds, 4, 4);
            MoveModeType result = MoveModeType.None;
            if (rectangle.Contains(location))
            {
                result = MoveModeType.Move;
            }

            if (!Selected)
            {
                return result;
            }

            Rectangle rectangle2 = new Rectangle(rectangle.X - 2, rectangle.Y - 2, 5, 5);
            Rectangle rectangle3 = new Rectangle((int)Math.Round(rectangle.X + rectangle.Width / 2.0 - 2.0), rectangle.Y - 2, 5, 5);
            Rectangle rectangle4 = new Rectangle(rectangle.X + rectangle.Width - 2, rectangle.Y - 2, 5, 5);
            Rectangle rectangle5 = new Rectangle(rectangle.X + rectangle.Width - 2, (int)Math.Round(rectangle.Y + rectangle.Height / 2.0 - 2.0), 5, 5);
            Rectangle rectangle6 = new Rectangle(rectangle.X + rectangle.Width - 2, rectangle.Y + rectangle.Height - 2, 5, 5);
            Rectangle rectangle7 = new Rectangle((int)Math.Round(rectangle.X + rectangle.Width / 2.0 - 2.0), rectangle.Y + rectangle.Height - 2, 5, 5);
            Rectangle rectangle8 = new Rectangle(rectangle.X - 2, rectangle.Y + rectangle.Height - 2, 5, 5);
            Rectangle rectangle9 = new Rectangle(rectangle.X - 2, (int)Math.Round(rectangle.Y + rectangle.Height / 2.0 - 2.0), 5, 5);

            if (rectangle6.Contains(location))
            {
                result = MoveModeType.ResizeBottomRight;
            }

            if (rectangle2.Contains(location))
            {
                result = MoveModeType.ResizeTopLeft;
            }

            if (rectangle4.Contains(location))
            {
                result = MoveModeType.ResizeTopRight;
            }

            if (rectangle8.Contains(location))
            {
                result = MoveModeType.ResizeBottomLeft;
            }

            if (rectangle9.Contains(location))
            {
                result = MoveModeType.ResizeLeft;
            }

            if (rectangle3.Contains(location))
            {
                result = MoveModeType.ResizeTop;
            }

            if (rectangle5.Contains(location))
            {
                result = MoveModeType.ResizeRight;
            }

            if (rectangle7.Contains(location))
            {
                result = MoveModeType.ResizeBottom;
            }

            return result;
        }
    }
}
