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
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasOne(x => x.Packing).WithMany(x => x.CartItems)
                                       .HasForeignKey(x => x.PackingId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User).WithMany(x => x.CartItems)
                                       .HasForeignKey(x => x.UserId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Quantity)
                        .IsRequired();

        }
    }
}
