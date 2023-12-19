using System;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Models;
using IrregularVerbs.Services;

using Language = IrregularVerbs.Services.Language;

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

    private void OnReviseClick(object sender, RoutedEventArgs e)
    {
        OnDemandRevise?.Invoke();
    }
    
    private void OnCheckClick(object sender, RoutedEventArgs e)
    {
        OnDemandCheck?.Invoke();
    }

    private void OnSelectedLanguageChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Enum.IsDefined(typeof(Language), _nativeLanguageComboBox.SelectedIndex))
        {
            App.Instance.SetNativeLanguage((Language)_nativeLanguageComboBox.SelectedIndex);
        }
    }
}