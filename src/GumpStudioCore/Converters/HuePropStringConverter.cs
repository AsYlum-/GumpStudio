using System;
using System.ComponentModel;
using System.Globalization;
using GumpStudio.Helpers;
using Ultima;

namespace GumpStudio.Converters
{
    public class HuePropStringConverter : StringConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(Hue);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string input = Convert.ToString(value);

            return int.TryParse(input, out int intValue)
                ? Hues.GetHue(intValue)
                : Hues.GetHue(HexHelper.HexToDec(input));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return value.ToString();
        }
    }
}
