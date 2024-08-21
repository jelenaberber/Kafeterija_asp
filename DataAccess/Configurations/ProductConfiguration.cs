using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(40);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(150);

            builder.HasOne(x => x.Category).WithMany(x => x.Products)
                                       .HasForeignKey(x => x.CategoryId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.Name });
        }
    }
}
