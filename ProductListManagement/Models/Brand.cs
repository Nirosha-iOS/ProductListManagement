using System.ComponentModel.DataAnnotations;

namespace ProductListManagement.Models
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Brand Logo")]
        public string BrandLogo { get; set; } = string.Empty;
        [Display(Name ="Establised Year")]
        public int EstablisedYear { get; set; }
    }
}
