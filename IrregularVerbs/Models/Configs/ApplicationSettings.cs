using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace IrregularVerbs.Models.Configs;

public class ApplicationSettings : INotifyPropertyChanged
{
    private string _nativeLanguage;
    private int _verbsCount;
    private bool _alphabeticalOrder;
    private bool _enableToolTips;
    private bool _darkTheme;
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    public string NativeLanguage
    {
        get => _nativeLanguage;
        set
        {
            _nativeLanguage = value;
            OnPropertyChanged();
        }
    }
    
    [Range(1, int.MaxValue)]
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
    
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}