using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IrregularVerbs.ViewModels;

namespace IrregularVerbs.ViewPresenters;

public partial class CheckPage : Page
{
    private readonly CheckPageViewModel _viewModel;
    
    public CheckPage(CheckPageViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        
        InitializeComponent();

        _viewModel.OnTaskChecked += OnTaskChecked;
        Unloaded += OnUnloaded;
        
        CommandBindings.Add(new CommandBinding(
            NavigationCommands.BrowseBack,
            (sender, args) => _viewModel.BackCommand.Execute(args.Parameter),
            (sender, args) => args.CanExecute = _viewModel.BackCommand.CanExecute(args.Parameter)));
    }
    
    private void OnTaskChecked()
    {
        _checkButton.Visibility = Visibility.Collapsed;
        _backButton.Visibility = Visibility.Visible;
    }
    
    private void OnUnloaded(object sender, RoutedEventArgs eventArgs)
    {
        _viewModel.OnTaskChecked -= OnTaskChecked;
        Unloaded -= OnUnloaded;
    }
}

