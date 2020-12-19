using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{  
    public class Mark_quantity
    {
        public int Mark_quantityId { get; set; }
        public string Mark_quantityName { get; set; }
        public string Mark_quantityRange { get; set; }
        public decimal Mark_quantityValue { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}
