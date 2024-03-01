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
            try
            {
                InitializeComponent();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new EngineerView().ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new AdminWindow().Show();

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }

}