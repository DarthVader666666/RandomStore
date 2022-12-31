using AutoMapper;
using Microsoft.Extensions.Logging;
using RandomStore.Repository.Repositories.CategoryRepositories;
using RandomStore.Services.Models.CategoryModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private IMapper _createMapper;
        private IMapper _updateMapper;
        private readonly ILogger _logger;

        public CategoryService(ICategoryRepository repo, IMapper createMapper, IMapper updateMapper, ILogger logger) 
        { 
            _repo = repo;
            _createMapper = createMapper;
            _updateMapper = updateMapper;
            _logger = logger;
        }

        public async Task<int> CreateCategoryAsync(CategoryCreateModel categoryModel)
        {
            var category = _createMapper.Map<Category>(categoryModel);

            try
            {
                await _repo.CreateAsync(category);               
            }
            catch 
            {
            
            }

            return category.CategoryId;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            bool result = false;

            try
            {
                result = await _repo.DeleteAsync(id);
            }
            catch
            {

            }

            return result;
        }

        public IAsyncEnumerable<Category> GetAllCategorysAsync()
        {
            return _repo.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _repo.GetItemAsync(id);
            return category;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryUpdateModel categoryModel, int id)
        {
            var category = _updateMapper.Map<Category>(categoryModel);
            var result = false;

            try
            {
                result = await _repo.UpdateAsync(category, id);
            }
            catch
            {

            }

            return result;            
        }

        public async Task<bool> UploadPictureAsync(Stream stream, int id)
        {
            var result = false;

            try
            {
                result = await _repo.UploadPicture(stream, id);
            }
            catch
            {

            }

            return result;
        }
    }
}
