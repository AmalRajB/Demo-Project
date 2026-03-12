using System.ComponentModel.DataAnnotations;

namespace demoWebAPI.API.model.domain
{
    /// <summary>
    /// Represents a country entity in the system.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Unique identifier for the country.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the country.
        /// </summary>
        [Required(ErrorMessage = "Country name is required.")]
        [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters.")]
        public string CountryName { get; set; }
        public ICollection<State> States { get; set; }
    }
}