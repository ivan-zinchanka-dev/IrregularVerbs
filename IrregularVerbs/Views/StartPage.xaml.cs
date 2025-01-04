using System.Windows.Controls;
using System.Windows.Data;
using IrregularVerbs.ViewModels;
using ValidationError = IrregularVerbs.CodeBase.Validation.ValidationError;

namespace IrregularVerbs.Views;

public partial class StartPage : Page
{
    private readonly StartPageViewModel _viewModel;
    
    public StartPage(StartPageViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        
        InitializeComponent();
    }
    
    private void OnValidationError(object sender, ValidationErrorEventArgs eventArgs)
    {
        switch (eventArgs.Action)
        {
            case ValidationErrorEventAction.Added:
            {
                if (eventArgs.Error.BindingInError is BindingExpression bindingExpression)
                {
                    string propertyName = bindingExpression.ResolvedSourcePropertyName;
                    string errorMessage = eventArgs.Error.ErrorContent?.ToString();
                
                    _viewModel.AddValidationError(new ValidationError(propertyName, errorMessage));
                }
                
                break;
            }
            case ValidationErrorEventAction.Removed:
            {
                if (eventArgs.Error.BindingInError is BindingExpression bindingExpression)
                {
                    string propertyName = bindingExpression.ResolvedSourcePropertyName;
                    string errorMessage = eventArgs.Error.ErrorContent?.ToString();
                    
                    _viewModel.RemoveValidationError(new ValidationError(propertyName, errorMessage));
                }
                
                break;
            }
        }
    }
}