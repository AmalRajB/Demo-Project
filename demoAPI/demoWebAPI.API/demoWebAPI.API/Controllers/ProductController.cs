using demoWebAPI.API.model.domain;
using demoWebAPI.API.model.DTO;
using demoWebAPI.API.Repositories.Implementation;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace demoWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]

        public async Task<IActionResult>AddProduct(AddproductDto request)
        {

            var IsExist = await categoryRepository.isExist(request.categoryId);

            if(!IsExist)
            {
                return BadRequest("the category is invalid or not exist");

            }

            var product = new Products
            {
                id = Guid.NewGuid(),
                productName = request.productName,
                productPrice = request.productPrice,
                categoryId = request.categoryId

            };

            var result = await productRepository.CreateProduct(product);

            var response = new ProductDto
            {
                id = result.id,
                productName = result.productName,
                productPrice = result.productPrice,
                categoryId = result.categoryId,
                category = new CategoryDto
                {
                    id = result.category.id,
                    CategoryName = result.category.CategoryName
                }
            };
            return Ok(response);

        }

        [HttpGet]

        public async Task<IActionResult> GetAllProduct([FromQuery] paginationDto pagination, [FromQuery] Guid? categoryId, [FromQuery] string? search)
        {
            var products = await productRepository.GetAll(pagination.PageNumber, pagination.PageSize, categoryId, search);
            var totalRecord = await productRepository.getTotalCount(categoryId, search);
            var response = new List<ProductDto>();

            foreach (var product in products)
            {
                response.Add(new ProductDto
                {
                    id = product.id,
                    productName = product.productName,
                    productPrice = product.productPrice,
                    categoryId = product.categoryId,
                    category = new CategoryDto
                    {
                        id = product.category.id,
                        CategoryName = product.category.CategoryName
                    }
                });  
            }
            return Ok(new
            {
                totalRecord,
                pageNumber = pagination.PageNumber,
                pageSize = pagination.PageSize,
                data = response
            });
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var result = await productRepository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            var response = new ProductDto
            {
                id = result.id,
                productName = result.productName,
                productPrice = result.productPrice,
                categoryId = result.categoryId,
                category = new CategoryDto
                {
                    id = result.category.id,
                    CategoryName = result.category.CategoryName
                }
            };
            return Ok(response);
        }

        //[HttpGet("category/{id:Guid}")]

        //public async Task<IActionResult> GetProductByCategory([FromRoute] Guid id)
        //{
        //    var result = await productRepository.GetByCategory(id);
        //    if (result == null )
        //    {
        //        return NotFound();
        //    }
        //    var response = result.Select(data => new ProductDto
        //    {
        //        id = data.id,
        //        productName = data.productName,
        //        productPrice = data.productPrice,
        //        categoryId = data.categoryId,
        //        category = new CategoryDto
        //        {
        //            id = data.category.id,
        //            CategoryName = data.category.CategoryName
        //        }
        //    }).ToList();
        //    return Ok(response);            
        //}

        [HttpPut]
        [Route("{id:Guid}")]


        public async Task<IActionResult> EditProduct([FromRoute] Guid id, UpdateproductRequestDto request)
        {
            var product = new Products
            {
                id = id,
                productName = request.productName,
                productPrice = request.productPrice,
                categoryId = request.categoryId
            };
            var updated_data = await productRepository.EditByid(product);
            if(updated_data == null)
            {
                return BadRequest();
            }
            var response = new ProductDto
            {
                id = updated_data.id,
                productName = updated_data.productName,
                productPrice = updated_data.productPrice,
                categoryId = updated_data.categoryId,
                category = new CategoryDto
                {
                    id = updated_data.category.id,
                    CategoryName = updated_data.category.CategoryName
                }
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var result = await productRepository.DeleteById(id);
            if (result == null)
            {
                return NotFound();
            }
            var response = new ProductDto
            {
                id = result.id,
                productName = result.productName,
                productPrice = result.productPrice,
                categoryId = result.categoryId
            };
            return Ok(response);
        }

    }
}