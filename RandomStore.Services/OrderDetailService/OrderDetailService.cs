using AutoMapper;
using Microsoft.Extensions.Logging;
using RandomStore.Repository.Repositories.OrderDetailsRepositories;
using RandomStore.Repository.Repositories.OrderRepositories;
using RandomStore.Repository.Repositories.ProductRepositories;
using RandomStore.Services.Models.OrderDetailsModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services.OrderDetailService
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailRepository _orderDetailRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrderDetailsService(IOrderDetailRepository orderDetailRepo, IOrderRepository orderRepo, 
            IProductRepository productRepo, IMapper mapper, ILogger logger)
        { 
            _orderDetailRepo = orderDetailRepo;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateOrderDetailsAsync(OrderDetailsCreateModel orderDetailModel)
        {
            if (orderDetailModel.Quantity <= 0)
            {
                _logger.LogError($"{GetType().Name}, Wrong Quantity.");
                return 0;
            }

            try
            {
                var orderDetail = _mapper.Map<OrderDetails>(orderDetailModel);

                await _orderDetailRepo.CreateAsync(orderDetail);
                return orderDetail.OrderId;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{GetType().Name}, {e.Message}");
            }

            return 0;
        }

        public async Task<bool> DeleteOrderDetailsAsync(int orderId, int productId)
        {
            var result = false;

            try
            {
                result = await _orderDetailRepo.DeleteAsync(orderId, productId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{GetType().Name}, {e.Message}");
            }

            return result;
        }

        public async IAsyncEnumerable<OrderDetailsGetModel> GetAllOrderDetailsAsync()
        {
            await foreach (var item in _orderDetailRepo.GetAll())
            {
                yield return _mapper.Map<OrderDetailsGetModel>(item);
            }
        }

        public async IAsyncEnumerable<OrderDetailsGetModel> GetOrderDetailsByIdAsync(int orderId)
        {
            await foreach (var item in _orderDetailRepo.GetItems(orderId))
            {
                yield return _mapper.Map<OrderDetailsGetModel>(item);
            }
        }

        public async Task<bool> UpdateOrderDetailsAsync(OrderDetailsUpdateModel orderDetailsModel)
        {
            if (orderDetailsModel.OrderId < 1)
            {
                _logger.LogError($"{GetType().Name}, Wrong Id.");
                return false;
            }

            var order = await _orderRepo.GetItemAsync(orderDetailsModel.OrderId);
            var product = await _productRepo.GetItemAsync(orderDetailsModel.ProductId);

            if (order == null || product == null)
            {
                _logger.LogError($"{GetType().Name}, Wrong OrderId or ProductId.");
                return false;
            }

            var result = false;

            try
            {
                var orderDetail = _mapper.Map<OrderDetails>(orderDetailsModel);
                result = await _orderDetailRepo.UpdateAsync(orderDetail, orderDetailsModel.OrderId, 
                    orderDetailsModel.ProductId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{GetType().Name}, {e.Message}");
            }

            return result;
        }
    }
}
