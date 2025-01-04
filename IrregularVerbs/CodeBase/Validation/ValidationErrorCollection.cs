using System.Collections.Generic;
using System.Linq;

namespace IrregularVerbs.CodeBase.Validation;

public class ValidationErrorCollection
{
    private readonly List<ValidationError> _errors = new List<ValidationError>();
    
    public bool HasErrors => _errors.Any();
    
    public IEnumerable<string> GetErrors(string propertyName)
    {
        return _errors
            .Where(error => error.PropertyName == propertyName)
            .Select(error => error.ErrorMessage);
    }
    
    public void AddError(ValidationError validationError)
    {
        _errors.Add(validationError);
    }
    
    public bool RemoveError(ValidationError validationError)
    {
        return _errors.Remove(validationError);
    }

    public void Clear()
    {
        _errors.Clear();
    }
    
}