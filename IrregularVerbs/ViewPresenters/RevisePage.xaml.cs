using System.Windows;
using System.Windows.Controls;

namespace IrregularVerbs.ViewPresenters;

public partial class RevisePage : Page
{
    public RevisePage()
    {
        InitializeComponent();
        Loaded += OnPageLoaded;
    }
    
    private void OnPageLoaded(object sender, RoutedEventArgs args)
    {
        _tableView.ItemsSource = App.Instance.IrregularVerbsStorage.IrregularVerbs;
    }

    private void OnBackClick(object sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}