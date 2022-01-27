using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiftSite.Models
{
    public class TypeEquViewModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Слишком длинное название")]
        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}
