using Microsoft.AspNetCore.Mvc;
using RandomStore.Services.Models.CategoryModels;
using RandomStore.Services;

namespace RandomStore.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostCategory([FromBody] CategoryCreateModel category)
        {
            var result = await _service.CreateCategoryAsync(category);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("get/all")]
        public IActionResult GetAllCategorys()
        {
            var products = _service.GetAllCategorysAsync();

            return Ok(products);
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var product = await _service.GetCategoryByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateModel product, [FromRoute] int id)
        {
            var result = await _service.UpdateCategoryAsync(product, id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPatch("upload/{id:int}")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image, [FromRoute] int id)
        {
            bool result = false;

            using (var stream = image.OpenReadStream())
            { 
                result = await _service.UploadPictureAsync(stream, id);
            }

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _service.DeleteCategoryAsync(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
