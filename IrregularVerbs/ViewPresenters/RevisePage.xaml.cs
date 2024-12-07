using IrregularVerbs.ViewModels;

namespace IrregularVerbs.ViewPresenters;

public partial class RevisePage : EndPage
{
    private readonly RevisePageViewModel _viewModel;
    
    public RevisePage(RevisePageViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        
        InitializeComponent();
        RegisterBackCommand(_viewModel.BackCommand);
    }
}