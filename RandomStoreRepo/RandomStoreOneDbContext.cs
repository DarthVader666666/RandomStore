using Microsoft.EntityFrameworkCore;
using RandomStoreRepo.Entities;

namespace RandomStoreRepo
{
    public class RandomStoreOneDbContext: DbContext
    {
        public RandomStoreOneDbContext() { }

        public RandomStoreOneDbContext(DbContextOptions<RandomStoreOneDbContext> options) : base(options)
        { 
            this.Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.UnitPrice).HasDefaultValueSql("((0))");
                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");
                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(product => product.Category)
                    .WithMany(category => category.Products)
                    .HasForeignKey(product => product.CategoryId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.HasMany(category => category.Products).
                    WithOne(product => product.Category);

                entity.Property(e => e.Picture).HasDefaultValue(null);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId);

                entity.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.ProductId);
            });
        }
    }
}