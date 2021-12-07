using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using LiftSite.Models;
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
        public ActionResult Index()
        {
            var Brands = brandRepository.GetListBrand();
            var list = new List<BrandViewModel>();

            foreach (var brand in Brands)
            {
                var item = new BrandViewModel
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Image = brand.Image,
                    Number = brand.Number,
                    Sorting = brand.Sorting,
                };

                list.Add(item);
            }
            return View(list);
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
