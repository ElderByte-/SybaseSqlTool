using System.Windows;

namespace SybaseSqlTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            //DataContext = new MainViewModelMsSQL();
        }
    }
}
