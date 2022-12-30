using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RandomStoreRepo.Entities;

namespace RandomStore.Repository.Context
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);
            builder.HasMany(category => category.Products).
                WithOne(product => product.Category);

            builder.Property(c => c.Picture).HasDefaultValue(null);
        }
    }
}
