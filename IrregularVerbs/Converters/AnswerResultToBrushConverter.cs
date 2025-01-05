using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using IrregularVerbs.CodeBase.ThemeManagement;
using IrregularVerbs.Models.Answers;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace IrregularVerbs.Converters;

public class AnswerResultToBrushConverter : IValueConverter
{
    private static class ResultColors
    {
        public static readonly Color CorrectLight = Color.FromRgb(50, 200, 50);
        public static readonly Color CorrectDark = Color.FromRgb(144, 238, 144);
        public static readonly Color IncorrectLight = Color.FromRgb(255, 0, 0);
        public static readonly Color IncorrectDark = Color.FromRgb(250, 128, 114);
    }
    
    private const string InputClassNameMismatch = "Value must be an IrregularVerbs.Models.Answers.AnswerResult";
    private const string OutputClassNameMismatch = "Value must be a System.Windows.Media.SolidColorBrush";
    
    private readonly ThemeManager _themeManager = App.Instance.Services.GetRequiredService<ThemeManager>();
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is AnswerResult result)
        {
            switch (result)
            {
                case AnswerResult.Correct:
                    return GetBrushByCurrentTheme(
                        new SolidColorBrush(ResultColors.CorrectLight), 
                        new SolidColorBrush(ResultColors.CorrectDark));    
                
                case AnswerResult.Incorrect:
                    return GetBrushByCurrentTheme(
                        new SolidColorBrush(ResultColors.IncorrectLight),
                        new SolidColorBrush(ResultColors.IncorrectDark));  
                
                case AnswerResult.None: 
                default:
                    return Brushes.Transparent;
            }
        }
        else
        {
            throw new InvalidOperationException(InputClassNameMismatch);
        }
    }

    private SolidColorBrush GetBrushByCurrentTheme(SolidColorBrush lightBrush, SolidColorBrush darkBrush)
    {
        return _themeManager.CurrentBaseTheme == BaseTheme.Dark ? darkBrush : lightBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            if (brush.Color == ResultColors.CorrectLight || brush.Color == ResultColors.CorrectDark)
            {
                return AnswerResult.Correct;
            }
            else if (brush.Color == ResultColors.IncorrectLight || brush.Color == ResultColors.IncorrectDark)
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