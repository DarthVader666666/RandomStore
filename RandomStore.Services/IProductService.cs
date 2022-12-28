using RandomStoreRepo.Entities;

namespace RandomStore.Services
{
    public interface IProductService
    {
        Task<int> CreateProductAsync(Product product);

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<Product> GetProductByNameAsync(string name);

        Task<bool> UpdateProductAsync(Product product, int id);

        Task<bool> DeleteProductAsync(int id);
    }
}