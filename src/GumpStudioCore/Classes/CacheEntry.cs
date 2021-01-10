using System;
using System.Drawing;

namespace GumpStudio.Classes
{
    [Serializable]
    public class CacheEntry
    {
        public int Id;

        [NonSerialized]
        public Image ImageCache;

        public string Name;

        public Size Size;

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
