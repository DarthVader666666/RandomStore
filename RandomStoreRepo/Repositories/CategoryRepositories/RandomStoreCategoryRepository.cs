using Microsoft.EntityFrameworkCore;
using RandomStore.Repository.Context;
using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.CategoryRepositories
{
    public class RandomStoreCategoryRepository : ICategoryRepository
    {
        private readonly RandomStoreOneDbContext _context;

        public RandomStoreCategoryRepository(RandomStoreOneDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Category item)
        {
            await _context.Categories.AddAsync(item);
            await SaveAsync();

            return item.CategoryId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null) 
            {
                return false;
            }

            _context.Categories.Remove(category);
            await SaveAsync();

            return true;
        }

        public async IAsyncEnumerable<Category> GetAllAsync()
        {
            var categories = _context.Categories.AsAsyncEnumerable();

            await foreach (var item in categories)
            { 
                yield return item;
            }
        }

        public async Task<Category> GetItemAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            return category;
        }

        public async Task<bool> UpdateAsync(Category item, int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null) 
            {
                return false;
            }

            _context.Entry(item).State= EntityState.Modified;
            await SaveAsync();

            return true;
        }

        public async Task<bool> UploadPicture(Stream stream, int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return false;
            }

            using (var memory = new MemoryStream())
            {
                await stream.CopyToAsync(memory);
                category.Picture = memory.ToArray();
            }

            await SaveAsync();

            return true;
        }

        private async Task<int> SaveAsync()
        { 
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
