using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Services;

namespace IrregularVerbs.ViewPresenters;

public partial class RevisePage : Page
{
    private readonly IrregularVerbsStorage _irregularVerbsStorage;
    
    public RevisePage(IrregularVerbsStorage irregularVerbsStorage)
    {
        _irregularVerbsStorage = irregularVerbsStorage;
        
        InitializeComponent();
        Loaded += OnPageLoaded;
    }
    
    private void OnPageLoaded(object sender, RoutedEventArgs args)
    {
        _tableView.ItemsSource = _irregularVerbsStorage.IrregularVerbs;
    }

    private void OnBackClick(object sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}