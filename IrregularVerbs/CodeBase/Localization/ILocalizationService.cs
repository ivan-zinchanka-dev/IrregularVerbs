using System.Collections.Generic;

namespace IrregularVerbs.CodeBase.Localization;

public interface ILocalizationService
{
    public IReadOnlyCollection<string> Languages { get; }
    public string CurrentLanguage { get; }

    public string Localize(string term);
    public bool TryLocalize(string term, out string result);
}