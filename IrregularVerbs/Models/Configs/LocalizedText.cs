using IrregularVerbs.Services.Localization;

namespace IrregularVerbs.Models.Configs;

public class LocalizedText
{
    private readonly string _term;
    private readonly LocalizationService _localizationService;

    public string Term => _term;
    
    public LocalizedText(string term, LocalizationService localizationService)
    {
        _term = term;
        _localizationService = localizationService;
    }

    public override string ToString()
    {
        return _localizationService.Localize(_term);
    }
    
    public static bool operator==(LocalizedText left, LocalizedText right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return false;
        }

        return left._term == right._term;
    }

    public static bool operator!=(LocalizedText left, LocalizedText right)
    {
        return !(left == right);
    }
}