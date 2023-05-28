using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data.Models;
using PublishingHouse.Data.Models.AuthorModel;
using PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities;
using PublishingHouse.Data.Models.BridgeModels;
using PublishingHouse.Data.Models.ProductModel;
using PublishingHouse.Data.Models.ProductModel.ProductHelperEntities;
using PublishingHouse.Data.Models.UserModel;

namespace PublishingHouse.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author_Product>()
                .HasKey(am => new
                {
                    am.AuthorId,
                    am.ProductId
                });
            modelBuilder.Entity<Author_Product>()
                .HasOne(m=>m.Author)
                .WithMany(am=>am.AuthorProducts)
                .HasForeignKey(m=>m.AuthorId);

            modelBuilder.Entity<Author_Product>()
                .HasOne(m => m.Product)
                .WithMany(am => am.AuthorProducts)
                .HasForeignKey(m => m.ProductId);

            modelBuilder.Entity<ProductType>()
                .HasMany(b => b.Product)
                .WithOne(p => p.Type)
                .HasForeignKey(p => p.ProductTypeId);
            
            modelBuilder.Entity<PublishingHouseHandBook>()
                .HasMany(b => b.Products)
                .WithOne(p => p.PublishingHouses)
                .HasForeignKey(p => p.PublishingHouseId);
            
            modelBuilder.Entity<GenderHandBook>()
                .HasMany(h => h.Authors)
                .WithOne(w => w.Gender)
                .HasForeignKey(h => h.GenderId);

            modelBuilder.Entity<CountryHandBook>()
                .HasMany(h => h.Cities)
                .WithOne(w => w.Country)
                .HasForeignKey(h => h.CountryId);

            modelBuilder.Entity<CountryHandBook>()
                .HasMany(h=>h.Authors)
                .WithOne(w => w.Country)
                .HasForeignKey(h => h.CountryId);
            
            modelBuilder.Entity<CityEntityHandBook>()
                .HasMany(h => h.Authors)
                .WithOne(w => w.City)
                .HasForeignKey(h => h.CityId);
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Author_Product> AuthorProducts { get; set; } 
        public DbSet<ProductType>ProductTypesHandBooks { get; set; }
        public DbSet<PublishingHouseHandBook>PublishingHouseHandBooks { get; set; }
        public DbSet<CountryHandBook> CauntryHandBooks { get; set; }
        public DbSet<CityEntityHandBook> CityHandBooks { get; set; }
        public DbSet<GenderHandBook>GenderHandBooks { get; set; }
    }
}
