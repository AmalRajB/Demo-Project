using demoWebAPI.API.model.domain;
using demoWebAPI.API.model.DTO;

namespace demoWebAPI.API.Repositories.Interface
{
    public interface IProductImageRepository
    {
        Task<ProductFile> UploadAsync(AddproductimageDto request);
    }
}
