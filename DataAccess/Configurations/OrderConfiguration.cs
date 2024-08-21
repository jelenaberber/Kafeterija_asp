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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.Property(x => x.Email)
                   .HasMaxLength(60)
                   .IsRequired();
            builder.HasIndex(x => x.Email)
                   .IsUnique();

            builder.Property(x => x.FullName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasIndex(x => x.FullName)
                   .IsUnique();

            builder.Property(x => x.Address)
                   .HasMaxLength(60)
                   .IsRequired();

            builder.Property(x => x.City)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(x => x.FullPrice)
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.User).WithMany(x => x.Orders)
                                       .HasForeignKey(x => x.UserId)
                                       .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
