using LiftSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiftSite.Models
{
    public class EquipmentViewModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Слишком длинное название")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Производитель")]
        public Brand Brand { get; set; }

        [Display(Name = "Модель")]
        public string Model { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public List<Image> ListImages { get; set; }

        [Display(Name = "Тип оборудования")]
        public TypeEquipment TypeEquipment { get; set; }
    }
}
