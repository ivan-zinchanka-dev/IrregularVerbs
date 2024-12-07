using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.ViewModels;

namespace IrregularVerbs.ViewPresenters;

public partial class CheckPage : Page
{
    private readonly CheckPageViewModel _viewModel;
    
    public CheckPage(CheckPageViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        
        InitializeComponent();

        _viewModel.OnTaskChecked += OnTaskChecked;
        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object sender, RoutedEventArgs eventArgs)
    {
        _viewModel.OnTaskChecked -= OnTaskChecked;
        Unloaded -= OnUnloaded;
    }
    
    private void OnTaskChecked()
    {
        _checkButton.Visibility = Visibility.Collapsed;
        _backButton.Visibility = Visibility.Visible;
    }

    private void OnMoreInfoClick(object sender, RoutedEventArgs args)
    {
        /*int answerInstanceId = (int)((FrameworkContentElement)sender).Tag;
        
        Console.WriteLine("OnMoreInfoClick with tag: " + answerInstanceId);

        IrregularVerbAnswer foundAnswer = _answers.First(answer => answer.InstanceId == answerInstanceId);
        BaseIrregularVerb foundOriginal = foundAnswer.Original;
        
        new IrregularVerbInfoWindow(foundOriginal).ShowDialog();*/
    }
}

