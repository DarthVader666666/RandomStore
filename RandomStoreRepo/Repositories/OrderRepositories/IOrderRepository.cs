using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.OrderRepositories
{
    public interface IOrderRepository
    {
        IAsyncEnumerable<Order> GetAllAsync();
        Task<Order> GetItemAsync(int id);
        Task<int> CreateAsync(Order item);
        Task<bool> UpdateAsync(Order item, int id);
        Task<bool> DeleteAsync(int id);
    }
}
