using System.IO;
using Serilog;
using Serilog.Core;

namespace IrregularVerbs.Services;

public class LoggingConfigurator : AppDataService
{
    private const string LogsFolderName = "Logs";
    private const string LogsFileName = "app_logs.txt";
    private const string LogOutputTemplate =
        "[{Timestamp:HH:mm:ss} {Level:u3}] ({SourceContext:l}) {Message:lj}{NewLine}{Exception}";
    private const int LogsFileSizeLimitBytes = 1_000_000;
    
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
            .MinimumLevel.Debug()
            .WriteTo.Debug()
            .WriteTo.File(
                path: LogsFilePath, 
                rollOnFileSizeLimit: true, 
                fileSizeLimitBytes: LogsFileSizeLimitBytes, 
                outputTemplate: LogOutputTemplate)
            .CreateLogger();
    }
}