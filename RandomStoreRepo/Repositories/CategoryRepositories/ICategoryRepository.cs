using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.CategoryRepositories
{
    public interface ICategoryRepository
    {
        IAsyncEnumerable<Category> GetAll();
        Task<Category> GetItemAsync(int id);
        Task<int> CreateAsync(Category item);
        Task<bool> UpdateAsync(Category item, int id);
        Task<bool> SavePictureAsync(Stream stream, int id);
        Task<bool> DeleteAsync(int id);
    }
}
