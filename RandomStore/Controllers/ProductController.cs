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
        public async Task<IActionResult> PostProduct([FromBody] ProductCreateModel product)
        {
            var result = await _service.CreateProductAsync(product);

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
    }
}
