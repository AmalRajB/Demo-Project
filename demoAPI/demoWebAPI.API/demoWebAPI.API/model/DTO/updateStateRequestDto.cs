using System.ComponentModel.DataAnnotations;

namespace demoWebAPI.API.model.DTO
{
    public class updateStateRequestDto
    {
        [Required]
        [StringLength(100)]
        public string StateName { get; set; }

        [Required]
        public Guid CountryId { get; set; }
    }
}
