using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows.Forms;
using GumpStudio.Classes;
using GumpStudio.Enums;
using GumpStudio.Plugins;

namespace GumpStudio.Elements
{
    [Serializable]
    public abstract class BaseElement : ISerializable, ICloneable
    {
        [Description("A sorter that will sort elemenets in the ascending order of thier Horizontal center point.")]
        private class ElementHorizontalSorter : IComparer<BaseElement>
        {
            public int Compare(BaseElement x, BaseElement y)
            {
                // TODO: add null checking
                int xCenter = (x.X + x.Width) / 2;
                int yCenter = (y.X + y.Width) / 2;
                if (xCenter > yCenter)
                {
                    return 1;
                }

                if (xCenter >= yCenter)
                {
                    return 0;
                }

                return -1;
            }
        }

        [Description("A sorter that will sort elemenets in the ascending order of thier Vertical center point.")]
        private class ElementVerticalSorter : IComparer<BaseElement>
        {
            public int Compare(BaseElement x, BaseElement y)
            {
                // TODO: add null checking
                int xCenter = (x.Y + x.Height) / 2;
                int yCenter = (y.Y + y.Height) / 2;
                if (xCenter > yCenter)
                {
                    return 1;
                }

                if (xCenter >= yCenter)
                {
                    return 0;
                }

                return -1;
            }
        }

        public delegate void RepaintEventHandler(object sender);

        public delegate void UpdateParentEventHandler(BaseElement element, bool clearSelected);

        protected static readonly Dictionary<string, long> TypeIds = new Dictionary<string, long>();

        protected static readonly List<ElementExtender> Extenders = new List<ElementExtender>();

        protected Point mLocation;

        protected GroupElement mParent;

        protected Size mSize;

        [Browsable(false)] public virtual Rectangle Bounds => new Rectangle(mLocation, mSize);

        [Description("A user defined comment for this element. UO does not use this.")]
        [MergableProperty(false)]
        [ParenthesizePropertyName(true)]
        public string Comment { get; set; }

        [Browsable(false)]
        public virtual int Height
        {
            get => mSize.Height;
            set => mSize.Height = value;
        }

        [MergableProperty(false)]
        public Point Location
        {
            get => mLocation;
            set => mLocation = value;
        }

        [MergableProperty(false)]
        [ParenthesizePropertyName(true)]
        [Description("A name used to identify the element in the Editor, or in script.  UO does not use this.")]
        public string Name { get; set; }

        [Browsable(false)]
        [Description("The group elements that this element belongs to.")]
        public GroupElement Parent => mParent;

        [Browsable(false)]
        public bool Selected { get; set; }

        [Browsable(false)]
        [MergableProperty(true)]
        public virtual Size Size
        {
            get => mSize;
            set => mSize = value;
        }

        [MergableProperty(false)] public abstract string Type { get; }

        [Browsable(false)]
        public virtual int Width
        {
            get => mSize.Width;
            set => mSize.Width = value;
        }

        [Browsable(false)]
        public int X
        {
            get => mLocation.X;
            set => mLocation.X = value;
        }

        [Browsable(false)]
        public int Y
        {
            get => mLocation.Y;
            set => mLocation.Y = value;
        }

        [Description("The Z order of this element. Highest is on top.")]
        [MergableProperty(false)]
        public int? Z
        {
            get => mParent?.GroupElements.IndexOf(this);
            set
            {
                // TODO: this is kinda weird...
                if (value != null)
                {
                    mParent.GroupElements.Remove(this);
                    mParent.GroupElements.Insert((int)value, this);
                }
            }
        }

        public event RepaintEventHandler Repaint;

        public event UpdateParentEventHandler UpdateParent;

        protected BaseElement()
        {
            long value;
            if (TypeIds.ContainsKey(Type))
            {
                long num = TypeIds[Type] + 1;
                TypeIds[Type] = num;
                value = num;
            }
            else
            {
                TypeIds.Add(Type, 1);
                value = 1L;
            }

            Name = $"{Type} {value}";
        }

        protected BaseElement(string name)
        {
            Name = name;
        }

        protected BaseElement(SerializationInfo info, StreamingContext context)
        {
            int baseElementVersion = info.GetInt32("BaseElementVersion");
            Name = info.GetString("Name");
            mLocation = (Point) info.GetValue("Location", typeof(Point));
            mSize = (Size) info.GetValue("Size", typeof(Size));
            mParent = (GroupElement) info.GetValue("Parent", typeof(GroupElement));
            Comment = baseElementVersion >= 2 ? info.GetString("Comment") : string.Empty;
        }

        public virtual void AddContextMenus(ref MenuItem groupMenu, ref MenuItem positionMenu, ref MenuItem orderMenu, ref MenuItem miscMenu)
        {
            int count = mParent.GetElements().Count - 1;
            if (count > 0)
            {
                if (Z < count)
                {
                    orderMenu.MenuItems.Add(new MenuItem("Move Front", DoMoveFrontMenu));
                    orderMenu.MenuItems.Add(new MenuItem("Move First", DoMoveFirstMenu));
                }

                if (Z >= 1)
                {
                    orderMenu.MenuItems.Add(new MenuItem("Move Back", DoMoveBackMenu));
                    orderMenu.MenuItems.Add(new MenuItem("Move Last", DoMoveLastMenu));
                }
            }

            if (mParent.GetSelectedElements().Count > 1)
            {
                positionMenu.MenuItems.Add(new MenuItem("Align Lefts", DoAlignLeftsMenu));
                positionMenu.MenuItems.Add(new MenuItem("Align Rights", DoAlignRightsMenu));
                positionMenu.MenuItems.Add(new MenuItem("Align Tops", DoAlignTopsMenu));
                positionMenu.MenuItems.Add(new MenuItem("Align Bottoms", DoAlignBottomsMenu));
                positionMenu.MenuItems.Add(new MenuItem("-"));
                positionMenu.MenuItems.Add(new MenuItem("Center Horizontally", DoAlignCentersMenu));
                positionMenu.MenuItems.Add(new MenuItem("Center Vertically", DoAlignMiddlesMenu));
                if (mParent.GetSelectedElements().Count > 2)
                {
                    positionMenu.MenuItems.Add(new MenuItem("-"));
                    positionMenu.MenuItems.Add(new MenuItem("Equalize Vertical Spacing", DoVerticalSpacingMenu));
                    positionMenu.MenuItems.Add(new MenuItem("Equalize Horizontal Spacing", DoHorizontalSpacingMenu));
                }
            }

            foreach (var extender in Extenders)
            {
                extender.AddContextMenus(ref groupMenu, ref positionMenu, ref orderMenu, ref miscMenu);
            }
        }

        [Description("Inserts an element extender into this element type's extender list.")]
        public void AddExtender(ElementExtender extender)
        {
            if (!Extenders.Contains(extender))
            {
                Extenders.Add(extender);
            }
        }

        [Description("Creates a copy of this element.")]
        public BaseElement Clone()
        {
            return (BaseElement) CloneMe();
        }

        [Description("Creates a copy of this element.")]
        protected virtual object CloneMe()
        {
            var baseElement = (BaseElement) MemberwiseClone();
            baseElement.RefreshCache();
            RefreshCache();
            return baseElement;
        }

        public virtual bool ContainsTest(Rectangle rect)
        {
            return rect.IntersectsWith(Bounds);
        }

        public virtual void DebugDump()
        {
        }

        public virtual bool DisplayInAbout()
        {
            return false;
        }

        protected virtual void DoAlignBottomsMenu(object sender, EventArgs e)
        {
            foreach (var element in mParent.GetSelectedElements())
            {
                element.Y = Y + Height - element.Height;
            }

            GlobalObjects.DesignerForm.CreateUndoPoint();
        }

        protected virtual void DoAlignCentersMenu(object sender, EventArgs e)
        {
            foreach (var element in mParent.GetSelectedElements())
            {
                if (element != this)
                {
                    element.X = (int) Math.Round(X + Width / 2.0 - element.Width / 2.0);
                }
            }

            GlobalObjects.DesignerForm.CreateUndoPoint();
        }

        protected virtual void DoAlignLeftsMenu(object sender, EventArgs e)
        {
            foreach (var element in mParent.GetSelectedElements())
            {
                element.X = X;
            }

            GlobalObjects.DesignerForm.CreateUndoPoint();
        }

        protected virtual void DoAlignMiddlesMenu(object sender, EventArgs e)
        {
            foreach (var element in mParent.GetSelectedElements())
            {
                if (element != this)
                {
                    element.Y = (int) Math.Round(Y + Height / 2.0 - element.Height / 2.0);
                }
            }

            GlobalObjects.DesignerForm.CreateUndoPoint();
        }

        protected virtual void DoAlignRightsMenu(object sender, EventArgs e)
        {
            foreach (var element in mParent.GetSelectedElements())
            {
                element.X = X + Width - element.Width;
            }

            GlobalObjects.DesignerForm.CreateUndoPoint();
        }

        protected virtual void DoAlignTopsMenu(object sender, EventArgs e)
        {
            foreach (var element in mParent.GetSelectedElements())
            {
                element.Y = Y;
            }

            GlobalObjects.DesignerForm.CreateUndoPoint();
        }

        protected virtual void DoDebugDumpMenu(object sender, EventArgs e)
        {
            DebugDump();
        }

        protected virtual void DoHorizontalSpacingMenu(object sender, EventArgs e)
        {
            int num = 0;
            var elements = new List<BaseElement>();
            int num2 = int.MaxValue;
            foreach (var selectedElement in mParent.GetSelectedElements())
            {
                int num3 = selectedElement.Width / 2 + selectedElement.X;
                if (num3 < num2)
                {
                    num2 = num3;
                }

                if (num3 > num)
                {
                    num = num3;
                }

                elements.Add(selectedElement);
            }

            elements.Sort(new ElementHorizontalSorter());

            double num4 = (num - num2) / (double) (elements.Count - 1);
            double num5 = num2;

            foreach (var baseElement in elements)
            {
                baseElement.X = (int) Math.Round(num5 - baseElement.Width / 2.0);
                num5 += num4;
            }

            RaiseRepaintEvent(this);
            GlobalObjects.DesignerForm.CreateUndoPoint();
        }

        protected virtual void DoMoveBackMenu(object sender, EventArgs e)
        {
            MoveBack();
        }

        protected virtual void DoMoveFirstMenu(object sender, EventArgs e)
        {
            MoveFirst();
        }

        protected virtual void DoMoveFrontMenu(object sender, EventArgs e)
        {
            MoveFront();
        }

        protected virtual void DoMoveLastMenu(object sender, EventArgs e)
        {
            MoveLast();
        }

        protected virtual void DoVerticalSpacingMenu(object sender, EventArgs e)
        {
            int num = 0;

            var elements = new List<BaseElement>();
            int num2 = int.MaxValue;
            foreach (var selectedElement in mParent.GetSelectedElements())
            {
                int num3 = selectedElement.Height / 2 + selectedElement.Y;
                if (num3 < num2)
                {
                    num2 = num3;
                }

                if (num3 > num)
                {
                    num = num3;
                }

                elements.Add(selectedElement);
            }

            elements.Sort(new ElementVerticalSorter());

            double num4 = (num - num2) / (double) (elements.Count - 1);
            double num5 = num2;
            foreach (var baseElement in elements)
            {
                baseElement.Y = (int) Math.Round(num5 - baseElement.Height / 2.0);
                num5 += num4;
            }

            RaiseRepaintEvent(this);
            GlobalObjects.DesignerForm.CreateUndoPoint();
        }

        public virtual void DrawBoundingBox(Graphics target, bool active)
        {
            var bounds = Bounds;
            bounds.Inflate(3, 3);
            var pen = (!active) ? new Pen(Color.DarkGray) : new Pen(Color.White);
            target.DrawRectangle(Pens.Gray, bounds);
            bounds.Inflate(1, 1);
            target.DrawRectangle(pen, bounds);
            pen.Dispose();
        }

        public virtual string GetAboutText()
        {
            return "You should override GetAboutText() to change this.";
        }

        [Description(
            "Returns the Element's location, taking into account the offset of all parent elements.  Export plugins should use this to get the position for scripts.")]
        public virtual Point GetAbsolutePosition()
        {
            if (Parent == null)
            {
                return Location;
            }

            var absolutePosition = Parent.GetAbsolutePosition();
            absolutePosition.Offset(X, Y);
            return absolutePosition;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("BaseElementVersion", 2);
            info.AddValue("Name", Name);
            info.AddValue("Location", mLocation);
            info.AddValue("Size", mSize);
            info.AddValue("Parent", mParent);
            info.AddValue("Comment", Comment);
        }

        public virtual MoveModeType HitTest(Point location)
        {
            var rectangle = Rectangle.Inflate(Bounds, 3, 3);
            var result = MoveModeType.None;
            if (rectangle.Contains(location))
            {
                result = MoveModeType.Move;
            }

            return result;
        }

        public void MoveBack()
        {
            if (Z > 0)
            {
                Z--;
            }

            GlobalObjects.DesignerForm.CreateUndoPoint("Move back");
        }

        public void MoveFirst()
        {
            mParent.GroupElements.Remove(this);
            mParent.GroupElements.Add(this);
            GlobalObjects.DesignerForm.CreateUndoPoint("Move first");
        }

        public void MoveFront()
        {
            if (Z < Parent.GroupElements.Count - 1)
            {
                Z++;
            }

            GlobalObjects.DesignerForm.CreateUndoPoint("Move front");
        }

        public void MoveLast()
        {
            Z = 0;
            GlobalObjects.DesignerForm.CreateUndoPoint("Move last");
        }

        public void RaiseRepaintEvent(object sender)
        {
            Repaint?.Invoke(RuntimeHelpers.GetObjectValue(sender));
        }

        public void RaiseUpdateEvent(BaseElement element, bool clearSelected)
        {
            UpdateParent?.Invoke(element, clearSelected);
        }

        public abstract void RefreshCache();

        public abstract void Render(Graphics target);

        public void ResetParent(GroupElement parent)
        {
            if (mParent != null)
            {
                UpdateParent -= mParent.RaiseUpdateEvent;
            }

            mParent = parent;
        }

        internal static void ResetId()
        {
            TypeIds.Clear();
        }

        public override string ToString()
        {
            return Name;
        }

        object ICloneable.Clone()
        {
            return null;
        }
    }
}