using PublishingHouse.Core.IRepository;
using PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities;

namespace PublishingHouse.Data.Services;

public interface IGenderService:IGenericRepository<GenderHandBook>
{
    public Task AddGender(string sex);
}