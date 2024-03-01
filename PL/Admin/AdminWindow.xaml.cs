using BlApi;
using PL.Admin;
using PL.Engineer;
using PL.Task;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void EngBtn_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }

        private void TaskBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskForList taskForListWindow = new TaskForList(this);
            taskForListWindow.Show();
        }

        private void btnInit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to initialize data?", "Yes", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Factory.Get().InitializeDB();
        }

        private void resetDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset data?", "Yes", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Factory.Get().ResetDB();
        }
        public void HandleReturnedTask(BO.TaskInList task)
        {

        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            //try {
                new ProjectStartDate().Show();
            //    s_bl.automaticSchedule();
            //    MessageBox.Show("scheduled all tasks succesfully");
            //}
            //catch (BO.BlDoesNotExistException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
