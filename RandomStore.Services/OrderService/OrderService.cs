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
        private IMapper _mapper;
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
                _logger.LogError(GenerateDateString() + "Parameter is null");
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
                _logger.LogError(GenerateDateString() + e.Message);
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
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return result;
        }

        public IAsyncEnumerable<Order> GetAllOrdersAsync()
        {
            return _repo.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            if (id < 1)
            {
                _logger.LogError(GenerateDateString() + "Wrong id.");
                return null;
            }

            try
            {
                var product = await _repo.GetItemAsync(id);
                return product;
            }
            catch (Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return null;
        }

        public async Task<bool> UpdateOrderAsync(OrderUpdateModel orderModel, int id)
        {
            if (id < 1)
            {
                _logger.LogError(GenerateDateString() + "Wrong Id.");
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
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return result;
        }

        private string GenerateDateString()
        {
            var dateNow = DateTime.Now;
            return dateNow.ToShortDateString() + " " + dateNow.ToShortTimeString() + $": {this.GetType().Name} - ";
        }
    }
}
