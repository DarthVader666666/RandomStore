using AutoMapper;
using Microsoft.Extensions.Logging;
using RandomStore.Repository.Repositories.ProductRepositories;
using RandomStore.Services.Models.ProductModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductService(IProductRepository repo, IMapper mapper, ILogger logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateProductAsync(ProductCreateModel productModel, int categoryId)
        {
            if (productModel.QuantityPerUnit == null)
            {
                _logger.LogError("Parameter is null");
                return 0;
            }

            try
            {
                var product = _mapper.Map<Product>(productModel);
                product.CategoryId = categoryId;
                await _repo.CreateAsync(product);
                return product.ProductId;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return 0;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var result = false;

            try 
            {
                result = await _repo.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return result;
        }

        public async IAsyncEnumerable<ProductGetModel> GetAllProductsAsync()
        {
            await foreach (var item in _repo.GetAll())
            {
                yield return _mapper.Map<ProductGetModel>(item);
            }
        }

        public async Task<ProductGetModel> GetProductByIdAsync(int id)
        {
            if (id < 1)
            {
                _logger.LogError("Wrong id.");
                return null;
            }

            try
            {
                var product = await _repo.GetItemAsync(id);
                return _mapper.Map<ProductGetModel>(product);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return null;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateModel productUpdate, int id)
        {
            if (id < 1)
            {
                _logger.LogError("Wrong Id.");
                return false;
            }

            var result = false;

            try
            {
                var product = _mapper.Map<Product>(productUpdate);
                result = await _repo.UpdateAsync(product, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            
            return result;
        }
    }
}
