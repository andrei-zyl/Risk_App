using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
    public class ObjectRiskViewModel
    {
        public int ObjectRiskId { get; set; }
        public string ObjectRiskName { get; set; }
        public ObservableCollection<IncidentViewModel> Incidents
        { get; set; }
    }
}
