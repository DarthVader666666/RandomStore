using Microsoft.EntityFrameworkCore;
using RandomStore.Repository.Context;
using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.OrderRepositories
{
    public class RandomStoreOrderRepository : IOrderRepository
    {
        private readonly RandomStoreOneDbContext _context;

        public RandomStoreOrderRepository(RandomStoreOneDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Order item)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(c => c.OrderId == item.OrderId);

            if (order == null)
            {
                return 0;
            }

            await _context.Orders.AddAsync(order);
            await SaveAsync();
            return order.OrderId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

            if (order is null)
            {
                return false;
            }
            else
            {
                _context.Orders.Remove(order);
                await SaveAsync();
                return true;
            }
        }

        public async IAsyncEnumerable<Order> GetAllAsync()
        {
            var orders = _context.Orders;

            await foreach (var item in orders)
            {
                yield return item;
            }
        }

        public async Task<Order> GetItemAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

            return order;
        }

        public async Task<bool> UpdateAsync(Order item, int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return false;
            }

            order.OrderDate = item.OrderDate;
            order.ShipAddress = item.ShipAddress;
            order.ShipCity = item.ShipCity;
            order.ShipCountry= item.ShipCountry;

            _context.Orders.Entry(order).State = EntityState.Modified;
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
