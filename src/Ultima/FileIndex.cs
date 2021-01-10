using System.IO;

namespace Ultima
{
    public class FileIndex
    {
        public Entry3D[] Index { get; }

        public Stream Stream { get; private set; }

        public string IndexPath { get; }

        public string MulPath { get; }

        public Stream Seek(int index, out int length, out int extra, out bool patched)
        {
            if (index < 0 || index >= Index.Length)
            {
                length = extra = 0;
                patched = false;
                return null;
            }

            var e = Index[index];

            if (e.Lookup < 0)
            {
                length = extra = 0;
                patched = false;
                return null;
            }

            length = e.Length & 0x7FFFFFFF;
            extra = e.Extra;

            if ((e.Length & (1 << 31)) != 0)
            {
                patched = true;

                Verdata.Stream.Seek(e.Lookup, SeekOrigin.Begin);
                return Verdata.Stream;
            }

            if (Stream == null)
            {
                length = extra = 0;
                patched = false;
                return null;
            }

            patched = false;

            InvalidateFileStream();

            Stream.Seek(e.Lookup, SeekOrigin.Begin);
            return Stream;
        }

        private void InvalidateFileStream()
        {
            if (Stream?.CanRead != true || !Stream.CanSeek)
            {
                Stream = new FileStream(MulPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
        }

        public FileIndex(string idxFile, string mulFile, int length, int file)
        {
            Index = new Entry3D[length];

            IndexPath = Client.GetFilePath(idxFile);
            MulPath = Client.GetFilePath(mulFile);

            if (IndexPath != null && MulPath != null)
            {
                using (var index = new FileStream(IndexPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var bin = new BinaryReader(index);
                    Stream = new FileStream(MulPath, FileMode.Open, FileAccess.Read, FileShare.Read);

                    var count = (int) (index.Length / 12);

                    for (var i = 0; i < count && i < length; ++i)
                    {
                        Index[i].Lookup = bin.ReadInt32();
                        Index[i].Length = bin.ReadInt32();
                        Index[i].Extra = bin.ReadInt32();
                    }

                    for (var i = count; i < length; ++i)
                    {
                        Index[i].Lookup = -1;
                        Index[i].Length = -1;
                        Index[i].Extra = -1;
                    }
                }
            }

            var patches = Verdata.Patches;

            for (var i = 0; i < patches.Length; ++i)
            {
                var patch = patches[i];

                if (patch.file == file && patch.index >= 0 && patch.index < length)
                {
                    Index[patch.index].Lookup = patch.lookup;
                    Index[patch.index].Length = patch.length | (1 << 31);
                    Index[patch.index].Extra = patch.extra;
                }
            }
        }

        public bool Valid(int index, out int length, out int extra, out bool patched)
        {
            if (index < 0 || index >= Index.Length)
            {
                length = extra = 0;
                patched = false;
                return false;
            }

            var e = Index[index];

            if (e.Lookup < 0)
            {
                length = extra = 0;
                patched = false;
                return false;
            }

            length = e.Length & 0x7FFFFFFF;
            extra = e.Extra;

            if ((e.Length & (1 << 31)) != 0)
            {
                patched = true;
                return true;
            }

            if (e.Length < 0)
            {
                length = extra = 0;
                patched = false;
                return false;
            }

            if (MulPath == null || !File.Exists(MulPath))
            {
                length = extra = 0;
                patched = false;
                return false;
            }

            if (Stream?.CanRead != true || !Stream.CanSeek)
            {
                Stream = new FileStream(MulPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }

            if (Stream.Length < e.Lookup)
            {
                length = extra = 0;
                patched = false;
                return false;
            }

            patched = false;

            return true;
        }
    }

    public struct Entry3D
    {
        public int Lookup;
        public int Length;
        public int Extra;
    }
}