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
    public class EquipmentController : Controller
    {
        private readonly IEquRepository equRepository;
        public EquipmentController(IEquRepository equRepository)
        {
            if (equRepository == null) throw new ArgumentNullException(nameof(equRepository));

            this.equRepository = equRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ModalTypeEquPartial(int id)
        {
            var item = new EquipmentViewModel();
            if (0 != id)
            {
                Equipment equipment = equRepository.GetEquipment(id);
                item = new EquipmentViewModel
                {
                    Id = equipment.Id,
                    Name = equipment.Name,
                    Brand = equipment.Brand,
                    Model = equipment.Model,
                    Description = equipment.Description,
                    ListImages = equipment.Images,
                    TypeEquipment = equipment.TypeEquipment
                };
            }

            return PartialView("~/Views/TypeEquipment/ModalTypeEquPartial.cshtml", item);
        }

        [HttpGet]
        public IActionResult TableTypeEquPartial()
        {
            var equipmentList = equRepository.GetListEquipment();
            var list = new List<EquipmentViewModel>();

            foreach (var equipment in equipmentList)
            {
                var item = new EquipmentViewModel
                {
                    Id = equipment.Id,
                    Name = equipment.Name,
                    Brand = equipment.Brand,
                    Model = equipment.Model,
                    Description = equipment.Description,
                    ListImages = equipment.Images,
                    TypeEquipment = equipment.TypeEquipment
                };

                list.Add(item);
            }

            return PartialView("~/Views/TypeEquipment/TableTypeEquPartial.cshtml", list);
        }
    }
}
