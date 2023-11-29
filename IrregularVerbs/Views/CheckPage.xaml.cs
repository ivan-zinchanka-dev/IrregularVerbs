using System.Windows;
using System.Windows.Controls;

namespace IrregularVerbs.Views;

public partial class CheckPage : Page
{
    public CheckPage()
    {
        InitializeComponent();
        Loaded += OnPageLoaded;
    }
    
    private void OnPageLoaded(object sender, RoutedEventArgs args)
    {
        _tableView.ItemsSource = App.Instance.IrregularVerbsService.GetRandomVerbForms(3);
    }
}