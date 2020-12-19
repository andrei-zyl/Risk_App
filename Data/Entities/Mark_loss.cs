using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Mark_loss
    {
        public int Mark_lossId { get; set; }
        public string Mark_lossName { get; set; }
        public string Mark_lossRange { get; set; }
        public decimal Mark_lossValue { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}
