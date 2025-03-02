using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Presentation.ViewModels;
using IrregularVerbs.Presentation.Views.Base;

namespace IrregularVerbs.Presentation.Views;

internal partial class RevisePage : EndPage
{
    private const double MinColumnWidthMultiplier = 0.75d;
    private const double MaxColumnWidthMultiplier = 1.25d;
    
    private readonly RevisePageViewModel _viewModel;
    
    public RevisePage(RevisePageViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        
        InitializeComponent();
        RegisterBackCommand(_viewModel.BackCommand);
    }

    private void AdjustGrid(object sender, RoutedEventArgs e)
    {
        foreach (DataGridColumn column in _grid.Columns)
        {
            double normalWidth = column.Width.Value;
            
            column.MinWidth = normalWidth * MinColumnWidthMultiplier;
            column.MaxWidth = normalWidth * MaxColumnWidthMultiplier;
        }

        _grid.Columns.Last().CanUserResize = false;
    }
}