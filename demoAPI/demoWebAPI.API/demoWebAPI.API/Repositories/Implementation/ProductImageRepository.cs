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

    }

}


