using PublishingHouse.Core.IRepository;
using PublishingHouse.Data.HelperClases;
using PublishingHouse.Data.Models.AuthorModel;
using PublishingHouse.Data.View_Models;

namespace PublishingHouse.Data.Services.AuthorServices;

public interface IAuthorService:IGenericRepository<Author>
{
    public Task<List<AuthorVMList>> GetAllAuthor(short page);
    public Task<AuthorVmIncludeProduct> GetAuthorById(long id);
    public Task<AuthorVm> AddAuthor(AuthorVm authorVm);
    public Task AddBookForAuthor(string authorPersonalNumber,string isbn);
    public Task<AuthorVm> EditAuthor(long id, AuthorVm authorVm);
    Task DeleteBookForAuthor(string authorPersonalNumber, string isbn);
    public Task DeleteAuthorById(long id);
}