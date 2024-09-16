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
            foreach (var item in patientRepository.GetPatients())
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

        // GET: PatientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
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
                BLL.Patient p = new BLL.Patient{
                    patientName = patient.Name,
                    dateOfBirth = patient.dateOfBirth,
                    patientId = id
                };
                if (patientRepository.UpdatePatent(p))
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
