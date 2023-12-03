using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Models;
using IrregularVerbs.Services;

namespace IrregularVerbs.ViewPresenters;

public partial class CheckPage : Page
{
    private IrregularVerbsService _irregularVerbsService;
    private ObservableCollection<IrregularVerbAnswer> _answers;
    
    public CheckPage()
    {
        InitializeComponent();
        _irregularVerbsService = App.Instance.IrregularVerbsService;
        _answers = _irregularVerbsService.GetRandomVerbAnswers(3);
        Loaded += OnPageLoaded;
    }
    
    private void OnPageLoaded(object sender, RoutedEventArgs args)
    {
        _tableView.ItemsSource = _answers;
    }
    
    private void OnCheckButtonClick(object sender, RoutedEventArgs e)
    {
        foreach (IrregularVerbAnswer answer in _answers)
        {
            bool checkResult = _irregularVerbsService.InspectAnswer(answer);
            Console.WriteLine(checkResult + " " + answer);
        }
    }
}