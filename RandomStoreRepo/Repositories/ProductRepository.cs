using Microsoft.EntityFrameworkCore;
using RandomStoreRepo;
using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        RandomStoreOneDbContext _context;

        public ProductRepository(RandomStoreOneDbContext context)
        {
            _context = context is null ? throw new ArgumentNullException() : context;
        }

        public async Task<int> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await SaveAsync();
            return product.ProductId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product is null)
            {
                return false;
            }
            else
            {
                _context.Products.Remove(product);
                await SaveAsync();
                return true;
            }
        }

        public async IAsyncEnumerable<Product> GetAllAsync()
        {
            var products = _context.Products;

            await foreach (var item in products)
            {
                yield return item;
            }
        }

        public async Task<Product?> GetItemAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            return product;
        }

        public async Task<int> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateAsync(Product item)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == item.ProductId);

            if (product is null)
            {
                return false;
            }
            else
            { 
                _context.Products.Entry(item).State = EntityState.Modified;
                await SaveAsync();
                return true;
            }
        }
    }
}
