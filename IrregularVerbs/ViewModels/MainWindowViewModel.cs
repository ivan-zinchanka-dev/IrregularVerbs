using IrregularVerbs.CodeBase;
using IrregularVerbs.Models.Configs;

namespace IrregularVerbs.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private ApplicationSettings _appSettings;
    
    public MainWindowViewModel(ApplicationSettings appSettings)
    {
        _appSettings = appSettings;
    }
    
    public ApplicationSettings AppSettings
    {
        get => _appSettings;
        set
        {
            _appSettings = value;
            OnPropertyChanged();
        }
    }
    
    
}