using System.Windows;
using IrregularVerbs.ViewPresenters;

namespace IrregularVerbs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs args)
        {
            ShowMainPage();
        }

        private void ShowMainPage()
        {
            StartPage startPage = new StartPage();
            startPage.OnDemandRevise += ShowRevisePage;
            startPage.OnDemandCheck += ShowCheckPage;
            _mainFrame.Navigate(startPage);
        }
        
        private void ShowRevisePage()
        {
            RevisePage revisePage = new RevisePage();
            //revisePage.OnDemandBack += ShowMainPage;
            _mainFrame.Navigate(revisePage);
        }

        private void ShowCheckPage()
        {
            CheckPage checkPage = new CheckPage();
            //revisePage.OnDemandBack += ShowMainPage;
            _mainFrame.Navigate(checkPage);
        }



    }
}