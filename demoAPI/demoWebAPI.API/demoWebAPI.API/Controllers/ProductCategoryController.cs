using demoWebAPI.API.model.domain;
using demoWebAPI.API.model.DTO;
using demoWebAPI.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demoWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public ProductCategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]

        public async Task<IActionResult> AddProductCategory(AddCategoryDto request)
        {
            var productcategory = new productCategory
            {
                CategoryName = request.CategoryName
            };

            var result = await categoryRepository.CreateAsync(productcategory);

            var response = new CategoryDto
            {
                id = result.id,
                CategoryName = result.CategoryName
            };
            return Ok(response);

        }

        [HttpGet]

        public async Task<IActionResult> GetallCategory()
        {
            var results = await categoryRepository.GetAllData();
            var response = new List<CategoryDto>();
            foreach (var result in results)
            {
                response.Add(new CategoryDto
                {
                    id = result.id,
                    CategoryName = result.CategoryName
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetDataById([FromRoute] Guid id)
        {
            var isExist = await categoryRepository.GetById(id);

            if (isExist == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                id = isExist.id,
                CategoryName = isExist.CategoryName
            };
            return Ok(response);

        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            var category = new productCategory
            {
                id = id,
                CategoryName = request.CategoryName
            };

            category = await categoryRepository.UpdateCategory(category);

            if (category == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                id = category.id,
                CategoryName = category.CategoryName
            };
            return Ok(response);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var result = await categoryRepository.DeleteById(id);
            if(result == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                id = result.id,
                CategoryName = result.CategoryName
            };
            return Ok(response);
        }


    }
}
