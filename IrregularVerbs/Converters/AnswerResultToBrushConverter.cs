﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using IrregularVerbs.Models.Answers;

namespace IrregularVerbs.Converters;

public class AnswerResultToBrushConverter : IValueConverter
{
    private const string InputClassNameMismatch = "Value must be an IrregularVerbs.Models.Answers.AnswerResult";
    private const string OutputClassNameMismatch = "Value must be a System.Windows.Media.SolidColorBrush";
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is AnswerResult result)
        {
            switch (result)
            {
                case AnswerResult.Correct:
                    return Brushes.LightGreen;
                
                case AnswerResult.Incorrect:
                    return Brushes.LightCoral;
                
                case AnswerResult.None: 
                default:
                    return Brushes.WhiteSmoke;
            }
        }
        else
        {
            throw new InvalidOperationException(InputClassNameMismatch);
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            if (brush.Equals(Brushes.LightGreen))
            {
                return AnswerResult.Correct;
            }
            else if (brush.Equals(Brushes.LightCoral))
            {
                return AnswerResult.Incorrect;
            }
            else
            {
                return AnswerResult.None;
            }
        }
        else
        {
            throw new InvalidOperationException(OutputClassNameMismatch);
        }
    }
}