using System.Windows;
using System.Windows.Controls;

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
        }

        public void NavigateTo(Page page)
        {
            _mainFrame.Navigate(page);
        }
    }
}