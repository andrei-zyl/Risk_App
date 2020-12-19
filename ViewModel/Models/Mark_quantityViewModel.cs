using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
    public class Mark_quantityViewModel
    {
        public int Mark_quantityId { get; set; }
        public string Mark_quantityName { get; set; }
        public string Mark_quantityRange { get; set; }
        public decimal Mark_quantityValue { get; set; }
        public ObservableCollection<IncidentViewModel> Incidents
        { get; set; }
    }
}
