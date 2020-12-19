using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
    public class Mark_timeViewModel
    {
        public int Mark_timeId { get; set; }
        public string Mark_timeName { get; set; }
        public string Mark_timeRange { get; set; }
        public decimal Mark_timeValue { get; set; }
        public ObservableCollection<IncidentViewModel> Incidents
        { get; set; }
    }
}