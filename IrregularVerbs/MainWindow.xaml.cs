using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace IrregularVerbs
{
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow() 
        {
            InitializeComponent();
        }

        public void NavigateTo(Page page)
        {
            Navigate(page);
        }
    }
}