using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.Models.AuthorModel.AuthorHelperEntities
{
    public class GenderHandBook
    {
        [Key]
        public short Id { get; set; }
        [Required]
        [StringLength(6, MinimumLength = 4)]
        public string? Gender { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}
