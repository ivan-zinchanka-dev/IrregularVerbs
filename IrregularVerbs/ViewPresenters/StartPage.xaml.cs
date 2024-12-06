using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Models.Components;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.ViewModels;

namespace IrregularVerbs.ViewPresenters;

public partial class StartPage : Page
{
    private readonly StartPageViewModel _startPageViewModel;
    
    public StartPage(StartPageViewModel startPageViewModel)
    {
        _startPageViewModel = startPageViewModel;
        DataContext = _startPageViewModel;
        
        InitializeComponent();
    }

    private bool ValidateAppSettings()
    {
        ValidationResult result = new VerbsCountValidationRule()
            .Validate(_verbsCountTextBox.Text, CultureInfo.CurrentCulture);
        return result.IsValid;
    }
    
    private void OnValidationError(object sender, ValidationErrorEventArgs e)
    {
        if (e.Action == ValidationErrorEventAction.Added)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());
        }
    }
}