using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using IrregularVerbs.CodeBase.ThemeManagement;
using IrregularVerbs.ViewModels;
using IrregularVerbs.Views.Base;
using MaterialDesignThemes.Wpf;

namespace IrregularVerbs.Views;

public partial class CheckPage : EndPage
{
    private const double MinColumnWidthMultiplier = 0.75d;
    private const double MaxColumnWidthMultiplier = 1.25d;
    
    private readonly CheckPageViewModel _viewModel;
    private readonly ThemeManager _themeManager;
    
    public Color HyperLinkColor => _themeManager.CurrentBaseTheme == BaseTheme.Dark ? 
        Colors.DodgerBlue : Colors.Navy;
    
    public CheckPage(CheckPageViewModel viewModel, ThemeManager themeManager)
    {
        _viewModel = viewModel;
        _themeManager = themeManager;
        DataContext = _viewModel;
        
        InitializeComponent();

        RegisterBackCommand(_viewModel.BackCommand);
        _viewModel.OnTaskChecked += OnTaskChecked;
        Unloaded += OnUnloaded;
    }
    
    private void AdjustGrid(object sender, RoutedEventArgs eventArgs)
    {
        foreach (DataGridColumn column in _grid.Columns)
        {
            double normalWidth = column.Width.Value;
            
            column.MinWidth = normalWidth * MinColumnWidthMultiplier;
            column.MaxWidth = normalWidth * MaxColumnWidthMultiplier;
        }

        _grid.Columns.Last().CanUserResize = false;
    }
    
    private void OnGridPreviewKeyDown(object sender, KeyEventArgs eventArgs)
    {
        if (eventArgs.Key == Key.Escape)                                        // prevent binding exception
        {
            eventArgs.Handled = true;
        }
    }

    private void OnTaskChecked()
    {
        _grid.IsReadOnly = true;
        ToolTipService.SetIsEnabled(_grid, false);
        BindingOperations.ClearBinding(_grid, ToolTipService.ToolTipProperty);
        
        _checkButton.Visibility = Visibility.Collapsed;
        _backButton.Visibility = Visibility.Visible;
    }
    
    private void OnUnloaded(object sender, RoutedEventArgs eventArgs)
    {
        _viewModel.OnTaskChecked -= OnTaskChecked;
        Unloaded -= OnUnloaded;
    }
}

