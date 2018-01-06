using App1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Data.MsSql.Map
{
    internal class GenderMap : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Genders");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .HasColumnName("GenderId")
                .IsRequired();

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
