using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using LiftSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftSite.Controllers
{
    [Authorize]
    public class TypeEquipmentController : Controller
    {
        private readonly ITypeEquRepository typeEquRepository;
        public TypeEquipmentController(ITypeEquRepository typeEquRepository)
        {
            if (typeEquRepository == null) throw new ArgumentNullException(nameof(typeEquRepository));

            this.typeEquRepository = typeEquRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ModalTypeEquPartial(int id)
        {
            var item = new TypeEquViewModel();
            if (0 != id)
            {
                TypeEquipment typeEq = typeEquRepository.GetTypeEqu(id);
                item = new TypeEquViewModel
                {
                    Id = typeEq.Id,
                    Name = typeEq.Name
                };
            }

            return PartialView("~/Views/TypeEquipment/ModalTypeEquPartial.cshtml", item);
        }

        [HttpGet]
        public IActionResult TableTypeEquPartial()
        {
            var typeEqs = typeEquRepository.GetListTypeEqu();
            var list = new List<TypeEquViewModel>();

            foreach (var type in typeEqs)
            {
                var item = new TypeEquViewModel
                {
                    Id = type.Id,
                    Name = type.Name
                };

                list.Add(item);
            }

            return PartialView("~/Views/TypeEquipment/TableTypeEquPartial.cshtml", list);
        }

        [HttpPost]
        public JsonResult Add(TypeEquViewModel typeEq)
        {
            if (0 == typeEq.Id)
            {
                var model = new TypeEquipment
                {
                    Name = typeEq.Name
                };
                typeEquRepository.CreateTypeEqu(model);
            }
            else
            {
                var model = new TypeEquipment
                {
                    Id = typeEq.Id,
                    Name = typeEq.Name
                };
                typeEquRepository.UpdateTypeEqu(model);
            }
            return Json("Succes");
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try 
            {
                typeEquRepository.DeleteTypeEqu(id);
            }
            catch(Exception ex)
            {
                return Json(ex);
            }
            return Json("Succes");
        }
    }
}
