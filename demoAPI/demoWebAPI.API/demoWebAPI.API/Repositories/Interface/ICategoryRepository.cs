using demoWebAPI.API.model.domain;

namespace demoWebAPI.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<productCategory> CreateAsync(productCategory category);
        Task<IEnumerable<productCategory>> GetAllData();
        Task<productCategory?> GetById(Guid id);
        Task<productCategory?> UpdateCategory(productCategory category);
        Task<productCategory?> DeleteById(Guid id);
        Task<bool> isExist(Guid id);


    }
}
