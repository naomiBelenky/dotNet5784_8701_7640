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
    /// Interaction logic for LittleTaskOfEngineer.xaml
    /// </summary>
    public partial class LittleTaskOfEngineer : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(LittleTaskOfEngineer), new PropertyMetadata(null));


        //באופן כללי צריך לדאוג שהכפתור של הוספת משימה יהיה רק במקרה שאין לי משימה
        //והכפתור של לראות את המשימה יהיה לי רק במקרה שיש משימה

        public LittleTaskOfEngineer(BO.Engineer currEng)
        {
            try
            {
                InitializeComponent();
                CurrentEngineer = currEng;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private void TaskDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new TaskWindow(false, CurrentEngineer.Task!.Id).Show();
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new TaskForList(() =>
                {
                    BO.Engineer tempEng = s_bl.Engineer.Read(CurrentEngineer.Id);
                    return s_bl?.Task.ReadAll(item => ((int)item.Difficulty <= (int)tempEng.Level) && item.Status == BO.Status.Scheduled && s_bl.Task.NodidntFinishLink(item))?? new List<BO.TaskInList>();
                    //מה עושים אם זה נאל? כאילו אם אין משימות שמתאימות לרמה שלו?
                    //עשיתי שזה מחזיר רשימה ריקה
                }, (BO.TaskInList task, TaskForList.Closer close, BO.Stage stage) =>
                {
                    HandleReturnedTask(task);
                    close();
                }, false).Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        public void HandleReturnedTask(BO.TaskInList task)
        {
            try
            {
                if (task == null) { throw new Exception("There is a problem in this task"); }
                BO.TaskInEngineer newTask = new BO.TaskInEngineer() { Id = task.Id, Name = task.Name };
                CurrentEngineer.Task = newTask;
                s_bl.Engineer.Update(CurrentEngineer);
                MessageBox.Show("You have a new task, Have pleasure and good luck!");
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }

        }
    }
}
