using demoWebAPI.API.model.domain;
using demoWebAPI.API.model.DTO;
using System.IO;

namespace demoWebAPI.API.Repositories.Interface
{
    public interface IFileRepository
    {
        Task<FileModel> UploadAsync(FileUploadDto request);
        Task<IEnumerable<FileModel>> GetAll(int pageNumber, int pageSize);
        Task<FileModel?> GetById(Guid id);
        Task<FileModel?> UpdateById(Guid id, FileUploadDto request);
        Task<FileModel?> DeleteFileByid(Guid id);
        Task<List<FileModel>> getByStateId(Guid stateId);
        Task<int> getTotalCount();
    }
}
