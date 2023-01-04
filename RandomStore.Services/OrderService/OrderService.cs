using AutoMapper;
using Microsoft.Extensions.Logging;
using RandomStore.Repository.Repositories.OrderRepositories;
using RandomStore.Services.Models.OrderModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrderService(IOrderRepository repo, IMapper mapper, ILogger logger) 
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateOrderAsync(OrderCreateModel orderModel)
        {
            if (orderModel.ShipCountry == null || orderModel.ShipCity == null || orderModel.ShipAddress == null)
            {
                _logger.LogError("Parameter is null");
                return 0;
            }

            try
            {
                var order = _mapper.Map<Order>(orderModel);
                await _repo.CreateAsync(order);

                return order.OrderId;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return 0;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            bool result = false;

            try
            {
                result = await _repo.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return result;
        }

        public async IAsyncEnumerable<OrderGetModel> GetAllOrdersAsync()
        {
            await foreach (var item in _repo.GetAll())
            {
                yield return _mapper.Map<OrderGetModel>(item);
            }
        }

        public async Task<OrderGetModel> GetOrderByIdAsync(int id)
        {
            if (id < 1)
            {
                _logger.LogError("Wrong id.");
                return null;
            }

            try
            {
                var product = await _repo.GetItemAsync(id);
                return _mapper.Map<OrderGetModel>(product);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return null;
        }

        public async Task<bool> UpdateOrderAsync(OrderUpdateModel orderModel, int id)
        {
            if (id < 1)
            {
                _logger.LogError("Wrong Id.");
                return false;
            }

            bool result = false;

            try
            {
                var order = _mapper.Map<Order>(orderModel);
                result = await _repo.UpdateAsync(order, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return result;
        }
    }
}
