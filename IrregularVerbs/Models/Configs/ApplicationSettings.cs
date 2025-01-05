using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IrregularVerbs.Models.Configs;

public class ApplicationSettings : INotifyPropertyChanged
{
    private Language _nativeLanguage;
    private int _verbsCount;
    private bool _alphabeticalOrder;
    private bool _enableToolTips;
    private bool _darkTheme;
    
    public ApplicationSettingsValidator Validator { get; private set; } = new ApplicationSettingsValidator();
    
    // TODO Better INotifyPropertyChanged, (INotifyDataErrorInfo) ?
    
    public Language NativeLanguage
    {
        get => _nativeLanguage;
        set
        {
            _nativeLanguage = value;
            OnPropertyChanged();
        }
    }
    
    public int VerbsCount
    {
        get => _verbsCount;
        set
        {
            _verbsCount = value;
            OnPropertyChanged();
        }
    }
    
    public bool AlphabeticalOrder
    {
        get => _alphabeticalOrder;
        set
        {
            _alphabeticalOrder = value;
            OnPropertyChanged();
        }
    }

    public bool EnableToolTips
    {
        get => _enableToolTips;
        set
        {
            _enableToolTips = value;
            OnPropertyChanged();
        }
    }
    
    public bool DarkTheme
    {
        get => _darkTheme;
        set
        {
            _darkTheme = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}