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
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        public BO.Level level { get; set; } = BO.Level.All;

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));


        public EngineerListWindow()
        {
            try
            {
                InitializeComponent();
                EngineerList = s_bl?.Engineer.ReadAll()!;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //the user can filter the engineers on the screens according to their level
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                EngineerList = ((level == BO.Level.All) ?
                   s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(eng => eng.Level == level)!)
                   .OrderBy(e => e.Id); // sort by ID so it will be easier to find the engineer in the list as a human
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        void UpdateEngineerList()
        {
            try
            {
                EngineerList = ((level == BO.Level.All) ?
                    s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(eng => eng.Level == level)!)
                    .OrderBy(e => e.Id); // sort by ID so it will be easier to find the engineer in the list as a human
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void listView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;

                if (engineer == null)
                {
                    MessageBox.Show("No engineer was selected"); Close(); return;
                }

                new EngineerWindow(engineer!.Id).ShowDialog();
                UpdateEngineerList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new EngineerWindow().ShowDialog();
                UpdateEngineerList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
