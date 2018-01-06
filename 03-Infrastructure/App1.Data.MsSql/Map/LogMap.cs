using App1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Data.MsSql.Map
{
    internal class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .HasAnnotation("MySql:ValueGeneratedOnAdd", true)
                .HasColumnName("LogId")
                .IsRequired();

            builder.Property(p => p.Data).HasMaxLength(300);

            builder.Property(p => p.CreatedBy);

            builder.Property(p => p.CreatedIp).HasMaxLength(30).IsRequired();

            builder.Property(p => p.CreatedOn).HasColumnType("datetime").IsRequired();

            builder.Property(p => p.LogTypeId).IsRequired();

            builder.HasOne(p => p.LogType).WithMany().HasForeignKey(p => p.LogTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
