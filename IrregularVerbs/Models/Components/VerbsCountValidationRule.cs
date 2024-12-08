using System.Globalization;
using System.Windows.Controls;
using IrregularVerbs.Models.Configs;

namespace IrregularVerbs.Models.Components;

public class VerbsCountValidationRule : ValidationRule
{
    private readonly ApplicationSettings _applicationSettings = App.Instance.AppSettings;
    
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        ApplicationSettingsValidator validator = _applicationSettings.Validator;
        
        try
        {
            if (value == null)
            {
                return new ValidationResult(false, validator.VerbsCountErrorMessage);
            }

            int verbsCount = int.Parse((string)value);

            if (validator.ValidateVerbsCount(verbsCount))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, validator.VerbsCountErrorMessage);
            }
        }
        catch
        {
            return new ValidationResult(false, validator.VerbsCountErrorMessage);
        }
    }
}