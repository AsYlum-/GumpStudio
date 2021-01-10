using System;

namespace GumpStudio.Helpers
{
    public static class HexHelper
    {
        public static int HexToDec(string value)
        {
            if (value.Length <= 2 || value.Length > 6 || !string.Equals(value.Substring(0, 2), "0X", StringComparison.OrdinalIgnoreCase))
            {
                return 0;
            }

            return Convert.ToInt32(value, 16);
        }
    }
}
