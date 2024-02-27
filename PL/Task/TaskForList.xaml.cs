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
        private BO.Stage stage { get; set; } = s_bl.getStage();
        private Window callingWindow;


        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskForList), new PropertyMetadata(null));

        private int newNextTask { get; set; } //for case that we here for add link to another task 
        public TaskForList(Window callingWindow, int id = 0, int engID = 0)
        {
            this.callingWindow = callingWindow;
            
            InitializeComponent();
            if (callingWindow is AdminWindow)
                TaskList = s_bl?.Task.ReadAll().OrderBy(t => t.Id)!;
            else if (callingWindow is PlanningTaskWindow)
                TaskList = s_bl?.Task.ReadAll(item => s_bl.Task.checkLink(item.Id, id)).OrderBy(t => t.Id)!;
            else if (callingWindow is LittleTaskOfEngineer) 
            {
                BO.Engineer tempEng = s_bl.Engineer.Read(engID);
                TaskList = s_bl?.Task.ReadAll(item => item.Difficulty <= tempEng.Level && item.Engineer == null && s_bl.Task.didntFinishLink(item));
                //מה עושים אם זה נאל? כאילו אם אין משימות שמתאימות לרמה שלו?

            }
        }
        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = ((status == BO.Status.All) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(task => task.Status == status)!)
                .OrderBy(t => t.Id);
        }

        void UpdateTaskList()
        {
            TaskList = ((status == BO.Status.All) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(task => task.Status == status)!)
                .OrderBy(t => t.Id);

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            BO.TaskInList? task = (sender as ListView)?.SelectedItem as BO.TaskInList;
            if (task == null) { /*exeption*/ }

            if (callingWindow is AdminWindow adminWindow)
            {
                if (stage == BO.Stage.Execution)
                    new TaskWindow(task!.Id).ShowDialog();
                else
                    new PlanningTaskWindow(task!.Id).ShowDialog();

                adminWindow.HandleReturnedTask(task);
            }
            else if (callingWindow is PlanningTaskWindow PlanningTaskWindow)
            {
                PlanningTaskWindow.HandleReturnedTask(task!);
                Close();
            }
            else if(callingWindow is LittleTaskOfEngineer littleTaskOfEngineer)
            {
                littleTaskOfEngineer.HandleReturnedTask(task!);
                Close();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new PlanningTaskWindow().ShowDialog();
            UpdateTaskList();
        }
    }
}
