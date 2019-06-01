using System.Windows;
using WebDataProvider.ViewModels;

namespace WebDataProvider
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, System.EventArgs e)
        {
            (DataContext as MainWindowViewModel).Driver.Close();
            (DataContext as MainWindowViewModel).Driver.Dispose();
        }
    }
}
