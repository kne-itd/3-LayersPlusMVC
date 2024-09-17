using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PatientRepository : IRepository
    {
        public List<Patient> Read()
        {
            DAL.DataAccess dataAccess = new DAL.DataAccess();
            List<DAL.Patient> dalPatients = dataAccess.GetPatients();
            List<Patient> patients = new List<Patient>();
            foreach (var item in dalPatients)
            {
                patients.Add(new Patient
                {
                    animalType = item.animalType,
                    patientId = item.patientId,
                    patientName = item.patientName,
                    dateOfBirth = item.dateOfBirth
                });
            }
            return patients;
        }
        public bool Update(Patient patient)
        {
            DAL.Patient p = new DAL.Patient
            {
                patientId = patient.patientId,
                patientName = patient.patientName,
                dateOfBirth = patient.dateOfBirth
            };
            DAL.DataAccess dataAccess = new DAL.DataAccess();
            if (dataAccess.Update(p))
            {
                return true;
            }
            return false;
        }
        public bool Create(Patient patient)
        {
            DAL.Patient p = new DAL.Patient
            {
                patientName = patient.patientName,
                dateOfBirth = patient.dateOfBirth
            };
            DataAccess dataAccess = new DataAccess();
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
