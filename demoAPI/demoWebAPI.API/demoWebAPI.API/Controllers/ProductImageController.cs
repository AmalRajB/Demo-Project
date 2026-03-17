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
            var result = await productImageRepository.GetAll();

            var response = result.Select(p => new AllproductDetailsDto
            {
                // ProductFile
                ProductFileId = p.id,
                FileName = p.FileName,
               
                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/ProductFiles/{p.FileName}",


                // Product
                ProductId = p.Product.id,
                ProductName = p.Product.productName,
                ProductPrice = p.Product.productPrice,

                // Category
                CategoryId = p.Product.categoryId,
                Category = new CategoryDto
                {
                    id = p.Product.category.id,
                    CategoryName = p.Product.category.CategoryName
                }
            }).ToList();

            return Ok(response);
        }


        [HttpGet("by-id/{id}")]

        public async Task<IActionResult> GetAllDetailsById([FromRoute] Guid id)
        {
            var result = await productImageRepository.GetAllById(id);
            
            if(result == null)
            {
                return NotFound();
            }

            var response = new AllproductDetailsDto
            {
                // ProductFile
                ProductFileId = result.id,
                FileName = result.FileName,

                FileUrl = $"{Request.Scheme}://{Request.Host}/FileStorage/ProductFiles/{result.FileName}",


                // Product
                ProductId = result.Product.id,
                ProductName = result.Product.productName,
                ProductPrice = result.Product.productPrice,

                // Category
                CategoryId = result.Product.categoryId,
                Category = new CategoryDto
                {
                    id = result.Product.category.id,
                    CategoryName = result.Product.category.CategoryName
                }


            };


            return Ok(response);
        }

    }
}

