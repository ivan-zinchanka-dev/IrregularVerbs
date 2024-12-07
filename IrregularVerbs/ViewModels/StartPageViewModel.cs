using System.Collections;
using System.Windows.Input;
using IrregularVerbs.CodeBase;
using IrregularVerbs.CodeBase.Validation;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;
using IrregularVerbs.Views;

namespace IrregularVerbs.ViewModels;

public class StartPageViewModel : BaseViewModel
{
    private ApplicationSettings _appSettings;
    private RelayCommand _reviseCommand;
    private RelayCommand _checkCommand;
    
    private readonly ValidationErrorCollection _validationErrorCollection = new ValidationErrorCollection();
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
            },
                obj=> !HasErrors);
        }
    }
    
    public ICommand CheckCommand
    {
        get
        {
            return _checkCommand ??= new RelayCommand(obj =>
            {
                _pageManager.SwitchTo<CheckPage>();
            }, 
                obj=> !HasErrors);
        }
    }

    public override bool HasErrors => _validationErrorCollection.HasErrors;

    public override IEnumerable GetErrors(string propertyName)
    {
        return _validationErrorCollection.GetErrors(propertyName);
    }

    public bool TryAddValidationError(string propertyName, string errorMessage)
    {
        return _validationErrorCollection.TryAddError(propertyName, errorMessage);
    }
    
    public bool TryRemoveValidationError(string propertyName)
    {
        return _validationErrorCollection.TryClearErrors(propertyName);
    }

}