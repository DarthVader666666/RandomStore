using RandomStore.Services.Models.OrderModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(OrderCreateModel orderModel);

        IAsyncEnumerable<Order> GetAllOrdersAsync();

        Task<Order> GetOrderByIdAsync(int id);

        Task<bool> UpdateOrderAsync(OrderUpdateModel orderModel, int id);

        Task<bool> DeleteOrderAsync(int id);
    }
}
