using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.View_Models
{
    public class RegistrationUserVm
    {

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Role { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 7)]
        public string Password { get; set; }
    }
}
