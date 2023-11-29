using System;
using System.Windows;
using System.Windows.Controls;

namespace IrregularVerbs;

public partial class StartPage : Page
{
    public StartPage()
    {
        InitializeComponent();
    }

    private void OnReviseClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("Revise");
    }
}