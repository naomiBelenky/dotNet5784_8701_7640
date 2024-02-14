using BlApi;
using PL.Engineer;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
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

        private void btnEngineers_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();

        }

        private void btnInit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to initialize data?", "Yes", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Factory.Get().InitializeDB();
        }

        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void resetDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to initialize data?", "Yes", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Factory.Get().ResetDB();
        }
    }
}