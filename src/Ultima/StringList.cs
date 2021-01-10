using System.Collections;
using System.IO;
using System.Text;

namespace Ultima
{
    public class StringList
    {
        public StringEntry[] Entries { get; }

        public Hashtable Table { get; }

        public string Language { get; }

        private static byte[] m_Buffer = new byte[1024];

        public StringList(string language)
        {
            Language = language;
            Table = new Hashtable();

            var path = Client.GetFilePath($"cliloc.{language}");

            if (path == null)
            {
                Entries = new StringEntry[0];
                return;
            }

            var list = new ArrayList();

            using (var bin = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                bin.ReadInt32();
                bin.ReadInt16();

                while (bin.BaseStream.Length != bin.BaseStream.Position)
                {
                    var number = bin.ReadInt32();
                    bin.ReadByte();
                    int length = bin.ReadInt16();

                    if (length > m_Buffer.Length)
                    {
                        m_Buffer = new byte[(length + 1023) & ~1023];
                    }

                    bin.Read(m_Buffer, 0, length);
                    var text = Encoding.UTF8.GetString(m_Buffer, 0, length);

                    list.Add(new StringEntry(number, text));
                    Table[number] = text;
                }
            }

            Entries = (StringEntry[]) list.ToArray(typeof(StringEntry));
        }
    }
}