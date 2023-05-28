using System.ComponentModel.DataAnnotations;
using PublishingHouse.Data.Models.AuthorModel;

namespace PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities
{
    public class CountryHandBook
    {
        [Key]
        public short Id { get; set; }
        [Required]
        public string CountryName { get; set; }
        public List<CityEntityHandBook> Cities { get; set; }=new List<CityEntityHandBook>();
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}
