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
    /// Interaction logic for EngineerView.xaml
    /// </summary>
    /// 

    public partial class EngineerView : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerView), new PropertyMetadata(null));
 
        public EngineerView()
        {
            InitializeComponent();
            SetValue(CurrentEngineerProperty, new BO.Engineer());
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //CurrentEngineer=new BO.Engineer();

                CurrentEngineer = s_bl.Engineer.Read(CurrentEngineer.Id);
                new LittleTaskOfEngineer(CurrentEngineer).Show();
                //BO.TaskInEngineer? taskOfCurrentEngineer = s_bl.Engineer.Read(Engineer.Id).Task;

                //if (taskOfCurrentEngineer == null)
                //    new LittleTaskOfEngineer().Show();
                ////MessageBox.Show("You are still not responsible for any tasks");
                //else
                //    idOfTaskOfCurrentEngineer = taskOfCurrentEngineer.Id;
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
                       
            //לפתוח חלון שמראה את המשימה הנוכחית ולשלוח לו את הת"ז של המהנדס
            //אולי לעשות עוד פעם חלונות נפרדים לשלב תכנון ולשלב ביצוע?
            Close();
        }
    }
}
