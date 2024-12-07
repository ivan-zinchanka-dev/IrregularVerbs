using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using IrregularVerbs.CodeBase;
using IrregularVerbs.Models.Answers;
using IrregularVerbs.Services;
using IrregularVerbs.ViewPresenters;

namespace IrregularVerbs.ViewModels;

public class CheckPageViewModel : BaseViewModel
{
    private ObservableCollection<IrregularVerbAnswer> _answers;
    private string _resultMessage;
    private RelayCommand _checkCommand;
    private RelayCommand _infoCommand;
    private RelayCommand _backCommand;
    
    private readonly IrregularVerbsTeacher _teacher;
    private readonly PageManager _pageManager;
    private bool _taskIsChecked;
    
    public event Action OnTaskChecked;
    
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
    
    public CheckPageViewModel(IrregularVerbsTeacher teacher, PageManager pageManager)
    {
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