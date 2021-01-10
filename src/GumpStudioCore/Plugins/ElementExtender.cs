using System.Windows.Forms;

namespace GumpStudio.Plugins
{
    public abstract class ElementExtender
    {
        public virtual void AddContextMenus(ref MenuItem groupMenu,
            ref MenuItem positionMenu,
            ref MenuItem orderMenu,
            ref MenuItem miscMenu)
        {
        }
    }
}
