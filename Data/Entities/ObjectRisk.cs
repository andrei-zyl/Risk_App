using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ObjectRisk
    {     
        public int ObjectRiskId { get; set; }
        public string ObjectRiskName { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}
