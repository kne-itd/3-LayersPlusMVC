using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Patient
    {
        public int patientId { get; set; }
        public string patientName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int animalType { get; set; }
    }
}
