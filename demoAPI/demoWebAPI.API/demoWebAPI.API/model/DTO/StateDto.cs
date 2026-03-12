using demoWebAPI.API.model.domain;

namespace demoWebAPI.API.model.DTO
{
    public class StateDto
    {
        public Guid Id { get; set; }
        public string StateName { get; set; }
        public Guid CountryId { get; set; }
        public CountryDto Country { get; set; }


    }
}
