using System;
using System.Windows;
using System.Windows.Controls;

namespace IrregularVerbs.Views;

public partial class RevisePage : Page
{
    public event Action OnDemandBack;
    
    public RevisePage()
    {
        InitializeComponent();
        Loaded += OnPageLoaded;
    }
    
    private void OnPageLoaded(object sender, RoutedEventArgs args)
    {
        _tableView.ItemsSource = App.Instance.IrregularVerbsService.IrregularVerbs;
    }

    private void OnBackButtonClick(object sender, RoutedEventArgs e)
    {
        OnDemandBack?.Invoke();
    }
}