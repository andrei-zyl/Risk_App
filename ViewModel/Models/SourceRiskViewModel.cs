using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ViewModel.Models
{
    public class SourceRiskViewModel
    {
        
        public int SourceRiskId { get; set; }
        public string SourceRiskName { get; set; }
        public ObservableCollection<IncidentViewModel> Incidents
        { get; set; }
    }
}