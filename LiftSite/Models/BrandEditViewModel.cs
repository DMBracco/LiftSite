using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiftSite.Models
{
    public class BrandEditViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Слишком длинное название")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Количество")]
        public int Number { get; set; }

        [Display(Name = "Сортировка")]
        public int Sorting { get; set; }
    }
}
