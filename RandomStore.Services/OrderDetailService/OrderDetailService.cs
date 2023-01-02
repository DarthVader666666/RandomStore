﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using RandomStore.Repository.Repositories.OrderDetailsRepositories;
using RandomStore.Repository.Repositories.OrderRepositories;
using RandomStore.Repository.Repositories.ProductRepositories;
using RandomStore.Services.Models.CategoryModels;
using RandomStore.Services.Models.OrderDetailModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services.OrderDetailService
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private IMapper _mapper;
        private readonly ILogger _logger;

        public OrderDetailService(IOrderDetailRepository orderDetailRepo, IOrderRepository orderRepo, 
            IProductRepository productRepo, IMapper mapper, ILogger logger)
        { 
            _orderDetailRepo = orderDetailRepo;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateOrderDetailAsync(OrderDetailCreateModel orderDetailModel)
        {
            if (orderDetailModel.Quantity <= 0)
            {
                _logger.LogError(GenerateDateString() + "Wronq Quantity.");
                return 0;
            }

            try
            {
                var orderDetail = _mapper.Map<OrderDetail>(orderDetailModel);

                await _orderDetailRepo.CreateAsync(orderDetail);
                return orderDetail.OrderId;
            }
            catch (Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return 0;
        }

        public async Task<bool> DeleteOrderDetailAsync(int orderId, int productId)
        {
            bool result = false;

            try
            {
                result = await _orderDetailRepo.DeleteAsync(orderId, productId);
            }
            catch (Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return result;
        }

        public IAsyncEnumerable<OrderDetailGetModel> GetAllOrderDetailsAsync()
        {
            try
            {
                return GetAllOrderDetailsCore();
            }
            catch (Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return null;

            async IAsyncEnumerable<OrderDetailGetModel> GetAllOrderDetailsCore()
            {
                await foreach (var item in _orderDetailRepo.GetAllAsync())
                {
                    yield return _mapper.Map<OrderDetailGetModel>(item);
                }
            }
        }

        public IAsyncEnumerable<OrderDetailGetModel> GetOrderDetailsByIdAsync(int orderId)
        {
            if (orderId < 1)
            {
                _logger.LogError(GenerateDateString() + "Wrong id.");
                return null;
            }

            try
            {
                return GetItemsCore();
            }
            catch (Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return null;

            async IAsyncEnumerable<OrderDetailGetModel> GetItemsCore()
            {
                await foreach (var item in _orderDetailRepo.GetItemsAsync(orderId))
                {
                    yield return _mapper.Map<OrderDetailGetModel>(item);
                }
            }
        }

        public async Task<bool> UpdateOrderDetailAsync(OrderDetailUpdateModel orderDetailModel, 
            int orderId, int productId)
        {
            if (orderId < 1)
            {
                _logger.LogError(GenerateDateString() + "Wrong Id.");
                return false;
            }

            var order = await _orderRepo.GetItemAsync(orderId);
            var product = await _productRepo.GetItemAsync(productId);

            if (order == null || product == null)
            {
                _logger.LogError(GenerateDateString() + "Wrong OrderId or ProductId.");
                return false;
            }

            bool result = false;

            try
            {
                var orderDetail = _mapper.Map<OrderDetail>(orderDetailModel);
                result = await _orderDetailRepo.UpdateAsync(orderDetail, orderId, productId);
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
