using System.Diagnostics;
using System.IO;
using IrregularVerbs.Domain.Services.AppData;
using IrregularVerbs.Presentation.ViewModels.Base;
using Serilog;
using Serilog.Core;

namespace IrregularVerbs.Presentation.Services.AppData;

internal class LoggingConfigurator : AppDataService
{
    private const string LogsFolderName = "Logs";
    private const string LogsFileName = "app_logs.txt";
    private const string LogOutputTemplate =
        "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";
    private const int LogsFileSizeLimitBytes = 1_000_000;
    private const int MaxLogsFiles = 3;
    
    private DirectoryInfo _logsDirectoryInfo;
    private string LogsFilePath => Path.Combine(AppDirectoryInfo.FullName, LogsFolderName, LogsFileName);
    
    public LoggingConfigurator()
    {
        CheckLogsFolder();
    }
    
    private void CheckLogsFolder()
    {
        string path = Path.Combine(AppDirectoryInfo.FullName, LogsFolderName);
        _logsDirectoryInfo = new DirectoryInfo(path);
        
        if (!_logsDirectoryInfo.Exists)
        {
            _logsDirectoryInfo.Create();
        }
    }

    public Logger CreateLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Debug()
            .WriteTo.File(
                path: LogsFilePath, 
                rollOnFileSizeLimit: true, 
                fileSizeLimitBytes: LogsFileSizeLimitBytes, 
                outputTemplate: LogOutputTemplate,
                retainedFileCountLimit: MaxLogsFiles)
            .CreateLogger();
    }

    public void AddBindingTraceListening()
    {
        PresentationTraceSources.DataBindingSource.Listeners.Add(new BindingErrorTraceListener());
        PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Error;
    }
}