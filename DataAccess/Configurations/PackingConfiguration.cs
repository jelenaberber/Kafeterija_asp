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
    public class PackingConfiguration : IEntityTypeConfiguration<Packing>
    {
        public void Configure(EntityTypeBuilder<Packing> builder)
        {
            builder.HasOne(x => x.Product).WithMany(x => x.Packings)
                                       .HasForeignKey(x => x.ProductId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Container).WithMany(x => x.Packings)
                                       .HasForeignKey(x => x.ContainerId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Quantity)
                        .IsRequired();

            builder.Property(x => x.Price)
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

            builder.Property(x => x.UnitOfMeasurement)
                        .IsRequired()
                        .HasMaxLength(50);

            builder.Property(x => x.InFocus).HasDefaultValue(true);
        }
    }
}
