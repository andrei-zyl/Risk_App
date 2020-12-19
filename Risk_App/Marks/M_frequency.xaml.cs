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
    public partial class M_frequency : Window
    {
        ObservableCollection<Mark_frequencyViewModel> Mark_frequencys;
        IMark_frequencyService Mark_frequencyService;

        public M_frequency()
        {
            InitializeComponent();
            ShowAll();
        }
        private void ShowAll()
        {
            Mark_frequencyService = new Mark_frequencyService("TestDbConnection");
            Mark_frequencys = Mark_frequencyService.GetAll();
            my_DataGrid_MF.DataContext = Mark_frequencys;
        }
        private void Add_Click_MF(object sender, RoutedEventArgs e)
        {
            Edit_MF dialog = new Edit_MF();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                Mark_frequencyService = new Mark_frequencyService("TestDbConnection");
                Mark_frequencyViewModel incident = dialog.WindowToModel();
                Mark_frequencyService.CreateMark_frequency(incident);
                dialog.Close();
            }
            ShowAll();
        }
        private void Edit_Click_MF(object sender, RoutedEventArgs e)
        {
            if (my_DataGrid_MF.SelectedItem != null)
            {
                Edit_MF dialog = new Edit_MF((Mark_frequencyViewModel)my_DataGrid_MF.SelectedItem);
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    Mark_frequencyService = new Mark_frequencyService("TestDbConnection");
                    Mark_frequencyViewModel incident = (Mark_frequencyViewModel)my_DataGrid_MF.SelectedItem;
                    incident.Mark_frequencyName = dialog.tb_Name_MF.Text;
                    incident.Mark_frequencyValue = Convert.ToDecimal(dialog.tb_Value_MF.Text);
                    Mark_frequencyService.UpdateMark_frequency(incident);
                    dialog.Close();
                }
                ShowAll();
            }
            else
            {
                MessageBox.Show("Выберите объект для внесения изменений!", "Обратите внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ShowAll_Click_MF(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }
    }
}
