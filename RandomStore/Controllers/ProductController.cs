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

        [HttpPost("post")]
        public async Task<IActionResult> PostProduct([FromBody] ProductCreateModel product, 
            [FromQuery(Name = "categoryId")] int categoryId)
        {
            var result = await _service.CreateProductAsync(product, categoryId);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("get/all")]
        public IActionResult GetAllProducts()
        {
            var products = _service.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await _service.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateModel product, [FromRoute] int id)
        {
            var result = await _service.UpdateProductAsync(product, id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        { 
            var result = await _service.DeleteProductAsync(id);

            if(result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
