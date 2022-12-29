using RandomStore.Services.Models.ProductModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services
{
    public interface IProductService
    {
        Task<int> CreateProductAsync(ProductCreateModel productModel);

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<Product> GetProductByNameAsync(string name);

        Task<bool> UpdateProductAsync(ProductUpdateModel productModel, int id);

        Task<bool> DeleteProductAsync(int id);
    }
}