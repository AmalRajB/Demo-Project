using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demoWebAPI.API.model.domain
{
    /// <summary>
    /// Represents a state entity in the system.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Unique identifier for the state.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the state.
        /// </summary>
        [Required(ErrorMessage = "State name is required.")]
        [StringLength(100, ErrorMessage = "State name cannot exceed 100 characters.")]
        public string StateName { get; set; }

        /// <summary>
        /// Foreign key referencing Country.
        /// </summary>
        [Required]
        public Guid CountryId { get; set; }

        /// <summary>
        /// Navigation property to Country.
        /// </summary>
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public ICollection<FileModel> Files { get; set; }
    }
}