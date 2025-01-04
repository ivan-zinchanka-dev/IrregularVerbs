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
    public event Action<string> OnPropertyChanged;
    
    // TODO Reactive property?
    
    public Language NativeLanguage
    {
        get => _nativeLanguage;
        set
        {
            _nativeLanguage = value;
            OnPropertyChanged?.Invoke(nameof(NativeLanguage));
        }
    }
    
    public int VerbsCount
    {
        get => _verbsCount;
        set
        {
            _verbsCount = value;
            OnPropertyChanged?.Invoke(nameof(VerbsCount));
        }
    }
    
    public bool AlphabeticalOrder
    {
        get => _alphabeticalOrder;
        set
        {
            _alphabeticalOrder = value;
            OnPropertyChanged?.Invoke(nameof(AlphabeticalOrder));
        }
    }

    public bool EnableToolTips
    {
        get => _enableToolTips;
        set
        {
            _enableToolTips = value;
            OnPropertyChanged?.Invoke(nameof(EnableToolTips));
        }
    }
    
    public bool DarkTheme
    {
        get => _darkTheme;
        set
        {
            _darkTheme = value;
            OnPropertyChanged?.Invoke(nameof(DarkTheme));
        }
    }
}