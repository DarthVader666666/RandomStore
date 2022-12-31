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
        private IMapper _createMapper;
        private IMapper _updateMapper;
        private readonly ILogger _logger;

        public ProductService(IProductRepository repo, IMapper createMapper, IMapper updateMapper, ILogger logger)
        {
            _repo = repo;
            _createMapper = createMapper;
            _updateMapper = updateMapper;
            _logger = logger;
        }

        public async Task<int> CreateProductAsync(ProductCreateModel productModel)
        {
            if (productModel.QuantityPerUnit is null)
            {
                _logger.LogError("Parameter is null", null);
                return 0;
            }

            try
            {
                var i = 1 / 0;

                var product = _createMapper.Map<Product>(productModel);

                await _repo.CreateAsync(product);
                return product.ProductId;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message, null);
            }

            return 0;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            bool result;

            try 
            {
                result = await _repo.DeleteAsync(id);
            }
            catch
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IAsyncEnumerable<Product> GetAllProductsAsync()
        {
            return _repo.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            if (id < 1)
            { 
                throw new ArgumentException(nameof(id));
            }

            try
            {
                var product = await _repo.GetItemAsync(id);
                return product;
            }
            catch
            { 
                throw new InvalidOperationException();
            }
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateModel productUpdate, int id)
        {
            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            bool result;

            try
            {
                var product = _updateMapper.Map<Product>(productUpdate);
                result = await _repo.UpdateAsync(product, id);
            }
            catch
            { 
                throw new InvalidOperationException();
            }
            
            return result;
        }
    }
}
