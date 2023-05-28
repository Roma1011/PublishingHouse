using PublishingHouse.Core.IRepository;
using PublishingHouse.Data.Models.UserModel;
using PublishingHouse.Data.View_Models;

namespace PublishingHouse.Data.Services.UserServices
{
    public interface IUserService:IGenericRepository<User>
    {
        public Task<User> Registration(RegistrationUserVm user);
        public Task<string> LogIn(LoginUserVm user);
    }
}
