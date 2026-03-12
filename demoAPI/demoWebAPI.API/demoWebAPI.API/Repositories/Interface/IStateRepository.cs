using demoWebAPI.API.model.domain;

namespace demoWebAPI.API.Repositories.Interface
{
    public interface IStateRepository
    {
        Task<State> CreateAsync(State state);
        //Task<State> validate(string CountryId);
        Task<IEnumerable<State>> getAll(int pageNumber, int pageSize);
        Task<int> getTotalCount();

        Task<State?> GetById(Guid id);
        Task<State?> EditStateById(State state);
        Task<State?> DeleteById(Guid id);
        Task<List<State>> getByCountryId(Guid id);
    }
}
