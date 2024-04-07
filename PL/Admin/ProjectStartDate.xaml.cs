using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for ProjectStartDate.xaml
    /// </summary>
    public partial class ProjectStartDate : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public static readonly DependencyProperty DateProperty =
              DependencyProperty.Register("Date", typeof(DateTime), typeof(ProjectStartDate), new PropertyMetadata(null));

        // Create a property wrapper for the dependency property
        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }
        public ProjectStartDate()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              
                s_bl.saveStartDate(Date);

                s_bl.automaticSchedule();
                MessageBox.Show("scheduled all tasks succesfully");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }

            Close();
        }
    }
}
