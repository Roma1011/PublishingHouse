using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities;
using PublishingHouse.Data.Models.BridgeModels;

namespace PublishingHouse.Data.Models.AuthorModel
{
    public class Author
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        [Column("Personal ID number")]
        public string PersonalNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [StringLength(50, MinimumLength = 4)]
        [Column("Phone number")]
        public string PhoneNumber { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        public short? GenderId { get; set; }
        public GenderHandBook? Gender { get; set; }
        
        public short? CountryId { get; set; }
        public CountryHandBook? Country { get; set; }
        
        public short? CityId { get; set; }
        public CityEntityHandBook? City { get; set; }
        public List<Author_Product> AuthorProducts { get; set; } = new List<Author_Product>();
    }
}