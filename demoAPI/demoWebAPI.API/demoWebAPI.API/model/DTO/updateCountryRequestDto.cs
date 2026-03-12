using System.ComponentModel.DataAnnotations;

namespace demoWebAPI.API.model.DTO
{
    public class updateCountryRequestDto
    {
        [Required(ErrorMessage = "Country name is required.")]
        [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Country name must be between 2 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$",
        ErrorMessage = "Country name can only contain letters and spaces.")]
        public string CountryName { get; set; }
    }
}
