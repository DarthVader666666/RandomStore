using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.ProductRepositories
{
    public interface IProductRepository
    {
        IAsyncEnumerable<Product> GetAll();
        Task<Product> GetItemAsync(int id);
        Task<int> CreateAsync(Product item);
        Task<bool> UpdateAsync(Product item, int id);
        Task<bool> DeleteAsync(int id);
    }
}
