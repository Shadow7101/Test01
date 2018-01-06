using App1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Data.MsSql.Map
{
    internal class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .HasColumnName("RoleId")
                .IsRequired();

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
