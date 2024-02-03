using System;
using System.Globalization;
using System.Windows.Data;
using IrregularVerbs.Models.Answers;

namespace IrregularVerbs.Converters;

public class AnswerResultToInfoConverter : IValueConverter
{
    private const string InputClassNameMismatch = "Value must be an IrregularVerbs.Models.Answers.AnswerResult";
    private const string OutputClassNameMismatch = "Value must be a System.String";

    private const string MoreInfo = "More info";
    
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
                    return MoreInfo;
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
                
                case MoreInfo:
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