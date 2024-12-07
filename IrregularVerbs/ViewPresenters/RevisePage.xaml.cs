using System.Windows.Controls;
using System.Windows.Input;
using IrregularVerbs.ViewModels;

namespace IrregularVerbs.ViewPresenters;

public partial class RevisePage : Page
{
    private readonly RevisePageViewModel _viewModel;
    
    public RevisePage(RevisePageViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        
        InitializeComponent();
        
        CommandBindings.Add(new CommandBinding(
            NavigationCommands.BrowseBack,
            (sender, args) => _viewModel.BackCommand.Execute(args.Parameter),
            (sender, args) => args.CanExecute = _viewModel.BackCommand.CanExecute(args.Parameter)));
    }
}