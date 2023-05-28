using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Data.Helper_Atributes
{
    public class AdultAgeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateOfBirth)
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;
                if (dateOfBirth > today.AddYears(-age))
                    age--;

                return age >= 18;
            }

            return false;
        }
    }
}
