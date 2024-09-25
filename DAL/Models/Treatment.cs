using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Treatment
    {
        public int treatmentId { get; set; }
        public string treatment { get; set; }
        public Decimal Price { get; set; }
    }
}
