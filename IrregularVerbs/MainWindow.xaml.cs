using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace IrregularVerbs
{
    public partial class MainWindow : Window
    {
        public event NavigatingCancelEventHandler Navigating
        {
            add => _mainFrame.Navigating += value;
            remove => _mainFrame.Navigating -= value;
        }
        
        public MainWindow() 
        {
            InitializeComponent();
        }
        
        public void NavigateTo(Page page)
        {
            _mainFrame.Navigate(page);
        }
    }
}