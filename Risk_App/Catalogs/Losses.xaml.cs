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

namespace Risk_App
{
    public partial class Losses : Window
    {
        ObservableCollection<LossViewModel> Losses1;
        ILossService LossService;

        public Losses()
        {
            InitializeComponent();
            ShowAll();
        }
        private void ShowAll()
        {
            LossService = new LossService("TestDbConnection");
            Losses1 = LossService.GetAll();
            my_DataGrid_Loss.DataContext = Losses1;
        }
        private void Add_Click_Loss(object sender, RoutedEventArgs e)
        {
            Edit_Loss dialog = new Edit_Loss();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                LossService = new LossService("TestDbConnection");
                LossViewModel incident = dialog.WindowToModel();
                LossService.CreateLoss(incident);
                dialog.Close();
            }
            ShowAll();
        }
        private void Edit_Click_Loss(object sender, RoutedEventArgs e)
        {
            if (my_DataGrid_Loss.SelectedItem != null)
            {
                Edit_Loss dialog = new Edit_Loss((LossViewModel)my_DataGrid_Loss.SelectedItem);
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    LossService = new LossService("TestDbConnection");
                    LossViewModel incident = (LossViewModel)my_DataGrid_Loss.SelectedItem;
                    incident.LossName = dialog.tb_Name_Loss.Text;
                    LossService.UpdateLoss(incident);
                    dialog.Close();
                }
                ShowAll();
            }
            else
            {
                MessageBox.Show("Выберите тип потерь для внесения изменений!", "Обратите внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ShowAll_Click_Loss(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }
    }
}
