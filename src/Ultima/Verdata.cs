using System.IO;

namespace Ultima
{
    public static class Verdata
    {
        public static Stream Stream { get; }

        public static Entry5D[] Patches { get; }

        static Verdata()
        {
            var path = Client.GetFilePath("verdata.mul");

            if (path == null)
            {
                Patches = new Entry5D[0];
                Stream = Stream.Null;
            }
            else
            {
                Stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var bin = new BinaryReader(Stream);

                Patches = new Entry5D[bin.ReadInt32()];

                for (var i = 0; i < Patches.Length; ++i)
                {
                    Patches[i].file = bin.ReadInt32();
                    Patches[i].index = bin.ReadInt32();
                    Patches[i].lookup = bin.ReadInt32();
                    Patches[i].length = bin.ReadInt32();
                    Patches[i].extra = bin.ReadInt32();
                }
            }
        }
    }

    public struct Entry5D
    {
        public int file;
        public int index;
        public int lookup;
        public int length;
        public int extra;
    }
}