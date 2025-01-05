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
                        new SolidColorBrush(Color.FromRgb(50, 205, 50)), 
                        new SolidColorBrush(Color.FromRgb(144, 238, 144)));    
                //return GetBrushByCurrentTheme(Brushes.LimeGreen, Brushes.LightGreen);
                
                case AnswerResult.Incorrect:
                    return GetBrushByCurrentTheme(
                        new SolidColorBrush(Color.FromRgb(255, 0, 0)),
                        new SolidColorBrush(Color.FromRgb(252, 62, 56)));  
                    /*return GetBrushByCurrentTheme(Brushes.Red, Brushes.Salmon);*/
                
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