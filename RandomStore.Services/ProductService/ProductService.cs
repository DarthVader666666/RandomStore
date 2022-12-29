using AutoMapper;
using RandomStore.Repository.Repositories;
using RandomStore.Services.Models.ProductModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repo;
        private IMapper _mapper;

        public ProductService(IRepository<Product> repo, IMapper mapper)
        {
            _repo = repo is null ? throw new ArgumentNullException() : repo;
            _mapper = mapper is null ? throw new ArgumentNullException() : mapper;
        }

        public async Task<int> CreateProductAsync(ProductCreateModel productModel)
        {
            if (productModel.QuantityPerUnit is null)
            {
                throw new ArgumentException();
            }

            try
            {
                var product = _mapper.Map<Product>(productModel);

                await _repo.CreateAsync(product);
                await _repo.SaveAsync();
                return product.ProductId;
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateModel product, int id)
        {
            throw new NotImplementedException();
        }
    }
}
