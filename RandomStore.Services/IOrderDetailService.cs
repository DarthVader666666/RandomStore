using RandomStore.Services.Models.OrderDetailModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services
{
    public interface IOrderDetailService
    {
        Task<int> CreateOrderDetailAsync(OrderDetailCreateModel orderDetailModel);

        IAsyncEnumerable<OrderDetail> GetAllOrderDetailsAsync();

        IAsyncEnumerable<OrderDetail> GetOrderDetailsByIdAsync(int orderId);

        Task<bool> UpdateOrderDetailAsync(OrderDetailUpdateModel orderDetailModel, int orderId, int productId);

        Task<bool> DeleteOrderDetailAsync(int orderId, int productId);
    }
}
