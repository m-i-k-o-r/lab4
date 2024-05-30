using System.Text;
using System.Windows;
using lab4_5.ViewModel;

namespace lab4_5
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();            
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = new MainViewModel(Dispatcher);
            DataContext = viewModel;
        }

        private void CreatNewBolidButton(object sender, RoutedEventArgs e)
        {
            viewModel.InitializeBolidAsync();
        }
    }    
}