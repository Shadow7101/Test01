using App1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App1.Data.MsSql.Map
{
    internal class TokenMap : IEntityTypeConfiguration<Token>
    {
       public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.ToTable("Tokens");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .HasColumnName("TokenId")
                .IsRequired();

            builder.Property(p => p.CreatedOn).HasColumnType("datetime").IsRequired();

            builder.Property(p => p.CreatedBy).IsRequired();

            builder.HasOne(X => X.User).WithMany().HasForeignKey(X => X.CreatedBy).OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CreatedIp).HasMaxLength(30).IsRequired();

            builder.Property(p => p.TypeId).IsRequired();

            builder.HasOne(x => x.Type).WithMany().HasForeignKey(x => x.TypeId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Validate).IsRequired();

            builder.Property(p => p.TokenData).HasColumnType("text");
        }
    }
}
