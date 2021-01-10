using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Ultima
{
    public static class Gumps
    {
        private static byte[] m_PixelBuffer;
        private static byte[] m_StreamBuffer;
        private static byte[] m_ColorTable;

        public static FileIndex FileIndex { get; } = new FileIndex("Gumpidx.mul", "Gumpart.mul", 0x10000, 12);

        public static unsafe Bitmap GetGump(int index, Hue hue, bool onlyHueGrayPixels)
        {
            var stream = FileIndex.Seek(index, out int length, out int extra, out bool patched);

            if (stream == null)
            {
                return null;
            }

            var width = (extra >> 16) & 0xFFFF;
            var height = extra & 0xFFFF;

            if (width <= 0 || height <= 0)
            {
                return null;
            }

            var bytesPerLine = width << 1;
            var bytesPerStride = (bytesPerLine + 3) & ~3;
            var bytesForImage = height * bytesPerStride;

            var pixelsPerStride = (width + 1) & ~1;
            var pixelsPerStrideDelta = pixelsPerStride - width;

            var pixelBuffer = m_PixelBuffer;

            if (pixelBuffer == null || pixelBuffer.Length < bytesForImage)
            {
                m_PixelBuffer = pixelBuffer = new byte[(bytesForImage + 2047) & ~2047];
            }

            var streamBuffer = m_StreamBuffer;

            if (streamBuffer == null || streamBuffer.Length < length)
            {
                m_StreamBuffer = streamBuffer = new byte[(length + 2047) & ~2047];
            }

            var colorTable = m_ColorTable;

            if (colorTable == null)
            {
                m_ColorTable = colorTable = new byte[128];
            }

            stream.Read(streamBuffer, 0, length);

            fixed (short* psHueColors = hue.Colors)
            {
                fixed (byte* pbStream = streamBuffer)
                {
                    fixed (byte* pbPixels = pixelBuffer)
                    {
                        fixed (byte* pbColorTable = colorTable)
                        {
                            var pHueColors = (ushort*) psHueColors;
                            var pHueColorsEnd = pHueColors + 32;

                            var pColorTable = (ushort*) pbColorTable;

                            var pColorTableOpaque = pColorTable;

                            while (pHueColors < pHueColorsEnd)
                            {
                                *pColorTableOpaque++ = *pHueColors++;
                            }

                            var pPixelDataStart = (ushort*) pbPixels;

                            var pLookup = (int*) pbStream;
                            var pLookupEnd = pLookup + height;
                            var pPixelRleStart = pLookup;
                            int* pPixelRle;

                            var pPixel = pPixelDataStart;
                            ushort* pRleEnd;
                            var pPixelEnd = pPixel + width;

                            ushort color, count;

                            if (onlyHueGrayPixels)
                            {
                                while (pLookup < pLookupEnd)
                                {
                                    pPixelRle = pPixelRleStart + *pLookup++;
                                    pRleEnd = pPixel;

                                    while (pPixel < pPixelEnd)
                                    {
                                        color = *(ushort*) pPixelRle;
                                        count = *(1 + (ushort*) pPixelRle);
                                        ++pPixelRle;

                                        pRleEnd += count;

                                        if (color != 0 && (color & 0x1F) == ((color >> 5) & 0x1F) &&
                                            (color & 0x1F) == ((color >> 10) & 0x1F))
                                        {
                                            color = pColorTable[color >> 10];
                                        }
                                        else if (color != 0)
                                        {
                                            color ^= 0x8000;
                                        }

                                        while (pPixel < pRleEnd)
                                        {
                                            *pPixel++ = color;
                                        }
                                    }

                                    pPixel += pixelsPerStrideDelta;
                                    pPixelEnd += pixelsPerStride;
                                }
                            }
                            else
                            {
                                while (pLookup < pLookupEnd)
                                {
                                    pPixelRle = pPixelRleStart + *pLookup++;
                                    pRleEnd = pPixel;

                                    while (pPixel < pPixelEnd)
                                    {
                                        color = *(ushort*) pPixelRle;
                                        count = *(1 + (ushort*) pPixelRle);
                                        ++pPixelRle;

                                        pRleEnd += count;

                                        if (color != 0)
                                        {
                                            color = pColorTable[color >> 10];
                                        }

                                        while (pPixel < pRleEnd)
                                        {
                                            *pPixel++ = color;
                                        }
                                    }

                                    pPixel += pixelsPerStrideDelta;
                                    pPixelEnd += pixelsPerStride;
                                }
                            }

                            return new Bitmap(width, height, bytesPerStride, PixelFormat.Format16bppArgb1555,
                                (IntPtr) pPixelDataStart);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Tests if index is defined
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool IsValidIndex(int index)
        {
            if (FileIndex == null)
            {
                return false;
            }

            //if (index > _cache.Length - 1)
            //{
            //    return false;
            //}

            //if (_removed[index])
            //{
            //    return false;
            //}

            //if (_cache[index] != null)
            //{
            //    return true;
            //}

            if (!FileIndex.Valid(index, out var _, out var extra, out var _))
            {
                return false;
            }

            if (extra == -1)
            {
                return false;
            }

            var width = (extra >> 16) & 0xFFFF;
            var height = extra & 0xFFFF;

            return width > 0 && height > 0;
        }

        public static unsafe Bitmap GetGump(int index)
        {
            var stream = FileIndex.Seek(index, out int length, out int extra, out bool patched);

            if (stream == null)
            {
                return null;
            }

            var width = (extra >> 16) & 0xFFFF;
            var height = extra & 0xFFFF;

            var bmp = new Bitmap(width, height, PixelFormat.Format16bppArgb1555);
            var bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly,
                PixelFormat.Format16bppArgb1555);
            var bin = new BinaryReader(stream);

            var lookups = new int[height];
            var start = (int) bin.BaseStream.Position;

            for (var i = 0; i < height; ++i)
            {
                lookups[i] = start + bin.ReadInt32() * 4;
            }

            var line = (ushort*) bd.Scan0;
            var delta = bd.Stride >> 1;

            for (var y = 0; y < height; ++y, line += delta)
            {
                bin.BaseStream.Seek(lookups[y], SeekOrigin.Begin);

                var cur = line;
                var end = line + bd.Width;

                while (cur < end)
                {
                    var color = bin.ReadUInt16();
                    var next = cur + bin.ReadUInt16();

                    if (color == 0)
                    {
                        cur = next;
                    }
                    else
                    {
                        color ^= 0x8000;

                        while (cur < next)
                        {
                            *cur++ = color;
                        }
                    }
                }
            }

            bmp.UnlockBits(bd);

            return bmp;
        }
    }
}