using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services.Main;
using IrregularVerbs.Views;

namespace IrregularVerbs
{
    public partial class MainWindow : Window
    {
        private readonly ApplicationSettings _applicationSettings;
        private readonly PageManager _pageManager;
        
        public MainWindow(ApplicationSettings appSettings, PageManager pageManager)
        {
            _applicationSettings = appSettings;
            _pageManager = pageManager;
            DataContext = _applicationSettings;
            
            InitializeComponent();
            
            Loaded += OnLoaded;
            _mainFrame.Navigating += OnNavigating;
            _pageManager.OnPageCreated += NavigateTo;
            
            Unloaded += OnUnloaded;
        }
        
        private void OnLoaded(object sender, RoutedEventArgs eventArgs)
        {
            _pageManager.SwitchTo<StartPage>();
        }
        
        private void OnNavigating(object sender, NavigatingCancelEventArgs eventArgs)
        {
            if (eventArgs.NavigationMode == NavigationMode.Forward || 
                eventArgs.NavigationMode == NavigationMode.Back)
            {
                eventArgs.Cancel = true;
            }
        }
        
        private void NavigateTo(Page page)
        {
            _mainFrame.Navigate(page);
        }
        
        private void OnUnloaded(object sender, RoutedEventArgs eventArgs)
        {
            Loaded -= OnLoaded;
            _mainFrame.Navigating -= OnNavigating;
            _pageManager.OnPageCreated -= NavigateTo;
            
            Unloaded -= OnUnloaded;
        }
    }
}