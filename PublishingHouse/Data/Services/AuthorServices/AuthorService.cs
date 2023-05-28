using System.Runtime.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublishingHouse.Core.Repository;
using PublishingHouse.Data.Context;
using PublishingHouse.Data.Models.AuthorModel;
using PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities;
using PublishingHouse.Data.Models.BridgeModels;
using PublishingHouse.Data.Models.ProductModel;
using PublishingHouse.Data.View_Models;

namespace PublishingHouse.Data.Services.AuthorServices;

public class AuthorService:GenericRepository<Author>,IAuthorService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public AuthorService(AppDbContext context) : base(context)
    {
        _context = context;
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<string, GenderHandBook>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src));
            
            cfg.CreateMap<string, CountryHandBook>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src));
            
            cfg.CreateMap<string, CityEntityHandBook>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src));
            
            cfg.CreateMap<Author, AuthorVmIncludeProduct>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Gender))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.CountryName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.CityName))
                .ForMember(dest => dest.books, opt => opt.MapFrom(src => src.AuthorProducts.Select(ap => ap.Product.Name)));

            cfg.CreateMap<Author, AuthorVMList>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"));
            cfg.CreateMap<AuthorVm, Author>();
            cfg.CreateMap<Author, AuthorVm>();
        }));
    }
    
    public async Task<List<AuthorVMList>> GetAllAuthor(short page)
    {
        var result=await GetAll();
        if (result is null)
            throw new InvalidDataException("No Data");
        
        var pageResult = 3f;
        var pageCount = Math.Ceiling(result.Count() / pageResult);
        var authors =await result.Skip((page - 1) * (int)pageResult)
            .Take((int)pageResult).ToListAsync();
        
        List<AuthorVMList> authorVm = _mapper.Map<List<AuthorVMList>>(authors);

        return authorVm;
    }
    public async Task<AuthorVmIncludeProduct> GetAuthorById(long id)
    {
        var author = await _context.Authors
            .Include(i => i.Country)
            .Include(i => i.City)
            .Include(i => i.Gender)
            .Include(i => i.AuthorProducts).ThenInclude(t => t.Product)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (author is not null)
        {
            AuthorVmIncludeProduct authorVm = _mapper.Map<AuthorVmIncludeProduct>(author);
            return authorVm;
        }
        else
        {
            throw new InvalidDataException("Data Not Existed");
        }
    }
    public async Task<AuthorVm> AddAuthor(AuthorVm authorVm)
    {
        var identifier= _context.Authors.Any(f => f.PersonalNumber == authorVm.PersonalNumber);

        if (identifier==false)
        {
            Author? author = new Author
            {
                Name = authorVm.Name,
                Surname = authorVm.Surname,
                PersonalNumber = authorVm.PersonalNumber,
                DateOfBirth = authorVm.DateOfBirth,
                PhoneNumber = authorVm.PhoneNumber,
                Email = authorVm.Email
            };
            var existingCountry = _context.CauntryHandBooks?.Include(x => x.Cities)?.FirstOrDefault(c => c.CountryName == authorVm.Country);
            author.CountryId = existingCountry?.Id;
            short ?cityId=_context.CityHandBooks.FirstOrDefault(s => s.CityName == authorVm.City)?.Id;
            author.CityId = cityId;
            short? genderId = _context.GenderHandBooks.FirstOrDefault(s => s.Gender == authorVm.Gender)?.Id;
            author.GenderId = genderId;
            try
            {
                await Add(author);
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Something is wrong");
            }
            return authorVm;
        }
        throw new InvalidDataException("Author Already Existed");
    }
    public async Task AddBookForAuthor(string authorPersonalNumber, string isbn)
    {
        var product = await _context.Products.Where(w => w.Isbn == isbn).FirstOrDefaultAsync();
        var author = await _context.Authors.Where(w => w.PersonalNumber == authorPersonalNumber).FirstOrDefaultAsync();

        if (product is null || author is null)
        {
            throw new InvalidDataException("Data Not Excisted");
        }
    
        bool relationshipExists = await _context.AuthorProducts
            .AnyAsync(ap => ap.AuthorId == author.Id && ap.ProductId == product.Id);

        if (relationshipExists)
        {
            throw new InvalidDataContractException("Relationship already exists");
        }
        
        var authorProduct = new Author_Product { Author = author, Product = product };
        _context.AuthorProducts.Add(authorProduct);
        await _context.SaveChangesAsync();
    }
    public async Task<AuthorVm> EditAuthor(long id,AuthorVm author)
    {
        var identyResult =await GetById(id);

        if (identyResult is null)
        {
            throw new InvalidDataException("Data Not existed");
        }

        var updatedAuthor = _mapper.Map(author, identyResult);
        
        var country = _context.CauntryHandBooks.FirstOrDefault(x => x.CountryName == author.Country);
        updatedAuthor.CountryId = country?.Id;
        
        var city = _context.CityHandBooks.FirstOrDefault(s => s.CityName == author.City);
        updatedAuthor.CityId = city?.Id;
        
        var gender = _context.GenderHandBooks.FirstOrDefault(s => s.Gender == author.Gender);
        updatedAuthor.GenderId = gender?.Id;
        
        if (country == null)
        {
            throw new InvalidDataException("country data not valid");
        }

        if (city == null)
        {
            throw new InvalidDataException("City data not valid");
        }

        if (gender == null && (author.Gender!="male" || author.Gender!="female"))
        {
            throw new InvalidDataException("gender data not valid");
        }
        try
        {
            await Update(updatedAuthor);
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Something is Wong");
        }
        
        
        var updatedAuthorVm = _mapper.Map<AuthorVm>(updatedAuthor);
        updatedAuthorVm.Country = country.CountryName;
        updatedAuthorVm.City = city.CityName;
        updatedAuthorVm.Gender = gender.Gender;
        if (updatedAuthor.Gender != null) updatedAuthor.Gender.Gender = gender?.Gender;
        return updatedAuthorVm;
    }
    public async Task DeleteBookForAuthor(string authorPersonalNumber, string isbn)
    {
        var product = await _context.Products.FirstOrDefaultAsync(w => w.Isbn == isbn);
        var author = await _context.Authors.FirstOrDefaultAsync(w => w.PersonalNumber == authorPersonalNumber);

        if (product == null || author == null)
        {
            throw new InvalidOperationException("Data Not Existed");
        }

        var authorProduct = await _context.AuthorProducts
            .FirstOrDefaultAsync(ap => ap.AuthorId == author.Id && ap.ProductId == product.Id);

        if (authorProduct != null)
        {
            _context.AuthorProducts.Remove(authorProduct);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidDataException("Data Not Excisted");
        }
    }
    public async Task DeleteAuthorById(long id)
    {
        var identifier=await GetById(id);
        if (identifier == null)
        {
            throw new InvalidDataException("No Data");
        }
        
        var result = await _context.AuthorProducts
            .Where(w => w.AuthorId == id)
            .Select(s => new { s.AuthorId, s.ProductId })
            .ToListAsync();

        if (result is not null)
        {
            long authorId = Convert.ToInt64(result[0].AuthorId.ToString());
            long productId = Convert.ToInt64(result[0].ProductId.ToString());
            
            string authorPersonalNumber=_context.Authors.Where(w => w.Id == authorId).Select(s => s.PersonalNumber).ToString();
            string productIsbn=_context.Products.Where(w => w.Id == productId).Select(s => s.Isbn).ToString();
            
            await DeleteBookForAuthor(authorPersonalNumber, productIsbn);
        }
        
        try
        {
            await Delete(identifier);
            await Save();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Something is wrong");
        }
        
    }
}