using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Controls;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;
using IrregularVerbs.Services.Main;
using Microsoft.Extensions.DependencyInjection;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace IrregularVerbs.Models.Components;

public class VerbsCountValidationRule : ValidationRule
{
    private const string ErrorMessagePattern = "Enter a number from 1 to {0} inclusive";
    
    private readonly ApplicationSettings _appSettings = App.Instance.AppSettings;
    private readonly IrregularVerbsStorage _verbsStorage = App.Instance.Services.GetRequiredService<IrregularVerbsStorage>();
    
    private string ErrorMessage => string.Format(ErrorMessagePattern, _verbsStorage.IrregularVerbs.Count);
    
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        try
        {
            if (value == null)
            {
                return Fail();
            }

            if (int.TryParse((string)value, out int verbsCount))
            {
                if (ValidateByCondition(verbsCount))
                {
                    return ValidateByAttribute(verbsCount) ? Success() : Fail();
                }
            }
            
            return Fail();
        }
        catch
        {
            return Fail();
        }
    }
    
    private bool ValidateByCondition(int verbsCount)
    {
        return verbsCount > 0 && verbsCount <= _verbsStorage.IrregularVerbs.Count;
    }
    
    private bool ValidateByAttribute(int verbsCount)
    {
        var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        
        ValidationContext verbsCountValidationContext = new ValidationContext(_appSettings)
        {
            MemberName = nameof(_appSettings.VerbsCount)
        };
                    
        return Validator.TryValidateProperty(verbsCount, verbsCountValidationContext, validationResults);
    }
    
    private ValidationResult Success()
    {
        return new ValidationResult(true, null);
    }
    
    private ValidationResult Fail()
    {
        return new ValidationResult(false, ErrorMessage);
    }
}