using RandomStore.Services.Models.ProductModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services
{
    public interface IProductService
    {
        Task<int> CreateProductAsync(ProductCreateModel productModel);

        IAsyncEnumerable<Product> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<bool> UpdateProductAsync(ProductUpdateModel productModel, int id);

        Task<bool> DeleteProductAsync(int id);
    }
}