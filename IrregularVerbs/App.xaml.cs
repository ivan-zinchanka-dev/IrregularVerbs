using System.Windows;
using IrregularVerbs.Services;

namespace IrregularVerbs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance { get; private set; } = null!;
        
        private ResourceDictionary LogicalResources => Resources.MergedDictionaries[0];
        public IrregularVerbsStorage IrregularVerbsStorage { get; private set; }
        public LocalizationService LocalizationService { get; private set; }
        public UserPreferencesService PreferencesService { get; private set; }
        public CacheService CacheService { get; private set; }
        
        public App()
        {
            Instance = this;
        }
        
        private void SetNativeLanguage()
        {
            LocalizationService.CurrentLanguage = PreferencesService.AppSettings.NativeLanguage;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            PreferencesService = new UserPreferencesService(LogicalResources);
            PreferencesService.InitializeAsync(() =>
            {
                LocalizationService = new LocalizationService();
                SetNativeLanguage();
                PreferencesService.AppSettings.OnPropertyChanged += SetNativeLanguage;
            
                IrregularVerbsStorage = new IrregularVerbsStorage();
                
            });
            
            CacheService = new CacheService();
            CacheService.InitializeAsync();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            PreferencesService.AppSettings.OnPropertyChanged -= SetNativeLanguage;
            base.OnExit(e);
        }
    }
}