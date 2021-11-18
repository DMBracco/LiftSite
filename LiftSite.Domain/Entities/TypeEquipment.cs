using System.ComponentModel.DataAnnotations;

namespace LiftSite.Domain.Entities
{
    public class TypeEquipment
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Слишком длинное название")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}
