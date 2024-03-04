using BlApi;
using PL.Engineer;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskForList.xaml
    /// </summary>
    public partial class TaskForList : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status status { get; set; } = BO.Status.All;
        public BO.Stage stage { get { return s_bl.getStage(); } }

        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskForList), new PropertyMetadata(null));

        private int newNextTask { get; set; } //for case that we here for add link to another task 

        public delegate IEnumerable<BO.TaskInList>? TaskListGetter();

        public delegate void Closer();

        public delegate void HandleReturnedTask(BO.TaskInList task, Closer closer, BO.Stage stage);

        HandleReturnedTask handleReturnedTask;

        public TaskForList(TaskListGetter? taskListGetter, HandleReturnedTask handleReturnedTask)
        {
            try 
            { 
                this.handleReturnedTask = handleReturnedTask;
            
                InitializeComponent();
                TaskList = taskListGetter().OrderBy(t => t.Id);
            }            
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TaskList = ((status == BO.Status.All) ?
                 s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(task => task.Status == status)!)
                 .OrderBy(t => t.Id);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }

        }

        void UpdateTaskList()
        {
            try
            {
                TaskList = ((status == BO.Status.All) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(task => task.Status == status)!)
                .OrderBy(t => t.Id);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
         

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listView_DoubleClick(object sender, EventArgs e)
        { 
            BO.TaskInList? task = (sender as ListView)?.SelectedItem as BO.TaskInList;
            if (task == null) { /*exeption*/ }

            handleReturnedTask(task, Close, stage);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new PlanningTaskWindow().ShowDialog();
            UpdateTaskList();
        }
    }
}
