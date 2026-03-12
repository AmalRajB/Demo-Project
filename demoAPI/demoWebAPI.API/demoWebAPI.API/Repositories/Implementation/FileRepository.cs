using demoWebAPI.API.data;
using demoWebAPI.API.model.domain;
using demoWebAPI.API.model.DTO;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace demoWebAPI.API.Repositories.Implementation
{
    public class FileRepository : IFileRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext dbContext;

        public FileRepository(IWebHostEnvironment webHostEnvironment,
                              ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.dbContext = dbContext;
        }

        public async Task<FileModel?> DeleteFileByid(Guid id)
        {
            var isExist = await dbContext.fileModels.FindAsync(id);
            if(isExist == null)
            {
                return null;
            }
            if (File.Exists(isExist.FilePath))
            {
                File.Delete(isExist.FilePath);
            }
            dbContext.fileModels.Remove(isExist);
            await dbContext.SaveChangesAsync();
            return isExist;

        }

        public async Task<FileModel?> GetById(Guid id)
        {
            return await dbContext.fileModels.FirstOrDefaultAsync(x => x.Id == id);
        
        }

        public Task<List<FileModel>> getByStateId(Guid stateId)
        {
            return dbContext.fileModels.Where(f => f.StateId == stateId).ToListAsync();

        }

        public Task<int> getTotalCount()
        {
            return dbContext.fileModels.CountAsync();
        }

        public async Task<FileModel?> UpdateById(Guid id, FileUploadDto request)
        {
            var IsExist = await dbContext.fileModels.FindAsync(id);
            if(IsExist == null)
            {
                return null;
            }
            var rootPath = Directory.GetCurrentDirectory();
            var folderPath = Path.Combine(rootPath, "FileStorage");
            if (File.Exists(IsExist.FilePath))
            {
                File.Delete(IsExist.FilePath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
            var filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }
            IsExist.FileName = fileName;
            IsExist.FileExtension = Path.GetExtension(request.File.FileName);
            IsExist.FilePath = filePath;
            IsExist.FileSize = request.File.Length;
            IsExist.StateId = request.StateId;

            await dbContext.SaveChangesAsync();
            return IsExist;

        }

        public async Task<FileModel> UploadAsync(FileUploadDto request)
        {
            var file = request.File;
            var rootPath = Directory.GetCurrentDirectory();
            var uploadsFolder = Path.Combine(rootPath, "FileStorage");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);


            //the file will locally stored in the server folder

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            //the indo about the file will stored in the db

            var fileDocument = new FileModel
            {
                Id = Guid.NewGuid(),
                FileName = fileName,
                FileExtension = Path.GetExtension(file.FileName),
                FilePath = filePath,
                FileSize = file.Length,
                StateId = request.StateId

            };

            await dbContext.fileModels.AddAsync(fileDocument);
            await dbContext.SaveChangesAsync();

            return fileDocument;
        }

        async Task<IEnumerable<FileModel>> IFileRepository.GetAll(int pageNumber, int pageSize)
        {
            return await dbContext.fileModels.
                Skip((pageNumber - 1) * pageSize).
                Take(pageSize).
                ToListAsync();
        }
    }
}


