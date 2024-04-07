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
            try
            {
                InitializeComponent();
                CurrentEngineer = new BO.Engineer();
                //SetValue(CurrentEngineerProperty, new BO.Engineer());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentEngineer = s_bl.Engineer.Read(CurrentEngineer.Id);
                new LittleTaskOfEngineer(CurrentEngineer).Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }

            Close();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only numeric input
            e.Handled = !char.IsDigit(e.Text, e.Text.Length-1);
        }

    }
}
