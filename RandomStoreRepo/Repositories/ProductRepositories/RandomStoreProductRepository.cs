using Microsoft.EntityFrameworkCore;
using RandomStore.Repository.Context;
using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.ProductRepositories
{
    public class RandomStoreProductRepository : IProductRepository
    {
        RandomStoreOneDbContext _context;

        public RandomStoreProductRepository(RandomStoreOneDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Product product)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == product.CategoryId);

            if (category == null)
            {
                return 0;
            }

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

        public async Task<Product> GetItemAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            return product;
        }

        public async Task<bool> UpdateAsync(Product item, int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == item.CategoryId);

            if (product == null || category == null)
            {
                return false;
            }

            product.ProductName = item.ProductName;
            product.CategoryId = item.CategoryId;
            product.QuantityPerUnit = item.QuantityPerUnit;
            product.UnitsOnOrder = item.UnitsOnOrder;
            product.UnitsInStock = item.UnitsInStock;

            _context.Products.Entry(product).State = EntityState.Modified;
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
