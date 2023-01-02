using RandomStore.Services.Models.ProductModels;

namespace RandomStore.Services
{
    public interface IProductService
    {
        Task<int> CreateProductAsync(ProductCreateModel productModel, int categoryId);

        IAsyncEnumerable<ProductGetModel> GetAllProductsAsync();

        Task<ProductGetModel> GetProductByIdAsync(int productId);

        Task<bool> UpdateProductAsync(ProductUpdateModel productModel, int productId);

        Task<bool> DeleteProductAsync(int id);
    }
}