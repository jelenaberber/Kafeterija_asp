using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class ContainerConfiguration : IEntityTypeConfiguration<Domain.Container>
    {
        public void Configure(EntityTypeBuilder<Domain.Container> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);

            builder.HasIndex(x => new { x.Name });
        }
    }
}
