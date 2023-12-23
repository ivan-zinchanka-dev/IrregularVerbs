using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using IrregularVerbs.Models.Configs;

namespace IrregularVerbs.Services;

public class UserPreferencesService : AppDataService
{
    private const string PreferencesFolderName = "Preferences";
    private const string AppSettingsResourceKey = "ApplicationSettings";
    private const string AppSettingsFileName = "app_settings.json";
    
    private readonly ResourceDictionary _appResourceDictionary;
    private DirectoryInfo _preferencesDirectoryInfo;
    
    public ApplicationSettings AppSettings { get; private set; }

    public UserPreferencesService(ResourceDictionary appResourceDictionary)
    {
        _appResourceDictionary = appResourceDictionary;
        CheckPreferencesFolder();
    }
    
    public override void InitializeAsync(Action onComplete = null)
    {
        LoadAppSettingsAsync().ContinueWith((task) =>
        {
            onComplete?.Invoke();
            
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void CheckPreferencesFolder()
    {
        string path = Path.Combine(AppDirectoryInfo.FullName, PreferencesFolderName);
        _preferencesDirectoryInfo = new DirectoryInfo(path);
        
        if (!_preferencesDirectoryInfo.Exists)
        {
            _preferencesDirectoryInfo.Create();
        }
    }
    
    private async Task LoadAppSettingsAsync()
    {
        string fullFileName = Path.Combine(_preferencesDirectoryInfo.FullName, AppSettingsFileName);

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
        string fullFileName = Path.Combine(_preferencesDirectoryInfo.FullName, AppSettingsFileName);
        
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