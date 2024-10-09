using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Patient
    {
        public int patientId { get; set; }
        public string patientName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int animalTypeId { get; set; }
        public AnimalType animalType { get; set; }
    }
}
