using App1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Data.MsSql.Map
{
    internal class LogTypeMap : IEntityTypeConfiguration<LogType>
    {
        public void Configure(EntityTypeBuilder<LogType> builder)
        {
            builder.ToTable("LogTypes");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .HasColumnName("LogTypeId")
                .IsRequired();

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
