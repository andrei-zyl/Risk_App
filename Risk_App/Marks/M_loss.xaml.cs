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
    public partial class M_loss : Window
    {
        ObservableCollection<Mark_lossViewModel> Mark_losss;
        IMark_lossService Mark_lossService;

        public M_loss()
        {
            InitializeComponent();
            ShowAll();
        }
        private void ShowAll()
        {
            Mark_lossService = new Mark_lossService("TestDbConnection");
            Mark_losss = Mark_lossService.GetAll();
            my_DataGrid_ML.DataContext = Mark_losss;
        }
        private void Add_Click_ML(object sender, RoutedEventArgs e)
        {
            Edit_ML dialog = new Edit_ML();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                Mark_lossService = new Mark_lossService("TestDbConnection");
                Mark_lossViewModel incident = dialog.WindowToModel();
                Mark_lossService.CreateMark_loss(incident);
                dialog.Close();
            }
            ShowAll();
        }
        private void Edit_Click_ML(object sender, RoutedEventArgs e)
        {
            if (my_DataGrid_ML.SelectedItem != null)
            {
                Edit_ML dialog = new Edit_ML((Mark_lossViewModel)my_DataGrid_ML.SelectedItem);
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    Mark_lossService = new Mark_lossService("TestDbConnection");
                    Mark_lossViewModel incident = (Mark_lossViewModel)my_DataGrid_ML.SelectedItem;
                    incident.Mark_lossName = dialog.tb_Name_ML.Text;
                    incident.Mark_lossValue = Convert.ToDecimal(dialog.tb_Value_ML.Text);
                    Mark_lossService.UpdateMark_loss(incident);
                    dialog.Close();
                }
                ShowAll();
            }
            else
            {
                MessageBox.Show("Выберите объект для внесения изменений!", "Обратите внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ShowAll_Click_ML(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }
    }
}
