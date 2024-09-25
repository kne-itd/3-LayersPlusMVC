namespace FrontEnd.Models
{
    public class ConsultationModel
    {
        public int Id { get; set; }
        public PatientModel Patient { get; set; }
        public TreatmentModel Treatment { get; set; }
        public Decimal ConsultationPrice { get; set; }
    }
}
