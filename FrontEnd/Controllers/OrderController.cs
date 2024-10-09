using BLL.Models;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class OrderController : Controller
    {
        private List<OrderModel> orders = new List<OrderModel>();
        private BLL.OrderRepository orderRepository = new BLL.OrderRepository();
        public OrderController()
        {
            foreach (var item in orderRepository.GetAll())
            {
                orders.Add(new OrderModel
                {
                    Id = item.OrderId,
                    OrderDate = item.OrderDate,
                    Name = $"{item.Owner.firstname} {item.Owner.lastname}",
                    Address = item.Owner.address,
                    Zip = item.Owner.zip,
                    City = item.Owner.city
                });
            }
        }
        // GET: OrderController
        public ActionResult Index()
        {
            
            return View(orders);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            Order BllOrder = orderRepository.GetById(id);
            OrderModel order = new OrderModel
            {
                Id = BllOrder.OrderId,
                OrderDate = BllOrder.OrderDate,
                Name = $"{BllOrder.Owner.firstname} {BllOrder.Owner.lastname}",
                Address = BllOrder.Owner.address,
                Zip = BllOrder.Owner.zip,
                City = BllOrder.Owner.city
            };
            order.Consultations = new List<ConsultationModel>();
            float subTotal = 0;
            foreach (var item in BllOrder.Consultations)
            {
                subTotal += (float) item.ConsultationPrice;
                order.Consultations.Add(new ConsultationModel
                {
                    Id = item.Id,
                    ConsultationPrice = item.ConsultationPrice,
                    Patient = new PatientModel
                    {
                        Id = item.Patient.patientId,
                        Name = item.Patient.patientName,
                        dateOfBirth = item.Patient.dateOfBirth

                    },
                    Treatment = new TreatmentModel
                    {
                        treatmentId = item.Treatment.treatmentId,
                        treatment = item.Treatment.treatment,
                        Price = item.Treatment.Price
                    }
                });
            }
            float taxRate = 0.25F;
            float tax = taxRate * subTotal;
            float total = subTotal + tax;
            ViewBag.Subtotal = subTotal.ToString("C2");
            ViewBag.Tax = tax.ToString("C2");
            ViewBag.Total = total.ToString("C2");
            return View(order);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
