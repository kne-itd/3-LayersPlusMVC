using BLL.Models;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class PatientController : Controller
    {
        private List<PatientModel> patients = new List<PatientModel>();
        private BLL.PatientRepository patientRepository = new BLL.PatientRepository();
        public PatientController()
        {
            foreach (var item in patientRepository.GetAll())
            {
                patients.Add(new PatientModel
                {
                    Id = item.patientId,
                    Name = item.patientName,
                    dateOfBirth = item.dateOfBirth
                });
            }
        }
        // GET: PatientController
        public ActionResult Index()
        {
            return View(patients);
        }

        // GET: PatientController/Details/5
        public ActionResult Details(int id)
        {
            foreach (var item in patients)
            {
                if (item.Id == id)
                {
                    return View(item);
                }
            }
            return View();
        }

        // GET: PatientController/Insert
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Insert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientModel patient)
        {
            try
            {
                Patient p = new BLL.Models.Patient
                {
                    patientName = patient.Name,
                    dateOfBirth = patient.dateOfBirth
                };
                if (patientRepository.Insert(p))
                {
                    return RedirectToAction(nameof(Index));
                };
                return View();
            }
            catch 
            {
                return View();
            }
        }

        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
            foreach (var item in patients)
            {
                if (item.Id == id)
                {
                    return View(item);
                }
            }
            return View();
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PatientModel patient)
        {
            try
            {
                Patient p = new BLL.Models.Patient{
                    patientName = patient.Name,
                    dateOfBirth = patient.dateOfBirth,
                    patientId = id
                };
                if (patientRepository.Update(p))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
                return View();
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (patientRepository.Delete(id))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
