namespace IrregularVerbs.Models.Configs;

public class ApplicationSettingsValidator
{
    private const string VerbsCountErrorMessagePattern = "Enter a number from 1 to {0} inclusive";

    public int MaxVerbsCount { get; set; } = 1000;
    public string VerbsCountErrorMessage => string.Format(VerbsCountErrorMessagePattern, MaxVerbsCount);
        
    public bool ValidateVerbsCount(int verbsCount)
    {
        return verbsCount > 0 && verbsCount <= MaxVerbsCount;
    }
}