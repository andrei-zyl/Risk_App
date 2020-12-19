using System.Windows;
using ViewModel.Models;

namespace Risk_App
{
    public partial class Edit_SR : Window
    {
        public int PID = -1;
        public Edit_SR()
        {
            InitializeComponent();
        }
        public Edit_SR(SourceRiskViewModel incidentVM)
        {
            InitializeComponent();
            ModelToWindow(incidentVM);
        }
        public SourceRiskViewModel WindowToModel()
        {
            SourceRiskViewModel incidentVM = new SourceRiskViewModel();
            incidentVM.SourceRiskName = tb_Name_SR.Text;
            incidentVM.SourceRiskId = PID;
            return incidentVM;
        }
        public void ModelToWindow(SourceRiskViewModel incidentVM)
        {
            tb_Name_SR.Text = incidentVM.SourceRiskName;
        }
        public void bOk_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_SR.Text == "")
            {
                MessageBox.Show("Заполните форму", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
