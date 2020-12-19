using LiveCharts;
using Microsoft.Win32;
using NLog;
using Risk_App.Marks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel.Interfaces;
using ViewModel.Models;
using ViewModel.Services;

namespace Risk_App
{
    public partial class MainWindow : Window
    {
        ObservableCollection<IncidentViewModel> sourcerisks;
        IIncidentService sourceriskService;
        Logger logger = LogManager.GetCurrentClassLogger();
        public List<string> st = new List<string> { "Урегулирован", "Не урегулирован" };

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += open;
            this.Closed += close;
            ShowAll();        
        }

        private void ShowAll()
        {
            decimal sum = 0, sum1=0;
            List<int> mark = new List<int> {1,2,3,4,5};
            List<string> so = new List<string> { "Все" };
            List<string> so1 = new List<string> { "Все", "Урегулирован", "Не урегулирован" };
            List<DateTime> mydate=new List<DateTime> { };
            sourceriskService = new IncidentService("TestDbConnection");
            ISourceRiskService sr = new SourceRiskService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            ObservableCollection<SourceRiskViewModel> srs = sr.GetAll();
            my_DataGrid.DataContext = sourcerisks;
            foreach (var a in sourcerisks)
            {
                sum += a.DirectLoss;
                sum1 += a.PotentialLoss;
            }
            var culture = new CultureInfo("ru-RU") { NumberFormat = { NumberGroupSeparator = " " }, };
            lb_TotalA.Text = sum.ToString("#0,0.0#", culture);
            lb_TotalQ.Content = sum1.ToString("#0,0.0#", culture);
            foreach (var a in sourcerisks)
            {
                mydate.Add( a.DateOfIncident);
            }
            foreach (var a in srs)
            {
                so.Add(a.SourceRiskId.ToString()+' '+a.SourceRiskName);
            }
            dp_begin_gr.Text = mydate.Min().ToString();
            dp_end_gr.Text = DateTime.Now.ToString();
            dp_begin.Text= mydate.Min().ToString();
            dp_end.Text = DateTime.Now.ToString();
            tb_SR.DataContext = so;
            tb_Status.DataContext = so1;
            //tb_Mark_start.Text = "1";
            //tb_Mark_end.Text = "5";
            tb_Mark_start.DataContext = mark;
            tb_Mark_end.DataContext = mark;
            tb_Mark_start.SelectedIndex = 0;
            tb_Mark_end.SelectedIndex = 4;
            tb_SR.SelectedIndex = 0;
            tb_Status.SelectedIndex = 0;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ObjectRiskViewModel> Objectrisks;
            ObservableCollection<SourceRiskViewModel> Sourcerisks;
            ObservableCollection<UnitViewModel> Units;
            ObservableCollection<LossViewModel> Losses;
            ObservableCollection<Mark_frequencyViewModel> Mark_frequencys;
            ObservableCollection<Mark_lossViewModel> Mark_losss;
            ObservableCollection<Mark_quantityViewModel> Mark_quantitys;
            ObservableCollection<Mark_timeViewModel> Mark_times;
            IObjectRiskService ObjectriskService = new ObjectRiskService("TestDbConnection");
            ISourceRiskService SourceriskService = new SourceRiskService("TestDbConnection");
            IUnitService UnitService = new UnitService("TestDbConnection");
            ILossService LossService = new LossService("TestDbConnection");
            IMark_frequencyService Mark_frequencyService = new Mark_frequencyService("TestDbConnection");
            IMark_lossService Mark_lossService = new Mark_lossService("TestDbConnection");
            IMark_quantityService Mark_quantityService = new Mark_quantityService("TestDbConnection");
            IMark_timeService Mark_timeService = new Mark_timeService("TestDbConnection");
            Objectrisks = ObjectriskService.GetAll();
            Sourcerisks = SourceriskService.GetAll();
            Units = UnitService.GetAll();
            Losses = LossService.GetAll();
            Mark_frequencys = Mark_frequencyService.GetAll();
            Mark_losss = Mark_lossService.GetAll();
            Mark_quantitys = Mark_quantityService.GetAll();
            Mark_times = Mark_timeService.GetAll();
            AddIncident dialog = new AddIncident();
            dialog.cb_OR.DataContext = Objectrisks;
            dialog.cb_SR.DataContext = Sourcerisks;
            dialog.cb_Loss.DataContext = Losses;
            dialog.cb_Unit.DataContext = Units;
            dialog.cb_Status.DataContext = st;
            dialog.cb_SR.SelectedIndex = 0;
            dialog.cb_OR.SelectedIndex = 0;
            dialog.cb_Loss.SelectedIndex = 0;
            dialog.cb_Unit.SelectedIndex = 0;
            dialog.cb_Status.SelectedIndex = 0;
            dialog.dp_Date.Text = DateTime.Today.ToString();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                IIncidentService incidentService = new IncidentService("TestDbConnection");
                IncidentViewModel incident = dialog.WindowToModel();
                incident.ObjectRiskId = Objectrisks[dialog.cb_OR.SelectedIndex].ObjectRiskId;
                incident.SourceRiskId = Sourcerisks[dialog.cb_SR.SelectedIndex].SourceRiskId;
                incident.UnitId = Units[dialog.cb_Unit.SelectedIndex].UnitId;
                incident.LossId = Losses[dialog.cb_Loss.SelectedIndex].LossId;
                incident.Mark_frequencyId = Mark_frequencys[dialog.incidentVM3.Mark_frequencyId].Mark_frequencyId;
                incident.Mark_lossId = Mark_losss[dialog.incidentVM3.Mark_lossId].Mark_lossId;
                incident.Mark_quantityId = Mark_quantitys[dialog.incidentVM3.Mark_quantityId].Mark_quantityId;
                incident.Mark_timeId = Mark_times[dialog.incidentVM3.Mark_timeId].Mark_timeId;
                incident.IncidentId = 4;
                incidentService.CreateIncident(incident);
                dialog.Close();
            }
            ShowAll();
        }
        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            dp_begin.Text = "";
            dp_end.Text = "";
            tb_SR.SelectedIndex = 0;
            tb_Mark_start.SelectedIndex = 0;
            tb_Mark_end.SelectedIndex = 4;
            ShowAll();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (my_DataGrid.SelectedItem != null)
            {
                AddIncident dialog = new AddIncident((IncidentViewModel)my_DataGrid.SelectedItem);
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    IIncidentService incidentService = new IncidentService("TestDbConnection");
                    ObservableCollection<ObjectRiskViewModel> Objectrisks;
                    ObservableCollection<SourceRiskViewModel> Sourcerisks;
                    ObservableCollection<UnitViewModel> Units;
                    ObservableCollection<LossViewModel> Losses;
                    ObservableCollection<Mark_frequencyViewModel> Mark_frequencys;
                    ObservableCollection<Mark_lossViewModel> Mark_losss;
                    ObservableCollection<Mark_quantityViewModel> Mark_quantitys;
                    ObservableCollection<Mark_timeViewModel> Mark_times;
                    IObjectRiskService ObjectriskService = new ObjectRiskService("TestDbConnection");
                    ISourceRiskService SourceriskService = new SourceRiskService("TestDbConnection");
                    IUnitService UnitService = new UnitService("TestDbConnection");
                    ILossService LossService = new LossService("TestDbConnection");
                    IMark_frequencyService Mark_frequencyService = new Mark_frequencyService("TestDbConnection");
                    IMark_lossService Mark_lossService = new Mark_lossService("TestDbConnection");
                    IMark_quantityService Mark_quantityService = new Mark_quantityService("TestDbConnection");
                    IMark_timeService Mark_timeService = new Mark_timeService("TestDbConnection");
                    Objectrisks = ObjectriskService.GetAll();
                    Sourcerisks = SourceriskService.GetAll();
                    Units = UnitService.GetAll();
                    Losses = LossService.GetAll();
                    Mark_frequencys = Mark_frequencyService.GetAll();
                    Mark_losss = Mark_lossService.GetAll();
                    Mark_quantitys = Mark_quantityService.GetAll();
                    Mark_times = Mark_timeService.GetAll();
                    IncidentViewModel incident = (IncidentViewModel)my_DataGrid.SelectedItem;
                    incident.Description = dialog.tb_Description.Text;
                    incident.DirectLoss = Convert.ToDecimal(dialog.tb_DirectLosses.Text);
                    incident.DateOfIncident = Convert.ToDateTime(dialog.dp_Date.Text);
                    incident.PotentialLoss = Convert.ToDecimal(dialog.tb_PotentialLosses.Text);
                    incident.ObjectRiskId = Objectrisks[dialog.cb_OR.SelectedIndex].ObjectRiskId;
                    incident.SourceRiskId = Sourcerisks[dialog.cb_SR.SelectedIndex].SourceRiskId;
                    incident.UnitId = Units[dialog.cb_Unit.SelectedIndex].UnitId;
                    incident.LossId = Losses[dialog.cb_Loss.SelectedIndex].LossId;
                    incident.Mark = Convert.ToInt32(dialog.lb_Mark.Content);
                    incident.Measures = dialog.tb_Measures.Text;
                    incident.Status = dialog.cb_Status.SelectedItem.ToString();
                    incidentService.UpdateIncident(incident);
                    dialog.Close();
                }
                ShowAll();
            }
            else
            {
                MessageBox.Show("Выберите инцидент для внесения изменений!", "Обратите внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (my_DataGrid.SelectedItem != null)
            {
                sourceriskService = new IncidentService("TestDbConnection");
                sourceriskService.DeleteIncident(((IncidentViewModel)my_DataGrid.SelectedItem).IncidentId);
                ShowAll();
            }
            else
            {
                MessageBox.Show("Выберите инцидент для удаления!", "Обратите внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void dGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void Sources_Click(object sender, RoutedEventArgs e)
        {
            Sources dialog = new Sources();
            dialog.ShowDialog();
            ShowAll();
        }
        private void Objects_Click(object sender, RoutedEventArgs e)
        {
            Objects dialog = new Objects();
            dialog.ShowDialog();
        }
        private void Losses_Click(object sender, RoutedEventArgs e)
        {
            Losses dialog = new Losses();
            dialog.ShowDialog();
        }
        private void Units_Click(object sender, RoutedEventArgs e)
        {
            Units dialog = new Units();
            dialog.ShowDialog();
        }
        private void Mark_frequency_Click(object sender, RoutedEventArgs e)
        {
            M_frequency dialog = new M_frequency();
            dialog.ShowDialog();
        }
        private void Mark_loss_Click(object sender, RoutedEventArgs e)
        {
            M_loss dialog = new M_loss();
            dialog.ShowDialog();
        }
        private void Mark_quantity_Click(object sender, RoutedEventArgs e)
        {
            M_quantity dialog = new M_quantity();
            dialog.ShowDialog();
        }
        private void Mark_time_Click(object sender, RoutedEventArgs e)
        {
            M_time dialog = new M_time();
            dialog.ShowDialog();
        }

        private void Filter1_Click(object sender, RoutedEventArgs e)
        {
            decimal sum = 0, sum1=0;
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            var sourcerisks1 = from c in sourcerisks where c.Mark >= Convert.ToInt32(tb_Mark_start.Text) && c.SourceRiskId == Convert.ToInt32(tb_SR.SelectedItem) select c;
            foreach (var car in sourcerisks1) { }
            my_DataGrid.DataContext = sourcerisks1;
            foreach (var a in sourcerisks1)
            {
                sum += a.DirectLoss;
                sum1 += a.PotentialLoss;
            }
            var culture = new CultureInfo("ru-RU") { NumberFormat = { NumberGroupSeparator = " " }, };
            lb_TotalA.Text = sum.ToString("#0,0.0#", culture);
            lb_TotalQ.Content = sum1.ToString("#0,0.0#", culture);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            bool true_SR = true;
            bool true_Status = true;
            decimal sum = 0,sum1=0;
            DateTime dt1, dt2;
            int i1, i2, i3;
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            if (dp_begin.Text == "" || DateTime.TryParse(dp_begin.Text, out dt1) == false)
            {
                MessageBox.Show("Значение начальной даты является некорректным! По умолчанию выбрано значение 01.01.2000", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin.Text = "01.01.2000";
            }
            if (dp_end.Text == "" || DateTime.TryParse(dp_end.Text, out dt2) == false)
            {
                MessageBox.Show("Значение конечной даты является некорректным! По умолчанию выбрано значение текущей даты", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_end.Text = DateTime.Today.ToString();
            }
            if (Convert.ToDateTime(dp_begin.Text) > Convert.ToDateTime(dp_end.Text))
            {
                MessageBox.Show("Начальная дата должна быть больше либо равна конечной!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin.Text = dp_end.Text;
            }

            if (tb_Mark_start.Text == "" || Int32.TryParse(tb_Mark_start.Text, out i1) == false)
            {
                MessageBox.Show("Значение начальной оценки тяжести является некорректным! По умолчанию выбрано значение 1", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                tb_Mark_start.Text = "1";
            }
            if (tb_Mark_end.Text == "" || Int32.TryParse(tb_Mark_end.Text, out i2) == false)
            {
                MessageBox.Show("Значение конечной оценки тяжести является некорректным! По умолчанию выбрано значение 5", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                tb_Mark_end.Text = "5";
            }
            if (Convert.ToInt32(tb_Mark_start.SelectedItem) > Convert.ToInt32(tb_Mark_end.SelectedItem))
            {
                MessageBox.Show("Начальная оценка тяжести должна быть больше либо равна конечной!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                tb_Mark_start.SelectedIndex = tb_Mark_end.SelectedIndex;
            }
            if (tb_SR.SelectedIndex == 0 || Int32.TryParse(tb_SR.Text[0].ToString(), out i3) == false)
            {
                //MessageBox.Show("Значение Id источника риска является некорректным! По умолчанию будет осуществлена фильтрация по всем источнам риска.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                true_SR = false;
                tb_SR.SelectedIndex = 0;
            }
            if (tb_Status.SelectedIndex == 0)
            {
                //MessageBox.Show("Значение Id источника риска является некорректным! По умолчанию будет осуществлена фильтрация по всем источнам риска.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                true_Status = false;
                tb_Status.SelectedIndex = 0;
            }
            if (true_SR)
            {
                if (true_Status) 
                {
                    var sourcerisks2 = from c in sourcerisks 
                                       where c.Mark >= Convert.ToInt32(tb_Mark_start.SelectedItem) 
                                       && c.Mark <= Convert.ToInt32(tb_Mark_end.SelectedItem) 
                                       && c.DateOfIncident >= Convert.ToDateTime(dp_begin.Text) 
                                       && c.DateOfIncident <= Convert.ToDateTime(dp_end.Text) 
                                       && c.SourceRiskId == Convert.ToInt32(tb_SR.Text[0].ToString())
                                       && c.Status == tb_Status.SelectedItem.ToString() select c;
                    foreach (var car in sourcerisks2) { }
                    my_DataGrid.DataContext = sourcerisks2;
                    foreach (var a in sourcerisks2)
                    {
                        sum += a.DirectLoss;
                        sum1 += a.PotentialLoss;
                    }
                    var culture = new CultureInfo("ru-RU") { NumberFormat = { NumberGroupSeparator = " " }, };
                    lb_TotalA.Text = sum.ToString("#0,0.0#", culture);
                    lb_TotalQ.Content = sum1.ToString("#0,0.0#", culture);
                }
                
                else
                {
                    var sourcerisks2 = from c in sourcerisks 
                                       where c.Mark >= Convert.ToInt32(tb_Mark_start.SelectedItem) 
                                       && c.Mark <= Convert.ToInt32(tb_Mark_end.SelectedItem) 
                                       && c.DateOfIncident >= Convert.ToDateTime(dp_begin.Text) 
                                       && c.DateOfIncident <= Convert.ToDateTime(dp_end.Text) 
                                       && c.SourceRiskId == Convert.ToInt32(tb_SR.Text[0].ToString()) select c;
                    foreach (var car in sourcerisks2) { }
                    my_DataGrid.DataContext = sourcerisks2;
                    foreach (var a in sourcerisks2)
                    {
                        sum += a.DirectLoss;
                        sum1 += a.PotentialLoss;
                    }
                    var culture = new CultureInfo("ru-RU") { NumberFormat = { NumberGroupSeparator = " " }, };
                    lb_TotalA.Text = sum.ToString("#0,0.0#", culture);
                    lb_TotalQ.Content = sum1.ToString("#0,0.0#", culture);
                }
            }
            else
            {
                if (true_Status) 
                {
                    var sourcerisks3 = from c in sourcerisks 
                                       where c.Mark >= Convert.ToInt32(tb_Mark_start.SelectedItem) 
                                       && c.Mark <= Convert.ToInt32(tb_Mark_end.SelectedItem) 
                                       && c.DateOfIncident >= Convert.ToDateTime(dp_begin.Text) 
                                       && c.DateOfIncident <= Convert.ToDateTime(dp_end.Text)
                                       && c.Status == tb_Status.SelectedItem.ToString() select c;
                    foreach (var car in sourcerisks3) { }
                    my_DataGrid.DataContext = sourcerisks3;
                    foreach (var a in sourcerisks3)
                    {
                        sum += a.DirectLoss;
                        sum1 += a.PotentialLoss;
                    }
                    var culture = new CultureInfo("ru-RU") { NumberFormat = { NumberGroupSeparator = " " }, };
                    lb_TotalA.Text = sum.ToString("#0,0.0#", culture);
                    lb_TotalQ.Content = sum1.ToString("#0,0.0#", culture);
                }
                
                else {
                    var sourcerisks3 = from c in sourcerisks 
                                       where c.Mark >= Convert.ToInt32(tb_Mark_start.SelectedItem) 
                                       && c.Mark <= Convert.ToInt32(tb_Mark_end.SelectedItem) 
                                       && c.DateOfIncident >= Convert.ToDateTime(dp_begin.Text) 
                                       && c.DateOfIncident <= Convert.ToDateTime(dp_end.Text) select c;
                    foreach (var car in sourcerisks3) { }
                    my_DataGrid.DataContext = sourcerisks3;
                    foreach (var a in sourcerisks3)
                    {
                        sum += a.DirectLoss;
                        sum1 += a.PotentialLoss;
                    }
                    var culture = new CultureInfo("ru-RU") { NumberFormat = { NumberGroupSeparator = " " }, };
                    lb_TotalA.Text = sum.ToString("#0,0.0#", culture);
                    lb_TotalQ.Content = sum1.ToString("#0,0.0#", culture);
                }
            }
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About dialog = new About();
            dialog.ShowDialog();
        }
        private void close(object sender, EventArgs e)
        {
            logger.Info("Программа закрыта");
        }
        private void open(object sender, RoutedEventArgs e)
        {
            logger.Info("Программа открыта");
        }
        private void Log_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\notepad.exe", "Application.log");
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Help.docx");
        }

        public ChartValues<decimal> abc()
        {
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            var sourcerisks3 = from c in sourcerisks group c by c.SourceRiskId into abc select new { SourceRiskId = abc.Key, total = abc.Sum(x => x.DirectLoss) };
            foreach (var car in sourcerisks3) { }
            ChartValues<decimal> bbb = new ChartValues<decimal> { 1, 2 };
            foreach (var s in sourcerisks3){}           
            return bbb;
        }

        public void Graph_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<SourceRiskViewModel> Sourcerisks;
            ISourceRiskService SourceriskService = new SourceRiskService("TestDbConnection");
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            Sourcerisks = SourceriskService.GetAll();
            DateTime dt3, dt4;
            if (dp_begin_gr.Text == "" || DateTime.TryParse(dp_begin_gr.Text, out dt3) == false)
            {
                MessageBox.Show("Значение начальной даты является некорректным! По умолчанию выбрано значение 01.01.2000", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = "01.01.2000";
            }
            if (dp_end_gr.Text == "" || DateTime.TryParse(dp_end_gr.Text, out dt4) == false)
            {
                MessageBox.Show("Значение конечной даты является некорректным! По умолчанию выбрано значение текущей даты", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_end_gr.Text = DateTime.Today.ToString();
            }
            if (Convert.ToDateTime(dp_begin_gr.Text) > Convert.ToDateTime(dp_end_gr.Text))
            {
                MessageBox.Show("Начальная дата должна быть больше либо равна конечной!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = dp_end_gr.Text;
            }
            else
            {
                var sourcerisks3 = from c in sourcerisks where c.DateOfIncident >= Convert.ToDateTime(dp_begin_gr.Text) && c.DateOfIncident <= Convert.ToDateTime(dp_end_gr.Text) group c by c.SourceRiskId into abc select new { SourceRiskId = abc.Key, total = abc.Sum(x => x.DirectLoss), total1 = abc.Sum(x => x.PotentialLoss) };
                ObservableCollection<int> aaa = new ObservableCollection<int> { };
                ChartValues<decimal> bbb = new ChartValues<decimal> { };
                ChartValues<decimal> ddd = new ChartValues<decimal> { };
                List<string> ccc = new List<string> { };
                foreach (var s in sourcerisks3)
                {
                    foreach (var j in Sourcerisks)
                    {
                        if (s.SourceRiskId == j.SourceRiskId)
                        {
                            ccc.Add(j.SourceRiskName);
                        }
                    }
                    aaa.Add(s.SourceRiskId);
                    ddd.Add(s.total1);
                    bbb.Add(s.total);
                }
                Graph dialog = new Graph(bbb, ccc, ddd, Convert.ToDateTime(dp_begin_gr.Text), Convert.ToDateTime(dp_end_gr.Text));
                var result = dialog.ShowDialog();
                if (result == true){}
            }
        }

        public void Graph_Click_OR(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ObjectRiskViewModel> ObjectRisks;
            IObjectRiskService ObjectRiskService = new ObjectRiskService("TestDbConnection");
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            ObjectRisks = ObjectRiskService.GetAll();
            DateTime dt3, dt4;
            if (dp_begin_gr.Text == "" || DateTime.TryParse(dp_begin_gr.Text, out dt3) == false)
            {
                MessageBox.Show("Значение начальной даты является некорректным! По умолчанию выбрано значение 01.01.2000", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = "01.01.2000";
            }
            if (dp_end_gr.Text == "" || DateTime.TryParse(dp_end_gr.Text, out dt4) == false)
            {
                MessageBox.Show("Значение конечной даты является некорректным! По умолчанию выбрано значение текущей даты", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_end_gr.Text = DateTime.Today.ToString();
            }
            if (Convert.ToDateTime(dp_begin_gr.Text) > Convert.ToDateTime(dp_end_gr.Text))
            {
                MessageBox.Show("Начальная дата должна быть больше либо равна конечной!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = dp_end_gr.Text;
            }
            else
            {
                var ObjectRisks3 = from c in sourcerisks where c.DateOfIncident >= Convert.ToDateTime(dp_begin_gr.Text) && c.DateOfIncident <= Convert.ToDateTime(dp_end_gr.Text) group c by c.ObjectRiskId into abc select new { ObjectRiskId = abc.Key, total = abc.Sum(x => x.DirectLoss), total1 = abc.Sum(x => x.PotentialLoss) };
                ObservableCollection<int> aaa = new ObservableCollection<int> { };
                ChartValues<decimal> bbb = new ChartValues<decimal> { };
                ChartValues<decimal> ddd = new ChartValues<decimal> { };
                List<string> ccc = new List<string> { };
                foreach (var s in ObjectRisks3)
                {
                    foreach (var j in ObjectRisks)
                    {
                        if (s.ObjectRiskId == j.ObjectRiskId)
                        {
                            ccc.Add(j.ObjectRiskName);
                        }
                    }
                    aaa.Add(s.ObjectRiskId);
                    ddd.Add(s.total1);
                    bbb.Add(s.total);
                }
                Graph_OR dialog = new Graph_OR(bbb, ccc, ddd, Convert.ToDateTime(dp_begin_gr.Text), Convert.ToDateTime(dp_end_gr.Text));
                var result = dialog.ShowDialog();
                if (result == true){ }
            }
        }

        public void Graph_Click_Loss(object sender, RoutedEventArgs e)
        {
            ObservableCollection<LossViewModel> Losss;
            ILossService LossService = new LossService("TestDbConnection");
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            Losss = LossService.GetAll();
            DateTime dt3, dt4;
            if (dp_begin_gr.Text == "" || DateTime.TryParse(dp_begin_gr.Text, out dt3) == false)
            {
                MessageBox.Show("Значение начальной даты является некорректным! По умолчанию выбрано значение 01.01.2000", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = "01.01.2000";
            }
            if (dp_end_gr.Text == "" || DateTime.TryParse(dp_end_gr.Text, out dt4) == false)
            {
                MessageBox.Show("Значение конечной даты является некорректным! По умолчанию выбрано значение текущей даты", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_end_gr.Text = DateTime.Today.ToString();
            }
            if (Convert.ToDateTime(dp_begin_gr.Text) > Convert.ToDateTime(dp_end_gr.Text))
            {
                MessageBox.Show("Начальная дата должна быть больше либо равна конечной!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = dp_end_gr.Text;
            }
            else
            {
                var Losss3 = from c in sourcerisks where c.DateOfIncident >= Convert.ToDateTime(dp_begin_gr.Text) && c.DateOfIncident <= Convert.ToDateTime(dp_end_gr.Text) group c by c.LossId into abc select new { LossId = abc.Key, total = abc.Sum(x => x.DirectLoss), total1 = abc.Sum(x => x.PotentialLoss) };
                ObservableCollection<int> aaa = new ObservableCollection<int> { };
                ChartValues<decimal> bbb = new ChartValues<decimal> { };
                ChartValues<decimal> ddd = new ChartValues<decimal> { };
                List<string> ccc = new List<string> { };
                foreach (var s in Losss3)
                {
                    foreach (var j in Losss)
                    {
                        if (s.LossId == j.LossId)
                        {
                            ccc.Add(j.LossName);
                        }
                    }
                    aaa.Add(s.LossId);
                    ddd.Add(s.total1);
                    bbb.Add(s.total);
                }
                Graph_Loss dialog = new Graph_Loss(bbb, ccc, ddd, Convert.ToDateTime(dp_begin_gr.Text), Convert.ToDateTime(dp_end_gr.Text));
                var result = dialog.ShowDialog();
                if (result == true){}
            }
        }
        public void Graph_Click_Unit(object sender, RoutedEventArgs e)
        {
            ObservableCollection<UnitViewModel> Units;
            IUnitService UnitService = new UnitService("TestDbConnection");
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            Units = UnitService.GetAll();
            DateTime dt3, dt4;
            if (dp_begin_gr.Text == "" || DateTime.TryParse(dp_begin_gr.Text, out dt3) == false)
            {
                MessageBox.Show("Значение начальной даты является некорректным! По умолчанию выбрано значение 01.01.2000", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = "01.01.2000";
            }
            if (dp_end_gr.Text == "" || DateTime.TryParse(dp_end_gr.Text, out dt4) == false)
            {
                MessageBox.Show("Значение конечной даты является некорректным! По умолчанию выбрано значение текущей даты", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_end_gr.Text = DateTime.Today.ToString();
            }
            if (Convert.ToDateTime(dp_begin_gr.Text) > Convert.ToDateTime(dp_end_gr.Text))
            {
                MessageBox.Show("Начальная дата должна быть больше либо равна конечной!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = dp_end_gr.Text;
            }
            else
            {
                var Units3 = from c in sourcerisks where c.DateOfIncident >= Convert.ToDateTime(dp_begin_gr.Text) && c.DateOfIncident <= Convert.ToDateTime(dp_end_gr.Text) group c by c.UnitId into abc select new { UnitId = abc.Key, total = abc.Sum(x => x.DirectLoss), total1 = abc.Sum(x => x.PotentialLoss) };
                ObservableCollection<int> aaa = new ObservableCollection<int> { };
                ChartValues<decimal> bbb = new ChartValues<decimal> { };
                ChartValues<decimal> ddd = new ChartValues<decimal> { };
                List<string> ccc = new List<string> { };
                foreach (var s in Units3)
                {
                    foreach (var j in Units)
                    {
                        if (s.UnitId == j.UnitId)
                        {
                            ccc.Add(j.UnitName);
                        }
                    }
                    aaa.Add(s.UnitId);
                    ddd.Add(s.total1);
                    bbb.Add(s.total);
                }
                Graph_Unit dialog = new Graph_Unit(bbb, ccc, ddd, Convert.ToDateTime(dp_begin_gr.Text), Convert.ToDateTime(dp_end_gr.Text));
                var result = dialog.ShowDialog();
                if (result == true){}
            }
        }

        public void Chart_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ObjectRiskViewModel> ObjectRisks;
            IObjectRiskService ObjectRiskService = new ObjectRiskService("TestDbConnection");
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            ObjectRisks = ObjectRiskService.GetAll();
            DateTime dt3, dt4;
            if (dp_begin_gr.Text == "" || DateTime.TryParse(dp_begin_gr.Text, out dt3) == false)
            {
                MessageBox.Show("Значение начальной даты является некорректным! По умолчанию выбрано значение 01.01.2000", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = "01.01.2000";
            }
            if (dp_end_gr.Text == "" || DateTime.TryParse(dp_end_gr.Text, out dt4) == false)
            {
                MessageBox.Show("Значение конечной даты является некорректным! По умолчанию выбрано значение текущей даты", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_end_gr.Text = DateTime.Today.ToString();
            }
            if (Convert.ToDateTime(dp_begin_gr.Text) > Convert.ToDateTime(dp_end_gr.Text))
            {
                MessageBox.Show("Начальная дата должна быть больше либо равна конечной!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = dp_end_gr.Text;
            }
            else
            {
                var ObjectRisks3 = from c in sourcerisks where c.DateOfIncident >= Convert.ToDateTime(dp_begin_gr.Text) && c.DateOfIncident <= Convert.ToDateTime(dp_end_gr.Text) group c by c.ObjectRiskId into abc select new { ObjectRiskId = abc.Key, total = abc.Count() };
                ObservableCollection<int> aaa = new ObservableCollection<int> { };
                ChartValues<decimal> bbb = new ChartValues<decimal> { };
                ChartValues<decimal> ddd = new ChartValues<decimal> { };
                List<string> ccc = new List<string> { };
                foreach (var s in ObjectRisks3)
                {
                    foreach (var j in ObjectRisks)
                    {
                        if (s.ObjectRiskId == j.ObjectRiskId)
                        {
                            ccc.Add(j.ObjectRiskName);
                        }
                    }
                    aaa.Add(s.ObjectRiskId);
                    bbb.Add(s.total);
                }

                Chart dialog = new Chart(bbb, ccc, Convert.ToDateTime(dp_begin_gr.Text), Convert.ToDateTime(dp_end_gr.Text));
                var result = dialog.ShowDialog();
                if (result == true){}
            }
        }

        public void Chart_SR_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<SourceRiskViewModel> SourceRisks;
            ISourceRiskService SourceRiskService = new SourceRiskService("TestDbConnection");
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            SourceRisks = SourceRiskService.GetAll();
            DateTime dt3, dt4;
            if (dp_begin_gr.Text == "" || DateTime.TryParse(dp_begin_gr.Text, out dt3) == false)
            {
                MessageBox.Show("Значение начальной даты является некорректным! По умолчанию выбрано значение 01.01.2000", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = "01.01.2000";
            }
            if (dp_end_gr.Text == "" || DateTime.TryParse(dp_end_gr.Text, out dt4) == false)
            {
                MessageBox.Show("Значение конечной даты является некорректным! По умолчанию выбрано значение текущей даты", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_end_gr.Text = DateTime.Today.ToString();
            }
            if (Convert.ToDateTime(dp_begin_gr.Text) > Convert.ToDateTime(dp_end_gr.Text))
            {
                MessageBox.Show("Начальная дата должна быть больше либо равна конечной!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = dp_end_gr.Text;
            }
            else
            {
                var ObjectRisks3 = from c in sourcerisks where c.DateOfIncident >= Convert.ToDateTime(dp_begin_gr.Text) && c.DateOfIncident <= Convert.ToDateTime(dp_end_gr.Text) group c by c.SourceRiskId into abc select new { SourceRiskId = abc.Key, total = abc.Count() };
                ObservableCollection<int> aaa = new ObservableCollection<int> { };
                ChartValues<decimal> bbb = new ChartValues<decimal> { };
                ChartValues<decimal> ddd = new ChartValues<decimal> { };
                List<string> ccc = new List<string> { };
                foreach (var s in ObjectRisks3)
                {
                    foreach (var j in SourceRisks)
                    {
                        if (s.SourceRiskId == j.SourceRiskId)
                        {
                            ccc.Add(j.SourceRiskName);
                        }
                    }
                    aaa.Add(s.SourceRiskId);
                    bbb.Add(s.total);
                }

                Chart_SR dialog = new Chart_SR(bbb, ccc, Convert.ToDateTime(dp_begin_gr.Text), Convert.ToDateTime(dp_end_gr.Text));
                var result = dialog.ShowDialog();
                if (result == true){}
            }
        }

        public void Chart_Loss_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<LossViewModel> Losss;
            ILossService LossService = new LossService("TestDbConnection");
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            Losss = LossService.GetAll();
            DateTime dt3, dt4;
            if (dp_begin_gr.Text == "" || DateTime.TryParse(dp_begin_gr.Text, out dt3) == false)
            {
                MessageBox.Show("Значение начальной даты является некорректным! По умолчанию выбрано значение 01.01.2000", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = "01.01.2000";
            }
            if (dp_end_gr.Text == "" || DateTime.TryParse(dp_end_gr.Text, out dt4) == false)
            {
                MessageBox.Show("Значение конечной даты является некорректным! По умолчанию выбрано значение текущей даты", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_end_gr.Text = DateTime.Today.ToString();
            }
            if (Convert.ToDateTime(dp_begin_gr.Text) > Convert.ToDateTime(dp_end_gr.Text))
            {
                MessageBox.Show("Начальная дата должна быть больше либо равна конечной!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = dp_end_gr.Text;
            }
            else
            {
                var Losss3 = from c in sourcerisks where c.DateOfIncident >= Convert.ToDateTime(dp_begin_gr.Text) && c.DateOfIncident <= Convert.ToDateTime(dp_end_gr.Text) group c by c.LossId into abc select new { LossId = abc.Key, total = abc.Count() };
                ObservableCollection<int> aaa = new ObservableCollection<int> { };
                ChartValues<decimal> bbb = new ChartValues<decimal> { };
                ChartValues<decimal> ddd = new ChartValues<decimal> { };
                List<string> ccc = new List<string> { };
                foreach (var s in Losss3)
                {
                    foreach (var j in Losss)
                    {
                        if (s.LossId == j.LossId)
                        {
                            ccc.Add(j.LossName);
                        }
                    }
                    aaa.Add(s.LossId);
                    bbb.Add(s.total);
                }
                Chart_Loss dialog = new Chart_Loss(bbb, ccc, Convert.ToDateTime(dp_begin_gr.Text), Convert.ToDateTime(dp_end_gr.Text));
                var result = dialog.ShowDialog();
                if (result == true){ }
            }
        }

        public void Chart_Unit_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<UnitViewModel> Units;
            IUnitService UnitService = new UnitService("TestDbConnection");
            sourceriskService = new IncidentService("TestDbConnection");
            sourcerisks = sourceriskService.GetAll();
            Units = UnitService.GetAll();
            DateTime dt3, dt4;
            if (dp_begin_gr.Text == "" || DateTime.TryParse(dp_begin_gr.Text, out dt3) == false)
            {
                MessageBox.Show("Значение начальной даты является некорректным! По умолчанию выбрано значение 01.01.2000", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = "01.01.2000";
            }
            if (dp_end_gr.Text == "" || DateTime.TryParse(dp_end_gr.Text, out dt4) == false)
            {
                MessageBox.Show("Значение конечной даты является некорректным! По умолчанию выбрано значение текущей даты", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_end_gr.Text = DateTime.Today.ToString();
            }
            if (Convert.ToDateTime(dp_begin_gr.Text) > Convert.ToDateTime(dp_end_gr.Text))
            {
                MessageBox.Show("Начальная дата должна быть больше либо равна конечной!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                dp_begin_gr.Text = dp_end_gr.Text;
            }
            else
            {
                var Units3 = from c in sourcerisks where c.DateOfIncident >= Convert.ToDateTime(dp_begin_gr.Text) && c.DateOfIncident <= Convert.ToDateTime(dp_end_gr.Text) group c by c.UnitId into abc select new { UnitId = abc.Key, total = abc.Count() };
                ObservableCollection<int> aaa = new ObservableCollection<int> { };
                ChartValues<decimal> bbb = new ChartValues<decimal> { };
                ChartValues<decimal> ddd = new ChartValues<decimal> { };
                List<string> ccc = new List<string> { };
                foreach (var s in Units3)
                {
                    foreach (var j in Units)
                    {
                        if (s.UnitId == j.UnitId)
                        {
                            ccc.Add(j.UnitName);
                        }
                    }
                    aaa.Add(s.UnitId);
                    bbb.Add(s.total);
                }
                Chart_Unit dialog = new Chart_Unit(bbb, ccc, Convert.ToDateTime(dp_begin_gr.Text), Convert.ToDateTime(dp_end_gr.Text));
                var result = dialog.ShowDialog();
                if (result == true){}
            }
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog SaveFD = new SaveFileDialog();
            SaveFD.InitialDirectory = "E:\\";
            SaveFD.Filter = "Книга Excel (*.xls)|*.xls|All files (*.*)|*.*";
            SaveFD.OverwritePrompt = true;
            SaveFD.Title = "Сохранить в файл";
            SaveFD.FileName = "Операционные инциденты";
            SaveFD.FileOk += SaveFD_FileOk;
            SaveFD.ShowDialog();
            if (string.IsNullOrEmpty(SaveFD.FileName) == false)
            {               
                my_DataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                my_DataGrid.SelectAllCells();
                ApplicationCommands.Copy.Execute(null, my_DataGrid);
                String result = (string)Clipboard.GetData(DataFormats.Text);
                System.IO.StreamWriter file1 = new System.IO.StreamWriter(SaveFD.FileName, false, Encoding.UTF32);
                file1.WriteLine(result.Replace(',', ','));
                file1.Close();
            }
            ShowAll();
        }
        private void SaveFD_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.MessageBox.Show("Список операционных инцидентов успешно сохранен!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }       
    }
}
