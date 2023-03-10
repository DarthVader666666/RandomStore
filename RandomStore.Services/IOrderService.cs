using RandomStore.Services.Models.OrderModels;

namespace RandomStore.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(OrderCreateModel orderModel);

        IAsyncEnumerable<OrderGetModel> GetAllOrdersAsync();

        Task<OrderGetModel> GetOrderByIdAsync(int id);

        Task<bool> UpdateOrderAsync(OrderUpdateModel orderModel, int id);

        Task<bool> DeleteOrderAsync(int id);
    }
}
