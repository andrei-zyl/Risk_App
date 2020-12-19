using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    
    public class Loss
    {
        public int LossId { get; set; }
        public string LossName { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}
