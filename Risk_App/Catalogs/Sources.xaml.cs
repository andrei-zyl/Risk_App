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
    public partial class Sources : Window
    {
        ObservableCollection<SourceRiskViewModel> sourcerisks;
        ISourceRiskService sourceriskService;
        
        public Sources()
        {
            InitializeComponent();
            ShowAll();
        }
        private void ShowAll()
        {
            sourceriskService = new SourceRiskService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            my_DataGrid_SR.DataContext = sourcerisks;
        }
        private void Add_Click_SR(object sender, RoutedEventArgs e)
        {
            Edit_SR dialog = new Edit_SR();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                sourceriskService = new SourceRiskService("TestDbConnection");
                SourceRiskViewModel incident = dialog.WindowToModel();
                sourceriskService.CreateSourceRisk(incident);
                dialog.Close();
            }
            ShowAll();
        }
        private void Edit_Click_SR(object sender, RoutedEventArgs e)
        {
            if (my_DataGrid_SR.SelectedItem != null)
            {
                Edit_SR dialog = new Edit_SR((SourceRiskViewModel)my_DataGrid_SR.SelectedItem);
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    sourceriskService = new SourceRiskService("TestDbConnection");
                    SourceRiskViewModel incident = (SourceRiskViewModel)my_DataGrid_SR.SelectedItem;
                    incident.SourceRiskName = dialog.tb_Name_SR.Text;
                    sourceriskService.UpdateSourceRisk(incident);
                    dialog.Close();
                }
                ShowAll();
            }
            else
            {
                MessageBox.Show("Выберите источник риска для внесения изменений!", "Обратите внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ShowAll_Click_SR(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }
    }
}
