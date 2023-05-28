using PublishingHouse.Core.IRepository;
using PublishingHouse.Data.Models.ProductModel;
using PublishingHouse.Data.Models.ProductModel.ProductHelperEntities;
using PublishingHouse.Data.View_Models;

namespace PublishingHouse.Data.Services.ProductServices;

public interface IProductService:IGenericRepository<Product>
{
    public Task<ProductVM> CreateProduct(ProductVM product);
    public Task<ProductType> CreateProductType(string type);
}