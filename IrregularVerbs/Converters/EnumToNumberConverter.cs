using System;
using System.Globalization;
using System.Windows.Data;

namespace IrregularVerbs.Converters;

public class EnumToNumberConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum)
        {
            Type type = value.GetType();
            
            throw new NotImplementedException();
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}