using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using LiftSite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiftSite.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandRepository brandRepository;
        private readonly IImageRepository imageRepository;

        public BrandsController(IBrandRepository brandRepository, IImageRepository imageRepository)
        {
            if (brandRepository == null) throw new ArgumentNullException(nameof(brandRepository));
            if (imageRepository == null) throw new ArgumentNullException(nameof(imageRepository));

            this.imageRepository = imageRepository;
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
                    Number = brand.Number,
                    Sorting = brand.Sorting,
                };

                list.Add(item);
            }
            return View(list);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(BrandEditViewModel brand)
        {
            if (ModelState.IsValid)
            {
                var model = new Brand
                {
                    Name = brand.Name,
                    Number = brand.Number,
                    Sorting = brand.Sorting,
                };
                brandRepository.CreateBrand(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Brand brand = brandRepository.GetBrand(id);
            var item = new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                Number = brand.Number,
                Sorting = brand.Sorting,
            };

            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(BrandViewModel brand)
        {
            if (ModelState.IsValid)
            {
                var model = new Brand
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Number = brand.Number,
                    Sorting = brand.Sorting,
                };
                brandRepository.EditBrand(model);

            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            imageRepository.DeleteImageByBrand(id);
            brandRepository.DeleteBrand(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddBrandImg(IFormFile file, int id)
        {
            string guid = Guid.NewGuid().ToString();
            if (file.Length > 0)
            {
                var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\uploadedImg\\brand",
                        file.FileName);
                var model = new Image
                {
                    Name = file.FileName,
                    Path = filePath,
                    Guid = guid,
                    BrandId = id
                };

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);

                    imageRepository.CreateImage(model);
                }
            }
            return Ok(guid);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBrandImg(int id)
        {
            
            return Ok();
        }
    }
}
