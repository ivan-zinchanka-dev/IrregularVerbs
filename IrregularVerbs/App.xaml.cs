using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using IrregularVerbs.CodeBase.ThemeManagement;
using IrregularVerbs.Factories;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;
using IrregularVerbs.Utilities;
using IrregularVerbs.ViewModels;
using IrregularVerbs.Views;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace IrregularVerbs
{
    public partial class App : Application
    {
        private IHost _host;
        private ILogger _appLogger;
        private ThemeManager _themeManager;
        private MainWindow _mainWindow;
        
        private LocalizationService _localizationService;
        private UserPreferencesService _preferencesService;
        private CacheService _cacheService;
        
        public static App Instance => (App)Current;
        public ApplicationSettings AppSettings => _preferencesService.AppSettings;
        public IServiceProvider Services => _host.Services;
        private ResourceDictionary LogicalResources => Resources.MergedDictionaries[0];
        
        private void PreventMultipleStartup()
        {
            Process currentProcess = Process.GetCurrentProcess();
            
            IEnumerable<Process> runningProcesses = Process.GetProcessesByName(currentProcess.ProcessName)
                .Where(process => process.Id != currentProcess.Id);

            bool alreadyRunning = runningProcesses.Any();
            
            if (alreadyRunning)
            {
                MessageBoxUtility.ShowWarningMessageBox();
                Shutdown();
            }
        }
        
        protected override async void OnStartup(StartupEventArgs eventArgs)
        {
            base.OnStartup(eventArgs);
            PreventMultipleStartup();
            
            LoggingConfigurator loggingConfigurator = new LoggingConfigurator();
            Log.Logger = loggingConfigurator.CreateLogger();
            _appLogger = Log.ForContext<App>();
            loggingConfigurator.AddBindingTraceListening();
            
            AppDomain.CurrentDomain.UnhandledException += OnAppDomainUnhandledException;
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            
            try
            {
                _appLogger.Information("Application is starting...");
                
                _preferencesService = new UserPreferencesService(LogicalResources);
                _cacheService = new CacheService();
                
                Task prefsServiceLaunchTask = _preferencesService.InitializeAsync();
                Task cacheServiceLaunchTask = _cacheService.InitializeAsync();
                
                await prefsServiceLaunchTask;
                
                _localizationService = new LocalizationService();
                SetNativeLanguage();
                _preferencesService.AppSettings.PropertyChanged += SetNativeLanguageIfNeed;
                _appLogger.Information("Localization service has been loaded successfully."); 
                
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
                _appLogger.Information("Verbs storage has been loaded successfully."); 
                
                _mainWindow = _host.Services.GetRequiredService<MainWindow>();
                _mainWindow.Show();
                
                SetBaseTheme();
                _preferencesService.AppSettings.PropertyChanged += SetBaseThemeIfNeed;
            }
            catch (Exception ex)
            {
                _appLogger.Fatal(ex, "An startup unhandled exception occured");
                MessageBoxUtility.ShowErrorMessageBox();
                Shutdown();
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
                .AddTransient<CheckPage>()
                .AddSingleton<ThemeManager>();
        }
        
        private void SetNativeLanguageIfNeed(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName == nameof(_preferencesService.AppSettings.NativeLanguage))
            {
                SetNativeLanguage();
            }
        }
        
        private void SetNativeLanguage()
        {
            _localizationService.CurrentLanguage = _preferencesService.AppSettings.NativeLanguage.ToString();
        }
        
        private void SetBaseThemeIfNeed(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName == nameof(_preferencesService.AppSettings.DarkTheme))
            {
                SetBaseTheme();
            }
        }
        
        private void SetBaseTheme()
        {
            _themeManager ??= _host.Services.GetRequiredService<ThemeManager>();
            _themeManager.SwitchBaseTheme(AppSettings.DarkTheme ? BaseTheme.Dark : BaseTheme.Light);
        }
        
        protected override async void OnExit(ExitEventArgs eventArgs)
        {
            _appLogger.Information("Application is shutting down...");
            await Log.CloseAndFlushAsync();
            
            await _host.StopAsync();
            _host.Dispose();
            
            _preferencesService.AppSettings.PropertyChanged -= SetBaseThemeIfNeed;
            _preferencesService.AppSettings.PropertyChanged -= SetNativeLanguageIfNeed;
            
            DispatcherUnhandledException -= OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException -= OnAppDomainUnhandledException;
            
            base.OnExit(eventArgs);
        }
        
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            _appLogger.Fatal(eventArgs.Exception, "An dispatcher unhandled exception occured");
            MessageBoxUtility.ShowErrorMessageBox();
        }

        private void OnAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            Exception exception = eventArgs.ExceptionObject as Exception;
            _appLogger.Fatal(exception, "An app domain unhandled exception occured");
            MessageBoxUtility.ShowErrorMessageBox();
        }
    }
}