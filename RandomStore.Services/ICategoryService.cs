using Microsoft.AspNetCore.Http;
using RandomStore.Services.Models.CategoryModels;

namespace RandomStore.Services
{
    public interface ICategoryService
    {
        Task<int> CreateCategoryAsync(CategoryCreateModel categoryModel);

        IAsyncEnumerable<CategoryGetModel> GetAllCategoriesAsync();

        Task<CategoryGetModel> GetCategoryByIdAsync(int id);

        Task<bool> UpdateCategoryAsync(CategoryUpdateModel categoryModel, int id);

        Task<bool> UpdatePictureAsync(IFormFile formFile, int id);

        Task<bool> DeleteCategoryAsync(int id);
    }
}
