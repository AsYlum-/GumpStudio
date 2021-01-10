namespace GumpStudio.Classes
{
    public abstract class TreeItem
    {
        public TreeItem Parent;

        public string Text;

        public override string ToString()
        {
            return Text;
        }
    }
}
