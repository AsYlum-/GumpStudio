using System;
using System.Drawing;
using System.IO;

namespace Ultima
{
    public static class Multis
    {
        public static MultiComponentList[] Cache { get; } = new MultiComponentList[0x4000];

        public static FileIndex FileIndex { get; } = new FileIndex("Multi.idx", "Multi.mul", 0x4000, 14);

        public static MultiComponentList GetComponents(int index)
        {
            MultiComponentList mcl;

            index &= 0x3FFF;

            if (index >= 0 && index < Cache.Length)
            {
                mcl = Cache[index];

                if (mcl == null)
                {
                    Cache[index] = mcl = Load(index);
                }
            }
            else
            {
                mcl = MultiComponentList.Empty;
            }

            return mcl;
        }

        public static MultiComponentList Load(int index)
        {
            try
            {
                var stream = FileIndex.Seek(index, out int length, out int extra, out bool patched);

                if (stream == null)
                {
                    return MultiComponentList.Empty;
                }

                return new MultiComponentList(new BinaryReader(stream), length / 12);
            }
            catch
            {
                return MultiComponentList.Empty;
            }
        }
    }

    public sealed class MultiComponentList
    {
        private readonly Point m_Min, m_Max;

        public static readonly MultiComponentList Empty = new MultiComponentList();

        public Point Min => m_Min;
        public Point Max => m_Max;
        public Point Center { get; }
        public int Width { get; }

        public int Height { get; }

        public Tile[][][] Tiles { get; }

        private struct MultiTileEntry
        {
            public short m_ItemID;
            public short m_OffsetX, m_OffsetY, m_OffsetZ;
            public int m_Flags;
        }

        public Bitmap GetImage()
        {
            if (Width == 0 || Height == 0)
            {
                return null;
            }

            int xMin = 1000, yMin = 1000;
            int xMax = -1000, yMax = -1000;

            for (var x = 0; x < Width; ++x)
            {
                for (var y = 0; y < Height; ++y)
            {
                var tiles = Tiles[x][y];

                for (var i = 0; i < tiles.Length; ++i)
                {
                    var bmp = Art.GetStatic(tiles[i].ID - 0x4000);

                    if (bmp == null)
                        {
                            continue;
                        }

                        var px = (x - y) * 22;
                    var py = (x + y) * 22;

                    px -= bmp.Width / 2;
                    py -= tiles[i].Z * 4;
                    py -= bmp.Height;

                    if (px < xMin)
                        {
                            xMin = px;
                        }

                        if (py < yMin)
                        {
                            yMin = py;
                        }

                        px += bmp.Width;
                    py += bmp.Height;

                    if (px > xMax)
                        {
                            xMax = px;
                        }

                        if (py > yMax)
                        {
                            yMax = py;
                        }
                    }
            }
            }

            var canvas = new Bitmap(xMax - xMin, yMax - yMin);
            var gfx = Graphics.FromImage(canvas);

            for (var x = 0; x < Width; ++x)
            {
                for (var y = 0; y < Height; ++y)
            {
                var tiles = Tiles[x][y];

                for (var i = 0; i < tiles.Length; ++i)
                {
                    var bmp = Art.GetStatic(tiles[i].ID - 0x4000);

                    if (bmp == null)
                        {
                            continue;
                        }

                        var px = (x - y) * 22;
                    var py = (x + y) * 22;

                    px -= bmp.Width / 2;
                    py -= tiles[i].Z * 4;
                    py -= bmp.Height;
                    px -= xMin;
                    py -= yMin;

                    gfx.DrawImageUnscaled(bmp, px, py, bmp.Width, bmp.Height);
                }
            }
            }

            gfx.Dispose();

            return canvas;
        }

        public MultiComponentList(BinaryReader reader, int count)
        {
            m_Min = m_Max = Point.Empty;

            var allTiles = new MultiTileEntry[count];

            for (var i = 0; i < count; ++i)
            {
                allTiles[i].m_ItemID = reader.ReadInt16();
                allTiles[i].m_OffsetX = reader.ReadInt16();
                allTiles[i].m_OffsetY = reader.ReadInt16();
                allTiles[i].m_OffsetZ = reader.ReadInt16();
                allTiles[i].m_Flags = reader.ReadInt32();

                var e = allTiles[i];

                if (e.m_OffsetX < m_Min.X)
                {
                    m_Min.X = e.m_OffsetX;
                }

                if (e.m_OffsetY < m_Min.Y)
                {
                    m_Min.Y = e.m_OffsetY;
                }

                if (e.m_OffsetX > m_Max.X)
                {
                    m_Max.X = e.m_OffsetX;
                }

                if (e.m_OffsetY > m_Max.Y)
                {
                    m_Max.Y = e.m_OffsetY;
                }
            }

            Center = new Point(-m_Min.X, -m_Min.Y);
            Width = m_Max.X - m_Min.X + 1;
            Height = m_Max.Y - m_Min.Y + 1;

            var tiles = new TileList[Width][];
            Tiles = new Tile[Width][][];

            for (var x = 0; x < Width; ++x)
            {
                tiles[x] = new TileList[Height];
                Tiles[x] = new Tile[Height][];

                for (var y = 0; y < Height; ++y)
                {
                    tiles[x][y] = new TileList();
                }
            }

            for (var i = 0; i < allTiles.Length; ++i)
            {
                var xOffset = allTiles[i].m_OffsetX + Center.X;
                var yOffset = allTiles[i].m_OffsetY + Center.Y;

                tiles[xOffset][yOffset].Add((short) ((allTiles[i].m_ItemID & 0x3FFF) + 0x4000),
                    (sbyte) allTiles[i].m_OffsetZ);
            }

            for (var x = 0; x < Width; ++x)
            {
                for (var y = 0; y < Height; ++y)
            {
                Tiles[x][y] = tiles[x][y].ToArray();

                if (Tiles[x][y].Length > 1)
                    {
                        Array.Sort(Tiles[x][y]);
                    }
                }
            }
        }

        private MultiComponentList()
        {
            Tiles = new Tile[0][][];
        }
    }
}