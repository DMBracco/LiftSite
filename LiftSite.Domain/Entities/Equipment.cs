using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiftSite.Domain.Entities
{
    public class Equipment
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Слишком длинное название")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Производитель")]
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }

        [Display(Name = "Модель")]
        public string Model { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public List<Image> Images { get; set; }

        [Display(Name = "Тип оборудования")]
        public int Type { get; set; }
        public TypeEquipment TypeEquipment { get; set; }
    }
}
