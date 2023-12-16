using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using IrregularVerbs.Models;
using IrregularVerbs.Services;

namespace IrregularVerbs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string AppSettingsResourceKey = "ApplicationSettings";
        private ResourceDictionary LogicalResources => Resources.MergedDictionaries[0];
        
        public static App Instance { get; private set; } = null!;

        public IrregularVerbsStorage IrregularVerbsStorage { get; private set; }
        public LocalizationService LocalizationService { get; private set; }
        public ApplicationSettings Settings { get; private set; }

        public App()
        {
            Instance = this;
        }

        public void SetNativeLanguage(Language language)
        {
            Settings.NativeLanguage = language;
            LocalizationService.CurrentLanguage = Settings.NativeLanguage;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Settings = (ApplicationSettings)LogicalResources[AppSettingsResourceKey];
            
            LocalizationService = new LocalizationService();
            LocalizationService.CurrentLanguage = Settings.NativeLanguage;
            
            IrregularVerbsStorage = new IrregularVerbsStorage();
        }
    }
}