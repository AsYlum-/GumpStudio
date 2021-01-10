using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

// ascii text support written by arul
namespace Ultima
{
    public sealed class ASCIIFont
    {
        public int Height { get; set; }

        public Bitmap[] Characters { get; set; }

        public ASCIIFont()
        {
            Height = 0;
            Characters = new Bitmap[224];
        }

        public Bitmap GetBitmap(char character)
        {
            return Characters[((character - 0x20) & 0x7FFFFFFF) % 224];
        }

        public int GetWidth(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            var width = 0;

            for (var i = 0; i < text.Length; ++i)
            {
                width += GetBitmap(text[i]).Width;
            }

            return width;
        }

        public static ASCIIFont GetFixed(int font)
        {
            if (font < 0 || font > 9)
            {
                return ASCIIText.Fonts[3];
            }

            return ASCIIText.Fonts[font];
        }
    }

    public static class ASCIIText
    {
        //QUERY: Does this really need to be exposed?
        public static ASCIIFont[] Fonts { get; set; } = new ASCIIFont[10];

        static ASCIIText()
        {
            var path = Client.GetFilePath("fonts.mul");

            if (path != null)
            {
                using (var reader = new BinaryReader(new FileStream(path, FileMode.Open)))
                {
                    for (var i = 0; i < 10; ++i)
                    {
                        Fonts[i] = new ASCIIFont();

                        reader.ReadByte(); //header

                        for (var k = 0; k < 224; ++k)
                        {
                            var width = reader.ReadByte();
                            var height = reader.ReadByte();
                            reader.ReadByte(); // delimeter?

                            if (width > 0 && height > 0)
                            {
                                if (height > Fonts[i].Height && k < 96)
                                {
                                    Fonts[i].Height = height;
                                }

                                var bmp = new Bitmap(width, height);

                                for (var y = 0; y < height; ++y)
                                {
                                    for (var x = 0; x < width; ++x)
                                {
                                    var pixel = (short) (reader.ReadByte() | (reader.ReadByte() << 8));

                                    if (pixel != 0)
                                        {
                                            bmp.SetPixel(x, y,
                                            Color.FromArgb((pixel & 0x7C00) >> 7, (pixel & 0x3E0) >> 2,
                                                (pixel & 0x1F) << 3));
                                        }
                                    }
                                }

                                Fonts[i].Characters[k] = bmp;
                            }
                        }
                    }
                }
            }
        }

        public static unsafe Bitmap DrawText(int fontId, string text, short hueId)
        {
            var font = ASCIIFont.GetFixed(fontId);

            var result =
                new Bitmap(font.GetWidth(text), font.Height);
            var surface =
                result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

            var dx = 0;

            for (var i = 0; i < text.Length; ++i)
            {
                var bmp =
                    font.GetBitmap(text[i]);
                var chr =
                    bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly,
                        PixelFormat.Format32bppArgb);

                for (var dy = 0; dy < chr.Height; ++dy)
                {
                    var src =
                        (int*) chr.Scan0 + chr.Stride * dy;
                    var dest =
                        (int*) surface.Scan0 + surface.Stride * (dy + (font.Height - chr.Height)) + (dx << 2);

                    for (var k = 0; k < chr.Width; ++k)
                    {
                        *dest++ = *src++;
                    }
                }

                dx += chr.Width;
                bmp.UnlockBits(chr);
            }

            result.UnlockBits(surface);

            hueId = (short) ((hueId & 0x3FFF) - 1);
            if (hueId >= 0 && hueId < Hues.List.Length)
            {
                var hueObject = Hues.List[hueId];

                hueObject?.ApplyTo(result, (hueId & 0x8000) == 0);
            }

            return result;
        }
    }
}