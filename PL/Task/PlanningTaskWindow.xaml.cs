using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace PL.Task
{
    /// <summary>
    /// Interaction logic for PlanningTaskWindow.xaml
    /// </summary>
    public partial class PlanningTaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Task Task
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(PlanningTaskWindow), new PropertyMetadata(null));

        private bool isAddMode { get; set; }
        public PlanningTaskWindow(int id = 0)
        {
            try
            {
                InitializeComponent();
                if (id == 0)
                {
                    SetValue(CurrentTaskProperty, new BO.Task());
                    isAddMode = true;
                }
                else
                {
                    BO.Task task = s_bl.Task.Read(id);
                    SetValue(CurrentTaskProperty, task);
                    isAddMode = false;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }


        }

        private void AddOrUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isAddMode)
                {
                    s_bl.Task.Add(Task);
                    MessageBox.Show("Task added successfully:)");
                }
                else
                {
                    s_bl.Task.Update(Task);

                    MessageBox.Show("Task updated successfully:)");
                }
                this.Close();
            }

            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }


        }
        private void addDependencyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TaskForList taskForListWindow = new TaskForList(() =>
                {
                    return s_bl.Task.ReadAll(item => s_bl.Task.checkLink(item.Id, Task.Id));
                }, (BO.TaskInList task, TaskForList.Closer close, BO.Stage stage) =>
                {
                    HandleReturnedTask(task);
                    close();
                }, false);
                taskForListWindow.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        public void HandleReturnedTask(BO.TaskInList task)
        {
            try
            {
                if (Task.Links == null) Task.Links = new List<BO.TaskInList>();
                if (!Task.Links.Exists(item => item.Id == task.Id))
                    Task.Links.Add(task);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void listBox_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                BO.TaskInList? task = (sender as ListBox)?.SelectedItem as BO.TaskInList;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this dependency?", "Yes", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    Task.Links!.Remove(task!);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
