namespace IrregularVerbs.Domain.Services.Localization;

public class LocalizationException : Exception
{
    private const string MessagePattern = "An localization exception occured. {0}";
    
    public LocalizationException(string message) 
        : base(string.Format(MessagePattern, message)) { }

    public LocalizationException(string message, Exception innerException) : 
        base(string.Format(MessagePattern, message), innerException) { }
}