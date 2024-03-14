using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Task Task
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

        private bool isAddMode { get; set; }
        public bool adminNotEngineer { get; set; }

        //private BO.Stage stage { get; set; } = BO.Stage.Planning;  //צריך לעשות שכשקובעים לו"ז זה ישתנה לשלב הביצוע
        public TaskWindow(bool adminNotEngineer = true, int id = 0, bool isAddLinkMode = false)
        {
            try
            {
                this.adminNotEngineer = adminNotEngineer;
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

        private void finishBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Task.FinishDate = s_bl.Clock;
                Button button = (Button)sender;
                button.IsEnabled = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
