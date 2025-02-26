using System;
using System.Globalization;
using System.Windows.Data;
using IrregularVerbs.Domain.Models.Answers;

namespace IrregularVerbs.Presentation.Converters;

public class AnswerResultToInfoTextConverter : IValueConverter
{
    private const string InputClassNameMismatch = "Value must be an IrregularVerbs.Models.Answers.AnswerResult";
    private const string OutputClassNameMismatch = "Value must be a System.String";

    private const string MoreInfoText = "More info";
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is AnswerResult result)
        {
            switch (result)
            {
                case AnswerResult.Correct:
                case AnswerResult.None: 
                default:
                    return string.Empty;
                
                case AnswerResult.Incorrect:
                    return MoreInfoText;
            }
        }
        else
        {
            throw new InvalidOperationException(InputClassNameMismatch);
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string text)
        {
            switch (text)
            {
                case "":
                     return AnswerResult.Correct;
                
                case MoreInfoText:
                    return AnswerResult.Incorrect;
                
                 default:
                     return AnswerResult.None;
            }
        }
        else
        {
            throw new InvalidOperationException(OutputClassNameMismatch);
        }
    }
}