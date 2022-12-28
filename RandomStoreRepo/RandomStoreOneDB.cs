using Microsoft.EntityFrameworkCore;

namespace RandomStoreRepo
{
    public class RandomStoreOneDB: DbContext
    {
        public RandomStoreOneDB(DbContextOptions<RandomStoreOneDB> options) : base(options)
        { 
            this.Database.EnsureCreated();
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; } 
        public virtual DbSet<Order> Orders { get; set; }
    }
}