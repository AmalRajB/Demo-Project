using demoWebAPI.API.model.domain;

namespace demoWebAPI.API.Repositories.Interface
{
    public interface IcountryRepository
    {
        Task<Country> CreateAsync(Country country);
        Task<Country?> checkCountry(string countryName);

        Task<IEnumerable<Country>> GetAll();
        Task<Country?> GetById(Guid id);
        Task<Country?> EditbyId(Country  country);

        Task<Country?> DeleteById(Guid id);
        Task<bool> ExistsAsync(Guid id);

    }
}
