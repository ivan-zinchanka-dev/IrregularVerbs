using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using IrregularVerbs.Domain.Models.Answers;
using IrregularVerbs.Domain.Models.Configs;
using IrregularVerbs.Domain.Services.Testing;
using IrregularVerbs.Presentation.Services.Management;
using IrregularVerbs.Presentation.ViewModels.Base;
using IrregularVerbs.Presentation.Views;

namespace IrregularVerbs.Presentation.ViewModels;

public class CheckPageViewModel : BaseViewModel
{
    private ApplicationSettings _appSettings;
    private ObservableCollection<IrregularVerbAnswer> _answers;
    private string _resultMessage;
    private RelayCommand _checkCommand;
    private RelayCommand _infoCommand;
    private RelayCommand _backCommand;
    
    private readonly IrregularVerbsTeacher _teacher;
    private readonly PageManager _pageManager;
    private bool _taskIsChecked;
    
    public event Action OnTaskChecked;
    
    public ApplicationSettings AppSettings
    {
        get => _appSettings;
        set
        {
            _appSettings = value;
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<IrregularVerbAnswer> Answers
    {
        get => _answers;

        set
        {
            _answers = value;
            OnPropertyChanged();
        }
    }
    
    public string ResultMessage
    {
        get => _resultMessage;

        set
        {
            _resultMessage = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand CheckCommand
    {
        get
        {
            return _checkCommand ??= new RelayCommand(CheckTask);
        }
    }

    public ICommand InfoCommand
    {
        get
        {
            return _infoCommand ??= new RelayCommand(ShowAnswerInfo);
        }
    }

    public ICommand BackCommand
    {
        get
        {
            return _backCommand ??= new RelayCommand(obj =>
            {
                _pageManager.SwitchTo<StartPage>();
            },
                obj => _taskIsChecked);
        }
    }
    
    public CheckPageViewModel(ApplicationSettings appSettings, IrregularVerbsTeacher teacher, PageManager pageManager)
    {
        _appSettings = appSettings;
        _teacher = teacher;
        _pageManager = pageManager;
        _teacher.UsePriorities();
        
        Answers = new ObservableCollection<IrregularVerbAnswer>(_teacher.GenerateTask());
    }

    private void CheckTask(object obj)
    {
        CheckingResult checkingResult = _teacher.CheckTask();
        _taskIsChecked = true;
        ResultMessage = $"Your result: {checkingResult.CorrectAnswersCount}/{checkingResult.AllAnswersCount}";
        
        OnTaskChecked?.Invoke();
    }

    private static void ShowAnswerInfo(object answer)
    {
        if (answer is IrregularVerbAnswer castAnswer)
        {
            new IrregularVerbInfoDialog(castAnswer.Original).ShowDialog();
        }
    }

}