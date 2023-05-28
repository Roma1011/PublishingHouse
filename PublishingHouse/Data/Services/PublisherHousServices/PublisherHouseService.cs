using Microsoft.EntityFrameworkCore;
using PublishingHouse.Core.Repository;
using PublishingHouse.Data.Context;
using PublishingHouse.Data.Models.ProductModel.ProductHelperEntities;

namespace PublishingHouse.Data.Services.PublisherHousServices;

public class PublisherHouseService:GenericRepository<PublishingHouseHandBook>,IPublisherHouseService
{
    private readonly AppDbContext _context;
    public PublisherHouseService(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PublishingHouseHandBookVM> AddPublishingHouse(PublishingHouseHandBookVM publishingHouseHandBookVM)
    {
        bool result=_context.PublishingHouseHandBooks.Any(w => w.Name == publishingHouseHandBookVM.Name);
        if (result is true)
        {
            throw new InvalidDataException("Data already existed");
        }
        
        PublishingHouseHandBook publishingHouseHandBook = new PublishingHouseHandBook
        {
            Name = publishingHouseHandBookVM.Name
        };

        try
        {
            await Add(publishingHouseHandBook);
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Something is wrong");
        }
        return publishingHouseHandBookVM;
    }
}