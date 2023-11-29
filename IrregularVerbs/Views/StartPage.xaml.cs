using System;
using System.Windows;
using System.Windows.Controls;

namespace IrregularVerbs.Views;

public partial class StartPage : Page
{
    public event Action OnDemandRevise;
    public event Action OnDemandCheck;
    
    public StartPage()
    {
        InitializeComponent();
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