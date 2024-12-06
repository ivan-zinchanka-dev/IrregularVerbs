using System.Windows;
using System.Windows.Controls;

namespace IrregularVerbs
{
    public partial class MainWindow : Window
    {
        public MainWindow() 
        {
            InitializeComponent();
        }

        public void NavigateTo(Page page)
        {
            _mainFrame.Navigate(page);
        }
        
        /*private void OnNavigate(object sender, NavigatingCancelEventArgs e)
        {
            e.Cancel = true;        // TODO Do something with back and forward user events
        }*/
    }
}