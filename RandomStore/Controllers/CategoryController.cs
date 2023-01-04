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

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateModel category)
        {
            var id = await _categoryService.CreateCategoryAsync(category);

            if (id > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = _categoryService.GetAllCategoriesAsync();

            if (categories == null)
            {
                return BadRequest();
            }

            if (await categories.CountAsync() == 0)
            { 
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryUpdateModel product, 
            [FromRoute] int id)
        {
            var result = await _categoryService.UpdateCategoryAsync(product, id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPatch("save-image/{id:int}")]
        public async Task<IActionResult> SaveImageAsync([FromForm] FileModel image, [FromRoute] int id)
        {
            var result = await _categoryService.UpdatePictureAsync(image.File, id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
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
