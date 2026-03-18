using demoWebAPI.API.model.DTO;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace demoWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository productImageRepository;

        public ProductImageController(IProductImageRepository productImageRepository)
        {
            this.productImageRepository = productImageRepository;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> AddproductImage([FromForm] AddproductimageDto request)
        {
            if(request.File == null || request.File.Length == 0)
            {
                return BadRequest("file field is required");
            }

            var result = await productImageRepository.UploadAsync(request);

            var response = new ProductImageDto
            {

                Id = result.id,
                FileName = result.FileName,
                FileExtension = result.FileExtension,
                FileSize = result.FileSize,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/ProductFiles/{result.FileName}",
                CreatedDate = result.CreatedDate,
                ProductId = result.ProductId
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetImageById([FromRoute] Guid id)
        {
            var result = await productImageRepository.GetById(id);

            if(result == null)
            {
                return NotFound();
            }

            var response = new ProductImageDto
            {
                Id = result.id,
                FileName = result.FileName,
                FileExtension = result.FileExtension,
                FileSize = result.FileSize,
                CreatedDate = result.CreatedDate,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/ProductFiles/{result.FileName}",
                ProductId = result.ProductId

            };

            return Ok(response);

        }

        [HttpGet("by-product/{productId}")]

        public async Task<IActionResult> getByproduct(Guid productId)
        {
            var results = await productImageRepository.getByProductId(productId);

            if(results == null)
            {
                return NotFound();
            }

            var response = results.Select(file => new ProductImageDto
            {
                Id = file.id,
                FileName = file.FileName,
                FileExtension = file.FileExtension,
                FileSize = file.FileSize,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/ProductFiles/{file.FileName}",
                CreatedDate = file.CreatedDate
            }).ToList();
            return Ok(response);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult>UpdateproductImage(Guid id, [FromForm] EditProductImageDto request)
        {
            var Updated_file = await productImageRepository.EditById(id, request);

            if(Updated_file == null)
            {
                return NotFound();
            }
            var response = new ProductImageDto
            {
                Id = Updated_file.id,
                FileName = Updated_file.FileName,
                FileExtension = Updated_file.FileExtension,
                FileSize = Updated_file.FileSize,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/ProductFiles/{Updated_file.FileName}",
                CreatedDate = Updated_file.CreatedDate,
                ProductId = Updated_file.ProductId

            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteImage([FromRoute] Guid id)
        {
            var result = await productImageRepository.DeleteById(id);

            if(result == null)
            {
                return NotFound();
            }

            var response = new ProductImageDto
            {
                Id = result.id,
                FileName = result.FileName,
                FileExtension = result.FileExtension,
                FileSize = result.FileSize,
                CreatedDate = result.CreatedDate,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/ProductFiles/{result.FileName}",
                ProductId = result.ProductId
            };
            return Ok(response);

        }

        [HttpPost("by-product/{productId}")]

        public async Task<IActionResult>UploadByProductid(Guid productId, [FromForm] AddImageByProductId request)
        {
            if (request.File == null || request.File.Length == 0)
            {
                return BadRequest("file field is required");
            }

            var result = await productImageRepository.AddById(productId, request);

            var response = new ProductImageDto
            {

                Id = result.id,
                FileName = result.FileName,
                FileExtension = result.FileExtension,
                FileSize = result.FileSize,
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/ProductFiles/{result.FileName}",
                CreatedDate = result.CreatedDate,
                ProductId = result.ProductId
            };
            return Ok(response);

        }


        [HttpGet("all-details")]
        public async Task<IActionResult> GetAllDetails()
        {
            var products = await productImageRepository.GetAll();

            var response = products.Select(product => new AllproductDetailsDto
            {
                ProductId = product.id,
                ProductName = product.productName,
                ProductPrice = product.productPrice,

                Category = new CategoryDto
                {
                    id = product.category.id,
                    CategoryName = product.category.CategoryName
                },

                ProductFiles = product.productFiles
                    .OrderBy(f => f.CreatedDate)
                    .Take(1)
                    .Select(f => new ProductImageDto
                    {
                        Id = f.id,
                        FileName = f.FileName,
                        FileExtension = f.FileExtension,
                        FileSize = f.FileSize,
                        FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/ProductFiles/{f.FileName}",
                        CreatedDate = f.CreatedDate,
                        ProductId = f.ProductId
                    }).ToList()

            }).ToList();

            return Ok(new
            {
                success = true,
                message = "Products retrieved successfully",
                data = response
            });
        }


        [HttpGet("all-details/{productId:Guid}")]
        public async Task<IActionResult> GetAllDetailsById([FromRoute] Guid productId)
        {
            var product = await productImageRepository.GetByid(productId);

            if (product == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Product not found",
                    data = (object?)null
                });
            }

            var response = new AllproductDetailsDto
            {
                ProductId = product.id,
                ProductName = product.productName,
                ProductPrice = product.productPrice,

                Category = new CategoryDto
                {
                    id = product.category.id,
                    CategoryName = product.category.CategoryName
                },

                ProductFiles = product.productFiles
                    .OrderBy(f => f.CreatedDate)
                    .Select(f => new ProductImageDto
                    {
                        Id = f.id,
                        FileName = f.FileName,
                        FileExtension = f.FileExtension,
                        FileSize = f.FileSize,
                        FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/ProductFiles/{f.FileName}",
                        CreatedDate = f.CreatedDate,
                        ProductId = f.ProductId
                    }).ToList()
            };

            return Ok(new
            {
                success = true,
                message = "Product details retrieved successfully",
                data = response
            });
        }






    }
}

