using GumpStudio.Elements;

namespace GumpStudio.Classes
{
    public class TreeGumpling : TreeItem
    {
        public GroupElement Gumpling;

        public TreeGumpling(string text, GroupElement gumpling)
        {
            Text = text;
            Gumpling = gumpling;
        }
    }
}
