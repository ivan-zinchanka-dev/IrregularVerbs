using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using IrregularVerbs.CodeBase;
using IrregularVerbs.Factories;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;
using IrregularVerbs.ViewModels;
using IrregularVerbs.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace IrregularVerbs
{
    public partial class App : Application
    {
        public static App Instance => (App)Current;

        public ApplicationSettings AppSettings => _preferencesService.AppSettings;

        private ResourceDictionary LogicalResources => Resources.MergedDictionaries[0];

        private LocalizationService _localizationService;
        private UserPreferencesService _preferencesService;
        private CacheService _cacheService;

        private IHost _host;
        private PageManager _pageManager;
        private MainWindow _mainWindow;

        private const string ErrorMessageHeader = "Error!";
        private const string ErrorMessageBody = "An error occurred while the program was running. For more details, see the log file.";
        
        private void PreventMultipleStartup()
        {
            Process currentProcess = Process.GetCurrentProcess();
            
            IEnumerable<Process> runningProcesses = Process.GetProcessesByName(currentProcess.ProcessName)
                .Where(process => process.Id != currentProcess.Id);

            bool alreadyRunning = runningProcesses.Any();
            
            if (alreadyRunning)
            {
                MessageBox.Show("This application is already running", "Warning!", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                Shutdown();
            }
        }
        
        private void SetNativeLanguage()
        {
            _localizationService.CurrentLanguage = _preferencesService.AppSettings.NativeLanguage;
        }
        
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            PreventMultipleStartup();
            
            PresentationTraceSources.DataBindingSource.Listeners.Add(new BindingErrorTraceListener());
            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Error;
            
            AppDomain.CurrentDomain.UnhandledException += OnAppDomainUnhandledException;
            DispatcherUnhandledException += OnDispatcherUnhandledException;

            Log.Logger = new LoggingConfigurator().CreateLogger();

            try
            {
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
                        logging.AddSerilog(Log.Logger);
                    })
                    .ConfigureServices(ConfigureServices)
                    .Build();

                await _host.StartAsync();

                IrregularVerbsStorage verbsStorage = _host.Services.GetRequiredService<IrregularVerbsStorage>();
                AppSettings.Validator.MaxVerbsCount = verbsStorage.IrregularVerbs.Count;
                
                // TODO handle resource files errors
                
                _pageManager = _host.Services.GetRequiredService<PageManager>();
                _mainWindow = _host.Services.GetRequiredService<MainWindow>();
                
                _mainWindow.Loaded += OnMainWindowLoaded;
                _mainWindow.Navigating += OnMainWindowNavigating;
                _pageManager.OnPageCreated += _mainWindow.NavigateTo;
                
                _mainWindow.Show();

            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unhandled exception occured");
                MessageBox.Show(ErrorMessageBody, ErrorMessageHeader, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                await Log.CloseAndFlushAsync();
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<ApplicationSettings>(_preferencesService.AppSettings)
                .AddSingleton<LocalizationService>(_localizationService)
                .AddSingleton<LocalizedTextFactory>()
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
                .AddTransient<StartPage>()
                .AddTransient<RevisePage>()
                .AddTransient<CheckPage>();
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
            _mainWindow.Navigating -= OnMainWindowNavigating;
            _mainWindow.Loaded -= OnMainWindowLoaded;
            
            await _host.StopAsync();
            _host.Dispose();
            
            _preferencesService.AppSettings.OnPropertyChanged -= SetNativeLanguage;
            
            DispatcherUnhandledException -= OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException -= OnAppDomainUnhandledException;
            
            base.OnExit(e);
        }
        
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            Log.Error(eventArgs.Exception, "An dispatcher unhandled exception occured");
            MessageBox.Show(ErrorMessageBody, ErrorMessageHeader, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            Exception exception = eventArgs.ExceptionObject as Exception;
            Log.Error(exception, "An app domain unhandled exception occured");
            MessageBox.Show(ErrorMessageBody, ErrorMessageHeader, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}