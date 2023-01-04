using Microsoft.EntityFrameworkCore;
using RandomStore.Repository.Context;
using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.OrderRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RandomStoreOneDbContext _context;

        public OrderRepository(RandomStoreOneDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Order order)
        {
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

            _context.Orders.Remove(order);
            await SaveAsync();

            return true;
        }

        public IAsyncEnumerable<Order> GetAll() => _context.Orders.AsAsyncEnumerable();

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
