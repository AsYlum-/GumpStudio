using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Ultima;

namespace UOFont
{
    public static class Fonts
    {
        private class CharInfo
        {
            public int Height;
            public int Width;
            public long Offset;
            public Bitmap Cache;

            public override string ToString()
            {
                return $"{Width} x {Height} offset: {Offset}";
            }
        }

        private class FontSet
        {
            public readonly CharInfo[] CharacterInfo;

            public FontSet()
            {
                CharacterInfo = new CharInfo[225];
            }
        }

        private static readonly FontSet[] MFontSet = new FontSet[10];

        private static FileStream _stream;

        private static void Init()
        {
            _stream = new FileStream(Client.GetFilePath("fonts.mul"), FileMode.Open, FileAccess.Read);

            int fontSetIndex = 0;

            do
            {
                FontSet fontSet = new FontSet();
                MFontSet[fontSetIndex] = fontSet;

                _stream.ReadByte(); // TODO: do we need variable here?

                int fontCharIndex = 0;
                do
                {
                    CharInfo charInfo = new CharInfo
                    {
                        Width = _stream.ReadByte(),
                        Height = _stream.ReadByte()
                    };

                    _stream.ReadByte(); // TODO: do we need variable here?

                    charInfo.Offset = _stream.Position;
                    fontSet.CharacterInfo[fontCharIndex] = charInfo;

                    int offset = charInfo.Width * charInfo.Height * 2;
                    _stream.Seek(offset, SeekOrigin.Current);

                    fontCharIndex++;
                }
                while (fontCharIndex <= 223); // TODO: why 223???

                fontSetIndex++;
            }
            while (fontSetIndex <= 9);
        }

        public static Bitmap GetCharImage(int font, char character)
        {
            if (_stream == null)
            {
                Init();
            }

            byte b = Encoding.GetEncoding(1251).GetBytes(new string(character, 1))[0];

            CharInfo charInfo = MFontSet[font].CharacterInfo[b - 32];
            if (charInfo.Cache != null)
            {
                return charInfo.Cache;
            }

            _stream.Seek(charInfo.Offset, SeekOrigin.Begin); // TODO: make sure streams are not null!

            Bitmap bitmap = new Bitmap(charInfo.Width, charInfo.Height, PixelFormat.Format32bppArgb);

            int charInfoHeight = charInfo.Height - 1;
            for (int i = 0; i <= charInfoHeight; i++)
            {
                int charInfoWidth = charInfo.Width - 1;
                for (int j = 0; j <= charInfoWidth; j++)
                {
                    short color = (short)(_stream.ReadByte() + (_stream.ReadByte() << 8) - 1);
                    bitmap.SetPixel(j, i, Convert555ToArgb(color));
                }
            }
            charInfo.Cache = bitmap;

            return bitmap;
        }

        public static Bitmap GetStringImage(int font, string text)
        {
            if (_stream == null)
            {
                Init();
            }

            var bitmapArray = new Bitmap[text.Length];

            int width = 0;
            int maxCharacterHeight = 0;

            for (int i = 0; i < text.Length; i++)
            {
                bitmapArray[i] = GetCharImage(font, text[i]);
                width += bitmapArray[i].Width;

                if (bitmapArray[i].Height > maxCharacterHeight)
                {
                    maxCharacterHeight = bitmapArray[i].Height;
                }
            }

            Bitmap bitmap = new Bitmap(width, maxCharacterHeight, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                int xPos = 0;
                for (int j = 0; j < text.Length; j++)
                {
                    graphics.DrawImage(bitmapArray[j], xPos, maxCharacterHeight - bitmapArray[j].Height);
                    xPos += bitmapArray[j].Width;
                }
            }

            bitmap.MakeTransparent();

            return bitmap;
        }

        private static Color Convert555ToArgb(short color)
        {
            int red = ((short)(color >> 10) & 0x1F) * 8;
            int green = ((short)(color >> 5) & 0x1F) * 8;
            int blue = (color & 0x1F) * 8;

            return Color.FromArgb(red, green, blue);
        }
    }
}