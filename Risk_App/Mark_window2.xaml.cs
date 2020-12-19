using System;
using System.Windows;
using System.Collections.ObjectModel;
using ViewModel.Models;
using ViewModel.Interfaces;
using ViewModel.Services;

namespace Risk_App
{
    public partial class Mark_window2 : Window
    {
        decimal loss_, frequency_, quantity_, time_;
        decimal total;
        int w;
        public Mark_window2()
        {
            InitializeComponent();
            ModelToWindow2();
        }
        public Mark_window2(IncidentViewModel incidentVM)
        {
            InitializeComponent();
            ModelToWindow(incidentVM);
        }

        public void ModelToWindow(IncidentViewModel incidentVM)
        {
            ObservableCollection<Mark_frequencyViewModel> Mark_frequencys;
            ObservableCollection<Mark_lossViewModel> Mark_losss;
            ObservableCollection<Mark_quantityViewModel> Mark_quantitys;
            ObservableCollection<Mark_timeViewModel> Mark_times;
            IMark_frequencyService Mark_frequencyService = new Mark_frequencyService("TestDbConnection");
            IMark_lossService Mark_lossService = new Mark_lossService("TestDbConnection");
            IMark_quantityService Mark_quantityService = new Mark_quantityService("TestDbConnection");
            IMark_timeService Mark_timeService = new Mark_timeService("TestDbConnection");
            Mark_frequencys = Mark_frequencyService.GetAll();
            Mark_losss = Mark_lossService.GetAll();
            Mark_quantitys = Mark_quantityService.GetAll();
            Mark_times = Mark_timeService.GetAll();
            cb_Loss.DataContext = Mark_losss;
            cb_Frequency.DataContext = Mark_frequencys;
            cb_Quantity.DataContext = Mark_quantitys;
            cb_Time.DataContext = Mark_times;           
            int a = 0, b = 0, c = 0, d = 0;
            foreach (var i in Mark_losss)
            {
                if (i.Mark_lossId == incidentVM.Mark_lossId)
                { cb_Loss.SelectedIndex = a; }
                a++;
            }
            foreach (var i in Mark_frequencys)
            {
                if (i.Mark_frequencyId == incidentVM.Mark_frequencyId)
                { cb_Frequency.SelectedIndex = b; }
                b++;
            }
            foreach (var i in Mark_times)
            {
                if (i.Mark_timeId == incidentVM.Mark_timeId)
                { cb_Time.SelectedIndex = c; }
                c++;
            }
            foreach (var i in Mark_quantitys)
            {
                if (i.Mark_quantityId == incidentVM.Mark_quantityId)
                { cb_Quantity.SelectedIndex = d; }
                d++;
            }         
        }

        public void ModelToWindow2()
        {
            ObservableCollection<Mark_frequencyViewModel> Mark_frequencys;
            ObservableCollection<Mark_lossViewModel> Mark_losss;
            ObservableCollection<Mark_quantityViewModel> Mark_quantitys;
            ObservableCollection<Mark_timeViewModel> Mark_times;
            IMark_frequencyService Mark_frequencyService = new Mark_frequencyService("TestDbConnection");
            IMark_lossService Mark_lossService = new Mark_lossService("TestDbConnection");
            IMark_quantityService Mark_quantityService = new Mark_quantityService("TestDbConnection");
            IMark_timeService Mark_timeService = new Mark_timeService("TestDbConnection");
            Mark_frequencys = Mark_frequencyService.GetAll();
            Mark_losss = Mark_lossService.GetAll();
            Mark_quantitys = Mark_quantityService.GetAll();
            Mark_times = Mark_timeService.GetAll();
            cb_Loss.DataContext = Mark_losss;
            cb_Frequency.DataContext = Mark_frequencys;
            cb_Quantity.DataContext = Mark_quantitys;
            cb_Time.DataContext = Mark_times;
            cb_Loss.SelectedIndex = 0;           
            cb_Frequency.SelectedIndex = 0;
            cb_Time.SelectedIndex = 0;
            cb_Quantity.SelectedIndex = 0;
        }

        public void bCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void count()
        {
            ObservableCollection<Mark_frequencyViewModel> Mark_frequencys;
            ObservableCollection<Mark_lossViewModel> Mark_losss;
            ObservableCollection<Mark_quantityViewModel> Mark_quantitys;
            ObservableCollection<Mark_timeViewModel> Mark_times;
            IMark_frequencyService Mark_frequencyService = new Mark_frequencyService("TestDbConnection");
            IMark_lossService Mark_lossService = new Mark_lossService("TestDbConnection");
            IMark_quantityService Mark_quantityService = new Mark_quantityService("TestDbConnection");
            IMark_timeService Mark_timeService = new Mark_timeService("TestDbConnection");
            Mark_frequencys = Mark_frequencyService.GetAll();
            Mark_losss = Mark_lossService.GetAll();
            Mark_quantitys = Mark_quantityService.GetAll();
            Mark_times = Mark_timeService.GetAll();

            switch (cb_Loss.SelectedIndex)
            {
                case 0:
                    loss_ = Mark_losss[0].Mark_lossValue;
                    break;
                case 1:
                    loss_ = Mark_losss[1].Mark_lossValue;
                    break;
                case 2:
                    loss_ = Mark_losss[2].Mark_lossValue;
                    break;
                case 3:
                    loss_ = Mark_losss[3].Mark_lossValue;
                    break;
                case 4:
                    loss_ = Mark_losss[4].Mark_lossValue;
                    break;
            }
            switch (cb_Frequency.SelectedIndex)
            {
                case 0:
                    frequency_ = Mark_frequencys[0].Mark_frequencyValue;
                    break;
                case 1:
                    frequency_ = Mark_frequencys[1].Mark_frequencyValue;
                    break;
                case 2:
                    frequency_ = Mark_frequencys[2].Mark_frequencyValue;
                    break;
                case 3:
                    frequency_ = Mark_frequencys[3].Mark_frequencyValue;
                    break;
                case 4:
                    frequency_ = Mark_frequencys[4].Mark_frequencyValue;
                    break;
            }
            switch (cb_Quantity.SelectedIndex)
            {
                case 0:
                    quantity_ = Mark_quantitys[0].Mark_quantityValue;
                    break;
                case 1:
                    quantity_ = Mark_quantitys[1].Mark_quantityValue;
                    break;
                case 2:
                    quantity_ = Mark_quantitys[2].Mark_quantityValue;
                    break;
                case 3:
                    quantity_ = Mark_quantitys[3].Mark_quantityValue;
                    break;
                case 4:
                    quantity_ = Mark_quantitys[4].Mark_quantityValue;
                    break;
            }
            switch (cb_Time.SelectedIndex)
            {
                case 0:
                    time_ = Mark_times[0].Mark_timeValue;
                    break;
                case 1:
                    time_ = Mark_times[1].Mark_timeValue;
                    break;
                case 2:
                    time_ = Mark_times[2].Mark_timeValue;
                    break;
                case 3:
                    time_ = Mark_times[3].Mark_timeValue;
                    break;
                case 4:
                    time_ = Mark_times[4].Mark_timeValue;
                    break;
            }
            total = (loss_ + frequency_ + quantity_ + time_) / 4;
            lbl_Mark_Total.Content = Math.Round(total, 0, 0);
        }

        public void bCount_Click(object sender, RoutedEventArgs e)
        {
            count();
        }
        public void bSave_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(lbl_Mark_Total.Content.ToString(), out w) == false)
            {
                MessageBox.Show("Для сохранения оценки тяжести ее необходимо рассчитать!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                count();
                DialogResult = true;
            }          
        }
    }
}
