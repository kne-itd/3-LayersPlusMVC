using BLL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TreatmentRepository : IGenericRepository<Treatment>
    {
        private TreatmentAccess treatmentAccess = new TreatmentAccess();
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Treatment> GetAll()
        {
            List<DAL.Models.Treatment> dalTreatments = treatmentAccess.GetAll() as List<DAL.Models.Treatment>;
            List<Treatment> treatments = new List<Treatment>();
            foreach (var item in dalTreatments)
            {
                treatments.Add(new Treatment
                {
                    treatmentId = item.treatmentId,
                    treatment = item.treatment,
                    Price = item.Price
                });
            }
            return treatments;
        }

        public Treatment GetById(int id)
        {
            DAL.Models.Treatment t = treatmentAccess.GetById(id);
            Treatment treatment = new Treatment
            {
                treatmentId = t.treatmentId,
                treatment = t.treatment,
                Price = t.Price
            };
            return treatment;
        }

        public bool Insert(Treatment t)
        {
            DAL.Models.Treatment dalTreatment = new DAL.Models.Treatment();
            dalTreatment.treatment = t.treatment;
            dalTreatment.Price = t.Price;
            if (treatmentAccess.Create(dalTreatment))
            {
                return true;
            }
            return false;
        }

        public bool Update(Treatment t)
        {
            throw new NotImplementedException();
        }
    }
}
