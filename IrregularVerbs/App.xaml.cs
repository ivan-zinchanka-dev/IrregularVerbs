using System.Threading.Tasks;
using System.Windows;
using IrregularVerbs.CodeBase;
using IrregularVerbs.CodeBase.AbstractFactory;
using IrregularVerbs.Factories;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;
using IrregularVerbs.ViewModels;
using IrregularVerbs.ViewPresenters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IrregularVerbs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance { get; private set; } = null!;
        
        private ResourceDictionary LogicalResources => Resources.MergedDictionaries[0];
        //public IrregularVerbsStorage IrregularVerbsStorage { get; private set; }
        //public IrregularVerbsFactory IrregularVerbsFactory { get; private set; }
        public LocalizationService LocalizationService { get; private set; }
        public UserPreferencesService PreferencesService { get; private set; }
        public CacheService CacheService { get; private set; }

        private IHost _host;
        private PageManager _pageManager;
        private MainWindow _mainWindow;
        
        // TODO Check multiple startups and message box if need
        
        public App()
        {
            Instance = this;
        }
        
        private void SetNativeLanguage()
        {
            LocalizationService.CurrentLanguage = PreferencesService.AppSettings.NativeLanguage;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            PreferencesService = new UserPreferencesService(LogicalResources);
            CacheService = new CacheService();

            Task prefsServiceLaunchTask = PreferencesService.InitializeAsync();
            Task cacheServiceLaunchTask = CacheService.InitializeAsync();
            
            await prefsServiceLaunchTask;
            
            LocalizationService = new LocalizationService();
            SetNativeLanguage();
            PreferencesService.AppSettings.OnPropertyChanged += SetNativeLanguage;
            
           
            
            await cacheServiceLaunchTask;

            _host = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<ApplicationSettings>(PreferencesService.AppSettings)
                        .AddSingleton<LocalizationService>(LocalizationService)
                        .AddSingleton<IParametrizedFactory<LocalizedText, string>, LocalizedTextFactory>()
                        .AddSingleton<FixedFormFactory>()
                        .AddSingleton<VolatileFormFactory>()
                        .AddSingleton<IrregularVerbsFactory>()
                        .AddSingleton<IrregularVerbsStorage>()
                        .AddSingleton<CacheService>(CacheService)
                        .AddSingleton<PageManager>()
                        .AddSingleton<MainWindow>()
                        .AddSingleton<StartPageViewModel>()
                        .AddAbstractFactory<StartPage>()
                        .AddAbstractFactory<RevisePage>()
                        .AddAbstractFactory<CheckPage>();
                })
                .Build();

            await _host.StartAsync();

            _host.Services.GetRequiredService<IrregularVerbsStorage>();
            _pageManager = _host.Services.GetRequiredService<PageManager>();
            
            _mainWindow = _host.Services.GetRequiredService<MainWindow>();
            _mainWindow.Show();

            _mainWindow.Loaded += OnMainWindowLoaded;
            _pageManager.OnPageCreated += _mainWindow.NavigateTo;

        }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs args)
        {
            _pageManager.SwitchTo<StartPage>();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            _pageManager.OnPageCreated -= _mainWindow.NavigateTo;
            _mainWindow.Loaded -= OnMainWindowLoaded;
            
            await _host.StopAsync();
            _host.Dispose();
            
            PreferencesService.AppSettings.OnPropertyChanged -= SetNativeLanguage;
            base.OnExit(e);
        }
    }
}