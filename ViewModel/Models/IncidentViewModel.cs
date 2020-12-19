using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ViewModel.Models
{
    public class IncidentViewModel
    {
        public int IncidentId { get; set; }
        public DateTime DateOfIncident { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public decimal DirectLoss { get; set; }
        public decimal PotentialLoss { get; set; }
        public int Mark { get; set; }
        public string Measures { get; set; }
        public int SourceRiskId { get; set; }
        public int ObjectRiskId { get; set; }
        public string ObjectRiskName { get; set; }
        public int LossId { get; set; }
        public int UnitId { get; set; }
        public int Mark_frequencyId { get; set; }
        public int Mark_quantityId { get; set; }
        public int Mark_lossId { get; set; }
        public int Mark_timeId { get; set; }
    }
}