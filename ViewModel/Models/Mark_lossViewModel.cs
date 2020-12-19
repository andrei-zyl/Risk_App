using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
    public class Mark_lossViewModel
    {
        public int Mark_lossId { get; set; }
        public string Mark_lossName { get; set; }
        public string Mark_lossRange { get; set; }
        public decimal Mark_lossValue { get; set; }
        public ObservableCollection<IncidentViewModel> Incidents
        { get; set; }
    }
}
