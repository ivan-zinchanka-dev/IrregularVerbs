using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using IrregularVerbs.Models.Configs;

namespace IrregularVerbs
{
    public partial class MainWindow : Window
    {
        private readonly ApplicationSettings _applicationSettings;
        
        public event NavigatingCancelEventHandler Navigating
        {
            add => _mainFrame.Navigating += value;
            remove => _mainFrame.Navigating -= value;
        }
        
        // TODO Add dark mode
        
        public MainWindow(ApplicationSettings appSettings)
        {
            _applicationSettings = appSettings;
            DataContext = _applicationSettings;
            
            InitializeComponent();
        }
        
        public void NavigateTo(Page page)
        {
            _mainFrame.Navigate(page);
        }
    }
}