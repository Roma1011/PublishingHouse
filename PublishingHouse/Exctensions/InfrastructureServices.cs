using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PublishingHouse.Core.IRepository;
using PublishingHouse.Core.Repository;
using PublishingHouse.Data.Context;
using PublishingHouse.Data.Models.AuthorModel;
using PublishingHouse.Data.Services;
using PublishingHouse.Data.Services.AuthorServices;
using PublishingHouse.Data.Services.ContryHandBookService;
using PublishingHouse.Data.Services.GenderServices;
using PublishingHouse.Data.Services.ProductServices;
using PublishingHouse.Data.Services.PublisherHousServices;
using PublishingHouse.Data.Services.UserServices;

namespace PublishingHouse.Exctensions
{
    public static class ConfigureServicesWeb
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICountryHandBookService, CountryHandBookService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPublisherHouseService, PublisherHouseService>();
            services.AddScoped<IGenderService, GenderService>();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Token").Value!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ManagerOnly", policy =>
                {
                    policy.RequireClaim("role", "Manager");
                });
            });
        }
    }
}
