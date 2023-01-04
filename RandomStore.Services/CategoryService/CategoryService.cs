using AutoMapper;
using Microsoft.Extensions.Logging;
using RandomStore.Repository.Repositories.CategoryRepositories;
using RandomStore.Services.Models.CategoryModels;
using RandomStoreRepo.Entities;
using Microsoft.AspNetCore.Http;

namespace RandomStore.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CategoryService(ICategoryRepository repo, IMapper mapper, ILogger logger) 
        { 
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateCategoryAsync(CategoryCreateModel categoryModel)
        {
            var category = _mapper.Map<Category>(categoryModel);

            try
            {
                await _repo.CreateAsync(category);               
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{GetType().Name}, {e.Message}");
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
            catch (Exception e)
            {
                _logger.LogError(e, $"{GetType().Name}, {e.Message}");
            }

            return result;
        }

        public async IAsyncEnumerable<CategoryGetModel> GetAllCategoriesAsync()
        {
            await foreach (var item in _repo.GetAll())
            {
                yield return _mapper.Map<CategoryGetModel>(item);
            }
        }

        public async Task<CategoryGetModel> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _repo.GetItemAsync(id);
                return _mapper.Map<CategoryGetModel>(category);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{GetType().Name}, {e.Message}");
            }

            return null;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryUpdateModel categoryModel, int id)
        {
            var category = _mapper.Map<Category>(categoryModel);
            var result = false;

            try
            {
                result = await _repo.UpdateAsync(category, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{GetType().Name}, {e.Message}");
            }

            return result;            
        }

        public async Task<bool> UpdatePictureAsync(IFormFile formFile, int id)
        {
            var result = false;

            try
            {
                using (var stream = formFile.OpenReadStream())
                {
                    result = await _repo.SavePictureAsync(stream, id);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{GetType().Name}, {e.Message}");
            }

            return result;
        }
    }
}
