using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Models;
using IrregularVerbs.Models.Components;

namespace IrregularVerbs.ViewPresenters;

public partial class StartPage : Page
{
    public event Action OnDemandRevise;
    public event Action OnDemandCheck;
    
    public StartPage()
    {
        InitializeComponent();
        DataContext = this;

        _settingsGroupBox.DataContext = App.Instance.PreferencesService.AppSettings;
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

    private void OnReviseClick(object sender, RoutedEventArgs e)
    {
        if (ValidateAppSettings())
        {
            OnDemandRevise?.Invoke();
        }
    }
    
    private void OnCheckClick(object sender, RoutedEventArgs e)
    {
        if (ValidateAppSettings())
        {
            OnDemandCheck?.Invoke();
        }
    }
    
}