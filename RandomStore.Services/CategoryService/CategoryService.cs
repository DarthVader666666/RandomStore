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
        private IMapper _mapper;
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
                _logger.LogError(GenerateDateString() + e.Message);
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
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return result;
        }

        public IAsyncEnumerable<CategoryGetModel> GetAllCategoriesAsync()
        {
            try
            {
                return GetCategoriesCore();
            }
            catch (Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return null;

            async IAsyncEnumerable<CategoryGetModel> GetCategoriesCore()
            {
                await foreach (var item in _repo.GetAllAsync())
                {
                    yield return _mapper.Map<CategoryGetModel>(item);
                }
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
                _logger.LogError(GenerateDateString() + e.Message);
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
                _logger.LogError(GenerateDateString() + e.Message);
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
            catch (Exception e)
            {
                _logger.LogError(GenerateDateString() + e.Message);
            }

            return result;
        }

        private string GenerateDateString()
        {
            var dateNow = DateTime.Now;
            return dateNow.ToShortDateString() + " " + dateNow.ToShortTimeString() + ": ";
        }
    }
}
