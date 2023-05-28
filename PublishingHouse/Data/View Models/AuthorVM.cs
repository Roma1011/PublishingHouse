using System.ComponentModel.DataAnnotations;
using PublishingHouse.Data.Helper_Atributes;
using PublishingHouse.Filters;

namespace PublishingHouse.Data.View_Models
{
    public class AuthorVm
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; }

        [Required]
        [RegularExpression("^(female|male)$", ErrorMessage = "Invalid gender")]
        public string Gender { get; set; }
        
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string PersonalNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [AdultAge(ErrorMessage = "You must be at least 18 years old.")]
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(50, MinimumLength = 4)]
        public string PhoneNumber { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        public string Country { get; set; }
        
        public string City { get; set; }
    }
    
    public class AuthorVmIncludeProduct
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; }

        [Required]
        public string Gender { get; set; }
        
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string PersonalNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [AdultAge(ErrorMessage = "You must be at least 18 years old.")]
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(50, MinimumLength = 4)]
        public string PhoneNumber { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        public string Country { get; set; }
        
        public string City { get; set; }

        public List<string> books { get; set; }
    }

    public class AuthorVMList
    {
        public long id { get; set; }
        public string FullName { get; set; }

    }
}
