using System.ComponentModel.DataAnnotations;

namespace LiftSite.Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }

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
