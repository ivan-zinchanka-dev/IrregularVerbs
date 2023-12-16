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
        
        //  TODO _nativeLanguageComboBox  SELECTION Changed
    }

    private void OnReviseClick(object sender, RoutedEventArgs e)
    {
        OnDemandRevise?.Invoke();
    }
    
    private void OnCheckClick(object sender, RoutedEventArgs e)
    {
        OnDemandCheck?.Invoke();
    }
    
}