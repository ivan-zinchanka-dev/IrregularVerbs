using IrregularVerbs.Domain.Services.Localization;

namespace IrregularVerbs.Domain.Factories;

public class LocalizedTextFactory
{
    private readonly ILocalizationService _localizationService;

    public LocalizedTextFactory(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public LocalizedText Create(string term)
    {
        return new LocalizedText(term, _localizationService);
    }
}