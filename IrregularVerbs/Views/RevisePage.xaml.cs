using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Services;

namespace IrregularVerbs.Views;

public partial class RevisePage : Page
{
    public RevisePage()
    {
        InitializeComponent();

        Loaded += OnPageLoaded;
    }
    
    private void OnPageLoaded(object sender, RoutedEventArgs args)
    {
        IrregularVerbsService s = new IrregularVerbsService();

        _tableView.ItemsSource = s.IrregularVerbs;


    }
    
}