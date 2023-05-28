using System.ComponentModel.DataAnnotations;
using PublishingHouse.Data.Models.ProductModel;

namespace PublishingHouse.Data.Models.ProductModel.ProductHelperEntities
{
    public class PublishingHouseHandBookVM
    {
        [Required]
        public string Name { get; set; }
    }
}
