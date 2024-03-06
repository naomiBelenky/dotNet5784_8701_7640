using System.Collections.ObjectModel;
using System.Windows;


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
        public bool withPermissions { get; set; }

        //private BO.Stage stage { get; set; } = BO.Stage.Planning;  //צריך לעשות שכשקובעים לו"ז זה ישתנה לשלב הביצוע
        public TaskWindow(bool withPermissions, int id = 0, bool isAddLinkMode = false)
        {
            try
            {
                this.withPermissions = withPermissions;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
