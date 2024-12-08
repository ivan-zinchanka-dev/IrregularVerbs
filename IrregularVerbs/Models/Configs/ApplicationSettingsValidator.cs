namespace IrregularVerbs.Models.Configs;

public class ApplicationSettingsValidator
{
    private const string VerbsCountErrorMessagePattern = 
        "The number of verbs must be an integer greater than 0 and less than {0}";

    public int MaxVerbsCount { get; set; } = 1000;
    public string VerbsCountErrorMessage => string.Format(VerbsCountErrorMessagePattern, MaxVerbsCount + 1);
        
    public bool ValidateVerbsCount(int verbsCount)
    {
        return verbsCount > 0 && verbsCount <= MaxVerbsCount;
    }
}