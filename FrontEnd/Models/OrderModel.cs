using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public String Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public List<ConsultationModel> Consultations;

    }
}
