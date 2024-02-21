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

        //private BO.Stage stage { get; set; } = s_bl.getStage();   //לא יודעת אם לשים את זה כאן או  ברשימת משימות... נראלי כאן יותר מתאים

        //private BO.Stage stage { get; set; } = BO.Stage.Planning;  //צריך לעשות שכשקובעים לו"ז זה ישתנה לשלב הביצוע
        public TaskWindow(int id = 0)
        {
            InitializeComponent();
            if (id == 0)
            {
                SetValue(CurrentTaskProperty, new BO.Task());
                isAddMode = true;
            }
            else
                try
                {
                    BO.Task task = s_bl.Task.Read(id);
                    SetValue(CurrentTaskProperty, task);
                    isAddMode = false;
                }
                catch (BO.BlDoesNotExistException ex)
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
                string message = ex.Message + " " + ex.GetType() + " ";
                if (ex.InnerException != null)
                    message += "Dal Exception";

                MessageBox.Show(message);
            }

        }
        private void addDependencyBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
