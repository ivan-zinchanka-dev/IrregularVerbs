﻿using System.Collections;
using System.ComponentModel;
using System.Text.Json;
using IrregularVerbs.Domain.Models.Configs;

namespace IrregularVerbs.Domain.Services.AppData;

public class UserPreferencesService : AppDataService, IDisposable
{
    private const string PreferencesFolderName = "Preferences";
    private const string AppSettingsResourceKey = "ApplicationSettings";
    private const string AppSettingsFileName = "app_settings.json";
    
    private readonly IDictionary _appResourceDictionary;
    private DirectoryInfo _preferencesDirectoryInfo;
    
    public ApplicationSettings AppSettings { get; private set; }

    public UserPreferencesService(IDictionary appResourceDictionary)
    {
        _appResourceDictionary = appResourceDictionary;
        CheckPreferencesFolder();
    }
    
    public async Task InitializeAsync()
    {
        await LoadAppSettingsAsync();
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

        AppSettings.PropertyChanged += SaveAppSettingsAsync;
    }

    private async void SaveAppSettingsAsync(object sender, PropertyChangedEventArgs eventArgs)
    {
        string jsonNotation = JsonSerializer.Serialize(AppSettings);
        string fullFileName = Path.Combine(_preferencesDirectoryInfo.FullName, AppSettingsFileName);
        
        await File.WriteAllTextAsync(fullFileName, jsonNotation);
    }
    
    public void Dispose()
    {
        if (AppSettings != null)
        {
            AppSettings.PropertyChanged -= SaveAppSettingsAsync;
        }
    }
}