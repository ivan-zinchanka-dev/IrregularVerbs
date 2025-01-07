using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using IrregularVerbs.CodeBase.Localization;
using IrregularVerbs.CodeBase.MVVM;
using IrregularVerbs.CodeBase.Validation;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;
using IrregularVerbs.Views;

namespace IrregularVerbs.ViewModels;

public class StartPageViewModel : BaseViewModel
{
    private ApplicationSettings _appSettings;
    private List<string> _languages;
    private RelayCommand _reviseCommand;
    private RelayCommand _checkCommand;

    private readonly PageManager _pageManager;
    private readonly ILocalizationService _localizationService;
    private readonly List<ValidationError> _validationErrors = new List<ValidationError>(5);
    
    public StartPageViewModel(
        ApplicationSettings appSettings, 
        PageManager pageManager, 
        ILocalizationService localizationService)
    {
        _appSettings = appSettings;
        _pageManager = pageManager;
        _localizationService = localizationService;
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

    public IReadOnlyCollection<string> Languages => _languages ??= _localizationService.Languages.ToList();

    public int SelectedLanguageIndex
    {
        get => _languages.IndexOf(AppSettings.NativeLanguage);
        set => AppSettings.NativeLanguage = _languages[value];
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

    public override bool HasErrors => _validationErrors.Any();

    public override IEnumerable GetErrors(string propertyName)
    {
        return _validationErrors
            .Where(error => error.PropertyName == propertyName)
            .Select(error => error.ErrorMessage);
    }

    public void AddValidationError(ValidationError error)
    { 
        _validationErrors.Add(error);
    }
    
    public bool RemoveValidationError(ValidationError error)
    {
        return _validationErrors.Remove(error);
    }
}