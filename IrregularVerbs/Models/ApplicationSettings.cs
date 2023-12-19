using System;
using IrregularVerbs.Services;

namespace IrregularVerbs.Models;

public class ApplicationSettings
{
    private Language _nativeLanguage;
    private int _verbsCount;
    private bool _alphabeticalOrder;

    public Language NativeLanguage
    {
        get => _nativeLanguage;
        set
        {
            _nativeLanguage = value;
            OnDemandSave?.Invoke();
        }
    }
    
    public int VerbsCount
    {
        get => _verbsCount;
        set
        {
            _verbsCount = value;
            OnDemandSave?.Invoke();
        }
    }
    public bool AlphabeticalOrder
    {
        get => _alphabeticalOrder;
        set
        {
            _alphabeticalOrder = value;
            OnDemandSave?.Invoke();
        }
    }
    
    public event Action OnDemandSave;
}