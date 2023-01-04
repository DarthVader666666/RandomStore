using Microsoft.EntityFrameworkCore;
using RandomStore.Repository.Context;
using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.OrderDetailsRepositories
{
    public class OrderDetailsRepository : IOrderDetailRepository
    {
        private readonly RandomStoreOneDbContext _context;

        public OrderDetailsRepository(RandomStoreOneDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(OrderDetails item)
        {
            await _context.OrderDetails.AddAsync(item);
            await SaveAsync();

            return item.OrderId;
        }

        public async Task<bool> DeleteAsync(int orderId, int productId)
        {
            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);

            if (orderDetail == null)
            {
                return false;
            }

            _context.OrderDetails.Remove(orderDetail);
            await SaveAsync();

            return true;
        }

        public IAsyncEnumerable<OrderDetails> GetAll() => _context.OrderDetails.AsAsyncEnumerable();

        public IAsyncEnumerable<OrderDetails> GetItems(int orderId) =>
            _context.OrderDetails.Where(od => od.OrderId == orderId).AsAsyncEnumerable();

        public async Task<bool> UpdateAsync(OrderDetails item, int orderId, int productId)
        {
            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);

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
