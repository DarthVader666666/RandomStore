using RandomStore.Services.Models.OrderDetailsModels;

namespace RandomStore.Services
{
    public interface IOrderDetailsService
    {
        Task<int> CreateOrderDetailsAsync(OrderDetailsCreateModel orderDetailModel);

        IAsyncEnumerable<OrderDetailsGetModel> GetAllOrderDetailsAsync();

        IAsyncEnumerable<OrderDetailsGetModel> GetOrderDetailsByIdAsync(int orderId);

        Task<bool> UpdateOrderDetailsAsync(OrderDetailsUpdateModel orderDetailModel);

        Task<bool> DeleteOrderDetailsAsync(int orderId, int productId);
    }
}
