using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Models;
using IrregularVerbs.Models.Answers;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;

namespace IrregularVerbs.ViewPresenters;

public partial class CheckPage : Page
{
    private ObservableCollection<IrregularVerbAnswer> _answers;
    private IrregularVerbsTeacher _teacher;
    
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
    
    private void OnBackClick(object sender, RoutedEventArgs args)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}

