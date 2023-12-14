using System;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Models;
using IrregularVerbs.Services;

namespace IrregularVerbs.ViewPresenters;

public partial class StartPage : Page
{
    public event Action OnDemandRevise;
    public event Action OnDemandCheck;
    
    public StartPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void OnReviseClick(object sender, RoutedEventArgs e)
    {
        OnDemandRevise?.Invoke();
    }
    
    private void OnCheckClick(object sender, RoutedEventArgs e)
    {
        OnDemandCheck?.Invoke();
    }

    private void ResetSettings()
    {
        App.Instance.Settings = new ApplicationSettings
        {
            NativeLanguage = _nativeLanguageComboBox.Text,
            VerbsCount = int.Parse(_verbsCountTextBox.Text),
            DisorderVerbs = _disorderVerbsCheckBox.IsChecked.Value,
        };
    }
}