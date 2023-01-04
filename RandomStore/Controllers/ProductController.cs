using Microsoft.AspNetCore.Mvc;
using RandomStore.Services;
using RandomStore.Services.Models.ProductModels;

namespace RandomStore.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductCreateModel product)
        {
            var result = await _service.CreateProductAsync(product, product.CategoryId);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = _service.GetAllProductsAsync();

            if (products == null)
            {
                return BadRequest();
            }

            if (await products.CountAsync() == 0)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var product = await _service.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductUpdateModel product, 
            [FromRoute] int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var result = await _service.UpdateProductAsync(product, id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var result = await _service.DeleteProductAsync(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
