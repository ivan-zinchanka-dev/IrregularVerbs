using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using IrregularVerbs.Models.Configs;

namespace IrregularVerbs.Services;

public class UserPreferencesService
{
    private const string AppFolderName = "IrregularVerbs";
    
    private const string AppSettingsResourceKey = "ApplicationSettings";
    private const string AppSettingsFileName = "app_settings.json";
    
    private static string AppDataPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    
    private ResourceDictionary _appResourceDictionary;
    private DirectoryInfo _appDirectoryInfo;
    
    public ApplicationSettings AppSettings { get; private set; }

    public UserPreferencesService(ResourceDictionary appResourceDictionary)
    {
        _appResourceDictionary = appResourceDictionary;
        CheckApplicationFolder();
    }

    public async Task InitializeAsync()
    {
        await LoadAppSettingsAsync();
    }

    private void CheckApplicationFolder()
    {
        string path = Path.Combine(AppDataPath, AppFolderName);
        _appDirectoryInfo = new DirectoryInfo(path);
        
        if (!_appDirectoryInfo.Exists)
        {
            _appDirectoryInfo.Create();
        }
    }
    
    private async Task LoadAppSettingsAsync()
    {
        string fullFileName = Path.Combine(_appDirectoryInfo.FullName, AppSettingsFileName);

        if (!File.Exists(fullFileName))
        {
            AppSettings = (ApplicationSettings)_appResourceDictionary[AppSettingsResourceKey];
        }
        else
        {
            try
            {
                string jsonNotation = await File.ReadAllTextAsync(fullFileName);
                AppSettings = JsonSerializer.Deserialize<ApplicationSettings>(jsonNotation);
            }
            catch (Exception ex)
            {
                AppSettings = (ApplicationSettings)_appResourceDictionary[AppSettingsResourceKey];
                File.Delete(fullFileName);
            }
        }

        AppSettings.OnPropertyChanged += SaveAppSettingsAsync;
    }

    private async void SaveAppSettingsAsync()
    {
        string jsonNotation = JsonSerializer.Serialize(AppSettings);
        string fullFileName = Path.Combine(_appDirectoryInfo.FullName, AppSettingsFileName);
        
        await File.WriteAllTextAsync(fullFileName, jsonNotation);
    }

    ~UserPreferencesService()
    {
        if (AppSettings != null)
        {
            AppSettings.OnPropertyChanged -= SaveAppSettingsAsync;
        }
    }

}