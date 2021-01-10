using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Ultima
{
    public static class Textures
    {
        public static FileIndex FileIndex { get; } = new FileIndex("Texidx.mul", "Texmaps.mul", 0x1000, 10);

        public static unsafe Bitmap GetTexture(int index)
        {
            var stream = FileIndex.Seek(index, out int length, out int extra, out bool patched);

            if (stream == null)
            {
                return null;
            }

            var size = extra == 0 ? 64 : 128;

            var bmp = new Bitmap(size, size, PixelFormat.Format16bppArgb1555);
            var bd = bmp.LockBits(new Rectangle(0, 0, size, size), ImageLockMode.WriteOnly,
                PixelFormat.Format16bppArgb1555);
            var bin = new BinaryReader(stream);

            var line = (ushort*) bd.Scan0;
            var delta = bd.Stride >> 1;

            for (var y = 0; y < size; ++y, line += delta)
            {
                var cur = line;
                var end = cur + size;

                while (cur < end)
                {
                    *cur++ = (ushort) (bin.ReadUInt16() ^ 0x8000);
                }
            }

            bmp.UnlockBits(bd);

            return bmp;
        }
    }
}