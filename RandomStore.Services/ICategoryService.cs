using RandomStore.Services.Models.CategoryModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services
{
    public interface ICategoryService
    {
        Task<int> CreateCategoryAsync(CategoryCreateModel categoryModel);

        IAsyncEnumerable<Category> GetAllCategorysAsync();

        Task<Category> GetCategoryByIdAsync(int id);

        Task<bool> UpdateCategoryAsync(CategoryUpdateModel categoryModel, int id);

        Task<bool> UploadPictureAsync(Stream stream, int id);

        Task<bool> DeleteCategoryAsync(int id);
    }
}
