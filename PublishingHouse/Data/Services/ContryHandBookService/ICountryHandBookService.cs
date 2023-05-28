using PublishingHouse.Data.View_Models;
using System.Diagnostics.Metrics;
using PublishingHouse.Core.IRepository;
using PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities;

namespace PublishingHouse.Data.Services.ContryHandBookService
{
    public interface ICountryHandBookService:IGenericRepository<CityEntityHandBook>
    {
       public Task CreateCountry(CountryHandBookVm country);
    }
}
