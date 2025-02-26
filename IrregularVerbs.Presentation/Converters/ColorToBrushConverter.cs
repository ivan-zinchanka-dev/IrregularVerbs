using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace IrregularVerbs.Presentation.Converters;

public class ColorToBrushConverter : IValueConverter {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

        if (value is Color color)
        {
            return new SolidColorBrush(color);
        }
        else
        {
            throw new InvalidOperationException("Value must be a System.Windows.Media.Color");
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        
        if (value is SolidColorBrush solidColorBrush)
        {
            return solidColorBrush.Color;
        }
        else
        {
            throw new InvalidOperationException("Value must be a System.Windows.Media.SolidColorBrush");
        }
    }

}