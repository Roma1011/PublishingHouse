using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Core.Repository;
using PublishingHouse.Data.Context;
using PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities;
using PublishingHouse.Data.View_Models;

namespace PublishingHouse.Data.Services.ContryHandBookService;
public class CountryHandBookService :GenericRepository<CityEntityHandBook>, ICountryHandBookService
{
    private readonly AppDbContext _context;
    private readonly Mapper _mapper;
    public CountryHandBookService(AppDbContext context) : base(context)
    {
        _context = context;
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CountryHandBookVm, CountryHandBook>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.CountryName));

            cfg.CreateMap<CountryHandBookVm, CityEntityHandBook>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName));
        }));
        }

    public async Task CreateCountry(CountryHandBookVm country)
    {

        var existingCountry = _context.CauntryHandBooks.Include(x => x.Cities).FirstOrDefault(c => c.CountryName == country.CountryName);
        if (existingCountry != null)
        {
            bool cityExists = existingCountry.Cities.Any(c => c.CityName == country.CityName);
            if (cityExists)
            {
                throw new ArgumentException("The Country and City already exist");
            }
            else
            {
                CityEntityHandBook cityNew = _mapper.Map<CityEntityHandBook>(country);
                cityNew.Country = existingCountry;
                await Add(cityNew);
            }
        }
        else
        {
            var countryNew = new CountryHandBook
            {
                CountryName = country.CountryName,
            };
            await _context.AddAsync(countryNew);
            await _context.SaveChangesAsync();
            
            var cityNew = new CityEntityHandBook
            {
                CityName = country.CityName,
                Country = countryNew
            };
            await _context.AddAsync(cityNew);
            await _context.SaveChangesAsync();
        }
    }
}
