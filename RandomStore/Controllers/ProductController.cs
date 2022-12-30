using Microsoft.AspNetCore.Mvc;
using RandomStore.Services;
using RandomStore.Services.Models.ProductModels;
using RandomStoreRepo.Entities;

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
                return Ok($"Product id={result} created!");
            }

            return BadRequest();
        }

        [HttpGet("get")]
        public IActionResult GetAllProducts()
        {
            var products = _service.GetAllProductsAsync();

            return Ok(products);
        }
    }
}
