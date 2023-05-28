using PublishingHouse.Data.Models.AuthorModel;
using PublishingHouse.Data.Models.ProductModel;

namespace PublishingHouse.Data.Models.BridgeModels;

public class Author_Product
{
    public long AuthorId { get; set; }
    public Author Author { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
}