using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftSite.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandRepository brandRepository;

        public BrandsController(IBrandRepository brandRepository)
        {
            if (brandRepository == null) throw new ArgumentNullException(nameof(brandRepository));

            this.brandRepository = brandRepository;
        }
        public IActionResult Index()
        {
            return View(brandRepository.GetListBrand());
        }
        public IActionResult Add()
        {
            return View("Edit");
        }

        public ActionResult Edit(int id)
        {
            Brand b = brandRepository.GetBrand(id);

            return View(b);
        }

        [HttpPost]
        public ActionResult Edit(Brand model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    brandRepository.CreateBrand(model);
                }
                else
                {
                    brandRepository.EditBrand(model);
                }
            }
            return RedirectToAction("Edit", "Brands");
        }

        public ActionResult Delete(int id)
        {
            brandRepository.DeleteBrand(id);

            return View();
        }
    }
}
