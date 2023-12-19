using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace IrregularVerbs.Models;

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
        LoadAppSettingsAsync();
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
    
    private void LoadAppSettingsAsync()
    {
        string fullFileName = Path.Combine(_appDirectoryInfo.FullName, AppSettingsFileName);

        if (!File.Exists(fullFileName))
        {
            AppSettings = (ApplicationSettings)_appResourceDictionary[AppSettingsResourceKey];
        }
        else
        {
            // TODO ASYNC
            //string jsonNotation = await File.ReadAllTextAsync(fullFileName);
            string jsonNotation = File.ReadAllText(fullFileName);
            AppSettings = JsonSerializer.Deserialize<ApplicationSettings>(jsonNotation);
        }
    }

    public async void SaveAppSettingsAsync()
    {
        string jsonNotation = JsonSerializer.Serialize(AppSettings);
        string fullFileName = Path.Combine(_appDirectoryInfo.FullName, AppSettingsFileName);
        
        await File.WriteAllTextAsync(fullFileName, jsonNotation);
    }
    
}