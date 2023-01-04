using Microsoft.EntityFrameworkCore;
using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Context
{
    public class RandomStoreOneDbContext : DbContext
    {
        public RandomStoreOneDbContext() { }

        public RandomStoreOneDbContext(DbContextOptions<RandomStoreOneDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // конфигурации в отдельн папку
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
        }
    }
}