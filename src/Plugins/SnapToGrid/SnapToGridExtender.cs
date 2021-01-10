using System;
using System.Drawing;
using System.Windows.Forms;
using GumpStudio.Elements;
using GumpStudio.Forms;
using GumpStudio.Plugins;

namespace SnapToGrid
{
    public class SnapToGridExtender : ElementExtender
    {
        public GridConfiguration Config;
        private readonly DesignerForm _designer;

        public SnapToGridExtender(DesignerForm designer)
        {
            _designer = designer;
        }

        protected virtual void DoSnapToGridMenu(object sender, EventArgs e)
        {
            foreach (BaseElement element in _designer.ElementStack.GetSelectedElements())
            {
                element.Location = SnapToGrid(element.Location);
            }

            _designer.CreateUndoPoint();
        }

        public Point SnapToGrid(Point position)
        {
            Point point = position;
            point.X = SnapXToGrid(position.X);
            point.Y = SnapXToGrid(position.Y);
            return point;
        }

        public int SnapXToGrid(int x)
        {
            return x / Config.GridSize.Width * Config.GridSize.Width;
        }

        public int SnapYToGrid(int y)
        {
            return y / Config.GridSize.Height * Config.GridSize.Height;
        }

        public override void AddContextMenus(
          ref MenuItem groupMenu,
          ref MenuItem positionMenu,
          ref MenuItem orderMenu,
          ref MenuItem miscMenu)
        {
            if (positionMenu.MenuItems.Count > 1)
            {
                positionMenu.MenuItems.Add(new MenuItem("-"));
            }

            positionMenu.MenuItems.Add(new MenuItem("Snap to Grid", DoSnapToGridMenu));
        }
    }
}
