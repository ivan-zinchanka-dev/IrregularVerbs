using System.Windows;
using IrregularVerbs.CodeBase;
using IrregularVerbs.CodeBase.AbstractFactory;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.ViewPresenters;

namespace IrregularVerbs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IAbstractFactory<StartPage> _startPageFactory;
        
        public MainWindow(IAbstractFactory<StartPage> startPageFactory)
        {
            _startPageFactory = startPageFactory;
            InitializeComponent();
            Loaded += OnWindowLoaded;

            /*Closed += (a, b) =>
            {
                Application.Current.Shutdown();

            };*/
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs args)
        {
            ShowMainPage();
        }

        private void ShowMainPage()
        {
            StartPage startPage = _startPageFactory.Create();
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