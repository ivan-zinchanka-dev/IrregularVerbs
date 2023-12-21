using System;
using System.IO;

namespace IrregularVerbs.Services;

public class AppDataService
{
    private const string AppFolderName = "IrregularVerbs";
    private static string AppDataPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    protected DirectoryInfo AppDirectoryInfo;

    public AppDataService()
    {
        CheckApplicationFolder();
    }
    
    private void CheckApplicationFolder()
    {
        string path = Path.Combine(AppDataPath, AppFolderName);
        AppDirectoryInfo = new DirectoryInfo(path);
        
        if (!AppDirectoryInfo.Exists)
        {
            AppDirectoryInfo.Create();
        }
    }
}