using System;
using System.Globalization;
using System.Windows.Data;

namespace IrregularVerbs.Presentation.Converters;

public class EnumToInt32Converter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null ? (int)value : default;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null ? Enum.ToObject(targetType, value) : Enum.ToObject(targetType, 0);
    }
}