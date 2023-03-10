using Microsoft.EntityFrameworkCore;
using RandomStore.Repository.Context;
using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Repositories.CategoryRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RandomStoreOneDbContext _context;

        public CategoryRepository(RandomStoreOneDbContext context)
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

        public IAsyncEnumerable<Category> GetAll() => _context.Categories.AsAsyncEnumerable();

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

            category.CategoryName = item.CategoryName;
            category.Description = item.Description;

            _context.Entry(category).State = EntityState.Modified;
            await SaveAsync();

            return true;
        }

        public async Task<bool> SavePictureAsync(Stream stream, int id)
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
                _context.Entry(category).State = EntityState.Modified;
                await SaveAsync();
            }

            return true;
        }

        private async Task<int> SaveAsync()
        { 
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
