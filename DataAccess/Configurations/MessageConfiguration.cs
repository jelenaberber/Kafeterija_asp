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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
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

            builder.Property(x => x.Text)
                  .HasMaxLength(300)
                  .IsRequired();

        }
    }
}
