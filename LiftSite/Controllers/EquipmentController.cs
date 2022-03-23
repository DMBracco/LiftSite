using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using LiftSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LiftSite.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquRepository equRepository;
        private readonly IBrandRepository brandRepository;
        private readonly ITypeEquRepository typeEquRepository;
        private readonly IImageRepository imageRepository;
        public EquipmentController(IEquRepository equRepository, IBrandRepository brandRepository, ITypeEquRepository typeEquRepository, IImageRepository imageRepository)
        {
            if (equRepository == null) throw new ArgumentNullException(nameof(equRepository));
            if (brandRepository == null) throw new ArgumentNullException(nameof(brandRepository));
            if (typeEquRepository == null) throw new ArgumentNullException(nameof(typeEquRepository));
            if (imageRepository == null) throw new ArgumentNullException(nameof(imageRepository));

            this.equRepository = equRepository;
            this.brandRepository = brandRepository;
            this.typeEquRepository = typeEquRepository;
            this.imageRepository = imageRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(IFormFile img, EquipmentViewModel equipment)
        {
            int equipmentId;
            if (0 == equipment.Id)
            {
                var model = new Equipment
                {
                    Id = equipment.Id,
                    Name = equipment.Name,
                    BrandId = equipment.BrandId,
                    Model = equipment.Model,
                    Description = equipment.Description,
                    TypeId = equipment.TypeId
                };
                equRepository.CreateEquipment(model);
                equipmentId = equipment.Id;
            }
            else
            {
                var model = new Equipment
                {
                    Id = equipment.Id,
                    Name = equipment.Name,
                    BrandId = equipment.BrandId,
                    Model = equipment.Model,
                    Description = equipment.Description,
                    TypeId = equipment.TypeId
                };
                equRepository.UpdateEquipment(model);
                equipmentId = equipment.Id;
            }
            //_ = ImageCreate(images, equipmentId);
            return Json("Succes");
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                equRepository.DeleteEquipment(id);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            return Json("Succes");
        }

        [HttpPost]
        public JsonResult AddEquipmentImg(IFormFile file, int id)
        {
            return Json(ImageCreate(file, id));
        }

        private async Task<string> ImageCreate(IFormFile file, int id)
        {
            var imageEntry = imageRepository.GetImageByEquipmentId(id);
            var equipmentName = equRepository.GetEquipment(id).Name;
            string guid = Guid.NewGuid().ToString();
            if (file.Length > 0)
            {
                var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\uploadedImg\\equipments",
                        equipmentName);

                CreateFolder(filePath);

                var filePathWeb = Path.Combine("\\uploadedImg\\equipments",
                        file.FileName);

                if (null == imageEntry)
                {
                    var model = new Image
                    {
                        Name = file.FileName,
                        Path = filePathWeb,
                        Guid = guid,
                        EquipmentId = id
                    };

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        imageRepository.CreateImageAsync(model);
                    }
                    return guid;
                }
                else
                {
                    imageEntry.Name = file.FileName;
                    imageEntry.Path = filePathWeb;

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        imageRepository.EditImage(imageEntry);
                    }
                    return guid;
                }
            }
            return "0";
        }

        private void CreateFolder(string path)
        {
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    //Console.WriteLine("That path exists already.");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
            }
            catch (Exception e)
            {
                //Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }

        public IActionResult ModalEquipmentPartial(int id)
        {
            var item = new EquipmentViewModel();

            var brands = brandRepository.GetListBrand();
            var typeEqs = typeEquRepository.GetListTypeEqu();
            if (0 != id)
            {
                Equipment equipment = equRepository.GetEquipment(id);
                item = new EquipmentViewModel
                {
                    Id = equipment.Id,
                    Name = equipment.Name,
                    BrandId = equipment.BrandId,
                    Model = equipment.Model,
                    Description = equipment.Description,
                    //Images = equipment.Images,
                    TypeId = equipment.TypeId
                };
                ViewBag.listBrands = new SelectList(brands, "Id", "Name", equipment.Brand.Id);
                ViewBag.listTypeEqs = new SelectList(typeEqs, "Id", "Name", equipment.TypeEquipment.Id);
            }
            else
            {
                ViewBag.listBrands = new SelectList(brands, "Id", "Name", null);
                ViewBag.listTypeEqs = new SelectList(typeEqs, "Id", "Name", null);
            }

            return PartialView("~/Views/Equipment/ModalEquipmentPartial.cshtml", item);
        }

        [HttpGet]
        public IActionResult TableEquipmentPartial()
        {
            var equipmentList = equRepository.GetListEquipment();
            var list = new List<EquipmentViewModel>();

            foreach (var equipment in equipmentList)
            {
                var item = new EquipmentViewModel
                {
                    Id = equipment.Id,
                    Name = equipment.Name,
                    //Brand = equipment.Brand,
                    Model = equipment.Model,
                    Description = equipment.Description,
                    //Images = equipment.Images,
                    //TypeEquipment = equipment.TypeEquipment
                };

                list.Add(item);
            }

            return PartialView("~/Views/Equipment/TableEquipmentPartial.cshtml", list);
        }
    }
}
