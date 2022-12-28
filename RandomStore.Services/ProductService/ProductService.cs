using RandomStoreRepo;
using RandomStoreRepo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomStore.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly RandomStoreOneDB _context;

        public ProductService(RandomStoreOneDB context)
        {
            this._context = context is not null ? context : throw new ArgumentNullException();
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            try
            {
                await this._context.Products.AddAsync(product);
                await this._context.SaveChangesAsync();
                return product.ProductId;
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProductAsync(Product product, int id)
        {
            throw new NotImplementedException();
        }
    }
}
