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
    private readonly ObservableCollection<IrregularVerbAnswer> _answers;
    private readonly IrregularVerbsTeacher _teacher;
    
    public CheckPage()
    {
        InitializeComponent();
        
        ApplicationSettings settings = App.Instance.PreferencesService.AppSettings;
        _teacher = new IrregularVerbsTeacher(App.Instance.IrregularVerbsStorage, settings.VerbsCount, 
            settings.AlphabeticalOrder).UsePriorities(App.Instance.CacheService.TermPriorities);
        
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

