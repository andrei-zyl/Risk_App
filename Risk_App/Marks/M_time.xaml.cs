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
using ViewModel.Interfaces;
using ViewModel.Models;
using ViewModel.Services;
using System.Collections.ObjectModel;

namespace Risk_App.Marks
{
    public partial class M_time : Window
    {
        ObservableCollection<Mark_timeViewModel> Mark_times;
        IMark_timeService Mark_timeService;

        public M_time()
        {
            InitializeComponent();
            ShowAll();
        }
        private void ShowAll()
        {
            Mark_timeService = new Mark_timeService("TestDbConnection");
            Mark_times = Mark_timeService.GetAll();
            my_DataGrid_MT.DataContext = Mark_times;
        }
        private void Add_Click_MT(object sender, RoutedEventArgs e)
        {
            Edit_MT dialog = new Edit_MT();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                Mark_timeService = new Mark_timeService("TestDbConnection");
                Mark_timeViewModel incident = dialog.WindowToModel();
                Mark_timeService.CreateMark_time(incident);
                dialog.Close();
            }
            ShowAll();
        }
        private void Edit_Click_MT(object sender, RoutedEventArgs e)
        {
            if (my_DataGrid_MT.SelectedItem != null)
            {
                Edit_MT dialog = new Edit_MT((Mark_timeViewModel)my_DataGrid_MT.SelectedItem);
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    Mark_timeService = new Mark_timeService("TestDbConnection");
                    Mark_timeViewModel incident = (Mark_timeViewModel)my_DataGrid_MT.SelectedItem;
                    incident.Mark_timeName = dialog.tb_Name_MT.Text;
                    incident.Mark_timeValue = Convert.ToDecimal(dialog.tb_Value_MT.Text);
                    Mark_timeService.UpdateMark_time(incident);
                    dialog.Close();
                }
                ShowAll();
            }
            else
            {
                MessageBox.Show("Выберите объект для внесения изменений!", "Обратите внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ShowAll_Click_MT(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }
    }
}
