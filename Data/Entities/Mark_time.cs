using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Mark_time
    {
        public int Mark_timeId { get; set; }
        public string Mark_timeName { get; set; }
        public string Mark_timeRange { get; set; }
        public decimal Mark_timeValue { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}
