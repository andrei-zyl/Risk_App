using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
    public class LossViewModel
    {
        public int LossId { get; set; }
        public string LossName { get; set; }
        public ObservableCollection<IncidentViewModel> Incidents
        { get; set; }
    }
}
