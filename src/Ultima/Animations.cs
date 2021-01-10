using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Ultima
{
    /// <summary>
    /// Contains translation tables used for mapping body values to file subsets.
    /// <seealso cref="Animations" />
    /// </summary>
    public static class BodyConverter
    {
        private static readonly int[] m_Table1 = new int[0];
        private static readonly int[] m_Table2 = new int[0];
        private static readonly int[] m_Table3 = new int[0];
        private static readonly int[] m_Table4 = new int[0];

        static BodyConverter()
        {
            var path = Client.GetFilePath("bodyconv.def");

            if (path == null)
            {
                return;
            }

            var list1 = new ArrayList();
            var list2 = new ArrayList();
            var list3 = new ArrayList();
            var list4 = new ArrayList();

            var max1 = 0;
            var max2 = 0;
            var max3 = 0;
            var max4 = 0;

            using (var ip = new StreamReader(path))
            {
                string line;

                while ((line = ip.ReadLine()) != null)
                {
                    if ((line = line.Trim()).Length == 0 || line.StartsWith("#"))
                    {
                        continue;
                    }

                    var split = line.Split('\t');

                    var original = System.Convert.ToInt32(split[0]);
                    var anim2 = System.Convert.ToInt32(split[1]);

                    if (split.Length >= 3 || !int.TryParse(split[2], out var anim3))
                    {
                        anim3 = -1;
                    }

                    if (split.Length >= 4 || !int.TryParse(split[3], out var anim4))
                    {
                        anim4 = -1;
                    }

                    if (split.Length >= 5 || !int.TryParse(split[4], out var anim5))
                    {
                        anim5 = -1;
                    }

                    if (anim2 != -1)
                    {
                        if (anim2 == 68)
                        {
                            anim2 = 122;
                        }

                        if (original > max1)
                        {
                            max1 = original;
                        }

                        list1.Add(original);
                        list1.Add(anim2);
                    }

                    if (anim3 != -1)
                    {
                        if (original > max2)
                        {
                            max2 = original;
                        }

                        list2.Add(original);
                        list2.Add(anim3);
                    }

                    if (anim4 != -1)
                    {
                        if (original > max3)
                        {
                            max3 = original;
                        }

                        list3.Add(original);
                        list3.Add(anim4);
                    }

                    if (anim5 != -1)
                    {
                        if (original > max4)
                        {
                            max4 = original;
                        }

                        list4.Add(original);
                        list4.Add(anim5);
                    }
                }
            }

            m_Table1 = new int[max1 + 1];
            m_Table2 = new int[max2 + 1];
            m_Table3 = new int[max3 + 1];
            m_Table4 = new int[max4 + 1];

            for (var i = 0; i < m_Table1.Length; ++i) m_Table1[i] = -1;

            for (var i = 0; i < list1.Count; i += 2) m_Table1[(int)list1[i]] = (int)list1[i + 1];

            for (var i = 0; i < m_Table2.Length; ++i) m_Table2[i] = -1;

            for (var i = 0; i < list2.Count; i += 2) m_Table2[(int)list2[i]] = (int)list2[i + 1];

            for (var i = 0; i < m_Table3.Length; ++i) m_Table3[i] = -1;

            for (var i = 0; i < list3.Count; i += 2) m_Table3[(int)list3[i]] = (int)list3[i + 1];

            for (var i = 0; i < m_Table4.Length; ++i) m_Table4[i] = -1;

            for (var i = 0; i < list4.Count; i += 2) m_Table4[(int)list4[i]] = (int)list4[i + 1];
        }

        /// <summary>
        /// Checks to see if <paramref name="body" /> is contained within the mapping table.
        /// </summary>
        /// <returns>True if it is, false if not.</returns>
        public static bool Contains(int body)
        {
            if (m_Table1 != null && body >= 0 && body < m_Table1.Length && m_Table1[body] != -1)
            {
                return true;
            }

            if (m_Table2 != null && body >= 0 && body < m_Table2.Length && m_Table2[body] != -1)
            {
                return true;
            }

            if (m_Table3 != null && body >= 0 && body < m_Table3.Length && m_Table3[body] != -1)
            {
                return true;
            }

            if (m_Table4 != null && body >= 0 && body < m_Table4.Length && m_Table4[body] != -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to convert <paramref name="body" /> to a body index relative to a file subset, specified by the return value.
        /// </summary>
        /// <returns>A value indicating a file subset:
        /// <list type="table">
        /// <listheader>
        /// <term>Return Value</term>
        /// <description>File Subset</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>Anim.mul, Anim.idx (Standard)</description>
        /// </item>
        /// <item>
        /// <term>2</term>
        /// <description>Anim2.mul, Anim2.idx (LBR)</description>
        /// </item>
        /// <item>
        /// <term>3</term>
        /// <description>Anim3.mul, Anim3.idx (AOS)</description>
        /// </item>
        /// </list>
        /// </returns>
        public static int Convert(ref int body)
        {
            if (m_Table1 != null && body >= 0 && body < m_Table1.Length)
            {
                var val = m_Table1[body];

                if (val != -1)
                {
                    body = val;
                    return 2;
                }
            }

            if (m_Table2 != null && body >= 0 && body < m_Table2.Length)
            {
                var val = m_Table2[body];

                if (val != -1)
                {
                    body = val;
                    return 3;
                }
            }

            if (m_Table3 != null && body >= 0 && body < m_Table3.Length)
            {
                var val = m_Table3[body];

                if (val != -1)
                {
                    body = val;
                    return 4;
                }
            }

            if (m_Table4 != null && body >= 0 && body < m_Table4.Length)
            {
                var val = m_Table4[body];

                if (val != -1)
                {
                    body = val;
                    return 5;
                }
            }

            return 1;
        }
    }

    public static class Animations
    {
        public static FileIndex FileIndex { get; } = new FileIndex("Anim.idx", "Anim.mul", 0x40000, 6);

        public static FileIndex FileIndex2 { get; } = new FileIndex("Anim2.idx", "Anim2.mul", 0x10000, -1);

        public static FileIndex FileIndex3 { get; } = new FileIndex("Anim3.idx", "Anim3.mul", 0x20000, -1);

        public static FileIndex FileIndex4 { get; } = new FileIndex("Anim4.idx", "Anim4.mul", 0x20000, -1);

        public static FileIndex FileIndex5 { get; } = new FileIndex("Anim5.idx", "Anim5.mul", 0x20000, -1);

        public static Frame[] GetAnimation(int body, int action, int direction, int hue, bool preserveHue)
        {
            if (preserveHue)
            {
                Translate(ref body);
            }
            else
            {
                Translate(ref body, ref hue);
            }

            var fileType = BodyConverter.Convert(ref body);
            FileIndex fileIndex;

            int index;

            switch (fileType)
            {
                default:
                    {
                        fileIndex = FileIndex;

                        if (body < 200)
                        {
                            index = body * 110;
                        }
                        else if (body < 400)
                        {
                            index = 22000 + (body - 200) * 65;
                        }
                        else
                        {
                            index = 35000 + (body - 400) * 175;
                        }

                        break;
                    }
                case 2:
                    {
                        fileIndex = FileIndex2;

                        if (body < 200)
                        {
                            index = body * 110;
                        }
                        else
                        {
                            index = 22000 + (body - 200) * 65;
                        }

                        break;
                    }
                case 3:
                    {
                        fileIndex = FileIndex3;

                        if (body < 300)
                        {
                            index = body * 65;
                        }
                        else if (body < 400)
                        {
                            index = 33000 + (body - 300) * 110;
                        }
                        else
                        {
                            index = 35000 + (body - 400) * 175;
                        }

                        break;
                    }
                case 4:
                    {
                        fileIndex = FileIndex4;

                        if (body < 200)
                        {
                            index = body * 110;
                        }
                        else if (body < 400)
                        {
                            index = 22000 + (body - 200) * 65;
                        }
                        else
                        {
                            index = 35000 + (body - 400) * 175;
                        }

                        break;
                    }
                case 5:
                    {
                        fileIndex = FileIndex5;

                        if (body < 200 && body != 34) // looks strange, though it works.
                        {
                            index = body * 110;
                        }
                        else
                        {
                            index = 35000 + (body - 400) * 65;
                        }

                        break;
                    }
            }

            if (index + action * 5 > int.MaxValue)
            {
                throw new ArithmeticException();
            }

            index += action * 5;

            if (direction <= 4)
            {
                index += direction;
            }
            else
            {
                index += direction - (direction - 4) * 2;
            }

            var stream = fileIndex.Seek(index, out _, out _, out _);

            if (stream == null)
            {
                return null;
            }

            var flip = direction > 4;

            var bin = new BinaryReader(stream);

            var palette = new ushort[0x100];

            for (var i = 0; i < 0x100; ++i) palette[i] = (ushort)(bin.ReadUInt16() ^ 0x8000);

            var start = (int)bin.BaseStream.Position;
            var frameCount = bin.ReadInt32();

            var lookups = new int[frameCount];

            for (var i = 0; i < frameCount; ++i) lookups[i] = start + bin.ReadInt32();

            var onlyHueGrayPixels = (hue & 0x8000) == 0;

            hue = (hue & 0x3FFF) - 1;

            Hue hueObject = null;

            if (hue >= 0 && hue < Hues.List.Length)
            {
                hueObject = Hues.List[hue];
            }

            var frames = new Frame[frameCount];

            for (var i = 0; i < frameCount; ++i)
            {
                bin.BaseStream.Seek(lookups[i], SeekOrigin.Begin);
                frames[i] = new Frame(palette, bin, flip);

                hueObject?.ApplyTo(frames[i].Bitmap, onlyHueGrayPixels);
            }

            return frames;
        }

        private static int[] m_Table;

        public static void Translate(ref int body)
        {
            if (m_Table == null)
            {
                LoadTable();
            }

            if (body <= 0 || body >= m_Table.Length)
            {
                body = 0;
                return;
            }

            body = m_Table[body] & 0x7FFF;
        }

        public static void Translate(ref int body, ref int hue)
        {
            if (m_Table == null)
            {
                LoadTable();
            }

            if (body <= 0 || body >= m_Table.Length)
            {
                body = 0;
                return;
            }

            var table = m_Table[body];

            if ((table & (1 << 31)) != 0)
            {
                body = table & 0x7FFF;

                var vhue = (hue & 0x3FFF) - 1;

                if (vhue < 0 || vhue >= Hues.List.Length)
                {
                    hue = (table >> 15) & 0xFFFF;
                }
            }
        }

        private static void LoadTable()
        {
            var count = 400 + (FileIndex.Index.Length - 35000) / 175;

            m_Table = new int[count];

            for (var i = 0; i < count; ++i)
            {
                var o = BodyTable.m_Entries[i];

                if (o == null || BodyConverter.Contains(i))
                {
                    m_Table[i] = i;
                }
                else
                {
                    var bte = (BodyTableEntry)o;

                    m_Table[i] = bte.m_OldID | (1 << 31) | (((bte.m_NewHue ^ 0x8000) & 0xFFFF) << 15);
                }
            }
        }
    }

    public sealed class Frame
    {
        public Point Center { get; }

        public Bitmap Bitmap { get; }

        private const int DoubleXor = (0x200 << 22) | (0x200 << 12);

        public static readonly Frame Empty = new Frame();
        public static readonly Frame[] EmptyFrames = new[] { Empty };

        private Frame()
        {
            Bitmap = new Bitmap(1, 1);
        }

        public unsafe Frame(ushort[] palette, BinaryReader bin, bool flip)
        {
            int xCenter = bin.ReadInt16();
            int yCenter = bin.ReadInt16();

            int width = bin.ReadUInt16();
            int height = bin.ReadUInt16();

            var bmp = new Bitmap(width, height, PixelFormat.Format16bppArgb1555);
            var bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly,
                PixelFormat.Format16bppArgb1555);

            var line = (ushort*)bd.Scan0;
            var delta = bd.Stride >> 1;

            int header;

            var xBase = xCenter - 0x200;
            var yBase = yCenter + height - 0x200;

            if (!flip)
            {
                line += xBase;
                line += yBase * delta;

                while ((header = bin.ReadInt32()) != 0x7FFF7FFF)
                {
                    header ^= DoubleXor;

                    var cur = line + (((header >> 12) & 0x3FF) * delta + ((header >> 22) & 0x3FF));
                    var end = cur + (header & 0xFFF);

                    while (cur < end) *cur++ = palette[bin.ReadByte()];
                }
            }
            else
            {
                line -= xBase - width + 1;
                line += yBase * delta;

                while ((header = bin.ReadInt32()) != 0x7FFF7FFF)
                {
                    header ^= DoubleXor;

                    var cur = line + (((header >> 12) & 0x3FF) * delta - ((header >> 22) & 0x3FF));
                    var end = cur - (header & 0xFFF);

                    while (cur > end) *cur-- = palette[bin.ReadByte()];
                }

                xCenter = width - xCenter;
            }

            bmp.UnlockBits(bd);

            Center = new Point(xCenter, yCenter);
            Bitmap = bmp;
        }
    }

    public sealed class BodyTableEntry
    {
        public int m_OldID;
        public int m_NewID;
        public int m_NewHue;

        public BodyTableEntry(int oldID, int newID, int newHue)
        {
            m_OldID = oldID;
            m_NewID = newID;
            m_NewHue = newHue;
        }
    }

    public sealed class BodyTable
    {
        public static Hashtable m_Entries;

        static BodyTable()
        {
            m_Entries = new Hashtable();

            var filePath = Client.GetFilePath("body.def");

            if (filePath == null)
            {
                return;
            }

            var def = new StreamReader(filePath);

            string line;

            while ((line = def.ReadLine()) != null)
            {
                if ((line = line.Trim()).Length == 0 || line.StartsWith("#"))
                {
                    continue;
                }

                try
                {
                    var index1 = line.IndexOf(" {");
                    var index2 = line.IndexOf("} ");

                    var param1 = line.Substring(0, index1);
                    var param2 = line.Substring(index1 + 2, index2 - index1 - 2);
                    var param3 = line.Substring(index2 + 2);

                    var indexOf = param2.IndexOf(',');

                    if (indexOf > -1)
                    {
                        param2 = param2.Substring(0, indexOf).Trim();
                    }

                    var iParam1 = Convert.ToInt32(param1);
                    var iParam2 = Convert.ToInt32(param2);
                    var iParam3 = Convert.ToInt32(param3);

                    m_Entries[iParam1] = new BodyTableEntry(iParam2, iParam1, iParam3);
                }
                catch
                {
                }
            }
        }
    }
}