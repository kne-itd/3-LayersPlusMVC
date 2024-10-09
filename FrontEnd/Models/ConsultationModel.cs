using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ConsultationModel
    {
        public int Id { get; set; }
        public PatientModel Patient { get; set; }
        public TreatmentModel Treatment { get; set; }
        [DisplayName("Pris")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ConsultationPrice { get; set; }
    }
}
