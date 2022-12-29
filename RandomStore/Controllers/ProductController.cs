using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RandomStore.Application.Models.ProductModels;
using RandomStore.Services;
using RandomStoreRepo.Entities;

namespace RandomStore.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _toProduct;

        public ProductController(IProductService service)
        {
            this._service = service is null ? throw new ArgumentNullException() : service;
            this._toProduct = new Mapper(new MapperConfiguration(conf =>
            conf.CreateMap<ProductCreateModel, Product>()));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(ProductCreateModel productModel)
        {
            var product = this._toProduct.Map<Product>(productModel);

            var result = await this._service.CreateProductAsync(product);

            if (result > 0)
            {
                return Ok($"Product id={result} created!");
            }

            return BadRequest();
        }
    }
}
