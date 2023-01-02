using Microsoft.AspNetCore.Mvc;
using RandomStore.Services.Models.CategoryModels;
using RandomStore.Services;
using RandomStore.Application.Models;

namespace RandomStore.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostCategory([FromBody] CategoryCreateModel category)
        {
            var id = await _categoryService.CreateCategoryAsync(category);

            if (id > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("get/all")]
        public IActionResult GetAllCategorys()
        {
            var products = _categoryService.GetAllCategoriesAsync();

            return Ok(products);
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateModel product, [FromRoute] int id)
        {
            var result = await _categoryService.UpdateCategoryAsync(product, id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPatch("upload/{id:int}")]
        public async Task<IActionResult> UploadImage([FromForm] FileModel image, [FromRoute] int id)
        {
            bool result = false;

            using (var stream = image.file.OpenReadStream())
            { 
                result = await _categoryService.UploadPictureAsync(stream, id);
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
            var result = await _categoryService.DeleteCategoryAsync(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
