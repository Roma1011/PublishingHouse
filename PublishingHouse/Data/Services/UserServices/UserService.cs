using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PublishingHouse.Core.Repository;
using PublishingHouse.Data.Context;
using PublishingHouse.Data.Models.UserModel;
using PublishingHouse.Data.View_Models;
using PublishingHouse.Filters;

namespace PublishingHouse.Data.Services.UserServices
{
    public class UserService :GenericRepository<User>, IUserService {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        public UserService(IConfiguration config,AppDbContext context) :base(context)
        {
            _context = context;
            _config = config;
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegistrationUserVm, User>();
                cfg.CreateMap<LoginUserVm, User>();
            }));
        }

        public async Task<User> Registration(RegistrationUserVm user)
        {
            var users = await GetAll();
            User? userModel = _mapper.Map<User>(user);

            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.UserName))
            {
                throw new ArgumentNullException("The password and username are required.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            userModel.Password = passwordHash;

            bool userExists = users.Any(u => u.UserName == user.UserName);
            if (userExists)
                throw new InvalidDataException("The username already exists. Please choose a different username.");

            bool saveChangesAsynceResult = await Add(userModel);
            if (!saveChangesAsynceResult)
                throw new DbUpdateException("An error occurred while saving the user. yPlease try again later.");

            return userModel;
        }

        public async Task<string> LogIn(LoginUserVm user)
        {
            var users = await GetAll();
            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.UserName) || users is null)
                throw new ArgumentNullException();

            RegistrationUserVm? data =  users.Where(u => u.UserName == user.UserName)
                .Select(u => new RegistrationUserVm { UserName = u.UserName, Password = u.Password, Role = u.Role })
                .FirstOrDefault();

            if (data is null)
                throw new InvalidDataException("User not found");

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(user.Password, data.Password);

            if (isPasswordCorrect == false)
                throw new InvalidDataException("Incorrect Password");

            string token = CreateToken(data.UserName, data.Role);

            return token;
        }
        private string CreateToken(string userName, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
            };

            if (role == "Manager")
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Token").Value!);
            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
