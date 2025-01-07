using System.Windows;

namespace IrregularVerbs.Utilities;

public static class MessageBoxUtility
{
    private const string ErrorCaption = "Error!";
    private const string WarningCaption = "Warning!";

    private const string CommonError = "An error occurred while the program was running. For more details, see the log file.";
    private const string AppAlreadyRunning = "This application is already running";
            
    public static MessageBoxResult ShowErrorMessageBox()
    {
        return MessageBox.Show(CommonError, ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
    }
        
    public static MessageBoxResult ShowWarningMessageBox()
    {
        return MessageBox.Show(AppAlreadyRunning, WarningCaption, MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}