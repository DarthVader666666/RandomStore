using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RandomStoreRepo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RandomStore.Repository.Context
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.UnitPrice).HasDefaultValueSql("((0))");
            builder.Property(p => p.UnitsInStock).HasDefaultValueSql("((0))");
            builder.Property(p => p.UnitsOnOrder).HasDefaultValueSql("((0))");

            builder.HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId);
        }
    }
}
