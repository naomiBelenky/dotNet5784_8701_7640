using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for Gantt.xaml
    /// </summary>
    public partial class Gantt : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        //public ObservableCollection<BO.Task> observeListTask
        //{
        //    get { return (ObservableCollection<BO.Task>)GetValue(observeListTaskProperty); }
        //    set { SetValue(observeListTaskProperty, value); }
        //}

        //public static readonly DependencyProperty observeListTaskProperty =
        //    DependencyProperty.Register("observeListTask", typeof(ObservableCollection<BO.Task>), typeof(MainWindow), new PropertyMetadata(null));


        public List<BO.Task> ListOfTasks
        {
            get { return (List<BO.Task>)GetValue(ListOfTasksProperty); }
            set { SetValue(ListOfTasksProperty, value); }
        }

        public static readonly DependencyProperty ListOfTasksProperty =
            DependencyProperty.Register("ListOfTasks", typeof(List<BO.Task>), typeof(Gantt), new PropertyMetadata(null));


        public Gantt()
        {
            try
            {
                InitializeComponent();
                ListOfTasks = new List<BO.Task>();

                List<BO.TaskInList> listT = s_bl.Task.ReadAll().ToList();
                ListOfTasks = (from item in listT
                               select s_bl.Task.Read(item.Id)).ToList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }    
    }
}
