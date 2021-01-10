using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Ultima
{
    public sealed class Art
    {
        private static readonly byte[] _validBuffer = new byte[4];

        private Art()
        {
        }

        public static FileIndex FileIndex { get; } = new FileIndex("Artidx.mul", "Art.mul", 0x14000, 4);

        //QUERY: Does this really need to be exposed?
        public static Bitmap[] Cache { get; } = new Bitmap[0x14000];

        public static Bitmap GetLand(int index)
        {
            return GetLand(index, true);
        }

        public static Bitmap GetLand(int index, bool cache)
        {
            index &= 0x3FFF;

            if (Cache[index] != null)
            {
                return Cache[index];
            }

            var stream = FileIndex.Seek(index, out _, out _, out _);

            if (stream == null)
            {
                return null;
            }

            if (cache)
            {
                return Cache[index] = LoadLand(stream);
            }

            return LoadLand(stream);
        }

        public static int GetMaxItemID()
        {
            if (GetIdxLength() >= 0x13FDC)
            {
                return 0xFFFF;
            }

            if (GetIdxLength() == 0xC000)
            {
                return 0x7FFF;
            }

            return 0x3FFF;
        }

        public static int GetIdxLength()
        {
            return FileIndex.Index.Length;
        }

        public static ushort GetLegalItemID(int itemId, bool checkMaxId = true)
        {
            if (itemId < 0)
            {
                return 0;
            }

            if (!checkMaxId)
            {
                return (ushort) itemId;
            }

            var max = GetMaxItemID();
            if (itemId > max)
            {
                return 0;
            }

            return (ushort) itemId;
        }

        /// <summary>
        ///     Tests if Static is defined (width and height check)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool IsValidStatic(int index, out byte[] artSize)
        {
            index = GetLegalItemID(index);
            index += 0x4000;

            if (Cache[index] != null)
            {
                artSize = new[] {(byte) Cache[index].Size.Width, (byte) Cache[index].Size.Height};

                return true;
            }

            var stream = FileIndex.Seek(index, out var _, out var _, out var _);

            if (stream == null)
            {
                artSize = new byte[] {0, 0};
                return false;
            }

            stream.Seek(4, SeekOrigin.Current);
            stream.Read(_validBuffer, 0, 4);

            ref var width = ref _validBuffer[0];
            ref var height = ref _validBuffer[2];

            artSize = new[] {width, height};
            return width > 0 && height > 0;
        }

        public static Bitmap GetStatic(int index)
        {
            return GetStatic(index, true);
        }

        public static Bitmap GetStatic(int index, bool cache)
        {
            if (index > int.MaxValue - 0x4000)
            {
                throw new ArithmeticException("The index must not exceed (int.MaxValue - 0x4000)");
            }

            index += 0x4000;
            //index &= 0xFFFF;

            if (cache && Cache[index] != null)
            {
                return Cache[index];
            }

            var stream = FileIndex.Seek(index, out _, out _, out _);

            if (stream == null)
            {
                return null;
            }

            if (cache)
            {
                return Cache[index] = LoadStatic(stream);
            }

            return LoadStatic(stream);
        }

        public static unsafe void Measure(Bitmap bmp, out int xMin, out int yMin, out int xMax, out int yMax)
        {
            xMin = yMin = 0;
            xMax = yMax = -1;

            if (bmp == null || bmp.Width <= 0 || bmp.Height <= 0)
            {
                return;
            }

            var bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format16bppArgb1555);

            var delta = (bd.Stride >> 1) - bd.Width;
            var lineDelta = bd.Stride >> 1;

            var pBuffer = (ushort*) bd.Scan0;
            var pLineEnd = pBuffer + bd.Width;
            var pEnd = pBuffer + bd.Height * lineDelta;

            var foundPixel = false;

            int x = 0, y = 0;

            while (pBuffer < pEnd)
            {
                while (pBuffer < pLineEnd)
                {
                    var c = *pBuffer++;

                    if ((c & 0x8000) != 0)
                    {
                        if (!foundPixel)
                        {
                            foundPixel = true;
                            xMin = xMax = x;
                            yMin = yMax = y;
                        }
                        else
                        {
                            if (x < xMin)
                            {
                                xMin = x;
                            }

                            if (y < yMin)
                            {
                                yMin = y;
                            }

                            if (x > xMax)
                            {
                                xMax = x;
                            }

                            if (y > yMax)
                            {
                                yMax = y;
                            }
                        }
                    }

                    ++x;
                }

                pBuffer += delta;
                pLineEnd += lineDelta;
                ++y;
                x = 0;
            }

            bmp.UnlockBits(bd);
        }

        private static unsafe Bitmap LoadStatic(Stream stream)
        {
            var bin = new BinaryReader(stream);

            bin.ReadInt32();
            int width = bin.ReadInt16();
            int height = bin.ReadInt16();

            if (width <= 0 || height <= 0)
            {
                return null;
            }

            var lookups = new int[height];

            var start = (int) bin.BaseStream.Position + height * 2;

            for (var i = 0; i < height; ++i)
            {
                lookups[i] = start + bin.ReadUInt16() * 2;
            }

            var bmp = new Bitmap(width, height, PixelFormat.Format16bppArgb1555);
            var bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly,
                PixelFormat.Format16bppArgb1555);

            var line = (ushort*) bd.Scan0;
            var delta = bd.Stride >> 1;

            for (var y = 0; y < height; ++y, line += delta)
            {
                bin.BaseStream.Seek(lookups[y], SeekOrigin.Begin);

                var cur = line;
                ushort* end;

                int xOffset, xRun;

                while ((xOffset = bin.ReadUInt16()) + (xRun = bin.ReadUInt16()) != 0)
                {
                    cur += xOffset;
                    end = cur + xRun;

                    while (cur < end)
                    {
                        *cur++ = (ushort) (bin.ReadUInt16() ^ 0x8000);
                    }
                }
            }

            bmp.UnlockBits(bd);

            return bmp;
        }

        private static unsafe Bitmap LoadLand(Stream stream)
        {
            var bmp = new Bitmap(44, 44, PixelFormat.Format16bppArgb1555);
            var bd = bmp.LockBits(new Rectangle(0, 0, 44, 44), ImageLockMode.WriteOnly,
                PixelFormat.Format16bppArgb1555);
            var bin = new BinaryReader(stream);

            var xOffset = 21;
            var xRun = 2;

            var line = (ushort*) bd.Scan0;
            var delta = bd.Stride >> 1;

            for (var y = 0; y < 22; ++y, --xOffset, xRun += 2, line += delta)
            {
                var cur = line + xOffset;
                var end = cur + xRun;

                while (cur < end)
                {
                    *cur++ = (ushort) (bin.ReadUInt16() | 0x8000);
                }
            }

            xOffset = 0;
            xRun = 44;

            for (var y = 0; y < 22; ++y, ++xOffset, xRun -= 2, line += delta)
            {
                var cur = line + xOffset;
                var end = cur + xRun;

                while (cur < end)
                {
                    *cur++ = (ushort) (bin.ReadUInt16() | 0x8000);
                }
            }

            bmp.UnlockBits(bd);

            return bmp;
        }
    }
}