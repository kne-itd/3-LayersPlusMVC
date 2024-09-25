using BLL.Models;

namespace BLL
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Owner Owner { get; set; }
        public List<Consultation> Consultations = new List<Consultation>();
    }
}