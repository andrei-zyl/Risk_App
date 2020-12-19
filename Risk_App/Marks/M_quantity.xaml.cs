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
    public partial class M_quantity : Window
    {
        ObservableCollection<Mark_quantityViewModel> Mark_quantitys;
        IMark_quantityService Mark_quantityService;

        public M_quantity()
        {
            InitializeComponent();
            ShowAll();
        }
        private void ShowAll()
        {
            Mark_quantityService = new Mark_quantityService("TestDbConnection");
            Mark_quantitys = Mark_quantityService.GetAll();
            my_DataGrid_MQ.DataContext = Mark_quantitys;
        }
        private void Add_Click_MQ(object sender, RoutedEventArgs e)
        {
            Edit_MQ dialog = new Edit_MQ();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                Mark_quantityService = new Mark_quantityService("TestDbConnection");
                Mark_quantityViewModel incident = dialog.WindowToModel();
                Mark_quantityService.CreateMark_quantity(incident);
                dialog.Close();
            }
            ShowAll();
        }
        private void Edit_Click_MQ(object sender, RoutedEventArgs e)
        {
            if (my_DataGrid_MQ.SelectedItem != null)
            {
                Edit_MQ dialog = new Edit_MQ((Mark_quantityViewModel)my_DataGrid_MQ.SelectedItem);
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    Mark_quantityService = new Mark_quantityService("TestDbConnection");
                    Mark_quantityViewModel incident = (Mark_quantityViewModel)my_DataGrid_MQ.SelectedItem;
                    incident.Mark_quantityName = dialog.tb_Name_MQ.Text;
                    incident.Mark_quantityValue = Convert.ToDecimal(dialog.tb_Value_MQ.Text);
                    Mark_quantityService.UpdateMark_quantity(incident);
                    dialog.Close();
                }
                ShowAll();
            }
            else
            {
                MessageBox.Show("Выберите объект для внесения изменений!", "Обратите внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ShowAll_Click_MQ(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }
    }
}
