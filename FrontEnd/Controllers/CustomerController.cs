using BLL;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class CustomerController : Controller
    {
        private OwnerRepository OwnerRepository = new OwnerRepository();
        private OwnerModel GetOwnerById(int id)
        {
            BLL.Models.Owner o = OwnerRepository.GetById(id);
            OwnerModel owner = new OwnerModel
            {
                Id = id,
                fornavn = o.firstname,
                efternavn = o.lastname,
                adresse = o.address,
                postnr = o.zip,
                by = o.city

            };
            return owner;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            List<BLL.Models.Owner> list = OwnerRepository.GetAll() as List<BLL.Models.Owner>;
            List<OwnerModel> owners = new List<OwnerModel>();
            foreach (var item in list)
            {
                owners.Add(new OwnerModel
                {
                    Id = item.ownerId,
                    fornavn = item.firstname,
                    efternavn = item.lastname,
                    adresse = item.address,
                    postnr = item.zip,
                    by = item.city,
                });
            }

            return View(owners);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            //BLL.Models.Owner o = OwnerRepository.GetById(id);
            //OwnerModel owner = new OwnerModel
            //{
            //    Id = id,
            //    fornavn = o.firstname,
            //    efternavn = o.lastname,
            //    adresse = o.address,
            //    postnr = o.zip,
            //    by = o.city

            //};

            return View(GetOwnerById(id));
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OwnerModel o)
        {
            o.postnr = o.postnr.Substring(0, 4);
            try
            {
                BLL.Models.Owner owner = new BLL.Models.Owner
                {
                    firstname = o.fornavn,
                    lastname = o.efternavn,
                    address = o.adresse,
                    zip = o.postnr
                };
                OwnerRepository.Insert(owner);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            //BLL.Models.Owner o = OwnerRepository.GetById(id);
            //OwnerModel owner = new OwnerModel
            //{
            //    Id = id,
            //    fornavn = o.firstname,
            //    efternavn = o.lastname,
            //    adresse = o.address,
            //    postnr = o.zip,
            //    by = o.city

            //};
            return View(GetOwnerById(id));
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OwnerModel o)
        {
            o.postnr = o.postnr.Substring(0, 4);
            try
            {
                    BLL.Models.Owner owner = new BLL.Models.Owner
                    {
                        ownerId = o.Id,
                        firstname = o.fornavn,
                        lastname = o.efternavn,
                        address = o.adresse,
                        zip = o.postnr
                    };
                    OwnerRepository.Update(owner);
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception x)
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/51
        public ActionResult Delete(int id)
        {
            var owner = GetOwnerById(id);
            return View(owner);
        }

        // POST: CustomerController/Delete/5
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
