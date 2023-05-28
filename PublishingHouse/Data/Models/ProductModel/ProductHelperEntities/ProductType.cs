using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.Models.ProductModel.ProductHelperEntities;

public class ProductType
{
    [Key]
    public short Id { get; set; }
    public string Type { get; set; }
    public List<Product>? Product { get; set; } = new List<Product>();
}