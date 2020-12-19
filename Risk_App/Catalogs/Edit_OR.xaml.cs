using System.Windows;
using ViewModel.Models;

namespace Risk_App
{
    public partial class Edit_OR : Window
    {
        public int PID = -1;
        public Edit_OR()
        {
            InitializeComponent();
        }
        public Edit_OR(ObjectRiskViewModel incidentVM)
        {
            InitializeComponent();
            ModelToWindow(incidentVM);
        }
        public ObjectRiskViewModel WindowToModel()
        {
            ObjectRiskViewModel incidentVM = new ObjectRiskViewModel();
            incidentVM.ObjectRiskName = tb_Name_OR.Text;
            incidentVM.ObjectRiskId = PID;
            return incidentVM;
        }
        public void ModelToWindow(ObjectRiskViewModel incidentVM)
        {
            tb_Name_OR.Text = incidentVM.ObjectRiskName;
        }
        public void bOk_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_OR.Text == "")
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
