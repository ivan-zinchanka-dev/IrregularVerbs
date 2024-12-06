using System.Windows.Controls;
using System.Windows.Data;
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
    
    private void OnValidationError(object sender, ValidationErrorEventArgs eventArgs)
    {
        if (eventArgs.Action == ValidationErrorEventAction.Added)
        {
            if (eventArgs.Error.BindingInError is BindingExpression bindingExpression)
            {
                string propertyName = bindingExpression.ResolvedSourcePropertyName;
                string errorMessage = eventArgs.Error.ErrorContent?.ToString();
                
                _startPageViewModel.TryAddValidationError(propertyName, errorMessage);
            }
        }
        else if (eventArgs.Action == ValidationErrorEventAction.Removed)
        {
            if (eventArgs.Error.BindingInError is BindingExpression bindingExpression)
            {
                string propertyName = bindingExpression.ResolvedSourcePropertyName;
                
                _startPageViewModel.TryRemoveValidationError(propertyName);
            }
        }
    }
}