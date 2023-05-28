using Microsoft.EntityFrameworkCore;
using PublishingHouse.Core.Repository;
using PublishingHouse.Data.Context;
using PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities;

namespace PublishingHouse.Data.Services.GenderServices;

public class GenderService:GenericRepository<GenderHandBook>,IGenderService
{
    private readonly AppDbContext _context;
    public GenderService(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddGender(string sex)
    {
        bool existeResult=_context.GenderHandBooks.Any(a => a.Gender == sex);

        if (existeResult is true)
        {
            throw new InvalidDataException("The Gender already existed");
        }
        
        GenderHandBook genderHandBook = new GenderHandBook
        {
            Gender = sex
        };
        
        try
        {
            await Add(genderHandBook);
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException("Something is Wrong");
        }
    }
}