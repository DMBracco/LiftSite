using System.ComponentModel.DataAnnotations;

namespace LiftSite.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }
    }
}
