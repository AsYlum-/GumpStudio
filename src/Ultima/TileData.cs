using System;
using System.IO;
using System.Text;

namespace Ultima
{
    /// <summary>
    /// Represents land tile data.
    /// <seealso cref="ItemData" />
    /// <seealso cref="LandData" />
    /// </summary>
    public struct LandData
    {
        public LandData(string name, TileFlag flags)
        {
            Name = name;
            Flags = flags;
        }

        /// <summary>
        /// Gets the name of this land tile.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets a bitfield representing the 32 individual flags of this land tile.
        /// </summary>
        public TileFlag Flags { get; }
    }

    /// <summary>
    /// Represents item tile data.
    /// <seealso cref="TileData" />
    /// <seealso cref="LandData" />
    /// </summary>
    public struct ItemData
    {
        internal string m_Name;
        internal TileFlag m_Flags;
        internal byte m_Weight;
        internal byte m_Quality;
        internal byte m_Quantity;
        internal byte m_Value;
        internal byte m_Height;
        internal short m_Animation;

        public ItemData(string name, TileFlag flags, int weight, int quality, int quantity, int value, int height,
            int anim)
        {
            m_Name = name;
            m_Flags = flags;
            m_Weight = (byte) weight;
            m_Quality = (byte) quality;
            m_Quantity = (byte) quantity;
            m_Value = (byte) value;
            m_Height = (byte) height;
            m_Animation = (short) anim;
        }

        /// <summary>
        /// Gets the name of this item.
        /// </summary>
        public string Name => m_Name;

        /// <summary>
        /// Gets the animation body index of this item.
        /// <seealso cref="Animations" />
        /// </summary>
        public int Animation => m_Animation;

        /// <summary>
        /// Gets a bitfield representing the 32 individual flags of this item.
        /// <seealso cref="TileFlag" />
        /// </summary>
        public TileFlag Flags => m_Flags;

        /// <summary>
        /// Whether or not this item is flagged as '<see cref="TileFlag.Background" />'.
        /// <seealso cref="TileFlag" />
        /// </summary>
        public bool Background => (m_Flags & TileFlag.Background) != 0;

        /// <summary>
        /// Whether or not this item is flagged as '<see cref="TileFlag.Bridge" />'.
        /// <seealso cref="TileFlag" />
        /// </summary>
        public bool Bridge => (m_Flags & TileFlag.Bridge) != 0;

        /// <summary>
        /// Whether or not this item is flagged as '<see cref="TileFlag.Impassable" />'.
        /// <seealso cref="TileFlag" />
        /// </summary>
        public bool Impassable => (m_Flags & TileFlag.Impassable) != 0;

        /// <summary>
        /// Whether or not this item is flagged as '<see cref="TileFlag.Surface" />'.
        /// <seealso cref="TileFlag" />
        /// </summary>
        public bool Surface => (m_Flags & TileFlag.Surface) != 0;

        /// <summary>
        /// Gets the weight of this item.
        /// </summary>
        public int Weight => m_Weight;

        /// <summary>
        /// Gets the 'quality' of this item. For wearable items, this will be the layer.
        /// </summary>
        public int Quality => m_Quality;

        /// <summary>
        /// Gets the 'quantity' of this item.
        /// </summary>
        public int Quantity => m_Quantity;

        /// <summary>
        /// Gets the 'value' of this item.
        /// </summary>
        public int Value => m_Value;

        /// <summary>
        /// Gets the height of this item.
        /// </summary>
        public int Height => m_Height;

        /// <summary>
        /// Gets the 'calculated height' of this item. For <see cref="Bridge">bridges</see>, this will be: <c>(<see cref="Height" /> / 2)</c>.
        /// </summary>
        public int CalcHeight
        {
            get
            {
                if ((m_Flags & TileFlag.Bridge) != 0)
                {
                    return m_Height / 2;
                }

                return m_Height;
            }
        }
    }

    /// <summary>
    /// An enumeration of 32 different tile flags.
    /// <seealso cref="ItemData" />
    /// <seealso cref="LandData" />
    /// </summary>
    [Flags]
    public enum TileFlag
    {
        /// <summary>
        /// Nothing is flagged.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        Background = 0x00000001,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        Weapon = 0x00000002,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        Transparent = 0x00000004,

        /// <summary>
        /// The tile is rendered with partial alpha-transparency.
        /// </summary>
        Translucent = 0x00000008,

        /// <summary>
        /// The tile is a wall.
        /// </summary>
        Wall = 0x00000010,

        /// <summary>
        /// The tile can cause damage when moved over.
        /// </summary>
        Damaging = 0x00000020,

        /// <summary>
        /// The tile may not be moved over or through.
        /// </summary>
        Impassable = 0x00000040,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        Wet = 0x00000080,

        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown1 = 0x00000100,

        /// <summary>
        /// The tile is a surface. It may be moved over, but not through.
        /// </summary>
        Surface = 0x00000200,

        /// <summary>
        /// The tile is a stair, ramp, or ladder.
        /// </summary>
        Bridge = 0x00000400,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        Generic = 0x00000800,

        /// <summary>
        /// The tile is a window. Like <see cref="TileFlag.NoShoot" />, tiles with this flag block line of sight.
        /// </summary>
        Window = 0x00001000,

        /// <summary>
        /// The tile blocks line of sight.
        /// </summary>
        NoShoot = 0x00002000,

        /// <summary>
        /// For single-amount tiles, the string "a " should be prepended to the tile name.
        /// </summary>
        ArticleA = 0x00004000,

        /// <summary>
        /// For single-amount tiles, the string "an " should be prepended to the tile name.
        /// </summary>
        ArticleAn = 0x00008000,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        Internal = 0x00010000,

        /// <summary>
        /// The tile becomes translucent when walked behind. Boat masts also have this flag.
        /// </summary>
        Foliage = 0x00020000,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        PartialHue = 0x00040000,

        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown2 = 0x00080000,

        /// <summary>
        /// The tile is a map--in the cartography sense. Unknown usage.
        /// </summary>
        Map = 0x00100000,

        /// <summary>
        /// The tile is a container.
        /// </summary>
        Container = 0x00200000,

        /// <summary>
        /// The tile may be equiped.
        /// </summary>
        Wearable = 0x00400000,

        /// <summary>
        /// The tile gives off light.
        /// </summary>
        LightSource = 0x00800000,

        /// <summary>
        /// The tile is animated.
        /// </summary>
        Animation = 0x01000000,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        NoDiagonal = 0x02000000,

        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown3 = 0x04000000,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        Armor = 0x08000000,

        /// <summary>
        /// The tile is a slanted roof.
        /// </summary>
        Roof = 0x10000000,

        /// <summary>
        /// The tile is a door. Tiles with this flag can be moved through by ghosts and GMs.
        /// </summary>
        Door = 0x20000000,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        StairBack = 0x40000000,

        /// <summary>
        /// Not yet documented.
        /// </summary>
        StairRight = unchecked((int) 0x80000000)
    }

    /// <summary>
    /// Contains lists of <see cref="LandData">land</see> and <see cref="ItemData">item</see> tile data.
    /// <seealso cref="LandData" />
    /// <seealso cref="ItemData" />
    /// </summary>
    public sealed class TileData
    {
        /// <summary>
        /// Gets the list of <see cref="LandData">land tile data</see>.
        /// </summary>
        public static LandData[] LandTable { get; }

        /// <summary>
        /// Gets the list of <see cref="ItemData">item tile data</see>.
        /// </summary>
        public static ItemData[] ItemTable { get; }

        public static int[] HeightTable { get; }

        private static readonly byte[] m_StringBuffer = new byte[20];

        private static string ReadNameString(BinaryReader bin)
        {
            bin.Read(m_StringBuffer, 0, 20);

            int count;

            // This is very hackish... 
            for (count = 0; count < 20 && m_StringBuffer[count] != 0; ++count)
            {
            }

            return Encoding.ASCII.GetString(m_StringBuffer, 0, count);
        }

        private TileData()
        {
        }

        static TileData()
        {
            var filePath = Client.GetFilePath("tiledata.mul");

            if (filePath != null)
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var bin = new BinaryReader(fs);
                    var flag = bin.BaseStream.Length >= 0x30A800L;
                    LandTable = new LandData[0x4000];

                    for (var i = 0; i < 0x4000; ++i)
                    {
                        if ((i & 0x1F) == 0)
                        {
                            bin.ReadInt32(); // header
                        }

                        var flags = (TileFlag) bin.ReadInt32();
                        if (flag)
                        {
                            bin.ReadInt32();
                        }

                        bin.ReadInt16(); // skip 2 bytes -- textureID

                        LandTable[i] = new LandData(ReadNameString(bin), flags);
                    }

                    ItemTable = new ItemData[0x10000];
                    HeightTable = new int[0x10000];

                    for (var i = 0; i < 0x10000; ++i)
                    {
                        if ((i & 0x1F) == 0 && bin.BaseStream.Length - bin.BaseStream.Position >= 4L)
                        {
                            bin.ReadInt32(); // header
                        }

                        var flags = (TileFlag) bin.ReadInt32();
                        if (flag)
                        {
                            bin.ReadInt32();
                        }

                        int weight = bin.ReadByte();
                        int quality = bin.ReadByte();
                        bin.ReadInt16();
                        bin.ReadByte();
                        int quantity = bin.ReadByte();
                        int anim = bin.ReadInt16();
                        bin.ReadInt16();
                        bin.ReadByte();
                        int value = bin.ReadByte();
                        int height = bin.ReadByte();

                        ItemTable[i] = new ItemData(ReadNameString(bin), flags, weight, quality, quantity, value,
                            height, anim);
                        HeightTable[i] = height;
                    }
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}