using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories
{
    public interface IProductRepository
    {
        IAsyncEnumerable<Product> GetAllAsync();
        Task<Product> GetItemAsync(int id);
        Task<int> CreateAsync(Product item);
        Task<bool> UpdateAsync(Product item);
        Task<bool> DeleteAsync(int id);
    }
}
