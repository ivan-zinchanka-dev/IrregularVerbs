using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using IrregularVerbs.CodeBase.AbstractFactory;
using IrregularVerbs.Factories;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;
using IrregularVerbs.ViewModels;
using IrregularVerbs.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IrregularVerbs
{
    public partial class App : Application
    {
        private ResourceDictionary LogicalResources => Resources.MergedDictionaries[0];

        private LocalizationService _localizationService;
        private UserPreferencesService _preferencesService;
        private CacheService _cacheService;

        private IHost _host;
        private PageManager _pageManager;
        private MainWindow _mainWindow;
        
        // TODO Check multiple startups and message box if need
        
        private void SetNativeLanguage()
        {
            _localizationService.CurrentLanguage = _preferencesService.AppSettings.NativeLanguage;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            _preferencesService = new UserPreferencesService(LogicalResources);
            _cacheService = new CacheService();

            Task prefsServiceLaunchTask = _preferencesService.InitializeAsync();
            Task cacheServiceLaunchTask = _cacheService.InitializeAsync();
            
            await prefsServiceLaunchTask;
            
            _localizationService = new LocalizationService();
            SetNativeLanguage();
            _preferencesService.AppSettings.OnPropertyChanged += SetNativeLanguage;
            
            await cacheServiceLaunchTask;

            _host = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<ApplicationSettings>(_preferencesService.AppSettings)
                        .AddSingleton<LocalizationService>(_localizationService)
                        .AddSingleton<IParametrizedFactory<LocalizedText, string>, LocalizedTextFactory>()
                        .AddSingleton<FixedFormFactory>()
                        .AddSingleton<VolatileFormFactory>()
                        .AddSingleton<IrregularVerbsFactory>()
                        .AddSingleton<IrregularVerbsStorage>()
                        .AddTransient<IrregularVerbsTeacher>()
                        .AddSingleton<CacheService>(_cacheService)
                        .AddSingleton<PageManager>()
                        .AddSingleton<MainWindow>()
                        .AddTransient<StartPageViewModel>()
                        .AddTransient<RevisePageViewModel>()
                        .AddTransient<CheckPageViewModel>()
                        .AddAbstractFactory<StartPage>()
                        .AddAbstractFactory<RevisePage>()
                        .AddAbstractFactory<CheckPage>();
                })
                .Build();

            await _host.StartAsync();

            _host.Services.GetRequiredService<IrregularVerbsStorage>();
            _pageManager = _host.Services.GetRequiredService<PageManager>();
            _mainWindow = _host.Services.GetRequiredService<MainWindow>();
            
            _mainWindow.Loaded += OnMainWindowLoaded;
            _mainWindow.Navigating += OnMainWindowNavigating;
            _pageManager.OnPageCreated += _mainWindow.NavigateTo;
            
            _mainWindow.Show();
        }

        private void OnMainWindowNavigating(object sender, NavigatingCancelEventArgs args)
        {
            if (args.NavigationMode == NavigationMode.Forward || 
                args.NavigationMode == NavigationMode.Back)
            {
                args.Cancel = true;
            }
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
            
            _preferencesService.AppSettings.OnPropertyChanged -= SetNativeLanguage;
            base.OnExit(e);
        }
        
        // TODO add UnhandledException
    }
}