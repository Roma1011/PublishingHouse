using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.Models.UserModel
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Role { get; set; }
        [MinLength(7)]
        public string Password { get; set; }
    }
}
