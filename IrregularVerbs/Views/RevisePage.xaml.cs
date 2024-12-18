using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.ViewModels;
using IrregularVerbs.Views.Base;

namespace IrregularVerbs.Views;

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

    private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
    {
        // TODO
        /*GridView gridView = _grid.View as GridView;
        
        foreach (GridViewColumn column in gridView.Columns)
        {
            double normalWidth = column.Width;
            
            column.MinWidth = normalWidth * MinColumnWidthMultiplier;
            column.MaxWidth = normalWidth * MaxColumnWidthMultiplier;
        }

        gridView.Columns.Last().CanUserResize = false;*/
    }
}