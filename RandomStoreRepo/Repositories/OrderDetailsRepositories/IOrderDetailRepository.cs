using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.OrderDetailsRepositories
{
    public interface IOrderDetailRepository
    {
        IAsyncEnumerable<OrderDetails> GetAll();
        IAsyncEnumerable<OrderDetails> GetItems(int orderId);
        Task<int> CreateAsync(OrderDetails item);
        Task<bool> UpdateAsync(OrderDetails item, int orderId, int productId);
        Task<bool> DeleteAsync(int orderId, int productId);
    }
}
