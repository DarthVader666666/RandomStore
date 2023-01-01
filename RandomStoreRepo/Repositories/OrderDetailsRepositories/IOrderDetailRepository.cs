using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.OrderDetailsRepositories
{
    public interface IOrderDetailRepository
    {
        IAsyncEnumerable<OrderDetail> GetAllAsync();
        IAsyncEnumerable<OrderDetail> GetItemsAsync(int orderId);
        Task<int> CreateAsync(OrderDetail item);
        Task<bool> UpdateAsync(OrderDetail item, int orderId, int productId);
        Task<bool> DeleteAsync(int orderId, int productId);
    }
}
