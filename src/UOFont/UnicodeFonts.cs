using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Ultima;

namespace UOFont
{
    public static class UnicodeFonts
    {
        private class CharInfo
        {
            public char Char;
            public sbyte XOffset;
            public sbyte YOffset;
            public int Width;
            public int Height;
            //public long Offset;
            //public Bitmap Cache;

            public override string ToString()
            {
                return Char.ToString();
            }
        }

        //private class FontSet
        //{
        //    private CharInfo[] cinfo;

        //    public FontSet()
        //    {
        //        cinfo = new CharInfo[65537];
        //    }
        //}

        private static readonly CharInfo[] UniCache;

        private static readonly FileStream[] FileStreams;

        private static readonly BinaryReader[] BinaryReaders;

        private static FileStream _fileStream;

        static UnicodeFonts()
        {
            UniCache = new CharInfo[1120];
            FileStreams = new FileStream[7];
            BinaryReaders = new BinaryReader[7];
        }

        public static void Init()
        {
            FileStreams[0] = new FileStream(Client.GetFilePath("unifont.mul"), FileMode.Open, FileAccess.Read);
            FileStreams[1] = new FileStream(Client.GetFilePath("unifont1.mul"), FileMode.Open, FileAccess.Read);
            FileStreams[2] = new FileStream(Client.GetFilePath("unifont2.mul"), FileMode.Open, FileAccess.Read);
            FileStreams[3] = new FileStream(Client.GetFilePath("unifont3.mul"), FileMode.Open, FileAccess.Read);
            FileStreams[4] = new FileStream(Client.GetFilePath("unifont4.mul"), FileMode.Open, FileAccess.Read);
            FileStreams[5] = new FileStream(Client.GetFilePath("unifont5.mul"), FileMode.Open, FileAccess.Read);
            FileStreams[6] = new FileStream(Client.GetFilePath("unifont6.mul"), FileMode.Open, FileAccess.Read);
            //FileStreams[7] = new FileStream(Client.GetFilePath("unifont7.mul"), FileMode.Open, FileAccess.Read);
            //FileStreams[8] = new FileStream(Client.GetFilePath("unifont8.mul"), FileMode.Open, FileAccess.Read);
            //FileStreams[9] = new FileStream(Client.GetFilePath("unifont9.mul"), FileMode.Open, FileAccess.Read);
            //FileStreams[10] = new FileStream(Client.GetFilePath("unifont10.mul"), FileMode.Open, FileAccess.Read);
            //FileStreams[11] = new FileStream(Client.GetFilePath("unifont11.mul"), FileMode.Open, FileAccess.Read);
            //FileStreams[12] = new FileStream(Client.GetFilePath("unifont12.mul"), FileMode.Open, FileAccess.Read);

            BinaryReaders[0] = new BinaryReader(FileStreams[0]);
            BinaryReaders[1] = new BinaryReader(FileStreams[1]);
            BinaryReaders[2] = new BinaryReader(FileStreams[2]);
            BinaryReaders[3] = new BinaryReader(FileStreams[3]);
            BinaryReaders[4] = new BinaryReader(FileStreams[4]);
            BinaryReaders[5] = new BinaryReader(FileStreams[5]);
            BinaryReaders[6] = new BinaryReader(FileStreams[6]);
            //BinaryReaders[7] = new BinaryReader(FileStreams[7]);
            //BinaryReaders[8] = new BinaryReader(FileStreams[8]);
            //BinaryReaders[9] = new BinaryReader(FileStreams[9]);
            //BinaryReaders[10] = new BinaryReader(FileStreams[10]);
            //BinaryReaders[11] = new BinaryReader(FileStreams[11]);
            //BinaryReaders[12] = new BinaryReader(FileStreams[12]);
        }

        public static Bitmap GetCharImage(int font, char character)
        {
            if (_fileStream == null)
            {
                Init();
            }

            if (font > 6)
            {
                return new Bitmap(1, 1);
            }

            _fileStream = FileStreams[font]; // TODO: check indexes and off by 1 error
            var binaryReader = BinaryReaders[font]; // TODO: check indexes and off by 1 error

            _fileStream.Seek(character * sizeof(int), SeekOrigin.Begin);

            int charPositionOffset = binaryReader.ReadInt32();
            _fileStream.Seek(charPositionOffset, SeekOrigin.Begin);

            var charInfo = new CharInfo
            {
                Char = character,
                XOffset = binaryReader.ReadSByte(),
                YOffset = binaryReader.ReadSByte(),
                Width = binaryReader.ReadByte(),
                Height = binaryReader.ReadByte()
            };

            //_ = charInfo.Height - charInfo.YOffset;
            UniCache[character] = charInfo; // TODO: do we need that?

            Bitmap bitmap;
            if (charInfo.Width + charInfo.Height != 0)
            {
                bitmap = new Bitmap(charInfo.Width + (charInfo.XOffset * 2) + 2, charInfo.Height + charInfo.YOffset + 2, PixelFormat.Format32bppArgb);

                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.Red);
                }

                for (int i = 0; i < charInfo.Height; i++)
                {
                    byte b = 0;
                    for (int j = 0; j < charInfo.Width; j++)
                    {
                        unchecked
                        {
                            int num4 = j % 8;
                            if (num4 == 0)
                            {
                                b = binaryReader.ReadByte();
                            }
                            if (((byte)((uint)b >> (checked(7 - num4) & 7)) & 1) == 1)
                            {
                                checked
                                {
                                    bitmap.SetPixel(j + charInfo.XOffset + 1, i + charInfo.YOffset + 1, Color.LightGray);
                                }
                            }
                        }
                    }
                }

                for (int k = 0; k < bitmap.Width; k++)
                {
                    for (int l = 0; l < bitmap.Height; l++)
                    {
                        if (bitmap.GetPixel(k, l).ToArgb() != -65536)
                        {
                            continue;
                        }

                        bool flag = false;
                        if (k < bitmap.Width - 1 && bitmap.GetPixel(k + 1, l).ToArgb() == -2894893)
                        {
                            bitmap.SetPixel(k, l, Color.Black);
                            flag = true;
                        }

                        if (!flag && k > 0 && bitmap.GetPixel(k - 1, l).ToArgb() == -2894893)
                        {
                            bitmap.SetPixel(k, l, Color.Black);
                            flag = true;
                        }

                        if (!flag && l < bitmap.Height - 1 && bitmap.GetPixel(k, l + 1).ToArgb() == -2894893)
                        {
                            bitmap.SetPixel(k, l, Color.Black);
                            flag = true;
                        }

                        if (!flag && l > 0 && bitmap.GetPixel(k, l - 1).ToArgb() == -2894893)
                        {
                            bitmap.SetPixel(k, l, Color.Black);
                        }
                    }
                }
            }
            else
            {
                bitmap = new Bitmap(3, 1, PixelFormat.Format32bppArgb);
                bitmap.MakeTransparent();
            }

            bitmap.MakeTransparent(Color.Red);

            //charInfo.Cache = bitmap;

            return bitmap;
        }

        public static Bitmap GetStringImage(int font, string text)
        {
            var bitmapArray = new Bitmap[text.Length];

            int width = 0;
            int maxHeight = 0;

            for (int i = 0; i < text.Length; i++)
            {
                bitmapArray[i] = GetCharImage(font, text[i]);
                width += bitmapArray[i].Width;
                if (bitmapArray[i].Height > maxHeight)
                {
                    maxHeight = bitmapArray[i].Height;
                }
            }

            var bitmap = new Bitmap(width, maxHeight, PixelFormat.Format32bppArgb);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                width = default;
                for (int j = 0; j < text.Length; j++)
                {
                    graphics.DrawImage(bitmapArray[j], width, 0);
                    width += bitmapArray[j].Width;
                }

                for (int k = 0; k < text.Length; k++)
                {
                    bitmapArray[k].Dispose();
                }
            }

            return bitmap;
        }
    }
}