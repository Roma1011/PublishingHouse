using PublishingHouse.Core.IRepository;
using PublishingHouse.Data.Models.ProductModel.ProductHelperEntities;

namespace PublishingHouse.Data.Services.PublisherHousServices;

public interface IPublisherHouseService:IGenericRepository<PublishingHouseHandBook>
{
    public Task<PublishingHouseHandBookVM> AddPublishingHouse(PublishingHouseHandBookVM publishingHouseHandBookVM);
}