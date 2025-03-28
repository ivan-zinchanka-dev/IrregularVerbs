﻿namespace IrregularVerbs.Domain.Services.Localization;

public interface ILocalizationService
{
    public IReadOnlyCollection<string> Languages { get; }
    public string CurrentLanguage { get; set; }

    public string Localize(string term);
    public bool TryLocalize(string term, out string result);
}