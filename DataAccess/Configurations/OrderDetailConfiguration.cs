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
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasOne(x => x.Order).WithMany(x => x.OrderDetails)
                                       .HasForeignKey(x => x.OrderId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Packing).WithMany(x => x.OrderDetails)
                                       .HasForeignKey(x => x.PackingId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Quantity)
                        .IsRequired();
        }
    }
}
