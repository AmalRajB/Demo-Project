using demoWebAPI.API.model.domain;

namespace demoWebAPI.API.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<Products> CreateProduct(Products products);
        Task<List<Products>> GetAll(int pageNumber, int pageSize, Guid? categoryId, string? search);
        Task<Products?> GetById(Guid id);
        //Task<List<Products>> GetByCategory(Guid id);
        Task<Products?> EditByid(Products products);
        Task<Products?> DeleteById(Guid id);
        Task<int> getTotalCount(Guid? categoryId, string? search);

    }
}
