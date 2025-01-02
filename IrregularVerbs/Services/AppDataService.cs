using System;
using System.IO;
using System.Threading.Tasks;

namespace IrregularVerbs.Services;

public abstract class AppDataService
{
    private const string AppFolderName = "IrregularVerbs";
    private static string AppDataPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    protected DirectoryInfo AppDirectoryInfo { get; private set; }

    protected AppDataService()
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