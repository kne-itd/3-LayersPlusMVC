using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Treatment Treatment { get; set; }
        public Decimal ConsultationPrice { get; set; }
    }
}
