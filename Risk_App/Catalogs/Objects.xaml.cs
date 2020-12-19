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
    public partial class Objects : Window
    {
        ObservableCollection<ObjectRiskViewModel> Objectrisks;
        IObjectRiskService ObjectriskService;

        public Objects()
        {
            InitializeComponent();
            ShowAll();
        }
        private void ShowAll()
        {
            ObjectriskService = new ObjectRiskService("TestDbConnection");
            Objectrisks = ObjectriskService.GetAll();
            my_DataGrid_OR.DataContext = Objectrisks;
        }
        private void Add_Click_OR(object sender, RoutedEventArgs e)
        {
            Edit_OR dialog = new Edit_OR();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                ObjectriskService = new ObjectRiskService("TestDbConnection");
                ObjectRiskViewModel incident = dialog.WindowToModel();
                ObjectriskService.CreateObjectRisk(incident);
                dialog.Close();
            }
            ShowAll();
        }
        private void Edit_Click_OR(object sender, RoutedEventArgs e)
        {
            if (my_DataGrid_OR.SelectedItem != null)
            {
                Edit_OR dialog = new Edit_OR((ObjectRiskViewModel)my_DataGrid_OR.SelectedItem);
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    ObjectriskService = new ObjectRiskService("TestDbConnection");
                    ObjectRiskViewModel incident = (ObjectRiskViewModel)my_DataGrid_OR.SelectedItem;
                    incident.ObjectRiskName = dialog.tb_Name_OR.Text;
                    ObjectriskService.UpdateObjectRisk(incident);
                    dialog.Close();
                }
                ShowAll();
            }
            else
            {
                MessageBox.Show("Выберите объект риска для внесения изменений!", "Обратите внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ShowAll_Click_OR(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }
    }
}
