using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class PatientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime dateOfBirth { get; set; }
        public int Age { get;}
        public PatientModel() 
        {
            Age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(Age))
            {
                Age--;
            }
        }
    }
}
