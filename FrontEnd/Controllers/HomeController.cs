using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            List<PatientModel> patients = new List<PatientModel>();
            BLL.PatientRepository patientRepository = new BLL.PatientRepository();
            foreach (var item in patientRepository.GetPatients())
            {
                patients.Add(new PatientModel
                {
                    Id = item.patientId,
                    Name = item.patientName,
                    dateOfBirth = item.dateOfBirth
                });
            }

            return View(patients);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
