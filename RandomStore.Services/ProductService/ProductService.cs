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
        private IMapper _mapper;
        private readonly ILogger _logger;

        public ProductService(IProductRepository repo, IMapper mapper, ILogger logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateProductAsync(ProductCreateModel productModel)
        {
            if (productModel.QuantityPerUnit is null)
            {
                _logger.LogError(GenerateDateString() + "Parameter is null");
                return 0;
            }

            try
            {
                var product = _mapper.Map<Product>(productModel);

                await _repo.CreateAsync(product);
                return product.ProductId;
            }
            catch(Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return 0;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            bool result = false;

            try 
            {
                result = await _repo.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
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
                _logger.LogError(GenerateDateString() + "Wrong id.");
                return null;
            }

            try
            {
                var product = await _repo.GetItemAsync(id);
                return product;
            }
            catch (Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return null;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateModel productUpdate, int id)
        {
            if (id < 1)
            {
                _logger.LogError(GenerateDateString() + "Wrong Id.");
                return false;
            }

            bool result = false;

            try
            {
                var product = _mapper.Map<Product>(productUpdate);
                result = await _repo.UpdateAsync(product, id);
            }
            catch(Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
            }
            
            return result;
        }

        private string GenerateDateString()
        {
            var dateNow = DateTime.Now;
            return dateNow.ToShortDateString() + " " + dateNow.ToShortTimeString() + ": ";
        }
    }
}
