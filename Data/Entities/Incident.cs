using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Incident
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
        public SourceRisk SourceRisk { get; set; }

        public SourceRisk SourceRiskld
        {
            get => default;
            set
            {
            }
        }
        public int ObjectRiskId { get; set; }
        public ObjectRisk ObjectRisk { get; set; }
        public string ObjectRiskName { get; set; }
        public ObjectRisk ObjectRiskld
        {
            get => default;
            set
            {
            }
        }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public Unit Unitld
        {
            get => default;
            set
            {
            }
        }
        public int LossId { get; set; }
        public Loss Loss { get; set; }

        public Loss Lossld
        {
            get => default;
            set
            {
            }
        }

        public int Mark_frequencyId { get; set; }
        public Mark_frequency Mark_frequency { get; set; }

        public Mark_frequency Mark_frequencyld
        {
            get => default;
            set
            {
            }
        }
        public int Mark_quantityId { get; set; }
        public Mark_quantity Mark_quantity { get; set; }

        public Mark_quantity Mark_quantityld
        {
            get => default;
            set
            {
            }
        }
        public int Mark_lossId { get; set; }
        public Mark_loss Mark_loss { get; set; }

        public Mark_loss Mark_lossld
        {
            get => default;
            set
            {
            }
        }
        public int Mark_timeId { get; set; }
        public Mark_time Mark_time { get; set; }

        public Mark_time Mark_timeld
        {
            get => default;
            set
            {
            }
        }
    }
}

