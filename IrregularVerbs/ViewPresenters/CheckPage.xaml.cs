using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Models.Answers;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Models.Verbs;
using IrregularVerbs.Services;

namespace IrregularVerbs.ViewPresenters;

public partial class CheckPage : Page
{
    private readonly ApplicationSettings _applicationSettings;
    private readonly IrregularVerbsStorage _irregularVerbsStorage;
    private readonly CacheService _cacheService;
    
    private readonly ObservableCollection<IrregularVerbAnswer> _answers;
    private readonly IrregularVerbsTeacher _teacher;
    
    public CheckPage(
        ApplicationSettings applicationSettings, 
        IrregularVerbsStorage irregularVerbsStorage, 
        CacheService cacheService)
    {
        _applicationSettings = applicationSettings;
        _irregularVerbsStorage = irregularVerbsStorage;
        _cacheService = cacheService;
        
        InitializeComponent();
        
        _teacher = new IrregularVerbsTeacher(
                _irregularVerbsStorage, 
                _applicationSettings.VerbsCount, 
                _applicationSettings.AlphabeticalOrder)
            .UsePriorities(_cacheService.TermPriorities);
        
        _answers = new ObservableCollection<IrregularVerbAnswer>(_teacher.GenerateTask());
        Loaded += OnPageLoaded;
    }
    
    private void OnPageLoaded(object sender, RoutedEventArgs args)
    {
        _tableView.ItemsSource = _answers;
    }
    
    private void OnCheckClick(object sender, RoutedEventArgs args)
    {
        CheckingResult checkingResult = _teacher.CheckTask();
        _resultTextBlock.Text = $"Your result: {checkingResult.CorrectAnswersCount}/{checkingResult.AllAnswersCount}";
        
        _checkButton.Visibility = Visibility.Collapsed;
        _backButton.Visibility = Visibility.Visible;
    }

    private void OnMoreInfoClick(object sender, RoutedEventArgs args)
    {
        int answerInstanceId = (int)((FrameworkContentElement)sender).Tag;
        
        Console.WriteLine("OnMoreInfoClick with tag: " + answerInstanceId);

        IrregularVerbAnswer foundAnswer = _answers.First(answer => answer.InstanceId == answerInstanceId);
        BaseIrregularVerb foundOriginal = foundAnswer.Original;
        
        new IrregularVerbInfoWindow(foundOriginal).ShowDialog();
    }

    private void OnBackClick(object sender, RoutedEventArgs args)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}

