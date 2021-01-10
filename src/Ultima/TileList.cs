namespace Ultima
{
    public class HuedTileList
    {
        private HuedTile[] m_Tiles;

        public HuedTileList()
        {
            m_Tiles = new HuedTile[8];
            Count = 0;
        }

        public int Count { get; private set; }

        public void Add(short id, short hue, sbyte z)
        {
            if (Count + 1 > m_Tiles.Length)
            {
                var old = m_Tiles;
                m_Tiles = new HuedTile[old.Length * 2];

                for (var i = 0; i < old.Length; ++i)
                {
                    m_Tiles[i] = old[i];
                }
            }

            m_Tiles[Count++].Set(id, hue, z);
        }

        public HuedTile[] ToArray()
        {
            var tiles = new HuedTile[Count];

            for (var i = 0; i < Count; ++i)
            {
                tiles[i] = m_Tiles[i];
            }

            Count = 0;

            return tiles;
        }
    }

    public class TileList
    {
        private Tile[] m_Tiles;

        public TileList()
        {
            m_Tiles = new Tile[8];
            Count = 0;
        }

        public int Count { get; private set; }

        public void Add(short id, sbyte z)
        {
            if (Count + 1 > m_Tiles.Length)
            {
                var old = m_Tiles;
                m_Tiles = new Tile[old.Length * 2];

                for (var i = 0; i < old.Length; ++i)
                {
                    m_Tiles[i] = old[i];
                }
            }

            m_Tiles[Count++].Set(id, z);
        }

        public Tile[] ToArray()
        {
            var tiles = new Tile[Count];

            for (var i = 0; i < Count; ++i)
            {
                tiles[i] = m_Tiles[i];
            }

            Count = 0;

            return tiles;
        }
    }
}