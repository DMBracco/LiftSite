using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using LiftSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LiftSite.Controllers
{
    [Authorize]
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
                    BrandImage = brand.BrandImage,
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
                BrandImage = brand.BrandImage,
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
                brandRepository.UpdateBrand(model);

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
            var image = imageRepository.GetImageByBrandId(id);
            string guid = Guid.NewGuid().ToString();
            if (file.Length > 0)
            {
                var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\uploadedImg",
                        file.FileName);
                var filePathWeb = Path.Combine("\\uploadedImg",
                        file.FileName);

                if (null == image)
                {
                    var model = new Image
                    {
                        Name = file.FileName,
                        Path = filePathWeb,
                        Guid = guid,
                        BrandId = id
                    };

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        imageRepository.CreateImageAsync(model);
                    }
                    return Ok(guid);
                }
                else
                {
                    image.Name = file.FileName;
                    image.Path = filePathWeb;


                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        imageRepository.EditImage(image);
                    }
                    return Ok(guid);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult DeleteBrandImg(int id)
        {

            return Ok();
        }
    }
}
