using LiftSite.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LiftSite.Models
{
    public class BrandViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Слишком длинное название")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Изображение")]
        public Image BrandImage { get; set; }

        [Display(Name = "Количество")]
        public int Number { get; set; }

        [Display(Name = "Сортировка")]
        public int Sorting { get; set; }
    }
}
