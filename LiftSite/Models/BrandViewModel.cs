using System.ComponentModel.DataAnnotations;

namespace LiftSite.Models
{
    public class BrandViewModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Слишком длинное название")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Display(Name = "Количество")]
        public int Number { get; set; }

        [Display(Name = "Сортировка")]
        public int Sorting { get; set; }
    }
}
