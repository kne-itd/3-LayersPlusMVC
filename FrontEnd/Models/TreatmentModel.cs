using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class TreatmentModel
    {
        public int treatmentId { get; set; }
        [DisplayName("Behandling")]

        public string treatment { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("Vejl. pris")]

        public Decimal Price { get; set; }
    }
}
