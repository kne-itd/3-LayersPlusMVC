using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class TreatMentController : Controller
    {
        private List<TreatmentModel> treatMents = new List<TreatmentModel>();
        private BLL.TreatmentRepository treatmentRepository = new BLL.TreatmentRepository();
        // GET: TreatMentController
        public ActionResult Index()
        {
            foreach (var item in treatmentRepository.GetAll())
            {
                treatMents.Add(new TreatmentModel
                {
                    treatmentId = item.treatmentId,
                    treatment = item.treatment,
                    Price = item.Price
                });
            }
            return View(treatMents);
        }

        // GET: TreatMentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TreatMentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TreatMentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TreatmentModel t)
        {
            BLL.Models.Treatment treatment = new BLL.Models.Treatment();
            treatment.treatment = t.treatment;
            treatment.Price = t.Price;
            try
            {
                treatmentRepository.Insert(treatment);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TreatMentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TreatMentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: TreatMentController/Delete/5
        public ActionResult Delete(int id)
        {
            BLL.Models.Treatment treatment = treatmentRepository.GetById(id);
            TreatmentModel treatmentModel = new TreatmentModel();
            treatmentModel.treatmentId = treatment.treatmentId;
            treatmentModel.treatment = treatment.treatment;
            treatmentModel.Price = treatment.Price;
            return View(treatmentModel);
        }

        // POST: TreatMentController/Delete/5
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
