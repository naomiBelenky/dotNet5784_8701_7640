﻿using System;
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
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Engineer Engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

        public bool isAddMode { get; }

        public EngineerWindow(int id = 0)
        {
            try
            {
                InitializeComponent();
                if (id == 0)
                {
                    SetValue(EngineerProperty, new BO.Engineer());
                    isAddMode = true;
                }
                else
                    try
                    {
                        BO.Engineer engineer = s_bl.Engineer.Read(id);
                        SetValue(EngineerProperty, engineer);
                        isAddMode = false;
                    }
                    catch (BO.BlDoesNotExistException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isAddMode)
                {
                    s_bl.Engineer.Add(Engineer);
                    MessageBox.Show("Engineer added successfully");
                }
                else
                {
                    s_bl.Engineer.Update(Engineer);
                    
                    MessageBox.Show("Engineer updated successfully");
                }
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error); }


        }
    }
}
