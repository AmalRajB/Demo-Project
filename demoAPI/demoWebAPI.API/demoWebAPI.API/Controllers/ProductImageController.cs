using demoWebAPI.API.model.DTO;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
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

    }
}
