using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using BLL.Models;

namespace BLL
{
    public class PatientRepository : IGenericRepository<BLL.Models.Patient>
    {
        private DataAccess dataAccess;
        public PatientRepository()
        {
            dataAccess = new DataAccess();
        }
        public IEnumerable<BLL.Models.Patient> GetAll()
        {
            List<DAL.Models.Patient>? dalPatients =  dataAccess.GetAll() as List<DAL.Models.Patient>;
            List<BLL.Models.Patient> patients = new List<BLL.Models.Patient>();
            foreach (var item in dalPatients)
            {
                patients.Add(new BLL.Models.Patient
                {
                    animalTypeId = item.animalType,
                    patientId = item.patientId,
                    patientName = item.patientName,
                    dateOfBirth = item.dateOfBirth
                });
            }
            return patients;
        }
        public BLL.Models.Patient GetById(int id)
        {
            DAL.Models.Patient p = dataAccess.GetById(id);
            BLL.Models.Patient patient = new BLL.Models.Patient
            {
                animalTypeId = p.animalType,
                patientId = p.patientId,
                patientName = p.patientName,
                dateOfBirth = p.dateOfBirth
            };
            return patient;
        }
        public bool Update(BLL.Models.Patient patient)
        {
            DAL.Models.Patient p = new DAL.Models.Patient
            {
                patientId = patient.patientId,
                patientName = patient.patientName,
                dateOfBirth = patient.dateOfBirth
            };
            if (dataAccess.Update(p))
            {
                return true;
            }
            return false;
        }
        public bool Insert(BLL.Models.Patient patient)
        {
            DAL.Models.Patient p = new DAL.Models.Patient
            {
                patientName = patient.patientName,
                dateOfBirth = patient.dateOfBirth
            };
            if (dataAccess.Create(p))
            {
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            DataAccess dataAccess = new DataAccess();
            if (dataAccess.Delete(id))
            {
                return true;
            }
            return false;
        }
    }
}
