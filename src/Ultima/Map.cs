using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Ultima
{
    public sealed class Map
    {
        public static short[] Colors { get; set; }

        public static readonly Map Felucca = new Map(0, 0, 6144, 4096);
        public static readonly Map Trammel = new Map(0, 1, 6144, 4096);
        public static readonly Map Ilshenar = new Map(2, 2, 2304, 1600);
        public static readonly Map Malas = new Map(3, 3, 2560, 2048);
        public static readonly Map Tokuno = new Map(4, 4, 1448, 1448);

        private TileMatrix m_Tiles;
        private readonly int m_FileIndex, m_MapID;

        private Map(int fileIndex, int mapID, int width, int height)
        {
            m_FileIndex = fileIndex;
            m_MapID = mapID;
            Width = width;
            Height = height;
        }

        public bool LoadedMatrix => m_Tiles != null;

        public TileMatrix Tiles => m_Tiles ?? (m_Tiles = new TileMatrix(m_FileIndex, m_MapID, Width, Height));

        public int Width { get; }

        public int Height { get; }

        public Bitmap GetImage(int x, int y, int width, int height)
        {
            return GetImage(x, y, width, height, true);
        }

        public Bitmap GetImage(int x, int y, int width, int height, bool statics)
        {
            var bmp = new Bitmap(width << 3, height << 3, PixelFormat.Format16bppRgb555);

            GetImage(x, y, width, height, bmp, statics);

            return bmp;
        }

        private short[][][] m_Cache;
        private short[][][] m_Cache_NoStatics;
        private short[] m_Black;

        private short[] GetRenderedBlock(int x, int y, bool statics)
        {
            var matrix = Tiles;

            var bw = matrix.BlockWidth;
            var bh = matrix.BlockHeight;

            if (x < 0 || y < 0 || x >= bw || y >= bh)
            {
                return m_Black ?? (m_Black = new short[64]);
            }

            var cache = statics ? m_Cache : m_Cache_NoStatics;

            if (cache == null)
            {
                if (statics)
                {
                    m_Cache = cache = new short[m_Tiles.BlockHeight][][];
                }
                else
                {
                    m_Cache_NoStatics = new short[m_Tiles.BlockHeight][][];
                }
            }

            if (cache[y] == null)
            {
                cache[y] = new short[m_Tiles.BlockWidth][];
            }

            var data = cache[y][x];

            if (data == null)
            {
                cache[y][x] = data = RenderBlock(x, y, statics);
            }

            return data;
        }

        private unsafe short[] RenderBlock(int x, int y, bool drawStatics)
        {
            var data = new short[64];

            var tiles = m_Tiles.GetLandBlock(x, y);

            fixed (short* pColors = Colors)
            {
                fixed (int* pHeight = TileData.HeightTable)
                {
                    fixed (Tile* ptTiles = tiles)
                    {
                        var pTiles = ptTiles;

                        fixed (short* pData = data)
                        {
                            var pvData = pData;

                            if (drawStatics)
                            {
                                var statics = m_Tiles.GetStaticBlock(x, y);

                                for (int k = 0, v = 0; k < 8; ++k, v += 8)
                                {
                                    for (var p = 0; p < 8; ++p)
                                {
                                    var highTop = -255;
                                    var highZ = -255;
                                    var highID = 0;
                                    var highHue = 0;
                                    int top;

                                    var curStatics = statics[p][k];

                                    if (curStatics.Length > 0)
                                        {
                                            fixed (HuedTile* phtStatics = curStatics)
                                        {
                                            var pStatics = phtStatics;
                                            var pStaticsEnd = pStatics + curStatics.Length;

                                            while (pStatics < pStaticsEnd)
                                            {
                                                int z = pStatics->m_Z;
                                                top = z + pHeight[pStatics->m_ID & 0x3FFF];

                                                if (top > highTop || z > highZ && top >= highTop)
                                                {
                                                    highTop = top;
                                                    highZ = z;
                                                    highID = pStatics->m_ID;
                                                    highHue = pStatics->m_Hue;
                                                }

                                                ++pStatics;
                                            }
                                        }
                                        }

                                        top = pTiles->m_Z;

                                    if (top > highTop)
                                    {
                                        highID = pTiles->m_ID;
                                        highHue = 0;
                                    }

                                    if (highHue == 0)
                                        {
                                            *pvData++ = pColors[highID];
                                        }
                                        else
                                        {
                                            *pvData++ = Hues.GetHue(highHue - 1).Colors[(pColors[highID] >> 10) & 0x1F];
                                        }

                                        ++pTiles;
                                }
                                }
                            }
                            else
                            {
                                var pEnd = pTiles + 64;

                                while (pTiles < pEnd)
                                {
                                    *pvData++ = pColors[(pTiles++)->m_ID];
                                }
                            }
                        }
                    }
                }
            }

            return data;
        }

        public void GetImage(int x, int y, int width, int height, Bitmap bmp)
        {
            GetImage(x, y, width, height, bmp, true);
        }

        public unsafe void GetImage(int x, int y, int width, int height, Bitmap bmp, bool statics)
        {
            if (Colors == null)
            {
                LoadColors();
            }

            var bd = bmp.LockBits(new Rectangle(0, 0, width << 3, height << 3), ImageLockMode.WriteOnly,
                PixelFormat.Format16bppRgb555);

            var stride = bd.Stride;
            var blockStride = stride << 3;

            var pStart = (byte*) bd.Scan0;

            for (int oy = 0, by = y; oy < height; ++oy, ++by, pStart += blockStride)
            {
                var pRow0 = (int*) (pStart + 0 * stride);
                var pRow1 = (int*) (pStart + 1 * stride);
                var pRow2 = (int*) (pStart + 2 * stride);
                var pRow3 = (int*) (pStart + 3 * stride);
                var pRow4 = (int*) (pStart + 4 * stride);
                var pRow5 = (int*) (pStart + 5 * stride);
                var pRow6 = (int*) (pStart + 6 * stride);
                var pRow7 = (int*) (pStart + 7 * stride);

                for (int ox = 0, bx = x; ox < width; ++ox, ++bx)
                {
                    var data = GetRenderedBlock(bx, by, statics);

                    fixed (short* pData = data)
                    {
                        var pvData = (int*) pData;

                        *pRow0++ = *pvData++;
                        *pRow0++ = *pvData++;
                        *pRow0++ = *pvData++;
                        *pRow0++ = *pvData++;

                        *pRow1++ = *pvData++;
                        *pRow1++ = *pvData++;
                        *pRow1++ = *pvData++;
                        *pRow1++ = *pvData++;

                        *pRow2++ = *pvData++;
                        *pRow2++ = *pvData++;
                        *pRow2++ = *pvData++;
                        *pRow2++ = *pvData++;

                        *pRow3++ = *pvData++;
                        *pRow3++ = *pvData++;
                        *pRow3++ = *pvData++;
                        *pRow3++ = *pvData++;

                        *pRow4++ = *pvData++;
                        *pRow4++ = *pvData++;
                        *pRow4++ = *pvData++;
                        *pRow4++ = *pvData++;

                        *pRow5++ = *pvData++;
                        *pRow5++ = *pvData++;
                        *pRow5++ = *pvData++;
                        *pRow5++ = *pvData++;

                        *pRow6++ = *pvData++;
                        *pRow6++ = *pvData++;
                        *pRow6++ = *pvData++;
                        *pRow6++ = *pvData++;

                        *pRow7++ = *pvData++;
                        *pRow7++ = *pvData++;
                        *pRow7++ = *pvData++;
                        *pRow7++ = *pvData++;
                    }
                }
            }

            bmp.UnlockBits(bd);
        }

        /*public unsafe void GetImage( int x, int y, int width, int height, Bitmap bmp )
        {
            if ( m_Colors == null )
                LoadColors();

            TileMatrix matrix = Tiles;

            BitmapData bd = bmp.LockBits( new Rectangle( 0, 0, width<<3, height<<3 ), ImageLockMode.WriteOnly, PixelFormat.Format16bppRgb555 );

            int scanDelta = bd.Stride >> 1;

            short *pvDest = (short *)bd.Scan0;

            fixed ( short *pColors = m_Colors )
            {
                fixed ( int *pHeight = TileData.HeightTable )
                {
                    for ( int i = 0; i < width; ++i )
                    {
                        pvDest = ((short *)bd.Scan0) + (i << 3);

                        for ( int j = 0; j < height; ++j )
                        {
                            Tile[] tiles = matrix.GetLandBlock( x + i, y + j );
                            HuedTile[][][] statics = matrix.GetStaticBlock( x + i, y + j );

                            for ( int k = 0, v = 0; k < 8; ++k, v += 8 )
                            {
                                for ( int p = 0; p < 8; ++p )
                                {
                                    int highTop = -255;
                                    int highZ = -255;
                                    int highID = 0;
                                    int highHue = 0;
                                    int z, top;

                                    HuedTile[] curStatics = statics[p][k];

                                    if ( curStatics.Length > 0 )
                                    {
                                        fixed ( HuedTile *phtStatics = curStatics )
                                        {
                                            HuedTile *pStatics = phtStatics;
                                            HuedTile *pStaticsEnd = pStatics + curStatics.Length;

                                            while ( pStatics < pStaticsEnd )
                                            {
                                                z = pStatics->m_Z;
                                                top = z + pHeight[pStatics->m_ID & 0x3FFF];

                                                if ( top > highTop || (z > highZ && top >= highTop) )
                                                {
                                                    highTop = top;
                                                    highZ = z;
                                                    highID = pStatics->m_ID;
                                                    highHue = pStatics->m_Hue;
                                                }

                                                ++pStatics;
                                            }
                                        }
                                    }

                                    top = tiles[v + p].Z;

                                    if ( top > highTop )
                                    {
                                        highID = tiles[v + p].ID;
                                        highHue = 0;
                                    }

                                    if ( highHue == 0 )
                                        pvDest[p] = pColors[highID];
                                    else
                                        pvDest[p] = Hues.GetHue( highHue - 1 ).Colors[(pColors[highID] >> 10) & 0x1F];
                                }

                                pvDest += scanDelta;
                            }
                        }
                    }
                }
            }

            bmp.UnlockBits(bd);
        }*/

        private static unsafe void LoadColors()
        {
            Colors = new short[0x8000];

            var path = Client.GetFilePath("radarcol.mul");

            if (path == null)
            {
                return;
            }

            fixed (short* pColors = Colors)
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    NativeMethods._lread(fs.SafeFileHandle, pColors, 0x10000);
                }
            }
        }
    }
}