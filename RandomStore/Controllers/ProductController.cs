using AutoMapper;
using Microsoft.AspNetCore.Http;
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
            this._service = service is not null ? service : throw new ArgumentNullException();
            this._toProduct = new Mapper(new MapperConfiguration(conf =>
            conf.CreateMap<Product, ProductCreateModel>()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateModel product)
        {
            var result = await this._service.CreateProductAsync(this._toProduct.Map<Product>(product));

            if (result > 0) return Ok($"Product id={result} created!");

            return BadRequest();
        }
    }
}
