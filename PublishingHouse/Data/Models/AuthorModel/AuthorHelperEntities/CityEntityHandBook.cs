using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities
{
    public class CityEntityHandBook
    {
        [Key]
        public short Id { get; set; }
        [Required]
        public string CityName { get; set; }
        public short CountryId { get; set; }
        public CountryHandBook Country { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}
