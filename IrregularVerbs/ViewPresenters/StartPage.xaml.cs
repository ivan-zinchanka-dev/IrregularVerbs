using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Models.Components;
using IrregularVerbs.Models.Configs;

namespace IrregularVerbs.ViewPresenters;

public partial class StartPage : Page
{
    private readonly ApplicationSettings _applicationSettings;
    
    public event Action OnDemandRevise;
    public event Action OnDemandCheck;
    
    public StartPage(ApplicationSettings applicationSettings)
    {
        _applicationSettings = applicationSettings;
        
        InitializeComponent();
        _settingsGroupBox.DataContext = _applicationSettings;
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