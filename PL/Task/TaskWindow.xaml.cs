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

        public bool isAddMode { get; }

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
                    message += "Dal Exepttion";

                MessageBox.Show(message);
            }

        }
    }




}
