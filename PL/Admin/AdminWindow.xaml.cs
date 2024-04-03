using BlApi;
using BO;
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

        public BO.Stage stage { get; set; } = s_bl.getStage();

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void EngBtn_Click(object sender, RoutedEventArgs e)
        {
            try { new EngineerListWindow().Show(); }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void TaskBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TaskForList taskForListWindow = new TaskForList(() => { return s_bl.Task.ReadAll(); },
                    (BO.TaskInList task, TaskForList.Closer close, BO.Stage stage) =>
                    {
                        if (stage == BO.Stage.Execution)
                            new TaskWindow(true, task!.Id).ShowDialog();
                        else
                            new PlanningTaskWindow(task.Id).ShowDialog();

                        HandleReturnedTask(task);
                    }, true);
                taskForListWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void btnInit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to initialize data?", "Yes", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    Factory.Get().InitializeDB();
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void resetDB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to reset data?", "Yes", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    Factory.Get().ResetDB();
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        public void HandleReturnedTask(BO.TaskInList task)
        {

        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new ProjectStartDate().Show();
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private void GantButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new Gantt().Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
