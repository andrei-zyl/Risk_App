using System;
using System.Windows;
using ViewModel.Interfaces;
using ViewModel.Models;
using ViewModel.Services;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Collections.Generic;

namespace Risk_App
{
    public partial class AddIncident : Window
    {
        bool flag = false;
        public int PID = -1;
        public int WID = -1;
        public int mf=3, ml, mq, mt;
        DateTime dt;
        Decimal dec, dec1, dec2;
        int i;
        public IncidentViewModel incidentVM2, incidentVM3;
        public List<string> st = new List<string> {"Урегулирован", "Не урегулирован" };

        public AddIncident()
        {
            InitializeComponent();
            incidentVM3 = new IncidentViewModel();
        }
        public AddIncident(IncidentViewModel incidentVM)
        {
            InitializeComponent();
            incidentVM2 = incidentVM;
            ModelToWindow(incidentVM2);
        }

        public void ModelToWindow(IncidentViewModel incidentVM)
        {
            ObservableCollection<ObjectRiskViewModel> Objectrisks;
            ObservableCollection<SourceRiskViewModel> Sourcerisks;
            ObservableCollection<UnitViewModel> Units;
            ObservableCollection<LossViewModel> Losses;
            IObjectRiskService ObjectriskService = new ObjectRiskService("TestDbConnection");
            ISourceRiskService SourceriskService = new SourceRiskService("TestDbConnection");
            IUnitService UnitService = new UnitService("TestDbConnection");
            ILossService LossService = new LossService("TestDbConnection");
            Objectrisks = ObjectriskService.GetAll();
            Sourcerisks = SourceriskService.GetAll();
            Units = UnitService.GetAll();
            Losses = LossService.GetAll();
            cb_OR.DataContext = Objectrisks;
            cb_SR.DataContext = Sourcerisks;
            cb_Loss.DataContext = Losses;
            cb_Unit.DataContext = Units;
            cb_Status.DataContext = st;
            dp_Date.Text = incidentVM.DateOfIncident.ToString();
            tb_Description.Text = incidentVM.Description;
            tb_DirectLosses.Text = incidentVM.DirectLoss.ToString();
            tb_PotentialLosses.Text = incidentVM.PotentialLoss.ToString();
            int a = 0, b = 0, c = 0, d = 0, e=0;
            foreach (var i in Sourcerisks)
            {
                if (i.SourceRiskId == incidentVM.SourceRiskId)
                { cb_SR.SelectedIndex = a; }
                a++;
            }
            foreach (var i in Objectrisks)
            {
                if (i.ObjectRiskId == incidentVM.ObjectRiskId)
                { cb_OR.SelectedIndex = b; }
                b++;
            }
            foreach (var i in Losses)
            {
                if (i.LossId == incidentVM.LossId)
                { cb_Loss.SelectedIndex = c; }
                c++;
            }
            foreach (var i in Units)
            {
                if (i.UnitId == incidentVM.UnitId)
                { cb_Unit.SelectedIndex = d; }
                d++;
            }
            foreach (var i in st)
            {
                if (i.ToString() == incidentVM.Status.ToString())
                { cb_Status.SelectedIndex = e; }
                e++;
            }
            lb_Mark.Content = incidentVM.Mark.ToString();
            tb_Measures.Text = incidentVM.Measures;
        }

        public IncidentViewModel WindowToModel()
        {            
            IncidentViewModel incidentVM = new IncidentViewModel();
            incidentVM.DateOfIncident = Convert.ToDateTime(dp_Date.Text);
            incidentVM.Description = tb_Description.Text;
            incidentVM.DirectLoss = Convert.ToDecimal(tb_DirectLosses.Text);
            incidentVM.PotentialLoss = Convert.ToDecimal(tb_PotentialLosses.Text);
            incidentVM.Mark= Convert.ToInt32(lb_Mark.Content);
            incidentVM.Measures=tb_Measures.Text;
            incidentVM.Status = cb_Status.SelectedItem.ToString();
            return incidentVM;
        }

        public void bOk_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Description.Text == "")
            {
                MessageBox.Show("Заполните поле описания инцидента!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (tb_DirectLosses.Text == "")
            {
                MessageBox.Show("Заполните поле прямых потерь!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (tb_PotentialLosses.Text == "")
            {
                MessageBox.Show("Заполните поле потенциальных потерь!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (tb_Measures.Text == "")
            {
                MessageBox.Show("Заполните поле мер по минимизации!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (DateTime.TryParse(dp_Date.Text, out dt) == false)
            {
                MessageBox.Show("Выберите дату!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Decimal.TryParse(tb_DirectLosses.Text, out dec2) == false)
            {
                MessageBox.Show("Введите число в поле прямых потерь!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Decimal.TryParse(tb_PotentialLosses.Text, out dec1) == false)
            {
                MessageBox.Show("Введите число в поле потенциальных потерь!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Convert.ToDecimal(tb_DirectLosses.Text) <0)
            {
                MessageBox.Show("Значение прямых потерь не может быть меньше 0!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Convert.ToDecimal(tb_DirectLosses.Text) > 1000000000)
            {
                MessageBox.Show("Введите корректное значение прямых потерь!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Convert.ToDecimal(tb_PotentialLosses.Text) > 1000000000)
            {
                MessageBox.Show("Введите корректное значение потенциальных потерь!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Convert.ToDecimal(tb_PotentialLosses.Text) <= 0)
            {
                MessageBox.Show("Значение потенциальных потерь должно быть больше 0!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Convert.ToInt32(lb_Mark.Content)<1 || Convert.ToInt32(lb_Mark.Content) > 5)
            {
                MessageBox.Show("Значение оценки тяжести должно быть в диапазоне от 1 до 5!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                DialogResult = true;
            }
        }
        public void bCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void bMark_Click(object sender, RoutedEventArgs e)
        {
            if (incidentVM2 != null)
            {
                Mark_window2 dialog = new Mark_window2(incidentVM2);
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    IIncidentService incidentService = new IncidentService("TestDbConnection");
                    lb_Mark.Content = dialog.lbl_Mark_Total.Content.ToString();
                    incidentVM2.Mark_frequencyId = dialog.cb_Frequency.SelectedIndex+1;
                    incidentVM2.Mark_lossId = dialog.cb_Loss.SelectedIndex+1;
                    incidentVM2.Mark_quantityId = dialog.cb_Quantity.SelectedIndex+1;
                    incidentVM2.Mark_timeId = dialog.cb_Time.SelectedIndex+1;
                    incidentService.UpdateIncident(incidentVM2);
                    dialog.Close();
                }
            }
            else
            {
                Mark_window2 dialog = new Mark_window2();
                var result = dialog.ShowDialog();
                if (result == true)
                {                 
                    lb_Mark.Content = dialog.lbl_Mark_Total.Content.ToString();
                    incidentVM3.Mark_frequencyId = dialog.cb_Frequency.SelectedIndex;
                    incidentVM3.Mark_lossId = dialog.cb_Loss.SelectedIndex;
                    incidentVM3.Mark_quantityId = dialog.cb_Quantity.SelectedIndex;
                    incidentVM3.Mark_timeId = dialog.cb_Time.SelectedIndex;
                    dialog.Close();
                }
            }
        }        
    }
}
