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
            return product.ProductId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateAsync(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
