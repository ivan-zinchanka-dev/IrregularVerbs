using System.Windows.Input;
using IrregularVerbs.CodeBase;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;
using IrregularVerbs.ViewPresenters;

namespace IrregularVerbs.ViewModels;

public class StartPageViewModel : BaseViewModel
{
    private ApplicationSettings _appSettings;
    private RelayCommand _reviseCommand;
    private RelayCommand _checkCommand;
    
    private readonly PageManager _pageManager;

    public StartPageViewModel(ApplicationSettings appSettings, PageManager pageManager)
    {
        _appSettings = appSettings;
        _pageManager = pageManager;
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
    
    public ICommand ReviseCommand
    {
        get
        {
            return _reviseCommand ??= new RelayCommand(obj =>
            {
                _pageManager.SwitchTo<RevisePage>();
            });
        }
    }
    
    public ICommand CheckCommand
    {
        get
        {
            return _checkCommand ??= new RelayCommand(obj =>
            {
                _pageManager.SwitchTo<CheckPage>();
            });
        }
    }
    
}