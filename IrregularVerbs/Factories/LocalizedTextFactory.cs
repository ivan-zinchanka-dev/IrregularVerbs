﻿using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;

namespace IrregularVerbs.Factories;

public class LocalizedTextFactory
{
    private readonly LocalizationService _localizationService;

    public LocalizedTextFactory(LocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public LocalizedText Create(string term)
    {
        return new LocalizedText(term, _localizationService);
    }
}