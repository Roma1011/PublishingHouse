using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Core.Repository;
using PublishingHouse.Data.Context;
using PublishingHouse.Data.Models.AuthorModel;
using PublishingHouse.Data.Models.ProductModel;
using PublishingHouse.Data.Models.ProductModel.ProductHelperEntities;
using PublishingHouse.Data.View_Models;

namespace PublishingHouse.Data.Services.ProductServices;

public class ProductService:GenericRepository<Product>,IProductService
{
    private readonly AppDbContext _context;
    public ProductService(IConfiguration config,AppDbContext context) :base(context)
    {
        _context = context;
    }

    public async Task<ProductVM> CreateProduct(ProductVM productvm)
    {
        bool result=_context.Products.Any(a => a.Isbn == productvm.Isbn);
        if (result is true)
        {
            throw new InvalidDataException("Data already existed");
        }
        Product product = new Product
        {
            Name = productvm.Name,
            Annotation = productvm.Annotation,
            Isbn = productvm.Isbn,
            ReleaseDate = productvm.ReleaseDate,
            NumberOfPages = productvm.NumberOfPages,
            Address = productvm.Address
        };
        var publiceHouseHandId = _context.PublishingHouseHandBooks.FirstOrDefault(f => f.Name == productvm.PublishingHouses)?.Id;
        product.PublishingHouseId = publiceHouseHandId;
        product.ProductTypeId = _context.ProductTypesHandBooks.FirstOrDefault(f => f.Type == productvm.Type)?.Id;
        try
        {
            await Add(product);
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Something is wrong");
        }
        return productvm;
    }

    public async Task<ProductType> CreateProductType(string type)
    { 
        bool result=_context.ProductTypesHandBooks.Any(a => a.Type == type);
        
        if (result is true)
        {
            throw new InvalidDataException("Data already existed");
        }
        ProductType productType = new ProductType
        {
            Type = type,
        };

        try
        {
            await _context.ProductTypesHandBooks.AddAsync(productType);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Something is wrong");
        }
       
        return productType;
    }
}