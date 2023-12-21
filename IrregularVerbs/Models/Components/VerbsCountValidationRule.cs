using System.Globalization;
using System.Windows.Controls;

namespace IrregularVerbs.Models.Components;

public class VerbsCountValidationRule : ValidationRule
{
    private const string CommonErrorMessage = "The number of verbs must be an integer greater than 0 and less than 1000";
    
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        try
        {
            if (value == null)
            {
                return new ValidationResult(false, CommonErrorMessage);
            }

            int verbsCount = int.Parse((string)value);

            if (verbsCount <= 0 || verbsCount >= 1000)
            {
                return new ValidationResult(false, CommonErrorMessage);
            }
        }
        catch
        {
            return new ValidationResult(false, CommonErrorMessage);
        }

        return new ValidationResult(true, null);
    }
}