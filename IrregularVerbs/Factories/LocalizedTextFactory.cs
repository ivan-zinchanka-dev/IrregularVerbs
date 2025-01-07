using IrregularVerbs.CodeBase.Localization;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services.Localization;

namespace IrregularVerbs.Factories;

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