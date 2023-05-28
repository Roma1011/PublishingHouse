using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.View_Models
{
    public class ProductVM
    {
        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 100)]
        public string Annotation { get; set; }
        
        [Required]
        [RegularExpression("^(book|article|electronic resource)$", ErrorMessage = "Invalid product type")]
        public string? Type { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string Isbn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        
        public short? NumberOfPages { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }
        
        public string? PublishingHouses { get; set; }
    }
}
