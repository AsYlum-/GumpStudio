using System.IO;

namespace Ultima
{
    public class TileMatrixPatch
    {
        public int LandBlocks { get; }

        public int StaticBlocks { get; }

        public TileMatrixPatch(TileMatrix matrix, int index)
        {
            var mapDataPath = Client.GetFilePath("mapdif{0}.mul", index);
            var mapIndexPath = Client.GetFilePath("mapdifl{0}.mul", index);

            if (mapDataPath != null && mapIndexPath != null)
            {
                LandBlocks = PatchLand(matrix, mapDataPath, mapIndexPath);
            }

            var staDataPath = Client.GetFilePath("stadif{0}.mul", index);
            var staIndexPath = Client.GetFilePath("stadifl{0}.mul", index);
            var staLookupPath = Client.GetFilePath("stadifi{0}.mul", index);

            if (staDataPath != null && staIndexPath != null && staLookupPath != null)
            {
                StaticBlocks = PatchStatics(matrix, staDataPath, staIndexPath, staLookupPath);
            }
        }

        private static unsafe int PatchLand(TileMatrix matrix, string dataPath, string indexPath)
        {
            using (var fsData = new FileStream(dataPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var fsIndex = new FileStream(indexPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var indexReader = new BinaryReader(fsIndex);

                    var count = (int) (indexReader.BaseStream.Length / 4);

                    for (var i = 0; i < count; ++i)
                    {
                        var blockID = indexReader.ReadInt32();
                        var x = blockID / matrix.BlockHeight;
                        var y = blockID % matrix.BlockHeight;

                        fsData.Seek(4, SeekOrigin.Current);

                        var tiles = new Tile[64];

                        fixed (Tile* pTiles = tiles)
                        {
                            NativeMethods._lread(fsData.SafeFileHandle, pTiles, 192);
                        }

                        matrix.SetLandBlock(x, y, tiles);
                    }

                    return count;
                }
            }
        }

        private static unsafe int PatchStatics(TileMatrix matrix, string dataPath, string indexPath, string lookupPath)
        {
            using (var fsData = new FileStream(dataPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var fsIndex = new FileStream(indexPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var fsLookup = new FileStream(lookupPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var indexReader = new BinaryReader(fsIndex);
                        var lookupReader = new BinaryReader(fsLookup);

                        var count = (int) (indexReader.BaseStream.Length / 4);

                        var lists = new HuedTileList[8][];

                        for (var x = 0; x < 8; ++x)
                        {
                            lists[x] = new HuedTileList[8];

                            for (var y = 0; y < 8; ++y)
                            {
                                lists[x][y] = new HuedTileList();
                            }
                        }

                        for (var i = 0; i < count; ++i)
                        {
                            var blockID = indexReader.ReadInt32();
                            var blockX = blockID / matrix.BlockHeight;
                            var blockY = blockID % matrix.BlockHeight;

                            var offset = lookupReader.ReadInt32();
                            var length = lookupReader.ReadInt32();
                            lookupReader.ReadInt32(); // Extra

                            if (offset < 0 || length <= 0)
                            {
                                matrix.SetStaticBlock(blockX, blockY, matrix.EmptyStaticBlock);
                                continue;
                            }

                            fsData.Seek(offset, SeekOrigin.Begin);

                            var tileCount = length / 7;

                            var staTiles = new StaticTile[tileCount];

                            fixed (StaticTile* pTiles = staTiles)
                            {
                                NativeMethods._lread(fsData.SafeFileHandle, pTiles, length);

                                StaticTile* pCur = pTiles, pEnd = pTiles + tileCount;

                                while (pCur < pEnd)
                                {
                                    lists[pCur->m_X & 0x7][pCur->m_Y & 0x7]
                                        .Add((short) ((pCur->m_ID & 0x3FFF) + 0x4000), pCur->m_Hue, pCur->m_Z);
                                    ++pCur;
                                }

                                var tiles = new HuedTile[8][][];

                                for (var x = 0; x < 8; ++x)
                                {
                                    tiles[x] = new HuedTile[8][];

                                    for (var y = 0; y < 8; ++y)
                                    {
                                        tiles[x][y] = lists[x][y].ToArray();
                                    }
                                }

                                matrix.SetStaticBlock(blockX, blockY, tiles);
                            }
                        }

                        return count;
                    }
                }
            }
        }
    }
}