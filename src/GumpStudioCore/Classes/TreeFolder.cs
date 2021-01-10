using System.Collections.Generic;

namespace GumpStudio.Classes
{
    public class TreeFolder : TreeItem
    {
        protected List<TreeItem> Children = new List<TreeItem>();

        public TreeFolder(string text)
        {
            Text = text;
        }

        public void AddItem(TreeItem item)
        {
            Children.Add(item);
            item.Parent = this;
        }

        public List<TreeItem> GetChildren()
        {
            return Children;
        }

        public void RemoveItem(TreeItem item)
        {
            Children.Remove(item);
            item.Parent = null;
        }
    }
}
