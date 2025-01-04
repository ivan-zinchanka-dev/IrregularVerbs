using System;

namespace IrregularVerbs.Models.Configs;

public class ApplicationSettings
{
    private Language _nativeLanguage;
    private int _verbsCount;
    private bool _alphabeticalOrder;
    private bool _enableToolTips;
    private bool _darkTheme;
    
    public ApplicationSettingsValidator Validator { get; private set; } = new ApplicationSettingsValidator();
    public event Action OnPropertyChanged;
    
    public Language NativeLanguage
    {
        get => _nativeLanguage;
        set
        {
            _nativeLanguage = value;
            OnPropertyChanged?.Invoke();
        }
    }
    
    public int VerbsCount
    {
        get => _verbsCount;
        set
        {
            _verbsCount = value;
            OnPropertyChanged?.Invoke();
        }
    }
    
    public bool AlphabeticalOrder
    {
        get => _alphabeticalOrder;
        set
        {
            _alphabeticalOrder = value;
            OnPropertyChanged?.Invoke();
        }
    }

    public bool EnableToolTips
    {
        get => _enableToolTips;
        set
        {
            _enableToolTips = value;
            OnPropertyChanged?.Invoke();
        }
    }
    
    public bool DarkTheme
    {
        get => _darkTheme;
        set
        {
            _darkTheme = value;
            OnPropertyChanged?.Invoke();
        }
    }
}