using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class SourceRisk
    {
        
        public int SourceRiskId { get; set; }
        public string SourceRiskName { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}
