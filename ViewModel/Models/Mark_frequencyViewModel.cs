using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
    public class Mark_frequencyViewModel
    {
        public int Mark_frequencyId { get; set; }
        public string Mark_frequencyName { get; set; }
        public string Mark_frequencyRange { get; set; }
        public decimal Mark_frequencyValue { get; set; }
        public ObservableCollection<IncidentViewModel> Incidents
        { get; set; }
    }
}
