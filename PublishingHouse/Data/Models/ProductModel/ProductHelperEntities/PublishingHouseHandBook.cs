using System.ComponentModel.DataAnnotations;
using PublishingHouse.Data.Models.ProductModel;

namespace PublishingHouse.Data.Models.ProductModel.ProductHelperEntities
{
    public class PublishingHouseHandBook
    {
        [Key]
        public short Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
