using demoWebAPI.API.model.domain;
using demoWebAPI.API.model.DTO;

namespace demoWebAPI.API.Repositories.Interface
{
    public interface IProductImageRepository
    {
        Task<ProductFile> UploadAsync(AddproductimageDto request);
        Task<ProductFile?> GetById(Guid id);
        Task<List<ProductFile?>> getByProductId(Guid productId);
        Task<ProductFile?> EditById(Guid id, EditProductImageDto request);
        Task<ProductFile?> DeleteById(Guid id);
        Task<ProductFile> AddById(Guid productId, AddImageByProductId request);


        Task<IEnumerable<Products>> GetAll();
        Task<Products?> GetByid(Guid productId);
    }
}
