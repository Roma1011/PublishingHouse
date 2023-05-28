using PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities;
using PublishingHouse.Data.Models.AuthorModel;
using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.View_Models
{
    public class CountryHandBookVm
    {
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string CityName { get; set; }
    }
}
