using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Ultima
{
    public static class Hues
    {
        public static Hue[] List { get; }

        static Hues()
        {
            var path = Client.GetFilePath("hues.mul");
            var index = 0;

            List = new Hue[3000];

            if (path != null)
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var bin = new BinaryReader(fs);

                    var blockCount = (int) fs.Length / 708;

                    if (blockCount > 375)
                    {
                        blockCount = 375;
                    }

                    for (var i = 0; i < blockCount; ++i)
                    {
                        bin.ReadInt32();

                        for (var j = 0; j < 8; ++j, ++index)
                        {
                            List[index] = new Hue(index, bin);
                        }
                    }
                }
            }

            for (; index < 3000; ++index)
            {
                List[index] = new Hue(index);
            }
        }

        public static Hue GetHue(int index)
        {
            index &= 0x3FFF;

            if (index >= 0 && index < 3000)
            {
                return List[index];
            }

            return List[0];
        }
    }

    public class Hue
    {
        public int Index { get; }

        public short[] Colors { get; }

        public string Name { get; }

        private Hue()
        {
        }

        public Hue(int index)
        {
            Name = "Null";
            Index = index;
            Colors = new short[34];
        }

        public override string ToString()
        {
            return Index.ToString();
        }

        public Color GetColor(int index)
        {
            int c16 = Colors[index];

            return Color.FromArgb((c16 & 0x7C00) >> 7, (c16 & 0x3E0) >> 2, (c16 & 0x1F) << 3);
        }

        public Hue(int index, BinaryReader bin)
        {
            Index = index;
            Colors = new short[34];

            for (var i = 0; i < 34; ++i)
            {
                Colors[i] = (short) (bin.ReadUInt16() | 0x8000);
            }

            var nulled = false;

            var sb = new StringBuilder(20, 20);

            for (var i = 0; i < 20; ++i)
            {
                var c = (char) bin.ReadByte();

                if (c == 0)
                {
                    nulled = true;
                }
                else if (!nulled)
                {
                    sb.Append(c);
                }
            }

            Name = sb.ToString();
        }

        public unsafe void ApplyTo(Bitmap bmp, bool onlyHueGrayPixels)
        {
            var bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format16bppArgb1555);

            var stride = bd.Stride >> 1;
            var width = bd.Width;
            var height = bd.Height;
            var delta = stride - width;

            var pBuffer = (ushort*) bd.Scan0;
            var pLineEnd = pBuffer + width;
            var pImageEnd = pBuffer + stride * height;

            var pColors = stackalloc ushort[0x40];

            fixed (short* pOriginal = Colors)
            {
                var pSource = (ushort*) pOriginal;
                var pDest = pColors;
                var pEnd = pDest + 32;

                while (pDest < pEnd)
                {
                    *pDest++ = 0;
                }

                pEnd += 32;

                while (pDest < pEnd)
                {
                    *pDest++ = *pSource++;
                }
            }

            if (onlyHueGrayPixels)
            {
                while (pBuffer < pImageEnd)
                {
                    while (pBuffer < pLineEnd)
                    {
                        int c = *pBuffer;
                        var r = (c >> 10) & 0x1F;
                        var g = (c >> 5) & 0x1F;
                        var b = c & 0x1F;

                        if (r == g && r == b)
                        {
                            *pBuffer++ = pColors[c >> 10];
                        }
                        else
                        {
                            ++pBuffer;
                        }
                    }

                    pBuffer += delta;
                    pLineEnd += stride;
                }
            }
            else
            {
                while (pBuffer < pImageEnd)
                {
                    while (pBuffer < pLineEnd)
                    {
                        *pBuffer = pColors[*pBuffer >> 10];
                        ++pBuffer;
                    }

                    pBuffer += delta;
                    pLineEnd += stride;
                }
            }

            bmp.UnlockBits(bd);
        }
    }
}