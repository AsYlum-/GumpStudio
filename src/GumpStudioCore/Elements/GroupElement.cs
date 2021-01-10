using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;
using GumpStudio.Classes;

namespace GumpStudio.Elements
{
    [Serializable]
    public class GroupElement : BaseElement
    {
        public List<BaseElement> GroupElements;
        public bool IsBaseWindow;

        [XmlArray("GElements")]
        public BaseElement[] Elements
        {
            get
            {
                BaseElement[] baseElementArray = null;

                foreach (BaseElement baseElement in GroupElements)
                {
                    if (baseElementArray == null)
                    {
                        baseElementArray = new BaseElement[1];
                    }
                    else
                    {
                        var newArray = new BaseElement[baseElementArray.Length + 1];
                        Array.Copy(baseElementArray, newArray, baseElementArray.Length);
                        baseElementArray = newArray;
                    }

                    baseElementArray[baseElementArray.Length - 1] = baseElement;
                }

                return baseElementArray;
            }
        }

        public int Items => GroupElements.Count;

        public override string Type => "Group";

        private GroupElement()
        {
        }

        protected GroupElement(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            IsBaseWindow = false;
            info.GetInt32("GroupElementVersion");
            GroupElements = (List<BaseElement>)info.GetValue("Elements", typeof(List<BaseElement>));
            IsBaseWindow = info.GetBoolean("IsBaseWindow");
        }

        public GroupElement(IReadOnlyCollection<BaseElement> elements = null, string name = null, bool isBaseWindow = false)
        {
            IsBaseWindow = false;
            IsBaseWindow = isBaseWindow; // TODO: wtf?

            GroupElements = new List<BaseElement>();
            if (name != null)
            {
                Name = name;
            }

            if (elements != null)
            {
                foreach (BaseElement element in Elements)
                {
                    GroupElements.Add(element);
                }
            }

            Location = new Point(0, 0);
        }

        public override void AddContextMenus(ref MenuItem groupMenu, ref MenuItem positionMenu, ref MenuItem orderMenu, ref MenuItem miscMenu)
        {
            base.AddContextMenus(ref groupMenu, ref positionMenu, ref orderMenu, ref miscMenu);
            if (mParent.GetSelectedElements().Count > 1)
            {
                groupMenu.MenuItems.Add(new MenuItem("Add Selection to Group", DoAddMenu));
            }
            groupMenu.MenuItems.Add(new MenuItem("Break Group", DoBreakGroupMenu));
            miscMenu.MenuItems.Add(new MenuItem("Export Gumpling", DoExportGumplingMenu));
        }

        public virtual void AddElement(BaseElement e)
        {
            if (GroupElements.Contains(e))
            {
                return;
            }

            if (!IsBaseWindow)
            {
                Rectangle rectangle;
                if (GroupElements.Count == 0)
                {
                    rectangle = e.Bounds;
                }
                else
                {
                    rectangle = Rectangle.Union(Bounds, e.Bounds);
                    if (X != rectangle.X || Y != rectangle.Y)
                    {
                        int dx = X - rectangle.X;
                        int dy = Y - rectangle.Y;
                        foreach (BaseElement element in GroupElements)
                        {
                            Point location = element.Location;
                            location.Offset(dx, dy);
                            element.Location = location;
                        }
                    }
                }
                Location = rectangle.Location;
                mSize = rectangle.Size;
                Point location2 = e.Location;
                location2.X -= rectangle.Location.X;
                location2.Y -= rectangle.Location.Y;
                e.Location = location2;
            }
            GroupElements.Add(e);
            e.ResetParent(this);
            AttachEvents(e);
        }

        public void AttachEvents(BaseElement element)
        {
            element.UpdateParent += RaiseUpdateEvent;
            element.Repaint += RaiseRepaintEvent;
        }

        public void BreakGroup()
        {
            foreach (BaseElement element in GroupElements)
            {
                mParent.AddElement(element);
                element.Selected = true;
                //Point point2 = baseElement.Location = new Point(X + baseElement.X, Y + baseElement.Y);
            }

            mParent.RemoveElement(this);
        }

        protected override object CloneMe()
        {
            GroupElement groupElement = (GroupElement)base.CloneMe();
            groupElement.GroupElements = new List<BaseElement>();
            foreach (BaseElement element in GroupElements)
            {
                BaseElement clone = element.Clone();
                groupElement.GroupElements.Add(clone);
                groupElement.AttachEvents(clone);
                clone.ResetParent(groupElement);
            }
            return groupElement;
        }

        public override void DebugDump()
        {
            base.DebugDump();

            foreach (BaseElement baseElement in GroupElements)
            {
                baseElement.DebugDump();
            }
        }

        protected void DoAddMenu(object sender, EventArgs e)
        {
            List<BaseElement> elements = new List<BaseElement>();
            elements.AddRange(mParent.GetElements());
            foreach (var baseElement in elements.Where(baseElement => baseElement != this && baseElement.Selected))
            {
                AddElement(baseElement);
                mParent.RemoveElement(baseElement);
                baseElement.Selected = false;
            }
        }

        protected void DoBreakGroupMenu(object sender, EventArgs e)
        {
            BreakGroup();
            mParent.RaiseUpdateEvent(null, clearSelected: false);
            GlobalObjects.DesignerForm.CreateUndoPoint();
        }

        protected void DoExportGumplingMenu(object sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog { Filter = "Gumpling|*.gumpling", AddExtension = true })
                {
                    if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    GroupElement parent = mParent;

                    mParent.RemoveElement(this);
                    mParent = null;

                    using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        new BinaryFormatter().Serialize(fileStream, this);
                    }

                    mParent = parent;
                    mParent.AddElement(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public BaseElement GetElementFromPoint(Point p)
        {
            BaseElement result = null;
            foreach (BaseElement baseElement in GroupElements)
            {
                Rectangle bounds = baseElement.Bounds;
                bounds.Inflate(7, 7);
                if (bounds.Contains(p))
                {
                    result = baseElement;
                }
            }

            return result;
        }

        public List<BaseElement> GetElements()
        {
            return GroupElements;
        }

        public List<BaseElement> GetElementsRecursive()
        {
            List<BaseElement> elements = new List<BaseElement>();
            foreach (BaseElement baseElement in GroupElements)
            {
                if (baseElement is GroupElement element)
                {
                    elements.AddRange(element.GetElementsRecursive());
                }
                else
                {
                    elements.Add(baseElement);
                }
            }

            return elements;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("GroupElementVersion", 1);
            info.AddValue("Elements", GroupElements);
            info.AddValue("IsBaseWindow", IsBaseWindow);
        }

        public List<BaseElement> GetSelectedElements()
        {
            return GroupElements.Where(baseElement => baseElement.Selected).ToList();
        }

        public void RecalculateBounds()
        {
            int count = GroupElements.Count;
            if (count < 1)
            {
                return;
            }

            Rectangle a = GroupElements[0].Bounds;

            if (count >= 2)
            {
                int num = count - 1;
                for (int i = 1; i <= num; i++)
                {
                    a = Rectangle.Union(a, GroupElements[i].Bounds);
                }
            }
            mSize = a.Size;

            RaiseRepaintEvent(this);
        }

        public override void RefreshCache()
        {
            foreach (BaseElement mElement in GroupElements)
            {
                mElement.RefreshCache();
            }
        }

        public void RemoveElement(BaseElement e)
        {
            GroupElements.Remove(e);
            RemoveEvents(e);
        }

        public void RemoveEvents(BaseElement element)
        {
            element.UpdateParent -= RaiseUpdateEvent;
            element.Repaint -= RaiseRepaintEvent;
        }

        public override void Render(Graphics target)
        {
            target.TranslateTransform(X, Y);

            foreach (BaseElement mElement in GroupElements)
            {
                mElement.Render(target);
            }

            target.TranslateTransform(-X, -Y);
        }
    }
}
