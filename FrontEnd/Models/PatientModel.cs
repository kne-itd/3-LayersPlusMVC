using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class PatientModel
    {
        public int Id { get; set; }
        [DisplayName("Patientnavn")]

        public string Name { get; set; }

        private DateTime dob;
        [DataType(DataType.Date)]
        public DateTime dateOfBirth 
        { 
            get { return dob;}
            set 
            {
                dob = value;
                Age = DateTime.Today.Year - dateOfBirth.Year;
                if (dateOfBirth.Date > DateTime.Today.AddYears(-Age))
                {
                    Age--;
                }
            }
        }
        public int Age { get; private set;}
    }
}
