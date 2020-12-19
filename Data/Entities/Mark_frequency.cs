using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Mark_frequency
    {
        public int Mark_frequencyId { get; set; }
        public string Mark_frequencyName { get; set; }
        public string Mark_frequencyRange { get; set; }
        public decimal Mark_frequencyValue { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}
