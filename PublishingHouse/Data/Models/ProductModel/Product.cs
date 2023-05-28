using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PublishingHouse.Data.Models.BridgeModels;
using PublishingHouse.Data.Models.ProductModel.ProductHelperEntities;

namespace PublishingHouse.Data.Models.ProductModel
{
    public class Product
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 100)]
        public string Annotation { get; set; }

        [Required]
        [RegularExpression("^(book|article|electronic resource)$", ErrorMessage = "Invalid product type")]
        public short? ProductTypeId { get; set; }
        public ProductType? Type { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string Isbn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public short? NumberOfPages { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "varchar(max)")]
        public string Address { get; set; }

        public short? PublishingHouseId { get; set; }
        public PublishingHouseHandBook? PublishingHouses { get; set; }

        public List<Author_Product> AuthorProducts { get; set; } = new List<Author_Product>();
    }
}
