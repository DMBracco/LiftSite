using System.ComponentModel.DataAnnotations;

namespace LiftSite.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Путь")]
        public string Path { get; set; }

        public int? BrandId { get; set; }
        public Brand Brand { get; set; }

        public string Guid { get; set; }
        public bool Deleted { get; set; }
    }
}
