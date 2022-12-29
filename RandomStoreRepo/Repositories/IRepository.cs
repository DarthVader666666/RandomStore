using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomStore.Repository.Repositories
{
    public interface IRepository<T> where T: class
    {
        IAsyncEnumerable<T> GetAllAsync();
        Task<T> GetItemAsync(int id);
        Task<int> CreateAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<int> SaveAsync();
    }
}
