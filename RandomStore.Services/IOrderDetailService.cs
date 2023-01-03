using RandomStore.Services.Models.OrderDetailModels;

namespace RandomStore.Services
{
    public interface IOrderDetailService
    {
        Task<int> CreateOrderDetailAsync(OrderDetailCreateModel orderDetailModel);

        IAsyncEnumerable<OrderDetailGetModel> GetAllOrderDetailsAsync();

        IAsyncEnumerable<OrderDetailGetModel> GetOrderDetailsByIdAsync(int orderId);

        Task<bool> UpdateOrderDetailAsync(OrderDetailUpdateModel orderDetailModel, int orderId, int productId);

        Task<bool> DeleteOrderDetailAsync(int orderId, int productId);
    }
}
