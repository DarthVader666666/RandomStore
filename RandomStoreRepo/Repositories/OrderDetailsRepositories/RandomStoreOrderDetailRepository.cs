using Microsoft.EntityFrameworkCore;
using RandomStore.Repository.Context;
using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.OrderDetailsRepositories
{
    public class RandomStoreOrderDetailRepository : IOrderDetailRepository
    {
        private readonly RandomStoreOneDbContext _context;

        public RandomStoreOrderDetailRepository(RandomStoreOneDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(OrderDetail item)
        {
            await _context.OrderDetails.AddAsync(item);
            await SaveAsync();
            return item.OrderId;
        }

        public async Task<bool> DeleteAsync(int orderId, int productId)
        {
            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(od => 
            od.OrderId == orderId && od.ProductId == productId);

            if (orderDetail is null)
            {
                return false;
            }
            else
            {
                _context.OrderDetails.Remove(orderDetail);
                await SaveAsync();
                return true;
            }

        }

        public async IAsyncEnumerable<OrderDetail> GetAllAsync()
        {
            var orderDetails = _context.OrderDetails;

            await foreach (var item in orderDetails)
            {
                yield return item;
            }
        }

        public async IAsyncEnumerable<OrderDetail> GetItemsAsync(int id)
        {
            var orderDetails = _context.OrderDetails.Where(od => od.OrderId == id).AsAsyncEnumerable();

            await foreach (var item in orderDetails)
            {
                yield return item;
            }
        }

        public async Task<bool> UpdateAsync(OrderDetail item, int orderId, int productId)
        {
            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(od => 
            od.OrderId == orderId && od.ProductId == productId);

            if (orderDetail == null)
            {
                return false;
            }

            orderDetail.Quantity = item.Quantity;

            _context.OrderDetails.Entry(orderDetail).State = EntityState.Modified;
            await SaveAsync();

            return true;
        }

        private async Task<int> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
