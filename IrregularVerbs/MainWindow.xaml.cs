using System.Windows;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.ViewPresenters;

namespace IrregularVerbs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationSettings _applicationSettings;
        
        public MainWindow(ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
            InitializeComponent();
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs args)
        {
            ShowMainPage();
        }

        private void ShowMainPage()
        {
            StartPage startPage = new StartPage(_applicationSettings);
            startPage.OnDemandRevise += ShowRevisePage;
            startPage.OnDemandCheck += ShowCheckPage;
            _mainFrame.Navigate(startPage);
        }
        
        private void ShowRevisePage()
        {
            RevisePage revisePage = new RevisePage();
            _mainFrame.Navigate(revisePage);
        }

        private void ShowCheckPage()
        {
            CheckPage checkPage = new CheckPage();
            _mainFrame.Navigate(checkPage);
        }
        
    }
}