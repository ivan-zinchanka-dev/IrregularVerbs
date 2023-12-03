using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using IrregularVerbs.Models;

namespace IrregularVerbs.Converters;

public class AnswerResultToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IrregularVerbAnswer.AnswerResult result)
        {
            switch (result)
            {
                case IrregularVerbAnswer.AnswerResult.Correct:
                    return Brushes.LightGreen;
                
                case IrregularVerbAnswer.AnswerResult.Incorrect:
                    return Brushes.LightCoral;
                
                case IrregularVerbAnswer.AnswerResult.None: 
                default:
                    return Brushes.WhiteSmoke;
            }
        }
        else
        {
            throw new InvalidOperationException("Value must be a System.Boolean");
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            return brush.Equals(Brushes.LightGreen);
        }
        else
        {
            throw new InvalidOperationException("Value must be a System.Windows.Media.SolidColorBrush");
        }
    }
}