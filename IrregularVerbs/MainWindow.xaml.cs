using System.Windows;
using System.Windows.Controls;
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
        private readonly IAbstractFactory<StartPage> _startPageFactory;
        private readonly IAbstractFactory<RevisePage> _revisePageFactory;
        private readonly IAbstractFactory<CheckPage> _checkPageFactory;
        
        public MainWindow(
            IAbstractFactory<StartPage> startPageFactory, 
            IAbstractFactory<RevisePage> revisePageFactory, 
            IAbstractFactory<CheckPage> checkPageFactory) 
        {
            _startPageFactory = startPageFactory;
            _revisePageFactory = revisePageFactory;
            _checkPageFactory = checkPageFactory;

            InitializeComponent();
            Loaded += OnWindowLoaded;
        }

        public void NavigateTo(Page page)
        {
            _mainFrame.Navigate(page);
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs args)
        {
            ShowMainPage();
        }

        private void ShowMainPage()
        {
            StartPage startPage = _startPageFactory.Create();
            /*startPage.OnDemandRevise += ShowRevisePage;
            startPage.OnDemandCheck += ShowCheckPage;*/
            _mainFrame.Navigate(startPage);
        }
        
        private void ShowRevisePage()
        {
            _mainFrame.Navigate(_revisePageFactory.Create());
        }

        private void ShowCheckPage()
        {
            _mainFrame.Navigate(_checkPageFactory.Create());
        }
        
    }
}