using demoWebAPI.API.data;
using demoWebAPI.API.model.domain;
using demoWebAPI.API.model.DTO;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace demoWebAPI.API.Repositories.Implementation
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductImageRepository(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<ProductFile> AddById(Guid productId, AddImageByProductId request)
        {
            var productExists = await dbContext.products

            .AnyAsync(p => p.id == productId);

            if (!productExists)
            {
                throw new Exception("Product does not exist");
            }
            var file = request.File;
            var rootPath = Directory.GetCurrentDirectory();
            var uploadsFolder = Path.Combine(rootPath, "FileStorage", "ProductFiles");
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

            var fileDocument = new ProductFile
            {
                id = Guid.NewGuid(),
                FileName = fileName,
                FileExtension = Path.GetExtension(file.FileName),
                FilePath = "/FileStorage/ProductFiles/" + fileName,
                FileSize = file.Length,
                ProductId = productId

            };

            await dbContext.productFiles.AddAsync(fileDocument);
            await dbContext.SaveChangesAsync();

            return fileDocument;
        }


        public async Task<ProductFile?> DeleteById(Guid id)
        {
            var isExist = await dbContext.productFiles.FindAsync(id);

            if(isExist == null)
            {
                return null;
            }
            if (File.Exists(isExist.FilePath))
            {
                File.Delete(isExist.FilePath);
            }
            dbContext.productFiles.Remove(isExist);
            await dbContext.SaveChangesAsync();
            return isExist;
        }

        public async Task<IEnumerable<ProductFile>> GetAll()
        {
            return await dbContext.productFiles
            .Include(p => p.Product)
            .ThenInclude(p => p.category)
            .ToListAsync();
        }

        public async Task<ProductFile?> GetAllById(Guid id)
        {
            return await dbContext.productFiles
            .Include(p => p.Product)
            .ThenInclude(p => p.category)
            .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<ProductFile?> GetById(Guid id)
        {
            return await dbContext.productFiles.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<List<ProductFile?>> getByProductId(Guid productId)
        {
            return await dbContext.productFiles.Where(p => p.ProductId == productId).ToListAsync();
        }

        public async Task<ProductFile> UploadAsync(AddproductimageDto request)
        {
            var productExists = await dbContext.products

            .AnyAsync(p => p.id == request.ProductId);

            if (!productExists)
            {
                throw new Exception("Product does not exist");
            }

            Console.WriteLine(request.ProductId);

            var file = request.File;
            var rootPath = Directory.GetCurrentDirectory();
            var uploadsFolder = Path.Combine(rootPath, "FileStorage", "ProductFiles");
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

            var fileDocument = new ProductFile
            {
                id = Guid.NewGuid(),
                FileName = fileName,
                FileExtension = Path.GetExtension(file.FileName),
                FilePath = "/FileStorage/ProductFiles/" + fileName,
                FileSize = file.Length,
                ProductId = request.ProductId

            };

            await dbContext.productFiles.AddAsync(fileDocument);
            await dbContext.SaveChangesAsync();

            return fileDocument;
        }

        async Task<ProductFile?> IProductImageRepository.EditById(Guid id, EditProductImageDto request)
        {
            var IsExist = await dbContext.productFiles.FindAsync(id);

            if(IsExist == null)
            {
                return null;
            }
            var rootPath = Directory.GetCurrentDirectory();
            var folderPath = Path.Combine(rootPath, "FileStorage", "ProductFiles");
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
            IsExist.ProductId = request.ProductId;

            await dbContext.SaveChangesAsync();
            return IsExist;


        }
    }

}


