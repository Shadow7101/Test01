using App1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Data.MsSql.Map
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Email).HasMaxLength(120).IsRequired();

            builder.Property(p => p.Password).HasMaxLength(100).IsRequired();

            builder.Property(p => p.BirthDate).HasColumnType("datetime").IsRequired();

            builder.Property(p => p.GenderId).IsRequired();

            builder.HasOne(p => p.Gender).WithMany().HasForeignKey(p => p.GenderId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.RoleId).IsRequired();

            builder.HasOne(p => p.Role).WithMany().HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Avaliation).HasColumnType("decimal(5,2)").IsRequired();

            builder.Property(p => p.EmailValidaded).IsRequired();

            //LastAccess
            builder.Property(p => p.LastAccess).HasColumnType("datetime");

            builder.Property(p => p.LastAccessIp).HasMaxLength(30);

            //Bloqued
            builder.Property(p => p.Bloqued).IsRequired();

            builder.Property(p => p.BloquedOn).HasColumnType("datetime");

            builder.Property(p => p.BloquedIp).HasMaxLength(30);

            builder.Property(p => p.BloquedBy);

            //banned
            builder.Property(p => p.Banned).IsRequired();

            builder.Property(p => p.BannedOn).HasColumnType("datetime");

            builder.Property(p => p.BannedIp).HasMaxLength(30);

            builder.Property(p => p.BannedBy);

            //created by
            builder.Property(p => p.CreatedOn).HasColumnType("datetime").IsRequired();

            builder.Property(p => p.CreatedIp).HasMaxLength(30).IsRequired();

            builder.Property(p => p.CreatedBy);

            //modify
            builder.Property(p => p.ModifyIp).HasMaxLength(30);

            builder.Property(p => p.ModifyBy);

            builder.Property(p => p.ModifyOn).HasColumnType("datetime");

            //deleted
            builder.Property(p => p.DeletedOn).HasColumnType("datetime");

            builder.Property(p => p.DeletedIp).HasMaxLength(30);

            builder.Property(p => p.DeletedBy);

            builder.Property(p => p.Deleted).IsRequired();
        }
    }
}
