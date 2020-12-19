using System.Windows;
using ViewModel.Models;

namespace Risk_App
{
    public partial class Edit_Loss : Window
    {
        public int PID = -1;
        public Edit_Loss()
        {
            InitializeComponent();
        }
        public Edit_Loss(LossViewModel incidentVM)
        {
            InitializeComponent();
            ModelToWindow(incidentVM);
        }
        public LossViewModel WindowToModel()
        {
            LossViewModel incidentVM = new LossViewModel();
            incidentVM.LossName = tb_Name_Loss.Text;
            incidentVM.LossId = PID;
            return incidentVM;
        }
        public void ModelToWindow(LossViewModel incidentVM)
        {
            tb_Name_Loss.Text = incidentVM.LossName;
        }
        public void bOk_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_Loss.Text == "")
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
