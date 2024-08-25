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
        [Display(Name = "Establised Year")]
        public int EstablisedYear { get; set; }
        public string Headquarters { get; set; } = string.Empty;
        public string Founder { get; set; } = string.Empty;
        public string CEO { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Display(Name = "Website URL")]
        public string WebsiteURL { get; set; } = string.Empty;

        [Display(Name = "Parent Company")]
        public string ParentCompany { get; set; } = string.Empty;
    }
}
