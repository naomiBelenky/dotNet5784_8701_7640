using BlApi;
using PL.Engineer;
using PL.Task;
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
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public DateTime Time
        {
            get { return (DateTime)GetValue(CurrentTime); }
            set { SetValue(CurrentTime, value); }
        }

        public static readonly DependencyProperty CurrentTime =
            DependencyProperty.Register("Time", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(null));

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Time = s_bl.InitTime();
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

        private void btnAddYear_Click(object sender, RoutedEventArgs e)
        {
            s_bl.PromoteYear();
            Time = Time.AddYears(1);
        }

        private void btnAddDay_Click(object sender, RoutedEventArgs e)
        {
            s_bl.PromoteDay();
            Time = Time.AddDays(1);
        }

        private void btnAddHour_Click(object sender, RoutedEventArgs e)
        {
            s_bl.PromoteHour();
            Time = Time.AddHours(1);
        }

        private void btnInitTime_Click(object sender, RoutedEventArgs e)
        {
            Time = s_bl.InitTime();
        }
    }

}