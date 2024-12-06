using System.Windows.Controls;
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
    }
}