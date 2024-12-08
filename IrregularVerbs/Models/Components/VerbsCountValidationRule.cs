using System.Globalization;
using System.Windows.Controls;
using IrregularVerbs.Models.Configs;

namespace IrregularVerbs.Models.Components;

public class VerbsCountValidationRule : ValidationRule
{
    private readonly ApplicationSettingsValidator _appSettingsValidator = App.Instance.AppSettings.Validator;
    
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        try
        {
            if (value == null)
            {
                return Fail();
            }

            int verbsCount = int.Parse((string)value);

            return _appSettingsValidator.ValidateVerbsCount(verbsCount) ? Success() : Fail();
        }
        catch
        {
            return Fail();
        }
    }

    private ValidationResult Success()
    {
        return new ValidationResult(true, null);
    }
    
    private ValidationResult Fail()
    {
        return new ValidationResult(false, _appSettingsValidator.VerbsCountErrorMessage);
    }
}